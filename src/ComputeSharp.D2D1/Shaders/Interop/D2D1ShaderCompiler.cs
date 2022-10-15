using System;
using System.Buffers;
using System.Text;
using ComputeSharp.D2D1.Exceptions;
#if !NET6_0_OR_GREATER
using ComputeSharp.D2D1.NetStandard.System.Text;
#endif
using ComputeSharp.D2D1.Shaders.Translation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// Provides methods to manually compile D2D1 shaders.
/// </summary>
public static class D2D1ShaderCompiler
{
    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <param name="hlslSource">The HLSL source code to compile.</param>
    /// <param name="entryPoint">The entry point of the shader being compiled.</param>
    /// <param name="shaderProfile">The shader profile to use to compile the shader.</param>
    /// <param name="options">The compiler options to use to compile the shader.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    /// <exception cref="FxcCompilationException">Thrown if the compilation fails.</exception>
    public static ReadOnlyMemory<byte> Compile(
        ReadOnlySpan<char> hlslSource,
        ReadOnlySpan<char> entryPoint,
        D2D1ShaderProfile shaderProfile,
        D2D1CompileOptions options)
    {
        // Encode the HLSL source to ASCII
        int maxSourceLength = Encoding.ASCII.GetMaxByteCount(hlslSource.Length);
        byte[] sourceBuffer = ArrayPool<byte>.Shared.Rent(maxSourceLength);
        int sourceWrittenBytes = Encoding.ASCII.GetBytes(hlslSource, sourceBuffer);

        // Encode the entry point to ASCII
        int maxEntryPointLength = Encoding.ASCII.GetMaxByteCount(entryPoint.Length) + 1;
        byte[] entryPointBuffer = ArrayPool<byte>.Shared.Rent(maxEntryPointLength);
        int entryPointWrittenBytes = Encoding.ASCII.GetBytes(entryPoint, entryPointBuffer);

        // The entry point has to be null-terminated
        entryPointBuffer[entryPointWrittenBytes] = (byte)'\0';

        ReadOnlySpan<byte> sourceAscii = sourceBuffer.AsSpan(0, sourceWrittenBytes);
        ReadOnlySpan<byte> entryPointAscii = entryPointBuffer.AsSpan(0, entryPointWrittenBytes);

        ReadOnlyMemory<byte> bytecode = Compile(sourceAscii, entryPointAscii, shaderProfile, options);

        ArrayPool<byte>.Shared.Return(sourceBuffer);
        ArrayPool<byte>.Shared.Return(entryPointBuffer);

        return bytecode;
    }

    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <param name="hlslSourceAscii">The HLSL source code to compile (in ASCII).</param>
    /// <param name="entryPointAscii">The entry point of the shader being compiled (in ASCII, null-terminated).</param>
    /// <param name="shaderProfile">The shader profile to use to compile the shader.</param>
    /// <param name="options">The compiler options to use to compile the shader.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    /// <exception cref="FxcCompilationException">Thrown if the compilation fails.</exception>
    public static unsafe ReadOnlyMemory<byte> Compile(
        ReadOnlySpan<byte> hlslSourceAscii,
        ReadOnlySpan<byte> entryPointAscii,
        D2D1ShaderProfile shaderProfile,
        D2D1CompileOptions options)
    {
        // Check linking support
        bool enableLinking = (options & D2D1CompileOptions.EnableLinking) == D2D1CompileOptions.EnableLinking;

        // Remove the linking flag to make the options blittable to flags
        options &= ~D2D1CompileOptions.EnableLinking;

        // Compile the standalone D2D1 full shader
        using ComPtr<ID3DBlob> d3DBlobFullShader = D3DCompiler.CompileShader(
            source: hlslSourceAscii,
            macro: D3DCompiler.ASCII.D2D_FULL_SHADER,
            d2DEntry: entryPointAscii,
            entryPoint: entryPointAscii,
            target: D3DCompiler.ASCII.GetPixelShaderProfile(shaderProfile),
            flags: (uint)options);

        if (!enableLinking)
        {
            void* blobFullShaderPtr = d3DBlobFullShader.Get()->GetBufferPointer();
            nuint blobFullShaderSize = d3DBlobFullShader.Get()->GetBufferSize();

            return new Span<byte>(blobFullShaderPtr, (int)blobFullShaderSize).ToArray();
        }

        // Compile the export function
        using ComPtr<ID3DBlob> d3DBlobFunction = D3DCompiler.CompileShader(
            source: hlslSourceAscii,
            macro: D3DCompiler.ASCII.D2D_FUNCTION,
            d2DEntry: entryPointAscii,
            entryPoint: default,
            target: D3DCompiler.ASCII.GetLibraryProfile(shaderProfile),
            flags: (uint)options);

        // Embed it as private data if requested
        using ComPtr<ID3DBlob> d3DBlobLinked = D3DCompiler.SetD3DPrivateData(d3DBlobFullShader.Get(), d3DBlobFunction.Get());

        void* blobLinkedPtr = d3DBlobLinked.Get()->GetBufferPointer();
        nuint blobLinkedSize = d3DBlobLinked.Get()->GetBufferSize();

        return new Span<byte>(blobLinkedPtr, (int)blobLinkedSize).ToArray();
    }
}