using System;
using System.Collections.Generic;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGenerators.Mappings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.SourceGenerators
{
    /// <inheritdoc/>
    public sealed partial class IShaderGenerator
    {
        /// <inheritdoc/>
        private static partial MethodDeclarationSyntax CreateLoadDispatchMetadataMethod(
            string? implicitTextureType,
            IReadOnlyCollection<string> discoveredResources,
            int root32BitConstantsCount,
            bool isSamplerUsed)
        {
            // This code produces a method declaration as follows:
            //
            // readonly void IShader.LoadDispatchMetadata<TMetadataLoader>(ref TMetadataLoader loader, out IntPtr result)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    Identifier("LoadDispatchMetadata"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName(nameof(IShader))))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TMetadataLoader")))
                .AddParameterListParameters(
                    Parameter(Identifier("loader"))
                        .AddModifiers(Token(SyntaxKind.RefKeyword))
                        .WithType(IdentifierName("TMetadataLoader")),
                    Parameter(Identifier("result"))
                        .AddModifiers(Token(SyntaxKind.OutKeyword))
                        .WithType(IdentifierName(typeof(IntPtr).Name)))
                .WithBody(Block(GetDispatchMetadataLoadingStatements(implicitTextureType, discoveredResources, root32BitConstantsCount, isSamplerUsed)));
        }

        /// <summary>
        /// Gets a sequence of statements to load the dispatch metadata for a given shader type.
        /// </summary>
        /// <param name="implicitTextureType">The implicit texture type, if available (if the shader is a pixel shader).</param>
        /// <param name="discoveredResources">The collection of resources used by the shader.</param>
        /// <param name="root32BitConstantsCount">The total number of 32 bit root constants being loaded for the shader.</param>
        /// <param name="isSamplerUsed">Whether or not the shader requires a static sampler to be available.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to load shader dispatch data.</returns>
        private static IEnumerable<StatementSyntax> GetDispatchMetadataLoadingStatements(
            string? implicitTextureType,
            IReadOnlyCollection<string> discoveredResources,
            int root32BitConstantsCount,
            bool isSamplerUsed)
        {
            // Span<byte> span0 = stackalloc byte[5];
            yield return
                LocalDeclarationStatement(
                    VariableDeclaration(
                        GenericName(Identifier("Span"))
                        .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("span0"))
                        .WithInitializer(EqualsValueClause(
                            StackAllocArrayCreationExpression(
                                ArrayType(PredefinedType(Token(SyntaxKind.ByteKeyword)))
                                .AddRankSpecifiers(
                                    ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(5))))))))));

            // Compute the total number of resources (discovered ones and the implicit target texture, if present)
            int totalResourcesCount = discoveredResources.Count + (implicitTextureType is not null ? 1 : 0);

            // Span<ResourceDescriptor> span1 = stackalloc ResourceDescriptor[<ADJUSTED_COUNT>];
            yield return
                LocalDeclarationStatement(
                    VariableDeclaration(
                        GenericName(Identifier("Span"))
                        .AddTypeArgumentListArguments(IdentifierName("ResourceDescriptor")))
                    .AddVariables(
                        VariableDeclarator(Identifier("span1"))
                        .WithInitializer(EqualsValueClause(
                            StackAllocArrayCreationExpression(
                                ArrayType(IdentifierName("ResourceDescriptor"))
                                .AddRankSpecifiers(
                                    ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(totalResourcesCount))))))))));

            // ref byte r0 = ref span0[0];
            yield return
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
                                        Literal(0)))))))));

            // ref ResourceDescriptor r1 = ref span1[0];
            yield return
                LocalDeclarationStatement(
                    VariableDeclaration(RefType(IdentifierName("ResourceDescriptor")))
                    .AddVariables(
                        VariableDeclarator(Identifier("r1"))
                        .WithInitializer(EqualsValueClause(
                            RefExpression(
                                ElementAccessExpression(IdentifierName("span1"))
                                .AddArgumentListArguments(Argument(
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(0)))))))));

            // Serialized shader metadata
            yield return ParseStatement($"Unsafe.WriteUnaligned<int>(ref Unsafe.Add(ref r0, 0), {root32BitConstantsCount});");
            yield return ParseStatement($"Unsafe.WriteUnaligned<bool>(ref Unsafe.Add(ref r0, 4), {isSamplerUsed.ToString().ToLowerInvariant()});");

            int
                constantBufferOffset = 1,
                readOnlyResourceOffset = 0,
                readWriteResourceOffset = 0,
                resourceOffset = 0;

            // Add the implicit texture descriptor, if needed
            if (implicitTextureType is not null)
            {
                yield return ParseStatement($"ResourceDescriptor.Create(1, {readWriteResourceOffset++}, out Unsafe.Add(ref r1, {resourceOffset++}));");
            }

            // Populate the sequence of resource descriptors
            foreach (var resource in discoveredResources)
            {
                if (HlslKnownTypes.IsConstantBufferType(resource))
                {
                    yield return ParseStatement($"ResourceDescriptor.Create(2, {constantBufferOffset++}, out Unsafe.Add(ref r1, {resourceOffset++}));");
                }   
                else if (HlslKnownTypes.IsReadOnlyTypedResourceType(resource))
                {
                    yield return ParseStatement($"ResourceDescriptor.Create(0, {readOnlyResourceOffset++}, out Unsafe.Add(ref r1, {resourceOffset++}));");
                }
                else
                {
                    yield return ParseStatement($"ResourceDescriptor.Create(1, {readWriteResourceOffset++}, out Unsafe.Add(ref r1, {resourceOffset++}));");
                }
            }

            // Invoke the value delegate to create the opaque root signature handle
            yield return ParseStatement("loader.LoadMetadataHandle(span0, span1, out result);");
        }
    }
}

