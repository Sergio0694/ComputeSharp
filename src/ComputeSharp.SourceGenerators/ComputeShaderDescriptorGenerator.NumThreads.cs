using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>[numthreads]</c> properties.
    /// </summary>
    private static partial class NumThreads
    {
        /// <summary>
        /// Gets the thread ids values for a given shader type, if available.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="threadsX">The thread ids value for the X axis.</param>
        /// <param name="threadsY">The thread ids value for the Y axis.</param>
        /// <param name="threadsZ">The thread ids value for the Z axis.</param>
        /// <param name="isCompilationEnabled">Whether compilation should be attempted for the current info.</param>
        public static void GetInfo(
            INamedTypeSymbol structDeclarationSymbol,
            out int threadsX,
            out int threadsY,
            out int threadsZ,
            out bool isCompilationEnabled)
        {
            // Try to get the attribute that controls shader precompilation (this is always required)
            if (!structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.ThreadGroupSizeAttribute", out AttributeData? attribute))
            {
                threadsX = threadsY = threadsZ = 0;
                isCompilationEnabled = false;

                return;
            }

            // Check for a dispatch axis argument first
            if (attribute.ConstructorArguments is [{ Value: var defaultSize }])
            {
                (threadsX, threadsY, threadsZ) = (DefaultThreadGroupSizes?)(defaultSize as int?) switch
                {
                    DefaultThreadGroupSizes.X => (64, 1, 1),
                    DefaultThreadGroupSizes.Y => (1, 64, 1),
                    DefaultThreadGroupSizes.Z => (1, 1, 64),
                    DefaultThreadGroupSizes.XY => (8, 8, 1),
                    DefaultThreadGroupSizes.XZ => (8, 1, 8),
                    DefaultThreadGroupSizes.YZ => (1, 8, 8),
                    DefaultThreadGroupSizes.XYZ => (4, 4, 4),
                    _ => (0, 0, 0)
                };

                // Only enable compilation if we have valid thread group size values
                isCompilationEnabled = (threadsX, threadsY, threadsZ) is not (0, 0, 0);
            }
            else if (attribute.ConstructorArguments is not [{ Value: int explicitThreadsX }, { Value: int explicitThreadsY }, { Value: int explicitThreadsZ }] ||
                     explicitThreadsX is < 1 or > 1024 ||
                     explicitThreadsY is < 1 or > 1024 ||
                     explicitThreadsZ is < 1 or > 64)
            {
                // Also disable compilation if we have no valid explicit sizes
                threadsX = threadsY = threadsZ = 0;
                isCompilationEnabled = false;

                return;
            }
            else
            {
                // Enable compilation and track the explicit thread group sizes
                threadsX = explicitThreadsX;
                threadsY = explicitThreadsY;
                threadsZ = explicitThreadsZ;
                isCompilationEnabled = true;
            }
        }
    }
}