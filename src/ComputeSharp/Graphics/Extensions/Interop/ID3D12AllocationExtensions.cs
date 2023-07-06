using ComputeSharp.Core.Extensions;
using ComputeSharp.Interop.Allocation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics.Extensions;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="ID3D12Allocation"/> type.
/// </summary>
internal static unsafe class ID3D12AllocationExtensions
{
    /// <summary>
    /// Gets the underlying <see cref="ID3D12Resource"/> for a given <see cref="ID3D12Allocation"/> object.
    /// </summary>
    /// <param name="allocation">The input <see cref="ID3D12Allocation"/> object.</param>
    /// <returns>An <see cref="ComPtr{T}"/> reference for the current <see cref="ID3D12Resource"/> object.</returns>
    /// <remarks>
    /// The returned resource should not be used after the current allocation instance has been released.
    /// </remarks>
    public static ComPtr<ID3D12Resource> GetD3D12Resource(this ref ID3D12Allocation allocation)
    {
        using ComPtr<ID3D12Resource> d3D12Resource = default;

        allocation.GetD3D12Resource(d3D12Resource.GetAddressOf()).Assert();

        return d3D12Resource.Move();
    }
}