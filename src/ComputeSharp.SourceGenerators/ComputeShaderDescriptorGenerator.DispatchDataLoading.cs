using System;
using System.Collections.Immutable;
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
partial class ComputeShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the dispatch data loading methods.
    /// </summary>
    private static partial class DispatchDataLoading
    {
        /// <summary>
        /// Explores a given type hierarchy and generates statements to load fields.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The current shader type being explored.</param>
        /// <param name="isPixelShaderLike">Whether the compute shader is "pixel shader like", ie. outputting a pixel into a target texture.</param>
        /// <param name="constantBufferSizeInBytes">The size of the shader constant buffer.</param>
        /// <param name="resourceCount">The total number of captured resources in the shader.</param>
        /// <param name="fields">The sequence of <see cref="FieldInfo"/> instances for all captured values.</param>
        /// <param name="resources">The sequence of <see cref="ResourceInfo"/> instances for all captured resources.</param>
        public static void GetInfo(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            Compilation compilation,
            ITypeSymbol structDeclarationSymbol,
            bool isPixelShaderLike,
            out int constantBufferSizeInBytes,
            out int resourceCount,
            out ImmutableArray<FieldInfo> fields,
            out ImmutableArray<ResourceInfo> resources)
        {
            static void GetInfo(
                Compilation compilation,
                ITypeSymbol currentTypeSymbol,
                ImmutableArray<FieldPathPart> fieldPath,
                ref int resourceOffset,
                ref int rawDataOffset,
                ImmutableArrayBuilder<FieldInfo> fields,
                ImmutableArrayBuilder<ResourceInfo> resources)
            {
                bool isFirstField = true;

                foreach (ISymbol memberSymbol in currentTypeSymbol.GetMembers())
                {
                    // Only process fields
                    if (memberSymbol is not IFieldSymbol { Type: INamedTypeSymbol { IsStatic: false }, IsConst: false, IsStatic: false, IsFixedSizeBuffer: false, IsImplicitlyDeclared: false } fieldSymbol)
                    {
                        continue;
                    }

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
                        rawDataOffset = AlignmentHelper.Pad(rawDataOffset, 16);

                        isFirstField = false;
                    }

                    if (HlslKnownTypes.IsTypedResourceType(typeName))
                    {
                        resources.Add(new ResourceInfo(fieldName, typeName, resourceOffset++));
                    }
                    else if (HlslKnownTypes.IsKnownHlslType(typeName))
                    {
                        // TODO: share the logic and fix the field path parts
                        fields.Add(GetHlslKnownTypeFieldInfo(fieldPath.Add(new FieldPathPart.Leaf(fieldName)), typeName, ref rawDataOffset));
                    }
                    else if (fieldSymbol.Type.IsUnmanagedType)
                    {
                        // Custom struct type defined by the user
                        GetInfo(compilation, fieldSymbol.Type, fieldPath.Add(new FieldPathPart.Leaf(fieldName)), ref resourceOffset, ref rawDataOffset, fields, resources);
                    }
                }
            }

            // Setup the resource and byte offsets for tracking. Pixel shaders have only two
            // implicitly captured int values, as they're always dispatched over a 2D texture.
            int rawDataOffset = sizeof(int) * (isPixelShaderLike ? 2 : 3);
            int resourceOffset = 0;

            using (ImmutableArrayBuilder<FieldInfo> fieldBuilder = new())
            using (ImmutableArrayBuilder<ResourceInfo> resourceBuilder = new())
            {
                // Traverse all shader fields and gather info, and update the tracking offsets
                GetInfo(
                    compilation,
                    structDeclarationSymbol,
                    ImmutableArray<FieldPathPart>.Empty,
                    ref resourceOffset,
                    ref rawDataOffset,
                    fieldBuilder,
                    resourceBuilder);

                resourceCount = resourceOffset;
                fields = fieldBuilder.ToImmutable();
                resources = resourceBuilder.ToImmutable();
            }

            // After all the captured fields have been processed, ensure the reported byte size for
            // the local variables is padded to a multiple of a 32 bit value. This is necessary to
            // enable loading all the dispatch data after reinterpreting it to a sequence of values
            // of size 32 bits (via SetComputeRoot32BitConstants), without reading out of bounds.
            constantBufferSizeInBytes = AlignmentHelper.Pad(rawDataOffset, sizeof(int));

            // A shader root signature has a maximum size of 64 DWORDs, so 256 bytes.
            // Loaded values in the root signature have the following costs:
            //  - Root constants cost 1 DWORD each, since they are 32-bit values.
            //  - Descriptor tables cost 1 DWORD each.
            //  - Root descriptors (64-bit GPU virtual addresses) cost 2 DWORDs each.
            // So here we check whether the current signature respects that constraint,
            // and emit a build error otherwise. For more info on this, see the docs here:
            // https://docs.microsoft.com/windows/win32/direct3d12/root-signature-limits.
            int root32BitConstantCount = (constantBufferSizeInBytes / sizeof(int)) + resourceCount;

            if (root32BitConstantCount > 64)
            {
                diagnostics.Add(ShaderDispatchDataSizeExceeded, structDeclarationSymbol, structDeclarationSymbol);
            }
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