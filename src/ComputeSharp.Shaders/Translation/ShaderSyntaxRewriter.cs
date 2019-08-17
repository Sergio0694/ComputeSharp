using ComputeSharp.Shaders.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A custom <see cref="CSharpSyntaxRewriter"/> <see langword="class"/> that processes C# methods to convert to HLSL
    /// </summary>
    internal class ShaderSyntaxRewriter : CSharpSyntaxRewriter
    {
        /// <summary>
        /// The <see cref="Microsoft.CodeAnalysis.SemanticModel"/> instance to use to rewrite the decompiled code
        /// </summary>
        private readonly SemanticModel SemanticModel;

        /// <summary>
        /// Creates a new <see cref="ShaderSyntaxRewriter"/> instance with the specified parameters
        /// </summary>
        /// <param name="semanticModel"></param>
        public ShaderSyntaxRewriter(SemanticModel semanticModel) => SemanticModel = semanticModel;

        /// <inheritdoc/>
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            node = (MethodDeclarationSyntax)base.VisitMethodDeclaration(node);

            SyntaxTokenList modifiers = new SyntaxTokenList();

            if (node.Modifiers.Any(SyntaxKind.StaticKeyword))
            {
                modifiers = modifiers.Add(SyntaxFactory.Token(default, SyntaxKind.StaticKeyword, SyntaxFactory.TriviaList(SyntaxFactory.SyntaxTrivia(SyntaxKind.WhitespaceTrivia, " "))));
            }

            return node.ReplaceType(node.ReturnType).WithModifiers(modifiers);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitParameter(ParameterSyntax node)
        {
            node = (ParameterSyntax)base.VisitParameter(node);
            node = node.WithAttributeLists(default);
            node = node.ReplaceType(node.Type);

            return node;
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
        {
            node = (CastExpressionSyntax)base.VisitCastExpression(node);

            return node.ReplaceType(node.Type);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            node = (LocalDeclarationStatementSyntax)base.VisitLocalDeclarationStatement(node);

            return node.ReplaceType(node.Declaration.Type);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            node = (ObjectCreationExpressionSyntax)base.VisitObjectCreationExpression(node);
            node = node.ReplaceType(node.Type);

            if (node.ArgumentList.Arguments.Count == 0)
            {
                return SyntaxFactory.CastExpression(node.Type, SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0)));
            }

            return SyntaxFactory.InvocationExpression(node.Type, node.ArgumentList);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            node = (DefaultExpressionSyntax)base.VisitDefaultExpression(node);
            node = node.ReplaceType(node.Type);

            return SyntaxFactory.CastExpression(node.Type, SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0)));
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            node = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            return node.ReplaceMember(SemanticModel, out _);
        }
    }
}
