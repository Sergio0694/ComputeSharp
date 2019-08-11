using System.Linq;
using DirectX12GameEngine.Shaders.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DirectX12GameEngine.Shaders
{
    public static class SyntaxNodeExtensions
    {
        public static TRoot ReplaceType<TRoot>(this TRoot node, TypeSyntax type) where TRoot : SyntaxNode
        {
            string value = HlslKnownTypes.GetMappedName(type.ToString());

            if (value == type.ToString())
            {
                return node;
            }
            else
            {
                return node.ReplaceNode(type, SyntaxFactory.ParseTypeName(value).WithLeadingTrivia(type.GetLeadingTrivia()).WithTrailingTrivia(type.GetTrailingTrivia()));
            }
        }

        public static SyntaxNode ReplaceMember(this MemberAccessExpressionSyntax node, SemanticModel semanticModel)
        {
            SymbolInfo containingMemberSymbolInfo = semanticModel.GetSymbolInfo(node.Expression);
            SymbolInfo memberSymbolInfo = semanticModel.GetSymbolInfo(node.Name);

            ISymbol? memberSymbol = memberSymbolInfo.Symbol ?? memberSymbolInfo.CandidateSymbols.FirstOrDefault();

            if (memberSymbol is null || (memberSymbol.Kind != SymbolKind.Field && memberSymbol.Kind != SymbolKind.Property && memberSymbol.Kind != SymbolKind.Method))
            {
                return node;
            }

            string? value = ShaderGenerator.HlslKnownMethods.GetMappedName(containingMemberSymbolInfo.Symbol, memberSymbol);

            if (value is null)
            {
                return node;
            }
            else
            {
                return SyntaxFactory.IdentifierName(value).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
            }
        }
    }
}
