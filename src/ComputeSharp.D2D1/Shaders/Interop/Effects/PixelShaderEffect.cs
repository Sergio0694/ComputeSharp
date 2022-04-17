using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.D2D1.Helpers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if NET6_0_OR_GREATER
using RuntimeHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#else
using RuntimeHelpers = ComputeSharp.D2D1.NetStandard.System.Runtime.CompilerServices.RuntimeHelpers;
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
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

    /// <inheritdoc cref="GetConstantBuffer"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int GetConstantBufferDelegate(IUnknown* effect, byte* data, uint dataSize, uint* actualSize);

    /// <inheritdoc cref="SetConstantBuffer"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int SetConstantBufferDelegate(IUnknown* effect, byte* data, uint dataSize);

    /// <summary>
    /// A cached <see cref="GetConstantBufferDelegate"/> instance wrapping <see cref="GetConstantBuffer"/>.
    /// </summary>
    public static readonly GetConstantBufferDelegate GetConstantBufferWrapper = GetConstantBuffer;

    /// <summary>
    /// A cached <see cref="SetConstantBufferDelegate"/> instance wrapping <see cref="SetConstantBuffer"/>.
    /// </summary>
    public static readonly SetConstantBufferDelegate SetConstantBufferWrapper = SetConstantBuffer;
