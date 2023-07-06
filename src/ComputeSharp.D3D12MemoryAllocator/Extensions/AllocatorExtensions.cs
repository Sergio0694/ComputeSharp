using ComputeSharp.Core.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_CPU_PAGE_PROPERTY;
using static TerraFX.Interop.DirectX.D3D12_HEAP_TYPE;
using static TerraFX.Interop.DirectX.D3D12_MEMORY_POOL;

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
        poolDesc.HeapProperties.Type = D3D12_HEAP_TYPE_CUSTOM;
        poolDesc.HeapProperties.CPUPageProperty = D3D12_CPU_PAGE_PROPERTY_WRITE_BACK;
        poolDesc.HeapProperties.MemoryPoolPreference = D3D12_MEMORY_POOL_L0;

        allocator.CreatePool(&poolDesc, pool.GetAddressOf()).Assert();

        return pool.Move();
    }
}