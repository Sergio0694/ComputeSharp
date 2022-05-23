using System;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Helpers;
using ComputeSharp.D2D1.Shaders.Interop.Buffers;
using ComputeSharp.D2D1.Shaders.Loaders;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// Provides methods to interop with D2D1 APIs and compile shaders or extract their constant buffer data.
/// </summary>
public static class D2D1PixelShader
{
    /// <summary>
    /// Loads the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</returns>
    /// <remarks>
    /// This method will only compile the shader using <see cref="D2D1ShaderProfile.PixelShader50"/> if no precompiled shader is available.
    /// <para>
    /// If the input shader was precompiled, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a pinned memory buffer (from the PE section).
    /// If the shader was compiled at runtime, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a <see cref="byte"/> array with the bytecode.
    /// </para>
    /// </remarks>
    public static ReadOnlyMemory<byte> LoadBytecode<T>()
        where T : unmanaged, ID2D1PixelShader
    {
        return LoadOrCompileBytecode<T>(null, null);
    }

    /// <summary>
    /// Loads the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="shaderProfile">The shader profile to use to get the shader bytecode.</param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</returns>
    /// <remarks>
    /// <para>
    /// If precompiled shader for the profile does not exist, the shader will be compiled with either the custom compile options specified on the shader
    /// type <typeparamref name="T"/> (through <see cref="D2DCompileOptionsAttribute"/>), or using <see cref="D2D1CompileOptions.Default"/> otherwise.
    /// </para>
    /// <para>
    /// Additionally, in case no custom compile options are specified and the the shader type <typeparamref name="T"/>
    /// supports linking, <see cref="D2D1CompileOptions.EnableLinking"/> will also be automatically added.
    /// </para>
    /// <para>
    /// If the input shader was precompiled, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a pinned memory buffer (from the PE section).
    /// If the shader was compiled at runtime, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a <see cref="byte"/> array with the bytecode.
    /// </para>
    /// </remarks>
    public static ReadOnlyMemory<byte> LoadBytecode<T>(D2D1ShaderProfile shaderProfile)
        where T : unmanaged, ID2D1PixelShader
    {
        return LoadOrCompileBytecode<T>(shaderProfile, null);
    }

    /// <summary>
    /// Loads the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="options">
    /// <para>The compile options to use to get the shader bytecode.</para>
    /// <para>For consistency with <see cref="D2DCompileOptionsAttribute"/>, <see cref="D2D1CompileOptions.PackMatrixRowMajor"/> will be automatically added.</para>
    /// </param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</returns>
    /// <exception cref="ArgumentException">Thrown if <see cref="D2D1CompileOptions.PackMatrixColumnMajor"/> is specified within <paramref name="options"/>.</exception>
    /// <remarks>
    /// <para>
    /// If precompiled shader with the requested options does not exist, the shader will be compiled with the input options. If additional compile
    /// options have been specified on the shader type <typeparamref name="T"/> (through <see cref="D2DCompileOptionsAttribute"/>), they will be ignored.
    /// </para>
    /// <para>
    /// If the shader needs to be recompiled, the shader profile that will be used is <see cref="D2D1ShaderProfile.PixelShader50"/>.
    /// </para>
    /// <para>
    /// If the input shader was precompiled, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a pinned memory buffer (from the PE section).
    /// If the shader was compiled at runtime, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a <see cref="byte"/> array with the bytecode.
    /// </para>
    /// </remarks>
    public static ReadOnlyMemory<byte> LoadBytecode<T>(D2D1CompileOptions options)
        where T : unmanaged, ID2D1PixelShader
    {
        if ((options & D2D1CompileOptions.PackMatrixColumnMajor) != 0)
        {
            ThrowHelper.ThrowArgumentException(nameof(options), "The PackMatrixColumnMajor compile options is not compatible with ComputeSharp.D2D1 shaders.");
        }

        return LoadOrCompileBytecode<T>(null, options | D2D1CompileOptions.PackMatrixRowMajor);
    }

    /// <summary>
    /// Loads the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="shaderProfile">The shader profile to use to get the shader bytecode.</param>
    /// <param name="options">
    /// <para>The compile options to use to get the shader bytecode.</para>
    /// <para>For consistency with <see cref="D2DCompileOptionsAttribute"/>, <see cref="D2D1CompileOptions.PackMatrixRowMajor"/> will be automatically added.</para>
    /// </param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</returns>
    /// <exception cref="ArgumentException">Thrown if <see cref="D2D1CompileOptions.PackMatrixColumnMajor"/> is specified within <paramref name="options"/>.</exception>
    /// <remarks>
    /// If the input shader was precompiled, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a pinned memory buffer (from the PE section).
    /// If the shader was compiled at runtime, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a <see cref="byte"/> array with the bytecode.
    /// </remarks>
    public static ReadOnlyMemory<byte> LoadBytecode<T>(D2D1ShaderProfile shaderProfile, D2D1CompileOptions options)
        where T : unmanaged, ID2D1PixelShader
    {
        if ((options & D2D1CompileOptions.PackMatrixColumnMajor) != 0)
        {
            ThrowHelper.ThrowArgumentException(nameof(options), "The PackMatrixColumnMajor compile options is not compatible with ComputeSharp.D2D1 shaders.");
        }

        return LoadOrCompileBytecode<T>(shaderProfile, options | D2D1CompileOptions.PackMatrixRowMajor);
    }

