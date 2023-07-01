using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.D3D12MemoryAllocator.Extensions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Interop.Allocation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_FEATURE;

namespace ComputeSharp.D3D12MemoryAllocator.Interop;

/// <summary>
/// An implementation of the <see cref="ID3D12MemoryAllocatorFactory"/> interface.
/// </summary>
internal unsafe struct ID3D12MemoryAllocatorFactoryImpl
{
    /// <summary>
    /// The shared method table pointer for all <see cref="ID3D12MemoryAllocatorFactory"/> instances.
    /// </summary>
    private static readonly void** Vtbl = InitVtbl();

    /// <summary>
    /// Builds the custom method table pointer for <see cref="ID3D12MemoryAllocatorFactory"/>.
    /// </summary>
    /// <returns>The method table pointer for <see cref="ID3D12MemoryAllocatorFactory"/>.</returns>
    private static void** InitVtbl()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(ID3D12MemoryAllocatorFactoryImpl), sizeof(void*) * 4);

        lpVtbl[0] = (delegate* unmanaged<ID3D12MemoryAllocatorFactoryImpl*, Guid*, void**, int>)&QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<ID3D12MemoryAllocatorFactoryImpl*, uint>)&AddRef;
        lpVtbl[2] = (delegate* unmanaged<ID3D12MemoryAllocatorFactoryImpl*, uint>)&Release;
        lpVtbl[3] = (delegate* unmanaged<ID3D12MemoryAllocatorFactoryImpl*, ID3D12Device*, IDXGIAdapter*, ID3D12MemoryAllocator**, int>)&CreateAllocator;

        return lpVtbl;
    }

    /// <summary>
    /// The vtable pointer for the current instance.
    /// </summary>
    private void** lpVtbl;

    /// <summary>
    /// The current reference count for the object (from <see cref="IUnknown"/>).
    /// </summary>
    private volatile int referenceCount;

    /// <summary>
    /// The factory method for <see cref="ID3D12MemoryAllocatorFactoryImpl"/> instances.
    /// </summary>
    /// <param name="allocatorFactoryImpl">The resulting <see cref="ID3D12MemoryAllocatorFactoryImpl"/> instance.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    public static HRESULT Factory(ID3D12MemoryAllocatorFactoryImpl** allocatorFactoryImpl)
    {
        ID3D12MemoryAllocatorFactoryImpl* @this;

        try
        {
            @this = (ID3D12MemoryAllocatorFactoryImpl*)NativeMemory.Alloc((nuint)sizeof(ID3D12MemoryAllocatorFactoryImpl));
        }
        catch (OutOfMemoryException)
        {
            *allocatorFactoryImpl = null;

            return E.E_OUTOFMEMORY;
        }

        @this->lpVtbl = Vtbl;
        @this->referenceCount = 1;

        *allocatorFactoryImpl = @this;

        return S.S_OK;
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int QueryInterface(Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(ID3D12MemoryAllocatorFactory.Guid))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        *ppvObject = null;

        return E.E_NOINTERFACE;
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint Release()
    {
        uint referenceCount = (uint)Interlocked.Decrement(ref this.referenceCount);

        if (referenceCount == 0)
        {
            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [UnmanagedCallersOnly]
    private static int QueryInterface(ID3D12MemoryAllocatorFactoryImpl* @this, Guid* riid, void** ppvObject)
    {
        return @this->QueryInterface(riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [UnmanagedCallersOnly]
    private static uint AddRef(ID3D12MemoryAllocatorFactoryImpl* @this)
    {
        return (uint)Interlocked.Increment(ref @this->referenceCount);
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [UnmanagedCallersOnly]
    private static uint Release(ID3D12MemoryAllocatorFactoryImpl* @this)
    {
        return @this->Release();
    }

    /// <inheritdoc cref="ID3D12MemoryAllocator.AllocateResource"/>
    [UnmanagedCallersOnly]
    private static int CreateAllocator(
        ID3D12MemoryAllocatorFactoryImpl* @this,
        ID3D12Device* device,
        IDXGIAdapter* adapter,
        ID3D12MemoryAllocator** allocator)
    {
        try
        {
            using ComPtr<D3D12MA_Allocator> d3D12MA_allocator = device->CreateAllocator(adapter);

            // If the target device is a UMA device, also create a pool and use that in the allocator
            if (device->CheckFeatureSupport<D3D12_FEATURE_DATA_ARCHITECTURE1>(D3D12_FEATURE_ARCHITECTURE1).CacheCoherentUMA)
            {
                using ComPtr<D3D12MA_Pool> pool = d3D12MA_allocator.Get()->CreatePoolForCacheCoherentUMA();

                return ID3D12MemoryAllocatorImpl.Factory(
                    allocatorImpl: (ID3D12MemoryAllocatorImpl**)allocator,
                    allocator: d3D12MA_allocator.Get(),
                    pool: pool.Get());
            }

            // Otherwise create a standard allocator with no explicit pool
            return ID3D12MemoryAllocatorImpl.Factory(
                allocatorImpl: (ID3D12MemoryAllocatorImpl**)allocator,
                allocator: d3D12MA_allocator.Get(),
                pool: null);
        }
        catch (Exception e)
        {
            return e.HResult;
        }
    }
}