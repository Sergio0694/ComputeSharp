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
/// An allocation wrapper for a native <see cref="ID3D12Resource"/> object.
/// </summary>
[Guid("D42D5782-2DE7-4539-A817-482E3AA01E2E")]
internal unsafe struct ID3D12Allocation
#if NET6_0_OR_GREATER
    : IUnknown.Interface
#endif
{
    /// <summary>
    /// Gets the <see cref="System.Guid"/> for <see cref="ID3D12Allocation"/> (<c>D42D5782-2DE7-4539-A817-482E3AA01E2E</c>).
    /// </summary>
    public static ref readonly Guid Guid
    {
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
                0x82, 0x57, 0x2D, 0xD4,
                0xE7, 0x2D,
                0x39, 0x45,
                0xA8, 0x17,
                0x48,
                0x2E,
                0x3A,
                0xA0,
                0x1E,
                0x2E
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
        return ((delegate* unmanaged[Stdcall]<ID3D12Allocation*, Guid*, void**, int>)this.lpVtbl[0])((ID3D12Allocation*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12Allocation*, uint>)this.lpVtbl[1])((ID3D12Allocation*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12Allocation*, uint>)this.lpVtbl[2])((ID3D12Allocation*)Unsafe.AsPointer(ref this));
    }

    /// <summary>
    /// Gets the underlying <see cref="ID3D12Resource"/> for the current instance.
    /// </summary>
    /// <param name="resource">The resulting <see cref="ID3D12Resource"/> object.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    /// <remarks>
    /// The returned resource should not be used after the current allocation instance has been released.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT GetD3D12Resource(ID3D12Resource** resource)
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12Allocation*, ID3D12Resource**, int>)this.lpVtbl[3])(
            (ID3D12Allocation*)Unsafe.AsPointer(ref this),
            resource);
    }
}