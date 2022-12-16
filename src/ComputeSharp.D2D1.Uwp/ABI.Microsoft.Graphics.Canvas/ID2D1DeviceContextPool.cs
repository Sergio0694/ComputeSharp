using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// An interface for an object that provides access to pooled <see cref="TerraFX.Interop.DirectX.ID2D1DeviceContext"/> objects.
/// </summary>
[Guid("454A82A1-F024-40DB-BD5B-8F527FD58AD0")]
[NativeTypeName("class ID2D1DeviceContextPool : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe struct ID2D1DeviceContextPool
{
    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContextPool*, Guid*, void**, int>)this.lpVtbl[0])((ID2D1DeviceContextPool*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContextPool*, uint>)this.lpVtbl[1])((ID2D1DeviceContextPool*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContextPool*, uint>)this.lpVtbl[2])((ID2D1DeviceContextPool*)Unsafe.AsPointer(ref this));
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
        return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContextPool*, ID2D1DeviceContextLease**, int>)this.lpVtbl[3])((ID2D1DeviceContextPool*)Unsafe.AsPointer(ref this), lease);
    }
}