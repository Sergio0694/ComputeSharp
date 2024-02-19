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
    /// <remarks>
    /// The return type is intentionally <see langword="void"/><c>*</c> because delegate targets are considered
    /// visible for reflection, so not using <see cref="IUnknown"/> avoids metadata for it being rooted unnecessarily.
    /// </remarks>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int FactoryDelegate(void** effectImpl);

    /// <summary>
    /// A base type with global values for pixel shader effects.
    /// </summary>
    /// <param name="effectFactory">The <see cref="FactoryDelegate"/> wrapper for the shader factory.</param>
    public abstract class Globals(FactoryDelegate effectFactory)
    {
        /// <summary>
        /// Gets the <see cref="FactoryDelegate"/> wrapper for the shader factory.
        /// </summary>
        private readonly FactoryDelegate effectFactory = effectFactory;

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
        public delegate* unmanaged<IUnknown**, HRESULT> Factory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (delegate* unmanaged<IUnknown**, HRESULT>)Marshal.GetFunctionPointerForDelegate(this.effectFactory);
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
        public override ref readonly Guid EffectId => ref T.EffectId;

        /// <inheritdoc/>
        public override int ConstantBufferSize => T.ConstantBufferSize;

        /// <inheritdoc/>
        public override int InputCount => T.InputCount;

        /// <inheritdoc/>
        public override int ResourceTextureCount => T.ResourceTextureCount;

        /// <inheritdoc/>
        public override ReadOnlyMemory<D2D1PixelShaderInputType> InputTypes => T.InputTypes;

        /// <inheritdoc/>
        public override ReadOnlyMemory<D2D1InputDescription> InputDescriptions => T.InputDescriptions;

        /// <inheritdoc/>
        public override ReadOnlyMemory<D2D1ResourceTextureDescription> ResourceTextureDescriptions => T.ResourceTextureDescriptions;

        /// <inheritdoc/>
        public override D2D1PixelOptions PixelOptions => T.PixelOptions;

        /// <inheritdoc/>
        public override D2D1BufferPrecision BufferPrecision => T.BufferPrecision;

        /// <inheritdoc/>
        public override D2D1ChannelDepth ChannelDepth => T.ChannelDepth;

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
        private static int CreateEffect(void** effectImpl)
        {
            return PixelShaderEffect.Factory(Instance, (IUnknown**)effectImpl);
        }
    }
}