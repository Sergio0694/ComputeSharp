using System;
using System.Diagnostics.Contracts;
using SharpDX.Direct3D12;

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
        /// <param name="heapType">The heap type for the current buffer</param>
        protected internal HlslBuffer(GraphicsDevice device, int size, int sizeInBytes, HeapType heapType) : base(device, size, sizeInBytes, heapType) { }

        /// <summary>
        /// Reads the contents of the current <see cref="HlslBuffer{T}"/> instance and returns an array
        /// </summary>
        /// <returns>A <typeparamref name="T"/> array with the contents of the current buffer</returns>
        [Pure]
        public T[] GetData()
        {
            T[] data = new T[Size];
            GetData(data);

            return data;
        }

        /// <summary>
        /// Reads the contents of the current <see cref="HlslBuffer{T}"/> instance and writes them into a target <see cref="Span{T}"/>
        /// </summary>
        /// <param name="span">The input <see cref="Span{T}"/> to write data to</param>
        public abstract void GetData(Span<T> span);

        /// <summary>
        /// Writes the contents of a given <see cref="Span{T}"/> to the current <see cref="HlslBuffer{T}"/> instance
        /// </summary>
        /// <param name="span">The input <see cref="Span{T}"/> to read data from</param>
        public abstract void SetData(Span<T> span);
    }
}
