using System;
using System.Text;
using ComputeSharp.D2D1.__Internals;
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
    partial class EffectId
    {
        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectId</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="EquatableArray{T}"/> instance with the effect GUID.</param>
        /// <param name="fixup">An opaque <see cref="Func{TResult}"/> instance to transform the final tree into text.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectId</c> property.</returns>
        public static PropertyDeclarationSyntax GetSyntax(EquatableArray<byte> info, out Func<SyntaxNode, SourceText> fixup)
        {
            // Create the local declaration:
            //
            // global::System.ReadOnlySpan<byte> bytes = new byte[] { __EMBEDDED_EFFECT_ID_BYTES };
            LocalDeclarationStatementSyntax guidBytesDeclaration =
                LocalDeclarationStatement(
                    VariableDeclaration(
                        GenericName(Identifier("global::System.ReadOnlySpan"))
                        .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("bytes"))
                        .WithInitializer(
                            EqualsValueClause(
                                ArrayCreationExpression(
                                ArrayType(PredefinedType(Token(SyntaxKind.ByteKeyword)))
                                .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                            .WithInitializer(
                                InitializerExpression(
                                    SyntaxKind.ArrayInitializerExpression,
                                    SingletonSeparatedList<ExpressionSyntax>(IdentifierName("__EMBEDDED_EFFECT_ID_BYTES"))))))));

            // Prepare the initialization text
            string effectIdLiterals = SyntaxFormattingHelper.BuildByteArrayInitializationExpressionString(info.AsSpan());

            // Set the fixup function
            fixup = tree => SourceText.From(tree.ToFullString().Replace("__EMBEDDED_EFFECT_ID_BYTES", effectIdLiterals), Encoding.UTF8);

            // Prepare the return statement:
            //
            // return
            //     ref global::System.Runtime.CompilerServices.Unsafe.As<byte, global::System.Guid>(
            //         ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(bytes));
            ReturnStatementSyntax returnStatement =
                ReturnStatement(
                    RefExpression(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("global::System.Runtime.CompilerServices.Unsafe"),
                                GenericName(Identifier("As"))
                                .AddTypeArgumentListArguments(
                                    PredefinedType(Token(SyntaxKind.ByteKeyword)),
                                    IdentifierName("global::System.Guid"))))
                        .AddArgumentListArguments(
                            Argument(
                                InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("global::System.Runtime.InteropServices.MemoryMarshal"),
                                        IdentifierName("GetReference")))
                                .AddArgumentListArguments(Argument(IdentifierName("bytes"))))
                            .WithRefOrOutKeyword(Token(SyntaxKind.RefKeyword)))));

            // This code produces a property declaration as follows:
            //
            // readonly ref readonly global::System.Guid global::ComputeSharp.D2D1.__Internals.ID2D1Shader.EffectId
            // {
            //     [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            //     get
            //     {
            //         <BYTES_DECLARATION>
            //         <RETURN_STATEMENT>
            //     }
            // }
            return
                PropertyDeclaration(
                    RefType(IdentifierName("global::System.Guid")).WithReadOnlyKeyword(Token(SyntaxKind.ReadOnlyKeyword)),
                    Identifier(nameof(EffectId)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddAccessorListAccessors(
                    AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .AddAttributeLists(
                        AttributeList(SingletonSeparatedList(
                            Attribute(IdentifierName("global::System.Runtime.CompilerServices.MethodImpl"))
                            .AddArgumentListArguments(
                                AttributeArgument(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("global::System.Runtime.CompilerServices.MethodImplOptions"),
                                        IdentifierName("AggressiveInlining")))))))
                    .AddBodyStatements(guidBytesDeclaration, returnStatement));
        }
    }
}