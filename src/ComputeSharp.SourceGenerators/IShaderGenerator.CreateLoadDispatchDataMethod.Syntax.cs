using System;
using System.Collections.Immutable;
using ComputeSharp.__Internals;
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
    partial class LoadDispatchData
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchDataMethod</c> method.
        /// </summary>
        /// <param name="shaderInterfaceType">The type of shader interface urrently being processed.</param>
        /// <param name="fieldInfos">The array of <see cref="FieldInfo"/> values for all captured fields.</param>
        /// <param name="resourceCount">The total number of captured resources in the shader.</param>
        /// <param name="root32BitConstantsCount">The total number of needed 32 bit constants in the shader root signature.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchDataMethod</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(
            Type shaderInterfaceType,
            ImmutableArray<FieldInfo> fieldInfos,
            int resourceCount,
            int root32BitConstantsCount)
        {
            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.__Internals.IShader.LoadDispatchData<TLoader>(ref TLoader loader, global::ComputeSharp.GraphicsDevice device, int x, int y, int z)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    Identifier("LoadDispatchData"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.__Internals.{nameof(IShader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TLoader")))
                .AddParameterListParameters(
                    Parameter(Identifier("loader"))
                        .AddModifiers(Token(SyntaxKind.RefKeyword))
                        .WithType(IdentifierName("TLoader")),
                    Parameter(Identifier("device")).WithType(IdentifierName("global::ComputeSharp.GraphicsDevice")),
                    Parameter(Identifier("x")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("y")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("z")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))))
                .WithBody(Block(GetDispatchDataLoadingStatements(shaderInterfaceType, fieldInfos, resourceCount, root32BitConstantsCount)));
        }

        /// <summary>
        /// Gets a sequence of statements to load the dispatch data for a given shader.
        /// </summary>
        /// <param name="shaderInterfaceType">The type of shader interface urrently being processed.</param>
        /// <param name="fieldInfos">The array of <see cref="FieldInfo"/> values for all captured fields.</param>
        /// <param name="resourceCount">The total number of captured resources in the shader.</param>
        /// <param name="root32BitConstantsCount">The total number of needed 32 bit constants in the shader root signature.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to load shader dispatch data.</returns>
        private static ImmutableArray<StatementSyntax> GetDispatchDataLoadingStatements(
            Type shaderInterfaceType,
            ImmutableArray<FieldInfo> fieldInfos,
            int resourceCount,
            int root32BitConstantsCount)
        {
            ImmutableArray<StatementSyntax>.Builder statements = ImmutableArray.CreateBuilder<StatementSyntax>();
            bool isComputeShader = shaderInterfaceType == typeof(IComputeShader);

            // Append the statements for the dispatch ranges:
            //
            // span0[0] = (uint)x;
            // span0[1] = (uint)y;
            // span0[2] = (uint)z;
            statements.Add(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ElementAccessExpression(IdentifierName("span0"))
                            .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)))),
                        CastExpression(PredefinedType(Token(SyntaxKind.UIntKeyword)), IdentifierName("x")))));

            statements.Add(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ElementAccessExpression(IdentifierName("span0"))
                            .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1)))),
                        CastExpression(PredefinedType(Token(SyntaxKind.UIntKeyword)), IdentifierName("y")))));

            // If the shader is a compute shader, also track the bounds on the Z axis
            if (isComputeShader)
            {
                statements.Add(
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            ElementAccessExpression(IdentifierName("span0"))
                                .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(2)))),
                            CastExpression(PredefinedType(Token(SyntaxKind.UIntKeyword)), IdentifierName("z")))));
            }

            // Generate loading statements for each captured field
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                switch (fieldInfo)
                {
                    case FieldInfo.Resource resource:

                        // Validate the resource and get a handle for it. This will generate:
                        //
                        // global::System.Runtime.CompilerServices.Unsafe.Add(ref r1, <OFFSET>) =
                        //     global::ComputeSharp.__Internals.GraphicsResourceHelper.ValidateAndGetGpuDescriptorHandle(<FIELD_NAME>, device);
                        statements.Add(ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.Add(ref r1, {resource.Offset})"),
                                ParseExpression($"global::ComputeSharp.__Internals.GraphicsResourceHelper.ValidateAndGetGpuDescriptorHandle({resource.FieldName}, device)"))));
                        break;
                    case FieldInfo.Primitive { TypeName: "System.Boolean" } primitive:

                        // Read a boolean value and cast it to Bool first, which will apply the correct size expansion. This will generate the following:
                        //
                        // global::System.Runtime.CompilerServices.Unsafe.As<uint, global::ComputeSharp.Bool>(
                        //     ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)<OFFSET>)) = (global::ComputeSharp.Bool)<FIELD_PATH>
                        statements.Add(ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<uint, global::ComputeSharp.Bool>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){primitive.Offset}))"),
                                ParseExpression($"(global::ComputeSharp.Bool){string.Join(".", primitive.FieldPath)}"))));
                        break;
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
                        // global::System.Runtime.CompilerServices.Unsafe.As<uint, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)rawDataOffset)) = global::System.Runtime.CompilerServices.Unsafe.Add(ref __m__row0, 0);
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

            // loader.LoadCapturedValues(span0);
            statements.Add(
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("loader"),
                            IdentifierName("LoadCapturedValues")))
                    .AddArgumentListArguments(Argument(IdentifierName("span0")))));

            if (resourceCount > 0)
            {
                // global::System.Span<ulong> span1 = stackalloc ulong[<RESOURCES>];
                statements.Insert(1,
                    LocalDeclarationStatement(
                        VariableDeclaration(
                            GenericName(Identifier("global::System.Span"))
                            .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ULongKeyword))))
                        .AddVariables(
                            VariableDeclarator(Identifier("span1"))
                            .WithInitializer(EqualsValueClause(
                                StackAllocArrayCreationExpression(
                                    ArrayType(PredefinedType(Token(SyntaxKind.ULongKeyword)))
                                    .AddRankSpecifiers(
                                        ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(resourceCount)))))))))));

                // ref ulong r1 = ref span1[0];
                statements.Insert(3,
                    LocalDeclarationStatement(
                        VariableDeclaration(RefType(PredefinedType(Token(SyntaxKind.ULongKeyword))))
                        .AddVariables(
                            VariableDeclarator(Identifier("r1"))
                            .WithInitializer(EqualsValueClause(
                                RefExpression(
                                    ElementAccessExpression(IdentifierName("span1"))
                                    .AddArgumentListArguments(Argument(
                                        LiteralExpression(
                                            SyntaxKind.NumericLiteralExpression,
                                            Literal(0))))))))));

                // loader.LoadCapturedResources(span1);
                statements.Add(
                    ExpressionStatement(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("loader"),
                                IdentifierName("LoadCapturedResources")))
                        .AddArgumentListArguments(Argument(IdentifierName("span1")))));
            }

            return statements.ToImmutable();
        }
    }
}
