using System;
using System.Runtime.CompilerServices;
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
        return LoadOrCompileBytecode<T>(null, null, out _, out _);
    }

    /// <summary>
    /// Loads the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="shaderProfile">The resulting <see cref="D2D1ShaderProfile"/> that was used to compile the returned bytecode.</param>
    /// <param name="compileOptions">The resulting <see cref="D2D1CompileOptions"/> that were used to compile the returned bytecode.</param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</returns>
    /// <remarks>
    /// This method will only compile the shader using <see cref="D2D1ShaderProfile.PixelShader50"/> if no precompiled shader is available.
    /// <para>
    /// If the input shader was precompiled, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a pinned memory buffer (from the PE section).
    /// If the shader was compiled at runtime, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a <see cref="byte"/> array with the bytecode.
    /// </para>
    /// </remarks>
    public static ReadOnlyMemory<byte> LoadBytecode<T>(out D2D1ShaderProfile shaderProfile, out D2D1CompileOptions compileOptions)
        where T : unmanaged, ID2D1PixelShader
    {
        return LoadOrCompileBytecode<T>(null, null, out shaderProfile, out compileOptions);
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
        return LoadOrCompileBytecode<T>(shaderProfile, null, out _, out _);
    }

    /// <summary>
    /// Loads the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="shaderProfile">The shader profile to use to get the shader bytecode.</param>
    /// <param name="compileOptions">The resulting <see cref="D2D1CompileOptions"/> that were used to compile the returned bytecode.</param>
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
    public static ReadOnlyMemory<byte> LoadBytecode<T>(D2D1ShaderProfile shaderProfile, out D2D1CompileOptions compileOptions)
        where T : unmanaged, ID2D1PixelShader
    {
        return LoadOrCompileBytecode<T>(shaderProfile, null, out _, out compileOptions);
    }

    /// <summary>
    /// Loads the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="compileOptions">
    /// <para>The compile options to use to get the shader bytecode.</para>
    /// <para>For consistency with <see cref="D2DCompileOptionsAttribute"/>, <see cref="D2D1CompileOptions.PackMatrixRowMajor"/> will be automatically added.</para>
    /// </param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</returns>
    /// <exception cref="ArgumentException">Thrown if <see cref="D2D1CompileOptions.PackMatrixColumnMajor"/> is specified within <paramref name="compileOptions"/>.</exception>
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
    public static ReadOnlyMemory<byte> LoadBytecode<T>(D2D1CompileOptions compileOptions)
        where T : unmanaged, ID2D1PixelShader
    {
        default(ArgumentException).ThrowIf((compileOptions & D2D1CompileOptions.PackMatrixColumnMajor) != 0, nameof(compileOptions));

        return LoadOrCompileBytecode<T>(null, compileOptions | D2D1CompileOptions.PackMatrixRowMajor, out _, out _);
    }

    /// <summary>
    /// Loads the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="compileOptions">
    /// <para>The compile options to use to get the shader bytecode.</para>
    /// <para>For consistency with <see cref="D2DCompileOptionsAttribute"/>, <see cref="D2D1CompileOptions.PackMatrixRowMajor"/> will be automatically added.</para>
    /// </param>
    ///  <param name="shaderProfile">The resulting <see cref="D2D1ShaderProfile"/> that was used to compile the returned bytecode.</param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</returns>
    /// <exception cref="ArgumentException">Thrown if <see cref="D2D1CompileOptions.PackMatrixColumnMajor"/> is specified within <paramref name="compileOptions"/>.</exception>
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
    public static ReadOnlyMemory<byte> LoadBytecode<T>(D2D1CompileOptions compileOptions, out D2D1ShaderProfile shaderProfile)
        where T : unmanaged, ID2D1PixelShader
    {
        default(ArgumentException).ThrowIf((compileOptions & D2D1CompileOptions.PackMatrixColumnMajor) != 0, nameof(compileOptions));

        return LoadOrCompileBytecode<T>(null, compileOptions | D2D1CompileOptions.PackMatrixRowMajor, out shaderProfile, out _);
    }

    /// <summary>
    /// Loads the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="shaderProfile">The shader profile to use to get the shader bytecode.</param>
    /// <param name="compileOptions">
    /// <para>The compile options to use to get the shader bytecode.</para>
    /// <para>For consistency with <see cref="D2DCompileOptionsAttribute"/>, <see cref="D2D1CompileOptions.PackMatrixRowMajor"/> will be automatically added.</para>
    /// </param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</returns>
    /// <exception cref="ArgumentException">Thrown if <see cref="D2D1CompileOptions.PackMatrixColumnMajor"/> is specified within <paramref name="compileOptions"/>.</exception>
    /// <remarks>
    /// If the input shader was precompiled, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a pinned memory buffer (from the PE section).
    /// If the shader was compiled at runtime, the returned <see cref="ReadOnlyMemory{T}"/> instance will wrap a <see cref="byte"/> array with the bytecode.
    /// </remarks>
    public static ReadOnlyMemory<byte> LoadBytecode<T>(D2D1ShaderProfile shaderProfile, D2D1CompileOptions compileOptions)
        where T : unmanaged, ID2D1PixelShader
    {
        default(ArgumentException).ThrowIf((compileOptions & D2D1CompileOptions.PackMatrixColumnMajor) != 0, nameof(compileOptions));

        return LoadOrCompileBytecode<T>(shaderProfile, compileOptions | D2D1CompileOptions.PackMatrixRowMajor, out _, out _);
    }

    /// <summary>
    /// Loads or compiles the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="requestedShaderProfile">The requested shader profile to use to get the shader bytecode.</param>
    /// <param name="requestedCompileOptions">The requested compile options to use to get the shader bytecode.</param>
    /// <param name="effectiveShaderProfile">The effective shader profile that was used to get the shader bytecode.</param>
    /// <param name="effectiveCompileOptions">The effective compile options that were used to get the shader bytecode.</param>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the input shader has not been precompiled.</exception>
    private static unsafe ReadOnlyMemory<byte> LoadOrCompileBytecode<T>(
        D2D1ShaderProfile? requestedShaderProfile,
        D2D1CompileOptions? requestedCompileOptions,
        out D2D1ShaderProfile effectiveShaderProfile,
        out D2D1CompileOptions effectiveCompileOptions)
        where T : unmanaged, ID2D1PixelShader
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
    /// Gets the input types for a D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the input types for.</typeparam>
    /// <returns>The input types of the target D2D1 pixel shader.</returns>
    public static ReadOnlyMemory<D2D1PixelShaderInputType> GetInputTypes<T>()
        where T : unmanaged, ID2D1PixelShader
    {
        Unsafe.SkipInit(out T shader);

        return shader.InputTypes;
    }

    /// <summary>
    /// Gets the available input descriptions for a D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the input descriptions for.</typeparam>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> with the available input descriptions for the shader.</returns>
    public static ReadOnlyMemory<D2D1InputDescription> GetInputDescriptions<T>()
        where T : unmanaged, ID2D1PixelShader
    {
        Unsafe.SkipInit(out T shader);

        return shader.InputDescriptions;
    }

    /// <summary>
    /// Gets the available resource texture descriptions for a D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the resource texture descriptions for.</typeparam>
    /// <returns>A <see cref="ReadOnlyMemory{T}"/> with the available resource texture descriptions for the shader.</returns>
    public static ReadOnlyMemory<D2D1ResourceTextureDescription> GetResourceTextureDescriptions<T>()
        where T : unmanaged, ID2D1PixelShader
    {
        D2D1ByteArrayResourceTextureDescriptionsLoader resourceTextureDescriptionsLoader = default;

        Unsafe.SkipInit(out T shader);

        shader.LoadResourceTextureDescriptions(ref resourceTextureDescriptionsLoader);

        return resourceTextureDescriptionsLoader.GetResultingResourceTextureDescriptions();
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
    /// Gets the size of the constant buffer for a D2D1 pixel shader of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to retrieve info for.</typeparam>
    /// <returns>The size of the constant buffer for a D2D1 pixel shader of type <typeparamref name="T"/>.</returns>
    public static int GetConstantBufferSize<T>()
        where T : unmanaged, ID2D1PixelShader
    {
        return D2D1BufferSizeDispatchDataLoader.For<T>.ConstantBufferSize;
    }

    /// <summary>
    /// Gets the constant buffer from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to retrieve info for.</typeparam>
    /// <param name="shader">The input D2D1 pixel shader to retrieve info for.</param>
    /// <param name="span">The target <see cref="Span{T}"/> to write the constant buffer to.</param>
    /// <returns>The number of bytes written into <paramref name="span"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="span"/> is not large enough to contain the constant buffer.</exception>
    public static int GetConstantBuffer<T>(in T shader, Span<byte> span)
        where T : unmanaged, ID2D1PixelShader
    {
        if (!TryGetConstantBuffer(in shader, span, out int writtenBytes))
        {
            default(ArgumentException).Throw(nameof(span));
        }

        return writtenBytes;
    }

    /// <summary>
    /// Tries to get the constant buffer from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to retrieve info for.</typeparam>
    /// <param name="shader">The input D2D1 pixel shader to retrieve info for.</param>
    /// <param name="span">The target <see cref="Span{T}"/> to write the constant buffer to.</param>
    /// <param name="bytesWritten">The number of bytes written into <paramref name="span"/>.</param>
    /// <returns>Whether or not the constant buffer was retrieved successfully.</returns>
    public static unsafe bool TryGetConstantBuffer<T>(in T shader, Span<byte> span, out int bytesWritten)
        where T : unmanaged, ID2D1PixelShader
    {
        fixed (byte* buffer = span)
        {
            D2D1ByteBufferDispatchDataLoader dataLoader = new(buffer, span.Length);

            Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);

            return dataLoader.TryGetWrittenBytes(out bytesWritten);
        }
    }

    /// <summary>
    /// Sets the constant buffer from an input D2D1 pixel shader, by calling <c>ID2D1DrawInfo::SetPixelShaderConstantBuffer</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to set the constant buffer for.</typeparam>
    /// <param name="d2D1DrawInfo">A pointer to the <c>ID2D1DrawInfo</c> instance to use.</param>
    /// <param name="shader">The input D2D1 pixel shader to set the contant buffer for.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1DrawInfo"/> is <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1drawinfo-setpixelshaderconstantbuffer"/>.</remarks>
    public static unsafe void SetConstantBufferForD2D1DrawInfo<T>(void* d2D1DrawInfo, in T shader)
        where T : unmanaged, ID2D1PixelShader
    {
        default(ArgumentNullException).ThrowIfNull(d2D1DrawInfo);

        D2D1DrawInfoDispatchDataLoader dataLoader = new((ID2D1DrawInfo*)d2D1DrawInfo);

        Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);
    }
}