using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
#if SOURCE_GENERATOR
using System.Threading;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D.Fxc;
using DirectX = Windows.Win32.PInvoke;
#else
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.Win32;
#endif

namespace ComputeSharp.D2D1.Shaders.Translation;

/// <summary>
/// A <see langword="class"/> that uses the FXC APIs to compile D2D1 pixel shaders.
/// </summary>
internal static unsafe partial class D3DCompiler
{
    /// <summary>
    /// The <see cref="ID3DInclude"/> instance to load <c>d2d1effecthelpers.hlsli</c>.
    /// </summary>
    private static readonly ID3DInclude* D3DIncludeForD2D1EffectHelpers = ID3DIncludeForD2DHelpers.Create();

    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <param name="source">The HLSL source code to compile.</param>
    /// <param name="shaderProfile">The shader profile to use to compile the shader.</param>
    /// <param name="options">The options to use to compile the shader.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    public static ComPtr<ID3DBlob> Compile(
        ReadOnlySpan<char> source,
        D2D1ShaderProfile shaderProfile,
#if SOURCE_GENERATOR
#pragma warning disable CS1573
        D2D1CompileOptions options,
        CancellationToken token) // The cancellation token used to cancel the operation.
#pragma warning restore CS1573
#else
        D2D1CompileOptions options)
#endif
    {
        // Transcode the input HLSL source from Unicode to ASCII encoding
        int maxLength = Encoding.ASCII.GetMaxByteCount(source.Length);
        byte[] buffer = ArrayPool<byte>.Shared.Rent(maxLength);
        int writtenBytes = Encoding.ASCII.GetBytes(source, buffer);
        ReadOnlySpan<byte> sourceAscii = new(buffer, 0, writtenBytes);

        bool enableLinking = (options & D2D1CompileOptions.EnableLinking) == D2D1CompileOptions.EnableLinking;
        bool stripReflectionData = (options & D2D1CompileOptions.StripReflectionData) == D2D1CompileOptions.StripReflectionData;

        options &= ~D2D1CompileOptions.EnableLinking;
        options &= ~D2D1CompileOptions.StripReflectionData;

        try
        {
#if SOURCE_GENERATOR
            token.ThrowIfCancellationRequested();
#endif

            // Compile the standalone D2D1 full shader
            using ComPtr<ID3DBlob> d3DBlobFullShader = Compile(
                source: sourceAscii,
                macro: ASCII.D2D_FULL_SHADER,
                d2DEntry: ASCII.Execute,
                entryPoint: ASCII.Execute,
                target: ASCII.GetPixelShaderProfile(shaderProfile),
                flags: (uint)options,
                stripReflectionData: stripReflectionData);

            if (!enableLinking)
            {
                return d3DBlobFullShader.Move();
            }

#if SOURCE_GENERATOR
            token.ThrowIfCancellationRequested();
#endif

            // Compile the export function
            using ComPtr<ID3DBlob> d3DBlobFunction = Compile(
                source: sourceAscii,
                macro: ASCII.D2D_FUNCTION,
                d2DEntry: ASCII.Execute,
                entryPoint: default,
                target: ASCII.GetLibraryProfile(shaderProfile),
                flags: (uint)options,
                stripReflectionData: stripReflectionData);

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
    /// <param name="stripReflectionData">Whether to strip reflection data from the shader bytecode.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    /// <exception cref="FxcCompilationException">Thrown if the compilation fails.</exception>
    public static ComPtr<ID3DBlob> Compile(
        ReadOnlySpan<byte> source,
        ReadOnlySpan<byte> macro,
        ReadOnlySpan<byte> d2DEntry,
        ReadOnlySpan<byte> entryPoint,
        ReadOnlySpan<byte> target,
        uint flags,
        bool stripReflectionData)
    {
        using ComPtr<ID3DBlob> d3DBlobBytecode = default;
        using ComPtr<ID3DBlob> d3DBlobErrors = default;

        HRESULT hResult;

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
#if SOURCE_GENERATOR
                    Name = new PCSTR(macroPtr),
                    Definition = new PCSTR((byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.NullTerminator)))
#else
                    Name = (sbyte*)macroPtr,
                    Definition = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.NullTerminator))
#endif
                },
                new()
                {
#if SOURCE_GENERATOR
                    Name = new PCSTR((byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.D2D_ENTRY))),
                    Definition = new PCSTR(d2DEntryPtr)
#else
                    Name = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(ASCII.D2D_ENTRY)),
                    Definition = (sbyte*)d2DEntryPtr
#endif
                },
                new()
            };

            // Compile the shader
            hResult = DirectX.D3DCompile(
                pSrcData: sourcePtr,
                SrcDataSize: (nuint)source.Length,
#if SOURCE_GENERATOR
                pSourceName: (PCSTR)null,
                pDefines: macros,
                pInclude: D3DIncludeForD2D1EffectHelpers,
                pEntrypoint: new PCSTR(entryPointPtr),
                pTarget: new PCSTR(targetPtr),
#else
                pSourceName: null,
                pDefines: macros,
                pInclude: D3DIncludeForD2D1EffectHelpers,
                pEntrypoint: (sbyte*)entryPointPtr,
                pTarget: (sbyte*)targetPtr,
#endif
                Flags1: flags,
                Flags2: 0,
                ppCode: d3DBlobBytecode.GetAddressOf(),
                ppErrorMsgs: d3DBlobErrors.GetAddressOf());
        }

        // Throw an exception with the input messages, if the compilation fails.
        // If compilation succeeds with warnings, we just ignore the messages.
        if (hResult.Value < 0 && d3DBlobErrors.Get() is not null)
        {
            ThrowHslsCompilationException(d3DBlobErrors.Get());
        }

        // Also just assert the HRESULT in general after checking messages. Not entirely
        // clear if this is reachable in case the compilation fails (one would assume
        // there would always be a message), but better safe than sorry.
        hResult.Assert();

        // If requested, strip the reflection data and return that bytecode blob
        if (stripReflectionData)
        {
            using ComPtr<ID3DBlob> d3DBlobBytecodeNoReflection = default;

            DirectX.D3DStripShader(
                pShaderBytecode: d3DBlobBytecode.Get()->GetBufferPointer(),
                BytecodeLength: d3DBlobBytecode.Get()->GetBufferSize(),
                uStripFlags: (uint)D3DCOMPILER_STRIP_FLAGS.D3DCOMPILER_STRIP_REFLECTION_DATA,
                ppStrippedBlob: d3DBlobBytecodeNoReflection.GetAddressOf()).Assert();

            return d3DBlobBytecodeNoReflection.Move();
        }

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
        string[] lines = message.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

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
}