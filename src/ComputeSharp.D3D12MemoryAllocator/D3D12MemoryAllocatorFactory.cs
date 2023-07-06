using System;
using System.Runtime.InteropServices;
using ComputeSharp.Core.Extensions;
using ComputeSharp.D3D12MemoryAllocator.Interop;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D3D12MemoryAllocator;

/// <summary>
/// The entry point to obtain <c>ID3D12MemoryAllocatorFactory</c> instances using <see href="https://gpuopen.com/d3d12-memory-allocator/">D3D12MA</see>.
/// </summary>
public sealed unsafe class D3D12MemoryAllocatorFactory : ICustomQueryInterface
{
    /// <summary>
    /// The <see cref="ID3D12MemoryAllocatorFactoryImpl"/> object wrapped by the current instance.
    /// </summary>
    private readonly ID3D12MemoryAllocatorFactoryImpl* allocatorFactory;

    /// <summary>
    /// Creates a new <see cref="D3D12MemoryAllocatorFactory"/> instance (implementing <c>ID3D12MemoryAllocatorFactory</c>).
    /// </summary>
    /// <remarks>
    /// The resulting object should only be used to provide an argument for <see cref="ComputeSharp.Interop.AllocationServices.ConfigureAllocatorFactory"/>.
    /// </remarks>
    public D3D12MemoryAllocatorFactory()
    {
        fixed (ID3D12MemoryAllocatorFactoryImpl** allocatorFactory = &this.allocatorFactory)
        {
            ID3D12MemoryAllocatorFactoryImpl.Factory(allocatorFactory).Assert();
        }
    }

    /// <summary>
    /// Releases the underlying <c>ID3D12MemoryAllocatorFactory</c> object.
    /// </summary>
    ~D3D12MemoryAllocatorFactory()
    {
        if (this.allocatorFactory is not null)
        {
            _ = this.allocatorFactory->Release();
        }
    }

    /// <inheritdoc/>
    CustomQueryInterfaceResult ICustomQueryInterface.GetInterface(ref Guid iid, out IntPtr ppv)
    {
        // Handle invalid cases where the object was not constructed correctly.
        // Practically speaking, this should never happen in a real world scenario.
        if (this.allocatorFactory is null)
        {
            ppv = default;

            return CustomQueryInterfaceResult.Failed;
        }

        fixed (Guid* riid = &iid)
        fixed (IntPtr* ppResult = &ppv)
        {
            int hresult = this.allocatorFactory->QueryInterface(riid, (void**)ppResult);

            GC.KeepAlive(this);

            return hresult switch
            {
                S.S_OK => CustomQueryInterfaceResult.Handled,
                _ => CustomQueryInterfaceResult.Failed
            };
        }
    }
}
