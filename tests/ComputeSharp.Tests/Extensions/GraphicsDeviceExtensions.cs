using System;
using System.Diagnostics.Contracts;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;

namespace ComputeSharp.Tests.Extensions
{
    /// <summary>
    /// A helper class for testing <see cref="Buffer{T}"/> APIs.
    /// </summary>
    public static class GraphicsDeviceExtensions
    {
        /// <summary>
        /// Allocates a new <see cref="Buffer{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the buffer.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
        /// <param name="type">The type of buffer to allocate.</param>
        /// <param name="length">The length of the buffer to create.</param>
        /// <returns>A <see cref="Buffer{T}"/> instance of the requested size.</returns>
        [Pure]
        public static Buffer<T> AllocateBuffer<T>(this GraphicsDevice device, Type type, int length)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(ConstantBuffer<>) => device.AllocateConstantBuffer<T>(length),
                _ when type == typeof(ReadOnlyBuffer<>) => device.AllocateReadOnlyBuffer<T>(length),
                _ when type == typeof(ReadWriteBuffer<>) => device.AllocateReadWriteBuffer<T>(length),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Allocates a new <see cref="Buffer{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the buffer.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
        /// <param name="type">The type of buffer to allocate.</param>
        /// <param name="data">The data to load on the buffer.</param>
        /// <returns>A <see cref="Buffer{T}"/> instance of the requested type.</returns>
        [Pure]
        public static Buffer<T> AllocateBuffer<T>(this GraphicsDevice device, Type type, T[] data)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(ConstantBuffer<>) => device.AllocateConstantBuffer(data),
                _ when type == typeof(ReadOnlyBuffer<>) => device.AllocateReadOnlyBuffer(data),
                _ when type == typeof(ReadWriteBuffer<>) => device.AllocateReadWriteBuffer(data),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Allocates a new <see cref="Buffer{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the buffer.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
        /// <param name="type">The type of buffer to allocate.</param>
        /// <param name="data">The data to load on the buffer.</param>
        /// <returns>A <see cref="Buffer{T}"/> instance of the requested type.</returns>
        [Pure]
        public static Buffer<T> AllocateBuffer<T>(this GraphicsDevice device, Type type, Buffer<T> data)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(ConstantBuffer<>) => device.AllocateConstantBuffer(data),
                _ when type == typeof(ReadOnlyBuffer<>) => device.AllocateReadOnlyBuffer(data),
                _ when type == typeof(ReadWriteBuffer<>) => device.AllocateReadWriteBuffer(data),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }
    }
}
