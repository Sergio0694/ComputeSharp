using ComputeSharp.Core.Extensions;
using ComputeSharp.D3D12MemoryAllocator.Interop;

namespace ComputeSharp.D3D12MemoryAllocator;

/// <summary>
/// The entry point to obtain <c>ID3D12MemoryAllocatorFactory</c> instances using <see href="https://gpuopen.com/d3d12-memory-allocator/">D3D12MA</see>.
/// </summary>
public static unsafe class D3D12MemoryAllocatorFactory
{
    /// <summary>
    /// Creates a new <c>ID3D12MemoryAllocatorFactory</c> instance for D3D12MA.
    /// </summary>
    /// <returns>The resulting <c>ID3D12MemoryAllocatorFactory</c> instance for D3D12MA.</returns>
    /// <remarks>
    /// This method should only be used to provide an argument for <see cref="ComputeSharp.Interop.AllocationServices.ConfigureAllocatorFactory"/>.
    /// </remarks>
    public static void* CreateInstance()
    {
        ID3D12MemoryAllocatorFactoryImpl* allocatorFactory = null;

        ID3D12MemoryAllocatorFactoryImpl.Factory(&allocatorFactory).Assert();

        return allocatorFactory;
    }
}
