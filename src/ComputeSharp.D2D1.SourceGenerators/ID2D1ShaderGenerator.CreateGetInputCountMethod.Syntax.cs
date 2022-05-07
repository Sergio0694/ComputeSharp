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
    /// A helper with all logic to generate the <c>GetInputCount</c> method.
    /// </summary>
    private static partial class GetInputCount
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>GetInputCount</c> method.
        /// </summary>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>GetInputCount</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(int inputCount)
        {
            // This code produces a method declaration as follows:
            //
            // readonly uint global::ComputeSharp.D2D1.__Internals.ID2D1Shader.GetInputCount()
            // {
            //     return <INPUT_COUNT>;
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.UIntKeyword)), Identifier(nameof(GetInputCount)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithBody(Block(ReturnStatement(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(inputCount)))));
        }
    }
}
