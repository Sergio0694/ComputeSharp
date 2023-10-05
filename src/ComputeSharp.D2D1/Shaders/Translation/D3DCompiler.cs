using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Shaders.Translation;

/// <summary>
/// A <see langword="class"/> that uses the FXC APIs to compile D2D1 pixel shaders.
/// </summary>
internal static unsafe partial class D3DCompiler
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
    /// <param name="options">The options to use to compile the shader.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    public static ComPtr<ID3DBlob> Compile(ReadOnlySpan<char> source, D2D1ShaderProfile shaderProfile, D2D1CompileOptions options)
    {
        int maxLength = Encoding.ASCII.GetMaxByteCount(source.Length);
        byte[] buffer = ArrayPool<byte>.Shared.Rent(maxLength);
        int writtenBytes = Encoding.ASCII.GetBytes(source, buffer);

        try
        {
            // Compile the standalone D2D1 full shader
            using ComPtr<ID3DBlob> d3DBlobFullShader = Compile(
                source: buffer.AsSpan(0, writtenBytes),
                macro: ASCII.D2D_FULL_SHADER,
                d2DEntry: ASCII.Execute,
                entryPoint: ASCII.Execute,
                target: ASCII.GetPixelShaderProfile(shaderProfile),
                flags: (uint)(options & ~D2D1CompileOptions.EnableLinking));

            if ((options & D2D1CompileOptions.EnableLinking) == 0)
            {
                return d3DBlobFullShader.Move();
            }

            // Compile the export function
            using ComPtr<ID3DBlob> d3DBlobFunction = Compile(
                source: buffer.AsSpan(0, writtenBytes),
                macro: ASCII.D2D_FUNCTION,
                d2DEntry: ASCII.Execute,
                entryPoint: default,
                target: ASCII.GetLibraryProfile(shaderProfile),
                flags: (uint)(options & ~D2D1CompileOptions.EnableLinking));

            // Embed it as private data if requested
            using ComPtr<ID3DBlob> d3DBlobLinked = SetD3DPrivateData(d3DBlobFullShader.Get(), d3DBlobFunction.Get());

            return d3DBlobLinked.Move();
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    /// <summary>
    /// Compiles an HLSL shader with the specified parameters.
    /// </summary>
    /// <param name="source">The HLSL source code to compile (in ASCII).</param>
    /// <param name="macro">The target macro to define for the shader (either <c>D2D_FULL_SHADER</c> or <c>D2D_FUNCTION</c>).</param>
    /// <param name="d2DEntry">The D2D entry to specify when compiling the shader.</param>
    /// <param name="entryPoint">The entry point for the shader.</param>
    /// <param name="target">The shader target to use to compile the input source.</param>
    /// <param name="flags">The compilationm flag to use.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    /// <exception cref="FxcCompilationException">Thrown if the compilation fails.</exception>
    public static ComPtr<ID3DBlob> Compile(
        ReadOnlySpan<byte> source,
        ReadOnlySpan<byte> macro,
        ReadOnlySpan<byte> d2DEntry,
        ReadOnlySpan<byte> entryPoint,
        ReadOnlySpan<byte> target,
        uint flags)
    {
        using ComPtr<ID3DBlob> d3DBlobBytecode = default;
        using ComPtr<ID3DBlob> d3DBlobErrors = default;

        int hResult;

        fixed (byte* sourcePtr = source)
        fixed (byte* macroPtr = macro)
        fixed (byte* d2DEntryPtr = d2DEntry)
        fixed (byte* entryPointPtr = entryPoint)
        fixed (byte* targetPtr = target)
        {
            // Prepare the macros for full shader compilation:
            //
            // -D <MACRO>
            // -D D2D_ENTRY=<D2DENTRY>
            // <EMPTY_MARKER>
            D3D_SHADER_MACRO* macros = stackalloc D3D_SHADER_MACRO[]
            {
                new()
                {
                    Name = (sbyte*)macroPtr,
                    Definition = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.NullTerminator))
                },
                new()
                {
                    Name = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.D2D_ENTRY)),
                    Definition = (sbyte*)d2DEntryPtr
                },
                new()
            };

            // Compile the shader
            hResult = DirectX.D3DCompile(
                pSrcData: sourcePtr,
                SrcDataSize: (nuint)source.Length,
                pSourceName: null,
                pDefines: macros,
                pInclude: D3DIncludeForD2D1EffectHelpers,
                pEntrypoint: (sbyte*)entryPointPtr,
                pTarget: (sbyte*)targetPtr,
                Flags1: flags,
                Flags2: 0,
                ppCode: d3DBlobBytecode.GetAddressOf(),
                ppErrorMsgs: d3DBlobErrors.GetAddressOf());
        }

        // Throw if an error was retrieved, then also double check the HRESULT
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
    /// <param name="shaderBlob">The bytecode for the full shader.</param>
    /// <param name="exportBlob">The bytecode for the shader function to export.</param>
    /// <returns>An <see cref="ID3DBlob"/> instance with the combined data of <paramref name="shaderBlob"/> and <paramref name="exportBlob"/>.</returns>
    public static ComPtr<ID3DBlob> SetD3DPrivateData(ID3DBlob* shaderBlob, ID3DBlob* exportBlob)
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
    /// Returns a "prettified" version of an FXC error message.
    /// </summary>
    /// <param name="message">The input error message.</param>
    /// <returns>The "prettified" error message.</returns>
    public static string PrettifyFxcErrorMessage(string message)
    {
        // The error message will be in a format like this:
        // "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\Roslyn\Shader@0x0000019AD1B4BA70(22,32-35): error X3004: undeclared identifier 'this'"
        // This regex tries to match the unnecessary header and remove it, if present. This doesn't need to be bulletproof, and this regex should match all cases anyway.
        //
        // We cannot use Regex (this method was originally using Regex.Replace(message, @"^[A-Z]:\\[^:]+: (\w+ \w+:)", ...)), because that
        // causes significant binary size increase, as it's the only thing rooting all the Regex stack across the whole library. So this
        // code reimplements equivalent logic in a simple way. Because this is a throw helper, allocating a little bit of garbage is fine.
        string[] lines = message.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        // Go over each (non empty) line and look for the leading file path to trim away
        foreach (ref string line in lines.AsSpan())
        {
            ReadOnlySpan<char> span = line.AsSpan().Trim();

            // Check whether the trimmed line starts with a drive letter path (eg. 'C:\') and get the rest
            if (span is [>= 'A' and <= 'Z', ':', '\\', .. { Length: > 0 } tail])
            {
                int indexOfColon = tail.IndexOf(':');

                // We're looking for the color at the end of the file path. If we find it, and the line
                // has at least another character (ie. there's content after that, which should always
                // be the case), then we take that slice, trim its start to skip the leading whitespace,
                // and finally allocate a string to replace the current line.
                if (indexOfColon != -1 &&
                    tail.Length > indexOfColon)
                {
                    ReadOnlySpan<char> trimmedLine = tail.Slice(indexOfColon + 1).TrimStart();

                    line = trimmedLine.ToString();
                }
            }
        }

        // Merge the message again with normalized newlines
        string updatedMessage = string.Join("\r\n", lines);

        // Add a trailing '.' if not present
        if (updatedMessage is not [.., '.'])
        {
            updatedMessage += '.';
        }

        return updatedMessage;
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
        string updatedMessage = PrettifyFxcErrorMessage(message);

        throw new FxcCompilationException(updatedMessage);
    }

    /// <summary>
    /// A container for ASCII encoded, null-terminated constant strings.
    /// </summary>
    public static class ASCII
    {
        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"D2D_FUNCTION"</c> ASCII text.
        /// </summary>
        public static ReadOnlySpan<byte> D2D_FUNCTION => "D2D_FUNCTION"u8;

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"D2D_FULL_SHADER"</c> ASCII text.
        /// </summary>
        public static ReadOnlySpan<byte> D2D_FULL_SHADER => "D2D_FULL_SHADER"u8;

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>'\0'</c> ASCII character.
        /// </summary>
        public static ReadOnlySpan<byte> NullTerminator => ""u8;

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"D2D_ENTRY"</c> ASCII text.
        /// </summary>
        public static ReadOnlySpan<byte> D2D_ENTRY => "D2D_ENTRY"u8;

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"Execute"</c> ASCII text.
        /// </summary>
        public static ReadOnlySpan<byte> Execute => "Execute"u8;

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> for a given shader profile, for a library.
        /// </summary>
        /// <param name="shaderProfile">The input shaader profile to use.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> for a given shader profile, for a library.</returns>
        public static ReadOnlySpan<byte> GetLibraryProfile(D2D1ShaderProfile shaderProfile)
        {
            return shaderProfile switch
            {
                D2D1ShaderProfile.PixelShader40Level91 => "lib_4_0_level_9_1"u8,
                D2D1ShaderProfile.PixelShader40Level93 => "lib_4_0_level_9_3"u8,
                D2D1ShaderProfile.PixelShader40 => "lib_4_0"u8,
                D2D1ShaderProfile.PixelShader41 => "lib_4_1"u8,
                _ => "lib_5_0"u8
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
                D2D1ShaderProfile.PixelShader40Level91 => "ps_4_0_level_9_1"u8,
                D2D1ShaderProfile.PixelShader40Level93 => "ps_4_0_level_9_3"u8,
                D2D1ShaderProfile.PixelShader40 => "ps_4_0"u8,
                D2D1ShaderProfile.PixelShader41 => "ps_4_1"u8,
                _ => "ps_5_0"u8
            };
        }
    }
}