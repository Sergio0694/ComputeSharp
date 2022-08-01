using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Diagnostics;
using ComputeSharp.Interop;
using ComputeSharp.Resources;
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
        /// Validates the given resource for usage with a specified device, and retrieves its GPU and CPU descriptor handles for a clear operation.
        /// </summary>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
        /// <param name="isNormalized">Indicates whether the current resource uses a normalized format.</param>
        /// <returns>The GPU and CPU descriptor handles for the resource.</returns> 
        (D3D12_GPU_DESCRIPTOR_HANDLE Gpu, D3D12_CPU_DESCRIPTOR_HANDLE Cpu) ValidateAndGetGpuAndCpuDescriptorHandlesForClear(GraphicsDevice device, out bool isNormalized);

        /// <summary>
        /// Validates the given resource for usage with a specified device, and retrieves the underlying <see cref="ID3D12Resource"/> object.
        /// </summary>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
        /// <param name="lease">The <see cref="NativeObject.Lease"/> value for the returned <see cref="ID3D12Resource"/> object.</param>
        /// <returns>The the underlying <see cref="ID3D12Resource"/> object.</returns> 
        ID3D12Resource* ValidateAndGetID3D12Resource(GraphicsDevice device, out NativeObject.Lease lease);

        /// <summary>
        /// Validates the given resource for usage with a specified device, and retrieves the underlying <see cref="ID3D12Resource"/> object, along with the transition states.
        /// </summary>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
        /// <param name="resourceState">The target state to transition the resource to.</param>
        /// <param name="d3D12Resource">The the underlying <see cref="ID3D12Resource"/> object.</param>
        /// <param name="lease">The <see cref="NativeObject.Lease"/> value for the returned <see cref="ID3D12Resource"/> object.</param>
        /// <returns>The resource states for <paramref name="d3D12Resource"/>, before and after the transition.</returns>
        (D3D12_RESOURCE_STATES Before, D3D12_RESOURCE_STATES After) ValidateAndGetID3D12ResourceAndTransitionStates(GraphicsDevice device, ResourceState resourceState, out ID3D12Resource* d3D12Resource, out NativeObject.Lease lease);
    }

    /// <summary>
    /// Validates the given buffer for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input buffer.</typeparam>
    /// <param name="buffer">The input <see cref="Buffer{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the buffer.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)] T>(Buffer<T> buffer, GraphicsDevice device)
        where T : unmanaged
    {
        Guard.IsNotNull(buffer);

        using var _0 = buffer.GetReferenceTrackingLease();

        buffer.ThrowIfDeviceMismatch(device);

        D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = buffer.D3D12GpuDescriptorHandle;

        return *(ulong*)&d3D12GpuDescriptorHandle;
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="ReadOnlyTexture2D{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<T>(ReadOnlyTexture2D<T> texture, GraphicsDevice device)
        where T : unmanaged
    {
        Guard.IsNotNull(texture);

        using var _0 = texture.GetReferenceTrackingLease();

        texture.ThrowIfDeviceMismatch(device);

        D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = texture.D3D12GpuDescriptorHandle;

        return *(ulong*)&d3D12GpuDescriptorHandle;
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="ReadWriteTexture2D{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<T>(ReadWriteTexture2D<T> texture, GraphicsDevice device)
        where T : unmanaged
    {
        Guard.IsNotNull(texture);

        using var _0 = texture.GetReferenceTrackingLease();

        texture.ThrowIfDeviceMismatch(device);

        D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = texture.D3D12GpuDescriptorHandle;

        return *(ulong*)&d3D12GpuDescriptorHandle;
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="IReadOnlyTexture2D{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<T>(IReadOnlyTexture2D<T> texture, GraphicsDevice device)
        where T : unmanaged
    {
        Guard.IsNotNull(texture);

        if (texture is IGraphicsResource resource)
        {
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = resource.ValidateAndGetGpuDescriptorHandle(device);

            return *(ulong*)&d3D12GpuDescriptorHandle;
        }

        return ThrowHelper.ThrowArgumentException<ulong>("The input texture is not a valid instance.");
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="TPixel">The type of normalized values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="IReadOnlyNormalizedTexture2D{TPixel}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<TPixel>(IReadOnlyNormalizedTexture2D<TPixel> texture, GraphicsDevice device)
        where TPixel : unmanaged
    {
        Guard.IsNotNull(texture);

        if (texture is IGraphicsResource resource)
        {
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = resource.ValidateAndGetGpuDescriptorHandle(device);

            return *(ulong*)&d3D12GpuDescriptorHandle;
        }

        return ThrowHelper.ThrowArgumentException<ulong>("The input texture is not a valid instance.");
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="TPixel">The type of normalized values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="IReadWriteNormalizedTexture2D{TPixel}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<TPixel>(IReadWriteNormalizedTexture2D<TPixel> texture, GraphicsDevice device)
        where TPixel : unmanaged
    {
        Guard.IsNotNull(texture);

        if (texture is IGraphicsResource resource)
        {
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = resource.ValidateAndGetGpuDescriptorHandle(device);

            return *(ulong*)&d3D12GpuDescriptorHandle;
        }

        return ThrowHelper.ThrowArgumentException<ulong>("The input texture is not a valid instance.");
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="ReadOnlyTexture3D{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<T>(ReadOnlyTexture3D<T> texture, GraphicsDevice device)
        where T : unmanaged
    {
        Guard.IsNotNull(texture);

        using var _0 = texture.GetReferenceTrackingLease();

        texture.ThrowIfDeviceMismatch(device);

        D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = texture.D3D12GpuDescriptorHandle;

        return *(ulong*)&d3D12GpuDescriptorHandle;
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="ReadWriteTexture3D{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<T>(ReadWriteTexture3D<T> texture, GraphicsDevice device)
        where T : unmanaged
    {
        Guard.IsNotNull(texture);

        using var _0 = texture.GetReferenceTrackingLease();

        texture.ThrowIfDeviceMismatch(device);

        D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = texture.D3D12GpuDescriptorHandle;

        return *(ulong*)&d3D12GpuDescriptorHandle;
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="IReadOnlyTexture3D{T}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<T>(IReadOnlyTexture3D<T> texture, GraphicsDevice device)
        where T : unmanaged
    {
        Guard.IsNotNull(texture);

        if (texture is IGraphicsResource resource)
        {
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = resource.ValidateAndGetGpuDescriptorHandle(device);

            return *(ulong*)&d3D12GpuDescriptorHandle;
        }

        return ThrowHelper.ThrowArgumentException<ulong>("The input texture is not a valid instance.");
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="TPixel">The type of normalized values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="IReadOnlyNormalizedTexture3D{TPixel}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<TPixel>(IReadOnlyNormalizedTexture3D<TPixel> texture, GraphicsDevice device)
        where TPixel : unmanaged
    {
        Guard.IsNotNull(texture);

        if (texture is IGraphicsResource resource)
        {
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = resource.ValidateAndGetGpuDescriptorHandle(device);

            return *(ulong*)&d3D12GpuDescriptorHandle;
        }

        return ThrowHelper.ThrowArgumentException<ulong>("The input texture is not a valid instance.");
    }

    /// <summary>
    /// Validates the given texture for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <typeparam name="TPixel">The type of normalized values stored in the input texture.</typeparam>
    /// <param name="texture">The input <see cref="IReadWriteNormalizedTexture3D{TPixel}"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the texture.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle<TPixel>(IReadWriteNormalizedTexture3D<TPixel> texture, GraphicsDevice device)
        where TPixel : unmanaged
    {
        Guard.IsNotNull(texture);

        if (texture is IGraphicsResource resource)
        {
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = resource.ValidateAndGetGpuDescriptorHandle(device);

            return *(ulong*)&d3D12GpuDescriptorHandle;
        }

        return ThrowHelper.ThrowArgumentException<ulong>("The input texture is not a valid instance.");
    }
}
