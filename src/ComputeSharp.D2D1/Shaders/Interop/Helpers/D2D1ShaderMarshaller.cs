using System;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.Shaders.Interop.Buffers;
using ComputeSharp.D2D1.Shaders.Loaders;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Interop.Helpers;

/// <summary>
/// Provides shared methods to interop with D2D1 APIs and compile all types of supported shaders.
/// </summary>
internal static class D2D1ShaderMarshaller
{
    /// <summary>
    /// Loads or compiles the bytecode from an input D2D1 shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to load the bytecode for.</typeparam>
    /// <param name="requestedShaderProfile">The requested shader profile to use to get the shader bytecode.</param>
    /// <param name="requestedCompileOptions">The requested compile options to use to get the shader bytecode.</param>
    /// <param name="effectiveShaderProfile">The effective shader profile that was used to get the shader bytecode.</param>
    /// <param name="effectiveCompileOptions">The effective compile options that were used to get the shader bytecode.</param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the input shader has not been precompiled.</exception>
    public static unsafe ReadOnlyMemory<byte> LoadOrCompileBytecode<T>(
        D2D1ShaderProfile? requestedShaderProfile,
        D2D1CompileOptions? requestedCompileOptions,
        out D2D1ShaderProfile effectiveShaderProfile,
        out D2D1CompileOptions effectiveCompileOptions)
        where T : unmanaged, ID2D1Shader
    {
        D2D1ShaderBytecodeLoader bytecodeLoader = default;

        Unsafe.SkipInit(out T shader);

        shader.LoadBytecode(ref bytecodeLoader, ref requestedShaderProfile, ref requestedCompileOptions);

        effectiveShaderProfile = requestedShaderProfile.GetValueOrDefault();
        effectiveCompileOptions = requestedCompileOptions.GetValueOrDefault();

        using ComPtr<ID3DBlob> dynamicBytecode = bytecodeLoader.GetResultingShaderBytecode(out ReadOnlySpan<byte> precompiledBytecode);

        // If a precompiled shader is available, return it
        if (!precompiledBytecode.IsEmpty)
        {
            return new PinnedBufferMemoryManager(precompiledBytecode).Memory;
        }

        // Otherwise, return the dynamic shader instead
        byte* bytecodePtr = (byte*)dynamicBytecode.Get()->GetBufferPointer();
        int bytecodeSize = (int)dynamicBytecode.Get()->GetBufferSize();

        return new ReadOnlySpan<byte>(bytecodePtr, bytecodeSize).ToArray();
    }

    /// <summary>
    /// Gets the number of inputs from an input D2D1 shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to get the input count for.</typeparam>
    /// <returns>The number of inputs for the D2D1 shader of type <typeparamref name="T"/>.</returns>
    public static int GetInputCount<T>()
        where T : unmanaged, ID2D1Shader
    {
        Unsafe.SkipInit(out T shader);

        return (int)shader.GetInputCount();
    }

    /// <summary>
    /// Gets the available input descriptions for a D2D1 shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to get the input descriptions for.</typeparam>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> with the available input descriptions for the shader.</returns>
    public static ReadOnlyMemory<D2D1InputDescription> GetInputDescriptions<T>()
        where T : unmanaged, ID2D1Shader
    {
        D2D1ByteArrayInputDescriptionsLoader inputDescriptionsLoader = default;

        Unsafe.SkipInit(out T shader);

        shader.LoadInputDescriptions(ref inputDescriptionsLoader);

        return inputDescriptionsLoader.GetResultingInputDescriptions();
    }

    /// <summary>
    /// Gets the available resource texture descriptions for a D2D1 shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to get the resource texture descriptions for.</typeparam>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> with the available resource texture descriptions for the shader.</returns>
    public static ReadOnlyMemory<D2D1ResourceTextureDescription> GetResourceTextureDescriptions<T>()
        where T : unmanaged, ID2D1Shader
    {
        D2D1ByteArrayResourceTextureDescriptionsLoader resourceTextureDescriptionsLoader = default;

        Unsafe.SkipInit(out T shader);

        shader.LoadResourceTextureDescriptions(ref resourceTextureDescriptionsLoader);

        return resourceTextureDescriptionsLoader.GetResultingResourceTextureDescriptions();
    }

