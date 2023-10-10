using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// The native WinRT interface for <see cref="global::Microsoft.Graphics.Canvas.CanvasDevice"/> objects.
/// </summary>
[Guid("A27F0B5D-EC2C-4D4F-948F-0AA1E95E33E6")]
[NativeTypeName("class ICanvasDevice : IInspectable")]
[NativeInheritance("IInspectable")]
internal unsafe struct ICanvasDevice
{
    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasDevice*, Guid*, void**, int>)this.lpVtbl[0])((ICanvasDevice*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasDevice*, uint>)this.lpVtbl[1])((ICanvasDevice*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasDevice*, uint>)this.lpVtbl[2])((ICanvasDevice*)Unsafe.AsPointer(ref this));
    }
}