using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using static ComputeSharp.D2D1.SourceGenerators.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>LoadInputDescriptions</c> method.
    /// </summary>
    private static partial class LoadInputDescriptions
    {
        /// <summary>
        /// Extracts the input descriptions for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="inputDescriptions">The produced input descriptions for the shader.</param>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        public static void GetInfo(
            INamedTypeSymbol structDeclarationSymbol,
            out ImmutableArray<InputDescription> inputDescriptions,
            out ImmutableArray<Diagnostic> diagnostics)
        {
            int inputCount = 0;
            ImmutableArray<InputDescription>.Builder inputDescriptionsBuilder = ImmutableArray.CreateBuilder<InputDescription>();
            ImmutableArray<Diagnostic>.Builder diagnosticsBuilder = ImmutableArray.CreateBuilder<Diagnostic>();       

            foreach (AttributeData attributeData in structDeclarationSymbol.GetAttributes())
            {
                switch (attributeData.AttributeClass?.GetFullMetadataName())
                {
                    case "ComputeSharp.D2D1.D2DInputCountAttribute":
                        inputCount = (int)attributeData.ConstructorArguments[0].Value!;
                        break;
                    case "ComputeSharp.D2D1.D2DInputDescriptionAttribute":
                        if (attributeData.ConstructorArguments.Length == 2)
                        {
                            int index = (int)attributeData.ConstructorArguments[0].Value!;
                            D2D1Filter filter = (D2D1Filter)attributeData.ConstructorArguments[1].Value!;

                            _ = attributeData.TryGetNamedArgument("LevelOfDetailCount", out int levelOfDetailCount);

                            inputDescriptionsBuilder.Add(new InputDescription((uint)index, filter, levelOfDetailCount));
                        }
                        break;
                    default:
                        break;
                }
            }

            inputDescriptions = ImmutableArray<InputDescription>.Empty;

            // Validate the input count (ignore if invalid, this will be validated by the HLSL source generator)
            if (inputCount is not (>= 0 and <= 8))
            {
                goto End;
            }

            // All simple indices must be in range
            if (inputDescriptionsBuilder.Any(description => description.Index >= inputCount))
            {
                diagnosticsBuilder.Add(OutOfRangeInputDescriptionIndex, structDeclarationSymbol, structDeclarationSymbol);

                goto End;
            }

            HashSet<uint> inputDescriptionIndices = new(inputDescriptionsBuilder.Select(description => description.Index));

            // All input description indices must be unique
            if (inputDescriptionIndices.Count != inputDescriptionsBuilder.Count)
            {
                diagnosticsBuilder.Add(RepeatedD2DInputDescriptionIndices, structDeclarationSymbol, structDeclarationSymbol);

                goto End;
            }

            inputDescriptions = inputDescriptionsBuilder.ToImmutable();

            End:
            diagnostics = diagnosticsBuilder.ToImmutable();
        }
    }
}
