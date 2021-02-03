using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Resources.Views;

#pragma warning disable CS0809

namespace ComputeSharp
{
    /// <summary>
    /// The <see cref="TextureView2D{T}"/> type represents a view over a 2D texture allocated on host memory, providing APIs to
    /// easily manipulate its contents without having to manually deal with the internal alignment requirements of textures.
    /// As such, one key difference with <see cref="Span{T}"/> and arrays is that the underlying buffer for a <see cref="TextureView2D{T}"/>
    /// instance might not be contiguous in memory. All this logic is handled internally by the <see cref="TextureView2D{T}"/>
    /// type and it is transparent to the user, but note that working over discontiguous buffers has a performance impact.
    /// It is recommended to execute as much computation on the GPU side as possible.
    /// </summary>
    /// <typeparam name="T">The type of items in the current <see cref="TextureView2D{T}"/> instance.</typeparam>
    public readonly unsafe ref partial struct TextureView2D<T>
        where T : unmanaged
    {
        // Let's consider a representation of a 2D texture or a generic 2D memory region.
        // The data is represented in row-major order as usual, and the 'XX' grid cells
        // represent locations that are mapped by a given TextureView2D<T> instance.
        // An important aspec to note is that the pitch of each row is calculated in
        // bytes, as the texture layout alignment is unrelated to the logical type in use.
        // Assuming we a stride that's greater than the logical rows, we might have:
        //
        //                _____________________stride_____...
        //    pointer__  /________width_________
        //             \/                       \
        // | -- | -- | |- | -- | -- | -- | -- | -- | -- | -- |_
        // | -- | -- | XX | XX | XX | XX | XX | XX | -- | -- | |
        // | -- | -- | XX | XX | XX | XX | XX | XX | -- | -- | |
        // | -- | -- | XX | XX | XX | XX | XX | XX | -- | -- | |_height
        // | -- | -- | XX | XX | XX | XX | XX | XX | -- | -- |_|
        // | -- | -- | -- | -- | -- | -- | -- | -- | -- | -- |
        // | -- | -- | -- | -- | -- | -- | -- | -- | -- | -- |
        // ..._stride__/
        //
        // The stride is used to calculate the offset between each discontiguous row, so
        // that any arbitrary memory locations can be used to internally represent a 2D view.
        // This gives users much more flexibility when creating spans from data.

        /// <summary>
        /// The pointer to the first element of the target 2D region.
        /// </summary>
        private readonly T* pointer;

        /// <summary>
        /// The width of the specified 2D texture.
        /// </summary>
        private readonly int width;

        /// <summary>
        /// The height of the specified 2D region.
        /// </summary>
        private readonly int height;

        /// <summary>
        /// The stride of the specified 2D region.
        /// </summary>
        private readonly int strideInBytes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureView2D{T}"/> struct with the specified parameters.
        /// </summary>
        /// <param name="pointer">The pointer to the start of the memory area to map.</param>
        /// <param name="width">The width of the 2D memory area to map.</param>
        /// <param name="height">The height of the 2D memory area to map.</param>
        /// <param name="strideInBytes">The stride in bytes of the 2D memory area to map.</param>
        internal TextureView2D(T* pointer, int width, int height, int strideInBytes)
        {
            this.pointer = pointer;
            this.height = height;
            this.width = width;
            this.strideInBytes = strideInBytes;
        }

        /// <summary>
        /// Gets an empty <see cref="TextureView2D{T}"/> instance.
        /// </summary>
        public static TextureView2D<T> Empty => default;

        /// <summary>
        /// Gets a value indicating whether the current <see cref="TextureView2D{T}"/> instance is empty.
        /// </summary>
        public bool IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.width == 0;
        }

