using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using TerraFX.Interop.WinRT;

#pragma warning disable CS0649, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// The interop Win2D interface for factories of external effects.
/// </summary>
[Guid("29BA1A1F-1CFE-44C3-984D-426D61B51427")]
[NativeTypeName("class ICanvasEffectFactoryNative : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe struct ICanvasEffectFactoryNative
{
    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasEffectFactoryNative*, Guid*, void**, int>)this.lpVtbl[0])((ICanvasEffectFactoryNative*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasEffectFactoryNative*, uint>)this.lpVtbl[1])((ICanvasEffectFactoryNative*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasEffectFactoryNative*, uint>)this.lpVtbl[2])((ICanvasEffectFactoryNative*)Unsafe.AsPointer(ref this));
    }

    /// <summary>
    /// Creates a new inspectable wrapper for an inpute D2D effect previosly registered through <see cref="ICanvasFactoryNative.RegisterEffectFactory"/>.
    /// </summary>
    /// <param name="device">The input canvas device.</param>
    /// <param name="resource">The input native effect to create a wrapper for.</param>
    /// <param name="dpi">The realization DPIs for <paramref name="resource"/>.</param>
    /// <param name="wrapper">The resulting wrapper for <paramref name="resource"/>.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    /// <remarks>
    /// All parameters are directly forwarded from the ones the caller passed to <see cref="ICanvasFactoryNative.GetOrCreate"/>. The returned
    /// wrapper should implement <see cref="global::Microsoft.Graphics.Canvas.ICanvasImage"/> to be returned correctly from Win2D after this call.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public HRESULT CreateWrapper(ICanvasDevice* device, ID2D1Effect* resource, float dpi, IInspectable** wrapper)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasEffectFactoryNative*, ICanvasDevice*, ID2D1Effect*, float, IInspectable**, int>)this.lpVtbl[3])(
            (ICanvasEffectFactoryNative*)Unsafe.AsPointer(ref this),
            device,
            resource,
            dpi,
            wrapper);
    }
}