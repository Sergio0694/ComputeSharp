using System.Collections.Immutable;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>InputDescriptions</c> property.
    /// </summary>
    private static partial class InputDescriptions
    {
        /// <summary>
        /// Extracts the input descriptions for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="inputDescriptions">The produced input descriptions for the shader.</param>
        public static void GetInfo(
            INamedTypeSymbol structDeclarationSymbol,
            out ImmutableArray<InputDescription> inputDescriptions)
        {
            using ImmutableArrayBuilder<InputDescription> builder = new();

            foreach (AttributeData attributeData in structDeclarationSymbol.GetAttributes())
            {
                if (attributeData.AttributeClass?.HasFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DInputDescriptionAttribute") == true)
                {
                    int index = (int)attributeData.ConstructorArguments[0].Value!;
                    D2D1Filter filter = (D2D1Filter)attributeData.ConstructorArguments[1].Value!;

                    _ = attributeData.TryGetNamedArgument("LevelOfDetailCount", out int levelOfDetailCount);

                    builder.Add(new InputDescription(index, filter, levelOfDetailCount));
                }
            }

            inputDescriptions = builder.ToImmutable();
        }
    }
}