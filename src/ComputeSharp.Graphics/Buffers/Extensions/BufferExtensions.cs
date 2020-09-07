using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using Microsoft.Toolkit.HighPerformance.Extensions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> with extension methods for the <see cref="GraphicsDevice"/> type to allocate buffers
    /// </summary>
    public static class BufferExtensions
    {
        /// <summary>
        /// Allocates a new constant buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer</param>
        /// <returns>A constant <see cref="ConstantBuffer{T}"/> instance with the contents of the input array</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, T[] array) where T : unmanaged
        {
            return device.AllocateConstantBuffer(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new constant buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="array">The input 2D <typeparamref name="T"/> array with the data to copy on the allocated buffer</param>
        /// <returns>A constant <see cref="ConstantBuffer{T}"/> instance with the contents of the input 2D array</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, T[,] array) where T : unmanaged
        {
            return device.AllocateConstantBuffer(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new constant buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer</param>
        /// <returns>A constant <see cref="ConstantBuffer{T}"/> instance with the contents of the input <see cref="Span{T}"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, Span<T> span) where T : unmanaged
        {
            ConstantBuffer<T> buffer = new ConstantBuffer<T>(device, span.Length);
            buffer.SetData(span);

            return buffer;
        }

        /// <summary>
        /// Allocates a new constant buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="buffer">The input <see cref="HlslBuffer{T}"/> with the data to copy on the allocated buffer</param>
        /// <returns>A constant <see cref="ConstantBuffer{T}"/> instance with the contents of the input <see cref="HlslBuffer{T}"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, HlslBuffer<T> buffer) where T : unmanaged
        {
            ConstantBuffer<T> constantBuffer = new ConstantBuffer<T>(device, buffer.Size);
            constantBuffer.SetData(buffer);

            return constantBuffer;
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="size">The size of the buffer to allocate</param>
        /// <returns>A zeroed <see cref="ReadOnlyBuffer{T}"/> instance of size <paramref name="size"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, int size) where T : unmanaged
        {
            return new ReadOnlyBuffer<T>(device, size);
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer</param>
        /// <returns>A read write <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input array</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, T[] array) where T : unmanaged
        {
            return device.AllocateReadOnlyBuffer(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="array">The input 2D <typeparamref name="T"/> array with the data to copy on the allocated buffer</param>
        /// <returns>A read write <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input 2D array</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, T[,] array) where T : unmanaged
        {
            return device.AllocateReadOnlyBuffer(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer</param>
        /// <returns>A read write <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input <see cref="Span{T}"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, Span<T> span) where T : unmanaged
        {
            ReadOnlyBuffer<T> buffer = new ReadOnlyBuffer<T>(device, span.Length);
            buffer.SetData(span);

            return buffer;
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="buffer">The input <see cref="HlslBuffer{T}"/> with the data to copy on the allocated buffer</param>
        /// <returns>A read write <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input <see cref="HlslBuffer{T}"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, HlslBuffer<T> buffer) where T : unmanaged
        {
            ReadOnlyBuffer<T> readWriteBuffer = new ReadOnlyBuffer<T>(device, buffer.Size);
            readWriteBuffer.SetData(buffer);

            return readWriteBuffer;
        }

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
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer</param>
        /// <returns>A read write <see cref="ReadWriteBuffer{T}"/> instance with the contents of the input array</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, T[] array) where T : unmanaged
        {
            return device.AllocateReadWriteBuffer(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="array">The input 2D <typeparamref name="T"/> array with the data to copy on the allocated buffer</param>
        /// <returns>A read write <see cref="ReadWriteBuffer{T}"/> instance with the contents of the input 2D array</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, T[,] array) where T : unmanaged
        {
            return device.AllocateReadWriteBuffer(array.AsSpan());
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
        /// Allocates a new read write buffer with the specified parameters
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="buffer">The input <see cref="HlslBuffer{T}"/> with the data to copy on the allocated buffer</param>
        /// <returns>A read write <see cref="ReadWriteBuffer{T}"/> instance with the contents of the input <see cref="HlslBuffer{T}"/></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, HlslBuffer<T> buffer) where T : unmanaged
        {
            ReadWriteBuffer<T> readWriteBuffer = new ReadWriteBuffer<T>(device, buffer.Size);
            readWriteBuffer.SetData(buffer);

            return readWriteBuffer;
        }
    }
}
