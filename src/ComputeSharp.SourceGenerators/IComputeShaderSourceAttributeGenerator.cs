using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
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
    public class IComputeShaderSourceAttributeGenerator : ISourceGenerator
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

                // The list to use to track all discovered custom types
                HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);

                // Helper that converts a sequence of string pairs into a nested array expression.
                // That is, this applies the following transformation:
                //   - { ("K1", "V1"), ("K2", "V2") } => new object[] { new[] { "K1", "V1" }, new[] { "K2", "V2" } }
                static ArrayCreationExpressionSyntax NestedPairsArrayExpression(IEnumerable<(string Key, string Value)> pairs)
                {
                    return
                        ArrayCreationExpression(
                        ArrayType(PredefinedType(Token(SyntaxKind.ObjectKeyword)))
                        .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                        .WithInitializer(InitializerExpression(SyntaxKind.ArrayInitializerExpression)
                        .AddExpressions(pairs.Select(static pair =>
                            ImplicitArrayCreationExpression(InitializerExpression(SyntaxKind.ArrayInitializerExpression).AddExpressions(
                                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(pair.Key)),
                                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(pair.Value))))).ToArray()));
                }

                // Helper that converts a sequence of strings into an array expression.
                // That is, this applies the following transformation:
                //   - { "S1", "S2" } => new[] { "S1", "S2" }
                static ImplicitArrayCreationExpressionSyntax ImplicitArrayExpression(IEnumerable<string> values)
                {
                    return
                        ImplicitArrayCreationExpression(InitializerExpression(SyntaxKind.ArrayInitializerExpression).AddExpressions(
                            values.Select(static value => LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(value))).ToArray()));
                }

                // Create the compilation unit with the source attribute
                var source =
                    CompilationUnit().AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName(typeof(IComputeShaderSourceAttribute).FullName)).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(structDeclarationSymbol.GetFullMetadataName()))),
                            AttributeArgument(NestedPairsArrayExpression(GetProcessedMembers(structDeclarationSymbol, discoveredTypes).ToArray())),
                            AttributeArgument(NestedPairsArrayExpression(GetProcessedMethods(structDeclaration, semanticModel, discoveredTypes).ToArray())),
                            AttributeArgument(ImplicitArrayExpression(GetProcessedTypes(discoveredTypes))))))
                    .WithOpenBracketToken(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.OpenBracketToken, TriviaList()))
                    .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
                    .NormalizeWhitespace()
                    .ToFullString();

                // Add the method source attribute
                context.AddSource(structDeclarationSymbol.GetGeneratedFileName<IComputeShaderSourceAttributeGenerator>(), SourceText.From(source, Encoding.UTF8));
            }
        }

        /// <summary>
        /// Gets a sequence of captured members and their mapped names.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <returns>A sequence of captured members in <paramref name="structDeclarationSymbol"/>.</returns>
        [Pure]
        private static IEnumerable<(string Key, string Value)> GetProcessedMembers(INamedTypeSymbol structDeclarationSymbol, ICollection<INamedTypeSymbol> types)
        {
            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                // Yield back the current mapping for the name (if the name used a reserved keyword)
                yield return (fieldSymbol.Name, mapping ?? fieldSymbol.Name);

                // Track the type of items in the current buffer
                if (fieldSymbol.Type is INamedTypeSymbol fieldType &&
                    HlslKnownTypes.IsStructuredBufferType(fieldType.GetFullMetadataName()))
                {
                    types.Add((INamedTypeSymbol)fieldType.TypeArguments[0]);
                }
            }
        }

        /// <summary>
        /// Gets a sequence of processed methods declared within a given type.
        /// </summary>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> instance for the current type.</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the type to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <returns>A sequence of processed methods in <paramref name="structDeclaration"/>.</returns>
        [Pure]
        private static IEnumerable<(string Key, string Value)> GetProcessedMethods(
            StructDeclarationSyntax structDeclaration,
            SemanticModel semanticModel,
            ICollection<INamedTypeSymbol> types)
        {
            // Find all declared methods in the type
            ImmutableArray<MethodDeclarationSyntax> methodDeclarations = (
                from syntaxNode in structDeclaration.DescendantNodes()
                where syntaxNode.IsKind(SyntaxKind.MethodDeclaration)
                select (MethodDeclarationSyntax)syntaxNode).ToImmutableArray();

            foreach (MethodDeclarationSyntax methodDeclaration in methodDeclarations)
            {
                IMethodSymbol methodDeclarationSymbol = semanticModel.GetDeclaredSymbol(methodDeclaration)!;
                ShaderSourceRewriter shaderSourceRewriter = new(semanticModel, types);

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
                }

                // Produce the final method source
                var processedMethodSource = processedMethod.NormalizeWhitespace().ToFullString();

                yield return (methodDeclarationSymbol.Name, processedMethodSource);

                // Emit the extracted local functions
                foreach (var localFunction in shaderSourceRewriter.LocalFunctions)
                {
                    yield return (localFunction.Key, localFunction.Value.NormalizeWhitespace().ToFullString());
                }
            }
        }

        /// <summary>
        /// Gets the sequence of processed discovered custom types.
        /// </summary>
        /// <param name="types">The sequence of discovered custom types.</param>
        /// <returns>A sequence of custom type definitions to add to the shader source.</returns>
        public static IEnumerable<string> GetProcessedTypes(IEnumerable<INamedTypeSymbol> types)
        {
            foreach (var type in HlslKnownTypes.GetCustomTypes(types))
            {
                var structType = type.GetFullMetadataName().Replace(".", "__");
                var structDeclaration = StructDeclaration(structType).WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

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

                yield return structDeclaration.NormalizeWhitespace().ToFullString();
            }
        }
    }
}

