using System.Collections.Immutable;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>ResourceTextureDescriptions</c> property.
    /// </summary>
    private static partial class ResourceTextureDescriptions
    {
        /// <summary>
        /// Extracts the resource texture descriptions for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="resourceTextureDescriptions">The produced resource texture descriptions for the shader.</param>
        public static void GetInfo(
            INamedTypeSymbol structDeclarationSymbol,
            out ImmutableArray<ResourceTextureDescription> resourceTextureDescriptions)
        {
            using ImmutableArrayBuilder<ResourceTextureDescription> builder = new();

            foreach (ISymbol memberSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                // Only look for fields of a named type symbol (diagnostics is emitted by the HLSL rewriter if it's not the case).
                // We're only looking for instance fields of unmanaged types in this case (as resource textures are structs).
                if (memberSymbol is not IFieldSymbol { IsStatic: false, Type: INamedTypeSymbol { IsUnmanagedType: true, IsGenericType: true } typeSymbol } fieldSymbol)
                {
                    continue;
                }

                string metadataName = typeSymbol.GetFullyQualifiedMetadataName();

                // Check that the field is a resource texture type (if not, it will be processed by the HLSL rewriter too)
                if (HlslKnownTypes.IsResourceTextureType(metadataName))
                {
                    // The type name will be ComputeSharp.D2D1.D2D1ResourceTexture1D`1 or the 2D/3D versions.
                    // To avoid the intermediate string and the parsing, we can rely on the fact the metadata
                    // has already been validated here, so we can get the rank by offsetting the characters.
                    int rank = metadataName[^4] - '0';
                    int? index = null;

                    // Get the index from the [D2DResourceTextureIndex] attribute over the field
                    if (fieldSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DResourceTextureIndexAttribute", out AttributeData? attributeData))
                    {
                        // If the constructor argument isn't available, it means the code is invalid. Just do nothing then, as the
                        // user will have to fix that to get the code to compile anyway. This way the generator won't crash too.
                        if (attributeData.TryGetConstructorArgument(0, out int resourceIndex))
                        {
                            index = resourceIndex;
                        }
                    }

                    builder.Add(new ResourceTextureDescription(index ?? 0, rank));
                }
            }

            resourceTextureDescriptions = builder.ToImmutable();
        }
    }
}