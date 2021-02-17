using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
using ComputeSharp.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static ComputeSharp.SourceGenerators.Helpers.SyntaxFactoryHelper;

#pragma warning disable CS0618

namespace ComputeSharp.SourceGenerators
{
    [Generator]
    public class IComputeShaderSourceGenerator : ISourceGenerator
    {
        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            // Find all the struct declarations
            ImmutableArray<StructDeclarationSyntax> structDeclarations = (
                from tree in context.Compilation.SyntaxTrees
                from structDeclaration in tree.GetRoot().DescendantNodes().OfType<StructDeclarationSyntax>()
                select structDeclaration).ToImmutableArray();

            foreach (StructDeclarationSyntax structDeclaration in structDeclarations)
            {
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(structDeclaration.SyntaxTree);
                INamedTypeSymbol structDeclarationSymbol = semanticModel.GetDeclaredSymbol(structDeclaration)!;

                // Only process compute shader types
                if (!structDeclarationSymbol.Interfaces.Any(static interfaceSymbol => interfaceSymbol.Name == nameof(IComputeShader))) continue;

                // We need to sets to track all discovered custom types and static methods
                HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);
                Dictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = new(SymbolEqualityComparer.Default);
                Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);

                // Explore the syntax tree and extract the processed info
                var processedMembers = GetProcessedMembers(context, structDeclarationSymbol, discoveredTypes).ToArray();
                var sharedBuffers = GetGroupSharedMembers(context, structDeclarationSymbol, discoveredTypes).ToArray();
                var (entryPoint, localFunctions) = GetProcessedMethods(structDeclaration, structDeclarationSymbol, semanticModel, discoveredTypes, staticMethods, constantDefinitions);
                var processedTypes = GetProcessedTypes(discoveredTypes).ToArray();
                var processedMethods = localFunctions.Concat(staticMethods.Values).Select(static method => method.NormalizeWhitespace().ToFullString()).ToArray();
                var processedConstants = GetProcessedConstants(constantDefinitions);

