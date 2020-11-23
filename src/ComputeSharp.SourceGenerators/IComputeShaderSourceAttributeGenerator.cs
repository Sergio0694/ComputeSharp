using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

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
                if (!structDeclarationSymbol.Interfaces.Any(interfaceSymbol => interfaceSymbol.Name == "IComputeShader")) continue;

                var structFullName = structDeclarationSymbol.ToDisplayString(new SymbolDisplayFormat(
                    SymbolDisplayGlobalNamespaceStyle.Omitted,
                    SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                    SymbolDisplayGenericsOptions.IncludeTypeParameters));

                // Find all declared methods in the type
                ImmutableArray<MethodDeclarationSyntax> methodDeclarations = (
                    from syntaxNode in structDeclaration.DescendantNodes()
                    where syntaxNode.IsKind(SyntaxKind.MethodDeclaration)
                    select (MethodDeclarationSyntax)syntaxNode).ToImmutableArray();

                foreach (MethodDeclarationSyntax methodDeclaration in methodDeclarations)
                {
                    IMethodSymbol methodDeclarationSymbol = semanticModel.GetDeclaredSymbol(methodDeclaration)!;

                    // Extract the source from either a block or an expression body
                    var methodSource = ((methodDeclaration.Body, methodDeclaration.ExpressionBody) switch
                    {
                        (BlockSyntax block, _) => block,
                        (_, ArrowExpressionClauseSyntax arrow) => Block(ExpressionStatement(arrow.Expression)),
                        _ => Block()
                    }).NormalizeWhitespace().ToFullString();

                    // Create the compilation unit with the source attribute
                    var source =
                        CompilationUnit().AddAttributeLists(
                        AttributeList(SingletonSeparatedList(
                            Attribute(IdentifierName("ComputeSharp.IComputeShaderSource")).AddArgumentListArguments(
                                AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(structFullName))),
                                AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(methodDeclarationSymbol.Name))),
                                AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(methodSource))))))
                        .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
                        .NormalizeWhitespace()
                        .ToFullString();

                    var generatedFileName = $"__ComputeSharp_IComputeShaderSourceAttribute_{structDeclarationSymbol.Name}_{methodDeclarationSymbol.Name}";

                    // Add the method source attribute
                    context.AddSource(generatedFileName, SourceText.From(source, Encoding.UTF8));
                }
            }
        }
    }
}

