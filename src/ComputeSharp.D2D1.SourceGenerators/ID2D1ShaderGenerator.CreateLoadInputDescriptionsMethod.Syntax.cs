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
    private static partial class LoadInputDescriptions
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadInputDescriptions</c> method.
        /// </summary>
        /// <param name="inputDescriptionsInfo">The input descriptions info gathered for the current shader.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadInputDescriptions</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(InputDescriptionsInfo inputDescriptionsInfo)
        {
            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.LoadInputDescriptions<TLoader>(ref TLoader loader)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier(nameof(LoadInputDescriptions)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TLoader")))
                .AddParameterListParameters(Parameter(Identifier("loader")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(IdentifierName("TLoader")))
                .WithBody(Block(GetInputDescriptionsLoadingStatements(inputDescriptionsInfo.InputDescriptions)));
        }

        /// <summary>
        /// Gets a sequence of statements to load the input descriptions for a given shader.
        /// </summary>
        /// <param name="inputDescriptions">The array of <see cref="InputDescription"/> values for all available input descriptions.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to load shader dispatch data.</returns>
        private static ImmutableArray<StatementSyntax> GetInputDescriptionsLoadingStatements(ImmutableArray<InputDescription> inputDescriptions)
        {
            // If there are no input descriptions available, just load an empty buffer
            if (inputDescriptions.IsEmpty)
            {
                // loader.LoadInputDescriptions(default);
                return
                    ImmutableArray.Create<StatementSyntax>(
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("loader"),
                                    IdentifierName("LoadInputDescriptions")))
                            .AddArgumentListArguments(Argument(
                                LiteralExpression(
                                    SyntaxKind.DefaultLiteralExpression,
                                    Token(SyntaxKind.DefaultKeyword))))));
            }

            ImmutableArray<StatementSyntax>.Builder statements = ImmutableArray.CreateBuilder<StatementSyntax>();

            // The size of the buffer with the input descriptions is the number of input descriptions, times the size of each
            // input description, which is a struct containing three int-sized fields (index, filter, and level of detail).
            int inputDescriptionSizeInBytes = inputDescriptions.Length * sizeof(int) * 3;

            // global::System.Span<byte> data = stackalloc byte[<INPUT_DESCRIPTIONS_SIZE>];
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
                                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(inputDescriptionSizeInBytes)))))))))));

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

            // Generate loading statements for each input description
            foreach (InputDescription inputDescription in inputDescriptions)
            {
                // Write the index of the current input description:
                //
                // global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)<OFFSET>)) = <INDEX>;
                statements.Add(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){offset}))"),
                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)inputDescription.Index)))));

                // Write the filter of the current input description:
                //
                // global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)<OFFSET> + 4)) = <FILTER>;
                statements.Add(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){offset + 4}))"),
                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)inputDescription.Filter)))));

                // Write the level of detail of the current input description:
                //
                // global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)<OFFSET> + 8)) = <LEVEL_OF_DETAIL>;
                statements.Add(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){offset + 8}))"),
                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)inputDescription.LevelOfDetail)))));

                offset += sizeof(int) * 3;
            }

            // loader.LoadInputDescriptions(data);
            statements.Add(
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("loader"),
                            IdentifierName("LoadInputDescriptions")))
                    .AddArgumentListArguments(Argument(IdentifierName("data")))));

            return statements.ToImmutable();
        }
    }
}
