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
    partial class GetPixelOptions
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>GetPixelOptions</c> method.
        /// </summary>
        /// <param name="pixelOptions">The pixel options for the shader.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>GetPixelOptions</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(D2D1PixelOptions pixelOptions)
        {
            // This code produces a method declaration as follows:
            //
            // readonly uint global::ComputeSharp.D2D1.__Internals.ID2D1Shader.GetPixelOptions()
            // {
            //     return <PIXEL_OPTIONS>;
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.UIntKeyword)), Identifier(nameof(GetPixelOptions)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithBody(Block(ReturnStatement(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)pixelOptions)))));
        }
    }
}
