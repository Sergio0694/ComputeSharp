using System.Diagnostics.Contracts;
using System.Linq;
using ComputeSharp.Shaders.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.Shaders.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with some extension methods for C# syntax nodes
    /// </summary>
    public static class SyntaxNodeExtensions
    {
        /// <summary>
        /// Checks a <see cref="SyntaxNode"/> value and replaces the value type to be HLSL compatible, if needed
        /// </summary>
        /// <typeparam name="TRoot">The type of the input <see cref="SyntaxNode"/> instance</typeparam>
        /// <param name="node">The input <see cref="SyntaxNode"/> to check and modify if needed</param>
        /// <param name="type">The <see cref="TypeSyntax"/> to use for the input node</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL</returns>
        [Pure]
        public static TRoot ReplaceType<TRoot>(this TRoot node, TypeSyntax type) where TRoot : SyntaxNode
        {
            string value = HlslKnownTypes.GetMappedName(type.ToString());

            // If the HLSL mapped full type name equals the original type, just return the input node
            if (value == type.ToString()) return node;

            // Process and return the type name
            TypeSyntax newType = SyntaxFactory.ParseTypeName(value).WithLeadingTrivia(type.GetLeadingTrivia()).WithTrailingTrivia(type.GetTrailingTrivia());
            return node.ReplaceNode(type, newType);
        }

        /// <summary>
        /// Checks a <see cref="MemberAccessExpressionSyntax"/> instance and replaces it to be HLSL compatible, if needed
        /// </summary>
        /// <param name="node">The input <see cref="MemberAccessExpressionSyntax"/> to check and modify if needed</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> to use to load symbols for the input node</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that is compatible with HLSL</returns>
        [Pure]
        public static SyntaxNode ReplaceMember(this MemberAccessExpressionSyntax node, SemanticModel semanticModel)
        {
            SymbolInfo containingMemberSymbolInfo = semanticModel.GetSymbolInfo(node.Expression);
            SymbolInfo memberSymbolInfo = semanticModel.GetSymbolInfo(node.Name);

            ISymbol? memberSymbol = memberSymbolInfo.Symbol ?? memberSymbolInfo.CandidateSymbols.FirstOrDefault();

            // If the input member has no symbol or is not a field, property or method, just return it
            if (memberSymbol is null ||
                memberSymbol.Kind != SymbolKind.Field &&
                memberSymbol.Kind != SymbolKind.Property &&
                memberSymbol.Kind != SymbolKind.Method)
            {
                return node;
            }

            // Process the input node if it's a known method invocation
            if (HlslKnownMethods.TryGetMappedName(containingMemberSymbolInfo.Symbol, memberSymbol) is string value)
            {
                return SyntaxFactory.IdentifierName(value).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
            }

            return node;
        }
    }
}
