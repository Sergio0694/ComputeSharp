using System;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Text.RegularExpressions;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators.Helpers;

/// <summary>
/// A helper class to parse D2D metadata values.
/// </summary>
internal static class D2D1EffectMetadataParser
{
    /// <summary>
    /// A <see cref="Regex"/> instance to find all newlines.
    /// </summary>
    private static readonly Regex NewLinesRegex = new("[\r\n\v]", RegexOptions.Compiled);

    /// <summary>
    /// Checks whether a given <see cref="AttributeData"/> value contains a valid effect metadata name.
    /// </summary>
    /// <param name="attributeData">The input <see cref="AttributeData"/> object to inspect.</param>
    /// <returns>Whether <paramref name="attributeData"/> contains a valid effect metadata name.</returns>
    public static bool IsValidEffectMetadataName(AttributeData attributeData)
    {
        if (attributeData.ConstructorArguments is [{ Value: string { Length: > 0 } value }])
        {
            // Remove new lines (the values cannot span multiple lines in XML)
            string singleLineValue = NewLinesRegex.Replace(value, string.Empty);

            // Trim directly and check that (no need to escape, as that wouldn't change things)
            return singleLineValue.AsSpan().Trim().Length > 0;
        }

        return false;
    }

    /// <summary>
    /// Tries to get the defined effect metadata name for a given shader type.
    /// </summary>
    /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance.</param>
    /// <param name="attributeFullyQualifiedMetadataName">The fully qualified metadata name of the target attribute.</param>
    /// <param name="isAssemblyLevelAttributeSupported">Whether the attribute can also be on the assembly.</param>
    /// <param name="effectMetadataName">The resulting defined effect metadata name, if found.</param>
    /// <returns>Whether or not a defined effect metadata name could be found.</returns>
    public static bool TryGetEffectMetadataName(
        Compilation compilation,
        INamedTypeSymbol typeSymbol,
        string attributeFullyQualifiedMetadataName,
        bool isAssemblyLevelAttributeSupported,
        [NotNullWhen(true)] out string? effectMetadataName)
    {
        INamedTypeSymbol metadataAttributeSymbol = compilation.GetTypeByMetadataName(attributeFullyQualifiedMetadataName)!;

        // Always check the attributes on the type first
        if (typeSymbol.TryGetAttributeWithType(metadataAttributeSymbol, out AttributeData? attributeData))
        {
            return TryGetEffectMetadataName(attributeData, out effectMetadataName);
        }

        // Check assembly-level attributes next, if needed
        if (isAssemblyLevelAttributeSupported &&
            typeSymbol.ContainingAssembly!.TryGetAttributeWithType(metadataAttributeSymbol, out attributeData))
        {
            return TryGetEffectMetadataName(attributeData, out effectMetadataName);
        }

        effectMetadataName = default;

        return false;
    }

    /// <summary>
    /// Tries to get the defined effect metadata name for a given attribute.
    /// </summary>
    /// <param name="attributeData">The input <see cref="AttributeData"/> object to inspect.</param>
    /// <param name="effectMetadataName">The resulting defined effect metadata name, if found.</param>
    /// <returns>Whether or not a defined effect metadata name could be found.</returns>
    public static bool TryGetEffectMetadataName(AttributeData attributeData, [NotNullWhen(true)] out string? effectMetadataName)
    {
        // Check that the attribute has a valid parameter
        if (attributeData.ConstructorArguments is [{ Value: string { Length: > 0 } value }])
        {
            // Remove new lines (the values cannot span multiple lines in XML)
            string singleLineValue = NewLinesRegex.Replace(value, string.Empty);

            // Make sure to escape any invalid XML characters
            string escapedValue = SecurityElement.Escape(singleLineValue);

            // Trim the display name as well
            if (escapedValue.AsSpan().Trim() is { Length: > 0 } trimmedValue)
            {
                effectMetadataName = trimmedValue.ToString();

                return true;
            }
        }

        effectMetadataName = default;

        return false;
    }
}