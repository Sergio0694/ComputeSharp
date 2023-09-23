using System;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.SourceGenerators.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class GetOutputBuffer
    {
        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>BufferPrecision</c> property.
        /// </summary>
        /// <param name="info">The input info for the shader output buffer.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>BufferPrecision</c> property.</returns>
        public static PropertyDeclarationSyntax GetBufferPrecisionSyntax(OutputBufferInfo info)
        {
            ExpressionSyntax bufferPrecisionExpression;

            // Set the right expression if the buffer options are valid
            if (Enum.IsDefined(typeof(D2D1BufferPrecision), info.BufferPrecision))
            {
                bufferPrecisionExpression =
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("ComputeSharp.D2D1.D2D1BufferPrecision"),
                        IdentifierName(info.BufferPrecision.ToString()));
            }
            else
            {
                // Otherwise just return default (the analyzer will emit a diagnostic)
                bufferPrecisionExpression = LiteralExpression(SyntaxKind.DefaultLiteralExpression, Token(SyntaxKind.DefaultKeyword));
            }

            // This code produces a property declaration as follows:
            //
            // readonly ComputeSharp.D2D1.D2D1BufferPrecision global::ComputeSharp.D2D1.__Internals.ID2D1Shader.BufferPrecision => <BUFFER_PRECISION>;
            return
                PropertyDeclaration(IdentifierName("ComputeSharp.D2D1.D2D1BufferPrecision"), Identifier("BufferPrecision"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(bufferPrecisionExpression))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>GetOutputBuffer</c> method.
        /// </summary>
        /// <param name="info">The input info for the shader output buffer.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>GetOutputBuffer</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(OutputBufferInfo info)
        {
            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.GetOutputBuffer(out uint precision, out uint depth)
            // {
            //     precision = <BUFFER_PRECISION>;
            //     depth = <CHANNEL_DEPTH>;
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier(nameof(GetOutputBuffer)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddParameterListParameters(
                    Parameter(Identifier("precision")).AddModifiers(Token(SyntaxKind.OutKeyword)).WithType(PredefinedType(Token(SyntaxKind.UIntKeyword))),
                    Parameter(Identifier("depth")).AddModifiers(Token(SyntaxKind.OutKeyword)).WithType(PredefinedType(Token(SyntaxKind.UIntKeyword))))
                .WithBody(Block(
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("precision"),
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)info.BufferPrecision)))),
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("depth"),
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)info.ChannelDepth))))));
        }
    }
}