using ComputeSharp.D2D1.__Internals;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class EffectMetadata
    {
        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectDisplayName</c> property.
        /// </summary>
        /// <param name="effectDisplayName">The input effect display name value, if available.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectDisplayName</c> property.</returns>
        public static PropertyDeclarationSyntax GetEffectDisplayNameSyntax(string? effectDisplayName)
        {
            return GetEffectMetadataSyntax("EffectDisplayName", effectDisplayName);
        }

        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectDescription</c> property.
        /// </summary>
        /// <param name="effectDescription">The input effect description value, if available.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectDescription</c> property.</returns>
        public static PropertyDeclarationSyntax GetEffectDescriptionSyntax(string? effectDescription)
        {
            return GetEffectMetadataSyntax("EffectDescription", effectDescription);
        }

        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectCategory</c> property.
        /// </summary>
        /// <param name="effectCategory">The input effect category value, if available.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectCategory</c> property.</returns>
        public static PropertyDeclarationSyntax GetEffectCategorySyntax(string? effectCategory)
        {
            return GetEffectMetadataSyntax("EffectCategory", effectCategory);
        }

        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectAuthor</c> property.
        /// </summary>
        /// <param name="effectAuthor">The input effect author value, if available.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectAuthor</c> property.</returns>
        public static PropertyDeclarationSyntax GetEffectAuthorSyntax(string? effectAuthor)
        {
            return GetEffectMetadataSyntax("EffectAuthor", effectAuthor);
        }

        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for a given metadata property.
        /// </summary>
        /// <param name="propertyName">The property name to generate.</param>
        /// <param name="metadataValue">The input effect metadata value, if available.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the given metadata property.</returns>
        private static PropertyDeclarationSyntax GetEffectMetadataSyntax(string propertyName, string? metadataValue)
        {
            // Get the expression: either a string literal, or just null
            ExpressionSyntax metadataValueExpression = metadataValue switch
            {
                { } => LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(metadataValue)),
                _ => LiteralExpression(SyntaxKind.NullLiteralExpression)
            };

            // This code produces a method declaration as follows:
            //
            // readonly string? global::ComputeSharp.D2D1.__Internals.ID2D1Shader.<PROPERTY_NAME> => <METADATA_VALUE>;
            return
                PropertyDeclaration(NullableType(PredefinedType(Token(SyntaxKind.StringKeyword))), Identifier(propertyName))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(metadataValueExpression))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }
    }
}