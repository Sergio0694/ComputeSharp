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
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class IShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>LoadDispatchData</c> method.
    /// </summary>
    private static partial class LoadDispatchData
    {
        /// <summary>
        /// Explores a given type hierarchy and generates statements to load fields.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The current shader type being explored.</param>
        /// <param name="shaderType">The type of shader currently being processed.</param>
        /// <param name="resourceCount">The total number of captured resources in the shader.</param>
        /// <param name="root32BitConstantCount">The total number of needed 32 bit constants in the shader root signature.</param>
        /// <returns>The sequence of <see cref="FieldInfo"/> instances for all captured resources and values.</returns>
        public static ImmutableArray<FieldInfo> GetInfo(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            ITypeSymbol structDeclarationSymbol,
            ShaderType shaderType,
            out int resourceCount,
            out int root32BitConstantCount)
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
                   where fieldSymbol is { Type: INamedTypeSymbol { IsStatic: false }, IsConst: false, IsStatic: false, IsFixedSizeBuffer: false, IsImplicitlyDeclared: false }
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
                        foreach (FieldInfo fieldInfo in GetCapturedFieldInfos(fieldSymbol.Type, fieldPath.Add(fieldName), resourceOffset, rawDataOffset))
                        {
                            yield return fieldInfo;
                        }
                    }
                }
            }

            // Setup the resource and byte offsets for tracking. Pixel shaders have only two
            // implicitly captured int values, as they're always dispatched over a 2D texture.
            bool isComputeShader = shaderType == ShaderType.ComputeShader;
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

            // After all the captured fields have been processed, ensure the reported byte size for
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
            (int fieldSize, int fieldPack) = HlslKnownSizes.GetTypeInfo(typeName);

            // Check if the current type is a matrix type with more than one row. In this
            // case, each row is aligned as if it was a separate array, so the start of
            // each row needs to be at a multiple of 16 bytes (a float4 register).
            if (HlslKnownTypes.IsNonLinearMatrixType(typeName, out string? elementName, out int rows, out int columns))
            {
                using ImmutableArrayBuilder<int> builder = ImmutableArrayBuilder<int>.Rent();

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