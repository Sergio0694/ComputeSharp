using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
#if NET6_0_OR_GREATER
using System.Numerics;
#endif
using System.Runtime.CompilerServices;
#if !NET6_0_OR_GREATER
using BitOperations = ComputeSharp.D2D1.NetStandard.System.Buffers.BitOperations;
#endif

namespace ComputeSharp.D2D1.Shaders.Interop.Buffers;

/// <inheritdoc cref="ArrayPoolBufferWriter{T}"/>
internal static class ArrayPoolBinaryWriter
{
    /// <summary>
    /// The default buffer size to use to expand empty arrays.
    /// </summary>
    public const int DefaultInitialBufferSize = 256;
}

/// <summary>
/// Represents a heap-based, array-backed output sink into which data can be written.
/// </summary>
/// <typeparam name="T">The type of values to write.</typeparam>
internal ref struct ArrayPoolBufferWriter<T>
    where T : unmanaged
{
    /// <summary>
    /// The underlying <typeparamref name="T"/> array.
    /// </summary>
    private T[]? array;

    /// <summary>
    /// The starting offset within <see cref="array"/>.
    /// </summary>
    private int index;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayPoolBufferWriter{T}"/> class.
    /// </summary>
    public ArrayPoolBufferWriter(int capacity)
    {
        this.array = ArrayPool<T>.Shared.Rent(capacity);
        this.index = 0;
    }

    /// <summary>
    /// Gets the data written to the underlying buffer so far, as a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    [UnscopedRef]
    public readonly ReadOnlySpan<T> WrittenSpan
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            T[]? array = this.array;

            if (array is null)
            {
                ThrowObjectDisposedException();
            }

            return array.AsSpan(0, this.index);
        }
    }

    /// <summary>
    /// Advances the current writer.
    /// </summary>
    /// <param name="count">The amount to advance.</param>
    /// <remarks>
    /// <para>Must be called after <see cref="GetSpan"/>.</para>
    /// <para>This and the methods below are <see langword="readonly"/> to enable mutating extensions.</para>
    /// </remarks>
    internal void Advance(int count)
    {
        T[]? array = this.array;

        if (array is null)
        {
            ThrowObjectDisposedException();
        }

        if (count < 0)
        {
            ThrowArgumentOutOfRangeExceptionForNegativeCount();
        }

        if (this.index > array.Length - count)
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
    [UnscopedRef]
    internal Span<T> GetSpan(int sizeHint = 0)
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
        T[]? array = this.array;

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

        T[] newArray = ArrayPool<T>.Shared.Rent((int)minimumSize);

        Buffer.BlockCopy(this.array!, 0, newArray, 0, this.index * sizeof(T));

        ArrayPool<T>.Shared.Return(this.array!);

        this.array = newArray;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        T[]? array = this.array;

        if (array is null)
        {
            return;
        }

        this.array = null;

        ArrayPool<T>.Shared.Return(array);
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> when the requested count is negative.
    /// </summary>
    [DoesNotReturn]
    private static void ThrowArgumentOutOfRangeExceptionForNegativeCount()
    {
        throw new ArgumentOutOfRangeException("count", "The count can't be a negative value.");
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> when the size hint is negative.
    /// </summary>
    [DoesNotReturn]
    private static void ThrowArgumentOutOfRangeExceptionForNegativeSizeHint()
    {
        throw new ArgumentOutOfRangeException("sizeHint", "The size hint can't be a negative value.");
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> when the requested count is negative.
    /// </summary>
    [DoesNotReturn]
    private static void ThrowArgumentExceptionForAdvancedTooFar()
    {
        throw new ArgumentException("The buffer writer has advanced too far.");
    }

    /// <summary>
    /// Throws an <see cref="ObjectDisposedException"/> when <see cref="array"/> is <see langword="null"/>.
    /// </summary>
    [DoesNotReturn]
    private static void ThrowObjectDisposedException()
    {
        throw new ObjectDisposedException("The current buffer has already been disposed.");
    }
}