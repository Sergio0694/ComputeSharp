using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// An interface for a COM object that provides access to a pooled <see cref="ID2D1DeviceContext"/> object.
/// </summary>
[Guid("A0928F38-F7D5-44DD-A5C9-E23D94734BBB")]
[NativeTypeName("class ID2D1DeviceContextLease : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe struct ID2D1DeviceContextLease
{
    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContextLease*, Guid*, void**, int>)this.lpVtbl[0])((ID2D1DeviceContextLease*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContextLease*, uint>)this.lpVtbl[1])((ID2D1DeviceContextLease*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContextLease*, uint>)this.lpVtbl[2])((ID2D1DeviceContextLease*)Unsafe.AsPointer(ref this));
    }

    /// <summary>
    /// Provides access to the <see cref="ID2D1DeviceContext"/> object wrapped by the current instance.
    /// </summary>
    /// <param name="deviceContext">The resulting <see cref="ID2D1DeviceContext"/> object wrapped by the current instance.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    /// <remarks>The returned <see cref="ID2D1DeviceContext"/> cannot be used once the owning lease has been released.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public HRESULT GetD2DDeviceContext(ID2D1DeviceContext** deviceContext)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DeviceContextLease*, ID2D1DeviceContext**, int>)this.lpVtbl[3])((ID2D1DeviceContextLease*)Unsafe.AsPointer(ref this), deviceContext);
    }
}