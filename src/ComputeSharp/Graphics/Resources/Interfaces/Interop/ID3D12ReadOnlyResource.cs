using ComputeSharp.Interop;
using ComputeSharp.Win32;

namespace ComputeSharp.Resources.Interop;

/// <summary>
/// An interface for a readonly, non-generic readonly graphics resource types.
/// </summary>
internal unsafe interface ID3D12ReadOnlyResource
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