using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#pragma warning disable CS0809, CA1065

namespace ComputeSharp;

/// <summary>
/// The <see cref="TextureView3D{T}"/> mirrors the <see cref="TextureView3D{T}"/> type but representing a memory
/// area spanning 3 dimensions. The underlying buffer can be discontiguous across both the height and the depth.
/// It is recommended to execute as much computation on the GPU side as possible.
/// </summary>
/// <typeparam name="T">The type of items in the current <see cref="TextureView3D{T}"/> instance.</typeparam>
public readonly unsafe ref struct TextureView3D<T>
    where T : unmanaged
{
    // The internal layout of a 3D texture mapped in memory is the same as that of a
    // 3D texture, with different 3D slices being stacked along the depth axis. Each
    // slice also has the same padding as the one between rows, which means that if
    // rows have no padding, all 3D slices are contiguous in memory as well.
    // The memory representation essentially looks like this:
    //
    //    ________________width______________
    //   /  __pointer                        \
    //  /  /                                  \
    // | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- |-|
    // | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- | |
    // | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- | |-height
    // | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- | |
    // | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- |-|
    // | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- |
    // | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- |
    // `    `    `    `    `    `    `    `    `    `    `
    //  `    `    `    `    `    `    `    `    `    `    `
    //   | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- |
    //   | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- |
    //   | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- |
    //   | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- |
    //   | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- |
    //   | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- |
    //   | XX | XX | XX | XX | XX | XX | XX | XX | -- | -- |
    //    \________________stride_________________________/
    //
    // Note that the depth stride is omitted after the last 3D slice.

    /// <summary>
    /// The pointer to the first element of the target 3D region.
    /// </summary>
    private readonly T* pointer;

    /// <summary>
    /// The width of the specified 3D texture.
    /// </summary>
    private readonly int width;

    /// <summary>
    /// The height of the specified 3D region.
    /// </summary>
    private readonly int height;

    /// <summary>
    /// The depth of the specified 3D region.
    /// </summary>
    private readonly int depth;

    /// <summary>
    /// The stride of the specified 3D region.
    /// </summary>
    private readonly int strideInBytes;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextureView3D{T}"/> struct with the specified parameters.
    /// </summary>
    /// <param name="pointer">The pointer to the start of the memory area to map.</param>
    /// <param name="width">The width of the 3D memory area to map.</param>
    /// <param name="height">The height of the 3D memory area to map.</param>
    /// <param name="depth">The depth of the 3D memory area to map.</param>
    /// <param name="strideInBytes">The stride in bytes of the 3D memory area to map.</param>
    internal TextureView3D(T* pointer, int width, int height, int depth, int strideInBytes)
    {
        this.pointer = pointer;
        this.width = width;
        this.height = height;
        this.depth = depth;
        this.strideInBytes = strideInBytes;
    }

    /// <summary>
    /// Gets an empty <see cref="TextureView3D{T}"/> instance.
    /// </summary>
    public static TextureView3D<T> Empty => default;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="TextureView3D{T}"/> instance is empty.
    /// </summary>
    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.width == 0;
    }

    /// <summary>
    /// Gets the total length of the current <see cref="TextureView3D{T}"/> instance.
    /// </summary>
    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.width * this.height * this.depth;
    }

    /// <summary>
    /// Gets the width of the underlying 3D memory area.
    /// </summary>
    public int Width
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.width;
    }

    /// <summary>
    /// Gets the height of the underlying 3D memory area.
    /// </summary>
    public int Height
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.height;
    }

    /// <summary>
    /// Gets the depth of the underlying 3D memory area.
    /// </summary>
    public int Depth
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.depth;
    }

    /// <summary>
    /// Gets the element at the specified zero-based indices.
    /// </summary>
    /// <param name="x">The target column to get the element from.</param>
    /// <param name="y">The target row to get the element from.</param>
    /// <param name="z">The target depth to get the element from.</param>
    /// <returns>A reference to the element at the specified indices.</returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when either <paramref name="x"/>, <paramref name="y"/> or <paramref name="y"/> are invalid.
    /// </exception>
    public ref T this[int x, int y, int z]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            default(ArgumentOutOfRangeException).ThrowIfNotInRange(x, 0, this.width);
            default(ArgumentOutOfRangeException).ThrowIfNotInRange(y, 0, this.height);
            default(ArgumentOutOfRangeException).ThrowIfNotInRange(z, 0, this.depth);

            return ref *((T*)((byte*)this.pointer + (z * this.height * this.strideInBytes) + (y * this.strideInBytes)) + x);
        }
    }

    /// <summary>
    /// Clears the contents of the current <see cref="TextureView3D{T}"/> instance.
    /// </summary>
    public void Clear()
    {
        if (IsEmpty)
        {
            return;
        }

        if (TryGetSpan(out Span<T> span))
        {
            span.Clear();
        }
        else
        {
            for (int z = 0; z < this.depth; z++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    GetRowSpan(y, z).Clear();
                }
            }
        }
    }

    /// <summary>
    /// Copies the contents of this <see cref="TextureView3D{T}"/> into a destination <see cref="Span{T}"/> instance.
    /// </summary>
    /// <param name="destination">The destination <see cref="Span{T}"/> instance.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="destination" /> is shorter than the source <see cref="TextureView3D{T}"/> instance.
    /// </exception>
    public void CopyTo(Span<T> destination)
    {
        if (IsEmpty)
        {
            return;
        }

        if (TryGetSpan(out Span<T> span))
        {
            span.CopyTo(destination);
        }
        else
        {
            for (int z = 0, j = 0; z < this.depth; z++)
            {
                for (int y = 0; y < this.height; y++, j += this.width)
                {
                    GetRowSpan(y, z).CopyTo(destination.Slice(j));
                }
            }
        }
    }

    /// <summary>
    /// Copies the contents of this <see cref="TextureView3D{T}"/> into a destination <see cref="TextureView3D{T}"/> instance.
    /// For this API to succeed, the target <see cref="TextureView3D{T}"/> has to have the same shape as the current one.
    /// </summary>
    /// <param name="destination">The destination <see cref="TextureView3D{T}"/> instance.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> doesn't match the size of the current <see cref="TextureView3D{T}"/> instance.</exception>
    public void CopyTo(TextureView3D<T> destination)
    {
        default(ArgumentException).ThrowIf(destination.width != this.width, nameof(destination));
        default(ArgumentException).ThrowIf(destination.height != this.height, nameof(destination));
        default(ArgumentException).ThrowIf(destination.depth != this.depth, nameof(destination));

        if (IsEmpty)
        {
            return;
        }

        if (destination.TryGetSpan(out Span<T> span))
        {
            CopyTo(span);
        }
        else
        {
            for (int z = 0; z < this.depth; z++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    GetRowSpan(y, z).CopyTo(destination.GetRowSpan(y, z));
                }
            }
        }
    }

    /// <summary>
    /// Attempts to copy the current <see cref="TextureView3D{T}"/> instance to a destination <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="destination">The target <see cref="Span{T}"/> of the copy operation.</param>
    /// <returns>Whether or not the operation was successful.</returns>
    public bool TryCopyTo(Span<T> destination)
    {
        if (destination.Length >= Length)
        {
            CopyTo(destination);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Attempts to copy the current <see cref="TextureView3D{T}"/> instance to a destination <see cref="TextureView3D{T}"/>.
    /// </summary>
    /// <param name="destination">The target <see cref="TextureView3D{T}"/> of the copy operation.</param>
    /// <returns>Whether or not the operation was successful.</returns>
    public bool TryCopyTo(TextureView3D<T> destination)
    {
        if (destination.width == this.width &&
            destination.height == this.height &&
            destination.depth == this.depth)
        {
            CopyTo(destination);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Fills the elements of this span with a specified value.
    /// </summary>
    /// <param name="value">The value to assign to each element of the <see cref="TextureView3D{T}"/> instance.</param>
    public void Fill(T value)
    {
        if (IsEmpty)
        {
            return;
        }

        if (TryGetSpan(out Span<T> span))
        {
            span.Fill(value);
        }
        else
        {
            for (int z = 0; z < this.depth; z++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    GetRowSpan(y, z).Fill(value);
                }
            }
        }
    }

    /// <summary>
    /// Returns a reference to the first element within the current instance, with no bounds check.
    /// </summary>
    /// <returns>A reference to the first element within the current instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T* DangerousGetAddressAndByteStride(out int strideInBytes)
    {
        strideInBytes = this.strideInBytes;

        return this.pointer;
    }

    /// <summary>
    /// Gets a <see cref="Span{T}"/> for a specified row.
    /// </summary>
    /// <param name="y">The index of the target row to retrieve.</param>
    /// <param name="z">The depth of the row to retrieve.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when either <paramref name="y"/> or <paramref name="z"/> are out of range.</exception>
    /// <returns>The resulting row <see cref="Span{T}"/>.</returns>
    public Span<T> GetRowSpan(int y, int z)
    {
        default(ArgumentOutOfRangeException).ThrowIfNotInRange(y, 0, this.height);
        default(ArgumentOutOfRangeException).ThrowIfNotInRange(z, 0, this.depth);

        return new((byte*)this.pointer + (z * this.height * this.strideInBytes) + (y * this.strideInBytes), this.width);
    }

    /// <summary>
    /// Gets a <see cref="TextureView2D{T}"/> for a depth layer.
    /// </summary>
    /// <param name="z">The depth of the layer to retrieve.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="z"/> is out of range.</exception>
    /// <returns>The resulting row <see cref="TextureView2D{T}"/>.</returns>
    public TextureView2D<T> GetDepthView(int z)
    {
        default(ArgumentOutOfRangeException).ThrowIfNotInRange(z, 0, this.depth);

        return new(
            (T*)((byte*)this.pointer + (z * this.height * this.strideInBytes)),
            this.width,
            this.height,
            this.strideInBytes);
    }

    /// <summary>
    /// Tries to get a <see cref="Span{T}"/> instance, if the underlying buffer is contiguous and small enough.
    /// </summary>
    /// <param name="span">The resulting <see cref="Span{T}"/>, in case of success.</param>
    /// <returns>Whether or not <paramref name="span"/> was correctly assigned.</returns>
    public bool TryGetSpan(out Span<T> span)
    {
        if (this.strideInBytes == this.width)
        {
            span = new(this.pointer, Length);

            return true;
        }

        span = default;

        return false;
    }

    /// <summary>
    /// Copies the contents of the current <see cref="TextureView3D{T}"/> instance into a new 3D array.
    /// </summary>
    /// <returns>A 3D array containing the data in the current <see cref="TextureView3D{T}"/> instance.</returns>
    public T[,,] ToArray()
    {
        T[,,] array = new T[this.depth, this.height, this.width];

        fixed (T* pointer = array)
        {
            CopyTo(new Span<T>(pointer, Length));
        }

        return array;
    }

    /// <inheritdoc cref="Span{T}.Equals(object)"/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Equals() on TextureView3D<T> will always throw an exception. Use == instead.")]
    public override bool Equals(object? obj)
    {
        throw new NotSupportedException("ComputeSharp.TextureView3D<T>.Equals(object) is not supported");
    }

    /// <inheritdoc cref="Span{T}.GetHashCode()"/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("GetHashCode() on TextureView3D<T> will always throw an exception.")]
    public override int GetHashCode()
    {
        throw new NotSupportedException("ComputeSharp.TextureView3D<T>.GetHashCode() is not supported");
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"ComputeSharp.TextureView3D<{typeof(T)}>[{Width}, {Height}, {Depth}]";
    }

    /// <summary>
    /// Checks whether two <see cref="TextureView3D{T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="TextureView3D{T}"/> instance to compare.</param>
    /// <param name="right">The second <see cref="TextureView3D{T}"/> instance to compare.</param>
    /// <returns>Whether or not <paramref name="left"/> and <paramref name="right"/> are equal.</returns>
    public static bool operator ==(TextureView3D<T> left, TextureView3D<T> right) => left.pointer == right.pointer;

    /// <summary>
    /// Checks whether two <see cref="TextureView3D{T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="TextureView3D{T}"/> instance to compare.</param>
    /// <param name="right">The second <see cref="TextureView3D{T}"/> instance to compare.</param>
    /// <returns>Whether or not <paramref name="left"/> and <paramref name="right"/> are not equal.</returns>
    public static bool operator !=(TextureView3D<T> left, TextureView3D<T> right) => !(left == right);
}