        /// <summary>
        /// Gets the length of the current <see cref="TextureView2D{T}"/> instance.
        /// </summary>
        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.width * this.height;
        }

        /// <summary>
        /// Gets the width of the underlying 2D memory area.
        /// </summary>
        public int Width
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.width;
        }

        /// <summary>
        /// Gets the height of the underlying 2D memory area.
        /// </summary>
        public int Height
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.height;
        }

        /// <summary>
        /// Gets the element at the specified zero-based indices.
        /// </summary>
        /// <param name="row">The target row to get the element from.</param>
        /// <param name="column">The target column to get the element from.</param>
        /// <returns>A reference to the element at the specified indices.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when either <paramref name="row"/> or <paramref name="column"/> are invalid.
        /// </exception>
        public ref T this[int row, int column]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if ((uint)row >= (uint)Height ||
                    (uint)column >= (uint)this.width)
                {
                    ThrowHelper.ThrowIndexOutOfRangeException();
                }

                return ref *(((T*)((byte*)this.pointer + (row * this.strideInBytes))) + column);
            }
        }

        /// <summary>
        /// Gets the element at the specified zero-based indices.
        /// </summary>
        /// <param name="row">The target row to get the element from.</param>
        /// <param name="column">The target column to get the element from.</param>
        /// <returns>A reference to the element at the specified indices.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when either <paramref name="row"/> or <paramref name="column"/> are invalid.
        /// </exception>
        public ref T this[Index row, Index column]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref this[row.GetOffset(Height), column.GetOffset(this.width)];
        }

        /// <summary>
        /// Slices the current instance with the specified parameters.
        /// </summary>
        /// <param name="rows">The target range of rows to select.</param>
        /// <param name="columns">The target range of columns to select.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when either <paramref name="rows"/> or <paramref name="columns"/> are invalid.
        /// </exception>
        /// <returns>A new <see cref="TextureView2D{T}"/> instance representing a slice of the current one.</returns>
        public TextureView2D<T> this[Range rows, Range columns]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var (row, height) = rows.GetOffsetAndLength(Height);
                var (column, width) = columns.GetOffsetAndLength(this.width);

                return Slice(row, column, height, width);
            }
        }

        /// <summary>
        /// Clears the contents of the current <see cref="TextureView2D{T}"/> instance.
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
                for (int i = 0; i < Height; i++)
                {
                    GetRowSpan(i).Clear();
                }
            }
        }

        /// <summary>
        /// Copies the contents of this <see cref="TextureView2D{T}"/> into a destination <see cref="Span{T}"/> instance.
        /// </summary>
        /// <param name="destination">The destination <see cref="Span{T}"/> instance.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="destination" /> is shorter than the source <see cref="TextureView2D{T}"/> instance.
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
                if (Length > destination.Length)
                {
                    ThrowHelper.ThrowArgumentExceptionForDestinationTooShort();
                }

                for (int i = 0, j = 0; i < Height; i++, j += this.width)
                {
                    GetRowSpan(i).CopyTo(destination.Slice(j));
                }
            }
        }

        /// <summary>
        /// Copies the contents of this <see cref="TextureView2D{T}"/> into a destination <see cref="TextureView2D{T}"/> instance.
        /// For this API to succeed, the target <see cref="TextureView2D{T}"/> has to have the same shape as the current one.
        /// </summary>
        /// <param name="destination">The destination <see cref="TextureView2D{T}"/> instance.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="destination" /> is shorter than the source <see cref="TextureView2D{T}"/> instance.
        /// </exception>
        public void CopyTo(TextureView2D<T> destination)
        {
            if (destination.Height != Height ||
                destination.width != this.width)
            {
                ThrowHelper.ThrowArgumentException();
            }

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
                for (int i = 0; i < Height; i++)
                {
                    GetRowSpan(i).CopyTo(destination.GetRowSpan(i));
                }
            }
        }

        /// <summary>
        /// Attempts to copy the current <see cref="TextureView2D{T}"/> instance to a destination <see cref="Span{T}"/>.
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
        /// Attempts to copy the current <see cref="TextureView2D{T}"/> instance to a destination <see cref="TextureView2D{T}"/>.
        /// </summary>
        /// <param name="destination">The target <see cref="TextureView2D{T}"/> of the copy operation.</param>
        /// <returns>Whether or not the operation was successful.</returns>
        public bool TryCopyTo(TextureView2D<T> destination)
        {
            if (destination.Height == Height &&
                destination.Width == this.width)
            {
                CopyTo(destination);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Fills the elements of this span with a specified value.
        /// </summary>
        /// <param name="value">The value to assign to each element of the <see cref="TextureView2D{T}"/> instance.</param>
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
                // Fill one row at a time
                for (int i = 0; i < Height; i++)
                {
                    GetRowSpan(i).Fill(value);
                }
            }
        }

        /// <summary>
        /// Returns a reference to the first element within the current instance, with no bounds check.
        /// </summary>
        /// <returns>A reference to the first element within the current instance.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T* DangerousGetAddressAndByteStride(out int strideInBytes)
        {
            strideInBytes = this.strideInBytes;

            return this.pointer;
        }

        /// <summary>
        /// Slices the current instance with the specified parameters.
        /// </summary>
        /// <param name="row">The target row to map within the current instance.</param>
        /// <param name="column">The target column to map within the current instance.</param>
        /// <param name="height">The height to map within the current instance.</param>
        /// <param name="width">The width to map within the current instance.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when either <paramref name="height"/>, <paramref name="width"/> or <paramref name="height"/>
        /// are negative or not within the bounds that are valid for the current instance.
        /// </exception>
        /// <returns>A new <see cref="TextureView2D{T}"/> instance representing a slice of the current one.</returns>
        [Pure]
        public TextureView2D<T> Slice(int row, int column, int height, int width)
        {
            if ((uint)row >= Height)
            {
                ThrowHelper.ThrowArgumentOutOfRangeExceptionForRow();
            }

            if ((uint)column >= this.width)
            {
                ThrowHelper.ThrowArgumentOutOfRangeExceptionForColumn();
            }

            if ((uint)height > (Height - row))
            {
                ThrowHelper.ThrowArgumentOutOfRangeExceptionForHeight();
            }

            if ((uint)width > (this.width - column))
            {
                ThrowHelper.ThrowArgumentOutOfRangeExceptionForWidth();
            }

            T* pointer = (T*)((byte*)this.pointer + (row * this.strideInBytes)) + column;

            return new(pointer, width, height, this.strideInBytes);
        }

        /// <summary>
        /// Gets a <see cref="Span{T}"/> for a specified row.
        /// </summary>
        /// <param name="row">The index of the target row to retrieve.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when <paramref name="row"/> is out of range.</exception>
        /// <returns>The resulting row <see cref="Span{T}"/>.</returns>
        [Pure]
        public Span<T> GetRowSpan(int row)
        {
            if ((uint)row >= (uint)Height)
            {
                ThrowHelper.ThrowArgumentOutOfRangeExceptionForRow();
            }

            return new((byte*)this.pointer + (row * this.strideInBytes), this.width);
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
        /// Copies the contents of the current <see cref="TextureView2D{T}"/> instance into a new 2D array.
        /// </summary>
        /// <returns>A 2D array containing the data in the current <see cref="TextureView2D{T}"/> instance.</returns>
        [Pure]
        public T[,] ToArray()
        {
            T[,] array = new T[this.height, this.width];

            fixed (T* pointer = array)
            {
                CopyTo(new Span<T>(pointer, Length));
            }

            return array;
        }

        /// <inheritdoc cref="Span{T}.Equals(object)"/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
        public override bool Equals(object? obj)
        {
            throw new NotSupportedException("ComputeSharp.TextureView2D<T>.Equals(object) is not supported");
        }

        /// <inheritdoc cref="Span{T}.GetHashCode()"/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("GetHashCode() on Span will always throw an exception.")]
        public override int GetHashCode()
        {
            throw new NotSupportedException("ComputeSharp.TextureView2D<T>.GetHashCode() is not supported");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"ComputeSharp.TextureView2D<{typeof(T)}>[{Width}, {Height}]";
        }

        /// <summary>
        /// Checks whether two <see cref="TextureView2D{T}"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="TextureView2D{T}"/> instance to compare.</param>
        /// <param name="right">The second <see cref="TextureView2D{T}"/> instance to compare.</param>
        /// <returns>Whether or not <paramref name="left"/> and <paramref name="right"/> are equal.</returns>
        public static bool operator ==(TextureView2D<T> left, TextureView2D<T> right)
        {
            return
                left.pointer == right.pointer &&
                left.width == right.width &&
                left.height == right.height;
        }

        /// <summary>
        /// Checks whether two <see cref="TextureView2D{T}"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="TextureView2D{T}"/> instance to compare.</param>
        /// <param name="right">The second <see cref="TextureView2D{T}"/> instance to compare.</param>
        /// <returns>Whether or not <paramref name="left"/> and <paramref name="right"/> are not equal.</returns>
        public static bool operator !=(TextureView2D<T> left, TextureView2D<T> right)
        {
            return !(left == right);
        }
    }
}
