using System.Collections.Immutable;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class InitializeFromDispatchData
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>InitializeFromDispatchData</c> method.
        /// </summary>
        /// <param name="fields">The discovered fields of the current shader.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>InitializeFromDispatchData</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(EquatableArray<FieldInfo> fields)
        {
            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.InitializeFromDispatchData(global::System.ReadOnlySpan<byte> data)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier(nameof(InitializeFromDispatchData)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddParameterListParameters(Parameter(Identifier("data")).WithType(
                    GenericName(Identifier("global::System.ReadOnlySpan"))
                    .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword)))))
                .WithBody(Block(GetDispatchDataUnloadingStatements(fields)));
        }

        /// <summary>
        /// Gets a sequence of statements to initialize a shader from a serialized dispatch data buffer.
        /// </summary>
        /// <param name="fieldInfos">The array of <see cref="FieldInfo"/> values for all captured fields.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to initialize a shader from the serialized dispatch data buffer.</returns>
        private static ImmutableArray<StatementSyntax> GetDispatchDataUnloadingStatements(ImmutableArray<FieldInfo> fieldInfos)
        {
            // If there are no fields, just return no statements
            if (fieldInfos.IsEmpty)
            {
                return ImmutableArray<StatementSyntax>.Empty;
            }

            using ImmutableArrayBuilder<StatementSyntax> statements = ImmutableArrayBuilder<StatementSyntax>.Rent();

            // Insert the fallback for empty shaders. This will generate the following code:
            //
            // if (data.IsEmpty)
            // {
            //     return;
            // }
            statements.Add(
                IfStatement(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("data"),
                        IdentifierName("IsEmpty")),
                    Block(ReturnStatement())));

            // ref readonly ConstantBuffer buffer = ref global::System.Runtime.CompilerServices.Unsafe.As<byte, ConstantBuffer>(ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(data));
            statements.Add(
                LocalDeclarationStatement(
                VariableDeclaration(RefType(IdentifierName("ConstantBuffer")).WithReadOnlyKeyword(Token(SyntaxKind.ReadOnlyKeyword)))
                .AddVariables(
                    VariableDeclarator(Identifier("buffer"))
                    .WithInitializer(
                        EqualsValueClause(RefExpression(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("global::System.Runtime.CompilerServices.Unsafe"),
                                    GenericName(Identifier("As"))
                                    .AddTypeArgumentListArguments(
                                        PredefinedType(Token(SyntaxKind.ByteKeyword)),
                                        IdentifierName("ConstantBuffer"))))
                            .AddArgumentListArguments(
                                Argument(
                                    InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("global::System.Runtime.InteropServices.MemoryMarshal"),
                                            IdentifierName("GetReference")))
                                    .AddArgumentListArguments(Argument(IdentifierName("data"))))
                                .WithRefOrOutKeyword(Token(SyntaxKind.RefKeyword)))))))));

            // Generate loading statements for each captured field
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                switch (fieldInfo)
                {
                    case FieldInfo.Primitive primitive:

                        // Read a primitive value:
                        //
                        // global::System.Runtime.CompilerServices.Unsafe.AsRef(in this.<FIELD_PATH>) = buffer.<CONSTANT_BUFFER_PATH>;
                        statements.Add(
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("global::System.Runtime.CompilerServices.Unsafe"),
                                            IdentifierName("AsRef")))
                                    .AddArgumentListArguments(
                                        Argument(MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            ThisExpression(),
                                            IdentifierName(string.Join(".", primitive.FieldPath))))
                                        .WithRefKindKeyword(Token(SyntaxKind.InKeyword))),
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("buffer"),
                                        IdentifierName(string.Join("_", primitive.FieldPath))))));
                        break;

                    case FieldInfo.NonLinearMatrix matrix:
                        string fieldPath = string.Join(".", matrix.FieldPath);
                        string fieldNamePrefix = string.Join("_", matrix.FieldPath);

                        // Read all rows of a given matrix type:
                        //
                        // global::System.Runtime.CompilerServices.Unsafe.AsRef(in this.<FIELD_PATH>)[0] = buffer.<CONSTANT_BUFFER_ROW_0_PATH>;
                        // global::System.Runtime.CompilerServices.Unsafe.AsRef(in this.<FIELD_PATH>)[1] = buffer.<CONSTANT_BUFFER_ROW_1_PATH>;
                        // ...
                        // global::System.Runtime.CompilerServices.Unsafe.AsRef(in this.<FIELD_PATH>)[N] = buffer.<CONSTANT_BUFFER_ROW_N_PATH>;
                        for (int j = 0; j < matrix.Rows; j++)
                        {
                            statements.Add(
                                ExpressionStatement(
                                    AssignmentExpression(
                                        SyntaxKind.SimpleAssignmentExpression,
                                        ElementAccessExpression(
                                            InvocationExpression(
                                                MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    IdentifierName("global::System.Runtime.CompilerServices.Unsafe"),
                                                    IdentifierName("AsRef")))
                                            .AddArgumentListArguments(
                                                Argument(MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    ThisExpression(),
                                                    IdentifierName(fieldPath)))
                                                .WithRefKindKeyword(Token(SyntaxKind.InKeyword))))
                                        .AddArgumentListArguments(
                                            Argument(LiteralExpression(
                                                SyntaxKind.NumericLiteralExpression,
                                                Literal(j)))),
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("buffer"),
                                            IdentifierName($"{fieldNamePrefix}_{j}")))));
                        }

                        break;
                }
            }

            return statements.ToImmutable();
        }
    }
}