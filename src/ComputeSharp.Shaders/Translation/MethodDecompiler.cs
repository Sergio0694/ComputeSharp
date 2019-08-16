using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
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
        /// The dummy object used to handle concurrent decompilation requests
        /// </summary>
        private readonly object Lock = new object();

        /// <summary>
        /// The mapping of available <see cref="CSharpDecompiler"/> instances targeting different assemblies
        /// </summary>
        private readonly Dictionary<string, CSharpDecompiler> Decompilers = new Dictionary<string, CSharpDecompiler>();

        /// <summary>
        /// The available <see cref="PEFile"/> instances for the loaded assemblies
        /// </summary>
        private readonly IReadOnlyList<PEFile> PEFiles;

        /// <summary>
        /// The mapping of <see cref="PEFile"/> and <see cref="TypeDefinition"/> values for the discovered <see cref="Type"/> instances
        /// </summary>
        private readonly Dictionary<Type, (PEFile PEFile, TypeDefinition TypeDefinition)> TypeDefinitions = new Dictionary<Type, (PEFile, TypeDefinition)>();

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

            // Load and store all the available PEFile instances
            PEFiles = assemblyPaths.Select(p => new PEFile(p)).ToArray();

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
        /// A <see cref="Regex"/> used to preprocess the closure type declarations for both lambda expressions and local methods
        /// </summary>
        private static readonly Regex ClosureTypeDeclarationRegex = new Regex(@"(?<=private sealed class )<\w*>[\w_]+", RegexOptions.Compiled);

        /// <summary>
        /// A <see cref="Regex"/> used to preprocess the entry point declaration for both lambda expressions and local methods
        /// </summary>
        private static readonly Regex LambdaMethodDeclarationRegex = new Regex(@"(private|internal) void <\w+>[\w_|]+(?=\()", RegexOptions.Compiled);

        /// <summary>
        /// Decompiles a target method and returns its <see cref="SyntaxTree"/> and <see cref="SemanticModel"/> info
        /// </summary>
        /// <param name="methodInfo">The input <see cref="MethodInfo"/> to inspect</param>
        /// <param name="rootNode">The root node for the syntax tree of the input method</param>
        /// <param name="semanticModel">The semantic model for the input method</param>
        public void GetSyntaxTree(MethodInfo methodInfo, out MethodDeclarationSyntax rootNode, out SemanticModel semanticModel)
        {
            lock (Lock)
            {
                // Get the handle of the containing type method
                string assemblyPath = methodInfo.DeclaringType?.Assembly.Location ?? throw new InvalidOperationException();
                EntityHandle typeHandle = MetadataTokenHelpers.TryAsEntityHandle(methodInfo.DeclaringType.MetadataToken) ?? throw new InvalidOperationException();

                // Get or create a decompiler for the target assembly, and decompile the type
                if (!Decompilers.TryGetValue(assemblyPath, out CSharpDecompiler decompiler))
                {
                    decompiler = CreateDecompiler(assemblyPath);
                    Decompilers.Add(assemblyPath, decompiler);
                }

                // Decompile the method source and fix the method declaration for local methods converted to lambdas
                string
                    sourceCode = decompiler.DecompileAsString(typeHandle),
                    typeFixedCode = ClosureTypeDeclarationRegex.Replace(sourceCode, "Shader"),
                    methodFixedCode = LambdaMethodDeclarationRegex.Replace(typeFixedCode, "internal void Main");

                // Load the type syntax tree
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(methodFixedCode);

                // Get the root node to return
                rootNode = syntaxTree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().First();

                // Update the incremental compilation and retrieve the syntax tree for the method
                _Compilation = _Compilation.AddSyntaxTrees(syntaxTree);
                semanticModel = _Compilation.GetSemanticModel(syntaxTree);
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
    }
}
