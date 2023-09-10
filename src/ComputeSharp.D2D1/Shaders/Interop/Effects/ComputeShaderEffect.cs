using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.D2D1.Shaders.Interop.Buffers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.D2D1.Shaders.Interop.Helpers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using RuntimeHelpers = ComputeSharp.NetStandard.RuntimeHelpers;
#endif

namespace ComputeSharp.D2D1.Interop.Effects;

/// <summary>
/// A simple <see cref="ID2D1EffectImpl"/> and <see cref="ID2D1ComputeTransform"/> implementation for a given compute shader.
/// </summary>
internal unsafe partial struct ComputeShaderEffect
{
#if !NET6_0_OR_GREATER
    /// <inheritdoc cref="QueryInterface"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int QueryInterfaceDelegate(ComputeShaderEffect* @this, Guid* riid, void** ppvObject);

    /// <inheritdoc cref="AddRef"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint AddRefDelegate(ComputeShaderEffect* @this);

    /// <inheritdoc cref="Release"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint ReleaseDelegate(ComputeShaderEffect* @this);
#endif

    /// <summary>
    /// The shared vtable pointer for <see cref="PixelShaderEffect"/> instance, for <see cref="ID2D1EffectImplMethods"/>.
    /// </summary>
    private static readonly void** VtblForID2D1EffectImpl = InitVtblForID2D1EffectImplAndID2D1ComputeTransform();

    /// <summary>
    /// The shared vtable pointer for <see cref="PixelShaderEffect"/> instance, for <see cref="ID2D1ComputeTransform"/>.
    /// </summary>
    private static readonly void** VtblForID2D1ComputeTransform = &VtblForID2D1EffectImpl[6];

    /// <summary>
    /// Initializes the combined vtable for <see cref="ID2D1EffectImpl"/> and <see cref="ID2D1ComputeTransform"/>.
    /// </summary>
    /// <returns>The combined vtable for <see cref="ID2D1EffectImpl"/> and <see cref="ID2D1ComputeTransform"/>.</returns>
    private static void** InitVtblForID2D1EffectImplAndID2D1ComputeTransform()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(ComputeShaderEffect), sizeof(void*) * 15);

        // ID2D1EffectImpl
#if NET6_0_OR_GREATER
        lpVtbl[0] = (delegate* unmanaged<ComputeShaderEffect*, Guid*, void**, int>)&ID2D1EffectImplMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<ComputeShaderEffect*, uint>)&ID2D1EffectImplMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged<ComputeShaderEffect*, uint>)&ID2D1EffectImplMethods.Release;
        lpVtbl[3] = (delegate* unmanaged<ComputeShaderEffect*, ID2D1EffectContext*, ID2D1TransformGraph*, int>)&ID2D1EffectImplMethods.Initialize;
        lpVtbl[4] = (delegate* unmanaged<ComputeShaderEffect*, D2D1_CHANGE_TYPE, int>)&ID2D1EffectImplMethods.PrepareForRender;
        lpVtbl[5] = (delegate* unmanaged<ComputeShaderEffect*, ID2D1TransformGraph*, int>)&ID2D1EffectImplMethods.SetGraph;
#else
        lpVtbl[0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.QueryInterfaceWrapper);
        lpVtbl[1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.AddRefWrapper);
        lpVtbl[2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.ReleaseWrapper);
        lpVtbl[3] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.InitializeWrapper);
        lpVtbl[4] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.PrepareForRenderWrapper);
        lpVtbl[5] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1EffectImplMethods.SetGraphWrapper);
#endif

        // ID2D1ComputeTransform
