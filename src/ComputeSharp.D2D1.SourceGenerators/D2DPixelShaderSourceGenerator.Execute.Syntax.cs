using System;
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
        public static MethodDeclarationSyntax GetSyntax(EmbeddedBytecodeInfo bytecodeInfo, out Func<SyntaxNode, SourceText> fixup)
        {
            // If there is no precompiled bytecode, just emit a span with a 0 value
            string bytecodeLiterals = bytecodeInfo.Bytecode.IsDefaultOrEmpty
                ? "0"
                : ID2D1ShaderGenerator.LoadBytecode.BuildShaderBytecodeExpressionString(bytecodeInfo.Bytecode.AsSpan());

            // Prepare the fixup function
            fixup = tree => SourceText.From(tree.ToFullString().Replace("__EMBEDDED_SHADER_BYTECODE", bytecodeLiterals), Encoding.UTF8);

            // This code produces a branch as follows:
            //
            // /// <inheritdoc/>
            // [global::System.CodeDom.Compiler.GeneratedCode("...", "...")]
            // [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            // <MODIFIERS> global::System.ReadOnlySpan<byte> <METHOD_NAME>()
            // {
            //     return new byte[] { __EMBEDDED_SHADER_BYTECODE };
            // }
            //
            // As in the ID2D1Shader generator, the __EMBEDDED_SHADER_BYTECODE identifier will be replaced with the bytecode
            // after the full string for the syntax tree has been generated, to avoid every literal going through a token.
            return
                MethodDeclaration(
                    GenericName(Identifier("global::System.ReadOnlySpan"))
                    .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))),
                    Identifier("TheMethodName")).AddModifiers(Token(SyntaxKind.PartialKeyword))
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
                .WithBody(Block(
                    ReturnStatement(
                        ArrayCreationExpression(
                            ArrayType(PredefinedType(Token(SyntaxKind.ByteKeyword)))
                            .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                        .WithInitializer(
                            InitializerExpression(
                                SyntaxKind.ArrayInitializerExpression,
                                SingletonSeparatedList<ExpressionSyntax>(IdentifierName("__EMBEDDED_SHADER_BYTECODE")))))));
        }
    }
}
