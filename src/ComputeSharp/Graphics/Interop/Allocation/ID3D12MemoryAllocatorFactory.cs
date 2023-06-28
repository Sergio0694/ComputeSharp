using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ComputeSharp.Interop.Allocation;

/// <summary>
/// A factory type for <see cref="ID3D12MemoryAllocator"/> objects.
/// </summary>
[Guid("CC1E74A7-786D-40F4-8AE2-F8B7A255587E")]
internal unsafe struct ID3D12MemoryAllocatorFactory
{
    /// <summary>
    /// Gets the <see cref="System.Guid"/> for <see cref="ID3D12MemoryAllocatorFactory"/> (<c>CC1E74A7-786D-40F4-8AE2-F8B7A255587E</c>).
    /// </summary>
    public static ref readonly Guid Guid
    {
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
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
            };

            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    /// <summary>
    /// The vtable for the current instance.
    /// </summary>
    private readonly void** lpVtbl;

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
        return ((delegate* unmanaged[Stdcall]<ID3D12MemoryAllocatorFactory*, ID3D12Device*, IDXGIAdapter*, ID3D12MemoryAllocator**, int>)this.lpVtbl[3])(
            (ID3D12MemoryAllocatorFactory*)Unsafe.AsPointer(ref this),
            device,
            adapter,
            allocator);
    }
}