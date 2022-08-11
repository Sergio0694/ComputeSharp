using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if NET6_0_OR_GREATER
using RuntimeHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#else
using RuntimeHelpers = ComputeSharp.D2D1.NetStandard.System.Runtime.CompilerServices.RuntimeHelpers;
#endif

namespace ComputeSharp.D2D1.Interop.Effects;

/// <summary>
/// A simple <see cref="ID2D1EffectImpl"/> and <see cref="ID2D1DrawTransform"/> implementation for a given pixel shader.
/// </summary>
internal unsafe partial struct PixelShaderEffect
{
    /// <summary>
    /// A wrapper for an effect factory.
    /// </summary>
    /// <param name="effectImpl">The resulting effect factory.</param>
    /// <returns>The <c>HRESULT</c> for the operation.</returns>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int FactoryDelegate(IUnknown** effectImpl);

#if !NET6_0_OR_GREATER
    /// <inheritdoc cref="QueryInterface"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int QueryInterfaceDelegate(PixelShaderEffect* @this, Guid* riid, void** ppvObject);

    /// <inheritdoc cref="AddRef"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint AddRefDelegate(PixelShaderEffect* @this);

    /// <inheritdoc cref="Release"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint ReleaseDelegate(PixelShaderEffect* @this);
#endif

    /// <summary>
    /// The shared vtable pointer for <see cref="PixelShaderEffect"/> instance, for <see cref="ID2D1EffectImplMethods"/>.
    /// </summary>
    private static readonly void** VtblForID2D1EffectImpl;

    /// <summary>
    /// The shared vtable pointer for <see cref="PixelShaderEffect"/> instance, for <see cref="ID2D1DrawTransform"/>.
    /// </summary>
    private static readonly void** VtblForID2D1DrawTransform;

    /// <summary>
    /// Initializes the shared state for <see cref="PixelShaderEffect"/>.
    /// </summary>
    static PixelShaderEffect()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(PixelShaderEffect), sizeof(void*) * 14);

        // ID2D1EffectImpl
#if NET6_0_OR_GREATER
        lpVtbl[0] = (delegate* unmanaged<PixelShaderEffect*, Guid*, void**, int>)&ID2D1EffectImplMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<PixelShaderEffect*, uint>)&ID2D1EffectImplMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged<PixelShaderEffect*, uint>)&ID2D1EffectImplMethods.Release;
        lpVtbl[3] = (delegate* unmanaged<PixelShaderEffect*, ID2D1EffectContext*, ID2D1TransformGraph*, int>)&ID2D1EffectImplMethods.Initialize;
        lpVtbl[4] = (delegate* unmanaged<PixelShaderEffect*, D2D1_CHANGE_TYPE, int>)&ID2D1EffectImplMethods.PrepareForRender;
        lpVtbl[5] = (delegate* unmanaged<PixelShaderEffect*, ID2D1TransformGraph*, int>)&ID2D1EffectImplMethods.SetGraph;
#else
        lpVtbl[0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.QueryInterfaceWrapper);
        lpVtbl[1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.AddRefWrapper);
        lpVtbl[2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.ReleaseWrapper);
        lpVtbl[3] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.InitializeWrapper);
        lpVtbl[4] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.PrepareForRenderWrapper);
        lpVtbl[5] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.SetGraphWrapper);
#endif

        // ID2D1DrawTransform
