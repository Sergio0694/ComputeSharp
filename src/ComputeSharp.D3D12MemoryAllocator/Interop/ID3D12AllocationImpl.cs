using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.Interop.Allocation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D3D12MemoryAllocator.Interop;

/// <summary>
/// An implementation of the <see cref="ID3D12Allocation"/> interface.
/// </summary>
internal unsafe struct ID3D12AllocationImpl
{
    /// <summary>
    /// The shared method table pointer for all <see cref="ID3D12Allocation"/> instances.
    /// </summary>
    private static readonly void** Vtbl = InitVtbl();

    /// <summary>
    /// Builds the custom method table pointer for <see cref="ID3D12Allocation"/>.
    /// </summary>
    /// <returns>The method table pointer for <see cref="ID3D12Allocation"/>.</returns>
    private static void** InitVtbl()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(ID3D12AllocationImpl), sizeof(void*) * 4);

        lpVtbl[0] = (delegate* unmanaged<ID3D12AllocationImpl*, Guid*, void**, int>)&QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<ID3D12AllocationImpl*, uint>)&AddRef;
        lpVtbl[2] = (delegate* unmanaged<ID3D12AllocationImpl*, uint>)&Release;
        lpVtbl[3] = (delegate* unmanaged<ID3D12AllocationImpl*, ID3D12Resource**, int>)&GetD3D12Resource;

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
    /// The parent <see cref="ID3D12MemoryAllocatorImpl"/> instance.
    /// </summary>
    private ID3D12MemoryAllocatorImpl* allocatorImpl;

    /// <summary>
    /// The wrapped <see cref="D3D12MA_Allocation"/> instance.
    /// </summary>
    private D3D12MA_Allocation* allocation;

    /// <summary>
    /// The underlying <see cref="ID3D12Resource"/> instance.
    /// </summary>
    private ID3D12Resource* d3D12Resource;

    /// <summary>
    /// The factory method for <see cref="ID3D12AllocationImpl"/> instances.
    /// </summary>
    /// <param name="allocationImpl">The resulting <see cref="ID3D12AllocationImpl"/> instance.</param>
    /// <param name="allocatorImpl">The parent <see cref="ID3D12MemoryAllocatorImpl"/> instance.</param>
    /// <param name="allocation">The wrapped <see cref="D3D12MA_Allocation"/> instance.</param>
    /// <param name="d3D12Resource">The underlying <see cref="ID3D12Resource"/> instance.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    public static HRESULT Factory(
        ID3D12AllocationImpl** allocationImpl,
        ID3D12MemoryAllocatorImpl* allocatorImpl,
        D3D12MA_Allocation* allocation,
        ID3D12Resource* d3D12Resource)
    {
        ID3D12AllocationImpl* @this;

        try
        {
            @this = (ID3D12AllocationImpl*)NativeMemory.Alloc((nuint)sizeof(ID3D12AllocationImpl));
        }
        catch (OutOfMemoryException)
        {
            *allocationImpl = null;

            return E.E_OUTOFMEMORY;
        }

        _ = allocatorImpl->AddRef();
        _ = allocation->AddRef();
        _ = d3D12Resource->AddRef();

        @this->lpVtbl = Vtbl;
        @this->referenceCount = 1;
        @this->allocatorImpl = allocatorImpl;
        @this->allocation = allocation;
        @this->d3D12Resource = d3D12Resource;

        *allocationImpl = @this;

        return S.S_OK;
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [UnmanagedCallersOnly]
    private static int QueryInterface(ID3D12AllocationImpl* @this, Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(ID3D12Allocation.Guid))
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
    private static uint AddRef(ID3D12AllocationImpl* @this)
    {
        return (uint)Interlocked.Increment(ref @this->referenceCount);
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [UnmanagedCallersOnly]
    private static uint Release(ID3D12AllocationImpl* @this)
    {
        uint referenceCount = (uint)Interlocked.Decrement(ref @this->referenceCount);

        if (referenceCount == 0)
        {
            _ = @this->allocation->Release();
            _ = @this->d3D12Resource->Release();

            // Release the parent allocator as well as last thing. This is needed because the D3D12MA allocator
            // will fail if an allocator is released while there are still outstanding allocations. So here we
            // increment the reference count of the allocator when creating a new allocation object, and then we
            // decrement it after the underlying allocation has been released. This means that if this allocation
            // is the last thing keeping the allocator alive, it'll be disposed correctly after this last object.
            _ = @this->allocatorImpl->Release();

            NativeMemory.Free(@this);
        }

        return referenceCount;
    }

    /// <inheritdoc cref="ID3D12Allocation.GetD3D12Resource"/>
    [UnmanagedCallersOnly]
    private static int GetD3D12Resource(ID3D12AllocationImpl* @this, ID3D12Resource** resource)
    {
        if (resource is null)
        {
            return E.E_POINTER;
        }

        _ = @this->d3D12Resource->AddRef();

        *resource = @this->d3D12Resource;

        return S.S_OK;
    }
}