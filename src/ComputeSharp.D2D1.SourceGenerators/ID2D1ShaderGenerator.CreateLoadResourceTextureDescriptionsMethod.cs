using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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
    /// A helper with all logic to generate the <c>LoadResourceTextureDescriptions</c> method.
    /// </summary>
    private static partial class LoadResourceTextureDescriptions
    {
        /// <summary>
        /// Extracts the resource texture descriptions for the current shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <param name="resourceTextureDescriptions">The produced resource texture descriptions for the shader.</param>
        public static void GetInfo(
            ImmutableArray<Diagnostic>.Builder diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            int inputCount,
            out ImmutableArray<ResourceTextureDescription> resourceTextureDescriptions)
        {
            ImmutableArray<(string Name, uint Index, uint Rank)>.Builder resourceTextureInfos = ImmutableArray.CreateBuilder<(string, uint, uint)>();

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
                    // The type name will be either Texture1D, Texture2D or Texture3D
                    string typeName = HlslKnownTypes.GetMappedName(typeSymbol);
                    uint rank = uint.Parse(typeName.Substring(typeName.Length - 2, 1));
                    uint index = uint.MaxValue;

                    // Get the index from the [D2DResourceTextureIndex] attribute over the field
                    if (fieldSymbol.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DResourceTextureIndexAttribute", out AttributeData? attributeData))
                    {
                        // If the constructor argument isn't available, it means the code is invalid. Just do nothing then, as the
                        // user will have to fix that to get the code to compile anyway. This way the generator won't crash too.
                        _ = attributeData!.TryGetConstructorArgument(0, out index);
                    }
                    else
                    {
                        // If the attribute is missing, emit a diagnostic
                        diagnostics.Add(MissingD2DResourceTextureIndexAttribute, fieldSymbol, fieldSymbol.Name, structDeclarationSymbol);
                    }

                    _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                    resourceTextureInfos.Add((mapping ?? fieldSymbol.Name, index, rank));
                }
            }

            // Extract the resource texture descriptions for the rest of the pipeline
            resourceTextureDescriptions = resourceTextureInfos.Select(static info => new ResourceTextureDescription(info.Index, info.Rank)).ToImmutableArray();

            // If the input count is invalid, do nothing and avoid emitting potentially incorrect
            // diagnostics based on the resource texture indices with respect to the input count.
            // The GetInputType() generator has already emitted diagnostic for this error, so this
            // generator can just wait for users to go back and fix that issue before proceeding.
            if (inputCount is not (>= 0 and <= 8))
            {
                return;
            }

            // Validate that the resource texture indices don't overlap with the shader inputs
            if (resourceTextureInfos.Any(info => info.Index < inputCount))
            {
                diagnostics.Add(ResourceTextureIndexOverlappingWithInputIndex, structDeclarationSymbol, structDeclarationSymbol);

                return;
            }

            // Validate that no resource texture has an index greater than or equal to 16
            if (resourceTextureInfos.Any(info => info.Index >= 16))
            {
                diagnostics.Add(OutOfRangeResourceTextureIndex, structDeclarationSymbol, structDeclarationSymbol);

                return;
            }

            HashSet<uint> resourceTextureIndices = new(resourceTextureInfos.Select(info => info.Index));

            // All input description indices must be unique
            if (resourceTextureIndices.Count != resourceTextureInfos.Count)
            {
                diagnostics.Add(RepeatedD2DResourceTextureIndices, structDeclarationSymbol, structDeclarationSymbol);
            }
        }

        /// <summary>
        /// Gets the diagnostics for a field with invalid <c>[D2DResourceTextureIndex]</c> attribute use, if that is the case.
        /// </summary>
        /// <param name="fieldSymbol">The input <see cref="IFieldSymbol"/> instance to process.</param>
        /// <returns>The resulting <see cref="Diagnostic"/> instance for <paramref name="fieldSymbol"/>, if needed.</returns>
        public static Diagnostic? GetDiagnosticForFieldWithInvalidD2DResourceTextureIndexAttribute(IFieldSymbol fieldSymbol)
        {
            if (fieldSymbol.HasAttributeWithFullyQualifiedName("ComputeSharp.D2D1.D2DResourceTextureIndexAttribute"))
            {
                string metadataName = fieldSymbol.Type.GetFullMetadataName();

                if (!HlslKnownTypes.IsResourceTextureType(metadataName))
                {
                    return Diagnostic.Create(
                        InvalidD2DResourceTextureIndexAttributeUse,
                        fieldSymbol.Locations.FirstOrDefault(),
                        fieldSymbol.Name,
                        fieldSymbol.ContainingType,
                        fieldSymbol.Type);
                }
            }

            return null;
        }
    }
}
