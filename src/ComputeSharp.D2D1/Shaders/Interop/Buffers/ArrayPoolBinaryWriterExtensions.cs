using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
    public static unsafe void WriteRaw<T>(this ref ArrayPoolBufferWriter<byte> writer, scoped in T value)
        where T : unmanaged
    {
        Span<byte> span = writer.GetSpan(sizeof(T));

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), value);

        writer.Advance(sizeof(T));
    }

    /// <summary>
    /// Writes the raw data from the input <see cref="ReadOnlySpan{T}"/> into the buffer.
    /// </summary>
    /// <typeparam name="T">The type of values in the target writer.</typeparam>
    /// <param name="writer">The target <see cref="ArrayPoolBufferWriter{T}"/> instance to write data to.</param>
    /// <param name="data">The data to write.</param>
    public static void WriteRaw<T>(this ref ArrayPoolBufferWriter<T> writer, scoped ReadOnlySpan<T> data)
        where T : unmanaged
    {
        Span<T> span = writer.GetSpan(data.Length);

        data.CopyTo(span);

        writer.Advance(data.Length);
    }
}