#if NET6_0_OR_GREATER
        lpVtbl[6 + 0] = (delegate* unmanaged<ComputeShaderEffect*, Guid*, void**, int>)&ID2D1ComputeTransformMethods.QueryInterface;
        lpVtbl[6 + 1] = (delegate* unmanaged<ComputeShaderEffect*, uint>)&ID2D1ComputeTransformMethods.AddRef;
        lpVtbl[6 + 2] = (delegate* unmanaged<ComputeShaderEffect*, uint>)&ID2D1ComputeTransformMethods.Release;
        lpVtbl[6 + 3] = (delegate* unmanaged<ComputeShaderEffect*, uint>)&ID2D1ComputeTransformMethods.GetInputCount;
        lpVtbl[6 + 4] = (delegate* unmanaged<ComputeShaderEffect*, RECT*, RECT*, uint, int>)&ID2D1ComputeTransformMethods.MapOutputRectToInputRects;
        lpVtbl[6 + 5] = (delegate* unmanaged<ComputeShaderEffect*, RECT*, RECT*, uint, RECT*, RECT*, int>)&ID2D1ComputeTransformMethods.MapInputRectsToOutputRect;
        lpVtbl[6 + 6] = (delegate* unmanaged<ComputeShaderEffect*, uint, RECT, RECT*, int>)&ID2D1ComputeTransformMethods.MapInvalidRect;
        lpVtbl[6 + 7] = (delegate* unmanaged<ComputeShaderEffect*, ID2D1ComputeInfo*, int>)&ID2D1ComputeTransformMethods.SetComputeInfo;
        lpVtbl[6 + 8] = (delegate* unmanaged<ComputeShaderEffect*, RECT*, uint*, uint*, uint*, int>)&ID2D1ComputeTransformMethods.CalculateThreadgroups;
#else
        lpVtbl[6 + 0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ComputeTransformMethods.QueryInterfaceWrapper);
        lpVtbl[6 + 1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ComputeTransformMethods.AddRefWrapper);
        lpVtbl[6 + 2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ComputeTransformMethods.ReleaseWrapper);
        lpVtbl[6 + 3] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ComputeTransformMethods.GetInputCountWrapper);
        lpVtbl[6 + 4] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ComputeTransformMethods.MapOutputRectToInputRectsWrapper);
        lpVtbl[6 + 5] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ComputeTransformMethods.MapInputRectsToOutputRectWrapper);
        lpVtbl[6 + 6] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ComputeTransformMethods.MapInvalidRectWrapper);
        lpVtbl[6 + 7] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ComputeTransformMethods.SetComputeInfoWrapper);
        lpVtbl[6 + 8] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ComputeTransformMethods.CalculateThreadgroupsWrapper);
