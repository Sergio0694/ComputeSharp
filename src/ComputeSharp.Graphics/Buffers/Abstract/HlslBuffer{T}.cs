using System;
using System.Buffers;
using System.Diagnostics.Contracts;
using ComputeSharp.Graphics.Buffers.Enums;

namespace ComputeSharp.Graphics.Buffers.Abstract
{
    /// <summary>
    /// A <see langword="class"/> representing a typed buffer stored on GPU memory, that can be used from an HLSL shader
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer</typeparam>
    public abstract class HlslBuffer<T> : Buffer<T> where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="HlslBuffer{T}"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance</param>
        /// <param name="size">The number of items to store in the current buffer</param>
        /// <param name="sizeInBytes">The size in bytes for the current buffer</param>
        /// <param name="bufferType">The buffer type for the current buffer</param>
        private protected HlslBuffer(GraphicsDevice device, int size, int sizeInBytes, BufferType bufferType) : base(device, size, sizeInBytes, bufferType) { }

        /// <summary>
        /// Reads the contents of the current <see cref="HlslBuffer{T}"/> instance and returns an array
        /// </summary>
        /// <returns>A <typeparamref name="T"/> array with the contents of the current buffer</returns>
        [Pure]
        public T[] GetData() => GetData(0, Size);

        /// <summary>
        /// Reads the contents of the current <see cref="HlslBuffer{T}"/> instance in a given range and returns an array
        /// </summary>
        /// <param name="offset">The offset to start reading data from</param>
        /// <param name="count">The number of items to read</param>
        /// <returns>A <typeparamref name="T"/> array with the contents of the specified range from the current buffer</returns>
        [Pure]
        public T[] GetData(int offset, int count)
        {
            T[] data = new T[count];
            GetData(data, offset, count);

            return data;
        }

        /// <summary>
        /// Reads the contents of the current <see cref="HlslBuffer{T}"/> instance and writes them into a target <see cref="Span{T}"/>
        /// </summary>
        /// <param name="span">The input <see cref="Span{T}"/> to write data to</param>
        public void GetData(Span<T> span) => GetData(span, 0, Size);

        /// <summary>
        /// Reads the contents of the specified range from the current <see cref="HlslBuffer{T}"/> instance and writes them into a target <see cref="Span{T}"/>
        /// </summary>
        /// <param name="span">The input <see cref="Span{T}"/> to write data to</param>
        /// <param name="offset">The offset to start reading data from</param>
        /// <param name="count">The number of items to read</param>
        public abstract void GetData(Span<T> span, int offset, int count);

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to the current <see cref="HlslBuffer{T}"/> instance
        /// </summary>
        /// <param name="array">The input <typeparamref name="T"/> array to read data from</param>
        public void SetData(T[] array) => SetData(array.AsSpan());

        /// <summary>
        /// Writes the contents of a given <typeparamref name="T"/> array to a specified area of the current <see cref="HlslBuffer{T}"/> instance
        /// </summary>
        /// <param name="array">The input <typeparamref name="T"/> array to read data from</param>
        /// <param name="offset">The offset to start writing data to</param>
        /// <param name="count">The number of items to write</param>
        public void SetData(T[] array, int offset, int count) => SetData(array.AsSpan(), offset, count);

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to the current <see cref="HlslBuffer{T}"/> instance
        /// </summary>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> to read data from</param>
        public void SetData(ReadOnlySpan<T> span) => SetData(span, 0, Size);

        /// <summary>
        /// Writes the contents of a given <see cref="ReadOnlySpan{T}"/> to a specified area of the current <see cref="HlslBuffer{T}"/> instance
        /// </summary>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> to read data from</param>
        /// <param name="offset">The offset to start writing data to</param>
        /// <param name="count">The number of items to write</param>
        public abstract void SetData(ReadOnlySpan<T> span, int offset, int count);

        /// <summary>
        /// Writes the contents of a given <see cref="HlslBuffer{T}"/> to the current <see cref="HlslBuffer{T}"/> instance
        /// </summary>
        /// <param name="buffer">The input <see cref="HlslBuffer{T}"/> to read data from</param>
        public abstract void SetData(HlslBuffer<T> buffer);

        /// <summary>
        /// Writes the contents of a given <see cref="HlslBuffer{T}"/> to the current <see cref="HlslBuffer{T}"/> instance, using a temporary CPU buffer
        /// </summary>
        /// <param name="buffer">The input <see cref="HlslBuffer{T}"/> to read data from</param>
        protected void SetDataWithCpuBuffer(HlslBuffer<T> buffer)
        {
            // Create a temporary array
            T[] array = ArrayPool<T>.Shared.Rent(buffer.Size);
            Span<T> span = array.AsSpan(0, buffer.Size);

            // Get the unpadded data from the read write buffer
            buffer.GetData(span);

            // Set the data, adding the padding if needed
            SetData(span);

            ArrayPool<T>.Shared.Return(array);
        }
    }
}
