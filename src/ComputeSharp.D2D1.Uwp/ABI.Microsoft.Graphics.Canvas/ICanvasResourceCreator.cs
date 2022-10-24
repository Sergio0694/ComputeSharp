using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// The native WinRT interface for <see cref="global::Microsoft.Graphics.Canvas.ICanvasResourceCreator"/> objects.
/// </summary>
[Guid("8F6D8AA8-492F-4BC6-B3D0-E7F5EAE84B11")]
[NativeTypeName("class ICanvasResourceCreator : IInspectable")]
[NativeInheritance("IInspectable")]
internal unsafe struct ICanvasResourceCreator
{
    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceWrapperNative*, Guid*, void**, int>)this.lpVtbl[0])((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceWrapperNative*, uint>)this.lpVtbl[1])((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceWrapperNative*, uint>)this.lpVtbl[2])((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this));
    }
}