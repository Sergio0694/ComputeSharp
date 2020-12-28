using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
using ComputeSharp.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

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

                // Helper that converts a sequence of string sequences into a nested array expression.
                // That is, this applies the following transformation:
                //   - { ("K1", "V1"), ("K2", "V2") } => new object[] { new[] { "K1", "V1" }, new[] { "K2", "V2" } }
                static ArrayCreationExpressionSyntax NestedArrayExpression(IEnumerable<IEnumerable<string>> groups)
                {
                    return
                        ArrayCreationExpression(
                        ArrayType(PredefinedType(Token(SyntaxKind.ObjectKeyword)))
                        .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                        .WithInitializer(InitializerExpression(SyntaxKind.ArrayInitializerExpression)
                        .AddExpressions(groups.Select(static group =>
                            ImplicitArrayCreationExpression(InitializerExpression(SyntaxKind.ArrayInitializerExpression).AddExpressions(
                                group.Select(static item => LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(item))).ToArray()))).ToArray()));
                }

                // Helper that converts a sequence of strings into an array expression.
                // That is, this applies the following transformation:
                //   - { "S1", "S2" } => new string[] { "S1", "S2" }
                static ArrayCreationExpressionSyntax ArrayExpression(IEnumerable<string> values)
                {
                    return
                        ArrayCreationExpression(
                        ArrayType(PredefinedType(Token(SyntaxKind.StringKeyword)))
                        .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                        .WithInitializer(InitializerExpression(SyntaxKind.ArrayInitializerExpression)
                        .AddExpressions(values.Select(static value => LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(value))).ToArray()));
                }

                // Explore the syntax tree and extract the processed info
                var processedMembers = GetProcessedMembers(structDeclarationSymbol, discoveredTypes).ToArray();
                var (entryPoint, localFunctions) = GetProcessedMethods(structDeclaration, structDeclarationSymbol, semanticModel, discoveredTypes, staticMethods);
                var processedTypes = GetProcessedTypes(discoveredTypes).ToArray();
                var processedMethods = localFunctions.Concat(staticMethods.Values.Select(static method => method.ToFullString())).ToArray();

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
                            AttributeArgument(ArrayExpression(processedMethods)))))
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
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <returns>A sequence of captured members in <paramref name="structDeclarationSymbol"/>.</returns>
        [Pure]
        private static IEnumerable<IEnumerable<string>> GetProcessedMembers(INamedTypeSymbol structDeclarationSymbol, ICollection<INamedTypeSymbol> types)
        {
            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                INamedTypeSymbol typeSymbol = (INamedTypeSymbol)fieldSymbol.Type;

                string typeName = HlslKnownTypes.GetMappedName(typeSymbol);

                _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                // Yield back the current mapping for the name (if the name used a reserved keyword)
                yield return new[] { fieldSymbol.Name, mapping ?? fieldSymbol.Name, typeName };

                // Track the type of items in the current buffer
                if (HlslKnownTypes.IsTypedResourceType(typeSymbol.GetFullMetadataName()))
                {
                    types.Add((INamedTypeSymbol)typeSymbol.TypeArguments[0]);
                }
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
        /// <returns>A sequence of processed methods in <paramref name="structDeclaration"/>, and the entry point.</returns>
        [Pure]
        private static (string EntryPoint, IEnumerable<string> Methods) GetProcessedMethods(
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            SemanticModel semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods)
        {
            // Find all declared methods in the type
            ImmutableArray<MethodDeclarationSyntax> methodDeclarations = (
                from syntaxNode in structDeclaration.DescendantNodes()
                where syntaxNode.IsKind(SyntaxKind.MethodDeclaration)
                select (MethodDeclarationSyntax)syntaxNode).ToImmutableArray();

            string? entryPoint = null;
            List<string> methods = new();

            foreach (MethodDeclarationSyntax methodDeclaration in methodDeclarations)
            {
                IMethodSymbol methodDeclarationSymbol = semanticModel.GetDeclaredSymbol(methodDeclaration)!;
                ShaderSourceRewriter shaderSourceRewriter = new(structDeclarationSymbol, semanticModel, discoveredTypes, staticMethods);

                // Rewrite the method syntax tree
                var processedMethod = shaderSourceRewriter.Visit(methodDeclaration)!.WithoutTrivia();

                // If the method is the shader entry point, do additional processing
                if (methodDeclarationSymbol.Name == nameof(IComputeShader.Execute) &&
                    methodDeclarationSymbol.ReturnsVoid &&
                    methodDeclarationSymbol.TypeParameters.Length == 0 &&
                    methodDeclarationSymbol.Parameters.Length == 1 &&
                    methodDeclarationSymbol.Parameters[0].Type.ToDisplayString() == typeof(ThreadIds).FullName)
                {
                    var parameterName = methodDeclarationSymbol.Parameters[0].Name;

                    processedMethod = new ExecuteMethodRewriter(parameterName).Visit(processedMethod)!;

                    entryPoint = processedMethod.NormalizeWhitespace().ToFullString();
                }
                else methods.Add(processedMethod.NormalizeWhitespace().ToFullString());

                // Emit the extracted local functions
                foreach (var localFunction in shaderSourceRewriter.LocalFunctions)
                {
                    methods.Add(localFunction.Value.NormalizeWhitespace().ToFullString());
                }
            }

            return (entryPoint!, methods);
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

