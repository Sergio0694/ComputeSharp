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
        /// The <see cref="ShaderSourceRewriter"/> instance used to process the input tree.
        /// </summary>
        private readonly ShaderSourceRewriter shaderSourceRewriter;

        /// <summary>
        /// Creates a new <see cref="ExecuteMethodRewriter"/> instance with the specified parameters.
        /// </summary>
        /// <param name="shaderSourceRewriter">The <see cref="ShaderSourceRewriter"/> instance used to process the input tree.</param>
        public ExecuteMethodRewriter(ShaderSourceRewriter shaderSourceRewriter)
        {
            this.shaderSourceRewriter = shaderSourceRewriter;
        }

        /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
        public TNode? Visit<TNode>(TNode? node)
            where TNode : SyntaxNode
        {
            return (TNode?)base.Visit(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitParameterList(ParameterListSyntax node)
        {
            var updatedNode = (ParameterListSyntax)base.VisitParameterList(node)!;

            updatedNode = updatedNode.AddParameters(Parameter(Identifier($"uint3 {nameof(ThreadIds)} : SV_DispatchThreadID")));

            if (this.shaderSourceRewriter.IsGroupIdsUsed)
            {
                updatedNode = updatedNode.AddParameters(Parameter(Identifier($"uint3 {nameof(GroupIds)} : SV_GroupThreadID")));
            }

            if (this.shaderSourceRewriter.IsGroupIdsIndexUsed)
            {
                updatedNode = updatedNode.AddParameters(Parameter(Identifier($"uint __{nameof(GroupIds)}__get_Index : SV_GroupIndex")));
            }

            if (this.shaderSourceRewriter.IsGridIdsUsed)
            {
                updatedNode = updatedNode.AddParameters(Parameter(Identifier($"uint3 {nameof(GridIds)} : SV_GroupID")));
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var updatedNode = ((MethodDeclarationSyntax)base.VisitMethodDeclaration(node)!).WithModifiers(TokenList());

            // When we're rewriting the main compute shader method, we need to insert a range
            // check to ensure that invocation outside of the requested range are discarded.
            // The following snippet creates this prologue before the user provided body:
            //
            // if (ThreadIds.x < __x &&
            //     ThreadIds.y < __y &&
            //     ThreadIds.z < __z)
            // {
            //     <body>
            // }
            var rangeCheckExpression =
                BinaryExpression(SyntaxKind.LogicalAndExpression,
                    BinaryExpression(SyntaxKind.LogicalAndExpression,
                        BinaryExpression(SyntaxKind.LessThanExpression,
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName(nameof(ThreadIds)),
                                IdentifierName("x")),
                            IdentifierName("__x")),
                        BinaryExpression(SyntaxKind.LessThanExpression,
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName(nameof(ThreadIds)),
                                IdentifierName("y")),
                            IdentifierName("__y"))),
                    BinaryExpression(SyntaxKind.LessThanExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName(nameof(ThreadIds)),
                            IdentifierName("z")),
                        IdentifierName("__z")));

            return updatedNode.WithBody(Block(IfStatement(rangeCheckExpression, updatedNode.Body!)));
        }
    }
}
