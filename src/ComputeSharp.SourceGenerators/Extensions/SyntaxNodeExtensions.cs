using System.Diagnostics.Contracts;
using ComputeSharp.SourceGenerators.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with some extension methods for C# syntax nodes.
    /// </summary>
    internal static class SyntaxNodeExtensions
    {
        private static readonly SymbolDisplayFormat FullyQualifiedWithoutGlobalFormat = new(
                globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters);

        /// <summary>
        /// Checks a <see cref="SyntaxNode"/> value and replaces the value type to be HLSL compatible, if needed.
        /// </summary>
        /// <typeparam name="TRoot">The type of the input <see cref="SyntaxNode"/> instance.</typeparam>
        /// <param name="node">The input <see cref="SyntaxNode"/> to check and modify if needed.</param>
        /// <param name="targetType">The target <see cref="TypeSyntax"/> node to replace.</param>
        /// <param name="sourceType">The source <see cref="TypeSyntax"/> to use to get type into from <paramref name="semanticModel"/>.</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance with info on the input tree.</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
        [Pure]
        public static TRoot ReplaceType<TRoot>(this TRoot node, TypeSyntax targetType, TypeSyntax sourceType, SemanticModel semanticModel)
            where TRoot : SyntaxNode
        {
            if (semanticModel.GetTypeInfo(sourceType).Type is ITypeSymbol typeSymbol &&
                HlslKnownTypes.TryGetMappedName(typeSymbol.ToDisplayString(FullyQualifiedWithoutGlobalFormat), out string? mappedName))
            {
                TypeSyntax newType = ParseTypeName(mappedName!);

                return node.ReplaceNode(targetType, newType);
            }

            return node;
        }

        /// <summary>
        /// Checks a <see cref="SyntaxNode"/> value and replaces the value type to be HLSL compatible, if needed.
        /// </summary>
        /// <typeparam name="TRoot">The type of the input <see cref="SyntaxNode"/> instance.</typeparam>
        /// <param name="node">The input <see cref="SyntaxNode"/> to check and modify if needed.</param>
        /// <param name="type">The <see cref="TypeSyntax"/> to use for the input node.</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
        [Pure]
        public static TypeSyntax ReplaceType(this LiteralExpressionSyntax node, SemanticModel semanticModel)
        {
            ITypeSymbol typeSymbol = semanticModel.GetTypeInfo(node).Type!;
            string typeName = typeSymbol.ToDisplayString(FullyQualifiedWithoutGlobalFormat);

            if (HlslKnownTypes.TryGetMappedName(typeName, out string? mappedName))
            {
                return ParseTypeName(mappedName!);
            }

            return ParseTypeName(typeName);
        }
    }
}
