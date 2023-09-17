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
    partial class EffectDisplayName
    {
        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectDisplayName</c> property.
        /// </summary>
        /// <param name="effectDisplayName">The input effect display name value, if available.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectDisplayName</c> property.</returns>
        public static PropertyDeclarationSyntax GetSyntax(string? effectDisplayName)
        {
            // Get the expression: either a string literal, or just null
            ExpressionSyntax displayNameExpression = effectDisplayName switch
            {
                { } => LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(effectDisplayName)),
                _ => LiteralExpression(SyntaxKind.NullLiteralExpression)
            };

            // This code produces a method declaration as follows:
            //
            // readonly string? global::ComputeSharp.D2D1.__Internals.ID2D1Shader.EffectDisplayName => <EFFECT_DISPLAY_NAME>;
            return
                PropertyDeclaration(NullableType(PredefinedType(Token(SyntaxKind.StringKeyword))), Identifier(nameof(EffectDisplayName)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(displayNameExpression))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }
    }
}