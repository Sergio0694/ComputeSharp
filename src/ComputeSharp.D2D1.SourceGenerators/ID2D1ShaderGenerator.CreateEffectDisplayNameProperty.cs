using System.Diagnostics.CodeAnalysis;
using ComputeSharp.D2D1.SourceGenerators.Helpers;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>EffectDisplayName</c> property.
    /// </summary>
    private static partial class EffectDisplayName
    {
        /// <summary>
        /// Extracts the effect display name info for the current shader.
        /// </summary>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the shader type in use.</param>
        /// <returns>The resulting effect display name.</returns>
        public static string GetInfo(Compilation compilation, INamedTypeSymbol structDeclarationSymbol)
        {
            if (TryGetDefinedEffectDisplayName(compilation, structDeclarationSymbol, out string? effectDisplayName))
            {
                return effectDisplayName;
            }

            return structDeclarationSymbol.GetFullyQualifiedMetadataName();
        }

        /// <summary>
        /// Tries to get the defined effect display name for a given shader type.
        /// </summary>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance.</param>
        /// <param name="effectDisplayName">The resulting defined effect display name, if found.</param>
        /// <returns>Whether or not a defined effect display name could be found.</returns>
        private static bool TryGetDefinedEffectDisplayName(Compilation compilation, INamedTypeSymbol typeSymbol, [NotNullWhen(true)] out string? effectDisplayName)
        {
            INamedTypeSymbol effectIdAttributeSymbol = compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DEffectDisplayNameAttribute")!;

            foreach (AttributeData attributeData in typeSymbol.GetAttributes())
            {
                // Check that the attribute is [D2DEffectDisplayName]
                if (SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, effectIdAttributeSymbol))
                {
                    return D2D1EffectMetadataParser.TryGetEffectDisplayName(attributeData, out effectDisplayName);
                }
            }

            effectDisplayName = default;

            return false;
        }
    }
}