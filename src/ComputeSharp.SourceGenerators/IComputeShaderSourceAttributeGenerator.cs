using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.SourceGenerators.Extensions;
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

                    if (methodDeclarationSymbol.Name == "Execute" &&
                        methodDeclarationSymbol.ReturnsVoid &&
                        methodDeclarationSymbol.TypeParameters.Length == 0 &&
                        methodDeclarationSymbol.Parameters.Length == 1 &&
                        methodDeclarationSymbol.Parameters[0].Type.ToDisplayString() == "ComputeSharp.ThreadIds")
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
                            Attribute(IdentifierName("ComputeSharp.IComputeShaderSource")).AddArgumentListArguments(
                                AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(structFullName))),
                                AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(methodDeclarationSymbol.Name))),
                                AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(processedMethodSource))))))
                        .WithOpenBracketToken(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.OpenBracketToken, TriviaList()))
                        .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
                        .NormalizeWhitespace()
                        .ToFullString();

                    var generatedFileName = $"__ComputeSharp_IComputeShaderSourceAttribute_{structDeclarationSymbol.Name}_{methodDeclarationSymbol.Name}";

                    // Add the method source attribute
                    context.AddSource(generatedFileName, SourceText.From(source, Encoding.UTF8));
                }
            }
        }

        /// <summary>
        /// A custom <see cref="CSharpSyntaxRewriter"/> type that processes C# methods to convert to HLSL compliant code.
        /// </summary>
        private sealed class ShaderSourceRewriter : CSharpSyntaxRewriter
        {
            /// <summary>
            /// The <see cref="SemanticModel"/> instance with semantic info on the target syntax tree.
            /// </summary>
            private readonly SemanticModel semanticModel;

            /// <summary>
            /// Creates a new <see cref="ShaderSourceRewriter"/> instance with the specified parameters.
            /// </summary>
            /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the target syntax tree.</param>
            public ShaderSourceRewriter(SemanticModel semanticModel)
            {
                this.semanticModel = semanticModel;
            }

            /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
            public TNode? Visit<TNode>(TNode? node)
                where TNode : SyntaxNode
            {
                return (TNode?)base.Visit(node);
            }

            /// <inheritdoc/>
            public override SyntaxNode VisitParameter(ParameterSyntax node)
            {
                var updatedNode = (ParameterSyntax)base.VisitParameter(node)!;

                return updatedNode
                    .WithAttributeLists(default)
                    .ReplaceType(updatedNode.Type!, node.Type!, this.semanticModel);
            }

            /// <inheritdoc/>
            public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
            {
                var updatedNode = (CastExpressionSyntax)base.VisitCastExpression(node)!;

                return updatedNode.ReplaceType(updatedNode.Type, node.Type, this.semanticModel);
            }

            /// <inheritdoc/>
            public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
            {
                var updatedNode = ((LocalDeclarationStatementSyntax)base.VisitLocalDeclarationStatement(node)!);

                return updatedNode.ReplaceType(updatedNode.Declaration.Type, node.Declaration.Type, this.semanticModel);
            }

            /// <inheritdoc/>
            public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
            {
                var updatedNode = (ObjectCreationExpressionSyntax)base.VisitObjectCreationExpression(node)!;

                updatedNode = updatedNode.ReplaceType(updatedNode.Type, node.Type, this.semanticModel);

                // New objects use the default HLSL cast syntax, eg. (float4)0
                if (updatedNode.ArgumentList!.Arguments.Count == 0)
                {
                    return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
                }

                return InvocationExpression(updatedNode.Type, updatedNode.ArgumentList);
            }

            /// <inheritdoc/>
            public override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
            {
                var updatedNode = (DefaultExpressionSyntax)base.VisitDefaultExpression(node)!;

                updatedNode = updatedNode.ReplaceType(updatedNode.Type, node.Type, this.semanticModel);

                // A default expression becomes (T)0 in HLSL
                return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }

            /// <inheritdoc/>
            public override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
            {
                node = ((LiteralExpressionSyntax)base.VisitLiteralExpression(node)!);

                if (node.IsKind(SyntaxKind.DefaultLiteralExpression))
                {
                    // Same HLSL-style expression in the form (T)0
                    return CastExpression(node.ReplaceType(this.semanticModel), LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
                }

                return node;
            }

            /// <inheritdoc/>
            public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
            {
                return ((MethodDeclarationSyntax)base.VisitMethodDeclaration(node)!).WithBlockBody();
            }

            /// <inheritdoc/>
            public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
            {
                var updatedNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node)!;

                if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
                    this.semanticModel.GetSymbolInfo(node).Symbol is ISymbol nodeSymbol &&
                    nodeSymbol.ContainingType.ToDisplayString() == "ComputeSharp.ThreadIds")
                {
                    // When accessing ThreadIds members, they are in lowercase in HLSL. This is
                    // because the type is mapped to uint3, which has the xyz members.
                    return updatedNode.WithName(IdentifierName(updatedNode.Name.ToString().ToLower()));
                }

                return updatedNode;
            }
        }

        /// <summary>
        /// A custom <see cref="CSharpSyntaxRewriter"/> type that does postprocessing fixups on the shader main method.
        /// </summary>
        private sealed class ExecuteMethodRewriter : CSharpSyntaxRewriter
        {
            /// <summary>
            /// The identifier name for the input <see langword="uint3"/> parameter.
            /// </summary>
            private readonly string threadIdsIdentifier;

            /// <summary>
            /// Creates a new <see cref="ExecuteMethodRewriter"/> instance with the specified parameters.
            /// </summary>
            /// <param name="threadIdsIdentifier">The identifier name for the input <see langword="uint3"/> parameter.</param>
            public ExecuteMethodRewriter(string threadIdsIdentifier)
            {
                this.threadIdsIdentifier = threadIdsIdentifier;
            }

            /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
            public TNode? Visit<TNode>(TNode? node)
                where TNode : SyntaxNode
            {
                return (TNode?)base.Visit(node);
            }

            /// <inheritdoc/>
            public override SyntaxNode VisitParameter(ParameterSyntax node)
            {
                var updatedNode = (ParameterSyntax)base.VisitParameter(node)!;

                return updatedNode.WithIdentifier(Identifier($"{updatedNode.Identifier.Text} : SV_DispatchThreadId"));
            }

            /// <inheritdoc/>
            public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
            {
                var updatedNode = ((MethodDeclarationSyntax)base.VisitMethodDeclaration(node)!).WithModifiers(TokenList());

                // When we're rewriting the main compute shader method, we need to insert a range
                // check to ensure that invocation outside of the requested range are discarded.
                // The following snippet creates this prologue before the user provided body:
                //
                // if (ids.X < __x &&
                //     ids.Y < __y &&
                //     ids.Z < __z)
                // {
                //     <body>
                // }
                var rangeCheckExpression =
                    BinaryExpression(SyntaxKind.LogicalAndExpression,
                        BinaryExpression(SyntaxKind.LogicalAndExpression,
                            BinaryExpression(SyntaxKind.LessThanExpression,
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName(this.threadIdsIdentifier),
                                    IdentifierName("x")),
                                IdentifierName("__x")),
                            BinaryExpression(SyntaxKind.LessThanExpression,
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName(this.threadIdsIdentifier),
                                    IdentifierName("y")),
                                IdentifierName("__y"))),
                        BinaryExpression(SyntaxKind.LessThanExpression,
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName(this.threadIdsIdentifier),
                                IdentifierName("z")),
                            IdentifierName("__z")));

                return updatedNode
                    .WithIdentifier(Identifier("CSMain"))
                    .WithBody(Block(IfStatement(rangeCheckExpression, updatedNode.Body!)));
            }
        }
    }
}

