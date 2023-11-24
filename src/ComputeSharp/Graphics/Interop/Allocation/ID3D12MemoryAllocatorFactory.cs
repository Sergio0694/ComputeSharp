using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055

namespace ComputeSharp.Interop.Allocation;

/// <summary>
/// A factory type for <see cref="ID3D12MemoryAllocator"/> objects.
/// </summary>
internal unsafe struct ID3D12MemoryAllocatorFactory : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xA7, 0x74, 0x1E, 0xCC,
                0x6D, 0x78,
                0xF4, 0x40,
                0x8A, 0xE2,
                0xF8,
                0xB7,
                0xA2,
                0x55,
                0x58,
                0x7E
            ];

            return (Guid*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    /// <summary>
    /// The vtable for the current instance.
    /// </summary>
    private readonly void** lpVtbl;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT QueryInterface(Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorFactory*, Guid*, void**, int>)this.lpVtbl[0])((ID3D12MemoryAllocatorFactory*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint AddRef()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorFactory*, uint>)this.lpVtbl[1])((ID3D12MemoryAllocatorFactory*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint Release()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorFactory*, uint>)this.lpVtbl[2])((ID3D12MemoryAllocatorFactory*)Unsafe.AsPointer(ref this));
    }

    /// <summary>
    /// Creates a new <see cref="ID3D12MemoryAllocator"/> for a given device.
    /// </summary>
    /// <param name="device">The device to create the allocator for.</param>
    /// <param name="adapter">The <see cref="IDXGIAdapter"/> for <paramref name="device"/>.</param>
    /// <param name="allocator">The resulting <see cref="ID3D12MemoryAllocator"/> instance.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT CreateAllocator(
        ID3D12Device* device,
        IDXGIAdapter* adapter,
        ID3D12MemoryAllocator** allocator)
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorFactory*, ID3D12Device*, IDXGIAdapter*, ID3D12MemoryAllocator**, int>)this.lpVtbl[3])(
            (ID3D12MemoryAllocatorFactory*)Unsafe.AsPointer(ref this),
            device,
            adapter,
            allocator);
    }
}