using ComputeSharp.Interop;
using ComputeSharp.Win32;

namespace ComputeSharp.Resources.Interop;

/// <summary>
/// An interface for a writeable, non-generic graphics resource types.
/// </summary>
internal unsafe interface ID3D12ReadWriteResource : ID3D12ReadOnlyResource
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
    (D3D12_RESOURCE_STATES Before, D3D12_RESOURCE_STATES After) ValidateAndGetID3D12ResourceAndTransitionStates(
        GraphicsDevice device,
        ResourceState resourceState,
        out ID3D12Resource* d3D12Resource,
        out ReferenceTracker.Lease lease);
}