using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using RuntimeHelpers = ComputeSharp.NetStandard.RuntimeHelpers;
#endif
using Win32 = TerraFX.Interop.Windows.Windows;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.Interop.Effects;
#else
namespace ComputeSharp.D2D1.WinUI.Interop.Effects;
#endif

/// <summary>
/// A simple <see cref="ID2D1EffectImpl"/> providing a passthrough over an input source.
/// </summary>
internal unsafe partial struct PassthroughEffect
{
#if !NET6_0_OR_GREATER
    /// <summary>
    /// A wrapper for an effect factory.
    /// </summary>
    /// <param name="effectImpl">The resulting effect factory.</param>
    /// <returns>The <c>HRESULT</c> for the operation.</returns>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int FactoryDelegate(IUnknown** effectImpl);

    /// <summary>
    /// A cached <see cref="FactoryDelegate"/> instance wrapping <see cref="CreateEffect"/>.
    /// </summary>
    private static readonly FactoryDelegate CreateEffectWrapper = CreateEffect;
#endif

    /// <summary>
    /// The shared method table pointer for all <see cref="PassthroughEffect"/> instances.
    /// </summary>
    private static readonly void** Vtbl = InitVtbl();

    /// <summary>
    /// Builds the custom method table pointer for <see cref="PassthroughEffect"/>.
    /// </summary>
    /// <returns>The method table pointer for <see cref="PassthroughEffect"/>.</returns>
    private static void** InitVtbl()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(PassthroughEffect), sizeof(void*) * 6);

#if NET6_0_OR_GREATER
        lpVtbl[0] = (delegate* unmanaged<PassthroughEffect*, Guid*, void**, int>)&ID2D1EffectImplMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<PassthroughEffect*, uint>)&ID2D1EffectImplMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged<PassthroughEffect*, uint>)&ID2D1EffectImplMethods.Release;
        lpVtbl[3] = (delegate* unmanaged<PassthroughEffect*, ID2D1EffectContext*, ID2D1TransformGraph*, int>)&ID2D1EffectImplMethods.Initialize;
        lpVtbl[4] = (delegate* unmanaged<PassthroughEffect*, D2D1_CHANGE_TYPE, int>)&ID2D1EffectImplMethods.PrepareForRender;
        lpVtbl[5] = (delegate* unmanaged<PassthroughEffect*, ID2D1TransformGraph*, int>)&ID2D1EffectImplMethods.SetGraph;
#else
        lpVtbl[0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.QueryInterfaceWrapper);
        lpVtbl[1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.AddRefWrapper);
        lpVtbl[2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.ReleaseWrapper);
        lpVtbl[3] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.InitializeWrapper);
        lpVtbl[4] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.PrepareForRenderWrapper);
        lpVtbl[5] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.SetGraphWrapper);
#endif

        return lpVtbl;
    }

    /// <summary>
    /// The method table pointer for the current instance.
    /// </summary>
    private void** lpVtbl;

    /// <summary>
    /// The current reference count for the object (from <c>IUnknown</c>).
    /// </summary>
    private volatile int referenceCount;

    /// <summary>
    /// Gets the factory for the current effect.
    /// </summary>
#if NET6_0_OR_GREATER
    public static delegate* unmanaged[Stdcall]<IUnknown**, HRESULT> Factory
#else
    public static void* Factory
#endif
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
        get => (delegate* unmanaged[Stdcall]<IUnknown**, HRESULT>)(delegate* unmanaged<IUnknown**, int>)&CreateEffect;
#else
        get => (void*)Marshal.GetFunctionPointerForDelegate(CreateEffectWrapper);
#endif
    }

    /// <summary>
    /// The factory method for <see cref="ID2D1Factory1.RegisterEffectFromString"/>.
    /// </summary>
    /// <param name="effectImpl">The resulting effect instance.</param>
    /// <returns>This always returns <c>0</c>.</returns>
    [UnmanagedCallersOnly]
    private static int CreateEffect(IUnknown** effectImpl)
    {
        PassthroughEffect* @this;

        try
        {
            @this = (PassthroughEffect*)NativeMemory.Alloc((nuint)sizeof(PassthroughEffect));
        }
        catch (OutOfMemoryException)
        {
            *effectImpl = null;

            return E.E_OUTOFMEMORY;
        }

        *@this = default;

        @this->lpVtbl = Vtbl;
        @this->referenceCount = 1;

        *effectImpl = (IUnknown*)@this;

        return S.S_OK;
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    private int QueryInterface(Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        if (riid->Equals(Win32.__uuidof<IUnknown>()) ||
            riid->Equals(Win32.__uuidof<ID2D1EffectImpl>()))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        *ppvObject = null;

        return E.E_NOINTERFACE;
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    private uint AddRef()
    {
        return (uint)Interlocked.Increment(ref this.referenceCount);
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    private uint Release()
    {
        uint referenceCount = (uint)Interlocked.Decrement(ref this.referenceCount);

        if (referenceCount == 0)
        {
            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }
}