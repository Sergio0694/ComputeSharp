using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGenerators.Models;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>ResourceDescriptorRanges</c> property.
    /// </summary>
    private static partial class ResourceDescriptorRanges
    {
        /// <summary>
        /// Gets the data related to the shader metadata for a given shader type.
        /// </summary>
        /// <param name="isImplicitTextureUsed">Indicates whether the current shader uses an implicit texture.</param>
        /// <param name="resources">The sequence of captured resources for the shader.</param>
        /// <returns>The metadata info for the shader.</returns>
        public static ImmutableArray<ResourceDescriptor> GetInfo(
            bool isImplicitTextureUsed,
            ImmutableArray<ResourceInfo> resources)
        {
            using ImmutableArrayBuilder<ResourceDescriptor> descriptors = new();

            int constantBufferOffset = 1;
            int readOnlyResourceOffset = 0;
            int readWriteResourceOffset = 0;

            // Add the implicit texture descriptor, if needed
            if (isImplicitTextureUsed)
            {
                descriptors.Add(new ResourceDescriptor(1, readWriteResourceOffset++));
            }

            // Populate the sequence of resource descriptors
            foreach (ResourceInfo resource in resources)
            {
                if (HlslKnownTypes.IsConstantBufferType(resource.TypeName))
                {
                    descriptors.Add(new ResourceDescriptor(2, constantBufferOffset++));
                }
                else if (HlslKnownTypes.IsReadOnlyTypedResourceType(resource.TypeName))
                {
                    descriptors.Add(new ResourceDescriptor(0, readOnlyResourceOffset++));
                }
                else
                {
                    descriptors.Add(new ResourceDescriptor(1, readWriteResourceOffset++));
                }
            }

            return descriptors.ToImmutable();
        }
    }
}