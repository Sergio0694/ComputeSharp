using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators.SyntaxRewriters
{
    /// <summary>
    /// A custom <see cref="CSharpSyntaxRewriter"/> type that does postprocessing fixups on the shader main method.
    /// </summary>
    internal sealed class ExecuteMethodRewriter : CSharpSyntaxRewriter
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

            return updatedNode.WithBody(Block(IfStatement(rangeCheckExpression, updatedNode.Body!)));
        }
    }
}
