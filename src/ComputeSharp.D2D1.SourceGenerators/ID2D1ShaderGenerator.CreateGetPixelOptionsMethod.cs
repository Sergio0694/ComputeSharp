using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>GetPixelOptions</c> method.
    /// </summary>
    private static partial class GetPixelOptions
    {
        /// <summary>
        /// Extracts the pixel options for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="pixelOptions">The pixel options for the shader.</param>
        public static void GetInfo(INamedTypeSymbol structDeclarationSymbol, out D2D1PixelOptions pixelOptions)
        {
            if (structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DPixelOptionsAttribute", out AttributeData? attributeData))
            {
                pixelOptions = (D2D1PixelOptions)attributeData!.ConstructorArguments[0].Value!;
            }
            else
            {
                pixelOptions = D2D1PixelOptions.None;
            }
        }
    }
}