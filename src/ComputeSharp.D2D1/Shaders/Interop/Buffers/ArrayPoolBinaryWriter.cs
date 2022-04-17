using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
#if !NET6_0_OR_GREATER
using ComputeSharp.D2D1.NetStandard.System.Text;
using BitOperations = ComputeSharp.D2D1.NetStandard.System.Buffers.BitOperations;
#endif

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
    /// Initializes a new instance of the <see cref="ArrayPoolBinaryWriter"/> class.
    /// </summary>
    public ArrayPoolBinaryWriter(int capacity)
    {
        this.array = ArrayPool<byte>.Shared.Rent(capacity);
        this.index = 0;
    }

    /// <summary>
    /// Gets the data written to the underlying buffer so far, as a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    public ReadOnlySpan<byte> WrittenSpan
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            byte[]? array = this.array;

            if (array is null)
            {
                ThrowObjectDisposedException();
            }

            return array!.AsSpan(0, this.index);
        }
    }

    /// <summary>
    /// Writes a value of type <typeparamref name="T"/> into the buffer.
    /// </summary>
    /// <typeparam name="T">The type of value to write.</typeparam>
    /// <param name="value">The value to write.</param>
    public unsafe void Write<T>(in T value)
        where T : unmanaged
    {
        Span<byte> span = GetSpan(sizeof(T));

        MemoryMarshal.Write(span, ref Unsafe.AsRef(in value));

        Advance(sizeof(T));
    }

    /// <summary>
    /// Writes text as UTF8 bytes into the buffer.
    /// </summary>
    /// <param name="text">The text to write.</param>
    public unsafe void WriteAsUtf8(string text)
    {
        int maximumByteSize = Encoding.UTF8.GetMaxByteCount(text.Length);

        Span<byte> span = GetSpan(maximumByteSize);

        int writtenBytes = Encoding.UTF8.GetBytes(text.AsSpan(), span);

        Advance(writtenBytes);
    }

    /// <summary>
    /// Advances the current writer.
    /// </summary>
    /// <param name="count">The amount to advance.</param>
    /// <remarks>Must be called after <see cref="GetSpan"/>.</remarks>
    private void Advance(int count)
    {
        byte[]? array = this.array;

        if (array is null)
        {
            ThrowObjectDisposedException();
        }

        if (count < 0)
        {
            ThrowArgumentOutOfRangeExceptionForNegativeCount();
        }

        if (this.index > array!.Length - count)
        {
            ThrowArgumentExceptionForAdvancedTooFar();
        }

        this.index += count;
    }

    /// <summary>
    /// Gets a <see cref="Span{T}"/> to write data to, with an optional minimum capacity.
    /// </summary>
    /// <param name="sizeHint">The capacity to request.</param>
    /// <returns>A <see cref="Span{T}"/> to write data to.</returns>
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

        if (array is null)
        {
            ThrowObjectDisposedException();
        }

        if (sizeHint < 0)
        {
            ThrowArgumentOutOfRangeExceptionForNegativeSizeHint();
        }

        if (sizeHint == 0)
        {
            sizeHint = 1;
        }

        if (sizeHint > array!.Length - this.index)
        {
            ResizeBuffer(sizeHint);
        }
    }

    /// <summary>
    /// Resizes <see cref="array"/> to ensure it can fit the specified number of new items.
    /// </summary>
    /// <param name="sizeHint">The minimum number of items to ensure space for in <see cref="array"/>.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void ResizeBuffer(int sizeHint)
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
    /// Throws an <see cref="ArgumentOutOfRangeException"/> when the requested count is negative.
    /// </summary>
    private static void ThrowArgumentOutOfRangeExceptionForNegativeCount()
    {
        throw new ArgumentOutOfRangeException("count", "The count can't be a negative value.");
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> when the size hint is negative.
    /// </summary>
    private static void ThrowArgumentOutOfRangeExceptionForNegativeSizeHint()
    {
        throw new ArgumentOutOfRangeException("sizeHint", "The size hint can't be a negative value.");
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> when the requested count is negative.
    /// </summary>
    private static void ThrowArgumentExceptionForAdvancedTooFar()
    {
        throw new ArgumentException("The buffer writer has advanced too far.");
    }

    /// <summary>
    /// Throws an <see cref="ObjectDisposedException"/> when <see cref="array"/> is <see langword="null"/>.
    /// </summary>
    private static void ThrowObjectDisposedException()
    {
        throw new ObjectDisposedException("The current buffer has already been disposed.");
    }
}