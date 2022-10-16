using System;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class IShaderGenerator
{
    /// <inheritdoc/>
    partial class LoadBytecode
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>TryGetBytecode</c> method.
        /// </summary>
        /// <param name="bytecodeInfo">The input <see cref="EmbeddedBytecodeInfo"/> instance.</param>
        /// <param name="supportsDynamicShaders">Indicates whether or not dynamic shaders are supported.</param>
        /// <param name="fixup">An opaque <see cref="Func{TResult}"/> instance to transform the final tree into text.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslSource</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(EmbeddedBytecodeInfo bytecodeInfo, bool supportsDynamicShaders, out Func<SyntaxNode, SourceText> fixup)
        {
            BlockSyntax block = GetShaderBytecodeBody(bytecodeInfo, supportsDynamicShaders, out string? bytecodeLiterals);

            if (bytecodeLiterals is not null)
            {
                fixup = tree => SourceText.From(tree.ToFullString().Replace("__EMBEDDED_SHADER_BYTECODE", bytecodeLiterals), Encoding.UTF8);
            }
            else
            {
                fixup = static tree => tree.GetText(Encoding.UTF8);
            }

            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.__Internals.IShader.LoadBytecode<TLoader>(ref TLoader loader, int threadsX, int threadsY, int threadsZ)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    Identifier("LoadBytecode"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.__Internals.{nameof(IShader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TLoader")))
                .AddParameterListParameters(
                    Parameter(Identifier("loader"))
                        .AddModifiers(Token(SyntaxKind.RefKeyword))
                        .WithType(IdentifierName("TLoader")),
                    Parameter(Identifier("threadsX")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("threadsY")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("threadsZ")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))))
                .WithBody(block);
        }

        /// <summary>
        /// Gets a <see cref="BlockSyntax"/> instance with the logic to try to get a compiled shader bytecode.
        /// </summary>
        /// <param name="bytecodeInfo">The input <see cref="EmbeddedBytecodeInfo"/> instance.</param>
        /// <param name="supportsDynamicShaders">Indicates whether or not dynamic shaders are supported.</param>
        /// <param name="bytecodeLiterals">The resulting bytecode literals to insert into the final source code.</param>
        /// <returns>The <see cref="BlockSyntax"/> instance trying to retrieve the precompiled shader.</returns>
        private static unsafe BlockSyntax GetShaderBytecodeBody(EmbeddedBytecodeInfo bytecodeInfo, bool supportsDynamicShaders, out string? bytecodeLiterals)
        {
            if (bytecodeInfo.Bytecode.IsEmpty)
            {
                bytecodeLiterals = null;

                if (supportsDynamicShaders)
                {
                    // Return the dynamic compilation path
                    return Block(GetDynamicCompilationStatement());
                }
                else
                {
                    // Faulting path
                    return Block(GetDynamicCompilationFaultingBlock());
                }
            }

            bytecodeLiterals = SyntaxFormattingHelper.BuildByteArrayInitializationExpressionString(bytecodeInfo.Bytecode.AsSpan());

            // This code produces a branch as follows:
            //
            // if (threadsX == <X> && threadsY == <Y> && threadsZ == <Z>)
            // {
            //     global::System.ReadOnlySpan<byte> bytecode = new byte[] { __EMBEDDED_SHADER_BYTECODE };
            //     loader.LoadEmbeddedBytecode(bytecode);
            // }
            //
            // Note that the __EMBEDDED_SHADER_BYTECODE identifier will be replaced after the string
            // is created by the generator. This greatly speeds up the code generation, as it avoids
            // having to create/parse tens of thousands of literal expression nodes for every shader.
            IfStatementSyntax embeddedBranch =
                IfStatement(
                    BinaryExpression(
                        SyntaxKind.LogicalAndExpression,
                        BinaryExpression(
                            SyntaxKind.LogicalAndExpression,
                            BinaryExpression(
                                SyntaxKind.EqualsExpression,
                                IdentifierName("threadsX"),
                                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(bytecodeInfo.X))),
                            BinaryExpression(
                                SyntaxKind.EqualsExpression,
                                IdentifierName("threadsY"),
                                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(bytecodeInfo.Y)))),
                        BinaryExpression(
                            SyntaxKind.EqualsExpression,
                            IdentifierName("threadsZ"),
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(bytecodeInfo.Z)))),
                    Block(
                        LocalDeclarationStatement(
                            VariableDeclaration(
                                GenericName(Identifier("global::System.ReadOnlySpan"))
                                .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                            .AddVariables(
                                VariableDeclarator(Identifier("bytecode"))
                                .WithInitializer(
                                    EqualsValueClause(
                                        ArrayCreationExpression(
                                        ArrayType(PredefinedType(Token(SyntaxKind.ByteKeyword)))
                                        .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                                    .WithInitializer(
                                        InitializerExpression(
                                            SyntaxKind.ArrayInitializerExpression,
                                            SingletonSeparatedList<ExpressionSyntax>(IdentifierName("__EMBEDDED_SHADER_BYTECODE")))))))),
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("loader"),
                                    IdentifierName("LoadEmbeddedBytecode")))
                            .AddArgumentListArguments(Argument(IdentifierName("bytecode"))))));

            // If dynamic shaders are supported, return the combined block
            if (supportsDynamicShaders)
            {
                return Block(embeddedBranch.WithElse(ElseClause(Block(GetDynamicCompilationStatement()))));
            }

            // If there is no dynamic shader support, add the faulting path after the branch
            return Block(embeddedBranch.WithElse(ElseClause(Block(GetDynamicCompilationFaultingBlock()))));
        }

        /// <summary>
        /// The cached tree of dynamic statements.
        /// </summary>
        private static StatementSyntax? dynamicStatement;

        /// <summary>
        /// Returns the <see cref="StatementSyntax"/> instance to create a new dynamic shader.
        /// </summary>
        /// <returns>The <see cref="StatementSyntax"/> instance to create a new dynamic shader.</returns>
        private static StatementSyntax GetDynamicCompilationStatement()
        {
            // This code produces a method declaration as follows:
            //
            // ComputeSharp.__Internals.ShaderCompiler.LoadDynamicBytecode(ref loader, threadsX, threadsY, threadsZ, in this);
            return dynamicStatement ??=
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("ComputeSharp.__Internals.ShaderCompiler"),
                            IdentifierName("LoadDynamicBytecode")))
                    .AddArgumentListArguments(
                        Argument(IdentifierName("loader")).WithRefKindKeyword(Token(SyntaxKind.RefKeyword)),
                        Argument(IdentifierName("threadsX")),
                        Argument(IdentifierName("threadsY")),
                        Argument(IdentifierName("threadsZ")),
                        Argument(ThisExpression()).WithRefKindKeyword(Token(SyntaxKind.InKeyword))));
        }

        /// <summary>
        /// The cached tree of dynamic fault statement.
        /// </summary>
        private static ExpressionStatementSyntax? dynamicFaultStatement;

        /// <summary>
        /// Returns a <see cref="ExpressionStatementSyntax"/> instances to perform a faulting call to <c>LoadDynamicBytecode(IntPtr)</c>.
        /// </summary>
        /// <returns>A <see cref="ExpressionStatementSyntax"/> instances to perform a faulting call to <c>LoadDynamicBytecode(IntPtr)</c>.</returns>
        private static ExpressionStatementSyntax GetDynamicCompilationFaultingBlock()
        {
            // This code produces a faulting call as follows:
            //
            // loader.LoadDynamicBytecode(default);
            return dynamicFaultStatement ??=
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("loader"),
                            IdentifierName("LoadDynamicBytecode")))
                    .AddArgumentListArguments(
                        Argument(LiteralExpression(
                            SyntaxKind.DefaultLiteralExpression,
                            Token(SyntaxKind.DefaultKeyword)))));
        }
    }
}