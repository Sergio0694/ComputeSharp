using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.SourceGenerators.Extensions;
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
                if (!structDeclarationSymbol.Interfaces.Any(interfaceSymbol => interfaceSymbol.Name == nameof(IComputeShader))) continue;           

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
                        .AddExpressions(pairs.Select(pair =>
                            ImplicitArrayCreationExpression(InitializerExpression(SyntaxKind.ArrayInitializerExpression).AddExpressions(
                                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(pair.Key)),
                                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(pair.Value))))).ToArray()));
                }

                // Create the compilation unit with the source attribute
                var source =
                    CompilationUnit().AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName(typeof(IComputeShaderSourceAttribute).FullName)).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(structDeclarationSymbol.GetFullMetadataName()))),
                            AttributeArgument(NestedPairsArrayExpression(GetProcessedMembers(structDeclarationSymbol))),
                            AttributeArgument(NestedPairsArrayExpression(GetProcessedMethods(structDeclaration, semanticModel))))))
                    .WithOpenBracketToken(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.OpenBracketToken, TriviaList()))
                    .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
                    .NormalizeWhitespace()
                    .ToFullString();

                var generatedFileName = $"__ComputeSharp_{nameof(IComputeShaderSourceAttribute)}_{structDeclarationSymbol.Name}";

                // Add the method source attribute
                context.AddSource(generatedFileName, SourceText.From(source, Encoding.UTF8));
            }
        }

        /// <summary>
        /// Gets a sequence of captured members and their mapped names.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>A sequence of captured members in <paramref name="structDeclarationSymbol"/>.</returns>
        [Pure]
        private static IEnumerable<(string Key, string Value)> GetProcessedMembers(INamedTypeSymbol structDeclarationSymbol)
        {
            foreach (string memberName in (
                from member in structDeclarationSymbol.GetMembers()
                where member.Kind == SymbolKind.Field ||
                      member.Kind == SymbolKind.Property
                select member.Name).Distinct())
            {
                _ = HlslKnownKeywords.TryGetMappedName(memberName, out string? mapping);

                yield return (memberName, mapping ?? memberName);
            }
        }

        /// <summary>
        /// Gets a sequence of processed methods declared within a given type.
        /// </summary>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> instance for the current type.</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the type to process.</param>
        /// <returns>A sequence of processed methods in <paramref name="structDeclaration"/>.</returns>
        [Pure]
        private static IEnumerable<(string Key, string Value)> GetProcessedMethods(
            StructDeclarationSyntax structDeclaration,
            SemanticModel semanticModel)
        {
            // Find all declared methods in the type
            ImmutableArray<MethodDeclarationSyntax> methodDeclarations = (
                from syntaxNode in structDeclaration.DescendantNodes()
                where syntaxNode.IsKind(SyntaxKind.MethodDeclaration)
                select (MethodDeclarationSyntax)syntaxNode).ToImmutableArray();

            foreach (MethodDeclarationSyntax methodDeclaration in methodDeclarations)
            {
                IMethodSymbol methodDeclarationSymbol = semanticModel.GetDeclaredSymbol(methodDeclaration)!;

                // Rewrite the method syntax tree
                var processedMethod = new ShaderSourceRewriter(semanticModel).Visit(methodDeclaration)!.WithoutTrivia();

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
            }
        }
    }
}

