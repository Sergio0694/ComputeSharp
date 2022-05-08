using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Helpers;
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
        /// <remarks>
        /// This field could have used <see cref="FixedAddressValueTypeAttribute"/> to reduce some verbosity where it is
        /// being used to pass its address to a native API, but the attribute is incompatible with collectible assemblies.
        /// Because of that, it can't be used, as this project is explicitly meant to support plugin-like scenarios.
        /// </remarks>
        private static Guid shaderId;

        /// <summary>
        /// The number of inputs for the shader.
        /// </summary>
        private static int inputCount;

        /// <summary>
        /// The buffer with the types of inputs for the shader.
        /// </summary>
        private static D2D1PixelShaderInputType* inputTypes;

        /// <summary>
        /// The number of available input descriptions.
        /// </summary>
        private static int inputDescriptionCount;

        /// <summary>
        /// The buffer with the available input descriptions for the shader.
        /// </summary>
        private static D2D1InputDescription* inputDescriptions;

        /// <summary>
        /// The pixel options for the shader.
        /// </summary>
        private static D2D1PixelOption pixelOptions;

        /// <summary>
        /// The shader bytecode.
        /// </summary>
        private static byte* bytecode;

        /// <summary>
        /// The size of <see cref="bytecode"/>.
        /// </summary>
        private static int bytecodeSize;

        /// <summary>
        /// The buffer precision for the resulting output buffer.
        /// </summary>
        private static D2D1BufferPrecision bufferPrecision;

        /// <summary>
        /// The channel depth for the resulting output buffer.
        /// </summary>
        private static D2D1ChannelDepth channelDepth;

        /// <summary>
        /// The factory of <see cref="ID2D1TransformMapper{T}"/> instances to use for each created effect.
        /// </summary>
        private static Func<ID2D1TransformMapper<T>>? d2D1DrawTransformMapperFactory;

        /// <summary>
        /// Initializes the <see cref="For{T}"/> shared state.
        /// </summary>
        /// <param name="d2D1DrawTransformMapperFactory">The factory of <see cref="ID2D1TransformMapper{T}"/> instances to use for each created effect.</param>
        /// <exception cref="InvalidOperationException">Thrown if initialization is attempted with a mismatched transform factory.</exception>
        public static void Initialize(Func<ID2D1TransformMapper<T>>? d2D1DrawTransformMapperFactory)
        {
            // This conceptually acts as a static constructor, and this type is
            // internal, so in this very specific case locking on the type is fine.
            lock (typeof(For<T>))
            {
                if (isInitialized)
                {
                    // If the factory is already initialized, ensure the draw transform mapper is the same
                    if (For<T>.d2D1DrawTransformMapperFactory != d2D1DrawTransformMapperFactory)
                    {
                        ThrowHelper.ThrowInvalidOperationException(
                            "Cannot initialize an ID2D1Effect factory for the same shader type with two different transform mappings. " +
                            "Make sure to only ever register a pixel shader effect with either no transform, or the same transform type.");
                    }
                }
                else
                {
                    // Load all shader properties
                    Guid shaderId = typeof(T).GUID;
                    ReadOnlyMemory<byte> bytecodeInfo = D2D1PixelShader.LoadBytecode<T>();
                    int bytecodeSize = bytecodeInfo.Length;
                    byte* bytecode = (byte*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(For<T>), bytecodeSize);
                    D2D1BufferPrecision bufferPrecision = D2D1PixelShader.GetOutputBufferPrecision<T>();
                    D2D1ChannelDepth channelDepth = D2D1PixelShader.GetOutputBufferChannelDepth<T>();
                    D2D1PixelOption pixelOptions = D2D1PixelShader.GetPixelOptions<T>();

                    // Prepare the inputs info
                    int inputCount = D2D1PixelShader.GetInputCount<T>();
                    D2D1PixelShaderInputType* inputTypes = (D2D1PixelShaderInputType*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(For<T>), inputCount);

                    for (int i = 0; i < inputCount; i++)
                    {
                        inputTypes[i] = D2D1PixelShader.GetInputType<T>(i);
                    }

                    // Prepare the input descriptions
                    ReadOnlyMemory<D2D1InputDescription> inputDescriptionsInfo = D2D1PixelShader.GetInputDescriptions<T>();
                    int inputDescriptionCount = inputDescriptionsInfo.Length;
                    D2D1InputDescription* inputDescriptions = (D2D1InputDescription*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(For<T>), inputDescriptionCount);

                    inputDescriptionsInfo.Span.CopyTo(new Span<D2D1InputDescription>(inputDescriptions, inputDescriptionCount));

                    // Copy the bytecode to the target buffer
                    bytecodeInfo.Span.CopyTo(new Span<byte>(bytecode, bytecodeSize));

                    // Set the shared state and mark the type as initialized
                    For<T>.shaderId = shaderId;
                    For<T>.inputCount = inputCount;
                    For<T>.inputTypes = inputTypes;
                    For<T>.inputDescriptionCount = inputDescriptionCount;
                    For<T>.inputDescriptions = inputDescriptions;
                    For<T>.pixelOptions = pixelOptions;
                    For<T>.bytecode = bytecode;
                    For<T>.bytecodeSize = bytecodeSize;
                    For<T>.bufferPrecision = bufferPrecision;
                    For<T>.channelDepth = channelDepth;
                    For<T>.d2D1DrawTransformMapperFactory = d2D1DrawTransformMapperFactory;

                    isInitialized = true;
                }
            }
        }

        /// <summary>
        /// Gets a reference to the id of the effect.
        /// </summary>
        public static ref Guid Id
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref shaderId;
        }

        /// <summary>
        /// Gets the factory for the current effect.
        /// </summary>
        public static delegate* unmanaged[Stdcall]<IUnknown**, HRESULT> Factory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (delegate* unmanaged[Stdcall]<IUnknown**, HRESULT>)Marshal.GetFunctionPointerForDelegate(EffectFactory);
        }

        /// <summary>
        /// Gets the number of inputs for the effect.
        /// </summary>
        public static int InputCount => inputCount;

        /// <summary>
        /// Tries to get the effect id, if it has been initialized.
        /// </summary>
        /// <param name="id">The resulting effect id.</param>
        /// <returns>Whether or not the effect had been initialized.</returns>
        public static bool TryGetId(out Guid id)
        {
            lock (typeof(For<T>))
            {
                if (isInitialized)
                {
                    id = Id;

                    return true;
                }
            }

            id = default;

            return false;
        }

        /// <inheritdoc cref="FactoryDelegate"/>
        private static int CreateEffect(IUnknown** effectImpl)
        {
            D2D1TransformMapper? d2D1TransformMapper;

            // If there is a custom draw transform factory, run it in a try block and handle exceptions. This
            // is needed because the factory will be running user code from a method that's invoked by COM,
            // and managed exceptions should never cross the ABI boundary. If it throws, just return the HRESULT.
            try
            {
                ID2D1TransformMapper<T>? d2D1DrawTransformMapper = d2D1DrawTransformMapperFactory?.Invoke();

                d2D1TransformMapper = D2D1TransformMapper.For<T>.Get(d2D1DrawTransformMapper);
            }
            catch (Exception e)
            {
                *effectImpl = null;

                return e.HResult;
            }

            return PixelShaderEffect.Factory(
                shaderId,
                inputCount,
                inputTypes,
                inputDescriptionCount,
                inputDescriptions,
                pixelOptions,
                bytecode,
                bytecodeSize,
                bufferPrecision,
                channelDepth,
                d2D1TransformMapper,
                effectImpl);
        }
    }
}