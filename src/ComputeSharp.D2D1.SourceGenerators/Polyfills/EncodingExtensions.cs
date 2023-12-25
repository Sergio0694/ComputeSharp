namespace System.Text;

/// <summary>
/// Extensions for the <see cref="Encoding"/> type.
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
        fixed (char* charsPtr = chars)
        fixed (byte* bytesPtr = bytes)
        {
            return encoding.GetBytes(charsPtr, chars.Length, bytesPtr, bytes.Length);
        }
    }
}