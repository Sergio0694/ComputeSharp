using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// An interface for an object that provides access to pooled <see cref="ID2D1DeviceContext"/> objects.
/// </summary>
[NativeTypeName("class ID2D1DeviceContextPool : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe struct ID2D1DeviceContextPool : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xA1, 0x82, 0x4A, 0x45,
                0x24, 0xF0,
                0xDB, 0x40,
                0xBD,
                0x5B,
                0x8F,
                0x52,
                0x7F,
                0xD5,
                0x8A,
                0xD0
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1DeviceContextPool*, Guid*, void**, int>)this.lpVtbl[0])((ID2D1DeviceContextPool*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1DeviceContextPool*, uint>)this.lpVtbl[1])((ID2D1DeviceContextPool*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1DeviceContextPool*, uint>)this.lpVtbl[2])((ID2D1DeviceContextPool*)Unsafe.AsPointer(ref this));
    }

    /// <summary>
    /// Retrieves a new <see cref="ID2D1DeviceContextLease"/> object from the current instance.
    /// </summary>
    /// <param name="lease">The resulting <see cref="ID2D1DeviceContextLease"/> object to use.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public HRESULT GetDeviceContextLease(ID2D1DeviceContextLease** lease)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1DeviceContextPool*, ID2D1DeviceContextLease**, int>)this.lpVtbl[3])((ID2D1DeviceContextPool*)Unsafe.AsPointer(ref this), lease);
    }
}