    /// <summary>
    /// Gets the buffer precision for the output buffer of a D2D1 shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to get the output buffer precision for.</typeparam>
    /// <returns>The output buffer precision for the D2D1 shader of type <typeparamref name="T"/>.</returns>
    public static D2D1BufferPrecision GetOutputBufferPrecision<T>()
        where T : unmanaged, ID2D1Shader
    {
        Unsafe.SkipInit(out T shader);

        shader.GetOutputBuffer(out uint precision, out _);

        return (D2D1BufferPrecision)precision;
    }

    /// <summary>
    /// Gets the channel depth for the output buffer of a D2D1 shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to get the output buffer channel depth for.</typeparam>
    /// <returns>The output buffer channel depth for the D2D1 shader of type <typeparamref name="T"/>.</returns>
    public static D2D1ChannelDepth GetOutputBufferChannelDepth<T>()
        where T : unmanaged, ID2D1Shader
    {
        Unsafe.SkipInit(out T shader);

        shader.GetOutputBuffer(out _, out uint depth);

        return (D2D1ChannelDepth)depth;
    }

    /// <summary>
    /// Gets the constant buffer from an input D2D1 shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to retrieve info for.</typeparam>
    /// <param name="shader">The input D2D1 shader to retrieve info for.</param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the shader constant buffer.</returns>
    public static ReadOnlyMemory<byte> GetConstantBuffer<T>(in T shader)
        where T : unmanaged, ID2D1Shader
    {
        D2D1ByteArrayDispatchDataLoader dataLoader = default;

        Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);

        return dataLoader.GetResultingDispatchData();
    }

    /// <summary>
    /// Gets the size of the constant buffer for a D2D1 shader of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to retrieve info for.</typeparam>
    /// <returns>The size of the constant buffer for a D2D1 shader of type <typeparamref name="T"/>.</returns>
    public static int GetConstantBufferSize<T>()
        where T : unmanaged, ID2D1Shader
    {
        return D2D1BufferSizeDispatchDataLoader.For<T>.ConstantBufferSize;
    }

    /// <summary>
    /// Gets the constant buffer from an input D2D1 shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to retrieve info for.</typeparam>
    /// <param name="shader">The input D2D1 shader to retrieve info for.</param>
    /// <param name="span">The target <see cref="Span{T}"/> to write the constant buffer to.</param>
    /// <returns>The number of bytes written into <paramref name="span"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="span"/> is not large enough to contain the constant buffer.</exception>
    public static int GetConstantBuffer<T>(in T shader, Span<byte> span)
        where T : unmanaged, ID2D1Shader
    {
        if (!TryGetConstantBuffer(in shader, span, out int writtenBytes))
        {
            default(ArgumentException).Throw(nameof(span));
        }

        return writtenBytes;
    }

    /// <summary>
    /// Tries to get the constant buffer from an input D2D1 shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to retrieve info for.</typeparam>
    /// <param name="shader">The input D2D1 shader to retrieve info for.</param>
    /// <param name="span">The target <see cref="Span{T}"/> to write the constant buffer to.</param>
    /// <param name="bytesWritten">The number of bytes written into <paramref name="span"/>.</param>
    /// <returns>Whether or not the constant buffer was retrieved successfully.</returns>
    public static unsafe bool TryGetConstantBuffer<T>(in T shader, Span<byte> span, out int bytesWritten)
        where T : unmanaged, ID2D1Shader
    {
        fixed (byte* buffer = span)
        {
            D2D1ByteBufferDispatchDataLoader dataLoader = new(buffer, span.Length);

            Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);

            return dataLoader.TryGetWrittenBytes(out bytesWritten);
        }
    }
}