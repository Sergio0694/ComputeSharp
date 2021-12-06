using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using ComputeSharp.__Internals;
using ComputeSharp.Core.Helpers;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
public sealed partial class IShaderGenerator2
{
    /// <summary>
    /// Explores a given type hierarchy and generates statements to load fields.
    /// </summary>
    /// <param name="structDeclarationSymbol">The current shader type being explored.</param>
    /// <param name="shaderInterfaceType">The type of shader interface urrently being processed.</param>
    /// <param name="resourceCount">The total number of captured resources in the shader.</param>
    /// <param name="root32BitConstantCount">The total number of needed 32 bit constants in the shader root signature.</param>
    /// <param name="diagnostics">The resulting diagnostics from the processing operation.</param>
    /// <returns>The sequence of <see cref="FieldInfo"/> instances for all captured resources and values.</returns>
    private static ImmutableArray<FieldInfo> GetCapturedFieldInfos(
        ITypeSymbol structDeclarationSymbol,
        Type shaderInterfaceType,
        out int resourceCount,
        out int root32BitConstantCount,
        out ImmutableArray<Diagnostic> diagnostics)
    {
        // Helper method that uses boxes instead of ref-s (illegal in enumerators)
        static IEnumerable<FieldInfo> GetCapturedFieldInfos(
            ITypeSymbol currentTypeSymbol,
            ImmutableArray<string> fieldPath,
            StrongBox<int> resourceOffset,
            StrongBox<int> rawDataOffset)
        {
            bool isFirstField = true;

            foreach (
               IFieldSymbol fieldSymbol in
               from fieldSymbol in currentTypeSymbol.GetMembers().OfType<IFieldSymbol>()
               where fieldSymbol.Type is INamedTypeSymbol { IsStatic: false } &&
                     !fieldSymbol.IsConst && !fieldSymbol.IsStatic &&
                     !fieldSymbol.IsFixedSizeBuffer
               select fieldSymbol)
            {
                string fieldName = fieldSymbol.Name;
                string typeName = fieldSymbol.Type.GetFullMetadataName();

                // Disambiguates the name of target fields against the current input parameters
                if (fieldName is "loader" or "device" or "x" or "y" or "z")
                {
                    fieldName = $"this.{fieldName}";
                }

                // The first item in each nested struct needs to be aligned to 16 bytes
                if (isFirstField && fieldPath.Length > 0)
                {
                    rawDataOffset.Value = AlignmentHelper.Pad(rawDataOffset.Value, 16);

                    isFirstField = false;
                }

                if (HlslKnownTypes.IsTypedResourceType(typeName))
                {
                    yield return new FieldInfo.Resource(fieldName, typeName, resourceOffset.Value++);
                }
                else if (HlslKnownTypes.IsKnownHlslType(typeName))
                {
                    yield return GetHlslKnownTypeFieldInfo(fieldPath.Add(fieldName), typeName, ref rawDataOffset.Value);
                }
                else if (fieldSymbol.Type.IsUnmanagedType)
                {
                    // Custom struct type defined by the user
                    foreach (var fieldInfo in GetCapturedFieldInfos(fieldSymbol.Type, fieldPath.Add(fieldName), resourceOffset, rawDataOffset))
                    {
                        yield return fieldInfo;
                    }
                }
            }
        }

        ImmutableArray<Diagnostic>.Builder builder = ImmutableArray.CreateBuilder<Diagnostic>();

        // Setup the resource and byte offsets for tracking. Pixel shaders have only two
        // implicitly captured int values, as they're always dispatched over a 2D texture.
        bool isComputeShader = shaderInterfaceType == typeof(IComputeShader);
        int initialRawDataOffset = sizeof(int) * (isComputeShader ? 3 : 2);
        StrongBox<int> resourceOffsetAsBox = new();
        StrongBox<int> rawDataOffsetAsBox = new(initialRawDataOffset);

        // Traverse all shader fields and gather info, and update the tracking offsets
        ImmutableArray<FieldInfo> fieldInfos = GetCapturedFieldInfos(
            structDeclarationSymbol,
            ImmutableArray<string>.Empty,
            resourceOffsetAsBox,
            rawDataOffsetAsBox).ToImmutableArray();

        resourceCount = resourceOffsetAsBox.Value;

        // After all the captured fields have been processed, ansure the reported byte size for
        // the local variables is padded to a multiple of a 32 bit value. This is necessary to
        // enable loading all the dispatch data after reinterpreting it to a sequence of values
        // of size 32 bits (via SetComputeRoot32BitConstants), without reading out of bounds.
        root32BitConstantCount = AlignmentHelper.Pad(rawDataOffsetAsBox.Value, sizeof(int)) / sizeof(int);

        // A shader root signature has a maximum size of 64 DWORDs, so 256 bytes.
        // Loaded values in the root signature have the following costs:
        //  - Root constants cost 1 DWORD each, since they are 32-bit values.
        //  - Descriptor tables cost 1 DWORD each.
        //  - Root descriptors (64-bit GPU virtual addresses) cost 2 DWORDs each.
        // So here we check whether the current signature respects that constraint,
        // and emit a build error otherwise. For more info on this, see the docs here:
        // https://docs.microsoft.com/windows/win32/direct3d12/root-signature-limits.
        int rootSignatureDwordSize = root32BitConstantCount + resourceCount;

        if (rootSignatureDwordSize > 64)
        {
            builder.Add(ShaderDispatchDataSizeExceeded, structDeclarationSymbol, structDeclarationSymbol);
        }

        diagnostics = builder.ToImmutable();

        return fieldInfos;
    }

