using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Graphics.Buffers.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with extension methods for the <see cref="GraphicsDevice"/> type to allocate buffers
    /// </summary>
    public static class BufferExtensions
    {
        /// <summary>
        /// Allocates a new read write buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="size">The size of the buffer to allocate</param>
        /// <returns>A zeroed <see cref="ReadWriteBuffer{T}"/> instance of size <paramref name="size"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, int size) where T : unmanaged
        {
            return new ReadWriteBuffer<T>(device, size);
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer</param>
        /// <returns>A read write <see cref="ReadWriteBuffer{T}"/> instance with the contents of the input <see cref="Span{T}"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, Span<T> span) where T : unmanaged
        {
            ReadWriteBuffer<T> buffer = new ReadWriteBuffer<T>(device, span.Length);
            buffer.SetData(span);

            return buffer;
        }

        /// <summary>
        /// Allocates a new constant buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer</param>
        /// <returns>A constant <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input <see cref="Span{T}"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, Span<T> span) where T : unmanaged
        {
            ReadOnlyBuffer<T> buffer = new ReadOnlyBuffer<T>(device, span.Length);
            buffer.SetData(span);

            return buffer;
        }
    }
}
