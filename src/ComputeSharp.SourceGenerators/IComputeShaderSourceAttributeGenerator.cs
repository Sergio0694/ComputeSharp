using System.Collections.Immutable;
using System.Linq;
using System.Text;
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

                var structFullName = structDeclarationSymbol.GetFullMetadataName();

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

                    // Create the compilation unit with the source attribute
                    var source =
                        CompilationUnit().AddAttributeLists(
                        AttributeList(SingletonSeparatedList(
                            Attribute(IdentifierName(typeof(IComputeShaderSourceAttribute).FullName)).AddArgumentListArguments(
                                AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(structFullName))),
                                AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(methodDeclarationSymbol.Name))),
                                AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(processedMethodSource))))))
                        .WithOpenBracketToken(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.OpenBracketToken, TriviaList()))
                        .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
                        .NormalizeWhitespace()
                        .ToFullString();

                    var generatedFileName = $"__ComputeSharp_{nameof(IComputeShaderSourceAttribute)}_{structDeclarationSymbol.Name}_{methodDeclarationSymbol.Name}";

                    // Add the method source attribute
                    context.AddSource(generatedFileName, SourceText.From(source, Encoding.UTF8));
                }
            }
        }
    }
}