    /// <summary>
    /// Loads or compiles the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="shaderProfile">The shader profile to use to get the shader bytecode.</param>
    /// <param name="options">The compile options to use to get the shader bytecode.</param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the input shader has not been precompiled.</exception>
    private static unsafe ReadOnlyMemory<byte> LoadOrCompileBytecode<T>(D2D1ShaderProfile? shaderProfile, D2D1CompileOptions? options)
        where T : unmanaged, ID2D1PixelShader
    {
        D2D1ShaderBytecodeLoader bytecodeLoader = default;

        Unsafe.SkipInit(out T shader);

        shader.LoadBytecode(ref bytecodeLoader, shaderProfile, options);

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
    /// Gets the pixel options from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the pixel options for.</typeparam>
    /// <returns>The pixel options for the D2D1 pixel shader of type <typeparamref name="T"/>.</returns>
    public static D2D1PixelOptions GetPixelOptions<T>()
        where T : unmanaged, ID2D1PixelShader
    {
        Unsafe.SkipInit(out T shader);

        return (D2D1PixelOptions)shader.GetPixelOptions();
    }

    /// <summary>
    /// Gets the number of inputs from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the input count for.</typeparam>
    /// <returns>The number of inputs for the D2D1 pixel shader of type <typeparamref name="T"/>.</returns>
    public static int GetInputCount<T>()
        where T : unmanaged, ID2D1PixelShader
    {
        Unsafe.SkipInit(out T shader);

        return (int)shader.GetInputCount();
    }

    /// <summary>
    /// Gets the type of a given input for a D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the input type for.</typeparam>
    /// <param name="index">The index of the input to get the type for.</param>
    /// <returns>The type of the input of the target D2D1 pixel shader at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is not in range for the available inputs for the shader type.</exception>
    public static D2D1PixelShaderInputType GetInputType<T>(int index)
        where T : unmanaged, ID2D1PixelShader
    {
        Unsafe.SkipInit(out T shader);

        if ((uint)index >= shader.GetInputCount())
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(index), "The input index is outside of range for the target pixel shader type.");
        }

        return (D2D1PixelShaderInputType)shader.GetInputType((uint)index);
    }

    /// <summary>
    /// Gets the available input descriptions for a D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the input descriptions for.</typeparam>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> with the available input descriptions for the shader.</returns>
    public static ReadOnlyMemory<D2D1InputDescription> GetInputDescriptions<T>()
        where T : unmanaged, ID2D1PixelShader
    {
        D2D1ByteArrayInputDescriptionsLoader inputDescriptionsLoader = default;

        Unsafe.SkipInit(out T shader);

        shader.LoadInputDescriptions(ref inputDescriptionsLoader);

        return inputDescriptionsLoader.GetResultingInputDescriptions();
    }

    /// <summary>
    /// Gets the buffer precision for the output buffer of a D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the output buffer precision for.</typeparam>
    /// <returns>The output buffer precision for the D2D1 pixel shader of type <typeparamref name="T"/>.</returns>
    public static D2D1BufferPrecision GetOutputBufferPrecision<T>()
        where T : unmanaged, ID2D1PixelShader
    {
        Unsafe.SkipInit(out T shader);

        shader.GetOutputBuffer(out uint precision, out _);

        return (D2D1BufferPrecision)precision;
    }

    /// <summary>
    /// Gets the channel depth for the output buffer of a D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the output buffer channel depth for.</typeparam>
    /// <returns>The output buffer channel depth for the D2D1 pixel shader of type <typeparamref name="T"/>.</returns>
    public static D2D1ChannelDepth GetOutputBufferChannelDepth<T>()
        where T : unmanaged, ID2D1PixelShader
    {
        Unsafe.SkipInit(out T shader);

        shader.GetOutputBuffer(out _, out uint depth);

        return (D2D1ChannelDepth)depth;
    }

    /// <summary>
    /// Gets the constant buffer from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to retrieve info for.</typeparam>
    /// <param name="shader">The input D2D1 pixel shader to retrieve info for.</param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the pixel shader constant buffer.</returns>
    /// <remarks>
    /// This method will allocate a buffer every time it is invoked.
    /// For a zero-allocation alternative, use <see cref="SetConstantBufferForD2D1DrawInfo"/>.</remarks>
    public static ReadOnlyMemory<byte> GetConstantBuffer<T>(in T shader)
        where T : unmanaged, ID2D1PixelShader
    {
        D2D1ByteArrayDispatchDataLoader dataLoader = default;

        Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);

        return dataLoader.GetResultingDispatchData();
    }

    /// <summary>
    /// Sets the constant buffer from an input D2D1 pixel shader, by calling <c>ID2D1DrawInfo::SetPixelShaderConstantBuffer</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to set the constant buffer for.</typeparam>
    /// <param name="shader">The input D2D1 pixel shader to set the contant buffer for.</param>
    /// <param name="d2D1DrawInfo">A pointer to the <c>ID2D1DrawInfo</c> instance to use.</param>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1drawinfo-setpixelshaderconstantbuffer"/>.</remarks>
    public static unsafe void SetConstantBufferForD2D1DrawInfo<T>(in T shader, void* d2D1DrawInfo)
        where T : unmanaged, ID2D1PixelShader
    {
        D2D1DrawInfoDispatchDataLoader dataLoader = new((ID2D1DrawInfo*)d2D1DrawInfo);

        Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);
    }
}
