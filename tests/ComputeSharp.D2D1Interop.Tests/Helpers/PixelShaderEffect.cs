using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1Interop.Tests.Helpers;

/// <summary>
/// A simple <see cref="ID2D1EffectImpl"/> implementation for a given pixel shader.
/// </summary>
internal unsafe partial struct PixelShaderEffect
{
    /// <summary>
    /// The shared vtable pointer for <see cref="PixelShaderEffect"/> instances.
    /// </summary>
    private static readonly void** Vtbl = InitVtbl();

    /// <summary>
    /// Setups the vtable pointer for <see cref="PixelShaderEffect"/>.
    /// </summary>
    /// <returns>The initialized vtable pointer for <see cref="PixelShaderEffect"/>.</returns>
    private static void** InitVtbl()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(PixelShaderEffect), sizeof(void*) * 6);

        lpVtbl[0] = (delegate* unmanaged<PixelShaderEffect*, Guid*, void**, int>)&QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<PixelShaderEffect*, uint>)&AddRef;
        lpVtbl[2] = (delegate* unmanaged<PixelShaderEffect*, uint>)&Release;
        lpVtbl[3] = (delegate* unmanaged<PixelShaderEffect*, ID2D1EffectContext*, ID2D1TransformGraph*, int>)&Initialize;
        lpVtbl[4] = (delegate* unmanaged<PixelShaderEffect*, D2D1_CHANGE_TYPE, int>)&PrepareForRender;
        lpVtbl[5] = (delegate* unmanaged<PixelShaderEffect*, ID2D1TransformGraph*, int>)&SetGraph;

        return lpVtbl;
    }

    /// <summary>
    /// The vtable pointer for the current instance.
    /// </summary>
    private void** lpVtbl;

    /// <summary>
    /// The current reference count for the object (from <c>IUnknown</c>).
    /// </summary>
    private volatile int referenceCount;

    /// <summary>
    /// The linked <see cref="PixelShaderTransform"/> instance currently in use.
    /// </summary>
    private PixelShaderTransform* pixelShaderTransform;

    /// <summary>
    /// Creates and initializes a new <see cref="PixelShaderEffect"/> instance.
    /// </summary>
    /// <returns>A new <see cref="PixelShaderEffect"/> instance.</returns>
    private static PixelShaderEffect* Create()
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)NativeMemory.Alloc((nuint)sizeof(PixelShaderEffect));

        @this->lpVtbl = Vtbl;
        @this->referenceCount = 1;
        @this->pixelShaderTransform = null;

        return @this;
    }

    /// <summary>
    /// The factory method for <see cref="ID2D1Factory2.RegisterEffectFromString"/>.
    /// </summary>
    /// <param name="effectImpl">The resulting effect instance.</param>
    /// <returns>This always returns <c>0</c>.</returns>
    [UnmanagedCallersOnly]
    public static int Factory(IUnknown** effectImpl)
    {
        PixelShaderEffect* pixelShaderEffect = Create();
        PixelShaderTransform* pixelShaderTransform = PixelShaderTransform.Create();

        pixelShaderEffect->pixelShaderTransform = pixelShaderTransform;

        *effectImpl = (IUnknown*)pixelShaderEffect;

        return S.S_OK;
    }

    /// <inheritdoc cref="ID2D1EffectImpl.QueryInterface"/>
    [UnmanagedCallersOnly]
    private static int QueryInterface(PixelShaderEffect* @this, Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(Windows.__uuidof<ID2D1EffectImpl>()))
        {
            _ = Interlocked.Increment(ref @this->referenceCount);

            *ppvObject = @this;

            return S.S_OK;
        }

        *ppvObject = null;

        return E.E_NOINTERFACE;
    }

    /// <inheritdoc cref="ID2D1EffectImpl.AddRef"/>
    [UnmanagedCallersOnly]
    private static uint AddRef(PixelShaderEffect* @this)
    {
        return (uint)Interlocked.Increment(ref @this->referenceCount);
    }

    /// <inheritdoc cref="ID2D1EffectImpl.Release"/>
    [UnmanagedCallersOnly]
    private static uint Release(PixelShaderEffect* @this)
    {
        uint referenceCount = (uint)Interlocked.Decrement(ref @this->referenceCount);

        if (referenceCount == 0)
        {
            ((IUnknown*)@this->pixelShaderTransform)->Release();

            NativeMemory.Free(@this);
        }

        return referenceCount;
    }

    /// <inheritdoc cref="ID2D1EffectImpl.Initialize"/>
    [UnmanagedCallersOnly]
    private static int Initialize(PixelShaderEffect* @this, ID2D1EffectContext* effectContext, ID2D1TransformGraph* transformGraph)
    {
        ReadOnlyMemory<byte> bytecode = D2D1InteropServices.LoadShaderBytecode<InvertShader>();

        int hresult;

        fixed (byte* bytecodePtr = bytecode.Span)
        {
            hresult = effectContext->LoadPixelShader(
                shaderId: (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(typeof(InvertShader).GUID)),
                shaderBuffer: bytecodePtr,
                shaderBufferCount: (uint)bytecode.Length);
        }

        if (Windows.SUCCEEDED(hresult))
        {
            hresult = transformGraph->SetSingleTransformNode((ID2D1TransformNode*)@this->pixelShaderTransform);
        }

        return hresult;
    }

    /// <inheritdoc cref="ID2D1EffectImpl.PrepareForRender"/>
    [UnmanagedCallersOnly]
    private static int PrepareForRender(PixelShaderEffect* @this, D2D1_CHANGE_TYPE changeType)
    {
        return S.S_OK;
    }

    /// <inheritdoc cref="ID2D1EffectImpl.SetGraph"/>
    [UnmanagedCallersOnly]
    private static int SetGraph(PixelShaderEffect* @this, ID2D1TransformGraph* transformGraph)
    {
        return E.E_NOTIMPL;
    }
}