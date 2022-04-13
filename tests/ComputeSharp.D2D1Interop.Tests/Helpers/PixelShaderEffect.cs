using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1Interop.Tests.Helpers;

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
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    private delegate int FactoryDelegate(IUnknown** effectImpl);

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

        /// <summary>
        /// Gets the number of inputs for the effect.
        /// </summary>
        public static int NumberOfInputs => numberOfInputs;

        /// <inheritdoc cref="FactoryDelegate"/>
        private static int CreateEffect(IUnknown** effectImpl)
        {
            return PixelShaderEffect.Factory(shaderId, bytecode, bytecodeSize, numberOfInputs, effectImpl);
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
        lpVtbl[0] = (delegate* unmanaged<PixelShaderEffect*, Guid*, void**, int>)&ID2D1EffectImplMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<PixelShaderEffect*, uint>)&ID2D1EffectImplMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged<PixelShaderEffect*, uint>)&ID2D1EffectImplMethods.Release;
        lpVtbl[3] = (delegate* unmanaged<PixelShaderEffect*, ID2D1EffectContext*, ID2D1TransformGraph*, int>)&ID2D1EffectImplMethods.Initialize;
        lpVtbl[4] = (delegate* unmanaged<PixelShaderEffect*, D2D1_CHANGE_TYPE, int>)&ID2D1EffectImplMethods.PrepareForRender;
        lpVtbl[5] = (delegate* unmanaged<PixelShaderEffect*, ID2D1TransformGraph*, int>)&ID2D1EffectImplMethods.SetGraph;

        // ID2D1DrawTransform
        lpVtbl[6 + 0] = (delegate* unmanaged<PixelShaderEffect*, Guid*, void**, int>)&ID2D1DrawTransformMethods.QueryInterface;
        lpVtbl[6 + 1] = (delegate* unmanaged<PixelShaderEffect*, uint>)&ID2D1DrawTransformMethods.AddRef;
        lpVtbl[6 + 2] = (delegate* unmanaged<PixelShaderEffect*, uint>)&ID2D1DrawTransformMethods.Release;
        lpVtbl[6 + 3] = (delegate* unmanaged<PixelShaderEffect*, uint>)&ID2D1DrawTransformMethods.GetInputCount;
        lpVtbl[6 + 4] = (delegate* unmanaged<PixelShaderEffect*, RECT*, RECT*, uint, int>)&ID2D1DrawTransformMethods.MapOutputRectToInputRects;
        lpVtbl[6 + 5] = (delegate* unmanaged<PixelShaderEffect*, RECT*, RECT*, uint, RECT*, RECT*, int>)&ID2D1DrawTransformMethods.MapInputRectsToOutputRect;
        lpVtbl[6 + 6] = (delegate* unmanaged<PixelShaderEffect*, uint, RECT, RECT*, int>)&ID2D1DrawTransformMethods.MapInvalidRect;
        lpVtbl[6 + 7] = (delegate* unmanaged<PixelShaderEffect*, ID2D1DrawInfo*, int>)&ID2D1DrawTransformMethods.SetDrawInfo;

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
    /// The current input rectangle.
    /// </summary>
    private RECT inputRect;

    /// <summary>
    /// The <see cref="ID2D1DrawInfo"/> instance currently in use.
    /// </summary>
    private ID2D1DrawInfo* d2D1DrawInfo;

    /// <summary>
    /// The factory method for <see cref="ID2D1Factory2.RegisterEffectFromString"/>.
    /// </summary>
    /// <param name="shaderId">The <see cref="Guid"/> for the shader.</param>
    /// <param name="bytecode">The shader bytecode.</param>
    /// <param name="effectImpl">The resulting effect instance.</param>
    /// <param name="bytecodeSize">The size of <paramref name="bytecode"/>.</param>
    /// <param name="numberOfInputs">The number of inputs for the shader.</param>
    /// <returns>This always returns <c>0</c>.</returns>
    private static int Factory(Guid shaderId, byte* bytecode, int bytecodeSize, int numberOfInputs,  IUnknown** effectImpl)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)NativeMemory.Alloc((nuint)sizeof(PixelShaderEffect));

        *@this = default;

        @this->lpVtblForID2D1EffectImpl = VtblForID2D1EffectImpl;
        @this->lpVtblForID2D1DrawTransform = VtblForID2D1DrawTransform;
        @this->referenceCount = 1;
        @this->shaderId = shaderId;
        @this->bytecode = bytecode;
        @this->bytecodeSize = bytecodeSize;
        @this->numberOfInputs = numberOfInputs;

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

    /// <inheritdoc cref="ID2D1EffectImpl.QueryInterface"/>
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

    /// <inheritdoc cref="ID2D1EffectImpl.AddRef"/>
    private uint AddRef()
    {
        return (uint)Interlocked.Increment(ref this.referenceCount);
    }

    /// <inheritdoc cref="ID2D1EffectImpl.Release"/>
    private uint Release()
    {
        uint referenceCount = (uint)Interlocked.Decrement(ref this.referenceCount);

        if (referenceCount == 0)
        {
            if (this.constantBuffer is not null)
            {
                NativeMemory.Free(this.constantBuffer);
            }

            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }
}