                // Create the compilation unit with the source attribute
                var source =
                    CompilationUnit().AddUsings(
                    UsingDirective(IdentifierName("ComputeSharp.__Internals"))).AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName(typeof(IComputeShaderSourceAttribute).FullName)).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(structDeclarationSymbol.GetFullMetadataName()))),
                            AttributeArgument(ArrayExpression(processedTypes)),
                            AttributeArgument(NestedArrayExpression(processedMembers)),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(entryPoint))),
                            AttributeArgument(ArrayExpression(processedMethods)),
                            AttributeArgument(NestedArrayExpression(processedConstants)),
                            AttributeArgument(NestedArrayExpression(sharedBuffers)))))
                    .WithOpenBracketToken(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.OpenBracketToken, TriviaList()))
                    .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
                    .NormalizeWhitespace()
                    .ToFullString();

                // Add the method source attribute
                context.AddSource(structDeclarationSymbol.GetGeneratedFileName<IComputeShaderSourceGenerator>(), SourceText.From(source, Encoding.UTF8));
            }
        }

        /// <summary>
        /// Gets a sequence of captured members and their mapped names.
        /// </summary>
        /// <param name="context">The current generator context in use.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <returns>A sequence of captured members in <paramref name="structDeclarationSymbol"/>.</returns>
        [Pure]
        private static IEnumerable<IEnumerable<string>> GetProcessedMembers(
            GeneratorExecutionContext context,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> types)
        {
            bool hlslResourceFound = false;

            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (fieldSymbol.IsStatic) continue;

                AttributeData? attribute = fieldSymbol.GetAttributes().FirstOrDefault(static a => a.AttributeClass is { Name: nameof(GroupSharedAttribute) });

                // Group shared fields must be static
                if (attribute is not null)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticDescriptors.InvalidGroupSharedFieldDeclaration,
                        fieldSymbol.Locations.FirstOrDefault(),
                        structDeclarationSymbol, fieldSymbol.Name));

                    continue;
                }

                // Captured fields must be named type symbols
                if (fieldSymbol.Type is not INamedTypeSymbol typeSymbol)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticDescriptors.InvalidShaderField,
                        fieldSymbol.Locations.FirstOrDefault(),
                        structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type));

                    continue;
                }

                string metadataName = typeSymbol.GetFullMetadataName();

                // Allowed fields must be either resources, unmanaged values or delegates
                if (HlslKnownTypes.IsTypedResourceType(metadataName))
                {
                    hlslResourceFound = true;

                    // Track the type of items in the current buffer
                    if (HlslKnownTypes.IsStructuredBufferType(metadataName))
                    {
                        types.Add((INamedTypeSymbol)typeSymbol.TypeArguments[0]);
                    }
                }
                else if (!typeSymbol.IsUnmanagedType &&
                         typeSymbol.TypeKind != TypeKind.Delegate)
                {
                    // Shaders can only capture valid HLSL resource types or unmanaged types
                    context.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticDescriptors.InvalidShaderField,
                        fieldSymbol.Locations.FirstOrDefault(),
                        structDeclarationSymbol, fieldSymbol.Name, typeSymbol));

                    continue;
                }

                string typeName = HlslKnownTypes.GetMappedName(typeSymbol);

                _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                // Yield back the current mapping for the name (if the name used a reserved keyword)
                yield return new[] { fieldSymbol.Name, mapping ?? fieldSymbol.Name, typeName };
            }

            if (!hlslResourceFound)
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.MissingShaderResources,
                    structDeclarationSymbol.Locations.FirstOrDefault(),
                    structDeclarationSymbol));
            }
        }

        /// <summary>
        /// Gets a sequence of captured members and their mapped names.
        /// </summary>
        /// <param name="context">The current generator context in use.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <returns>A sequence of captured members in <paramref name="structDeclarationSymbol"/>.</returns>
        [Pure]
        private static IEnumerable<(string Name, string Type, int? Count)> GetGroupSharedMembers(
            GeneratorExecutionContext context,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> types)
        {
            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (!fieldSymbol.IsStatic) continue;

                AttributeData? attribute = fieldSymbol.GetAttributes().FirstOrDefault(static a => a.AttributeClass is { Name: nameof(GroupSharedAttribute) });

                if (attribute is null) continue;

                if (fieldSymbol.Type is not IArrayTypeSymbol typeSymbol)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticDescriptors.InvalidGroupSharedFieldType,
                        fieldSymbol.Locations.FirstOrDefault(),
                        structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type));

                    continue;
                }

                if (!typeSymbol.ElementType.IsUnmanagedType)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                           DiagnosticDescriptors.InvalidGroupSharedFieldElementType,
                           fieldSymbol.Locations.FirstOrDefault(),
                           structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type));

                    continue;
                }
                
                int? bufferSize = (int?)attribute.ConstructorArguments.FirstOrDefault().Value;

                string typeName = HlslKnownTypes.GetMappedElementName(typeSymbol);

                _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                yield return (mapping ?? fieldSymbol.Name, typeName, bufferSize);

                types.Add((INamedTypeSymbol)typeSymbol.ElementType);
            }
        }

        /// <summary>
        /// Gets a sequence of processed methods declared within a given type.
        /// </summary>
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> instance for the current type.</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the type to process.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <returns>A sequence of processed methods in <paramref name="structDeclaration"/>, and the entry point.</returns>
        [Pure]
        private static (string EntryPoint, IEnumerable<SyntaxNode> Methods) GetProcessedMethods(
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            SemanticModel semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IFieldSymbol, string> constantDefinitions)
        {
            // Find all declared methods in the type
            ImmutableArray<MethodDeclarationSyntax> methodDeclarations = (
                from syntaxNode in structDeclaration.DescendantNodes()
                where syntaxNode.IsKind(SyntaxKind.MethodDeclaration)
                select (MethodDeclarationSyntax)syntaxNode).ToImmutableArray();

            string? entryPoint = null;
            List<SyntaxNode> methods = new();

            foreach (MethodDeclarationSyntax methodDeclaration in methodDeclarations)
            {
                IMethodSymbol methodDeclarationSymbol = semanticModel.GetDeclaredSymbol(methodDeclaration)!;
                ShaderSourceRewriter shaderSourceRewriter = new(structDeclarationSymbol, semanticModel, discoveredTypes, staticMethods, constantDefinitions);

                // Rewrite the method syntax tree
                MethodDeclarationSyntax? processedMethod = shaderSourceRewriter.Visit(methodDeclaration)!.WithoutTrivia();

                // If the method is the shader entry point, do additional processing
                if (methodDeclarationSymbol.Name == nameof(IComputeShader.Execute) &&
                    methodDeclarationSymbol.ReturnsVoid &&
                    methodDeclarationSymbol.TypeParameters.Length == 0 &&
                    methodDeclarationSymbol.Parameters.Length == 0)
                {
                    processedMethod = new ExecuteMethodRewriter(shaderSourceRewriter).Visit(processedMethod)!;

                    entryPoint = processedMethod.NormalizeWhitespace().ToFullString();
                }
                else methods.Add(processedMethod);

                // Emit the extracted local functions
                foreach (var localFunction in shaderSourceRewriter.LocalFunctions)
                {
                    methods.Add(localFunction.Value);
                }
            }

            return (entryPoint!, methods);
        }

        /// <summary>
        /// Gets a sequence of discovered constants.
        /// </summary>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <returns>A sequence of discovered constants to declare in the shader.</returns>
        [Pure]
        internal static IEnumerable<IEnumerable<string>> GetProcessedConstants(IReadOnlyDictionary<IFieldSymbol, string> constantDefinitions)
        {
            foreach (var constant in constantDefinitions)
            {
                var ownerTypeName = ((INamedTypeSymbol)constant.Key.ContainingSymbol).ToDisplayString().Replace(".", "__");
                var constantName = $"__{ownerTypeName}__{constant.Key.Name}";

                yield return new string[] { constantName, constant.Value };
            }
        }

        /// <summary>
        /// Gets the sequence of processed discovered custom types.
        /// </summary>
        /// <param name="types">The sequence of discovered custom types.</param>
        /// <returns>A sequence of custom type definitions to add to the shader source.</returns>
        internal static IEnumerable<string> GetProcessedTypes(IEnumerable<INamedTypeSymbol> types)
        {
            foreach (var type in HlslKnownTypes.GetCustomTypes(types))
            {
                var structType = type.GetFullMetadataName().Replace(".", "__");
                var structDeclaration = StructDeclaration(structType);

                // Declare the fields of the current type
                foreach (var field in type.GetMembers().OfType<IFieldSymbol>())
                {
                    INamedTypeSymbol fieldType = (INamedTypeSymbol)field.Type;

                    // Convert the name to the fully qualified HLSL version
                    if (!HlslKnownTypes.TryGetMappedName(fieldType.GetFullMetadataName(), out string? mapped))
                    {
                        mapped = fieldType.GetFullMetadataName().Replace(".", "__");
                    }

                    structDeclaration = structDeclaration.AddMembers(
                        FieldDeclaration(VariableDeclaration(
                            IdentifierName(mapped!)).AddVariables(
                            VariableDeclarator(Identifier(field.Name)))));
                }

                // Insert the trailing ; right after the closing bracket (after normalization)
                yield return
                    structDeclaration
                    .NormalizeWhitespace()
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                    .ToFullString();
            }
        }
    }
}

