using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if NET6_0_OR_GREATER
using TerraFX.Interop;
#endif
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ComputeSharp.Interop.Allocation;

/// <summary>
/// An object that can allocate resources for a given <see cref="ID3D12Device"/> instance.
/// </summary>
[Guid("2D5E55D2-9244-431F-868E-0D90AAB6E575")]
internal unsafe struct ID3D12MemoryAllocator
#if NET6_0_OR_GREATER
    : IUnknown.Interface
#endif
{
    /// <summary>
    /// Gets the <see cref="System.Guid"/> for <see cref="ID3D12MemoryAllocator"/> (<c>2D5E55D2-9244-431F-868E-0D90AAB6E575</c>).
    /// </summary>
    public static ref readonly Guid Guid
    {
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
                0xD2, 0x55, 0x5E, 0x2D,
                0x44, 0x92,
                0x1F, 0x43,
                0x86, 0x8E,
                0x0D,
                0x90,
                0xAA,
                0xB6,
                0xE5,
                0x75
            };

            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

#if NET6_0_OR_GREATER
    /// <inheritdoc/>
    static Guid* INativeGuid.NativeGuid => (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in Guid));
#endif

    /// <summary>
    /// The vtable for the current instance.
    /// </summary>
    private readonly void** lpVtbl;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT QueryInterface(Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12MemoryAllocator*, Guid*, void**, int>)this.lpVtbl[0])((ID3D12MemoryAllocator*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12MemoryAllocator*, uint>)this.lpVtbl[1])((ID3D12MemoryAllocator*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12MemoryAllocator*, uint>)this.lpVtbl[2])((ID3D12MemoryAllocator*)Unsafe.AsPointer(ref this));
    }

    /// <summary>
    /// Creates a new resource allocation from the current allocator.
    /// </summary>
    /// <param name="resourceDescription">The description of the resource to allocate.</param>
    /// <param name="heapType">The type of heap to use for the allocation.</param>
    /// <param name="resourceStates">The resource states to use initially for the resource.</param>
    /// <param name="clearAllocation">Whether to clear the allocation after creating it.</param>
    /// <param name="allocation">The resulting allocation.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT AllocateResource(
        D3D12_RESOURCE_DESC* resourceDescription,
        D3D12_HEAP_TYPE heapType,
        D3D12_RESOURCE_STATES resourceStates,
        BOOL clearAllocation,
        ID3D12Allocation** allocation)
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12MemoryAllocator*, D3D12_RESOURCE_DESC*, D3D12_HEAP_TYPE, D3D12_RESOURCE_STATES, BOOL, ID3D12Allocation**, int>)this.lpVtbl[3])(
            (ID3D12MemoryAllocator*)Unsafe.AsPointer(ref this),
            resourceDescription,
            heapType,
            resourceStates,
            clearAllocation,
            allocation);
    }
}