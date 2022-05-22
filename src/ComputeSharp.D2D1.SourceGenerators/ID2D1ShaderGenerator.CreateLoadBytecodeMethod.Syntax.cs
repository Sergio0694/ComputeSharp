using System;
using System.Globalization;
using System.Linq;
using ComputeSharp.__Internals;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>TryGetBytecode</c> method.
        /// </summary>
        /// <param name="bytecodeInfo">The input bytecode info.</param>
        /// <param name="fixup">An opaque <see cref="Func{TResult}"/> instance to transform the final tree into text.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslSource</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(EmbeddedBytecodeInfo bytecodeInfo, out Func<SyntaxNode, string> fixup)
        {
            BlockSyntax block = GetShaderBytecodeBody(bytecodeInfo, out string? bytecodeLiterals);

            if (bytecodeLiterals is not null)
            {
                fixup = tree => tree.ToFullString().Replace("__EMBEDDED_SHADER_BYTECODE", bytecodeLiterals);
            }
            else
            {
                fixup = static tree => tree.ToFullString();
            }

            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.LoadBytecode<TLoader>(ref TLoader loader, global::ComputeSharp.D2D1.D2D1ShaderProfile? shaderProfile, global::ComputeSharp.D2D1.D2D1CompileOptions? options)
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
                    Parameter(Identifier("shaderProfile")).WithType(NullableType(IdentifierName("global::ComputeSharp.D2D1.D2D1ShaderProfile"))),
                    Parameter(Identifier("options")).WithType(NullableType(IdentifierName("global::ComputeSharp.D2D1.D2D1CompileOptions"))))
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
            ExpressionSyntax optionsExpression =
                ParseExpression(
                    bytecodeInfo.CompileOptions
                    .GetValueOrDefault()
                    .ToString(CultureInfo.InvariantCulture)
                    .Split(',')
                    .Select(static name => $"global::ComputeSharp.D2D1.D2D1CompileOptions.{name.Trim()}")
                    .Aggregate("", static (left, right) => left.Length > 0 ? $"{left} | {right}" : right));

            // Add parantheses if needed
            if (optionsExpression is BinaryExpressionSyntax)
            {
                optionsExpression = ParenthesizedExpression(optionsExpression);
            }

            // This code produces a method declaration as follows:
            //
            // global::ComputeSharp.D2D1.__Internals.D2D1ShaderCompiler.LoadDynamicBytecode(ref loader, in this, shaderProfile ?? <DEFAULT_PROFILE>, options ?? <REQUESTED_OPTIONS>);
            ExpressionStatementSyntax dynamicLoadingStatement =
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("global::ComputeSharp.D2D1.__Internals.D2D1ShaderCompiler"),
                            IdentifierName("LoadDynamicBytecode")))
                    .AddArgumentListArguments(
                        Argument(IdentifierName("loader")).WithRefKindKeyword(Token(SyntaxKind.RefKeyword)),
                        Argument(ThisExpression()).WithRefKindKeyword(Token(SyntaxKind.InKeyword)),
                        Argument(BinaryExpression(
                            SyntaxKind.CoalesceExpression,
                            IdentifierName("shaderProfile"),
                            IdentifierName($"global::ComputeSharp.D2D1.D2D1ShaderProfile.{D2D1ShaderProfile.PixelShader50}"))),
                        Argument(BinaryExpression(
                            SyntaxKind.CoalesceExpression,
                            IdentifierName("options"),
                            optionsExpression))));

            // If there is no precompiled bytecode, just return the dynamic path
            if (bytecodeInfo.Bytecode.IsDefaultOrEmpty)
            {
                bytecodeLiterals = null;

                return Block(dynamicLoadingStatement);
            }

            bytecodeLiterals = BuildShaderBytecodeExpressionString(bytecodeInfo.Bytecode.AsSpan());

            // This code produces a branch as follows:
            //
            // if (shaderProfile is null or <SHADER_PROFILE> && options is null or <OPTIONS>)
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
                        IsPatternExpression(
                            IdentifierName("shaderProfile"),
                            BinaryPattern(
                                SyntaxKind.OrPattern,
                                ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression)),
                                ConstantPattern(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("global::ComputeSharp.D2D1.D2D1ShaderProfile"),
                                        IdentifierName(bytecodeInfo.ShaderProfile.GetValueOrDefault().ToString(CultureInfo.InvariantCulture)))))),
                        IsPatternExpression(
                            IdentifierName("options"),
                            BinaryPattern(
                                SyntaxKind.OrPattern,
                                ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression)),
                                ConstantPattern(optionsExpression)))),
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

            // Return the combined block
            return Block(embeddedBranch.WithElse(ElseClause(Block(dynamicLoadingStatement))));
        }

        /// <summary>
        /// A mapping of precomputed literals for all <see cref="byte"/> values.
        /// </summary>
        private static readonly string[] formattedBytes =
        {
            "0x00", "0x01", "0x02", "0x03", "0x04", "0x05", "0x06", "0x07", "0x08", "0x09", "0x0A", "0x0B", "0x0C", "0x0D", "0x0E", "0x0F",
            "0x10", "0x11", "0x12", "0x13", "0x14", "0x15", "0x16", "0x17", "0x18", "0x19", "0x1A", "0x1B", "0x1C", "0x1D", "0x1E", "0x1F",
            "0x20", "0x21", "0x22", "0x23", "0x24", "0x25", "0x26", "0x27", "0x28", "0x29", "0x2A", "0x2B", "0x2C", "0x2D", "0x2E", "0x2F",
            "0x30", "0x31", "0x32", "0x33", "0x34", "0x35", "0x36", "0x37", "0x38", "0x39", "0x3A", "0x3B", "0x3C", "0x3D", "0x3E", "0x3F",
            "0x40", "0x41", "0x42", "0x43", "0x44", "0x45", "0x46", "0x47", "0x48", "0x49", "0x4A", "0x4B", "0x4C", "0x4D", "0x4E", "0x4F",
            "0x50", "0x51", "0x52", "0x53", "0x54", "0x55", "0x56", "0x57", "0x58", "0x59", "0x5A", "0x5B", "0x5C", "0x5D", "0x5E", "0x5F",
            "0x60", "0x61", "0x62", "0x63", "0x64", "0x65", "0x66", "0x67", "0x68", "0x69", "0x6A", "0x6B", "0x6C", "0x6D", "0x6E", "0x6F",
            "0x70", "0x71", "0x72", "0x73", "0x74", "0x75", "0x76", "0x77", "0x78", "0x79", "0x7A", "0x7B", "0x7C", "0x7D", "0x7E", "0x7F",
            "0x80", "0x81", "0x82", "0x83", "0x84", "0x85", "0x86", "0x87", "0x88", "0x89", "0x8A", "0x8B", "0x8C", "0x8D", "0x8E", "0x8F",
            "0x90", "0x91", "0x92", "0x93", "0x94", "0x95", "0x96", "0x97", "0x98", "0x99", "0x9A", "0x9B", "0x9C", "0x9D", "0x9E", "0x9F",
            "0xA0", "0xA1", "0xA2", "0xA3", "0xA4", "0xA5", "0xA6", "0xA7", "0xA8", "0xA9", "0xAA", "0xAB", "0xAC", "0xAD", "0xAE", "0xAF",
            "0xB0", "0xB1", "0xB2", "0xB3", "0xB4", "0xB5", "0xB6", "0xB7", "0xB8", "0xB9", "0xBA", "0xBB", "0xBC", "0xBD", "0xBE", "0xBF",
            "0xC0", "0xC1", "0xC2", "0xC3", "0xC4", "0xC5", "0xC6", "0xC7", "0xC8", "0xC9", "0xCA", "0xCB", "0xCC", "0xCD", "0xCE", "0xCF",
            "0xD0", "0xD1", "0xD2", "0xD3", "0xD4", "0xD5", "0xD6", "0xD7", "0xD8", "0xD9", "0xDA", "0xDB", "0xDC", "0xDD", "0xDE", "0xDF",
            "0xE0", "0xE1", "0xE2", "0xE3", "0xE4", "0xE5", "0xE6", "0xE7", "0xE8", "0xE9", "0xEA", "0xEB", "0xEC", "0xED", "0xEE", "0xEF",
            "0xF0", "0xF1", "0xF2", "0xF3", "0xF4", "0xF5", "0xF6", "0xF7", "0xF8", "0xF9", "0xFA", "0xFB", "0xFC", "0xFD", "0xFE", "0xFF"
        };

        /// <summary>
        /// Creates a formatted expression to initialize a <see cref="ReadOnlySpan{T}"/> with a given shader data.
        /// </summary>
        /// <param name="bytecode">The input shader bytecode to serialize.</param>
        /// <returns>A formatted <see cref="string"/> with the serialized data.</returns>
        private static string BuildShaderBytecodeExpressionString(ReadOnlySpan<byte> bytecode)
        {
            //The estimation is 4 characters per byte (up to "255" in hex), plus ", " to separate sequential values
            using ArrayPoolStringBuilder builder = ArrayPoolStringBuilder.Create(bytecode.Length * 6);

            builder.Append(formattedBytes[bytecode[0]]);

            foreach (byte b in bytecode.Slice(1))
            {
                builder.Append(", ");
                builder.Append(formattedBytes[b]);
            }

            return builder.WrittenSpan.ToString();
        }
    }
}