#if NET6_0_OR_GREATER
        lpVtbl[6 + 0] = (delegate* unmanaged<PixelShaderEffect*, Guid*, void**, int>)&ID2D1DrawTransformMethods.QueryInterface;
        lpVtbl[6 + 1] = (delegate* unmanaged<PixelShaderEffect*, uint>)&ID2D1DrawTransformMethods.AddRef;
        lpVtbl[6 + 2] = (delegate* unmanaged<PixelShaderEffect*, uint>)&ID2D1DrawTransformMethods.Release;
        lpVtbl[6 + 3] = (delegate* unmanaged<PixelShaderEffect*, uint>)&ID2D1DrawTransformMethods.GetInputCount;
        lpVtbl[6 + 4] = (delegate* unmanaged<PixelShaderEffect*, RECT*, RECT*, uint, int>)&ID2D1DrawTransformMethods.MapOutputRectToInputRects;
        lpVtbl[6 + 5] = (delegate* unmanaged<PixelShaderEffect*, RECT*, RECT*, uint, RECT*, RECT*, int>)&ID2D1DrawTransformMethods.MapInputRectsToOutputRect;
        lpVtbl[6 + 6] = (delegate* unmanaged<PixelShaderEffect*, uint, RECT, RECT*, int>)&ID2D1DrawTransformMethods.MapInvalidRect;
        lpVtbl[6 + 7] = (delegate* unmanaged<PixelShaderEffect*, ID2D1DrawInfo*, int>)&ID2D1DrawTransformMethods.SetDrawInfo;
#else
        lpVtbl[6 + 0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1DrawTransformMethods.QueryInterfaceWrapper);
        lpVtbl[6 + 1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1DrawTransformMethods.AddRefWrapper);
        lpVtbl[6 + 2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1DrawTransformMethods.ReleaseWrapper);
        lpVtbl[6 + 3] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1DrawTransformMethods.GetInputCountWrapper);
        lpVtbl[6 + 4] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1DrawTransformMethods.MapOutputRectToInputRectsWrapper);
        lpVtbl[6 + 5] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1DrawTransformMethods.MapInputRectsToOutputRectWrapper);
        lpVtbl[6 + 6] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1DrawTransformMethods.MapInvalidRectWrapper);
        lpVtbl[6 + 7] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1DrawTransformMethods.SetDrawInfoWrapper);
