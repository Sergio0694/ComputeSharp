using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using ComputeSharp.D2D1.Exceptions;
using ComputeSharp.D2D1.Extensions;
#if !NET6_0_OR_GREATER
using ComputeSharp.D2D1.NetStandard.System.Text;
#endif
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable IDE1006

namespace ComputeSharp.D2D1.Shaders.Translation;

/// <summary>
/// A <see langword="class"/> that uses the FXC APIs to compile D2D1 pixel shaders.
/// </summary>
internal static unsafe partial class D2D1ShaderCompiler
{
    /// <summary>
    /// The <see cref="ID3DInclude"/> instance to load <c>d2d1effecthelpers.hlsli</c>.
    /// </summary>
    private static readonly ID3DIncludeForD2DHelpers* D3DIncludeForD2D1EffectHelpers = ID3DIncludeForD2DHelpers.Create();

    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <param name="source">The HLSL source code to compile.</param>
    /// <param name="shaderProfile">The shader profile to use to compile the shader.</param>
    /// <param name="enableLinkingSupport">Whether to enable linking support for the shader.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    public static ComPtr<ID3DBlob> Compile(ReadOnlySpan<char> source, D2D1ShaderProfile shaderProfile, bool enableLinkingSupport)
    {
        // Compile the standalone D2D1 full shader
        using ComPtr<ID3DBlob> d3DBlobFullShader = CompileD2DFullShader(source, shaderProfile);

        if (!enableLinkingSupport)
        {
            return d3DBlobFullShader.Move();
        }

        // Compile the export function and embed it as private data if requested
        using ComPtr<ID3DBlob> d3DBlobFunction = CompileD2DFunction(source, shaderProfile);
        using ComPtr<ID3DBlob> d3DBlobLinked = EmbedD2DFunctionPrivateData(d3DBlobFullShader.Get(), d3DBlobFunction.Get());

        return d3DBlobLinked.Move();
    }

