using System;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>GetInputType</c> method.
    /// </summary>
    private static partial class GetInputType
    {
        /// <summary>
        /// Extracts the input info for the current shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="inputCount">The number of shader inputs to declare.</param>
        /// <param name="inputSimpleIndices">The indicess of the simple shader inputs.</param>
        /// <param name="inputComplexIndices">The indices of the complex shader inputs.</param>
        /// <param name="combinedInputTypes">The combined and serialized input types for each available input.</param>
        public static void GetInfo(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            out int inputCount,
            out ImmutableArray<int> inputSimpleIndices,
            out ImmutableArray<int> inputComplexIndices,
            out ImmutableArray<uint> combinedInputTypes)
        {
            // We need a separate local here so we can keep the original value to apply
            // diagnostics to, but without returning invalid values to the caller which
            // might cause generator errors (eg. -1 would cause other code to just throw).
            int rawInputCount = 0;

            inputCount = 0;

            using ImmutableArrayBuilder<int> inputSimpleIndicesBuilder = ImmutableArrayBuilder<int>.Rent();
            using ImmutableArrayBuilder<int> inputComplexIndicesBuilder = ImmutableArrayBuilder<int>.Rent();

            foreach (AttributeData attributeData in structDeclarationSymbol.GetAttributes())
            {
                switch (attributeData.AttributeClass?.GetFullMetadataName())
                {
                    case "ComputeSharp.D2D1.D2DInputCountAttribute":
                        rawInputCount = (int)attributeData.ConstructorArguments[0].Value!;
                        inputCount = Math.Max(rawInputCount, 0);
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
            combinedInputTypes = ImmutableArray<uint>.Empty;

            // Validate the input count
            if (rawInputCount is not (>= 0 and <= 8))
            {
                diagnostics.Add(InvalidD2DInputCount, structDeclarationSymbol, structDeclarationSymbol);

                return;
            }

            // All simple indices must be in range
            foreach (int index in inputSimpleIndicesBuilder.WrittenSpan)
            {
                if ((uint)index >= rawInputCount)
                {
                    diagnostics.Add(OutOfRangeInputIndex, structDeclarationSymbol, structDeclarationSymbol);

                    return;
                }
            }

            // All complex indices must be in range as well
            foreach (int index in inputComplexIndicesBuilder.WrittenSpan)
            {
                if ((uint)index >= rawInputCount)
                {
                    diagnostics.Add(OutOfRangeInputIndex, structDeclarationSymbol, structDeclarationSymbol);

                    return;
                }
            }

            Span<bool> selectedSimpleInputIndices = stackalloc bool[8];
            Span<bool> selectedComplexInputIndices = stackalloc bool[8];

            selectedSimpleInputIndices.Clear();
            selectedComplexInputIndices.Clear();

            // All simple indices must be unique
            foreach (int index in inputSimpleIndicesBuilder.WrittenSpan)
            {
                ref bool isInputIndexUsed = ref selectedSimpleInputIndices[index];

                if (isInputIndexUsed)
                {
                    diagnostics.Add(RepeatedD2DInputSimpleIndices, structDeclarationSymbol, structDeclarationSymbol);

                    return;
                }

                isInputIndexUsed = true;
            }

            // All complex indices must be unique as well
            foreach (int index in inputComplexIndicesBuilder.WrittenSpan)
            {
                ref bool isInputIndexUsed = ref selectedComplexInputIndices[index];

                if (isInputIndexUsed)
                {
                    diagnostics.Add(RepeatedD2DInputComplexIndices, structDeclarationSymbol, structDeclarationSymbol);

                    return;
                }

                isInputIndexUsed = true;
            }

            // Simple and complex indices can't have indices in common
            for (int i = 0; i < rawInputCount; i++)
            {
                if (selectedSimpleInputIndices[i] && selectedComplexInputIndices[i])
                {
                    diagnostics.Add(InvalidSimpleAndComplexIndicesCombination, structDeclarationSymbol, structDeclarationSymbol);

                    return;
                }
            }

            // Build the combined input types list now that inputs have been validated
            using ImmutableArrayBuilder<uint> combinedBuilder = ImmutableArrayBuilder<uint>.Rent();

            for (int i = 0; i < inputCount; i++)
            {
                // The D2D_INPUT<N>_SIMPLE define is optional, and if omitted, the corresponding input
                // will automatically be of complex type. So we just need to check whether the i-th
                // input is present in the set of explicitly simple inputs. These values will map to
                // values from D2D1PixelShaderInputType, which has Simple and Complex as members.
                // So, simple inputs map to 0, and complex inputs map to 1.
                combinedBuilder.Add(selectedSimpleInputIndices[i] ? 0u : 1u);
            }

            combinedInputTypes = combinedBuilder.ToImmutable();
        }
    }
}