#endif

        VtblForID2D1EffectImpl = lpVtbl;
        VtblForID2D1DrawTransform = &lpVtbl[6];
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
    /// The constant buffer data, if set.
    /// </summary>
    private byte* constantBuffer;

    /// <summary>
    /// The size of <see cref="constantBuffer"/>, if set.
    /// </summary>
    private int constantBufferSize;

    /// <summary>
    /// The <see cref="Guid"/> for the shader.
    /// </summary>
    private Guid shaderId;

    /// <summary>
    /// The number of inputs for the shader.
    /// </summary>
    private int inputCount;

    /// <summary>
    /// The buffer with the types of inputs for the shader.
    /// </summary>
    /// <remarks>
    /// This buffer is shared among effect instances, so it should not be released. It is
    /// owned by <see cref="For{T}"/>, which will release it when the target type is unloaded.
    /// </remarks>
    private D2D1PixelShaderInputType* inputTypes;

    /// <summary>
    /// The number of available input descriptions.
    /// </summary>
    private int inputDescriptionCount;

    /// <summary>
    /// The buffer with the available input descriptions for the shader.
    /// </summary>
    /// <remarks>
    /// This buffer is also shared among effect instances, so it should not be released. It is
    /// owned by <see cref="For{T}"/>, which will release it when the target type is unloaded.
    /// </remarks>
    private D2D1InputDescription* inputDescriptions;

    /// <summary>
    /// The pixel options for the shader.
    /// </summary>
    private D2D1PixelOptions pixelOptions;

    /// <summary>
    /// The shader bytecode.
    /// </summary>
    private byte* bytecode;

    /// <summary>
    /// The size of <see cref="bytecode"/>.
    /// </summary>
    private int bytecodeSize;

    /// <summary>
    /// The buffer precision for the resulting output buffer.
    /// </summary>
    private D2D1BufferPrecision bufferPrecision;

    /// <summary>
    /// The channel depth for the resulting output buffer.
    /// </summary>
    private D2D1ChannelDepth channelDepth;

    /// <summary>
    /// The handle for the <see cref="D2D1TransformMapper"/> instance in use, if any.
    /// </summary>
    private GCHandle d2D1TransformMapperHandle;

    /// <summary>
    /// The <see cref="ID2D1DrawInfo"/> instance currently in use.
    /// </summary>
    private ID2D1DrawInfo* d2D1DrawInfo;

    /// <summary>
    /// The <see cref="ID2D1EffectContext"/> instance currently in use.
    /// </summary>
    private ID2D1EffectContext* d2D1EffectContext;

    /// <summary>
    /// The factory method for <see cref="ID2D1Factory1.RegisterEffectFromString"/>.
    /// </summary>
    /// <param name="shaderId">The <see cref="Guid"/> for the shader.</param>
    /// <param name="inputCount">The number of inputs for the shader.</param>
    /// <param name="inputTypes">The buffer with the types of inputs for the shader.</param>
    /// <param name="inputDescriptionCount">The number of available input descriptions.</param>
    /// <param name="inputDescriptions">The buffer with the available input descriptions for the shader.</param>
    /// <param name="pixelOptions">The pixel options for the shader.</param>
    /// <param name="bytecode">The shader bytecode.</param>
    /// <param name="bytecodeSize">The size of <paramref name="bytecode"/>.</param>
    /// <param name="bufferPrecision">The buffer precision for the resulting output buffer.</param>
    /// <param name="channelDepth">The channel depth for the resulting output buffer.</param>
    /// <param name="d2D1TransformMapper">The <see cref="D2D1TransformMapper"/> instance to use for the effect.</param>
    /// <param name="effectImpl">The resulting effect instance.</param>
    /// <returns>This always returns <c>0</c>.</returns>
    private static int Factory(
        Guid shaderId,
        int inputCount,
        D2D1PixelShaderInputType* inputTypes,
        int inputDescriptionCount,
        D2D1InputDescription* inputDescriptions,
        D2D1PixelOptions pixelOptions,
        byte* bytecode,
        int bytecodeSize,
        D2D1BufferPrecision bufferPrecision,
        D2D1ChannelDepth channelDepth,
        D2D1TransformMapper? d2D1TransformMapper,
        IUnknown** effectImpl)
    {
        PixelShaderEffect* @this;

        try
        {
            @this = (PixelShaderEffect*)NativeMemory.Alloc((nuint)sizeof(PixelShaderEffect));
        }
        catch (OutOfMemoryException)
        {
            *effectImpl = null;

            return E.E_OUTOFMEMORY;
        }

        *@this = default;

        @this->lpVtblForID2D1EffectImpl = VtblForID2D1EffectImpl;
        @this->lpVtblForID2D1DrawTransform = VtblForID2D1DrawTransform;
        @this->referenceCount = 1;
        @this->shaderId = shaderId;
        @this->inputCount = inputCount;
        @this->inputTypes = inputTypes;
        @this->inputDescriptionCount = inputDescriptionCount;
        @this->inputDescriptions = inputDescriptions;
        @this->pixelOptions = pixelOptions;
        @this->bytecode = bytecode;
        @this->bytecodeSize = bytecodeSize;
        @this->bufferPrecision = bufferPrecision;
        @this->channelDepth = channelDepth;
        @this->d2D1TransformMapperHandle = GCHandle.Alloc(d2D1TransformMapper);
        @this->d2D1DrawInfo = null;
        @this->d2D1EffectContext = null;

        * effectImpl = (IUnknown*)@this;

        return S.S_OK;
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
            if (this.constantBuffer is not null)
            {
                NativeMemory.Free(this.constantBuffer);
            }

            this.d2D1TransformMapperHandle.Free();

            if (this.d2D1DrawInfo is not null)
            {
                _ = this.d2D1DrawInfo->Release();
            }

            if (this.d2D1EffectContext is not null)
            {
                _ = this.d2D1EffectContext->Release();
            }

            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }
}