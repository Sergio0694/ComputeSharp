using System.Runtime.CompilerServices;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Win32;
using D3D12MA_Allocator = TerraFX.Interop.DirectX.D3D12MA_Allocator;
using D3D12MA_ALLOCATOR_DESC = TerraFX.Interop.DirectX.D3D12MA_ALLOCATOR_DESC;
using D3D12MemAlloc = TerraFX.Interop.DirectX.D3D12MemAlloc;

namespace ComputeSharp.D3D12MemoryAllocator.Extensions;

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
        allocatorDesc.pDevice = (TerraFX.Interop.DirectX.ID3D12Device*)Unsafe.AsPointer(ref d3D12Device);
        allocatorDesc.pAdapter = (TerraFX.Interop.DirectX.IDXGIAdapter*)dxgiAdapter;

        ((HRESULT)(int)D3D12MemAlloc.D3D12MA_CreateAllocator(&allocatorDesc, allocator.GetAddressOf())).Assert();

        return allocator.Move();
    }
}