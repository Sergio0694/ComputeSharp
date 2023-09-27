using ComputeSharp.D2D1.__Internals;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>InputCount</c> property.
    /// </summary>
    private static partial class InputCount
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>InputCount</c> property.
        /// </summary>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>InputCount</c> property.</returns>
        public static PropertyDeclarationSyntax GetSyntax(int inputCount)
        {
            // This code produces a property declaration as follows:
            //
            // readonly int global::ComputeSharp.D2D1.__Internals.ID2D1Shader.InputCount => <INPUT_COUNT>;
            return
                PropertyDeclaration(PredefinedType(Token(SyntaxKind.IntKeyword)), Identifier(nameof(InputCount)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(inputCount))))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }
    }
}