using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Helpers;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis;
using static ComputeSharp.D2D1.SourceGenerators.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>LoadDispatchData</c> method.
    /// </summary>
    private static partial class LoadDispatchData
    {
        /// <summary>
        /// Explores a given type hierarchy and generates statements to load fields.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="structDeclarationSymbol">The current shader type being explored.</param>
        /// <param name="constantBufferSizeInBytes">The size of the shader constant buffer.</param>
        /// <returns>The sequence of <see cref="FieldInfo"/> instances for all captured resources and values.</returns>
        public static ImmutableArray<FieldInfo> GetInfo(
            ImmutableArray<Diagnostic>.Builder diagnostics,
            ITypeSymbol structDeclarationSymbol,
            out int constantBufferSizeInBytes)
        {
            // Helper method that uses boxes instead of ref-s (illegal in enumerators)
            static IEnumerable<FieldInfo> GetCapturedFieldInfos(
                ITypeSymbol currentTypeSymbol,
                ImmutableArray<string> fieldPath,
                StrongBox<int> rawDataOffset)
            {
                bool isFirstField = true;

                foreach (
                   IFieldSymbol fieldSymbol in
                   from fieldSymbol in currentTypeSymbol.GetMembers().OfType<IFieldSymbol>()
                   where fieldSymbol is { Type: INamedTypeSymbol { IsStatic: false }, IsConst: false, IsStatic: false, IsFixedSizeBuffer: false, IsImplicitlyDeclared: false }
                   select fieldSymbol)
                {
                    string fieldName = fieldSymbol.Name;
                    string typeName = fieldSymbol.Type.GetFullMetadataName();

                    // Disambiguates the name of target fields against the current input parameters
                    if (fieldName is "loader")
                    {
                        fieldName = $"this.{fieldName}";
                    }

                    // The first item in each nested struct needs to be aligned to 16 bytes
                    if (isFirstField && fieldPath.Length > 0)
                    {
                        rawDataOffset.Value = AlignmentHelper.Pad(rawDataOffset.Value, 16);

                        isFirstField = false;
                    }

                    if (HlslKnownTypes.IsKnownHlslType(typeName))
                    {
                        yield return GetHlslKnownTypeFieldInfo(fieldPath.Add(fieldName), typeName, ref rawDataOffset.Value);
                    }
                    else if (fieldSymbol.Type.IsUnmanagedType)
                    {
                        // Custom struct type defined by the user
                        foreach (var fieldInfo in GetCapturedFieldInfos(fieldSymbol.Type, fieldPath.Add(fieldName), rawDataOffset))
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
                structDeclarationSymbol,
                ImmutableArray<string>.Empty,
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
    }
}
