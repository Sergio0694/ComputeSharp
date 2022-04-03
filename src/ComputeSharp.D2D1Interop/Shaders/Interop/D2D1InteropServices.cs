using System;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1Interop.Shaders.Dispatching;
using ComputeSharp.D2D1Interop.Shaders.Interop.Buffers;
using ComputeSharp.Shaders.Dispatching;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1Interop.Interop;

/// <summary>
/// Provides methods to extract reflection info on D2D1 shaders generated using this library.
/// </summary>
public static unsafe class D2D1InteropServices
{
    /// <summary>
    /// Loads the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="bytecode">A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</param>
    /// <exception cref="InvalidOperationException">Thrown if the input shader has not been precompiled.</exception>
    /// <remarks>
    /// This method will only run correctly if the input shader has been precompiled, or it will throw an exception.
    /// The returned <paramref name="bytecode"/> will wrap a pinned memory buffer (from the PE section).
    /// </remarks>
    public static void LoadShaderBytecode<T>(out ReadOnlyMemory<byte> bytecode)
        where T : unmanaged, ID2D1PixelShader
    {
        LoadOrCompileShaderBytecode<T>(null, out bytecode);
    }

    /// <summary>
    /// Loads the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="bytecode">A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</param>
    /// <param name="shaderProfile">The shader profile to use to get the shader bytecode.</param>
    /// <remarks>
    /// If the input shader was precompiled, <paramref name="bytecode"/> will wrap a pinned memory buffer (from the PE section).
    /// If the shader was compiled at runtime, <paramref name="bytecode"/> will wrap a <see cref="byte"/> array with the bytecode.
    /// </remarks>
    public static void LoadShaderBytecode<T>(D2D1ShaderProfile shaderProfile, out ReadOnlyMemory<byte> bytecode)
        where T : unmanaged, ID2D1PixelShader
    {
        LoadOrCompileShaderBytecode<T>(shaderProfile, out bytecode);
    }

    /// <summary>
    /// Loads or compiles the bytecode from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to load the bytecode for.</typeparam>
    /// <param name="shaderProfile">The shader profile to use to get the shader bytecode.</param>
    /// <param name="bytecode">A <see cref="ReadOnlyMemory{T}"/> instance with the resulting shader bytecode.</param>
    /// <exception cref="InvalidOperationException">Thrown if the input shader has not been precompiled.</exception>
    private static void LoadOrCompileShaderBytecode<T>(D2D1ShaderProfile? shaderProfile, out ReadOnlyMemory<byte> bytecode)
        where T : unmanaged, ID2D1PixelShader
    {
        D2D1ShaderBytecodeLoader bytecodeLoader = default;

        Unsafe.NullRef<T>().LoadBytecode(ref bytecodeLoader, shaderProfile, out _);

        using ComPtr<ID3DBlob> dynamicBytecode = bytecodeLoader.GetResultingShaderBytecode(out ReadOnlySpan<byte> precompiledBytecode);

        // If a precompiled shader is available, return it
        if (!precompiledBytecode.IsEmpty)
        {
            bytecode = new PinnedBufferMemoryManager(precompiledBytecode).Memory;
        }

        // Otherwise, return the dynamic shader instead
        byte* bytecodePtr = (byte*)dynamicBytecode.Get()->GetBufferPointer();
        int bytecodeSize = (int)dynamicBytecode.Get()->GetBufferSize();

        bytecode = new ReadOnlySpan<byte>(bytecodePtr, bytecodeSize).ToArray();
    }

    /// <summary>
    /// Gets the constant buffer from an input D2D1 pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to retrieve info for.</typeparam>
    /// <param name="shader">The input D2D1 pixel shader to retrieve info for.</param>
    /// <param name="buffer">A <see cref="ReadOnlyMemory{T}"/> instance with the pixel shader constant buffer.</param>
    /// <remarks>
    /// This method will allocate a buffer every time it is invoked.
    /// For a zero-allocation alternative, use <see cref="SetPixelShaderConstantBufferForD2D1DrawInfo"/>.</remarks>
    public static void GetPixelShaderConstantBufferForD2D1DrawInfo<T>(in T shader, out ReadOnlyMemory<byte> buffer)
        where T : unmanaged, ID2D1PixelShader
    {
        D2D1ByteArrayDispatchDataLoader dataLoader = default;

        Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);

        buffer = dataLoader.GetResultingDispatchData();
    }

    /// <summary>
    /// Sets the constant buffer from an input D2D1 pixel shader, by calling <c>ID2D1DrawInfo::SetPixelShaderConstantBuffer</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to retrieve info for.</typeparam>
    /// <param name="shader">The input D2D1 pixel shader to retrieve info for.</param>
    /// <param name="d2D1DrawInfo">A pointer to the <c>ID2D1DrawInfo</c> instance to use.</param>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1drawinfo-setpixelshaderconstantbuffer"/>.</remarks>
    public static void SetPixelShaderConstantBufferForD2D1DrawInfo<T>(in T shader, void* d2D1DrawInfo)
        where T : unmanaged, ID2D1PixelShader
    {
        D2D1DrawInfoDispatchDataLoader dataLoader = new((ID2D1DrawInfo*)d2D1DrawInfo);

        Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);
    }
}
