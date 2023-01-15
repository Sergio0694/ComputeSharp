using System.Runtime.InteropServices;

namespace System.Text;

/// <summary>
/// A polyfill type that mirrors some methods from <see cref="Encoding"/> on .NET 5.
/// </summary>
internal static class EncodingExtensions
{
    /// <summary>
    /// Encodes into a span of bytes a set of characters from the specified read-only span.
    /// </summary>
    /// <param name="encoding">The input <see cref="Encoding"/> instance to use.</param>
    /// <param name="chars">The span containing the set of characters to encode.</param>
    /// <param name="bytes">The byte span to hold the encoded bytes.</param>
    /// <returns>The number of encoded bytes.</returns>
    public static unsafe int GetBytes(this Encoding encoding, ReadOnlySpan<char> chars, Span<byte> bytes)
    {
        fixed (char* charsPtr = &MemoryMarshal.GetReference(chars))
        fixed (byte* bytesPtr = &MemoryMarshal.GetReference(bytes))
        {
            return encoding.GetBytes(charsPtr, chars.Length, bytesPtr, bytes.Length);
        }
    }

    /// <summary>
    /// Decodes a text from a sequence of bytes.
    /// </summary>
    /// <param name="encoding">The input <see cref="Encoding"/> instance to use.</param>
    /// <param name="bytes">The byte span that holds the encoded bytes.</param>
    /// <returns>The resulting text.</returns>
    public static unsafe string GetString(this Encoding encoding, ReadOnlySpan<byte> bytes)
    {
        fixed (byte* bytesPtr = &MemoryMarshal.GetReference(bytes))
        {
            return encoding.GetString(bytesPtr, bytes.Length);
        }
    }
}