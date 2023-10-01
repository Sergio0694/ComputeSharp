using ComputeSharp.D2D1.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
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
                isAssemblyLevelAttributeSupported: false,
                out string? effectDisplayName))
            {
                return effectDisplayName;
            }

            return null;
        }

        /// <summary>
        /// Extracts the effect description info for the current shader.
        /// </summary>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the shader type in use.</param>
        /// <returns>The resulting effect description, if available.</returns>
        public static string? GetEffectDescriptionInfo(Compilation compilation, INamedTypeSymbol structDeclarationSymbol)
        {
            if (D2D1EffectMetadataParser.TryGetEffectMetadataName(
                compilation,
                structDeclarationSymbol,
                "ComputeSharp.D2D1.D2DEffectDescriptionAttribute",
                isAssemblyLevelAttributeSupported: false,
                out string? effectDescription))
            {
                return effectDescription;
            }

            return null;
        }

        /// <summary>
        /// Extracts the effect category info for the current shader.
        /// </summary>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the shader type in use.</param>
        /// <returns>The resulting effect category, if available.</returns>
        public static string? GetEffectCategoryInfo(Compilation compilation, INamedTypeSymbol structDeclarationSymbol)
        {
            if (D2D1EffectMetadataParser.TryGetEffectMetadataName(
                compilation,
                structDeclarationSymbol,
                "ComputeSharp.D2D1.D2DEffectCategoryAttribute",
                isAssemblyLevelAttributeSupported: true,
                out string? effectCategory))
            {
                return effectCategory;
            }

            return null;
        }

        /// <summary>
        /// Extracts the effect author info for the current shader.
        /// </summary>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the shader type in use.</param>
        /// <returns>The resulting effect author, if available.</returns>
        public static string? GetEffectAuthorInfo(Compilation compilation, INamedTypeSymbol structDeclarationSymbol)
        {
            if (D2D1EffectMetadataParser.TryGetEffectMetadataName(
                compilation,
                structDeclarationSymbol,
                "ComputeSharp.D2D1.D2DEffectAuthorAttribute",
                isAssemblyLevelAttributeSupported: true,
                out string? effectAuthor))
            {
                return effectAuthor;
            }

            return null;
        }
    }
}