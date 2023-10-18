using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.Win32;

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
    public delegate int FactoryDelegate(IUnknown** effectImpl);

    /// <summary>
    /// A base type with global values for pixel shader effects.
    /// </summary>
    public abstract class Globals
    {
        /// <summary>
        /// Gets the <see cref="FactoryDelegate"/> wrapper for the shader factory.
        /// </summary>
        private readonly FactoryDelegate effectFactory;

        /// <summary>
        /// Creates a new <see cref="Globals"/> instance with the specified parameters.
        /// </summary>
        /// <param name="effectFactory">The <see cref="FactoryDelegate"/> wrapper for the shader factory.</param>
        protected Globals(FactoryDelegate effectFactory)
        {
            this.effectFactory = effectFactory;
        }

        /// <inheritdoc cref="ID2D1PixelShaderDescriptor{T}.EffectId"/>
        public abstract ref readonly Guid EffectId { get; }

        /// <inheritdoc cref="ID2D1PixelShaderDescriptor{T}.ConstantBufferSize"/>
        public abstract int ConstantBufferSize { get; }

        /// <inheritdoc cref="ID2D1PixelShaderDescriptor{T}.InputCount"/>
        public abstract int InputCount { get; }

        /// <inheritdoc cref="ID2D1PixelShaderDescriptor{T}.ResourceTextureCount"/>
        public abstract int ResourceTextureCount { get; }

        /// <inheritdoc cref="ID2D1PixelShaderDescriptor{T}.InputTypes"/>
        public abstract ReadOnlyMemory<D2D1PixelShaderInputType> InputTypes { get; }

        /// <inheritdoc cref="ID2D1PixelShaderDescriptor{T}.InputDescriptions"/>
        public abstract ReadOnlyMemory<D2D1InputDescription> InputDescriptions { get; }

        /// <inheritdoc cref="ID2D1PixelShaderDescriptor{T}.ResourceTextureDescriptions"/>
        public abstract ReadOnlyMemory<D2D1ResourceTextureDescription> ResourceTextureDescriptions { get; }

        /// <inheritdoc cref="ID2D1PixelShaderDescriptor{T}.PixelOptions"/>
        public abstract D2D1PixelOptions PixelOptions { get; }

        /// <inheritdoc cref="ID2D1PixelShaderDescriptor{T}.BufferPrecision"/>
        public abstract D2D1BufferPrecision BufferPrecision { get; }

        /// <inheritdoc cref="ID2D1PixelShaderDescriptor{T}.ChannelDepth"/>
        public abstract D2D1ChannelDepth ChannelDepth { get; }

        /// <inheritdoc cref="ID2D1PixelShaderDescriptor{T}.HlslBytecode"/>
        public abstract ReadOnlyMemory<byte> HlslBytecode { get; }

        /// <summary>
        /// Gets the factory for the current effect.
        /// </summary>
#if NET6_0_OR_GREATER
        public delegate* unmanaged[Stdcall]<IUnknown**, HRESULT> Factory
#else
        public void* Factory
#endif
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
            get => (delegate* unmanaged[Stdcall]<IUnknown**, HRESULT>)Marshal.GetFunctionPointerForDelegate(this.effectFactory);
#else
            get => (void*)Marshal.GetFunctionPointerForDelegate(this.effectFactory);
#endif
        }
    }

    /// <summary>
    /// A generic pixel shader implementation.
    /// </summary>
    /// <typeparam name="T">The type of shader.</typeparam>
    public sealed class Globals<T> : Globals
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        /// <summary>
        /// The HLSL bytecode (either precompiled, or the currently available one after compilation).
        /// </summary>
        private ReadOnlyMemory<byte> hlslBytecode;

        /// <summary>
        /// Creates a new <see cref="Globals"/> instance for shaders of type <typeparamref name="T"/>.
        /// </summary>
        private Globals()
            : base(CreateEffect)
        {
        }

        /// <summary>
        /// Gets the shared <see cref="Globals{T}"/> instance to use.
        /// </summary>
        public static Globals<T> Instance { get; } = new();

        /// <inheritdoc/>
        public override ref readonly Guid EffectId => ref D2D1PixelShaderEffect.GetEffectId<T>();

        /// <inheritdoc/>
        public override int ConstantBufferSize => D2D1PixelShader.GetConstantBufferSize<T>();

        /// <inheritdoc/>
        public override int InputCount => D2D1PixelShader.GetInputCount<T>();

        /// <inheritdoc/>
        public override int ResourceTextureCount => D2D1PixelShader.GetResourceTextureCount<T>();

        /// <inheritdoc/>
        public override ReadOnlyMemory<D2D1PixelShaderInputType> InputTypes => D2D1PixelShader.GetInputTypes<T>();

        /// <inheritdoc/>
        public override ReadOnlyMemory<D2D1InputDescription> InputDescriptions => D2D1PixelShader.GetInputDescriptions<T>();

        /// <inheritdoc/>
        public override ReadOnlyMemory<D2D1ResourceTextureDescription> ResourceTextureDescriptions => D2D1PixelShader.GetResourceTextureDescriptions<T>();

        /// <inheritdoc/>
        public override D2D1PixelOptions PixelOptions => D2D1PixelShader.GetPixelOptions<T>();

        /// <inheritdoc/>
        public override D2D1BufferPrecision BufferPrecision => D2D1PixelShader.GetOutputBufferPrecision<T>();

        /// <inheritdoc/>
        public override D2D1ChannelDepth ChannelDepth => D2D1PixelShader.GetOutputBufferChannelDepth<T>();

        /// <inheritdoc/>
        public override ReadOnlyMemory<byte> HlslBytecode
        {
            get
            {
                // Fast path if we already have some bytecode (either precompiled, or already compiled)
                if (this.hlslBytecode is ReadOnlyMemory<byte> { Length: > 0 } hlslBytecode)
                {
                    return hlslBytecode;
                }

                // If not, lazily compile the bytecode when requested
                return this.hlslBytecode = D2D1PixelShader.LoadBytecode<T>();
            }
        }

        /// <inheritdoc cref="FactoryDelegate"/>
        private static int CreateEffect(IUnknown** effectImpl)
        {
            return PixelShaderEffect.Factory(Instance, effectImpl);
        }
    }
}