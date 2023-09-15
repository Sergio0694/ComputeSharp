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
        /// <param name="effectDisplayName">The input effect display name value.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectDisplayName</c> property.</returns>
        public static PropertyDeclarationSyntax GetSyntax(string effectDisplayName)
        {
            // This code produces a method declaration as follows:
            //
            // readonly string global::ComputeSharp.D2D1.__Internals.ID2D1Shader.EffectDisplayName => "<EFFECT_DISPLAY_NAME>";
            return
                PropertyDeclaration(PredefinedType(Token(SyntaxKind.StringKeyword)), Identifier(nameof(EffectDisplayName)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(effectDisplayName))))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }
    }
}