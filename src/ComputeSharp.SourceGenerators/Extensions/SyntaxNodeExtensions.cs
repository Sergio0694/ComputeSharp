using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ComputeSharp.SourceGenerators.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with some extension methods for C# syntax nodes.
    /// </summary>
    internal static class SyntaxNodeExtensions
    {
        /// <summary>
        /// A custom <see cref="SymbolDisplayFormat"/> instance with fully qualified style, without global::.
        /// </summary>
        private static readonly SymbolDisplayFormat FullyQualifiedWithoutGlobalFormat = new(
                globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters);

        /// <summary>
        /// Checks a <see cref="SyntaxNode"/> value and replaces the value type to be HLSL compatible, if needed.
        /// </summary>
        /// <typeparam name="TRoot">The type of the input <see cref="TypeSyntax"/> instance.</typeparam>
        /// <param name="node">The input <see cref="SyntaxNode"/> to check and modify if needed.</param>
        /// <param name="sourceType">The source <see cref="SyntaxNode"/> to use to get type into from <paramref name="semanticModel"/>.</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance with info on the input tree.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
        [Pure]
        public static TRoot ReplaceAndTrackType<TRoot>(this TRoot node, SyntaxNode sourceType, SemanticModel semanticModel, ICollection<INamedTypeSymbol> discoveredTypes)
            where TRoot : TypeSyntax
        {
            return node.ReplaceAndTrackType(node, sourceType, semanticModel, discoveredTypes);
        }

        /// <summary>
        /// Checks a <see cref="SyntaxNode"/> value and replaces the value type to be HLSL compatible, if needed.
        /// </summary>
        /// <typeparam name="TRoot">The type of the input <see cref="SyntaxNode"/> instance.</typeparam>
        /// <param name="node">The input <see cref="SyntaxNode"/> to check and modify if needed.</param>
        /// <param name="targetType">The target <see cref="TypeSyntax"/> node to replace.</param>
        /// <param name="sourceType">The source <see cref="SyntaxNode"/> to use to get type into from <paramref name="semanticModel"/>.</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance with info on the input tree.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
        [Pure]
        public static TRoot ReplaceAndTrackType<TRoot>(this TRoot node, TypeSyntax targetType, SyntaxNode sourceType, SemanticModel semanticModel, ICollection<INamedTypeSymbol> discoveredTypes)
            where TRoot : SyntaxNode
        {
            ITypeSymbol typeSymbol = semanticModel.GetTypeInfo(sourceType).Type!;
            string typeName = typeSymbol.ToDisplayString(FullyQualifiedWithoutGlobalFormat);

            discoveredTypes.Add((INamedTypeSymbol)typeSymbol);

            if (HlslKnownTypes.TryGetMappedName(typeName, out string? mappedName))
            {
                TypeSyntax newType = ParseTypeName(mappedName!);

                return node.ReplaceNode(targetType, newType);
            }

            return node.ReplaceNode(targetType, ParseTypeName(typeName.Replace(".", "__")));
        }

        /// <summary>
        /// Checks a <see cref="SyntaxNode"/> value and replaces the value type to be HLSL compatible, if needed.
        /// </summary>
        /// <typeparam name="TRoot">The type of the input <see cref="SyntaxNode"/> instance.</typeparam>
        /// <param name="node">The input <see cref="SyntaxNode"/> to check and modify if needed.</param>
        /// <param name="sourceNode">The original node in the input source tree.</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance with info on the input tree.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL.</returns>
        [Pure]
        public static TypeSyntax ReplaceAndTrackType(this LiteralExpressionSyntax node, SyntaxNode sourceNode, SemanticModel semanticModel, ICollection<INamedTypeSymbol> discoveredTypes)
        {
            ITypeSymbol typeSymbol = semanticModel.GetTypeInfo(sourceNode).Type!;
            string typeName = typeSymbol.ToDisplayString(FullyQualifiedWithoutGlobalFormat);

            discoveredTypes.Add((INamedTypeSymbol)typeSymbol);

            if (HlslKnownTypes.TryGetMappedName(typeName, out string? mappedName))
            {
                return ParseTypeName(mappedName!);
            }

            return ParseTypeName(typeName.Replace(".", "__"));
        }

        /// <summary>
        /// Returns a <see cref="MethodDeclarationSyntax"/> with a block body.
        /// </summary>
        /// <param name="node">The input <see cref="MethodDeclarationSyntax"/> node.</param>
        /// <returns>A node like the one in input, but always with a block body.</returns>
        [Pure]
        public static MethodDeclarationSyntax WithBlockBody(this MethodDeclarationSyntax node)
        {
            if (node.ExpressionBody is ArrowExpressionClauseSyntax arrow)
            {
                StatementSyntax statement = node.ReturnType switch
                {
                    PredefinedTypeSyntax pts when pts.Keyword.IsKind(SyntaxKind.VoidKeyword) => ExpressionStatement(arrow.Expression),
                    _ => ReturnStatement(arrow.Expression)
                };

                return node
                    .WithBody(Block(statement))
                    .WithExpressionBody(null)
                    .WithSemicolonToken(MissingToken(SyntaxKind.SemicolonToken));
            }

            return node;
        }

        /// <summary>
        /// Returns a <see cref="LocalFunctionStatementSyntax"/> with a block body.
        /// </summary>
        /// <param name="node">The input <see cref="LocalFunctionStatementSyntax"/> node.</param>
        /// <returns>A node like the one in input, but always with a block body.</returns>
        /// <remarks>
        /// This method is the same as <see cref="WithBlockBody(MethodDeclarationSyntax)"/>, but it is necessary to
        /// duplicate the code because the two types don't have a common base type or interface that can be leveraged.
        /// </remarks>
        [Pure]
        public static LocalFunctionStatementSyntax WithBlockBody(this LocalFunctionStatementSyntax node)
        {
            if (node.ExpressionBody is ArrowExpressionClauseSyntax arrow)
            {
                StatementSyntax statement = node.ReturnType switch
                {
                    PredefinedTypeSyntax pts when pts.Keyword.IsKind(SyntaxKind.VoidKeyword) => ExpressionStatement(arrow.Expression),
                    _ => ReturnStatement(arrow.Expression)
                };

                return node
                    .WithBody(Block(statement))
                    .WithExpressionBody(null)
                    .WithSemicolonToken(MissingToken(SyntaxKind.SemicolonToken));
            }

            return node;
        }

        /// <summary>
        /// Returns a <see cref="MethodDeclarationSyntax"/> instance with no accessibility modifiers.
        /// </summary>
        /// <param name="node">The input <see cref="MethodDeclarationSyntax"/> node.</param>
        /// <returns>A node just like <paramref name="node"/> but with no accessibility modifiers.</returns>
        [Pure]
        public static MethodDeclarationSyntax WithoutAccessibilityModifiers(this MethodDeclarationSyntax node)
        {
            return node.WithModifiers(TokenList(node.Modifiers.Where(static modifier => modifier.Kind() switch
            {
                SyntaxKind.PublicKeyword => false,
                SyntaxKind.PrivateKeyword => false,
                SyntaxKind.ProtectedKeyword => false,
                SyntaxKind.InternalKeyword => false,
                _ => true
            }).ToArray()));
        }
    }
}
