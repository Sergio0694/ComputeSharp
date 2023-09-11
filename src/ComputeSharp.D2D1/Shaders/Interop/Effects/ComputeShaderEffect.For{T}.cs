using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Interop.Helpers;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Interop.Effects;

/// <inheritdoc/>
unsafe partial struct ComputeShaderEffect
{
    /// <summary>
    /// A generic compute shader implementation.
    /// </summary>
    /// <typeparam name="T">The type of shader.</typeparam>
    public sealed class For<T>
        where T : unmanaged, ID2D1ComputeShader
    {
        /// <inheritdoc cref="PixelShaderEffect.For{T}.effectFactory"/>
        private readonly PixelShaderEffect.FactoryDelegate effectFactory;

        /// <inheritdoc cref="PixelShaderEffect.For{T}.shaderId"/>
        private readonly Guid shaderId;

        /// <inheritdoc cref="PixelShaderEffect.For{T}.constantBufferSize"/>
        private readonly int constantBufferSize;

        /// <inheritdoc cref="PixelShaderEffect.For{T}.inputCount"/>
        private readonly int inputCount;

        /// <inheritdoc cref="PixelShaderEffect.For{T}.inputDescriptionCount"/>
        private readonly int inputDescriptionCount;

        /// <inheritdoc cref="PixelShaderEffect.For{T}.inputDescriptions"/>
        private readonly D2D1InputDescription* inputDescriptions;

        /// <inheritdoc cref="PixelShaderEffect.For{T}.bytecode"/>
        private readonly byte* bytecode;

        /// <inheritdoc cref="PixelShaderEffect.For{T}.bytecodeSize"/>
        private readonly int bytecodeSize;

        /// <inheritdoc cref="PixelShaderEffect.For{T}.bufferPrecision"/>
        private readonly D2D1BufferPrecision bufferPrecision;

        /// <inheritdoc cref="PixelShaderEffect.For{T}.channelDepth"/>
        private readonly D2D1ChannelDepth channelDepth;

        /// <inheritdoc cref="PixelShaderEffect.For{T}.resourceTextureDescriptionCount"/>
        private readonly int resourceTextureDescriptionCount;

        /// <inheritdoc cref="PixelShaderEffect.For{T}.resourceTextureDescriptions"/>
        private readonly D2D1ResourceTextureDescription* resourceTextureDescriptions;

        /// <summary>
        /// The <see cref="D2D1DispatchThreadNumbers"/> value for the current shader type.
        /// </summary>
        private readonly D2D1DispatchThreadNumbers dispatchThreadNumbers;

        /// <summary>
        /// Creates a new <see cref="For{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="effectFactory"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='effectFactory']/node()"/></param>
        /// <param name="shaderId"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='shaderId']/node()"/></param>
        /// <param name="constantBufferSize"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='constantBufferSize']/node()"/></param>
        /// <param name="inputCount"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='inputCount']/node()"/></param>
        /// <param name="inputDescriptionCount"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='inputDescriptionCount']/node()"/></param>
        /// <param name="inputDescriptions"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='inputDescriptions']/node()"/></param>
        /// <param name="bytecode"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='bytecode']/node()"/></param>
        /// <param name="bytecodeSize"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='bytecodeSize']/node()"/></param>
        /// <param name="bufferPrecision"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='bufferPrecision']/node()"/></param>
        /// <param name="channelDepth"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='channelDepth']/node()"/></param>
        /// <param name="resourceTextureDescriptionCount"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='resourceTextureDescriptionCount']/node()"/></param>
        /// <param name="resourceTextureDescriptions"><inheritdoc cref="PixelShaderEffect.For{T}.For" path="/param[@name='resourceTextureDescriptions']/node()"/></param>
        /// <param name="dispatchThreadNumbers">The <see cref="D2D1DispatchThreadNumbers"/> value for the current shader type.</param>
        private For(
            PixelShaderEffect.FactoryDelegate effectFactory,
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
            D2D1DispatchThreadNumbers dispatchThreadNumbers)
        {
            this.effectFactory = effectFactory;
            this.shaderId = shaderId;
            this.constantBufferSize = constantBufferSize;
            this.inputCount = inputCount;
            this.inputDescriptionCount = inputDescriptionCount;
            this.inputDescriptions = inputDescriptions;
            this.bytecode = bytecode;
            this.bytecodeSize = bytecodeSize;
            this.bufferPrecision = bufferPrecision;
            this.channelDepth = channelDepth;
            this.resourceTextureDescriptionCount = resourceTextureDescriptionCount;
            this.resourceTextureDescriptions = resourceTextureDescriptions;
            this.dispatchThreadNumbers = dispatchThreadNumbers;
        }

        /// <inheritdoc cref="PixelShaderEffect.For{T}.Instance"/>
        public static For<T> Instance { get; } = CreateInstance();

        /// <inheritdoc cref="PixelShaderEffect.For{T}.CreateInstance"/>
        private static For<T> CreateInstance()
        {
            // Load all shader properties
            D2D1ShaderMarshaller.GetSharedEffectProperties<T>(
                out Guid shaderId,
                out int constantBufferSize,
                out int inputCount,
                out int inputDescriptionCount,
                out D2D1InputDescription* inputDescriptions,
                out byte* bytecode,
                out int bytecodeSize,
                out D2D1BufferPrecision bufferPrecision,
                out D2D1ChannelDepth channelDepth,
                out int resourceTextureDescriptionCount,
                out D2D1ResourceTextureDescription* resourceTextureDescriptions);

            // Also get the dispatch thread numbers, as this is a compute shader
            D2D1DispatchThreadNumbers dispatchThreadNumbers = D2D1ComputeShader.GetDispatchThreadNumbers<T>();

            // Initialize the shared instance with the computed state
            return new(
                CreateEffect,
                shaderId,
                constantBufferSize,
                inputCount,
                inputDescriptionCount,
                inputDescriptions,
                bytecode,
                bytecodeSize,
                bufferPrecision,
                channelDepth,
                resourceTextureDescriptionCount,
                resourceTextureDescriptions,
                dispatchThreadNumbers);
        }

        /// <summary>
        /// Gets a reference to the id of the effect.
        /// </summary>
        public ref readonly Guid Id
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref this.shaderId;
        }

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

        /// <summary>
        /// Gets the number of inputs for the effect.
        /// </summary>
        public int InputCount => this.inputCount;

        /// <inheritdoc cref="PixelShaderEffect.FactoryDelegate"/>
        private static int CreateEffect(IUnknown** effectImpl)
        {
            For<T> instance = Instance;

            return ComputeShaderEffect.Factory(
                instance.shaderId,
                instance.constantBufferSize,
                instance.inputCount,
                instance.inputDescriptionCount,
                instance.inputDescriptions,
                instance.bytecode,
                instance.bytecodeSize,
                instance.bufferPrecision,
                instance.channelDepth,
                instance.resourceTextureDescriptionCount,
                instance.resourceTextureDescriptions,
                instance.dispatchThreadNumbers,
                effectImpl);
        }
    }
}