    /// <summary>
    /// Gets info on a given captured HLSL primitive type (either a scalar, a vector or a matrix).
    /// </summary>
    /// <param name="fieldPath">The current path of the field with respect to the shader instance.</param>
    /// <param name="typeName">The type name currently being read.</param>
    /// <param name="rawDataOffset">The current offset within the loaded data buffer.</param>
    private static FieldInfo GetHlslKnownTypeFieldInfo(
        ImmutableArray<string> fieldPath,
        string typeName,
        ref int rawDataOffset)
    {
        // For scalar, vector and linear matrix types, serialize the value normally.
        // Only the initial alignment needs to be considered, while data is packed.
        var (fieldSize, fieldPack) = HlslKnownSizes.GetTypeInfo(typeName);

        // Check if the current type is a matrix type with more than one row. In this
        // case, each row is aligned as if it was a separate array, so the start of
        // each row needs to be at a multiple of 16 bytes (a float4 register).
        if (HlslKnownTypes.IsNonLinearMatrixType(typeName, out string? elementName, out int rows, out int columns))
        {
            ImmutableArray<int>.Builder builder = ImmutableArray.CreateBuilder<int>(rows);

            for (int j = 0; j < rows; j++)
            {
                int startingRawDataOffset = AlignmentHelper.Pad(rawDataOffset, 16);

                builder.Add(startingRawDataOffset);

                rawDataOffset = startingRawDataOffset + fieldPack * columns;
            }

            return new FieldInfo.NonLinearMatrix(
                fieldPath,
                typeName,
                elementName!,
                rows,
                columns,
                builder.MoveToImmutable());
        }
        else
        {
            // Calculate the right offset with 16-bytes padding (HLSL constant buffer).
            // Since we're in a constant buffer, we need to both pad the starting offset
            // to be aligned to the packing size of the type, and also to align the initial
            // offset to ensure that values do not cross 16 bytes boundaries either.
            int startingRawDataOffset = AlignmentHelper.AlignToBoundary(
                AlignmentHelper.Pad(rawDataOffset, fieldPack),
                fieldSize,
                16);

            rawDataOffset = startingRawDataOffset + fieldSize;

            return new FieldInfo.Primitive(fieldPath, typeName, startingRawDataOffset);
        }
    }

    /// <summary>
    /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchDataMethod</c> method.
    /// </summary>
    /// <param name="shaderInterfaceType">The type of shader interface urrently being processed.</param>
    /// <param name="fieldInfos">The array of <see cref="FieldInfo"/> values for all captured fields.</param>
    /// <param name="resourceCount">The total number of captured resources in the shader.</param>
    /// <param name="root32BitConstantsCount">The total number of needed 32 bit constants in the shader root signature.</param>
    /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchDataMethod</c> method.</returns>
    private static MethodDeclarationSyntax CreateLoadDispatchDataMethod(
        Type shaderInterfaceType,
        ImmutableArray<FieldInfo> fieldInfos,
        int resourceCount,
        int root32BitConstantsCount)
    {
        // This code produces a method declaration as follows:
        //
        // readonly void global::ComputeSharp.__Internals.IShader.LoadDispatchData<TDataLoader>(ref TDataLoader loader, global::ComputeSharp.GraphicsDevice device, int x, int y, int z)
        // {
        //     <BODY>
        // }
        return
            MethodDeclaration(
                PredefinedType(Token(SyntaxKind.VoidKeyword)),
                Identifier("LoadDispatchData"))
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.__Internals.{nameof(IShader)}")))
            .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
            .AddTypeParameterListParameters(TypeParameter(Identifier("TDataLoader")))
            .AddParameterListParameters(
                Parameter(Identifier("loader"))
                    .AddModifiers(Token(SyntaxKind.RefKeyword))
                    .WithType(IdentifierName("TDataLoader")),
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
