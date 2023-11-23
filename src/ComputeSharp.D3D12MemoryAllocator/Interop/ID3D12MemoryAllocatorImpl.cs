using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.Interop.Allocation;
using ComputeSharp.Win32;
using D3D12MA_Allocation = TerraFX.Interop.DirectX.D3D12MA_Allocation;
using D3D12MA_ALLOCATION_DESC = TerraFX.Interop.DirectX.D3D12MA_ALLOCATION_DESC;
using D3D12MA_ALLOCATION_FLAGS = TerraFX.Interop.DirectX.D3D12MA_ALLOCATION_FLAGS;
using D3D12MA_Allocator = TerraFX.Interop.DirectX.D3D12MA_Allocator;
using D3D12MA_Pool = TerraFX.Interop.DirectX.D3D12MA_Pool;

namespace ComputeSharp.D3D12MemoryAllocator.Interop;

/// <summary>
/// An implementation of the <see cref="ID3D12MemoryAllocator"/> interface.
/// </summary>
internal unsafe struct ID3D12MemoryAllocatorImpl
{
    /// <summary>
    /// The shared method table pointer for all <see cref="ID3D12MemoryAllocator"/> instances.
    /// </summary>
    private static readonly void** Vtbl = InitVtbl();

    /// <summary>
    /// Builds the custom method table pointer for <see cref="ID3D12MemoryAllocator"/>.
    /// </summary>
    /// <returns>The method table pointer for <see cref="ID3D12MemoryAllocator"/>.</returns>
    private static void** InitVtbl()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(ID3D12MemoryAllocator), sizeof(void*) * 4);

        lpVtbl[0] = (delegate* unmanaged<ID3D12MemoryAllocatorImpl*, Guid*, void**, int>)&QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<ID3D12MemoryAllocatorImpl*, uint>)&AddRef;
        lpVtbl[2] = (delegate* unmanaged<ID3D12MemoryAllocatorImpl*, uint>)&Release;
        lpVtbl[3] = (delegate* unmanaged<ID3D12MemoryAllocatorImpl*, D3D12_RESOURCE_DESC*, D3D12_HEAP_TYPE, D3D12_RESOURCE_STATES, BOOL, ID3D12Allocation**, int>)&AllocateResource;

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
    /// The <see cref="D3D12MA_Allocator"/> in use associated to the current device.
    /// </summary>
    private ComPtr<D3D12MA_Allocator> allocator;

    /// <summary>
    /// The <see cref="D3D12MA_Pool"/> instance in use, if the device is using cache coherent UMA.
    /// </summary>
    private ComPtr<D3D12MA_Pool> pool;

    /// <summary>
    /// The factory method for <see cref="ID3D12MemoryAllocatorImpl"/> instances.
    /// </summary>
    /// <param name="allocatorImpl">The resulting <see cref="ID3D12MemoryAllocatorImpl"/> instance.</param>
    /// <param name="allocator">The input <see cref="D3D12MA_Allocator"/> instance.</param>
    /// <param name="pool">The input <see cref="D3D12MA_Pool"/> instance, if available.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    public static HRESULT Factory(
        ID3D12MemoryAllocatorImpl** allocatorImpl,
        D3D12MA_Allocator* allocator,
        D3D12MA_Pool* pool)
    {
        ID3D12MemoryAllocatorImpl* @this;

        try
        {
            @this = (ID3D12MemoryAllocatorImpl*)NativeMemory.Alloc((nuint)sizeof(ID3D12MemoryAllocatorImpl));
        }
        catch (OutOfMemoryException)
        {
            *allocatorImpl = null;

            return E.E_OUTOFMEMORY;
        }

        @this->lpVtbl = Vtbl;
        @this->referenceCount = 1;
        @this->allocator = new ComPtr<D3D12MA_Allocator>(allocator);
        @this->pool = new ComPtr<D3D12MA_Pool>(pool);

        *allocatorImpl = @this;

        return S.S_OK;
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint AddRef()
    {
        return (uint)Interlocked.Increment(ref this.referenceCount);
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint Release()
    {
        uint referenceCount = (uint)Interlocked.Decrement(ref this.referenceCount);

        if (referenceCount == 0)
        {
            // The pool must be released before its associated allocator is disposed. Note that in
            // order for the disposal to work correctly, all resources created from the allocator
            // must have been disposed already before the allocator is released. This is done by
            // having the allocator also increment the reference count of their associated allocator.
            // So here we just have to release first the pool, if present, and then the allocator.
            this.pool.Dispose();
            this.allocator.Dispose();

            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [UnmanagedCallersOnly]
    private static int QueryInterface(ID3D12MemoryAllocatorImpl* @this, Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(Windows.__uuidof<ID3D12MemoryAllocator>()))
        {
            _ = Interlocked.Increment(ref @this->referenceCount);

            *ppvObject = @this;

            return S.S_OK;
        }

        *ppvObject = null;

        return E.E_NOINTERFACE;
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [UnmanagedCallersOnly]
    private static uint AddRef(ID3D12MemoryAllocatorImpl* @this)
    {
        return @this->AddRef();
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [UnmanagedCallersOnly]
    private static uint Release(ID3D12MemoryAllocatorImpl* @this)
    {
        return @this->Release();
    }

    /// <inheritdoc cref="ID3D12MemoryAllocator.AllocateResource"/>
    [UnmanagedCallersOnly]
    private static int AllocateResource(
        ID3D12MemoryAllocatorImpl* @this,
        D3D12_RESOURCE_DESC* resourceDescription,
        D3D12_HEAP_TYPE heapType,
        D3D12_RESOURCE_STATES resourceStates,
        BOOL clearAllocation,
        ID3D12Allocation** allocation)
    {
        D3D12MA_ALLOCATION_FLAGS allocationFlags = clearAllocation ?
            D3D12MA_ALLOCATION_FLAGS.D3D12MA_ALLOCATION_FLAG_COMMITTED :
            D3D12MA_ALLOCATION_FLAGS.D3D12MA_ALLOCATION_FLAG_NONE;

        D3D12MA_ALLOCATION_DESC allocationDesc = default;
        allocationDesc.HeapType = (TerraFX.Interop.DirectX.D3D12_HEAP_TYPE)heapType;
        allocationDesc.Flags = allocationFlags;
        allocationDesc.CustomPool = @this->pool.Get();

        using ComPtr<D3D12MA_Allocation> d3D12MA_allocation = default;
        using ComPtr<ID3D12Resource> d3D12Resource = default;

        // Invoke D3D12MA and create an allocation and resource
        TerraFX.Interop.Windows.HRESULT hresult = @this->allocator.Get()->CreateResource(
            pAllocDesc: &allocationDesc,
            pResourceDesc: (TerraFX.Interop.DirectX.D3D12_RESOURCE_DESC*)resourceDescription,
            InitialResourceState: (TerraFX.Interop.DirectX.D3D12_RESOURCE_STATES)resourceStates,
            pOptimizedClearValue: null,
            ppAllocation: d3D12MA_allocation.GetAddressOf(),
            riidResource: Windows.__uuidof<ID3D12Resource>(),
            ppvResource: (void**)d3D12Resource.GetAddressOf());

        if (!TerraFX.Interop.Windows.Windows.SUCCEEDED(hresult))
        {
            return hresult;
        }

        // Create the allocation wrapper
        return ID3D12AllocationImpl.Factory(
            allocationImpl: (ID3D12AllocationImpl**)allocation,
            allocatorImpl: @this,
            allocation: d3D12MA_allocation.Get(),
            d3D12Resource: d3D12Resource.Get());
    }
}