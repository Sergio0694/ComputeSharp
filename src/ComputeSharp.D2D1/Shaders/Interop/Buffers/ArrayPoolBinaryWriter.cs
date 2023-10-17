using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.D2D1.Shaders.Interop.Buffers;

/// <summary>
/// Represents a heap-based, array-backed output sink into which binary data can be written.
/// </summary>
internal ref struct ArrayPoolBinaryWriter
{
    /// <summary>
    /// The default buffer size to use to expand empty arrays.
    /// </summary>
    public const int DefaultInitialBufferSize = 256;

    /// <summary>
    /// The underlying <see cref="byte"/> array.
    /// </summary>
    private byte[]? array;

    /// <summary>
    /// The starting offset within <see cref="array"/>.
    /// </summary>
    private int index;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayPoolBinaryWriter"/> type.
    /// </summary>
    public ArrayPoolBinaryWriter(int capacity)
    {
        this.array = ArrayPool<byte>.Shared.Rent(capacity);
        this.index = 0;
    }

    /// <summary>
    /// Gets the data written to the underlying buffer so far, as a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    [UnscopedRef]
    public readonly ReadOnlySpan<byte> WrittenSpan
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            byte[]? array = this.array;

            default(ObjectDisposedException).ThrowIfNull(array);

            return array.AsSpan(0, this.index);
        }
    }

    /// <summary>
    /// Writes the raw value of type <typeparamref name="T"/> into the buffer.
    /// </summary>
    /// <typeparam name="T">The type of value to write.</typeparam>
    /// <param name="value">The value to write.</param>
    /// <remarks>This method will just blit the data of <paramref name="value"/> into the target buffer.</remarks>
    public unsafe void Write<T>(scoped in T value)
    {
        Span<byte> span = GetSpan(sizeof(T));

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), value);

        Advance(sizeof(T));
    }

    /// <summary>
    /// Writes the raw data from the input <see cref="ReadOnlySpan{T}"/> into the buffer.
    /// </summary>
    /// <param name="data">The data to write.</param>
    public void Write(scoped ReadOnlySpan<byte> data)
    {
        Span<byte> span = GetSpan(data.Length);

        data.CopyTo(span);

        Advance(data.Length);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        byte[]? array = this.array;

        if (array is null)
        {
            return;
        }

        this.array = null;

        ArrayPool<byte>.Shared.Return(array);
    }

    /// <summary>
    /// Advances the current writer.
    /// </summary>
    /// <param name="count">The amount to advance.</param>
    /// <remarks>Must be called after <see cref="GetSpan"/>.</remarks>
    private void Advance(int count)
    {
        byte[]? array = this.array;

        default(ObjectDisposedException).ThrowIfNull(array);
        default(ArgumentException).ThrowIf(this.index > array.Length - count);

        this.index += count;
    }

    /// <summary>
    /// Gets a <see cref="Span{T}"/> to write data to, with an optional minimum capacity.
    /// </summary>
    /// <param name="sizeHint">The capacity to request.</param>
    /// <returns>A <see cref="Span{T}"/> to write data to.</returns>
    [UnscopedRef]
    private Span<byte> GetSpan(int sizeHint = 0)
    {
        CheckBufferAndEnsureCapacity(sizeHint);

        return this.array.AsSpan(this.index);
    }

    /// <summary>
    /// Ensures that <see cref="array"/> has enough free space to contain a given number of new items.
    /// </summary>
    /// <param name="sizeHint">The minimum number of items to ensure space for in <see cref="array"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CheckBufferAndEnsureCapacity(int sizeHint)
    {
        byte[]? array = this.array;

        default(ObjectDisposedException).ThrowIfNull(array);
        default(ArgumentOutOfRangeException).ThrowIfNegative(sizeHint);

        if (sizeHint == 0)
        {
            sizeHint = 1;
        }

        if (sizeHint > array.Length - this.index)
        {
            ResizeBuffer(sizeHint);
        }
    }

    /// <summary>
    /// Resizes <see cref="array"/> to ensure it can fit the specified number of new items.
    /// </summary>
    /// <param name="sizeHint">The minimum number of items to ensure space for in <see cref="array"/>.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private unsafe void ResizeBuffer(int sizeHint)
    {
        uint minimumSize = (uint)this.index + (uint)sizeHint;

        // The ArrayPool<T> class has a maximum threshold of 1024 * 1024 for the maximum length of
        // pooled arrays, and once this is exceeded it will just allocate a new array every time
        // of exactly the requested size. In that case, we manually round up the requested size to
        // the nearest power of two, to ensure that repeated consecutive writes when the array in
        // use is bigger than that threshold don't end up causing a resize every single time.
        if (minimumSize > 1024 * 1024)
        {
            minimumSize = BitOperations.RoundUpToPowerOf2(minimumSize);
        }

        byte[] newArray = ArrayPool<byte>.Shared.Rent((int)minimumSize);

        Buffer.BlockCopy(this.array!, 0, newArray, 0, this.index);

        ArrayPool<byte>.Shared.Return(this.array!);

        this.array = newArray;
    }
}