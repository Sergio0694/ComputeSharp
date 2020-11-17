using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> with extension methods for the <see cref="GraphicsDevice2"/> type to allocate buffers.
    /// </summary>
    public static class BufferExtensions2
    {
        /// <summary>
        /// Allocates a new constant buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice2"/> instance to use to allocate the buffer.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer.</param>
        /// <returns>A constant <see cref="ConstantBuffer2{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstantBuffer2<T> AllocateConstantBuffer<T>(this GraphicsDevice2 device, T[] array)
            where T : unmanaged
        {
            return device.AllocateConstantBuffer(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new constant buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice2"/> instance to use to allocate the buffer.</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A constant <see cref="ConstantBuffer2{T}"/> instance with the contents of the input <see cref="Span{T}"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstantBuffer2<T> AllocateConstantBuffer<T>(this GraphicsDevice2 device, Span<T> span)
            where T : unmanaged
        {
            ConstantBuffer2<T> buffer = new(device, span.Length);

            buffer.SetData(span);

            return buffer;
        }

        /// <summary>
        /// Allocates a new constant buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice2"/> instance to use to allocate the buffer.</param>
        /// <param name="buffer">The input <see cref="HlslBuffer2{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A constant <see cref="ConstantBuffer2{T}"/> instance with the contents of the input <see cref="HlslBuffer2{T}"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstantBuffer2<T> AllocateConstantBuffer<T>(this GraphicsDevice2 device, HlslBuffer2<T> buffer)
            where T : unmanaged
        {
            ConstantBuffer2<T> constantBuffer = new(device, buffer.Size);

            constantBuffer.SetData(buffer);

            return constantBuffer;
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice2"/> instance to use to allocate the buffer.</param>
        /// <param name="size">The size of the buffer to allocate.</param>
        /// <returns>A zeroed <see cref="ReadOnlyBuffer2{T}"/> instance of size <paramref name="size"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyBuffer2<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice2 device, int size)
            where T : unmanaged
        {
            return new(device, size);
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice2"/> instance to use to allocate the buffer.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadOnlyBuffer2{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyBuffer2<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice2 device, T[] array)
            where T : unmanaged
        {
            return device.AllocateReadOnlyBuffer(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice2"/> instance to use to allocate the buffer.</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadOnlyBuffer2{T}"/> instance with the contents of the input <see cref="Span{T}"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyBuffer2<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice2 device, Span<T> span)
            where T : unmanaged
        {
            ReadOnlyBuffer2<T> buffer = new(device, span.Length);

            buffer.SetData(span);

            return buffer;
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice2"/> instance to use to allocate the buffer.</param>
        /// <param name="buffer">The input <see cref="HlslBuffer2{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadOnlyBuffer2{T}"/> instance with the contents of the input <see cref="HlslBuffer2{T}"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyBuffer2<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice2 device, HlslBuffer2<T> buffer)
            where T : unmanaged
        {
            ReadOnlyBuffer2<T> readWriteBuffer = new(device, buffer.Size);

            readWriteBuffer.SetData(buffer);

            return readWriteBuffer;
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice2"/> instance to use to allocate the buffer.</param>
        /// <param name="size">The size of the buffer to allocate.</param>
        /// <returns>A zeroed <see cref="ReadWriteBuffer2{T}"/> instance of size <paramref name="size"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadWriteBuffer2<T> AllocateReadWriteBuffer<T>(this GraphicsDevice2 device, int size)
            where T : unmanaged
        {
            return new(device, size);
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice2"/> instance to use to allocate the buffer.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadWriteBuffer2{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadWriteBuffer2<T> AllocateReadWriteBuffer<T>(this GraphicsDevice2 device, T[] array)
            where T : unmanaged
        {
            return device.AllocateReadWriteBuffer(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice2"/> instance to use to allocate the buffer.</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadWriteBuffer2{T}"/> instance with the contents of the input <see cref="Span{T}"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadWriteBuffer2<T> AllocateReadWriteBuffer<T>(this GraphicsDevice2 device, Span<T> span)
            where T : unmanaged
        {
            ReadWriteBuffer2<T> buffer = new(device, span.Length);

            buffer.SetData(span);

            return buffer;
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice2"/> instance to use to allocate the buffer.</param>
        /// <param name="buffer">The input <see cref="HlslBuffer{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadWriteBuffer2{T}"/> instance with the contents of the input <see cref="HlslBuffer2{T}"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadWriteBuffer2<T> AllocateReadWriteBuffer<T>(this GraphicsDevice2 device, HlslBuffer2<T> buffer)
            where T : unmanaged
        {
            ReadWriteBuffer2<T> readWriteBuffer = new (device, buffer.Size);

            readWriteBuffer.SetData(buffer);

            return readWriteBuffer;
        }
    }
}
