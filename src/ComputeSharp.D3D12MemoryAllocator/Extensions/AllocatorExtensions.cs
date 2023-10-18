using ComputeSharp.Core.Extensions;
using ComputeSharp.Win32;
using static ComputeSharp.Win32.D3D12_CPU_PAGE_PROPERTY;
using static ComputeSharp.Win32.D3D12_HEAP_TYPE;
using static ComputeSharp.Win32.D3D12_MEMORY_POOL;
using D3D12MA_Allocator = TerraFX.Interop.DirectX.D3D12MA_Allocator;
using D3D12MA_Pool = TerraFX.Interop.DirectX.D3D12MA_Pool;
using D3D12MA_POOL_DESC = TerraFX.Interop.DirectX.D3D12MA_POOL_DESC;

namespace ComputeSharp.D3D12MemoryAllocator.Extensions;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="D3D12MA_Allocator"/> type.
/// </summary>
internal static unsafe class AllocatorExtensions
{
    /// <summary>
    /// Creates a <see cref="D3D12MA_Pool"/> instance suited to be used for cache coherent UMA devices.
    /// </summary>
    /// <param name="allocator">The <see cref="D3D12MA_Allocator"/> instance in use.</param>
    /// <returns>A <see cref="D3D12MA_Pool"/> instance suited to be used for cache coherent UMA devices.</returns>
    public static ComPtr<D3D12MA_Pool> CreatePoolForCacheCoherentUMA(this ref D3D12MA_Allocator allocator)
    {
        using ComPtr<D3D12MA_Pool> pool = default;

        D3D12MA_POOL_DESC poolDesc = default;
        poolDesc.HeapProperties.CreationNodeMask = 1;
        poolDesc.HeapProperties.VisibleNodeMask = 1;
        poolDesc.HeapProperties.Type = (TerraFX.Interop.DirectX.D3D12_HEAP_TYPE)D3D12_HEAP_TYPE_CUSTOM;
        poolDesc.HeapProperties.CPUPageProperty = (TerraFX.Interop.DirectX.D3D12_CPU_PAGE_PROPERTY)D3D12_CPU_PAGE_PROPERTY_WRITE_BACK;
        poolDesc.HeapProperties.MemoryPoolPreference = (TerraFX.Interop.DirectX.D3D12_MEMORY_POOL)D3D12_MEMORY_POOL_L0;

        ((HRESULT)(int)allocator.CreatePool(&poolDesc, pool.GetAddressOf())).Assert();

        return pool.Move();
    }
}