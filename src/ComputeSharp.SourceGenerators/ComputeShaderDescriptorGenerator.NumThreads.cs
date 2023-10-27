using System;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

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
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="threadsX">The thread ids value for the X axis.</param>
        /// <param name="threadsY">The thread ids value for the Y axis.</param>
        /// <param name="threadsZ">The thread ids value for the Z axis.</param>
        /// <param name="isCompilationEnabled">Whether compilation should be attempted for the current info.</param>
        public static void GetInfo(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            out int threadsX,
            out int threadsY,
            out int threadsZ,
            out bool isCompilationEnabled)
        {
            // Try to get the attribute that controls shader precompilation (this is always required)
            if (!structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName(typeof(ThreadGroupSizeAttribute).FullName, out AttributeData? attribute))
            {
                diagnostics.Add(MissingThreadGroupSizeAttribute, structDeclarationSymbol, structDeclarationSymbol);

                threadsX = threadsY = threadsZ = 0;
                isCompilationEnabled = false;

                return;
            }

            // Check for a dispatch axis argument first
            if (attribute.ConstructorArguments.Length == 1)
            {
                int dispatchAxis = (int)attribute.ConstructorArguments[0].Value!;

                (threadsX, threadsY, threadsZ) = (DispatchAxis)dispatchAxis switch
                {
                    DispatchAxis.X => (64, 1, 1),
                    DispatchAxis.Y => (1, 64, 1),
                    DispatchAxis.Z => (1, 1, 64),
                    DispatchAxis.XY => (8, 8, 1),
                    DispatchAxis.XZ => (8, 1, 8),
                    DispatchAxis.YZ => (1, 8, 8),
                    DispatchAxis.XYZ => (4, 4, 4),
                    _ => (0, 0, 0)
                };

                // Validate the dispatch axis argument
                if ((threadsX, threadsY, threadsZ) is (0, 0, 0))
                {
                    diagnostics.Add(InvalidThreadGroupSizeAttributeDefaultThreadGroupSize, structDeclarationSymbol, structDeclarationSymbol);

                    threadsX = threadsY = threadsZ = 0;
                    isCompilationEnabled = false;

                    return;
                }

                isCompilationEnabled = true;
            }
            else if (
                attribute.ConstructorArguments.Length != 3 ||
                attribute.ConstructorArguments[0].Value is not int explicitThreadsX ||
                attribute.ConstructorArguments[1].Value is not int explicitThreadsY ||
                attribute.ConstructorArguments[2].Value is not int explicitThreadsZ ||
                explicitThreadsX is < 1 or > 1024 ||
                explicitThreadsY is < 1 or > 1024 ||
                explicitThreadsZ is < 1 or > 64)
            {
                // Failed to validate the thread number arguments
                diagnostics.Add(InvalidThreadGroupSizeAttributeValues, structDeclarationSymbol, structDeclarationSymbol);

                threadsX = threadsY = threadsZ = 0;
                isCompilationEnabled = false;

                return;
            }
            else
            {
                threadsX = explicitThreadsX;
                threadsY = explicitThreadsY;
                threadsZ = explicitThreadsZ;
                isCompilationEnabled = true;
            }
        }
    }
}