#endif

    /// <summary>
    /// A generic pixel shader implementation.
    /// </summary>
    /// <typeparam name="T">The type of shader.</typeparam>
    public static class For<T>
        where T : unmanaged, ID2D1PixelShader
    {
        /// <summary>
        /// The <see cref="FactoryDelegate"/> wrapper for the shader factory.
        /// </summary>
        private static readonly FactoryDelegate EffectFactory = CreateEffect;

        /// <summary>
        /// Indicates whether or not initialization has completed.
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// The <see cref="Guid"/> for the shader.
        /// </summary>
        private static Guid shaderId;

        /// <summary>
        /// The number of inputs for the shader.
        /// </summary>
        private static int numberOfInputs;

        /// <summary>
        /// The shader bytecode.
        /// </summary>
        private static byte* bytecode;

        /// <summary>
        /// The size of <see cref="bytecode"/>.
        /// </summary>
        private static int bytecodeSize;

        /// <summary>
        /// The factory of <see cref="ID2D1DrawTransformMapper"/> instances to use for each created effect.
        /// </summary>
        private static Func<ID2D1DrawTransformMapper>? d2D1DrawTransformMapperFactory;

        /// <summary>
        /// Initializes the <see cref="For{T}"/> shared state.
        /// </summary>
        /// <param name="factory">The factory of <see cref="ID2D1DrawTransformMapper"/> instances to use for each created effect.</param>
        /// <exception cref="InvalidOperationException">Thrown if initialization is attempted with a mismatched transform factory.</exception>
        public static void Initialize(Func<ID2D1DrawTransformMapper>? factory)
        {
            // This conceptually acts as a static constructor, and this type is
            // internal, so in this very specific case locking on the type is fine.
            lock (typeof(For<T>))
            {
                if (isInitialized)
                {
                    // If the factory is already initialized, ensure the draw transform mapper is the same
                    if (d2D1DrawTransformMapperFactory != factory)
                    {
                        ThrowHelper.ThrowInvalidOperationException(
                            "Cannot initialize an ID2D1Effect factory for the same shader type with two different transform mappings. " +
                            "Make sure to only ever register a pixel shader effect with either no transform, or the same transform type.");
                    }
                }
                else
                {
                    // Load all shader properties
                    Guid guid = Guid.NewGuid();
                    int inputCount = ((D2DInputCountAttribute[])typeof(T).GetCustomAttributes(typeof(D2DInputCountAttribute), false))[0].Count;
                    ReadOnlyMemory<byte> buffer = D2D1InteropServices.LoadShaderBytecode<T>();
                    byte* typeAssociatedMemory = (byte*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(For<T>), buffer.Length);

                    // Copy the bytecode to the target buffer
                    buffer.Span.CopyTo(new Span<byte>(typeAssociatedMemory, buffer.Length));

                    // Set the shared state and mark the type as initialized
                    shaderId = guid;
                    numberOfInputs = inputCount;
                    bytecode = typeAssociatedMemory;
                    bytecodeSize = buffer.Length;
                    d2D1DrawTransformMapperFactory = factory;

                    isInitialized = true;
                }
            }
        }

        /// <summary>
        /// Gets the factory for the current effect.
        /// </summary>
        public static delegate* unmanaged[Stdcall]<IUnknown**, HRESULT> Factory
        {
            get => (delegate* unmanaged[Stdcall]<IUnknown**, HRESULT>)Marshal.GetFunctionPointerForDelegate(EffectFactory);
        }

        /// <summary>
        /// Gets the number of inputs for the effect.
        /// </summary>
        public static int NumberOfInputs => numberOfInputs;

        /// <inheritdoc cref="FactoryDelegate"/>
        private static int CreateEffect(IUnknown** effectImpl)
        {
            ID2D1DrawTransformMapper? d2D1DrawTransformMapper = d2D1DrawTransformMapperFactory?.Invoke();

            return PixelShaderEffect.Factory(shaderId, numberOfInputs, bytecode, bytecodeSize, d2D1DrawTransformMapper, effectImpl);
        }
    }

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
    private int numberOfInputs;

    /// <summary>
    /// The shader bytecode.
    /// </summary>
    private byte* bytecode;

    /// <summary>
    /// The size of <see cref="bytecode"/>.
    /// </summary>
    private int bytecodeSize;

    /// <summary>
    /// The handle for the <see cref="ID2D1DrawTransformMapper"/> instance in use, if any.
    /// </summary>
    private GCHandle d2D1DrawTransformMapperFactoryHandle;

    /// <summary>
    /// The current input rectangle.
    /// </summary>
    private RECT inputRect;

    /// <summary>
    /// The <see cref="ID2D1DrawInfo"/> instance currently in use.
    /// </summary>
    private ID2D1DrawInfo* d2D1DrawInfo;

    /// <summary>
    /// The factory method for <see cref="ID2D1Factory1.RegisterEffectFromString"/>.
    /// </summary>
    /// <param name="shaderId">The <see cref="Guid"/> for the shader.</param>
    /// <param name="numberOfInputs">The number of inputs for the shader.</param>
    /// <param name="bytecode">The shader bytecode.</param>
    /// <param name="bytecodeSize">The size of <paramref name="bytecode"/>.</param>
    /// <param name="d2D1DrawTransformMapper">The <see cref="ID2D1DrawTransformMapper"/> instance to use for the effect.</param>
    /// <param name="effectImpl">The resulting effect instance.</param>
    /// <returns>This always returns <c>0</c>.</returns>
    private static int Factory(
        Guid shaderId,
        int numberOfInputs,
        byte* bytecode,
        int bytecodeSize,
        ID2D1DrawTransformMapper? d2D1DrawTransformMapper,
        IUnknown** effectImpl)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)NativeMemory.Alloc((nuint)sizeof(PixelShaderEffect));

        *@this = default;

        @this->lpVtblForID2D1EffectImpl = VtblForID2D1EffectImpl;
        @this->lpVtblForID2D1DrawTransform = VtblForID2D1DrawTransform;
        @this->referenceCount = 1;
        @this->shaderId = shaderId;
        @this->numberOfInputs = numberOfInputs;
        @this->bytecode = bytecode;
        @this->bytecodeSize = bytecodeSize;
        @this->d2D1DrawTransformMapperFactoryHandle = GCHandle.Alloc(d2D1DrawTransformMapper);

        *effectImpl = (IUnknown*)@this;

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

            this.d2D1DrawTransformMapperFactoryHandle.Free();

            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }
}