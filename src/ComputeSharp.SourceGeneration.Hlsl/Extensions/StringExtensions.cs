using System;
using System.Buffers;

namespace ComputeSharp.SourceGeneration.Extensions;

/// <summary>
/// Extension methods for <see cref="string"/> types.
/// </summary>
internal static class StringExtensions
{
    /// <summary>
    /// Converts a given <see cref="string"/> instance to a valid HLSL identifier name.
    /// </summary>
    /// <param name="text">The input <see cref="string"/> instance to convert.</param>
    /// <returns>An valid HLSL identifier name from <paramref name="text"/>.</returns>
    public static string ToHlslIdentifierName(this string text)
    {
        return text.Replace('.', '_').Replace('+', '_');
    }

    /// <summary>
    /// Normalizes a given <see cref="string"/> instance to make it single line and with no repeated spaces.
    /// </summary>
    /// <param name="text">The input <see cref="string"/> instance to normalize.</param>
    /// <returns>The normalized version of <paramref name="text"/>.</returns>
    public static string NormalizeToSingleLine(this string text)
    {
        if (text is "")
        {
            return text;
        }

        char[] buffer = ArrayPool<char>.Shared.Rent(text.Length);
        int index = 0;
        char lastCharacter = '\0';

        foreach (char newCharacter in text)
        {
            char pendingCharacter = newCharacter;

            // Ignore '\r' characters
            if (pendingCharacter is '\r')
            {
                continue;
            }

            // Convert newlines to spaces (this avoids VS droppping multiline diagnostics)
            if (pendingCharacter is '\n')
            {
                pendingCharacter = ' ';
            }

            // Drop duplicate spaces
            if (pendingCharacter is ' ' &&
                lastCharacter is ' ')
            {
                continue;
            }

            buffer[index++] = lastCharacter = pendingCharacter;
        }

        text = buffer.AsSpan(0, index).ToString();

        ArrayPool<char>.Shared.Return(buffer);

        return text;
    }
}