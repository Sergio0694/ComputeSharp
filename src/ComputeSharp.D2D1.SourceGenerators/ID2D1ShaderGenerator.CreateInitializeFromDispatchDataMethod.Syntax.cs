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
    /// <inheritdoc/>
    partial class InitializeFromDispatchData
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>InitializeFromDispatchData</c> method.
        /// </summary>
        /// <param name="dispatchInfo">The dispatch info gathered for the current shader.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>InitializeFromDispatchData</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(DispatchDataInfo dispatchInfo)
        {
            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.InitializeFromDispatchData(global::System.ReadOnlySpan<byte> data)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier("InitializeFromDispatchData"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddParameterListParameters(Parameter(Identifier("data")).WithType(
                    GenericName(Identifier("global::System.ReadOnlySpan"))
                    .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword)))))
                .WithBody(Block(GetDispatchDataUnloadingStatements(dispatchInfo.FieldInfos)));
        }

        /// <summary>
        /// Gets a sequence of statements to initialize a shader from a serialized dispatch data buffer.
        /// </summary>
        /// <param name="fieldInfos">The array of <see cref="FieldInfo"/> values for all captured fields.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to initialize a shader from the serialized dispatch data buffer.</returns>
        private static ImmutableArray<StatementSyntax> GetDispatchDataUnloadingStatements(ImmutableArray<FieldInfo> fieldInfos)
        {
            ImmutableArray<StatementSyntax>.Builder statements = ImmutableArray.CreateBuilder<StatementSyntax>();

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

            // ref byte r0 = ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(data);
            statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(RefType(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("r0"))
                        .WithInitializer(EqualsValueClause(
                            RefExpression(
                                InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("global::System.Runtime.InteropServices.MemoryMarshal"),
                                        IdentifierName("GetReference")))
                                .AddArgumentListArguments(Argument(IdentifierName("data")))))))));

            // Generate loading statements for each captured field
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                switch (fieldInfo)
                {
                    case FieldInfo.Primitive primitive:

                        // Read a primitive value from the target buffer. This will generate:
                        //
                        // Unsafe.AsRef(in <FIELD_PATH>) = global::System.Runtime.CompilerServices.Unsafe.As<byte, global::<TYPE_NAME>>(
                        //     ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)<OFFSET>))
                        statements.Add(ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.AsRef(in {string.Join(".", primitive.FieldPath)})"),
                                ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<byte, global::{primitive.TypeName}>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){primitive.Offset}))"))));
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
                        // global::System.Runtime.CompilerServices.Unsafe.Add(ref __m__row0, 0) = global::System.Runtime.CompilerServices.Unsafe.As<byte, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)rawDataOffset));
                        // global::System.Runtime.CompilerServices.Unsafe.Add(ref __m__row0, 1) = global::System.Runtime.CompilerServices.Unsafe.As<byte, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)(rawDataOffset + 16)));
                        for (int j = 0; j < matrix.Rows; j++)
                        {
                            statements.Add(ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.Add(ref {rowLocalName}, {j})"),
                                    ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<byte, {rowTypeName}>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){matrix.Offsets[j]}))"))));
                        }
                        break;
                }
            }

            return statements.ToImmutable();
        }
    }
}
