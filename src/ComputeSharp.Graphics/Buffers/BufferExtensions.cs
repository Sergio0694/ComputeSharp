using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using SharpDX.Direct3D12;

namespace ComputeSharp.Graphics.Buffers
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
        /// <returns>A zeroed read write <see cref="Buffer2{T}"/> instance of size <paramref name="size"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Buffer2<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, int size) where T : unmanaged
        {
            return new Buffer2<T>(device, size, HeapType.Default);
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer</param>
        /// <returns>A read write <see cref="Buffer2{T}"/> instance with the contents of the input <see cref="Span{T}"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Buffer2<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, Span<T> span) where T : unmanaged
        {
            Buffer2<T> buffer = new Buffer2<T>(device, span.Length, HeapType.Default);
            buffer.SetData(span);

            return buffer;
        }

        /// <summary>
        /// Allocates a new constant buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer</param>
        /// <returns>A constant <see cref="Buffer2{T}"/> instance with the contents of the input <see cref="Span{T}"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Buffer2<T> AllocateConstantBuffer<T>(this GraphicsDevice device, Span<T> span) where T : unmanaged
        {
            Buffer2<T> buffer = new Buffer2<T>(device, span.Length, HeapType.Upload);
            buffer.SetData(span);

            return buffer;
        }
    }
}
