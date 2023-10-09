using System;
using System.Collections.Immutable;
using System.Globalization;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class IShaderGenerator
{
    /// <inheritdoc/>
    partial class LoadDispatchMetadata
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchMetadata</c> method.
        /// </summary>
        /// <param name="metadataInfo">The dispatch metadata for the current shader..</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchMetadata</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(DispatchMetadataInfo metadataInfo)
        {
            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.__Internals.IShader.LoadDispatchMetadata<TLoader>(ref TLoader loader, out global::System.IntPtr result)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    Identifier("LoadDispatchMetadata"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.__Internals.{nameof(IShader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TLoader")))
                .AddParameterListParameters(
                    Parameter(Identifier("loader"))
                        .AddModifiers(Token(SyntaxKind.RefKeyword))
                        .WithType(IdentifierName("TLoader")),
                    Parameter(Identifier("result"))
                        .AddModifiers(Token(SyntaxKind.OutKeyword))
                        .WithType(IdentifierName($"global::System.{nameof(IntPtr)}")))
                .WithBody(Block(GetDispatchMetadataLoadingStatements(metadataInfo)));
        }

        /// <summary>
        /// Gets a sequence of statements to load the dispatch metadata for a given shader type.
        /// </summary>
        /// <param name="metadataInfo">The dispatch metadata for the current shader..</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to load shader dispatch data.</returns>
        private static ImmutableArray<StatementSyntax> GetDispatchMetadataLoadingStatements(DispatchMetadataInfo metadataInfo)
        {
            using ImmutableArrayBuilder<StatementSyntax> statements = new();

            // global::System.Span<byte> span0 = stackalloc byte[5];
            statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(
                        GenericName(Identifier("global::System.Span"))
                        .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("span0"))
                        .WithInitializer(EqualsValueClause(
                            StackAllocArrayCreationExpression(
                                ArrayType(PredefinedType(Token(SyntaxKind.ByteKeyword)))
                                .AddRankSpecifiers(
                                    ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(5)))))))))));

            // Compute the total number of resources
            int totalResourcesCount = metadataInfo.ResourceDescriptors.Length;

            // Span<global::ComputeSharp.__Internals.ResourceDescriptor> span1 = stackalloc global::ComputeSharp.__Internals.ResourceDescriptor[<ADJUSTED_COUNT>];
            statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(
                        GenericName(Identifier("global::System.Span"))
                        .AddTypeArgumentListArguments(IdentifierName("global::ComputeSharp.__Internals.ResourceDescriptor")))
                    .AddVariables(
                        VariableDeclarator(Identifier("span1"))
                        .WithInitializer(EqualsValueClause(
                            StackAllocArrayCreationExpression(
                                ArrayType(IdentifierName("global::ComputeSharp.__Internals.ResourceDescriptor"))
                                .AddRankSpecifiers(
                                    ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(totalResourcesCount)))))))))));

            // ref byte r0 = ref span0[0];
            statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(RefType(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("r0"))
                        .WithInitializer(EqualsValueClause(
                            RefExpression(
                                ElementAccessExpression(IdentifierName("span0"))
                                .AddArgumentListArguments(Argument(
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(0))))))))));

            // ref global::ComputeSharp.__Internals.ResourceDescriptor r1 = ref span1[0];
            statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(RefType(IdentifierName("global::ComputeSharp.__Internals.ResourceDescriptor")))
                    .AddVariables(
                        VariableDeclarator(Identifier("r1"))
                        .WithInitializer(EqualsValueClause(
                            RefExpression(
                                ElementAccessExpression(IdentifierName("span1"))
                                .AddArgumentListArguments(Argument(
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(0))))))))));

            // Serialized shader metadata
            statements.Add(ParseStatement($"global::System.Runtime.CompilerServices.Unsafe.WriteUnaligned<int>(ref global::System.Runtime.CompilerServices.Unsafe.Add(ref r0, 0), {metadataInfo.Root32BitConstantCount});"));
            statements.Add(ParseStatement($"global::System.Runtime.CompilerServices.Unsafe.WriteUnaligned<bool>(ref global::System.Runtime.CompilerServices.Unsafe.Add(ref r0, 4), {metadataInfo.IsSamplerUsed.ToString(CultureInfo.InvariantCulture).ToLowerInvariant()});"));

            // Populate the sequence of resource descriptors
            foreach (ResourceDescriptor descriptor in metadataInfo.ResourceDescriptors)
            {
                statements.Add(ParseStatement($"global::ComputeSharp.__Internals.ResourceDescriptor.Create({descriptor.TypeId}, {descriptor.RegisterOffset}, out global::System.Runtime.CompilerServices.Unsafe.Add(ref r1, {descriptor.Offset}));"));
            }

            // Invoke the value delegate to create the opaque root signature handle
            statements.Add(ParseStatement("loader.LoadMetadataHandle(span0, span1, out result);"));

            return statements.ToImmutable();
        }
    }
}