using ComputeSharp.Core.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Graphics.Extensions;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="ID3D12Device"/> type.
/// </summary>
internal static unsafe class ID3D12DeviceExtensions
{
    /// <summary>
    /// Creates a <see cref="D3D12MA_Allocator"/> instance for a given device.
    /// </summary>
    /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to create the allocator.</param>
    /// <param name="dxgiAdapter">The <see cref="IDXGIAdapter"/> that <paramref name="d3D12Device"/> was created from.</param>
    /// <returns>A <see cref="D3D12MA_Allocator"/> instance to create resources on the device in use.</returns>
    public static ComPtr<D3D12MA_Allocator> CreateAllocator(this ref ID3D12Device d3D12Device, IDXGIAdapter* dxgiAdapter)
    {
        using ComPtr<D3D12MA_Allocator> allocator = default;

        D3D12MA_ALLOCATOR_DESC allocatorDesc = default;
        allocatorDesc.pDevice = (ID3D12Device*)Unsafe.AsPointer(ref d3D12Device);
        allocatorDesc.pAdapter = dxgiAdapter;

        D3D12MemAlloc.D3D12MA_CreateAllocator(&allocatorDesc, allocator.GetAddressOf()).Assert();

        return allocator.Move();
    }
}