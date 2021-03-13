using System;
using System.Diagnostics.Contracts;
using ComputeSharp.__Internals;
using ComputeSharp.Resources;

#pragma warning disable CS0618

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
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <returns>A <see cref="Buffer{T}"/> instance of the requested size.</returns>
        [Pure]
        public static Buffer<T> AllocateBuffer<T>(this GraphicsDevice device, Type type, int length, AllocationMode allocationMode = AllocationMode.Default)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(ConstantBuffer<>) => device.AllocateConstantBuffer<T>(length, allocationMode),
                _ when type == typeof(ReadOnlyBuffer<>) => device.AllocateReadOnlyBuffer<T>(length, allocationMode),
                _ when type == typeof(ReadWriteBuffer<>) => device.AllocateReadWriteBuffer<T>(length, allocationMode),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Allocates a new <see cref="Texture2D{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the texture.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
        /// <param name="type">The type of texture to allocate.</param>
        /// <param name="width">The width of the texture to create.</param>
        /// <param name="height">The height of the texture to create.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <returns>A <see cref="Texture2D{T}"/> instance of the requested size.</returns>
        [Pure]
        public static Texture2D<T> AllocateTexture2D<T>(this GraphicsDevice device, Type type, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(ReadOnlyTexture2D<>) => device.AllocateReadOnlyTexture2D<T>(width, height, allocationMode),
                _ when type == typeof(ReadWriteTexture2D<>) => device.AllocateReadWriteTexture2D<T>(width, height, allocationMode),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Allocates a new <see cref="Texture2D{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
        /// <param name="type">The type of texture to allocate.</param>
        /// <param name="width">The width of the texture to create.</param>
        /// <param name="height">The height of the texture to create.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <returns>A <see cref="Texture2D{T}"/> instance of the requested size.</returns>
        [Pure]
        public static Texture2D<T> AllocateTexture2D<T, TPixel>(this GraphicsDevice device, Type type, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return type switch
            {
                _ when type == typeof(ReadOnlyTexture2D<,>) => device.AllocateReadOnlyTexture2D<T, TPixel>(width, height, allocationMode),
                _ when type == typeof(ReadWriteTexture2D<,>) => device.AllocateReadWriteTexture2D<T, TPixel>(width, height, allocationMode),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Allocates a new <see cref="Texture3D{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the texture.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
        /// <param name="type">The type of texture to allocate.</param>
        /// <param name="width">The width of the texture to create.</param>
        /// <param name="height">The height of the texture to create.</param>
        /// <param name="depth">The depth of the texture to create.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <returns>A <see cref="Texture2D{T}"/> instance of the requested size.</returns>
        [Pure]
        public static Texture3D<T> AllocateTexture3D<T>(this GraphicsDevice device, Type type, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(ReadOnlyTexture3D<>) => device.AllocateReadOnlyTexture3D<T>(width, height, depth, allocationMode),
                _ when type == typeof(ReadWriteTexture3D<>) => device.AllocateReadWriteTexture3D<T>(width, height, depth, allocationMode),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Allocates a new <see cref="Texture3D{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the texture.</typeparam>
        /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
        /// <param name="type">The type of texture to allocate.</param>
        /// <param name="width">The width of the texture to create.</param>
        /// <param name="height">The height of the texture to create.</param>
        /// <param name="depth">The depth of the texture to create.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <returns>A <see cref="Texture3D{T}"/> instance of the requested size.</returns>
        [Pure]
        public static Texture3D<T> AllocateTexture3D<T, TPixel>(this GraphicsDevice device, Type type, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            return type switch
            {
                _ when type == typeof(ReadOnlyTexture3D<,>) => device.AllocateReadOnlyTexture3D<T, TPixel>(width, height, depth, allocationMode),
                _ when type == typeof(ReadWriteTexture3D<,>) => device.AllocateReadWriteTexture3D<T, TPixel>(width, height, depth, allocationMode),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Allocates a new <see cref="TransferBuffer{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the buffer.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
        /// <param name="type">The type of buffer to allocate.</param>
        /// <param name="length">The length of the buffer to create.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <returns>A <see cref="TransferBuffer{T}"/> instance of the requested size.</returns>
        [Pure]
        public static TransferBuffer<T> AllocateTransferBuffer<T>(this GraphicsDevice device, Type type, int length, AllocationMode allocationMode = AllocationMode.Default)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(UploadBuffer<>) => device.AllocateUploadBuffer<T>(length, allocationMode),
                _ when type == typeof(ReadBackBuffer<>) => device.AllocateReadBackBuffer<T>(length, allocationMode),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Allocates a new <see cref="TransferTexture2D{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the buffer.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
        /// <param name="type">The type of buffer to allocate.</param>
        /// <param name="width">The width of the texture to create.</param>
        /// <param name="height">The height of the texture to create.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <returns>A <see cref="TransferTexture2D{T}"/> instance of the requested size.</returns>
        [Pure]
        public static TransferTexture2D<T> AllocateTransferTexture2D<T>(this GraphicsDevice device, Type type, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(UploadTexture2D<>) => device.AllocateUploadTexture2D<T>(width, height, allocationMode),
                _ when type == typeof(ReadBackTexture2D<>) => device.AllocateReadBackTexture2D<T>(width, height, allocationMode),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Allocates a new <see cref="TransferTexture3D{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the buffer.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
        /// <param name="type">The type of buffer to allocate.</param>
        /// <param name="width">The width of the texture to create.</param>
        /// <param name="height">The height of the texture to create.</param>
        /// <param name="depth">The depth of the texture to create.</param>
        /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
        /// <returns>A <see cref="TransferTexture3D{T}"/> instance of the requested size.</returns>
        [Pure]
        public static TransferTexture3D<T> AllocateTransferTexture3D<T>(this GraphicsDevice device, Type type, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(UploadTexture3D<>) => device.AllocateUploadTexture3D<T>(width, height, depth, allocationMode),
                _ when type == typeof(ReadBackTexture3D<>) => device.AllocateReadBackTexture3D<T>(width, height, depth, allocationMode),
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
        /// Allocates a new <see cref="Texture2D{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the texture.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
        /// <param name="type">The type of buffer to allocate.</param>
        /// <param name="data">The data to load on the texture.</param>
        /// <param name="width">The width of the texture to create.</param>
        /// <param name="height">The height of the texture to create.</param>
        /// <returns>A <see cref="Texture2D{T}"/> instance of the requested type.</returns>
        [Pure]
        public static Texture2D<T> AllocateTexture2D<T>(this GraphicsDevice device, Type type, T[] data, int width, int height)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(ReadOnlyTexture2D<>) => device.AllocateReadOnlyTexture2D(data, width, height),
                _ when type == typeof(ReadWriteTexture2D<>) => device.AllocateReadWriteTexture2D(data, width, height),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Allocates a new <see cref="Texture3D{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the texture.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
        /// <param name="type">The type of buffer to allocate.</param>
        /// <param name="data">The data to load on the texture.</param>
        /// <param name="width">The width of the texture to create.</param>
        /// <param name="height">The height of the texture to create.</param>
        /// <param name="depth">The depth of the texture to create.</param>
        /// <returns>A <see cref="Texture2D{T}"/> instance of the requested type.</returns>
        [Pure]
        public static Texture3D<T> AllocateTexture3D<T>(this GraphicsDevice device, Type type, T[] data, int width, int height, int depth)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(ReadOnlyTexture3D<>) => device.AllocateReadOnlyTexture3D(data, width, height, depth),
                _ when type == typeof(ReadWriteTexture3D<>) => device.AllocateReadWriteTexture3D(data, width, height, depth),
                _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Allocates a new <see cref="Texture3D{T}"/> instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of items in the texture.</typeparam>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
        /// <param name="type">The type of texture to allocate.</param>
        /// <returns>A <see cref="Texture2D{T}"/> instance of the requested size.</returns>
        [Pure]
        public static Texture3D<T> AllocateTexture3D<T>(this GraphicsDevice device, Type type, T[,,] data)
            where T : unmanaged
        {
            return type switch
            {
                _ when type == typeof(ReadOnlyTexture3D<>) => device.AllocateReadOnlyTexture3D<T>(data),
                _ when type == typeof(ReadWriteTexture3D<>) => device.AllocateReadWriteTexture3D<T>(data),
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