#endif

        return lpVtbl;
    }

    /// <inheritdoc cref="PixelShaderEffect.lpVtblForID2D1EffectImpl"/>
    private void** lpVtblForID2D1EffectImpl;

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1ComputeTransform"/>.
    /// </summary>
    private void** lpVtblForID2D1ComputeTransform;

    /// <inheritdoc cref="PixelShaderEffect.referenceCount"/>
    private volatile int referenceCount;

    /// <inheritdoc cref="PixelShaderEffect.shaderId"/>
    private Guid shaderId;

    /// <inheritdoc cref="PixelShaderEffect.constantBuffer"/>
    private byte* constantBuffer;

    /// <inheritdoc cref="PixelShaderEffect.constantBufferSize"/>
    private int constantBufferSize;

    /// <inheritdoc cref="PixelShaderEffect.inputCount"/>
    private int inputCount;

    /// <inheritdoc cref="PixelShaderEffect.inputDescriptionCount"/>
    private int inputDescriptionCount;

    /// <inheritdoc cref="PixelShaderEffect.inputDescriptions"/>
    private D2D1InputDescription* inputDescriptions;

    /// <inheritdoc cref="PixelShaderEffect.bytecode"/>
    private byte* bytecode;

    /// <inheritdoc cref="PixelShaderEffect.bytecodeSize"/>
    private int bytecodeSize;

    /// <inheritdoc cref="PixelShaderEffect.bufferPrecision"/>
    private D2D1BufferPrecision bufferPrecision;

    /// <inheritdoc cref="PixelShaderEffect.channelDepth"/>
    private D2D1ChannelDepth channelDepth;

    /// <inheritdoc cref="PixelShaderEffect.resourceTextureDescriptionCount"/>
    private int resourceTextureDescriptionCount;

    /// <inheritdoc cref="PixelShaderEffect.resourceTextureDescriptions"/>
    private D2D1ResourceTextureDescription* resourceTextureDescriptions;

    /// <inheritdoc cref="PixelShaderEffect.d2D1TransformMapper"/>
    private ID2D1TransformMapper* d2D1TransformMapper;

    /// <summary>
    /// The <see cref="ID2D1ComputeInfo"/> instance currently in use.
    /// </summary>
    private ID2D1ComputeInfo* d2D1ComputeInfo;

    /// <inheritdoc cref="PixelShaderEffect.d2D1EffectContext"/>
    private ID2D1EffectContext* d2D1EffectContext;

    /// <inheritdoc cref="PixelShaderEffect.resourceTextureManagerBuffer"/>
    private ResourceTextureManagerBuffer resourceTextureManagerBuffer;

    /// <inheritdoc cref="PixelShaderEffect.Factory"/>
    private static int Factory(
        Guid shaderId,
        int constantBufferSize,
        int inputCount,
        int inputDescriptionCount,
        D2D1InputDescription* inputDescriptions,
        byte* bytecode,
        int bytecodeSize,
        D2D1BufferPrecision bufferPrecision,
        D2D1ChannelDepth channelDepth,
        int resourceTextureDescriptionCount,
        D2D1ResourceTextureDescription* resourceTextureDescriptions,
        IUnknown** effectImpl)
    {
        ComputeShaderEffect* @this;

        try
        {
            @this = (ComputeShaderEffect*)NativeMemory.Alloc((nuint)sizeof(ComputeShaderEffect));
        }
        catch (OutOfMemoryException)
        {
            *effectImpl = null;

            return E.E_OUTOFMEMORY;
        }

        *@this = default;

        @this->lpVtblForID2D1EffectImpl = VtblForID2D1EffectImpl;
        @this->lpVtblForID2D1ComputeTransform = VtblForID2D1ComputeTransform;
        @this->referenceCount = 1;
        @this->shaderId = shaderId;
        @this->constantBuffer = null;
        @this->constantBufferSize = constantBufferSize;
        @this->inputCount = inputCount;
        @this->inputDescriptionCount = inputDescriptionCount;
        @this->inputDescriptions = inputDescriptions;
        @this->bytecode = bytecode;
        @this->bytecodeSize = bytecodeSize;
        @this->bufferPrecision = bufferPrecision;
        @this->channelDepth = channelDepth;
        @this->resourceTextureDescriptionCount = resourceTextureDescriptionCount;
        @this->resourceTextureDescriptions = resourceTextureDescriptions;
        @this->d2D1TransformMapper = null;
        @this->d2D1ComputeInfo = null;
        @this->d2D1EffectContext = null;
        @this->resourceTextureManagerBuffer = default;

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

        // ID2D1EffectImpl
        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(Windows.__uuidof<ID2D1EffectImpl>()))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        // ID2D1ComputeTransform
        if (riid->Equals(Windows.__uuidof<ID2D1TransformNode>()) ||
            riid->Equals(Windows.__uuidof<ID2D1Transform>()) ||
            riid->Equals(Windows.__uuidof<ID2D1ComputeTransform>()))
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

            if (this.d2D1TransformMapper is not null)
            {
                _ = ((IUnknown*)this.d2D1TransformMapper)->Release();
            }

            if (this.d2D1ComputeInfo is not null)
            {
                _ = this.d2D1ComputeInfo->Release();
            }

            if (this.d2D1EffectContext is not null)
            {
                _ = this.d2D1EffectContext->Release();
            }

            D2D1ShaderEffect.ReleaseResourceTextureManagers(
                this.resourceTextureDescriptionCount,
                this.resourceTextureDescriptions,
                ref this.resourceTextureManagerBuffer);

            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }
}