using System;
using System.Diagnostics.Contracts;
using ComputeSharp.__Internals;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Shaders;

#pragma warning disable CS0618

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that contains extension methods for the <see cref="GraphicsDevice"/> type, used to run compute shaders.
    /// </summary>
    public static class GraphicsDeviceExtensions
    {
        /// <summary>
        /// Allocates a new constant buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="length">The length of the buffer to allocate.</param>
        /// <returns>A zeroed <see cref="ConstantBuffer{T}"/> instance of size <paramref name="length"/>.</returns>
        [Pure]
        public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, int length)
            where T : unmanaged
        {
            return new(device, length);
        }

        /// <summary>
        /// Allocates a new constant buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer.</param>
        /// <returns>A constant <see cref="ConstantBuffer{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, T[] array)
            where T : unmanaged
        {
            return device.AllocateConstantBuffer<T>(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new constant buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A constant <see cref="ConstantBuffer{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, ReadOnlySpan<T> span)
            where T : unmanaged
        {
            ConstantBuffer<T> buffer = new(device, span.Length);

            buffer.SetData(span);

            return buffer;
        }

        /// <summary>
        /// Allocates a new constant buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A constant <see cref="ConstantBuffer{T}"/> instance with the contents of the input <see cref="Buffer{T}"/>.</returns>
        [Pure]
        public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, Buffer<T> buffer)
            where T : unmanaged
        {
            ConstantBuffer<T> constantBuffer = new(device, buffer.Length);

            constantBuffer.SetData(buffer);

            return constantBuffer;
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="length">The length of the buffer to allocate.</param>
        /// <returns>A zeroed <see cref="ReadOnlyBuffer{T}"/> instance of size <paramref name="length"/>.</returns>
        [Pure]
        public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, int length)
            where T : unmanaged
        {
            return new(device, length);
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, T[] array)
            where T : unmanaged
        {
            return device.AllocateReadOnlyBuffer<T>(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, ReadOnlySpan<T> span)
            where T : unmanaged
        {
            ReadOnlyBuffer<T> buffer = new(device, span.Length);

            buffer.SetData(span);

            return buffer;
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input <see cref="Buffer{T}"/>.</returns>
        [Pure]
        public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, Buffer<T> buffer)
            where T : unmanaged
        {
            ReadOnlyBuffer<T> readWriteBuffer = new(device, buffer.Length);

            readWriteBuffer.SetData(buffer);

            return readWriteBuffer;
        }

        /// <summary>
        /// Allocates a new readonly 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A zeroed <see cref="ReadOnlyTexture2D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
        [Pure]
        public static ReadOnlyTexture2D<T> AllocateReadOnlyTexture2D<T>(this GraphicsDevice device, int width, int height)
            where T : unmanaged
        {
            return new(device, width, height);
        }

        /// <summary>
        /// Allocates a new readonly 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture2D{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadOnlyTexture2D<T> AllocateReadOnlyTexture2D<T>(this GraphicsDevice device, T[] array, int width, int height)
            where T : unmanaged
        {
            return device.AllocateReadOnlyTexture2D<T>(array.AsSpan(), width, height);
        }

        /// <summary>
        /// Allocates a new readonly 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture2D{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadOnlyTexture2D<T> AllocateReadOnlyTexture2D<T>(this GraphicsDevice device, T[] array, int offset, int width, int height)
            where T : unmanaged
        {
            return device.AllocateReadOnlyTexture2D<T>(array.AsSpan(offset), width, height);
        }

        /// <summary>
        /// Allocates a new readonly 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <returns>A read write <see cref="ReadOnlyTexture2D{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadOnlyTexture2D<T> AllocateReadOnlyTexture2D<T>(this GraphicsDevice device, T[,] array)
            where T : unmanaged
        {
            ReadOnlyTexture2D<T> texture = new(device, array.GetLength(1), array.GetLength(0));

            texture.SetData(array);

            return texture;
        }

        /// <summary>
        /// Allocates a new readonly 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture2D{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadOnlyTexture2D<T> AllocateReadOnlyTexture2D<T>(this GraphicsDevice device, ReadOnlySpan<T> span, int width, int height)
            where T : unmanaged
        {
            ReadOnlyTexture2D<T> texture = new(device, width, height);

            texture.SetData(span);

            return texture;
        }

        /// <summary>
        /// Allocates a new readonly 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A zeroed <see cref="ReadOnlyTexture2D{T,TPixel}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
        [Pure]
        public static ReadOnlyTexture2D<T, TPixel> AllocateReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, int width, int height)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return new(device, width, height);
        }

        /// <summary>
        /// Allocates a new readonly 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture2D{T,TPixel}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadOnlyTexture2D<T, TPixel> AllocateReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, T[] array, int width, int height)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return device.AllocateReadOnlyTexture2D<T, TPixel>(array.AsSpan(), width, height);
        }

        /// <summary>
        /// Allocates a new readonly 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture2D{T,TPixel}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadOnlyTexture2D<T, TPixel> AllocateReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, T[] array, int offset, int width, int height)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return device.AllocateReadOnlyTexture2D<T, TPixel>(array.AsSpan(offset), width, height);
        }

        /// <summary>
        /// Allocates a new readonly 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <returns>A read write <see cref="ReadOnlyTexture2D{T,TPixel}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadOnlyTexture2D<T, TPixel> AllocateReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, T[,] array)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            ReadOnlyTexture2D<T, TPixel> texture = new(device, array.GetLength(1), array.GetLength(0));

            texture.SetData(array);

            return texture;
        }

        /// <summary>
        /// Allocates a new readonly 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture2D{T,TPixel}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadOnlyTexture2D<T, TPixel> AllocateReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<T> span, int width, int height)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            ReadOnlyTexture2D<T, TPixel> texture = new(device, width, height);

            texture.SetData(span);

            return texture;
        }

        /// <summary>
        /// Allocates a new readonly 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A zeroed <see cref="ReadOnlyTexture3D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>, <paramref name="depth"/>].</returns>
        [Pure]
        public static ReadOnlyTexture3D<T> AllocateReadOnlyTexture3D<T>(this GraphicsDevice device, int width, int height, int depth)
            where T : unmanaged
        {
            return new(device, width, height, depth);
        }

        /// <summary>
        /// Allocates a new readonly 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture3D{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadOnlyTexture3D<T> AllocateReadOnlyTexture3D<T>(this GraphicsDevice device, T[] array, int width, int height, int depth)
            where T : unmanaged
        {
            return device.AllocateReadOnlyTexture3D<T>(array.AsSpan(), width, height, depth);
        }

        /// <summary>
        /// Allocates a new readonly 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture3D{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadOnlyTexture3D<T> AllocateReadOnlyTexture3D<T>(this GraphicsDevice device, T[] array, int offset, int width, int height, int depth)
            where T : unmanaged
        {
            return device.AllocateReadOnlyTexture3D<T>(array.AsSpan(offset), width, height, depth);
        }

        /// <summary>
        /// Allocates a new readonly 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <returns>A read write <see cref="ReadOnlyTexture3D{T}"/> instance with the contents of the input array.</returns>
        /// <remarks>
        /// The source 3D array needs to have each 2D plane stacked on the depth axis.
        /// That is, the expected layout of the input array has to be [depth, height, width].
        /// </remarks>
        [Pure]
        public static ReadOnlyTexture3D<T> AllocateReadOnlyTexture3D<T>(this GraphicsDevice device, T[,,] array)
            where T : unmanaged
        {
            ReadOnlyTexture3D<T> texture = new(device, array.GetLength(2), array.GetLength(1), array.GetLength(0));

            texture.SetData(array);

            return texture;
        }

        /// <summary>
        /// Allocates a new readonly 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture3D{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadOnlyTexture3D<T> AllocateReadOnlyTexture3D<T>(this GraphicsDevice device, ReadOnlySpan<T> span, int width, int height, int depth)
            where T : unmanaged
        {
            ReadOnlyTexture3D<T> texture = new(device, width, height, depth);

            texture.SetData(span);

            return texture;
        }

        /// <summary>
        /// Allocates a new readonly 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A zeroed <see cref="ReadOnlyTexture3D{T,TPixel}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>, <paramref name="depth"/>].</returns>
        [Pure]
        public static ReadOnlyTexture3D<T, TPixel> AllocateReadOnlyTexture3D<T, TPixel>(this GraphicsDevice device, int width, int height, int depth)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return new(device, width, height, depth);
        }

        /// <summary>
        /// Allocates a new readonly 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture3D{T,TPixel}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadOnlyTexture3D<T, TPixel> AllocateReadOnlyTexture3D<T, TPixel>(this GraphicsDevice device, T[] array, int width, int height, int depth)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return device.AllocateReadOnlyTexture3D<T, TPixel>(array.AsSpan(), width, height, depth);
        }

        /// <summary>
        /// Allocates a new readonly 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture3D{T,TPixel}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadOnlyTexture3D<T, TPixel> AllocateReadOnlyTexture3D<T, TPixel>(this GraphicsDevice device, T[] array, int offset, int width, int height, int depth)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return device.AllocateReadOnlyTexture3D<T, TPixel>(array.AsSpan(offset), width, height, depth);
        }

        /// <summary>
        /// Allocates a new readonly 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <returns>A read write <see cref="ReadOnlyTexture3D{T,TPixel}"/> instance with the contents of the input array.</returns>
        /// <remarks>
        /// The source 3D array needs to have each 2D plane stacked on the depth axis.
        /// That is, the expected layout of the input array has to be [depth, height, width].
        /// </remarks>
        [Pure]
        public static ReadOnlyTexture3D<T, TPixel> AllocateReadOnlyTexture3D<T, TPixel>(this GraphicsDevice device, T[,,] array)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            ReadOnlyTexture3D<T, TPixel> texture = new(device, array.GetLength(2), array.GetLength(1), array.GetLength(0));

            texture.SetData(array);

            return texture;
        }

        /// <summary>
        /// Allocates a new readonly 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadOnlyTexture3D{T,TPixel}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadOnlyTexture3D<T, TPixel> AllocateReadOnlyTexture3D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<T> span, int width, int height, int depth)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            ReadOnlyTexture3D<T, TPixel> texture = new(device, width, height, depth);

            texture.SetData(span);

            return texture;
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="length">The length of the buffer to allocate.</param>
        /// <returns>A zeroed <see cref="ReadWriteBuffer{T}"/> instance of size <paramref name="length"/>.</returns>
        [Pure]
        public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, int length)
            where T : unmanaged
        {
            return new(device, length);
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadWriteBuffer{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, T[] array)
            where T : unmanaged
        {
            return device.AllocateReadWriteBuffer<T>(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadWriteBuffer{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, ReadOnlySpan<T> span)
            where T : unmanaged
        {
            ReadWriteBuffer<T> buffer = new(device, span.Length);

            buffer.SetData(span);

            return buffer;
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadWriteBuffer{T}"/> instance with the contents of the input <see cref="Buffer{T}"/>.</returns>
        [Pure]
        public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, Buffer<T> buffer)
            where T : unmanaged
        {
            ReadWriteBuffer<T> readWriteBuffer = new(device, buffer.Length);

            readWriteBuffer.SetData(buffer);

            return readWriteBuffer;
        }

        /// <summary>
        /// Allocates a new writeable 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A zeroed <see cref="ReadWriteTexture2D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
        [Pure]
        public static ReadWriteTexture2D<T> AllocateReadWriteTexture2D<T>(this GraphicsDevice device, int width, int height)
            where T : unmanaged
        {
            return new(device, width, height);
        }

        /// <summary>
        /// Allocates a new writeable 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture2D{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadWriteTexture2D<T> AllocateReadWriteTexture2D<T>(this GraphicsDevice device, T[] array, int width, int height)
            where T : unmanaged
        {
            return device.AllocateReadWriteTexture2D<T>(array.AsSpan(), width, height);
        }

        /// <summary>
        /// Allocates a new writeable 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture2D{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadWriteTexture2D<T> AllocateReadWriteTexture2D<T>(this GraphicsDevice device, T[] array, int offset, int width, int height)
            where T : unmanaged
        {
            return device.AllocateReadWriteTexture2D<T>(array.AsSpan(offset), width, height);
        }

        /// <summary>
        /// Allocates a new writeable 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <returns>A read write <see cref="ReadWriteTexture2D{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadWriteTexture2D<T> AllocateReadWriteTexture2D<T>(this GraphicsDevice device, T[,] array)
            where T : unmanaged
        {
            ReadWriteTexture2D<T> texture = new(device, array.GetLength(1), array.GetLength(0));

            texture.SetData(array);

            return texture;
        }

        /// <summary>
        /// Allocates a new writeable 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture2D{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadWriteTexture2D<T> AllocateReadWriteTexture2D<T>(this GraphicsDevice device, ReadOnlySpan<T> span, int width, int height)
            where T : unmanaged
        {
            ReadWriteTexture2D<T> texture = new(device, width, height);

            texture.SetData(span);

            return texture;
        }

        /// <summary>
        /// Allocates a new writeable 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A zeroed <see cref="ReadWriteTexture2D{T,TPixel}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
        [Pure]
        public static ReadWriteTexture2D<T, TPixel> AllocateReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, int width, int height)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return new(device, width, height);
        }

        /// <summary>
        /// Allocates a new writeable 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture2D{T,TPixel}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadWriteTexture2D<T, TPixel> AllocateReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, T[] array, int width, int height)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return device.AllocateReadWriteTexture2D<T, TPixel>(array.AsSpan(), width, height);
        }

        /// <summary>
        /// Allocates a new writeable 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture2D{T,TPixel}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadWriteTexture2D<T, TPixel> AllocateReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, T[] array, int offset, int width, int height)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return device.AllocateReadWriteTexture2D<T, TPixel>(array.AsSpan(offset), width, height);
        }

        /// <summary>
        /// Allocates a new writeable 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <returns>A read write <see cref="ReadWriteTexture2D{T,TPixel}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadWriteTexture2D<T, TPixel> AllocateReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, T[,] array)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            ReadWriteTexture2D<T, TPixel> texture = new(device, array.GetLength(1), array.GetLength(0));

            texture.SetData(array);

            return texture;
        }

        /// <summary>
        /// Allocates a new writeable 2D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture2D{T,TPixel}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadWriteTexture2D<T, TPixel> AllocateReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<T> span, int width, int height)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            ReadWriteTexture2D<T, TPixel> texture = new(device, width, height);

            texture.SetData(span);

            return texture;
        }

        /// <summary>
        /// Allocates a new writeable 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A zeroed <see cref="ReadWriteTexture3D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
        [Pure]
        public static ReadWriteTexture3D<T> AllocateReadWriteTexture3D<T>(this GraphicsDevice device, int width, int height, int depth)
            where T : unmanaged
        {
            return new(device, width, height, depth);
        }

        /// <summary>
        /// Allocates a new writeable 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture3D{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadWriteTexture3D<T> AllocateReadWriteTexture3D<T>(this GraphicsDevice device, T[] array, int width, int height, int depth)
            where T : unmanaged
        {
            return device.AllocateReadWriteTexture3D<T>(array.AsSpan(), width, height, depth);
        }

        /// <summary>
        /// Allocates a new writeable 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture3D{T}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadWriteTexture3D<T> AllocateReadWriteTexture3D<T>(this GraphicsDevice device, T[] array, int offset, int width, int height, int depth)
            where T : unmanaged
        {
            return device.AllocateReadWriteTexture3D<T>(array.AsSpan(offset), width, height, depth);
        }

        /// <summary>
        /// Allocates a new writeable 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <returns>A read write <see cref="ReadWriteTexture3D{T}"/> instance with the contents of the input array.</returns>
        /// <remarks>
        /// The source 3D array needs to have each 2D plane stacked on the depth axis.
        /// That is, the expected layout of the input array has to be [depth, height, width].
        /// </remarks>
        [Pure]
        public static ReadWriteTexture3D<T> AllocateReadWriteTexture3D<T>(this GraphicsDevice device, T[,,] array)
            where T : unmanaged
        {
            ReadWriteTexture3D<T> texture = new(device, array.GetLength(2), array.GetLength(1), array.GetLength(0));

            texture.SetData(array);

            return texture;
        }

        /// <summary>
        /// Allocates a new writeable 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture3D{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadWriteTexture3D<T> AllocateReadWriteTexture3D<T>(this GraphicsDevice device, ReadOnlySpan<T> span, int width, int height, int depth)
            where T : unmanaged
        {
            ReadWriteTexture3D<T> texture = new(device, width, height, depth);

            texture.SetData(span);

            return texture;
        }

        /// <summary>
        /// Allocates a new writeable 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A zeroed <see cref="ReadWriteTexture3D{T,TPixel}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
        [Pure]
        public static ReadWriteTexture3D<T, TPixel> AllocateReadWriteTexture3D<T, TPixel>(this GraphicsDevice device, int width, int height, int depth)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return new(device, width, height, depth);
        }

        /// <summary>
        /// Allocates a new writeable 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture3D{T,TPixel}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadWriteTexture3D<T, TPixel> AllocateReadWriteTexture3D<T, TPixel>(this GraphicsDevice device, T[] array, int width, int height, int depth)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return device.AllocateReadWriteTexture3D<T, TPixel>(array.AsSpan(), width, height, depth);
        }

        /// <summary>
        /// Allocates a new writeable 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture3D{T,TPixel}"/> instance with the contents of the input array.</returns>
        [Pure]
        public static ReadWriteTexture3D<T, TPixel> AllocateReadWriteTexture3D<T, TPixel>(this GraphicsDevice device, T[] array, int offset, int width, int height, int depth)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return device.AllocateReadWriteTexture3D<T, TPixel>(array.AsSpan(offset), width, height, depth);
        }

        /// <summary>
        /// Allocates a new writeable 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="array">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
        /// <returns>A read write <see cref="ReadWriteTexture3D{T,TPixel}"/> instance with the contents of the input array.</returns>
        /// <remarks>
        /// The source 3D array needs to have each 2D plane stacked on the depth axis.
        /// That is, the expected layout of the input array has to be [depth, height, width].
        /// </remarks>
        [Pure]
        public static ReadWriteTexture3D<T, TPixel> AllocateReadWriteTexture3D<T, TPixel>(this GraphicsDevice device, T[,,] array)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            ReadWriteTexture3D<T, TPixel> texture = new(device, array.GetLength(2), array.GetLength(1), array.GetLength(0));

            texture.SetData(array);

            return texture;
        }

        /// <summary>
        /// Allocates a new writeable 3D texture with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
        /// <param name="span">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <returns>A <see cref="ReadWriteTexture3D{T,TPixel}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadWriteTexture3D<T, TPixel> AllocateReadWriteTexture3D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<T> span, int width, int height, int depth)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            ReadWriteTexture3D<T, TPixel> texture = new(device, width, height, depth);

            texture.SetData(span);

            return texture;
        }

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of compute shader to run.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
        /// <param name="x">The number of iterations to run on the X axis.</param>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
        public static void For<T>(this GraphicsDevice device, int x, in T shader)
            where T : struct, IComputeShader
        {
            ShaderRunner<T>.Run(device, x, 1, 1, in shader);
        }

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of compute shader to run.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
        /// <param name="x">The number of iterations to run on the X axis.</param>
        /// <param name="y">The number of iterations to run on the Y axis.</param>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
        public static void For<T>(this GraphicsDevice device, int x, int y, in T shader)
            where T : struct, IComputeShader
        {
            ShaderRunner<T>.Run(device, x, y, 1, in shader);
        }

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of compute shader to run.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
        /// <param name="x">The number of iterations to run on the X axis.</param>
        /// <param name="y">The number of iterations to run on the Y axis.</param>
        /// <param name="z">The number of iterations to run on the Z axis.</param>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
        public static void For<T>(this GraphicsDevice device, int x, int y, int z, in T shader)
            where T : struct, IComputeShader
        {
            ShaderRunner<T>.Run(device, x, y, z, in shader);
        }

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of compute shader to run.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
        /// <param name="x">The number of iterations to run on the X axis.</param>
        /// <param name="y">The number of iterations to run on the Y axis.</param>
        /// <param name="z">The number of iterations to run on the Z axis.</param>
        /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
        /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
        /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
        public static void For<T>(this GraphicsDevice device, int x, int y, int z, int threadsX, int threadsY, int threadsZ, in T shader)
            where T : struct, IComputeShader
        {
            ShaderRunner<T>.Run(device, x, y, z, threadsX, threadsY, threadsZ, in shader);
        }
    }
}
