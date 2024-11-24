using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>InputTypes</c> properties.
    /// </summary>
    internal static partial class InputTypes
    {
        /// <summary>
        /// Extracts the input info for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="inputCount">The number of shader inputs to declare.</param>
        /// <param name="inputSimpleIndices">The indicess of the simple shader inputs.</param>
        /// <param name="inputComplexIndices">The indices of the complex shader inputs.</param>
        /// <param name="combinedInputTypes">The combined and serialized input types for each available input.</param>
        public static void GetInfo(
            INamedTypeSymbol structDeclarationSymbol,
            out int inputCount,
            out ImmutableArray<int> inputSimpleIndices,
            out ImmutableArray<int> inputComplexIndices,
            out ImmutableArray<uint> combinedInputTypes)
        {
            // Note: this logic is shared with InvalidD2DResourceTextureIndexAttributeUseAnalyzer.
            // If any changes are made here, they should be reflected in that analyzer as well.
            inputCount = 0;

            using ImmutableArrayBuilder<int> inputSimpleIndicesBuilder = new();
            using ImmutableArrayBuilder<int> inputComplexIndicesBuilder = new();

            foreach (AttributeData attributeData in structDeclarationSymbol.GetAttributes())
            {
                using ImmutableArrayBuilder<char> builder = new();

                attributeData.AttributeClass?.AppendFullyQualifiedMetadataName(in builder);

                switch (builder.WrittenSpan)
                {
                    case "ComputeSharp.D2D1.D2DInputCountAttribute":

                        // We ignore negative values here to avoid the rest of the generator crashing due
                        // to out of bound exceptions or other issues. The analyzer will detect this and
                        // emit a diagnostic if that's the case, so it is safe to ignore that from here.
                        inputCount = Math.Max((int)attributeData.ConstructorArguments[0].Value!, 0);
                        break;
                    case "ComputeSharp.D2D1.D2DInputSimpleAttribute":
                        inputSimpleIndicesBuilder.Add((int)attributeData.ConstructorArguments[0].Value!);
                        break;
                    case "ComputeSharp.D2D1.D2DInputComplexAttribute":
                        inputComplexIndicesBuilder.Add((int)attributeData.ConstructorArguments[0].Value!);
                        break;
                    default:
                        break;
                }
            }

            inputSimpleIndices = inputSimpleIndicesBuilder.ToImmutable();
            inputComplexIndices = inputComplexIndicesBuilder.ToImmutable();

            uint[] inputTypes = new uint[inputCount];

            // The D2D_INPUT<N>_SIMPLE define is optional, and if omitted, the corresponding input
            // will automatically be of complex type. For the same reason, D2D_INPUT<N>_COMPLEX is
            // also essentially redundant. So we can do the following to compute the input types:
            //   - Create an array with as many inputs as the declared number of inputs
            //   - Fill it with 1 (ie. D2D1PixelShaderInputType.Complex), as that's the default
            //   - Traverse the declared simple inputs once, and update their corresponding values
            inputTypes.AsSpan().Fill(1);

            foreach (int index in inputSimpleIndicesBuilder.WrittenSpan)
            {
                // Simply ignore any out of range indices here, to avoid throwing an exception.
                // The analyzer will detect all of these cases and correctly issue a diagnostic.
                if ((uint)index < inputCount)
                {
                    inputTypes[index] = 0;
                }
            }

            // We own the array, so we can just reinterpret it here
            combinedInputTypes = Unsafe.As<uint[], ImmutableArray<uint>>(ref inputTypes);
        }
    }
}