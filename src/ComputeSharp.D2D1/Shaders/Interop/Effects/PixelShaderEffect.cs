using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Interop.Effects;

/// <summary>
/// A simple <see cref="ID2D1EffectImpl"/> and <see cref="ID2D1DrawTransform"/> implementation for a given pixel shader.
/// </summary>
internal unsafe partial struct PixelShaderEffect
{
    /// <summary>
    /// The shared vtable pointer for <see cref="PixelShaderEffect"/> instance, for <see cref="ID2D1EffectImplMethods"/>.
    /// </summary>
    private static readonly void** VtblForID2D1EffectImpl = InitVtblForID2D1EffectImplAndID2D1DrawTransform();

    /// <summary>
    /// The shared vtable pointer for <see cref="PixelShaderEffect"/> instance, for <see cref="ID2D1DrawTransform"/>.
    /// </summary>
    private static readonly void** VtblForID2D1DrawTransform = &VtblForID2D1EffectImpl[6];

    /// <summary>
    /// Initializes the combined vtable for <see cref="ID2D1EffectImpl"/> and <see cref="ID2D1DrawTransform"/>.
    /// </summary>
    /// <returns>The combined vtable for <see cref="ID2D1EffectImpl"/> and <see cref="ID2D1DrawTransform"/>.</returns>
    private static void** InitVtblForID2D1EffectImplAndID2D1DrawTransform()
    {
        void** lpVtbl = (void**)D2D1AssemblyAssociatedMemory.Allocate(sizeof(void*) * 14);

        // ID2D1EffectImpl
        lpVtbl[0] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, Guid*, void**, int>)&ID2D1EffectImplMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint>)&ID2D1EffectImplMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint>)&ID2D1EffectImplMethods.Release;
        lpVtbl[3] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, ID2D1EffectContext*, ID2D1TransformGraph*, int>)&ID2D1EffectImplMethods.Initialize;
        lpVtbl[4] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, D2D1_CHANGE_TYPE, int>)&ID2D1EffectImplMethods.PrepareForRender;
        lpVtbl[5] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, ID2D1TransformGraph*, int>)&ID2D1EffectImplMethods.SetGraph;

        // ID2D1DrawTransform
        lpVtbl[6 + 0] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, Guid*, void**, int>)&ID2D1DrawTransformMethods.QueryInterface;
        lpVtbl[6 + 1] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint>)&ID2D1DrawTransformMethods.AddRef;
        lpVtbl[6 + 2] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint>)&ID2D1DrawTransformMethods.Release;
        lpVtbl[6 + 3] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint>)&ID2D1DrawTransformMethods.GetInputCount;
        lpVtbl[6 + 4] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, RECT*, RECT*, uint, int>)&ID2D1DrawTransformMethods.MapOutputRectToInputRects;
        lpVtbl[6 + 5] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, RECT*, RECT*, uint, RECT*, RECT*, int>)&ID2D1DrawTransformMethods.MapInputRectsToOutputRect;
        lpVtbl[6 + 6] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint, RECT, RECT*, int>)&ID2D1DrawTransformMethods.MapInvalidRect;
        lpVtbl[6 + 7] = (delegate* unmanaged[MemberFunction]<PixelShaderEffect*, ID2D1DrawInfo*, int>)&ID2D1DrawTransformMethods.SetDrawInfo;

        return lpVtbl;
    }

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1Effect"/>.
    /// </summary>
    private void** lpVtblForID2D1EffectImpl;

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1DrawTransform"/>.
    /// </summary>
    private void** lpVtblForID2D1DrawTransform;

    /// <summary>
    /// The current reference count for the object (from <c>IUnknown</c>).
    /// </summary>
    private volatile int referenceCount;

    /// <summary>
    /// The <see cref="GCHandle"/> for the <see cref="Globals"/> instance in use.
    /// </summary>
    private GCHandle globalsHandle;

    /// <summary>
    /// The constant buffer data, if set.
    /// </summary>
    private byte* constantBuffer;

    /// <summary>
    /// The <see cref="ID2D1DrawTransformMapper"/> instance to use, if any.
    /// </summary>
    private ComPtr<ID2D1DrawTransformMapper> d2D1TransformMapper;

    /// <summary>
    /// The <see cref="ID2D1DrawInfo"/> instance currently in use.
    /// </summary>
    private ComPtr<ID2D1DrawInfo> d2D1DrawInfo;

    /// <summary>
    /// The <see cref="ID2D1EffectContext"/> instance currently in use.
    /// </summary>
    private ComPtr<ID2D1EffectContext> d2D1EffectContext;

    /// <summary>
    /// The resource texture managers for the current instance.
    /// </summary>
    private ResourceTextureManagerBuffer resourceTextureManagerBuffer;

    /// <summary>
    /// The factory method for <see cref="ID2D1Factory1.RegisterEffectFromString"/>.
    /// </summary>
    /// <param name="globals">The <see cref="Globals"/> instance to use.</param>
    /// <param name="effectImpl">The resulting effect instance.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static int Factory(Globals globals, IUnknown** effectImpl)
    {
        PixelShaderEffect* @this = null;
        GCHandle globalsHandle = default;

        try
        {
            @this = (PixelShaderEffect*)NativeMemory.Alloc((nuint)sizeof(PixelShaderEffect));
            globalsHandle = GCHandle.Alloc(globals);

            @this->lpVtblForID2D1EffectImpl = VtblForID2D1EffectImpl;
            @this->lpVtblForID2D1DrawTransform = VtblForID2D1DrawTransform;
            @this->referenceCount = 1;
            @this->globalsHandle = globalsHandle;
            @this->constantBuffer = null;
            @this->d2D1TransformMapper = default;
            @this->d2D1DrawInfo = default;
            @this->d2D1EffectContext = default;
            @this->resourceTextureManagerBuffer = default;

            *effectImpl = (IUnknown*)@this;

            return S.S_OK;
        }
        catch (Exception e)
        {
            // Free the effect, if we have one
            NativeMemory.Free(@this);

            // Free the handle, if we have one
            if (globalsHandle.IsAllocated)
            {
                globalsHandle.Free();
            }

            *effectImpl = null;

            return Marshal.GetHRForException(e);
        }
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    private int QueryInterface(Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        // ID2D1EffectImpl
        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(Windows.__uuidof<ID2D1EffectImpl>()))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        // ID2D1DrawTransform
        if (riid->Equals(Windows.__uuidof<ID2D1TransformNode>()) ||
            riid->Equals(Windows.__uuidof<ID2D1Transform>()) ||
            riid->Equals(Windows.__uuidof<ID2D1DrawTransform>()))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = (void**)Unsafe.AsPointer(ref this) + 1;

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
            this.globalsHandle.Free();

            NativeMemory.Free(this.constantBuffer);

            this.d2D1TransformMapper.Dispose();
            this.d2D1DrawInfo.Dispose();
            this.d2D1EffectContext.Dispose();

            // Retrieve all possible resource texture managers in use and release the ones that had been
            // assigned (from one of the property bindings). We just try to dispose all of them here and
            // don't access the globals, as technically invoking APIs on it might throw an exception.
            foreach (ref ComPtr<ID2D1ResourceTextureManager> resourceTextureManager in (Span<ComPtr<ID2D1ResourceTextureManager>>)this.resourceTextureManagerBuffer)
            {
                resourceTextureManager.Dispose();
            }

            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }

    /// <summary>
    /// Gets the <see cref="Globals"/> instance for the current effect.
    /// </summary>
    /// <returns>The <see cref="Globals"/> instance for the current effect.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Globals GetGlobals()
    {
        return Unsafe.As<Globals>(this.globalsHandle.Target!);
    }
}