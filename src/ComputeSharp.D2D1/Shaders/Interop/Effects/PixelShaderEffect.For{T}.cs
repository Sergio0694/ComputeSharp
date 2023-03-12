using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using RuntimeHelpers = ComputeSharp.NetStandard.RuntimeHelpers;
#endif

namespace ComputeSharp.D2D1.Interop.Effects;

/// <inheritdoc/>
unsafe partial struct PixelShaderEffect
{
    /// <summary>
    /// A wrapper for an effect factory.
    /// </summary>
    /// <param name="effectImpl">The resulting effect factory.</param>
    /// <returns>The <c>HRESULT</c> for the operation.</returns>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int FactoryDelegate(IUnknown** effectImpl);

    /// <summary>
    /// A generic pixel shader implementation.
    /// </summary>
    /// <typeparam name="T">The type of shader.</typeparam>
    public sealed class For<T>
        where T : unmanaged, ID2D1PixelShader
    {
        /// <summary>
        /// The <see cref="FactoryDelegate"/> wrapper for the shader factory.
        /// </summary>
        private readonly FactoryDelegate effectFactory;

        /// <summary>
        /// The <see cref="Guid"/> for the shader.
        /// </summary>
        /// <remarks>
        /// This field could have used <see cref="FixedAddressValueTypeAttribute"/> to reduce some verbosity where it is
        /// being used to pass its address to a native API, but the attribute is incompatible with collectible assemblies.
        /// Because of that, it can't be used, as this project is explicitly meant to support plugin-like scenarios.
        /// </remarks>
        private readonly Guid shaderId;

        /// <summary>
        /// The size of the constant buffer for the shader.
        /// </summary>
        private readonly int constantBufferSize;

        /// <summary>
        /// The number of inputs for the shader.
        /// </summary>
        private readonly int inputCount;

        /// <summary>
        /// The buffer with the types of inputs for the shader.
        /// </summary>
        private readonly D2D1PixelShaderInputType* inputTypes;

        /// <summary>
        /// The number of available input descriptions.
        /// </summary>
        private readonly int inputDescriptionCount;

        /// <summary>
        /// The buffer with the available input descriptions for the shader.
        /// </summary>
        private readonly D2D1InputDescription* inputDescriptions;

        /// <summary>
        /// The pixel options for the shader.
        /// </summary>
        private readonly D2D1PixelOptions pixelOptions;

        /// <summary>
        /// The shader bytecode.
        /// </summary>
        private readonly byte* bytecode;

        /// <summary>
        /// The size of <see cref="bytecode"/>.
        /// </summary>
        private readonly int bytecodeSize;

        /// <summary>
        /// The buffer precision for the resulting output buffer.
        /// </summary>
        private readonly D2D1BufferPrecision bufferPrecision;

        /// <summary>
        /// The channel depth for the resulting output buffer.
        /// </summary>
        private readonly D2D1ChannelDepth channelDepth;

        /// <summary>
        /// The number of available resource texture descriptions.
        /// </summary>
        private readonly int resourceTextureDescriptionCount;

        /// <summary>
        /// The buffer with the available resource texture descriptions for the shader.
        /// </summary>
        private readonly D2D1ResourceTextureDescription* resourceTextureDescriptions;

        /// <summary>
        /// Creates a new <see cref="For{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="effectFactory">The <see cref="FactoryDelegate"/> wrapper for the shader factory.</param>
        /// <param name="shaderId">The <see cref="Guid"/> for the shader.</param>
        /// <param name="constantBufferSize">The size of the constant buffer for the shader.</param>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <param name="inputTypes">The buffer with the types of inputs for the shader.</param>
        /// <param name="inputDescriptionCount">The number of available input descriptions.</param>
        /// <param name="inputDescriptions">The buffer with the available input descriptions for the shader.</param>
        /// <param name="pixelOptions">The pixel options for the shader.</param>
        /// <param name="bytecode">The shader bytecode.</param>
        /// <param name="bytecodeSize">The size of <paramref name="bytecode"/>.</param>
        /// <param name="bufferPrecision">The buffer precision for the resulting output buffer.</param>
        /// <param name="channelDepth">The channel depth for the resulting output buffer.</param>
        /// <param name="resourceTextureDescriptionCount">The number of available resource texture descriptions.</param>
        /// <param name="resourceTextureDescriptions">The buffer with the available resource texture descriptions for the shader.</param>
        private For(
            FactoryDelegate effectFactory,
            Guid shaderId,
            int constantBufferSize,
            int inputCount,
            D2D1PixelShaderInputType* inputTypes,
            int inputDescriptionCount,
            D2D1InputDescription* inputDescriptions,
            D2D1PixelOptions pixelOptions,
            byte* bytecode,
            int bytecodeSize,
            D2D1BufferPrecision bufferPrecision,
            D2D1ChannelDepth channelDepth,
            int resourceTextureDescriptionCount,
            D2D1ResourceTextureDescription* resourceTextureDescriptions)
        {
            this.effectFactory = effectFactory;
            this.shaderId = shaderId;
            this.constantBufferSize = constantBufferSize;
            this.inputCount = inputCount;
            this.inputTypes = inputTypes;
            this.inputDescriptionCount = inputDescriptionCount;
            this.inputDescriptions = inputDescriptions;
            this.pixelOptions = pixelOptions;
            this.bytecode = bytecode;
            this.bytecodeSize = bytecodeSize;
            this.bufferPrecision = bufferPrecision;
            this.channelDepth = channelDepth;
            this.resourceTextureDescriptionCount = resourceTextureDescriptionCount;
            this.resourceTextureDescriptions = resourceTextureDescriptions;
        }

        /// <summary>
        /// Gets the shader <see cref="For{T}"/> instance.
        /// </summary>
        public static For<T> Instance { get; } = CreateInstance();

        /// <summary>
        /// Creates a new <see cref="For{T}"/> instance.
        /// </summary>
        /// <returns>The initialized <see cref="For{T}"/> instance.</returns>
        private static For<T> CreateInstance()
        {
            // Load all shader properties
            Guid shaderId = typeof(T).GUID;
            int constantBufferSize = D2D1PixelShader.GetConstantBufferSize<T>();
            D2D1BufferPrecision bufferPrecision = D2D1PixelShader.GetOutputBufferPrecision<T>();
            D2D1ChannelDepth channelDepth = D2D1PixelShader.GetOutputBufferChannelDepth<T>();
            D2D1PixelOptions pixelOptions = D2D1PixelShader.GetPixelOptions<T>();

            // Prepare the inputs info
            int inputCount = D2D1PixelShader.GetInputCount<T>();
            D2D1PixelShaderInputType* inputTypes = (D2D1PixelShaderInputType*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(For<T>), sizeof(D2D1PixelShaderInputType) * inputCount);

            for (int i = 0; i < inputCount; i++)
            {
                inputTypes[i] = D2D1PixelShader.GetInputType<T>(i);
            }

            // Prepare the input descriptions
            ReadOnlyMemory<D2D1InputDescription> inputDescriptionsInfo = D2D1PixelShader.GetInputDescriptions<T>();
            int inputDescriptionCount = inputDescriptionsInfo.Length;
            D2D1InputDescription* inputDescriptions = (D2D1InputDescription*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(For<T>), sizeof(D2D1InputDescription) * inputDescriptionCount);

            inputDescriptionsInfo.Span.CopyTo(new Span<D2D1InputDescription>(inputDescriptions, inputDescriptionCount));

            // Prepare the resource texture descriptions
            ReadOnlyMemory<D2D1ResourceTextureDescription> resourceTextureDescriptionsInfo = D2D1PixelShader.GetResourceTextureDescriptions<T>();
            int resourceTextureDescriptionCount = resourceTextureDescriptionsInfo.Length;
            D2D1ResourceTextureDescription* resourceTextureDescriptions = (D2D1ResourceTextureDescription*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(For<T>), sizeof(D2D1ResourceTextureDescription) * resourceTextureDescriptionCount);

            resourceTextureDescriptionsInfo.Span.CopyTo(new Span<D2D1ResourceTextureDescription>(resourceTextureDescriptions, resourceTextureDescriptionCount));

            // Copy the bytecode to the target buffer
            ReadOnlyMemory<byte> bytecodeInfo = D2D1PixelShader.LoadBytecode<T>();
            int bytecodeSize = bytecodeInfo.Length;
            byte* bytecode = (byte*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(For<T>), bytecodeSize);

            bytecodeInfo.Span.CopyTo(new Span<byte>(bytecode, bytecodeSize));

            // Initialize the shared instance with the computed state
            return new(
                CreateEffect,
                shaderId,
                constantBufferSize,
                inputCount,
                inputTypes,
                inputDescriptionCount,
                inputDescriptions,
                pixelOptions,
                bytecode,
                bytecodeSize,
                bufferPrecision,
                channelDepth,
                resourceTextureDescriptionCount,
                resourceTextureDescriptions);
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

        /// <inheritdoc cref="FactoryDelegate"/>
        private static int CreateEffect(IUnknown** effectImpl)
        {
            For<T> instance = Instance;

            return PixelShaderEffect.Factory(
                instance.shaderId,
                instance.constantBufferSize,
                instance.inputCount,
                instance.inputTypes,
                instance.inputDescriptionCount,
                instance.inputDescriptions,
                instance.pixelOptions,
                instance.bytecode,
                instance.bytecodeSize,
                instance.bufferPrecision,
                instance.channelDepth,
                instance.resourceTextureDescriptionCount,
                instance.resourceTextureDescriptions,
                effectImpl);
        }
    }
}