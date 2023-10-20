using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Helpers;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>ConstantBuffer</c> methods and additional types.
    /// </summary>
    private static partial class ConstantBuffer
    {
        /// <summary>
        /// Explores a given type hierarchy and generates statements to load fields.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The current shader type being explored.</param>
        /// <param name="constantBufferSizeInBytes">The size of the shader constant buffer.</param>
        /// <returns>The sequence of <see cref="FieldInfo"/> instances for all captured resources and values.</returns>
        public static ImmutableArray<FieldInfo> GetInfo(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            Compilation compilation,
            ITypeSymbol structDeclarationSymbol,
            out int constantBufferSizeInBytes)
        {
            // Helper method that uses boxes instead of ref-s (illegal in enumerators)
            static IEnumerable<FieldInfo> GetCapturedFieldInfos(
                Compilation compilation,
                ITypeSymbol currentTypeSymbol,
                ImmutableArray<FieldPathPart> fieldPath,
                StrongBox<int> rawDataOffset)
            {
                bool isFirstField = true;

                foreach (
                   IFieldSymbol fieldSymbol in
                   from fieldSymbol in currentTypeSymbol.GetMembers().OfType<IFieldSymbol>()
                   where fieldSymbol is { Type: INamedTypeSymbol { IsStatic: false }, IsConst: false, IsStatic: false, IsFixedSizeBuffer: false, IsImplicitlyDeclared: false }
                   select fieldSymbol)
                {
                    // Skip fields of not accessible types (the analyzer will handle this)
                    if (!fieldSymbol.Type.IsAccessibleFromCompilationAssembly(compilation))
                    {
                        continue;
                    }

                    string fieldName = fieldSymbol.Name;
                    string typeName = fieldSymbol.Type.GetFullyQualifiedMetadataName();

                    // The first item in each nested struct needs to be aligned to 16 bytes
                    if (isFirstField && fieldPath.Length > 0)
                    {
                        rawDataOffset.Value = AlignmentHelper.Pad(rawDataOffset.Value, 16);

                        isFirstField = false;
                    }

                    if (HlslKnownTypes.IsKnownHlslType(typeName))
                    {
                        ImmutableArray<FieldPathPart> nestedFieldPath = fieldPath.Add(new FieldPathPart.Leaf(fieldName));

                        yield return GetHlslKnownTypeFieldInfo(nestedFieldPath, typeName, ref rawDataOffset.Value);
                    }
                    else if (fieldSymbol.Type.IsUnmanagedType)
                    {
                        string nestedTypeName = fieldSymbol.Type.GetFullyQualifiedName(includeGlobal: true);
                        ImmutableArray<FieldPathPart> nestedFieldPath = fieldPath.Add(new FieldPathPart.Nested(fieldName, nestedTypeName));

                        // Custom struct type defined by the user
                        foreach (FieldInfo fieldInfo in GetCapturedFieldInfos(compilation, fieldSymbol.Type, nestedFieldPath, rawDataOffset))
                        {
                            yield return fieldInfo;
                        }
                    }
                }
            }

            // Setup the resource and byte offsets for tracking
            StrongBox<int> rawDataOffsetAsBox = new();

            // Traverse all shader fields and gather info, and update the tracking offsets
            ImmutableArray<FieldInfo> fieldInfos = GetCapturedFieldInfos(
                compilation,
                structDeclarationSymbol,
                ImmutableArray<FieldPathPart>.Empty,
                rawDataOffsetAsBox).ToImmutableArray();

            constantBufferSizeInBytes = rawDataOffsetAsBox.Value;

            // The maximum size for a constant buffer is 64KB
            const int D3D11_REQ_CONSTANT_BUFFER_ELEMENT_COUNT = 4096;
            const int MaximumConstantBufferSize = D3D11_REQ_CONSTANT_BUFFER_ELEMENT_COUNT * 4 * sizeof(float);

            // Emit a diagnostic if the shader constant buffer is too large
            if (constantBufferSizeInBytes > MaximumConstantBufferSize)
            {
                diagnostics.Add(ShaderDispatchDataSizeExceeded, structDeclarationSymbol, structDeclarationSymbol);
            }

            return fieldInfos;
        }

        /// <summary>
        /// Gets info on a given captured HLSL primitive type (either a scalar, a vector or a matrix).
        /// </summary>
        /// <param name="fieldPath">The current path of the field with respect to the shader instance.</param>
        /// <param name="typeName">The type name currently being read.</param>
        /// <param name="rawDataOffset">The current offset within the loaded data buffer.</param>
        private static FieldInfo GetHlslKnownTypeFieldInfo(
            ImmutableArray<FieldPathPart> fieldPath,
            string typeName,
            ref int rawDataOffset)
        {
            // For scalar, vector and linear matrix types, serialize the value normally.
            // Only the initial alignment needs to be considered, while data is packed.
            (int fieldSize, int fieldPack) = HlslKnownSizes.GetTypeInfo(typeName);

            // Check if the current type is a matrix type with more than one row. In this
            // case, each row is aligned as if it was a separate array, so the start of
            // each row needs to be at a multiple of 16 bytes (a float4 register).
            if (HlslKnownTypes.IsNonLinearMatrixType(typeName, out string? elementName, out int rows, out int columns))
            {
                using ImmutableArrayBuilder<int> builder = new();

                for (int j = 0; j < rows; j++)
                {
                    int startingRawDataOffset = AlignmentHelper.Pad(rawDataOffset, 16);

                    builder.Add(startingRawDataOffset);

                    rawDataOffset = startingRawDataOffset + (fieldPack * columns);
                }

                return new FieldInfo.NonLinearMatrix(
                    fieldPath,
                    typeName,
                    elementName!,
                    rows,
                    columns,
                    builder.ToImmutable());
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
    }
}