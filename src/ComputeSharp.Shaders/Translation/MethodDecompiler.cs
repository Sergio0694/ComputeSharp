using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.Shaders.Translation.Enums;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> that is able to decompile arbitrary methods
    /// </summary>
    internal sealed class MethodDecompiler
    {
        /// <summary>
        /// Gets the singleton <see cref="MethodDecompiler"/> instance to use
        /// </summary>
        public static MethodDecompiler Instance { get; } = new MethodDecompiler();

        /// <summary>
        /// The mapping of available <see cref="CSharpDecompiler"/> instances targeting different assemblies
        /// </summary>
        private readonly Dictionary<string, CSharpDecompiler> Decompilers = new Dictionary<string, CSharpDecompiler>();

        /// <summary>
        /// The incremental <see cref="Compilation"/> instance for the shader assembly
        /// </summary>
        private Compilation _Compilation;

        /// <summary>
        /// Creates a new <see cref="MethodDecompiler"/> instance
        /// </summary>
        private MethodDecompiler()
        {
            // Load the assembly paths for all the available dlls
            IReadOnlyList<string> assemblyPaths;
            if (!string.IsNullOrEmpty(Assembly.GetEntryAssembly()?.Location))
            {
                assemblyPaths = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).Select(a => a.Location).ToArray();
            }
            else
            {
                assemblyPaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll").Where(p =>
                {
                    try
                    {
                        using PEFile _ = new PEFile(p);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }).ToArray();
            }

            // Ceate an incremental shader assembly with the available references
            IEnumerable<PortableExecutableReference> metadataReferences = assemblyPaths.Select(p => MetadataReference.CreateFromFile(p));
            _Compilation = CSharpCompilation.Create("ShaderAssembly").WithReferences(metadataReferences);

            // Monitor for new assemblies being loaded
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }

        // Increment the shader assembly when a new assembly is loaded
        private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs e)
        {
            if (!e.LoadedAssembly.IsDynamic)
            {
                PortableExecutableReference metadataReference = MetadataReference.CreateFromFile(e.LoadedAssembly.Location);
                _Compilation = _Compilation.AddReferences(metadataReference);
            }
        }

        /// <summary>
        /// Creates a new <see cref="CSharpDecompiler"/> instance targeting a given assembly
        /// </summary>
        /// <param name="assemblyPath">The path of the assembly to decompile</param>
        /// <returns>A <see cref="CSharpDecompiler"/> instance that can be used on the targeted assembly</returns>
        [Pure]
        private static CSharpDecompiler CreateDecompiler(string assemblyPath)
        {
            DecompilerSettings decompilerSettings = new DecompilerSettings(ICSharpCode.Decompiler.CSharp.LanguageVersion.Latest)
            {
                ObjectOrCollectionInitializers = false,
                UsingDeclarations = false,
                CSharpFormattingOptions = { IndentationString = IndentedTextWriter.DefaultTabString }
            };

            UniversalAssemblyResolver resolver = new UniversalAssemblyResolver(assemblyPath, false, "netstandard");
            return new CSharpDecompiler(assemblyPath, resolver, decompilerSettings);
        }

        /// <summary>
        /// Decompiles a method or the whole declaring type
        /// </summary>
        /// <param name="methodInfo">The target <see cref="MethodInfo"/> to decompile</param>
        /// <param name="methodOnly">Specifies whether or not to force the decompilation of just the given method, even if not static</param>
        /// <returns>The decompiled source code</returns>
        [Pure]
        private string DecompileMethodOrDeclaringType(MethodInfo methodInfo, bool methodOnly = false)
        {
            // Get the handle of the containing type method
            string assemblyPath = methodInfo.DeclaringType?.Assembly.Location ?? throw new InvalidOperationException();
            int metadataToken = methodInfo.IsStatic || methodOnly ? methodInfo.MetadataToken : methodInfo.DeclaringType.MetadataToken;
            EntityHandle typeHandle = MetadataTokenHelpers.TryAsEntityHandle(metadataToken) ?? throw new InvalidOperationException();

            // Get or create a decompiler for the target assembly, and decompile the type
            if (!Decompilers.TryGetValue(assemblyPath, out CSharpDecompiler decompiler))
            {
                decompiler = CreateDecompiler(assemblyPath);
                Decompilers.Add(assemblyPath, decompiler);
            }

            return decompiler.DecompileAsString(typeHandle);
        }

        /// <summary>
        /// Decompiles a target method and returns its <see cref="SyntaxTree"/> and <see cref="SemanticModel"/> info
        /// </summary>
        /// <param name="methodInfo">The input <see cref="MethodInfo"/> to inspect</param>
        /// <param name="methodType">The type of method to decompile</param>
        /// <param name="rootNode">The root node for the syntax tree of the input method</param>
        /// <param name="semanticModel">The semantic model for the input method</param>
        public void GetSyntaxTree(MethodInfo methodInfo, MethodType methodType, out MethodDeclarationSyntax rootNode, out SemanticModel semanticModel)
        {
            string sourceCode = methodType switch
            {
                MethodType.Execute => GetSyntaxTreeForExecuteMethod(methodInfo),
                MethodType.Static => GetSyntaxTreeForStaticMethod(methodInfo),
                _ => throw new ArgumentOutOfRangeException(nameof(methodType), $"Invalid method type: {methodType}")
            };

            // Load the type syntax tree
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

            // Get the root node to return
            rootNode = syntaxTree
                .GetRoot()
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .First(node => node.Identifier.ToString().Equals(methodInfo.Name) ||
                               node.GetLeadingTrivia().ToFullString().Contains(methodInfo.Name));

            // Update the incremental compilation and retrieve the syntax tree for the method
            Compilation compilation = _Compilation.AddSyntaxTrees(syntaxTree);
            semanticModel = compilation.GetSemanticModel(syntaxTree);
        }

        /// <summary>
        /// Decompiles a target closure method
        /// </summary>
        /// <param name="methodInfo">The input <see cref="MethodInfo"/> to inspect</param>
        [Pure]
        private string GetSyntaxTreeForExecuteMethod(MethodInfo methodInfo)
        {
            // Decompile the method source
            string sourceCode = DecompileMethodOrDeclaringType(methodInfo);

            // Remove the in keyword from the source
            string inFixedSourceCode = Regex.Replace(sourceCode, @"(?<!\w)in ", string.Empty);

            // Tweak the out declarations
            string outFixedSourceCode = RefactorInlineOutDeclarations(inFixedSourceCode, methodInfo.Name);

            return outFixedSourceCode;
        }

        /// <summary>
        /// Decompiles a target method and returns its <see cref="SyntaxTree"/> and <see cref="SemanticModel"/> info
        /// </summary>
        /// <param name="methodInfo">The input <see cref="MethodInfo"/> to inspect</param>
        [Pure]
        private string GetSyntaxTreeForStaticMethod(MethodInfo methodInfo)
        {
            string
                sourceCode = DecompileMethodOrDeclaringType(methodInfo, true),
                methodFixedCode = sourceCode.Replace(methodInfo.Name, Regex.Replace(methodInfo.Name, "[<>|]", "_")),
                methodWithoutAttributes = CSharpSyntaxTree
                    .ParseText(methodFixedCode)
                    .GetRoot()
                    .DescendantNodes()
                    .OfType<MethodDeclarationSyntax>()
                    .First()
                    .WithAttributeLists(default)
                    .ToString(),
                prototype = methodWithoutAttributes.Split(Environment.NewLine)[0],
                commentedSourceCode = $"// {methodInfo.Name}{Environment.NewLine}{methodWithoutAttributes}",
                inFixedSourceCode = Regex.Replace(commentedSourceCode, @"(?<!\w)in ", string.Empty),
                outFixedSourceCode = RefactorInlineOutDeclarations(inFixedSourceCode, methodInfo.Name);

            // Restore the original prototype, as some out parameters might have been removed
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(outFixedSourceCode);
            BlockSyntax block = syntaxTree.GetRoot().DescendantNodes().OfType<BlockSyntax>().First();

            return $"// {methodInfo.Name}{Environment.NewLine}{prototype}{Environment.NewLine}{block.ToFullString()}";
        }

        /// <summary>
        /// Refactors all the inline out expressions with a variable declaration
        /// </summary>
        /// <param name="source">The input source code to process</param>
        /// <param name="entryPoint">The name of the shader method being processed</param>
        [Pure]
        private string RefactorInlineOutDeclarations(string source, string entryPoint)
        {
            // Replace the discards
            source = Regex.Replace(source, @"(?<!\w)out ([\w.]+) _(?!_)", m => $"out {m.Groups[1].Value} {new string(Guid.NewGuid().ToByteArray().Select(b => (char)('a' + b % 26)).ToArray())}");

            // Fix invalid C# names with @ prefix
            source = Regex.Replace(source, @"@(\w+)", m => $"__at__{m.Groups[1].Value}");

            // Load the syntax tree and the entry node
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
            MethodDeclarationSyntax rootNode = syntaxTree
                .GetRoot()
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .First(node => node.Identifier.ToString().Equals(entryPoint) ||
                               node.GetLeadingTrivia().ToFullString().Contains(entryPoint));

            // Get the out declarations to replace
            var outs = (
                from argument in rootNode.DescendantNodes().OfType<ArgumentSyntax>()
                where argument.RefKindKeyword.IsKind(SyntaxKind.OutKeyword) &&
                      argument.Expression.IsKind(SyntaxKind.DeclarationExpression)
                let match = Regex.Match(argument.Expression.ToFullString(), @"([\w.]+) ([\w_]+)")
                let mappedType = HlslKnownTypes.GetMappedName(match.Groups[1].Value)
                let declaration = $"{mappedType} {match.Groups[2].Value} = ({mappedType})0;"
                select declaration).ToArray();

            // Insert the explicit declarations at the start of the method
            int start = rootNode.Body.ChildNodes().First().SpanStart;
            foreach (var declaration in outs.Reverse())
            {
                source = source.Insert(start, $"{declaration}{Environment.NewLine}        ");
            }

            // Remove the out keyword from the source
            source = Regex.Replace(source, @"(?<!\w)out [\w.]+ (?=[\w_]+)", string.Empty); // Inline out declarations
            source = Regex.Replace(source, @"(?<!\w)out ", string.Empty); // Leftovers out keywords

            return source;
        }
    }
}
