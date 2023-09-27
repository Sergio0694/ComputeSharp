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
    partial class PixelOptions
    {
        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>PixelOptions</c> property.
        /// </summary>
        /// <param name="pixelOptions">The pixel options for the shader.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>PixelOptions</c> property.</returns>
        public static PropertyDeclarationSyntax GetSyntax(D2D1PixelOptions pixelOptions)
        {
            ExpressionSyntax pixelOptionsExpression;

            // Set the right expression if the pixel options are valid
            if (pixelOptions is D2D1PixelOptions.None or D2D1PixelOptions.TrivialSampling)
            {
                pixelOptionsExpression =
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("ComputeSharp.D2D1.D2D1PixelOptions"),
                        IdentifierName(pixelOptions.ToString()));
            }
            else
            {
                // Otherwise just return default (the analyzer will emit a diagnostic)
                pixelOptionsExpression = LiteralExpression(SyntaxKind.DefaultLiteralExpression, Token(SyntaxKind.DefaultKeyword));
            }

            // This code produces a property declaration as follows:
            //
            // readonly ComputeSharp.D2D1.D2D1PixelOptions global::ComputeSharp.D2D1.__Internals.ID2D1Shader.PixelOptions => <PIXEL_OPTIONS>;
            return
                PropertyDeclaration(IdentifierName("ComputeSharp.D2D1.D2D1PixelOptions"), Identifier(nameof(PixelOptions)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(pixelOptionsExpression))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }
    }
}