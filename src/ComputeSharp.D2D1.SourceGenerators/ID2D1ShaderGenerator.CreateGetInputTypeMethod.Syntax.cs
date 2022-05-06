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
            // readonly byte global::ComputeSharp.D2D1.__Internals.ID2D1Shader.GetInputType(byte index)
            // {
            //     return <INPUT_TYPE>;
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.ByteKeyword)), Identifier("GetInputType"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddParameterListParameters(Parameter(Identifier("index")).WithType(PredefinedType(Token(SyntaxKind.ByteKeyword))))
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

            ImmutableArray<SwitchExpressionArmSyntax>.Builder switches = ImmutableArray.CreateBuilder<SwitchExpressionArmSyntax>();

            for (int i = 0; i < inputTypes.Length; i++)
            {
                // Add a switch branch to map a given input to its serialized type, as follows:
                //
                // 0 => 1
                switches.Add(
                    SwitchExpressionArm(
                        ConstantPattern(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(i))),
                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)inputTypes[i]))));
            }

            // Add a default arm
            switches.Add(
                SwitchExpressionArm(
                    DiscardPattern(),
                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))));

            // Build a switch expression as follows:
            //
            // index switch
            // {
            //     <SWITCH_ARMS>
            // }
            return
                SwitchExpression(IdentifierName("index"))
                .AddArms(switches.ToArray());
        }
    }
}
