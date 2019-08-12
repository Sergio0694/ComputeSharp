using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.Shaders
{
    public class ShaderSyntaxRewriter : CSharpSyntaxRewriter
    {
        private readonly int depth;
        private readonly bool isTopLevel;
        private readonly SemanticModel semanticModel;

        public ShaderSyntaxRewriter(SemanticModel semanticModel, bool isTopLevel = false, int depth = 0)
        {
            this.semanticModel = semanticModel;
            this.isTopLevel = isTopLevel;
            this.depth = depth;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            node = (MethodDeclarationSyntax)base.VisitMethodDeclaration(node);

            if (isTopLevel && depth > 0)
            {
                node = node.ReplaceToken(node.Identifier, SyntaxFactory.Identifier($"Base_{depth}_{node.Identifier.ValueText}"));
            }

            SyntaxTokenList modifiers = new SyntaxTokenList();

            if (node.Modifiers.Any(SyntaxKind.StaticKeyword))
            {
                modifiers = modifiers.Add(SyntaxFactory.Token(default, SyntaxKind.StaticKeyword, SyntaxFactory.TriviaList(SyntaxFactory.SyntaxTrivia(SyntaxKind.WhitespaceTrivia, " "))));
            }

            return node.ReplaceType(node.ReturnType).WithModifiers(modifiers);
        }

        public override SyntaxNode VisitParameter(ParameterSyntax node)
        {
            node = (ParameterSyntax)base.VisitParameter(node);
            node = node.WithAttributeLists(default);
            node = node.ReplaceType(node.Type);

            return node;
        }

        public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
        {
            node = (CastExpressionSyntax)base.VisitCastExpression(node);
            return node.ReplaceType(node.Type);
        }

        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            node = (LocalDeclarationStatementSyntax)base.VisitLocalDeclarationStatement(node);
            return node.ReplaceType(node.Declaration.Type);
        }

        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            node = (ObjectCreationExpressionSyntax)base.VisitObjectCreationExpression(node);
            node = node.ReplaceType(node.Type);

            if (node.ArgumentList.Arguments.Count == 0)
            {
                return SyntaxFactory.CastExpression(node.Type, SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0)));
            }
            else
            {
                return SyntaxFactory.InvocationExpression(node.Type, node.ArgumentList);
            }
        }

        public override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            node = (DefaultExpressionSyntax)base.VisitDefaultExpression(node);
            node = node.ReplaceType(node.Type);
            return SyntaxFactory.CastExpression(node.Type, SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0)));
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            node = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (node.Expression is BaseExpressionSyntax)
            {
                if (isTopLevel)
                {
                    return SyntaxFactory.IdentifierName($"Base_{depth + 1}_{node.Name}");
                }
                else
                {
                    SymbolInfo memberSymbolInfo = semanticModel.GetSymbolInfo(node.Name);
                    ISymbol? memberSymbol = memberSymbolInfo.Symbol ?? memberSymbolInfo.CandidateSymbols.FirstOrDefault();

                    if (memberSymbol != null)
                    {
                        return SyntaxFactory.IdentifierName($"{memberSymbol.ContainingType.Name}::{node.Name}");
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
            else
            {
                return node.ReplaceMember(semanticModel);
            }
        }
    }
}
