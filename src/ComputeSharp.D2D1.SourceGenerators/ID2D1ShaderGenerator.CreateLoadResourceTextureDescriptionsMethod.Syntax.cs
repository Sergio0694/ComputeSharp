using System.Collections.Immutable;
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
    /// <inheritoc/>
    private static partial class LoadResourceTextureDescriptions
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadResourceTextureDescriptions</c> method.
        /// </summary>
        /// <param name="resourceTextureDescriptionsInfo">The resource texture descriptions info gathered for the current shader.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadResourceTextureDescriptions</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(ResourceTextureDescriptionsInfo resourceTextureDescriptionsInfo)
        {
            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.LoadResourceTextureDescriptions<TLoader>(ref TLoader loader)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier(nameof(LoadResourceTextureDescriptions)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TLoader")))
                .AddParameterListParameters(Parameter(Identifier("loader")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(IdentifierName("TLoader")))
                .WithBody(Block(GetInputDescriptionsLoadingStatements(resourceTextureDescriptionsInfo.ResourceTextureDescriptions)));
        }

        /// <summary>
        /// Gets a sequence of statements to load the resource texture descriptions for a given shader.
        /// </summary>
        /// <param name="resourceTextureDescriptionsInfo">The array of <see cref="ResourceTextureDescriptionsInfo"/> values for all available resource texture descriptions.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to load the resource texture descriptions data.</returns>
        private static ImmutableArray<StatementSyntax> GetInputDescriptionsLoadingStatements(ImmutableArray<ResourceTextureDescription> resourceTextureDescriptionsInfo)
        {
            // If there are no resource texture descriptions available, just load an empty buffer
            if (resourceTextureDescriptionsInfo.IsEmpty)
            {
                // loader.LoadResourceTextureDescriptions(default);
                return
                    ImmutableArray.Create<StatementSyntax>(
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("loader"),
                                    IdentifierName("LoadResourceTextureDescriptions")))
                            .AddArgumentListArguments(Argument(
                                LiteralExpression(
                                    SyntaxKind.DefaultLiteralExpression,
                                    Token(SyntaxKind.DefaultKeyword))))));
            }

            ImmutableArray<StatementSyntax>.Builder statements = ImmutableArray.CreateBuilder<StatementSyntax>();

            // The size of the buffer with the resource texture descriptions is the number of resurce textures descriptions,
            // times the size of each input description, which is a struct containing two int-sized fields (index and rank).
            int resourceTextureDescriptionSizeInBytes = resourceTextureDescriptionsInfo.Length * sizeof(int) * 2;

            // global::System.Span<byte> data = stackalloc byte[<RESOURCE_TEXTURE_DESCRIPTIONS_SIZE>];
            statements.Insert(0,
                LocalDeclarationStatement(
                    VariableDeclaration(
                        GenericName(Identifier("global::System.Span"))
                        .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("data"))
                        .WithInitializer(EqualsValueClause(
                            StackAllocArrayCreationExpression(
                                ArrayType(PredefinedType(Token(SyntaxKind.ByteKeyword)))
                                .AddRankSpecifiers(
                                    ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(resourceTextureDescriptionSizeInBytes)))))))))));

            // ref byte r0 = ref data[0];
            statements.Insert(1,
                LocalDeclarationStatement(
                    VariableDeclaration(RefType(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("r0"))
                        .WithInitializer(EqualsValueClause(
                            RefExpression(
                                ElementAccessExpression(IdentifierName("data"))
                                .AddArgumentListArguments(Argument(
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(0))))))))));

            int offset = 0;

            // Generate loading statements for each resource texture description
            foreach (ResourceTextureDescription resourceTextureDescription in resourceTextureDescriptionsInfo)
            {
                // Write the index of the current resource texture description:
                //
                // global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)<OFFSET>)) = <INDEX>;
                statements.Add(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){offset}))"),
                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)resourceTextureDescription.Index)))));

                // Write the rank of the current resource texture description:
                //
                // global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)<OFFSET> + 4)) = <FILTER>;
                statements.Add(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){offset + 4}))"),
                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)resourceTextureDescription.Rank)))));

                offset += sizeof(int) * 2;
            }

            // loader.LoadInputDescriptions(data);
            statements.Add(
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("loader"),
                            IdentifierName("LoadResourceTextureDescriptions")))
                    .AddArgumentListArguments(Argument(IdentifierName("data")))));

            return statements.ToImmutable();
        }
    }
}
