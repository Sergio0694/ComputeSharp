using System.Buffers;

namespace System.IO;

/// <summary>
/// A polyfill type with extensions for <see cref="Stream"/>.
/// </summary>
internal static class StreamExtensions
{
    /// <summary>
    /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    /// <param name="stream">The input <see cref="Stream"/> instance.</param>
    /// <param name="span">A region of memory. When this method returns, the contents of this region are replaced by the bytes read from the current source.</param>
    /// <returns>The total number of bytes read into the buffer.</returns>
    public static int Read(this Stream stream, Span<byte> span)
    {
        if (span.IsEmpty)
        {
            return 0;
        }

        byte[] array = ArrayPool<byte>.Shared.Rent(span.Length);

        int bytesRead = stream.Read(array, 0, span.Length);

        array.AsSpan(0, bytesRead).CopyTo(span);

        ArrayPool<byte>.Shared.Return(array);

        return bytesRead;
    }

    /// <summary>
    /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
    /// </summary>
    /// <param name="stream">The input <see cref="Stream"/> instance.</param>
    /// <param name="span">A region of memory. This method copies the contents of this region to the current stream.</param>
    public static void Write(this Stream stream, ReadOnlySpan<byte> span)
    {
        if (span.IsEmpty)
        {
            return;
        }

        byte[] array = ArrayPool<byte>.Shared.Rent(span.Length);

        span.CopyTo(array);

        stream.Write(array, 0, span.Length);

        ArrayPool<byte>.Shared.Return(array);
    }
}