    /// <summary>
    /// Compiles a D2D1 pixel shader with <c>D2D_FULL_SHADER</c>.
    /// </summary>
    /// <param name="source">The HLSL source code to compile.</param>
    /// <param name="shaderProfile">The shader profile to use to compile the shader.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    private static ComPtr<ID3DBlob> CompileD2DFullShader(ReadOnlySpan<char> source, D2D1ShaderProfile shaderProfile)
    {
        // Encode the HLSL source to ASCII
        int maxLength = Encoding.ASCII.GetMaxByteCount(source.Length);
        byte[] buffer = ArrayPool<byte>.Shared.Rent(maxLength);
        int writtenBytes = Encoding.ASCII.GetBytes(source, buffer);

        using ComPtr<ID3DBlob> d3DBlobBytecode = default;
        using ComPtr<ID3DBlob> d3DBlobErrors = default;

        int hResult;

        fixed (byte* bufferPtr = buffer)
        {
            // Prepare the macros for full shader compilation:
            //
            // -D D2D_FULL_SHADER
            // -D D2D_ENTRY=Execute
            D3D_SHADER_MACRO* macros = stackalloc D3D_SHADER_MACRO[]
            {
                new()
                {
                    Name = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.D2D_FULL_SHADER)),
                    Definition = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.NullTerminator))
                },
                new()
                {
                    Name = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.D2D_ENTRY)),
                    Definition = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.Execute))
                },
                new()
            };

            // Compile the shader with -ps_5_0 -O3 -We -Zpr
            hResult = DirectX.D3DCompile(
                pSrcData: bufferPtr,
                SrcDataSize: (nuint)writtenBytes,
                pSourceName: null,
                pDefines: macros,
                pInclude: D3DIncludeForD2D1EffectHelpers,
                pEntrypoint: (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.Execute)),
                pTarget: (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.GetPixelShaderProfile(shaderProfile))),
                Flags1: D3DCOMPILE.D3DCOMPILE_OPTIMIZATION_LEVEL3 | D3DCOMPILE.D3DCOMPILE_WARNINGS_ARE_ERRORS | D3DCOMPILE.D3DCOMPILE_PACK_MATRIX_ROW_MAJOR,
                Flags2: 0,
                ppCode: d3DBlobBytecode.GetAddressOf(),
                ppErrorMsgs: d3DBlobErrors.GetAddressOf());
        }

        ArrayPool<byte>.Shared.Return(buffer);

        // Throw if an error was retrieved, then also double check the HRESULT
        if (d3DBlobErrors.Get() is not null)
        {
            ThrowHslsCompilationException(d3DBlobErrors.Get());
        }

        hResult.Assert();

        return d3DBlobBytecode.Move();
    }

    /// <summary>
    /// Compiles a D2D1 pixel shader with <c>D2D_FUNCTION</c>.
    /// </summary>
    /// <param name="source">The HLSL source code to compile.</param>
    /// <param name="shaderProfile">The shader profile to use to compile the shader.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    private static ComPtr<ID3DBlob> CompileD2DFunction(ReadOnlySpan<char> source, D2D1ShaderProfile shaderProfile)
    {
        int maxLength = Encoding.ASCII.GetMaxByteCount(source.Length);
        byte[] buffer = ArrayPool<byte>.Shared.Rent(maxLength);
        int writtenBytes = Encoding.ASCII.GetBytes(source, buffer);

        using ComPtr<ID3DBlob> d3DBlobBytecode = default;
        using ComPtr<ID3DBlob> d3DBlobErrors = default;

        int hResult;

        fixed (byte* bufferPtr = buffer)
        {
            // Prepare the macros for full shader compilation:
            //
            // -D D2D_FUNCTION
            // -D D2D_ENTRY=Execute
            D3D_SHADER_MACRO* macros = stackalloc D3D_SHADER_MACRO[]
            {
                new()
                {
                    Name = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.D2D_FUNCTION)),
                    Definition = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.NullTerminator))
                },
                new()
                {
                    Name = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.D2D_ENTRY)),
                    Definition = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.Execute))
                },
                new()
            };

            // Compile the shader with -lib_5_0 -O3 -We -Zpr
            hResult = DirectX.D3DCompile(
                pSrcData: bufferPtr,
                SrcDataSize: (nuint)writtenBytes,
                pSourceName: null,
                pDefines: macros,
                pInclude: D3DIncludeForD2D1EffectHelpers,
                pEntrypoint: null,
                pTarget: (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.GetLibraryProfile(shaderProfile))),
                Flags1: D3DCOMPILE.D3DCOMPILE_OPTIMIZATION_LEVEL3 | D3DCOMPILE.D3DCOMPILE_WARNINGS_ARE_ERRORS | D3DCOMPILE.D3DCOMPILE_PACK_MATRIX_ROW_MAJOR,
                Flags2: 0,
                ppCode: d3DBlobBytecode.GetAddressOf(),
                ppErrorMsgs: d3DBlobErrors.GetAddressOf());
        }

        ArrayPool<byte>.Shared.Return(buffer);

        // Check for errors and double check like above
        if (d3DBlobErrors.Get() is not null)
        {
            ThrowHslsCompilationException(d3DBlobErrors.Get());
        }

        hResult.Assert();

        return d3DBlobBytecode.Move();
    }

    /// <summary>
    /// Embeds the bytecode for an exported shader as private data into another shader bytecode.
    /// </summary>
    /// <param name="shaderBlob">The bytecode produced by <see cref="CompileD2DFullShader(ReadOnlySpan{char}, D2D1ShaderProfile)"/>.</param>
    /// <param name="exportBlob">The bytecode produced by <see cref="CompileD2DFunction(ReadOnlySpan{char}, D2D1ShaderProfile)"/>.</param>
    /// <returns>An <see cref="ID3DBlob"/> instance with the combined data of <paramref name="shaderBlob"/> and <paramref name="exportBlob"/>.</returns>
    private static ComPtr<ID3DBlob> EmbedD2DFunctionPrivateData(ID3DBlob* shaderBlob, ID3DBlob* exportBlob)
    {
        void* shaderPtr = shaderBlob->GetBufferPointer();
        nuint shaderSize = shaderBlob->GetBufferSize();

        void* exportPtr = exportBlob->GetBufferPointer();
        nuint exportSize = exportBlob->GetBufferSize();

        using ComPtr<ID3DBlob> resultBlob = default;

        DirectX.D3DSetBlobPart(
            pSrcData: shaderPtr,
            SrcDataSize: shaderSize,
            Part: D3D_BLOB_PART.D3D_BLOB_PRIVATE_DATA,
            Flags: 0,
            pPart: exportPtr,
            PartSize: exportSize,
            ppNewShader: resultBlob.GetAddressOf()).Assert();

        return resultBlob.Move();
    }

    /// <summary>
    /// Throws an exception when a shader compilation fails.
    /// </summary>
    /// <param name="d3DOperationResult">The input (faulting) operation.</param>
    /// <returns>This method always throws and never actually returs.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowHslsCompilationException(ID3DBlob* d3DOperationResult)
    {
        string message = new((sbyte*)d3DOperationResult->GetBufferPointer());

        // The error message will be in a format like this:
        // "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\Roslyn\Shader@0x0000019AD1B4BA70(22,32-35): error X3004: undeclared identifier 'this'"
        // This regex tries to match the unnecessary header and remove it, if present. This doesn't need to be bulletproof, and this regex should match all cases anyway.
        message = Regex.Replace(message, @"[A-Z]:\\[^:]+: (\w+ \w+:)", static m => m.Groups[1].Value).Trim();

        // Add a trailing '.' if not present
        if (message is { Length: > 0 } &&
            message[message.Length - 1] != '.')
        {
            message += '.';
        }

        throw new FxcCompilationException(message);
    }

    /// <summary>
    /// A container for ASCII encoded, null-terminated constant strings.
    /// </summary>
    private static class ASCII
    {
        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"D2D_FUNCTION"</c> ASCII text.
        /// </summary>
        public static ReadOnlySpan<byte> D2D_FUNCTION => new[] { (byte)'D', (byte)'2', (byte)'D', (byte)'_', (byte)'F', (byte)'U', (byte)'N', (byte)'C', (byte)'T', (byte)'I', (byte)'O', (byte)'N', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"D2D_FULL_SHADER"</c> ASCII text.
        /// </summary>
        public static ReadOnlySpan<byte> D2D_FULL_SHADER => new[] { (byte)'D', (byte)'2', (byte)'D', (byte)'_', (byte)'F', (byte)'U', (byte)'L', (byte)'L', (byte)'_', (byte)'S', (byte)'H', (byte)'A', (byte)'D', (byte)'E', (byte)'R', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>'\0'</c> ASCII character.
        /// </summary>
        public static ReadOnlySpan<byte> NullTerminator => new[] { (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"D2D_ENTRY"</c> ASCII text.
        /// </summary>
        public static ReadOnlySpan<byte> D2D_ENTRY => new[] { (byte)'D', (byte)'2', (byte)'D', (byte)'_', (byte)'E', (byte)'N', (byte)'T', (byte)'R', (byte)'Y', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"Execute"</c> ASCII text.
        /// </summary>
        public static ReadOnlySpan<byte> Execute => new[] { (byte)'E', (byte)'x', (byte)'e', (byte)'c', (byte)'u', (byte)'t', (byte)'e', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"lib_4_0_level_9_1"</c> ASCII text.
        /// </summary>
        private static ReadOnlySpan<byte> lib_4_0_level_9_1 => new[] { (byte)'l', (byte)'i', (byte)'b', (byte)'_', (byte)'4', (byte)'_', (byte)'0', (byte)'_', (byte)'l', (byte)'e', (byte)'v', (byte)'e', (byte)'l', (byte)'_', (byte)'9', (byte)'_', (byte)'1', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"lib_4_0_level_9_3"</c> ASCII text.
        /// </summary>
        private static ReadOnlySpan<byte> lib_4_0_level_9_3 => new[] { (byte)'l', (byte)'i', (byte)'b', (byte)'_', (byte)'4', (byte)'_', (byte)'0', (byte)'_', (byte)'l', (byte)'e', (byte)'v', (byte)'e', (byte)'l', (byte)'_', (byte)'9', (byte)'_', (byte)'3', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"lib_4_0"</c> ASCII text.
        /// </summary>
        private static ReadOnlySpan<byte> lib_4_0 => new[] { (byte)'l', (byte)'i', (byte)'b', (byte)'_', (byte)'4', (byte)'_', (byte)'0', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"lib_4_1"</c> ASCII text.
        /// </summary>
        private static ReadOnlySpan<byte> lib_4_1 => new[] { (byte)'l', (byte)'i', (byte)'b', (byte)'_', (byte)'4', (byte)'_', (byte)'1', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"lib_5_0"</c> ASCII text.
        /// </summary>
        private static ReadOnlySpan<byte> lib_5_0 => new[] { (byte)'l', (byte)'i', (byte)'b', (byte)'_', (byte)'5', (byte)'_', (byte)'0', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"ps_4_0_level_9_1"</c> ASCII text.
        /// </summary>
        private static ReadOnlySpan<byte> ps_4_0_level_9_1 => new[] { (byte)'p', (byte)'s', (byte)'_', (byte)'4', (byte)'_', (byte)'0', (byte)'_', (byte)'l', (byte)'e', (byte)'v', (byte)'e', (byte)'l', (byte)'_', (byte)'9', (byte)'_', (byte)'1', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"ps_4_0_level_9_3"</c> ASCII text.
        /// </summary>
        private static ReadOnlySpan<byte> ps_4_0_level_9_3 => new[] { (byte)'p', (byte)'s', (byte)'_', (byte)'4', (byte)'_', (byte)'0', (byte)'_', (byte)'l', (byte)'e', (byte)'v', (byte)'e', (byte)'l', (byte)'_', (byte)'9', (byte)'_', (byte)'3', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"ps_4_0"</c> ASCII text.
        /// </summary>
        private static ReadOnlySpan<byte> ps_4_0 => new[] { (byte)'p', (byte)'s', (byte)'_', (byte)'4', (byte)'_', (byte)'0', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"ps_4_1"</c> ASCII text.
        /// </summary>
        private static ReadOnlySpan<byte> ps_4_1 => new[] { (byte)'p', (byte)'s', (byte)'_', (byte)'4', (byte)'_', (byte)'1', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"ps_5_0"</c> ASCII text.
        /// </summary>
        private static ReadOnlySpan<byte> ps_5_0 => new[] { (byte)'p', (byte)'s', (byte)'_', (byte)'5', (byte)'_', (byte)'0', (byte)'\0' };

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> for a given shader profile, for a library.
        /// </summary>
        /// <param name="shaderProfile">The input shaader profile to use.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> for a given shader profile, for a library.</returns>
        public static ReadOnlySpan<byte> GetLibraryProfile(D2D1ShaderProfile shaderProfile)
        {
            return shaderProfile switch
            {
                D2D1ShaderProfile.PixelShader40Level91 => lib_4_0_level_9_1,
                D2D1ShaderProfile.PixelShader40Level93 => lib_4_0_level_9_3,
                D2D1ShaderProfile.PixelShader40 => lib_4_0,
                D2D1ShaderProfile.PixelShader41 => lib_4_1,
                _ => lib_5_0
            };
        }

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> for a given shader profile, for a full shader.
        /// </summary>
        /// <param name="shaderProfile">The input shaader profile to use.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> for a given shader profile, for a full shader.</returns>
        public static ReadOnlySpan<byte> GetPixelShaderProfile(D2D1ShaderProfile shaderProfile)
        {
            return shaderProfile switch
            {
                D2D1ShaderProfile.PixelShader40Level91 => ps_4_0_level_9_1,
                D2D1ShaderProfile.PixelShader40Level93 => ps_4_0_level_9_3,
                D2D1ShaderProfile.PixelShader40 => ps_4_0,
                D2D1ShaderProfile.PixelShader41 => ps_4_1,
                _ => ps_5_0
            };
        }
    }
}
