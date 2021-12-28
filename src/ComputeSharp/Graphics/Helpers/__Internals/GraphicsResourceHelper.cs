using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComputeSharp.Resources;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop.DirectX;

namespace ComputeSharp.__Internals;

/// <summary>
/// A helper class with some proxy methods to expose to generated code in external projects.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This type is not intended to be used directly by user code")]
public static class GraphicsResourceHelper
{
    /// <summary>
    /// An interface for non-generic graphics resource types.
    /// </summary>
    internal unsafe interface IGraphicsResource
    {
        /// <summary>
        /// Validates the given resource for usage with a specified device, and retrieves its GPU descriptor handle.
        /// </summary>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
        /// <returns>The GPU descriptor handle for the resource.</returns> 
        D3D12_GPU_DESCRIPTOR_HANDLE ValidateAndGetGpuDescriptorHandle(GraphicsDevice device);

        /// <summary>
        /// Validates the given resource for usage with a specified device, and retrieves the underlying <see cref="ID3D12Resource"/> object.
        /// </summary>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
        /// <returns>The the underlying <see cref="ID3D12Resource"/> object.</returns> 
        ID3D12Resource* ValidateAndGetID3D12Resource(GraphicsDevice device);
    }

    /// <summary>
    /// Validates the given buffer for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input buffer.</typeparam>
    /// <param name="buffer">The input <see cref="Buffer{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the buffer.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ValidateAndGetGpuDescriptorHandle<T>(Buffer<T> buffer, GraphicsDevice device)
        where T : unmanaged
    {
        buffer.ThrowIfDisposed();
        buffer.ThrowIfDeviceMismatch(device);

        return Unsafe.As<D3D12_GPU_DESCRIPTOR_HANDLE, ulong>(ref Unsafe.AsRef(in buffer.D3D12GpuDescriptorHandle));
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="ReadOnlyTexture2D{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ValidateAndGetGpuDescriptorHandle<T>(ReadOnlyTexture2D<T> texture, GraphicsDevice device)
        where T : unmanaged
    {
        texture.ThrowIfDisposed();
        texture.ThrowIfDeviceMismatch(device);

        return Unsafe.As<D3D12_GPU_DESCRIPTOR_HANDLE, ulong>(ref Unsafe.AsRef(in texture.D3D12GpuDescriptorHandle));
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="ReadWriteTexture2D{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ValidateAndGetGpuDescriptorHandle<T>(ReadWriteTexture2D<T> texture, GraphicsDevice device)
        where T : unmanaged
    {
        texture.ThrowIfDisposed();
        texture.ThrowIfDeviceMismatch(device);

        return Unsafe.As<D3D12_GPU_DESCRIPTOR_HANDLE, ulong>(ref Unsafe.AsRef(in texture.D3D12GpuDescriptorHandle));
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="TPixel">The type of normalized values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="IReadOnlyTexture2D{TPixel}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<TPixel>(IReadOnlyTexture2D<TPixel> texture, GraphicsDevice device)
        where TPixel : unmanaged
    {
        if (texture is IGraphicsResource resource)
        {
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = resource.ValidateAndGetGpuDescriptorHandle(device);

            return *(ulong*)&d3D12GpuDescriptorHandle;
        }

        return ThrowHelper.ThrowArgumentException<ulong>("The input texture is not a valid instance");
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="TPixel">The type of normalized values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="IReadWriteTexture2D{TPixel}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<TPixel>(IReadWriteTexture2D<TPixel> texture, GraphicsDevice device)
        where TPixel : unmanaged
    {
        if (texture is IGraphicsResource resource)
        {
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = resource.ValidateAndGetGpuDescriptorHandle(device);

            return *(ulong*)&d3D12GpuDescriptorHandle;
        }

        return ThrowHelper.ThrowArgumentException<ulong>("The input texture is not a valid instance");
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="ReadOnlyTexture3D{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ValidateAndGetGpuDescriptorHandle<T>(ReadOnlyTexture3D<T> texture, GraphicsDevice device)
        where T : unmanaged
    {
        texture.ThrowIfDisposed();
        texture.ThrowIfDeviceMismatch(device);

        return Unsafe.As<D3D12_GPU_DESCRIPTOR_HANDLE, ulong>(ref Unsafe.AsRef(in texture.D3D12GpuDescriptorHandle));
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="ReadWriteTexture3D{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ValidateAndGetGpuDescriptorHandle<T>(ReadWriteTexture3D<T> texture, GraphicsDevice device)
        where T : unmanaged
    {
        texture.ThrowIfDisposed();
        texture.ThrowIfDeviceMismatch(device);

        return Unsafe.As<D3D12_GPU_DESCRIPTOR_HANDLE, ulong>(ref Unsafe.AsRef(in texture.D3D12GpuDescriptorHandle));
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="TPixel">The type of normalized values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="IReadOnlyTexture3D{TPixel}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<TPixel>(IReadOnlyTexture3D<TPixel> texture, GraphicsDevice device)
        where TPixel : unmanaged
    {
        if (texture is IGraphicsResource resource)
        {
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = resource.ValidateAndGetGpuDescriptorHandle(device);

            return *(ulong*)&d3D12GpuDescriptorHandle;
        }

        return ThrowHelper.ThrowArgumentException<ulong>("The input texture is not a valid instance");
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="TPixel">The type of normalized values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="IReadWriteTexture3D{TPixel}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<TPixel>(IReadWriteTexture3D<TPixel> texture, GraphicsDevice device)
        where TPixel : unmanaged
    {
        if (texture is IGraphicsResource resource)
        {
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = resource.ValidateAndGetGpuDescriptorHandle(device);

            return *(ulong*)&d3D12GpuDescriptorHandle;
        }

        return ThrowHelper.ThrowArgumentException<ulong>("The input texture is not a valid instance");
    }
}
