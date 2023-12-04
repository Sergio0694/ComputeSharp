using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;
using WinRT;
using WinRT.Interop;
using IInspectable = ComputeSharp.Win32.IInspectable;

#pragma warning disable CS0649, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// The activation factory for <see cref="global::Microsoft.Graphics.Canvas.CanvasDevice"/>.
/// </summary>
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
        return ((delegate* unmanaged[MemberFunction]<ICanvasFactoryNative*, Guid*, void**, int>)this.lpVtbl[0])((ICanvasFactoryNative*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasFactoryNative*, uint>)this.lpVtbl[1])((ICanvasFactoryNative*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasFactoryNative*, uint>)this.lpVtbl[2])((ICanvasFactoryNative*)Unsafe.AsPointer(ref this));
    }

    /// <summary>
    /// Gets or creates an <c>IInspectable</c> Win2D wrapper for a given native D2D resource.
    /// </summary>
    /// <param name="device">The input canvas device (as a marshalled <see cref="global::Microsoft.Graphics.Canvas.CanvasDevice"/>).</param>
    /// <param name="resource">The input native resource to create a wrapper for.</param>
    /// <param name="dpi">The realization DPIs for <paramref name="resource"/>.</param>
    /// <param name="wrapper">The resulting wrapper for <paramref name="resource"/>.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(6)]
    public HRESULT GetOrCreate(ICanvasDevice* device, IUnknown* resource, float dpi, IInspectable** wrapper)
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasFactoryNative*, ICanvasDevice*, IUnknown*, float, IInspectable**, int>)this.lpVtbl[6])(
            (ICanvasFactoryNative*)Unsafe.AsPointer(ref this),
            device,
            resource,
            dpi,
            wrapper);
    }

    /// <summary>
    /// Registers an <c>IInspectable</c> wrapper for a given native D2D resource.
    /// </summary>
    /// <param name="resource">The input native resource to register a wrapper for.</param>
    /// <param name="wrapper">The wrapper to register for <paramref name="resource"/>.</param>
    /// <returns>
    /// The <see cref="HRESULT"/> for the operation. In case of success, it will be <see cref="S.S_OK"/> if the wrapper
    /// was not previously registered, or <see cref="S.S_FALSE"/> if <paramref name="resource"/> was already registered.
    /// </returns>
    /// <remarks>
    /// The method validates at runtime that <paramref name="resource"/> is an <see cref="ID2D1Image"/>
    /// and that <paramref name="wrapper"/> is an <see cref="global::Microsoft.Graphics.Canvas.ICanvasImage"/>. Additionally,
    /// <paramref name="wrapper"/> has to implement <c>IWeakReferenceSource</c> for this method to be successful.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(7)]
    public HRESULT RegisterWrapper(IUnknown* resource, IInspectable* wrapper)
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasFactoryNative*, IUnknown*, IInspectable*, int>)this.lpVtbl[7])(
            (ICanvasFactoryNative*)Unsafe.AsPointer(ref this),
            resource,
            wrapper);
    }

    /// <summary>
    /// Unregisters a native D2D resource previously registered with <see cref="RegisterWrapper"/>.
    /// </summary>
    /// <param name="resource">The input native resource to unregister.</param>
    /// <returns>
    /// The <see cref="HRESULT"/> for the operation. In case of success, it will be <see cref="S.S_OK"/> if the
    /// resource was previously registered and could be correctly unregistered, <see cref="S.S_FALSE"/> otherwise.
    /// </returns>
    /// <remarks>
    /// The method validates at runtime that <paramref name="resource"/> is an <see cref="ID2D1Image"/>.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(8)]
    public HRESULT UnregisterWrapper(IUnknown* resource)
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasFactoryNative*, IUnknown*, int>)this.lpVtbl[8])(
            (ICanvasFactoryNative*)Unsafe.AsPointer(ref this),
            resource);
    }

    /// <summary>
    /// Registers a factory of wrappers for custom effects.
    /// </summary>
    /// <param name="effectId">The id of the effects to register a factory for.</param>
    /// <param name="factory">The input factory to create wrappers of native effects.</param>
    /// <returns>
    /// The <see cref="HRESULT"/> for the operation. In case of success, it will be <see cref="S.S_OK"/> if the factory
    /// was not previously registered, or <see cref="S.S_FALSE"/> if <paramref name="effectId"/> was already registered.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(9)]
    public HRESULT RegisterEffectFactory([NativeTypeName("const IID &")] Guid* effectId, ICanvasEffectFactoryNative* factory)
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasFactoryNative*, Guid*, ICanvasEffectFactoryNative*, int>)this.lpVtbl[9])(
            (ICanvasFactoryNative*)Unsafe.AsPointer(ref this),
            effectId,
            factory);
    }

    /// <summary>
    /// Unregisters an effect factory that was previosuly registered with <see cref="RegisterEffectFactory"/>.
    /// </summary>
    /// <param name="effectId">The id of the effects to unregister the factory for.</param>
    /// <returns>
    /// The <see cref="HRESULT"/> for the operation. In case of success, it will be <see cref="S.S_OK"/> if the
    /// factory was previously registered and could be correctly unregistered, <see cref="S.S_FALSE"/> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(10)]
    public HRESULT UnregisterEffectFactory([NativeTypeName("const IID &")] Guid* effectId)
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasFactoryNative*, Guid*, int>)this.lpVtbl[10])(
            (ICanvasFactoryNative*)Unsafe.AsPointer(ref this),
            effectId);
    }

    /// <summary>
    /// The managed interface for <see cref="ICanvasFactoryNative"/>.
    /// </summary>
    [Guid("695C440D-04B3-4EDD-BFD9-63E51E9F7202")]
    [WindowsRuntimeType]
    [WindowsRuntimeHelperType(typeof(Interface))]
    public interface Interface
    {
        /// <summary>
        /// The vtable type for <see cref="Interface"/>.
        /// </summary>
        [Guid("695C440D-04B3-4EDD-BFD9-63E51E9F7202")]
        public readonly struct Vftbl
        {
            /// <summary>
            /// Allows CsWinRT to retrieve a pointer to the projection vtable (the name is hardcoded by convention).
            /// </summary>
            public static readonly IntPtr AbiToProjectionVftablePtr = IUnknownVftbl.AbiToProjectionVftblPtr;
        }
    }
}