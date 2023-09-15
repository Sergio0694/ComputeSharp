using System;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
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
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the shader type in use.</param>
        /// <returns>The resulting effect display name.</returns>
        public static string GetInfo(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            Compilation compilation,
            INamedTypeSymbol structDeclarationSymbol)
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
                // Check that the attribute is [D2DEffectDisplayName] and with a valid parameter
                if (SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, effectIdAttributeSymbol) &&
                    attributeData.ConstructorArguments is [{ Value: string { Length: > 0 } value }])
                {
                    // Make sure to escape any invalid XML characters
                    string escapedValue = SecurityElement.Escape(value);

                    // Trim the display name as well
                    if (escapedValue.AsSpan().Trim() is { Length: > 0 } trimmedValue)
                    {
                        effectDisplayName = trimmedValue.ToString();

                        return true;
                    }
                }
            }

            effectDisplayName = default;

            return false;
        }
    }
}