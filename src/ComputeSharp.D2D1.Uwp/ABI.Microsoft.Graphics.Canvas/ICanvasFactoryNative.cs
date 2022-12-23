using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// The activation factory for <see cref="global::Microsoft.Graphics.Canvas.CanvasDevice"/>.
/// </summary>
[Guid("695C440D-04B3-4EDD-BFD9-63E51E9F7202")]
[NativeTypeName("class ICanvasFactoryNative : public IInspectable")]
[NativeInheritance("IInspectable")]
internal unsafe struct ICanvasFactoryNative
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

    /// <summary>
    /// Gets or creates an <c>IInspectable</c> Win2D wrapper for a given native D2D resource.
    /// </summary>
    /// <param name="device">The input canvas device (as a marshalled <see cref="global::Microsoft.Graphics.Canvas.CanvasDevice"/>).</param>
    /// <param name="resource">The input native resource to create a wrapper for.</param>
    /// <param name="dpi">The realization DPIs for <paramref name="resource"/></param>
    /// <param name="wrapper">The resulting wrapper for <paramref name="resource"/>.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(6)]
    [return: NativeTypeName("ULONG")]
    public HRESULT GetOrCreate(ICanvasDevice* device, IUnknown* resource, float dpi, [NativeTypeName("IInspectable**")] void** wrapper)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasFactoryNative*, ICanvasDevice*, IUnknown*, float, void**, int>)this.lpVtbl[6])(
            (ICanvasFactoryNative*)Unsafe.AsPointer(ref this),
            device,
            resource,
            dpi,
            wrapper);
    }
}