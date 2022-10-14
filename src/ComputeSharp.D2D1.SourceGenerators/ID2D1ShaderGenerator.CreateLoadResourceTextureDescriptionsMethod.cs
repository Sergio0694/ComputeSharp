using System;
using System.Collections.Immutable;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>LoadResourceTextureDescriptions</c> method.
    /// </summary>
    private static partial class LoadResourceTextureDescriptions
    {
        /// <summary>
        /// Extracts the resource texture descriptions for the current shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <param name="resourceTextureDescriptions">The produced resource texture descriptions for the shader.</param>
        public static void GetInfo(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            int inputCount,
            out ImmutableArray<ResourceTextureDescription> resourceTextureDescriptions)
        {
            using ImmutableArrayBuilder<(int? Index, int Rank)> resourceTextureInfos = ImmutableArrayBuilder<(int? Index, int Rank)>.Rent();

            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                // Only look for fields of a named type symbol (diagnostics is emitted by the HLSL rewriter if it's not the case).
                // We're only looking for instance fields of unmanaged types in this case (as resource textures are structs).
                if (fieldSymbol is not { IsStatic: false, Type: INamedTypeSymbol { IsUnmanagedType: true } typeSymbol })
                {
                    continue;
                }

                string metadataName = typeSymbol.GetFullMetadataName();

                // Check that the field is a resource texture type (if not, it will be processed by the HLSL rewriter too)
                if (HlslKnownTypes.IsResourceTextureType(metadataName))
                {
                    // The type name will be ComputeSharp.D2D1.D2D1ResourceTexture1D`1 or the 2D/3D versions
                    int rank = int.Parse(metadataName.Substring(metadataName.Length - 4, 1));
                    int? index = null;

                    // Get the index from the [D2DResourceTextureIndex] attribute over the field
                    if (fieldSymbol.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DResourceTextureIndexAttribute", out AttributeData? attributeData))
                    {
                        // If the constructor argument isn't available, it means the code is invalid. Just do nothing then, as the
                        // user will have to fix that to get the code to compile anyway. This way the generator won't crash too.
                        if (attributeData!.TryGetConstructorArgument(0, out int resourceIndex))
                        {
                            index = resourceIndex;
                        }
                    }
                    else
                    {
                        // If the attribute is missing, emit a diagnostic
                        diagnostics.Add(MissingD2DResourceTextureIndexAttribute, fieldSymbol, fieldSymbol.Name, structDeclarationSymbol);
                    }

                    resourceTextureInfos.Add((index, rank));
                }
            }

            // Extract the resource texture descriptions for the rest of the pipeline
            using (ImmutableArrayBuilder<ResourceTextureDescription> resourceTextureDescriptionsBuilder = ImmutableArrayBuilder<ResourceTextureDescription>.Rent())
            {
                foreach ((int? index, int rank) in resourceTextureInfos.WrittenSpan)
                {
                    resourceTextureDescriptionsBuilder.Add(new ResourceTextureDescription((uint)(index ?? 0), (uint)rank));
                }

                resourceTextureDescriptions = resourceTextureDescriptionsBuilder.ToImmutable();
            }

            // If the input count is invalid, do nothing and avoid emitting potentially incorrect
            // diagnostics based on the resource texture indices with respect to the input count.
            // The GetInputType() generator has already emitted diagnostic for this error, so this
            // generator can just wait for users to go back and fix that issue before proceeding.
            if (inputCount is not (>= 0 and <= 8))
            {
                return;
            }

            // Validate that the resource texture indices don't overlap with the shader inputs
            foreach ((int? index, _) in resourceTextureInfos.WrittenSpan)
            {
                if (index < inputCount)
                {
                    diagnostics.Add(ResourceTextureIndexOverlappingWithInputIndex, structDeclarationSymbol, structDeclarationSymbol);

                    return;
                }
            }

            // Validate that no resource texture has an index greater than or equal to 16
            foreach ((int? index, _) in resourceTextureInfos.WrittenSpan)
            {
                if (index >= 16)
                {
                    diagnostics.Add(OutOfRangeResourceTextureIndex, structDeclarationSymbol, structDeclarationSymbol);

                    return;
                }
            }

            Span<bool> selectedResourceTextureIndices = stackalloc bool[16];

            selectedResourceTextureIndices.Clear();

            // All input description indices must be unique (also take this path for invalid indices)
            foreach ((int? index, _) in resourceTextureInfos.WrittenSpan)
            {
                ref bool isResourceTextureIndexUsed = ref selectedResourceTextureIndices[index ?? 0];

                if (isResourceTextureIndexUsed)
                {
                    diagnostics.Add(RepeatedD2DResourceTextureIndices, structDeclarationSymbol, structDeclarationSymbol);

                    return;
                }

                isResourceTextureIndexUsed = true;
            }
        }
    }
}