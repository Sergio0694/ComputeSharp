using System;
using System.Linq;
using System.Text;
using ComputeSharp.D2D1.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderSourceGenerator
{
    /// <inheritdoc/>
    partial class Execute
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>TryGetBytecode</c> method.
        /// </summary>
        /// <param name="bytecodeInfo">The input bytecode info.</param>
        /// <param name="fixup">An opaque <see cref="Func{TResult}"/> instance to transform the final tree into text.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the target method.</returns>
        public static MethodDeclarationSyntax GetSyntax(EmbeddedBytecodeMethodInfo bytecodeInfo, out Func<SyntaxNode, SourceText> fixup)
        {
            // Get the TypeSyntax for the return type (either ReadOnlySpan<byte>, or an invalid one).
            // This is done so that if the user picks an invalid type, the code is still generated,
            // just returning default, so that proper diagnostic can be generated, without also
            // having hard to understand compiler errors due to the mismatched return type here.
            TypeSyntax returnType = bytecodeInfo.InvalidReturnType is string invalidReturnType
                ? IdentifierName(invalidReturnType)
                : GenericName(Identifier("global::System.ReadOnlySpan"))
                    .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword)));

            BlockSyntax methodBlock;

            if (bytecodeInfo.Bytecode.IsDefaultOrEmpty)
            {
                // This code generates the following block:
                //
                // {
                //     return default;
                // }
                methodBlock =
                    Block(ReturnStatement(
                        LiteralExpression(
                            SyntaxKind.DefaultLiteralExpression,
                            Token(SyntaxKind.DefaultKeyword))));

                fixup = tree => tree.GetText(Encoding.UTF8);
            }
            else
            {
                // This code generates the following block:
                //
                // {
                //     return new byte[] { __EMBEDDED_SHADER_BYTECODE };
                // }
                //
                // As in the ID2D1Shader generator, the __EMBEDDED_SHADER_BYTECODE identifier will be replaced with the bytecode
                // after the full string for the syntax tree has been generated, to avoid every literal going through a token.
                methodBlock =
                    Block(
                        ReturnStatement(
                            ArrayCreationExpression(
                                ArrayType(PredefinedType(Token(SyntaxKind.ByteKeyword)))
                                .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                            .WithInitializer(
                                InitializerExpression(
                                    SyntaxKind.ArrayInitializerExpression,
                                    SingletonSeparatedList<ExpressionSyntax>(IdentifierName("__EMBEDDED_SHADER_BYTECODE"))))));

                string bytecodeLiterals = ID2D1ShaderGenerator.LoadBytecode.BuildShaderBytecodeExpressionString(bytecodeInfo.Bytecode.AsSpan());

                // Prepare the fixup function
                fixup = tree => SourceText.From(tree.ToFullString().Replace("__EMBEDDED_SHADER_BYTECODE", bytecodeLiterals), Encoding.UTF8);
            }

            // This code produces a branch as follows:
            //
            // /// <inheritdoc/>
            // [global::System.CodeDom.Compiler.GeneratedCode("...", "...")]
            // [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            // <MODIFIERS> <RETURN_TYPE> <METHOD_NAME>()
            // <METHOD_BODY>
            return
                MethodDeclaration(returnType, Identifier(bytecodeInfo.MethodName))
                .AddModifiers(bytecodeInfo.Modifiers.Select(Token).ToArray())
                .AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode"))
                        .AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(D2DPixelShaderSourceAttribute).FullName))),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(D2DPixelShaderSourceAttribute).Assembly.GetName().Version.ToString()))))))
                    .WithOpenBracketToken(Token(TriviaList(Comment(
                        $"/// <inheritdoc/>")),
                        SyntaxKind.OpenBracketToken,
                        TriviaList())),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage")))))
                .WithBody(methodBlock);
        }
    }
}