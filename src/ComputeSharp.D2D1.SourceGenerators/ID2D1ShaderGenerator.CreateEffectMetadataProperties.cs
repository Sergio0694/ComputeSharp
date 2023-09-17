using ComputeSharp.D2D1.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the effect metadata properties.
    /// </summary>
    private static partial class EffectMetadata
    {
        /// <summary>
        /// Extracts the effect display name info for the current shader.
        /// </summary>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the shader type in use.</param>
        /// <returns>The resulting effect display name, if available.</returns>
        public static string? GetEffectDisplayNameInfo(Compilation compilation, INamedTypeSymbol structDeclarationSymbol)
        {
            if (D2D1EffectMetadataParser.TryGetEffectMetadataName(
                compilation,
                structDeclarationSymbol,
                "ComputeSharp.D2D1.D2DEffectDisplayNameAttribute",
                out string? effectDisplayName))
            {
                return effectDisplayName;
            }

            return null;
        }
    }
}