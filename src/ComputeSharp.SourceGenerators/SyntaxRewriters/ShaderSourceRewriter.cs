using ComputeSharp.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators.SyntaxRewriters
{
    /// <summary>
    /// A custom <see cref="CSharpSyntaxRewriter"/> type that processes C# methods to convert to HLSL compliant code.
    /// </summary>
    internal sealed class ShaderSourceRewriter : CSharpSyntaxRewriter
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
                nodeSymbol.ContainingType.ToDisplayString() == typeof(ThreadIds).FullName)
            {
                // When accessing ThreadIds members, they are in lowercase in HLSL. This is
                // because the type is mapped to uint3, which has the xyz members.
                return updatedNode.WithName(IdentifierName(updatedNode.Name.ToString().ToLower()));
            }

            return updatedNode;
        }
    }
}
