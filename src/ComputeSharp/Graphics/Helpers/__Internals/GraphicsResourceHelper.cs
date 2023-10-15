using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComputeSharp.Interop;
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
    /// An interface for a readonly, non-generic readonly graphics resource types.
    /// </summary>
    internal unsafe interface IReadOnlyResource
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
        /// <param name="lease">The <see cref="ReferenceTracker.Lease"/> value for the returned <see cref="ID3D12Resource"/> object.</param>
        /// <returns>The the underlying <see cref="ID3D12Resource"/> object.</returns> 
        ID3D12Resource* ValidateAndGetID3D12Resource(GraphicsDevice device, out ReferenceTracker.Lease lease);
    }

    /// <summary>
    /// An interface for a writeable, non-generic graphics resource types.
    /// </summary>
    internal unsafe interface IReadWriteResource : IReadOnlyResource
    {
        /// <summary>
        /// Validates the given resource for usage with a specified device, and retrieves its GPU and CPU descriptor handles for a clear operation.
        /// </summary>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
        /// <param name="isNormalized">Indicates whether the current resource uses a normalized format.</param>
        /// <returns>The GPU and CPU descriptor handles for the resource.</returns> 
        (D3D12_GPU_DESCRIPTOR_HANDLE Gpu, D3D12_CPU_DESCRIPTOR_HANDLE Cpu) ValidateAndGetGpuAndCpuDescriptorHandlesForClear(GraphicsDevice device, out bool isNormalized);

        /// <summary>
        /// Validates the given resource for usage with a specified device, and retrieves the underlying <see cref="ID3D12Resource"/> object, along with the transition states.
        /// </summary>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
        /// <param name="resourceState">The target state to transition the resource to.</param>
        /// <param name="d3D12Resource">The the underlying <see cref="ID3D12Resource"/> object.</param>
        /// <param name="lease">The <see cref="ReferenceTracker.Lease"/> value for the returned <see cref="ID3D12Resource"/> object.</param>
        /// <returns>The resource states for <paramref name="d3D12Resource"/>, before and after the transition.</returns>
        (D3D12_RESOURCE_STATES Before, D3D12_RESOURCE_STATES After) ValidateAndGetID3D12ResourceAndTransitionStates(GraphicsDevice device, ResourceState resourceState, out ID3D12Resource* d3D12Resource, out ReferenceTracker.Lease lease);
    }

    /// <summary>
    /// Validates the given resource for usage with a specified device, and retrieves its GPU descriptor handle.
    /// </summary>
    /// <param name="resource">The input <see cref="IGraphicsResource"/> instance to check.</param>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
    /// <returns>The GPU descriptor handle for the resource.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ulong ValidateAndGetGpuDescriptorHandle(IGraphicsResource resource, GraphicsDevice device)
    {
        default(ArgumentNullException).ThrowIfNull(resource);

        if (resource is IReadOnlyResource readOnlyResource)
        {
            D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle = readOnlyResource.ValidateAndGetGpuDescriptorHandle(device);

            return *(ulong*)&d3D12GpuDescriptorHandle;
        }

        return default(ArgumentException).Throw<ulong>(nameof(resource));
    }
}