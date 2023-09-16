using System;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Text.RegularExpressions;
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
    /// Checks whether a given <see cref="AttributeData"/> value contains a valid effect display name.
    /// </summary>
    /// <param name="attributeData">The input <see cref="AttributeData"/> object to inspect.</param>
    /// <returns>Whether <paramref name="attributeData"/> contains a valid effect display name.</returns>
    public static bool IsValidEffectDisplayName(AttributeData attributeData)
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
    /// Tries to get the defined effect display name for a given attribute.
    /// </summary>
    /// <param name="attributeData">The input <see cref="AttributeData"/> object to inspect.</param>
    /// <param name="effectDisplayName">The resulting defined effect display name, if found.</param>
    /// <returns>Whether or not a defined effect display name could be found.</returns>
    public static bool TryGetEffectDisplayName(AttributeData attributeData, [NotNullWhen(true)] out string? effectDisplayName)
    {
        // Check that the attribute is [D2DEffectDisplayName] and with a valid parameter
        if (attributeData.ConstructorArguments is [{ Value: string { Length: > 0 } value }])
        {
            // Remove new lines (the values cannot span multiple lines in XML)
            string singleLineValue = NewLinesRegex.Replace(value, string.Empty);

            // Make sure to escape any invalid XML characters
            string escapedValue = SecurityElement.Escape(singleLineValue);

            // Trim the display name as well
            if (escapedValue.AsSpan().Trim() is { Length: > 0 } trimmedValue)
            {
                effectDisplayName = trimmedValue.ToString();

                return true;
            }
        }

        effectDisplayName = default;

        return false;
    }
}
