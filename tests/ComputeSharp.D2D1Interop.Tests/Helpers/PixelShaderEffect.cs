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
    /// A generic pixel shader implementation.
    /// </summary>
    /// <typeparam name="T">The type of shader.</typeparam>
    public static class For<T>
        where T : unmanaged, ID2D1PixelShader
    {
        /// <summary>
        /// The <see cref="Guid"/> for the shader.
        /// </summary>
        private static readonly Guid shaderId;

        /// <summary>
        /// The shader bytecode.
        /// </summary>
        private static readonly byte* bytecode;

        /// <summary>
        /// The size of <see cref="bytecode"/>.
        /// </summary>
        private static readonly int bytecodeSize;

        /// <summary>
        /// The number of inputs for the shader.
        /// </summary>
        private static readonly int numberOfInputs;

        /// <summary>
        /// The <see cref="FactoryDelegate"/> wrapper for the shader factory.
        /// </summary>
        private static readonly FactoryDelegate EffectFactory = CreateEffect;

        /// <summary>
        /// The static constructor for <see cref="For{T}"/>.
        /// </summary>
        static For()
        {
            shaderId = typeof(T).GUID;

            ReadOnlyMemory<byte> buffer = D2D1InteropServices.LoadShaderBytecode<T>();

            bytecode = (byte*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(For<T>), buffer.Length);

            buffer.Span.CopyTo(new Span<byte>(bytecode, buffer.Length));

            bytecodeSize = buffer.Length;

            numberOfInputs = ((D2DInputCountAttribute[])typeof(T).GetCustomAttributes(typeof(D2DInputCountAttribute), false))[0].Count;
        }

        /// <summary>
        /// Gets the factory for the current effect.
        /// </summary>
        public static delegate* unmanaged<IUnknown**, HRESULT> Factory
        {
            get => (delegate* unmanaged<IUnknown**, HRESULT>)Marshal.GetFunctionPointerForDelegate(EffectFactory);
        }

        /// <inheritdoc cref="FactoryDelegate"/>
        private static int CreateEffect(IUnknown** effectImpl)
        {
            return PixelShaderEffect.Factory(shaderId, bytecode, bytecodeSize, numberOfInputs, effectImpl);
        }
    }

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
    /// The shader bytecode.
    /// </summary>
    private byte* bytecode;

    /// <summary>
    /// The size of <see cref="bytecode"/>.
    /// </summary>
    private int bytecodeSize;

    /// <summary>
    /// The number of inputs for the shader.
    /// </summary>
    private int numberOfInputs;

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
        @this->constantBuffer = null;
        @this->constantBufferSize = 0;

        return @this;
    }

    /// <summary>
    /// A wrapper for an effect factory.
    /// </summary>
    /// <param name="effectImpl">The resulting effect factory.</param>
    /// <returns>The <c>HRESULT</c> for the operation.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    private delegate int FactoryDelegate(IUnknown** effectImpl);

    /// <summary>
    /// The factory method for <see cref="ID2D1Factory2.RegisterEffectFromString"/>.
    /// </summary>
    /// <param name="shaderId">The <see cref="Guid"/> for the shader.</param>
    /// <param name="bytecode">The shader bytecode.</param>
    /// <param name="effectImpl">The resulting effect instance.</param>
    /// <param name="bytecodeSize">The size of <paramref name="bytecode"/>.</param>
    /// <param name="numberOfInpputs">The number of inputs for the shader.</param>
    /// <returns>This always returns <c>0</c>.</returns>
    private static int Factory(Guid shaderId, byte* bytecode, int bytecodeSize, int numberOfInpputs,  IUnknown** effectImpl)
    {
        PixelShaderEffect* pixelShaderEffect = Create();
        PixelShaderTransform* pixelShaderTransform = PixelShaderTransform.Create(shaderId, numberOfInpputs);

        pixelShaderEffect->pixelShaderTransform = pixelShaderTransform;
        pixelShaderEffect->shaderId = shaderId;
        pixelShaderEffect->bytecode = bytecode;
        pixelShaderEffect->bytecodeSize = bytecodeSize;
        pixelShaderEffect->numberOfInputs = numberOfInpputs;

        *effectImpl = (IUnknown*)pixelShaderEffect;

        return S.S_OK;
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetConstantBuffer(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        if (@this->constantBufferSize == 0)
        {
            *actualSize = 0;
        }
        else
        {
            int bytesToCopy = Math.Min((int)dataSize, @this->constantBufferSize);

            Buffer.MemoryCopy(@this->constantBuffer, data, dataSize, bytesToCopy);

            *actualSize = (uint)bytesToCopy;
        }

        return S.S_OK;
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetConstantBuffer(IUnknown* effect, byte* data, uint dataSize)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        if (@this->constantBuffer is not null)
        {
            NativeMemory.Free(@this->constantBuffer);
        }

        void* buffer = NativeMemory.Alloc(dataSize);

        Buffer.MemoryCopy(data, buffer, dataSize, dataSize);

        @this->constantBuffer = (byte*)buffer;
        @this->constantBufferSize = (int)dataSize;

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

            if (@this->constantBuffer is not null)
            {
                NativeMemory.Free(@this->constantBuffer);
            }

            NativeMemory.Free(@this);
        }

        return referenceCount;
    }

    /// <inheritdoc cref="ID2D1EffectImpl.Initialize"/>
    [UnmanagedCallersOnly]
    private static int Initialize(PixelShaderEffect* @this, ID2D1EffectContext* effectContext, ID2D1TransformGraph* transformGraph)
    {
        int hresult = effectContext->LoadPixelShader(
            shaderId: &@this->shaderId,
            shaderBuffer: @this->bytecode,
            shaderBufferCount: (uint)@this->bytecodeSize);

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
        if (@this->constantBuffer is not null)
        {
            return @this->pixelShaderTransform->D2D1DrawInfo->SetPixelShaderConstantBuffer(
                buffer: @this->constantBuffer,
                bufferCount: (uint)@this->constantBufferSize);
        }

        return S.S_OK;
    }

    /// <inheritdoc cref="ID2D1EffectImpl.SetGraph"/>
    [UnmanagedCallersOnly]
    private static int SetGraph(PixelShaderEffect* @this, ID2D1TransformGraph* transformGraph)
    {
        return E.E_NOTIMPL;
    }
}