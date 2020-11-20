using System;
using System.Diagnostics.Contracts;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Shaders;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that contains extension methods for the <see cref="GraphicsDevice"/> type, used to run compute shaders.
    /// </summary>
    public static class GraphicsDevice2Extensions
    {
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
            return device.AllocateConstantBuffer(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new constant buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A constant <see cref="ConstantBuffer{T}"/> instance with the contents of the input <see cref="Span{T}"/>.</returns>
        [Pure]
        public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, Span<T> span)
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
            ConstantBuffer<T> constantBuffer = new(device, buffer.Size);

            constantBuffer.SetData(buffer);

            return constantBuffer;
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="size">The size of the buffer to allocate.</param>
        /// <returns>A zeroed <see cref="ReadOnlyBuffer{T}"/> instance of size <paramref name="size"/>.</returns>
        [Pure]
        public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, int size)
            where T : unmanaged
        {
            return new(device, size);
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
            return device.AllocateReadOnlyBuffer(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new readonly buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input <see cref="Span{T}"/>.</returns>
        [Pure]
        public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, Span<T> span)
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
            ReadOnlyBuffer<T> readWriteBuffer = new(device, buffer.Size);

            readWriteBuffer.SetData(buffer);

            return readWriteBuffer;
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="size">The size of the buffer to allocate.</param>
        /// <returns>A zeroed <see cref="ReadWriteBuffer{T}"/> instance of size <paramref name="size"/>.</returns>
        [Pure]
        public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, int size)
            where T : unmanaged
        {
            return new(device, size);
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
            return device.AllocateReadWriteBuffer(array.AsSpan());
        }

        /// <summary>
        /// Allocates a new read write buffer with the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
        /// <param name="span">The input <see cref="Span{T}"/> with the data to copy on the allocated buffer.</param>
        /// <returns>A read write <see cref="ReadWriteBuffer{T}"/> instance with the contents of the input <see cref="Span{T}"/>.</returns>
        [Pure]
        public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, Span<T> span)
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
            ReadWriteBuffer<T> readWriteBuffer = new(device, buffer.Size);

            readWriteBuffer.SetData(buffer);

            return readWriteBuffer;
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
