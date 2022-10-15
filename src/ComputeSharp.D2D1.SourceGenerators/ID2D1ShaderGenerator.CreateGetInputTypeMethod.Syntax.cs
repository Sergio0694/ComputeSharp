using System.Collections.Immutable;
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
    partial class GetInputType
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>GetInputTypeMethod</c> method.
        /// </summary>
        /// <param name="inputTypes">The input types for the shader.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>GetInputTypeMethod</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(ImmutableArray<uint> inputTypes)
        {
            // This code produces a method declaration as follows:
            //
            // readonly uint global::ComputeSharp.D2D1.__Internals.ID2D1Shader.GetInputType(uint index)
            // {
            //     return <INPUT_TYPE>;
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.UIntKeyword)), Identifier(nameof(GetInputType)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddParameterListParameters(Parameter(Identifier("index")).WithType(PredefinedType(Token(SyntaxKind.UIntKeyword))))
                .WithBody(Block(ReturnStatement(GetInputTypesSwitchExpression(inputTypes))));
        }

        /// <summary>
        /// Gets the expression to get the type of a given shader input from its index.
        /// </summary>
        /// <param name="inputTypes">The input types for the shader.</param>
        /// <returns>The expression to extract the values from <paramref name="inputTypes"/>.</returns>
        private static ExpressionSyntax GetInputTypesSwitchExpression(ImmutableArray<uint> inputTypes)
        {
            // If there are no inputs, just return an unspecified value.
            if (inputTypes.IsEmpty)
            {
                return LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0));
            }

            // In order to avoid repeated branches, we can build a bitmask that stores in each
            // bit whether the corresponding pixel shader input is simple or complex. This will
            // allow the logic to be branchless and to just extract the target bit from the mask.
            uint bitmask = 0;

            for (int i = 0; i < inputTypes.Length; i++)
            {
                bitmask |= inputTypes[i] << i;
            }

            // If all inputs are simple, also just return 0
            if (bitmask == 0)
            {
                return LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0));
            }

            // This code produces a bitmask flag extraction expression as follows:
            //
            // (<BITMASK> >> (int)index) & 1;
            return
                BinaryExpression(
                    SyntaxKind.BitwiseAndExpression,
                    ParenthesizedExpression(
                        BinaryExpression(
                            SyntaxKind.RightShiftExpression,
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(bitmask)),
                            CastExpression(PredefinedType(Token(SyntaxKind.IntKeyword)), IdentifierName("index")))),
                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1)));
        }
    }
}