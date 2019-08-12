using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> that is able to decompile arbitrary methods
    /// </summary>
    public sealed class MethodDecompiler
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
        /// Decompiles a target method and returns its <see cref="SyntaxTree"/> and <see cref="SemanticModel"/> info
        /// </summary>
        /// <param name="methodInfo">The input <see cref="MethodInfo"/> to inspect</param>
        /// <param name="rootNode">The root node for the syntax tree of the input method</param>
        /// <param name="semanticModel">The semantic model for the input method</param>
        public void GetSyntaxTree(MethodInfo methodInfo, out SyntaxNode rootNode, out SemanticModel semanticModel)
        {
            lock (Lock)
            {
                // Get the handle of the input method
                GetMethodHandle(methodInfo, out string assemblyPath, out EntityHandle methodHandle);

                // Get or create a decompiler for the target assembly, and decompile the method
                if (!Decompilers.TryGetValue(assemblyPath, out CSharpDecompiler decompiler))
                {
                    decompiler = CreateDecompiler(assemblyPath);
                    Decompilers.Add(assemblyPath, decompiler);
                }

                string sourceCode = decompiler.DecompileAsString(methodHandle);

                // Load the method syntax tree
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
                rootNode = syntaxTree.GetRoot();

                // Update the incremental compilation and retrieve the syntax tree for the method
                _Compilation = _Compilation.AddSyntaxTrees(syntaxTree);
                semanticModel = _Compilation.GetSemanticModel(syntaxTree);
            }
        }

        /// <summary>
        /// Loads the assembly path and method handle for the input <see cref="MethodInfo"/> instance
        /// </summary>
        /// <param name="methodInfo">The input <see cref="MethodInfo"/> to inspect</param>
        /// <param name="assemblyPath">The path of the assembly containing the input method</param>
        /// <param name="methodHandle">The handle for the input method</param>
        private void GetMethodHandle(MethodInfo methodInfo, out string assemblyPath, out EntityHandle methodHandle)
        {
            // Try to get the method handle from the assembly of the declaring type
            Type declaringType = methodInfo.DeclaringType ?? throw new InvalidOperationException("Invalid declaring type");
            string path = declaringType.Assembly.Location;
            if (!string.IsNullOrEmpty(path))
            {
                assemblyPath = path;
                methodHandle = MetadataTokenHelpers.TryAsEntityHandle(methodInfo.MetadataToken) ?? throw new InvalidOperationException();
            }
            else
            {
                // Try to get the PEFile and type definition for the method declaring type
                if (!TypeDefinitions.TryGetValue(methodInfo.DeclaringType, out (PEFile PEFile, TypeDefinition TypeDefinition) tuple))
                {
                    foreach (PEFile peFile in PEFiles)
                    {
                        // If not available, browse through the loaded PEFiles to find the type definition
                        TypeDefinitionHandle typeDefinitionHandle = peFile.Metadata.TypeDefinitions.FirstOrDefault(t => t.GetFullTypeName(peFile.Metadata).ToString() == methodInfo.DeclaringType.FullName);

                        if (!typeDefinitionHandle.IsNil)
                        {
                            tuple = (peFile, peFile.Metadata.GetTypeDefinition(typeDefinitionHandle));
                            TypeDefinitions.Add(methodInfo.DeclaringType, tuple);

                            break;
                        }
                    }

                    if (tuple.PEFile is null) throw new InvalidOperationException($"Missing PEFile for declaring type {methodInfo.DeclaringType.FullName}");
                }

                PEFile peFileForMethod = tuple.PEFile;
                TypeDefinition typeDefinition = tuple.TypeDefinition;

                // Retrieve the assembly path and method handle from the loaded PEFile and metadata
                assemblyPath = peFileForMethod.FileName;
                methodHandle = typeDefinition.GetMethods()
                    .Where(m => peFileForMethod.Metadata.StringComparer.Equals(peFileForMethod.Metadata.GetMethodDefinition(m).Name, methodInfo.Name))
                    .First(m => peFileForMethod.Metadata.GetMethodDefinition(m).GetParameters().Count == methodInfo.GetParameters().Length);
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
