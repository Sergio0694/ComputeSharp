using System.Collections.Immutable;
using ComputeSharp.D2D1Interop.__Internals;
using ComputeSharp.D2D1Interop.SourceGenerators.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1Interop.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class LoadDispatchData
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchDataMethod</c> method.
        /// </summary>
        /// <param name="fieldInfos">The array of <see cref="FieldInfo"/> values for all captured fields.</param>
        /// <param name="root32BitConstantsCount">The total number of needed 32 bit constants in the shader root signature.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchDataMethod</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(ImmutableArray<FieldInfo> fieldInfos, int root32BitConstantsCount)
        {
            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.D2D1Interop.__Internals.ID2D1Shader.LoadDispatchData<TLoader>(ref TLoader loader)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier("LoadDispatchData"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1Interop.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TLoader")))
                .AddParameterListParameters(Parameter(Identifier("loader")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(IdentifierName("TLoader")))
                .WithBody(Block(GetDispatchDataLoadingStatements(fieldInfos, root32BitConstantsCount)));
        }

        /// <summary>
        /// Gets a sequence of statements to load the dispatch data for a given shader.
        /// </summary>
        /// <param name="fieldInfos">The array of <see cref="FieldInfo"/> values for all captured fields.</param>
        /// <param name="root32BitConstantsCount">The total number of needed 32 bit constants in the shader root signature.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to load shader dispatch data.</returns>
        private static ImmutableArray<StatementSyntax> GetDispatchDataLoadingStatements(ImmutableArray<FieldInfo> fieldInfos, int root32BitConstantsCount)
        {
            ImmutableArray<StatementSyntax>.Builder statements = ImmutableArray.CreateBuilder<StatementSyntax>();

            // Generate loading statements for each captured field
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                switch (fieldInfo)
                {
                    case FieldInfo.Primitive primitive:

                        // Read a primitive value and serialize it into the target buffer. This will generate:
                        //
                        // global::System.Runtime.CompilerServices.Unsafe.As<uint, global::<TYPE_NAME>>(
                        //     ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)<OFFSET>)) = <FIELD_PATH>
                        statements.Add(ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<uint, global::{primitive.TypeName}>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){primitive.Offset}))"),
                                ParseExpression($"{string.Join(".", primitive.FieldPath)}"))));
                        break;

                    case FieldInfo.NonLinearMatrix matrix:
                        string rowTypeName = $"global::ComputeSharp.{matrix.ElementName}{matrix.Columns}";
                        string rowLocalName = $"__{string.Join("_", matrix.FieldPath)}__row0";

                        // Declare a local to index into individual rows. This will generate:
                        //
                        // ref <ROW_TYPE> <ROW_NAME> = ref global::System.Runtime.CompilerServices.Unsafe.As<global::<TYPE_NAME>, <ROW_TYPE_NAME>>(
                        //     ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in <FIELD_PATH>));
                        statements.Add(ParseStatement($"ref {rowTypeName} {rowLocalName} = ref global::System.Runtime.CompilerServices.Unsafe.As<global::{matrix.TypeName}, {rowTypeName}>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in {string.Join(".", matrix.FieldPath)}));"));

                        // Generate the loading code for each individual row, with proper alignment.
                        // This will result in the following (assuming Float2x3 m):
                        //
                        // ref global::ComputeSharp.Float3 __m__row0 = ref global::System.Runtime.CompilerServices.Unsafe.As<global::ComputeSharp.Float2x3, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in m));
                        // global::System.Runtime.CompilerServices.Unsafe.As<uint, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)rawDataOffset)) =, global::System.Runtime.CompilerServices.Unsafe.Add(ref __m__row0, 0);
                        // global::System.Runtime.CompilerServices.Unsafe.As<uint, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)(rawDataOffset + 16))) = global::System.Runtime.CompilerServices.Unsafe.Add(ref __m__row0, 1);
                        for (int j = 0; j < matrix.Rows; j++)
                        {
                            statements.Add(ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<uint, {rowTypeName}>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){matrix.Offsets[j]}))"),
                                    ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.Add(ref {rowLocalName}, {j})"))));
                        }
                        break;
                }
            }

            // global::System.Span<uint> span0 = stackalloc uint[<VARIABLES>];
            statements.Insert(0,
                LocalDeclarationStatement(
                    VariableDeclaration(
                        GenericName(Identifier("global::System.Span"))
                        .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.UIntKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("span0"))
                        .WithInitializer(EqualsValueClause(
                            StackAllocArrayCreationExpression(
                                ArrayType(PredefinedType(Token(SyntaxKind.UIntKeyword)))
                                .AddRankSpecifiers(
                                    ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(root32BitConstantsCount)))))))))));

            // ref uint r0 = ref span0[0];
            statements.Insert(1,
                LocalDeclarationStatement(
                    VariableDeclaration(RefType(PredefinedType(Token(SyntaxKind.UIntKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("r0"))
                        .WithInitializer(EqualsValueClause(
                            RefExpression(
                                ElementAccessExpression(IdentifierName("span0"))
                                .AddArgumentListArguments(Argument(
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(0))))))))));

            // loader.LoadConstantBuffer(span0);
            statements.Add(
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("loader"),
                            IdentifierName("LoadConstantBuffer")))
                    .AddArgumentListArguments(Argument(IdentifierName("span0")))));

            return statements.ToImmutable();
        }
    }
}
