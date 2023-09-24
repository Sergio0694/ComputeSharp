using System;
using System.Globalization;
using System.Linq;
using System.Text;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class LoadBytecode
    {
        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>ShaderProfile</c> property.
        /// </summary>
        /// <param name="bytecodeInfo">The input bytecode info.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>ShaderProfile</c> property.</returns>
        public static PropertyDeclarationSyntax GetShaderProfileSyntax(EmbeddedBytecodeInfo bytecodeInfo)
        {
            // This code produces a method declaration as follows:
            //
            // readonly ComputeSharp.D2D1.D2D1ShaderProfile global::ComputeSharp.D2D1.__Internals.ID2D1Shader.ShaderProfile => <SHADER_PROFILE>;
            return
                PropertyDeclaration(IdentifierName("ComputeSharp.D2D1.D2D1ShaderProfile"), Identifier(nameof(ID2D1Shader.ShaderProfile)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("global::ComputeSharp.D2D1.D2D1ShaderProfile"),
                        IdentifierName(bytecodeInfo.ShaderProfile.ToString(CultureInfo.InvariantCulture)))))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>CompileOptions</c> property.
        /// </summary>
        /// <param name="bytecodeInfo">The input bytecode info.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>CompileOptions</c> property.</returns>
        public static PropertyDeclarationSyntax GetCompileOptionsSyntax(EmbeddedBytecodeInfo bytecodeInfo)
        {
            // Get a formatted representation of the compile options being used
            ExpressionSyntax compileOptionsExpression =
                ParseExpression(
                    bytecodeInfo.CompileOptions
                    .ToString(CultureInfo.InvariantCulture)
                    .Split(',')
                    .Select(static name => $"global::ComputeSharp.D2D1.D2D1CompileOptions.{name.Trim()}")
                    .Aggregate("", static (left, right) => left.Length > 0 ? $"{left} | {right}" : right));

            // This code produces a method declaration as follows:
            //
            // readonly ComputeSharp.D2D1.D2D1CompileOptions global::ComputeSharp.D2D1.__Internals.ID2D1Shader.CompileOptions => <COMPILE_OPTIONS_EXPRESSION>;
            return
                PropertyDeclaration(IdentifierName("ComputeSharp.D2D1.D2D1CompileOptions"), Identifier(nameof(ID2D1Shader.CompileOptions)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(compileOptionsExpression))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>TryGetBytecode</c> method.
        /// </summary>
        /// <param name="bytecodeInfo">The input bytecode info.</param>
        /// <param name="fixup">An opaque <see cref="Func{TResult}"/> instance to transform the final tree into text.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslSource</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(EmbeddedBytecodeInfo bytecodeInfo, out Func<SyntaxNode, SourceText> fixup)
        {
            BlockSyntax block = GetShaderBytecodeBody(bytecodeInfo, out string? bytecodeLiterals);

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
            // readonly void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.LoadBytecode<TLoader>(ref TLoader loader, ref global::ComputeSharp.D2D1.D2D1ShaderProfile? shaderProfile, ref global::ComputeSharp.D2D1.D2D1CompileOptions? compileOptions)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier(nameof(LoadBytecode)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TLoader")))
                .AddParameterListParameters(
                    Parameter(Identifier("loader")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(IdentifierName("TLoader")),
                    Parameter(Identifier("shaderProfile")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(NullableType(IdentifierName("global::ComputeSharp.D2D1.D2D1ShaderProfile"))),
                    Parameter(Identifier("compileOptions")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(NullableType(IdentifierName("global::ComputeSharp.D2D1.D2D1CompileOptions"))))
                .WithBody(block);
        }

        /// <summary>
        /// Gets a <see cref="BlockSyntax"/> instance with the logic to try to get a compiled shader bytecode.
        /// </summary>
        /// <param name="bytecodeInfo">The input bytecode info.</param>
        /// <param name="bytecodeLiterals">The resulting bytecode literals to insert into the final source code.</param>
        /// <returns>The <see cref="BlockSyntax"/> instance trying to retrieve the precompiled shader.</returns>
        private static unsafe BlockSyntax GetShaderBytecodeBody(EmbeddedBytecodeInfo bytecodeInfo, out string? bytecodeLiterals)
        {
            // This method needs to handle several different scenarions, which are as follows:
            //   1) If [EmbeddedBytecode] is used, then the shader is precompiled and stored as a ReadOnlySpan<byte>. In this case,
            //      this bytecode would have been generated with some specific D2D1ShaderProfile and D2D1CompileOptions values. So,
            //      a branch is generated to try to retrieve this precompiled bytecode, which can be returned if and only if the
            //      requested shader profile and compile options are either null, or either matches the values that have been used
            //      to generate that bytecode. If this is the case, then no further work is done and that binary data is returned.
            //   2) If either the requested shader profile or options do not match the values used to create the shader bytecode,
            //      then a call to the internal D2D1ShaderCompiler.LoadDynamicBytecode API is used, to create the shader on the fly.
            //   3) If [EmbeddedBytecode] is not used, then regardless of the requested profile and options, a new shader is compiled.
            //   4) If this is the case and either the shader profile or the options are null, then this method will manually set the
            //      value being null to the default one to use. That is, D2D1ShaderProfile.PixelShader50 will be used as the target,
            //      and the options will be set to D2D1CompileOptions.Default (and optionally, D2D1CompileOptions.EnableLinking too).

            // Get a formatted representation of the compile options being used
            ExpressionSyntax compileOptionsExpression =
                ParseExpression(
                    bytecodeInfo.CompileOptions
                    .ToString(CultureInfo.InvariantCulture)
                    .Split(',')
                    .Select(static name => $"global::ComputeSharp.D2D1.D2D1CompileOptions.{name.Trim()}")
                    .Aggregate("", static (left, right) => left.Length > 0 ? $"{left} | {right}" : right));

            // Add parantheses if needed
            if (compileOptionsExpression is BinaryExpressionSyntax)
            {
                compileOptionsExpression = ParenthesizedExpression(compileOptionsExpression);
            }

            // This code produces a block declaration as follows:
            //
            // global::ComputeSharp.D2D1.D2D1ShaderProfile effectiveShaderProfile = shaderProfile ??= <DEFAULT_PROFILE>;
            // global::ComputeSharp.D2D1.D2D1CompileOptions effectiveCompileOptions = compileOptions ??= <REQUESTED_OPTIONS>;
            //
            // global::ComputeSharp.D2D1.__Internals.D2D1ShaderCompiler.LoadDynamicBytecode(ref loader, in this, effectiveShaderProfile, effectiveCompileOptions);
            BlockSyntax dynamicLoadingBlock =
                Block(
                    LocalDeclarationStatement(
                        VariableDeclaration(IdentifierName("global::ComputeSharp.D2D1.D2D1ShaderProfile"))
                        .AddVariables(
                            VariableDeclarator(Identifier("effectiveShaderProfile"))
                            .WithInitializer(EqualsValueClause(
                                AssignmentExpression(
                                    SyntaxKind.CoalesceAssignmentExpression,
                                    IdentifierName("shaderProfile"),
                                    IdentifierName($"global::ComputeSharp.D2D1.D2D1ShaderProfile.{D2D1ShaderProfile.PixelShader50}")))))),
                    LocalDeclarationStatement(
                        VariableDeclaration(IdentifierName("global::ComputeSharp.D2D1.D2D1CompileOptions"))
                        .AddVariables(
                            VariableDeclarator(Identifier("effectiveCompileOptions"))
                            .WithInitializer(EqualsValueClause(
                                AssignmentExpression(
                                    SyntaxKind.CoalesceAssignmentExpression,
                                    IdentifierName("compileOptions"),
                                    compileOptionsExpression))))),
                    ExpressionStatement(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("global::ComputeSharp.D2D1.__Internals.D2D1ShaderCompiler"),
                                IdentifierName("LoadDynamicBytecode")))
                        .AddArgumentListArguments(
                            Argument(IdentifierName("loader")).WithRefKindKeyword(Token(SyntaxKind.RefKeyword)),
                            Argument(ThisExpression()).WithRefKindKeyword(Token(SyntaxKind.InKeyword)),
                            Argument(IdentifierName("effectiveShaderProfile")),
                            Argument(IdentifierName("effectiveCompileOptions")))));

            // If there is no precompiled bytecode, just return the dynamic path
            if (bytecodeInfo.Bytecode.IsDefaultOrEmpty)
            {
                bytecodeLiterals = null;

                return dynamicLoadingBlock;
            }

            bytecodeLiterals = SyntaxFormattingHelper.BuildByteArrayInitializationExpressionString(bytecodeInfo.Bytecode.AsSpan());

            // Prepare the expression for the default shader profile to use
            ExpressionSyntax shaderProfileExpression =
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("global::ComputeSharp.D2D1.D2D1ShaderProfile"),
                    IdentifierName(bytecodeInfo.ShaderProfile.ToString(CultureInfo.InvariantCulture)));

            // This code produces a branch as follows:
            //
            // if (shaderProfile is null or <SHADER_PROFILE> && compileOptions is null or <COMPILE_OPTIONS>)
            // {
            //     global::System.ReadOnlySpan<byte> bytecode = new byte[] { __EMBEDDED_SHADER_BYTECODE };
            //
            //     loader.LoadEmbeddedBytecode(bytecode);
            //
            //     shaderProfile = <SHADER_PROFILE>;
            //     compileOptions =  <COMPILE_OPTIONS>;
            // }
            //
            // Note that the __EMBEDDED_SHADER_BYTECODE identifier will be replaced after the string
            // is created by the generator. This greatly speeds up the code generation, as it avoids
            // having to create/parse tens of thousands of literal expression nodes for every shader.
            IfStatementSyntax embeddedBranch =
                IfStatement(
                    BinaryExpression(
                        SyntaxKind.LogicalAndExpression,
                        IsPatternExpression(
                            IdentifierName("shaderProfile"),
                            BinaryPattern(
                                SyntaxKind.OrPattern,
                                ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression)),
                                ConstantPattern(shaderProfileExpression))),
                        IsPatternExpression(
                            IdentifierName("compileOptions"),
                            BinaryPattern(
                                SyntaxKind.OrPattern,
                                ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression)),
                                ConstantPattern(compileOptionsExpression)))),
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
                            .AddArgumentListArguments(Argument(IdentifierName("bytecode")))),
                        ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                IdentifierName("shaderProfile"),
                                shaderProfileExpression)),
                        ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                IdentifierName("compileOptions"),
                                compileOptionsExpression))));

            // Return the combined block
            return Block(embeddedBranch.WithElse(ElseClause(dynamicLoadingBlock)));
        }
    }
}