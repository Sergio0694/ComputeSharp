using System;
#if NET6_0_OR_GREATER
using System.Buffers.Text;
#endif
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
#if !NET6_0_OR_GREATER
using ComputeSharp.D2D1.NetStandard.System.Text;
#endif

namespace ComputeSharp.D2D1.Shaders.Interop.Buffers;

/// <summary>
/// Helpers for the <see cref="ArrayPoolBufferWriter{T}"/> type.
/// </summary>
internal static class ArrayPoolBinaryWriterExtensions
{
    /// <summary>
    /// Writes the raw value of type <typeparamref name="T"/> into the buffer.
    /// </summary>
    /// <typeparam name="T">The type of value to write.</typeparam>
    /// <param name="writer">The target <see cref="ArrayPoolBufferWriter{T}"/> instance to write data to.</param>
    /// <param name="value">The value to write.</param>
    /// <remarks>This method will just blit the data of <paramref name="value"/> into the target buffer.</remarks>
    public static unsafe void WriteRaw<T>(this in ArrayPoolBufferWriter<byte> writer, in T value)
        where T : unmanaged
    {
        Span<byte> span = writer.GetSpan(sizeof(T));

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), value);

        writer.Advance(sizeof(T));
    }

    /// <summary>
    /// Writes the raw value of type <typeparamref name="T"/> into the buffer.
    /// </summary>
    /// <typeparam name="T">The type of value to write.</typeparam>
    /// <param name="writer">The target <see cref="ArrayPoolBufferWriter{T}"/> instance to write data to.</param>
    /// <param name="value">The value to write.</param>
    public static unsafe void WriteRaw<T>(this in ArrayPoolBufferWriter<T> writer, in T value)
        where T : unmanaged
    {
        Span<T> span = writer.GetSpan(1);

        MemoryMarshal.GetReference(span) = value;

        writer.Advance(1);
    }

    /// <summary>
    /// Writes the raw data from the input <see cref="ReadOnlySpan{T}"/> into the buffer.
    /// </summary>
    /// <typeparam name="T">The type of values in the target writer.</typeparam>
    /// <param name="writer">The target <see cref="ArrayPoolBufferWriter{T}"/> instance to write data to.</param>
    /// <param name="data">The data to write.</param>
    public static void WriteRaw<T>(this in ArrayPoolBufferWriter<T> writer, ReadOnlySpan<T> data)
        where T : unmanaged
    {
        Span<T> span = writer.GetSpan(data.Length);

        data.CopyTo(span);

        writer.Advance(data.Length);
    }

#if !NET6_0_OR_GREATER
    /// <summary>
    /// Writes the raw data from the input <see cref="ReadOnlySpan{T}"/> into the buffer.
    /// </summary>
    /// <param name="writer">The target <see cref="ArrayPoolBufferWriter{T}"/> instance to write data to.</param>
    /// <param name="data">The data to write.</param>
    public static void WriteRaw(this in ArrayPoolBufferWriter<char> writer, string data)
    {
        Span<char> span = writer.GetSpan(data.Length);

        data.AsSpan().CopyTo(span);

        writer.Advance(data.Length);
    }
#endif

    /// <summary>
    /// Writes an <see langword="int"/> value as UTF8 bytes into the buffer.
    /// </summary>
    /// <param name="writer">The target <see cref="ArrayPoolBufferWriter{T}"/> instance to write data to.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteAsUtf8(this in ArrayPoolBufferWriter<byte> writer, int value)
    {
#if NET6_0_OR_GREATER
        // Get a span of at least 10 elements (10 is the length of int.MaxValue as text)
        Span<byte> span = writer.GetSpan(10);

        _ = Utf8Formatter.TryFormat(value, span, out int bytesWritten);

        writer.Advance(bytesWritten);
#else
        // Just accept the allocation on .NET Standard 2.0
        writer.WriteAsUtf8(value.ToString());
#endif
    }

    /// <summary>
    /// Writes text as UTF8 bytes into the buffer.
    /// </summary>
    /// <param name="writer">The target <see cref="ArrayPoolBufferWriter{T}"/> instance to write data to.</param>
    /// <param name="text">The text to write.</param>
    public static void WriteAsUtf8(this in ArrayPoolBufferWriter<byte> writer, string text)
    {
        int maximumByteSize = Encoding.UTF8.GetMaxByteCount(text.Length);

        Span<byte> span = writer.GetSpan(maximumByteSize);

        int writtenBytes = Encoding.UTF8.GetBytes(text.AsSpan(), span);

        writer.Advance(writtenBytes);
    }

    /// <summary>
    /// Writes an <see langword="int"/> value as Unicode bytes into the buffer.
    /// </summary>
    /// <param name="writer">The target <see cref="ArrayPoolBufferWriter{T}"/> instance to write data to.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteAsUnicode(this in ArrayPoolBufferWriter<char> writer, int value)
    {
#if NET6_0_OR_GREATER
        // Get a span of at least 10 elements (10 is the length of int.MaxValue as text)
        Span<char> span = writer.GetSpan(10);

        _ = value.TryFormat(span, out int charsWritten);

        writer.Advance(charsWritten);
#else
        // Just accept the allocation on .NET Standard 2.0
        writer.WriteRaw(value.ToString().AsSpan());
#endif
    }
}