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