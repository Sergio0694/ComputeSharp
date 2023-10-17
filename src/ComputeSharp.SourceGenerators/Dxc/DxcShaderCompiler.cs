using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using ComputeSharp.SourceGenerators.Dxc.Interop;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D.Dxc;

namespace ComputeSharp.SourceGenerators.Dxc;

/// <summary>
/// A <see langword="class"/> that uses the DXC APIs to compile compute shaders.
/// </summary>
internal sealed unsafe class DxcShaderCompiler
{
    /// <summary>
    /// The thread local <see cref="DxcShaderCompiler"/> instance.
    /// This is necessary because the DXC library is strictly single-threaded.
    /// </summary>
    [ThreadStatic]
    private static DxcShaderCompiler? instance;

    /// <summary>
    /// The <see cref="IDxcCompiler"/> instance to use to create the bytecode for HLSL sources.
    /// </summary>
    private readonly ComPtr<IDxcCompiler> dxcCompiler;

    /// <summary>
    /// The <see cref="IDxcLibrary"/> instance to use to work with <see cref="dxcCompiler"/>.
    /// </summary>
    private readonly ComPtr<IDxcLibrary> dxcLibrary;

    /// <summary>
    /// The <see cref="IDxcIncludeHandler"/> instance used to compile shaders with <see cref="dxcCompiler"/>.
    /// </summary>
    private readonly ComPtr<IDxcIncludeHandler> dxcIncludeHandler;

    /// <summary>
    /// Creates a new <see cref="DxcShaderCompiler"/> instance.
    /// </summary>
    private DxcShaderCompiler()
    {
        using ComPtr<IDxcCompiler> dxcCompiler = default;
        using ComPtr<IDxcLibrary> dxcLibrary = default;

        _ = PInvoke.DxcCreateInstance(
            PInvoke.CLSID_DxcCompiler,
            IDxcCompiler.IID_Guid,
            out *(void**)dxcCompiler.GetAddressOf()).ThrowOnFailure();

        _ = PInvoke.DxcCreateInstance(
            PInvoke.CLSID_DxcLibrary,
            IDxcLibrary.IID_Guid,
            out *(void**)dxcLibrary.GetAddressOf()).ThrowOnFailure();

        using ComPtr<IDxcIncludeHandler> dxcIncludeHandler = default;

        _ = dxcLibrary.Get()->CreateIncludeHandler(dxcIncludeHandler.GetAddressOf()).ThrowOnFailure();

        this.dxcCompiler = new ComPtr<IDxcCompiler>(dxcCompiler.Get());
        this.dxcLibrary = new ComPtr<IDxcLibrary>(dxcLibrary.Get());
        this.dxcIncludeHandler = new ComPtr<IDxcIncludeHandler>(dxcIncludeHandler.Get());
    }

    /// <summary>
    /// Destroys the current <see cref="DxcShaderCompiler"/> instance.
    /// </summary>
    ~DxcShaderCompiler()
    {
        this.dxcCompiler.Dispose();
        this.dxcLibrary.Dispose();
        this.dxcIncludeHandler.Dispose();
    }

    /// <summary>
    /// Gets a <see cref="DxcShaderCompiler"/> instance to use.
    /// </summary>
    public static DxcShaderCompiler Instance => instance ??= new();

    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <param name="source">The HLSL source code to compile.</param>
    /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    public byte[] Compile(ReadOnlySpan<char> source, CancellationToken token)
    {
        using ComPtr<IDxcBlobEncoding> dxcBlobEncoding = default;
        using ComPtr<IDxcOperationResult> dxcOperationResult = default;

        // Get the encoded blob from the source code
        fixed (char* p = source)
        {
            _ = this.dxcLibrary.Get()->CreateBlobWithEncodingOnHeapCopy(
                pText: p,
                size: (uint)source.Length * 2,
                codePage: DXC_CP.DXC_CP_UTF16,
                pBlobEncoding: dxcBlobEncoding.GetAddressOf()).ThrowOnFailure();
        }

        token.ThrowIfCancellationRequested();

        // Try to compile the new compute shader
        fixed (char* optimization = "-O3")
        fixed (char* rowMajor = "-Zpr")
        fixed (char* warningsAsErrors = "-Werror")
        {
            Span<PCWSTR> arguments = stackalloc PCWSTR[] { optimization, rowMajor, warningsAsErrors };

            _ = this.dxcCompiler.Get()->Compile(
                pSource: (IDxcBlob*)dxcBlobEncoding.Get(),
                pSourceName: "",
                pEntryPoint: "Execute",
                pTargetProfile: "cs_6_0",
                pArguments: arguments,
                pDefines: ReadOnlySpan<DxcDefine>.Empty,
                pIncludeHandler: this.dxcIncludeHandler.Get(),
                ppResult: dxcOperationResult.GetAddressOf()).ThrowOnFailure();
        }

        token.ThrowIfCancellationRequested();

        HRESULT status;

        _ = dxcOperationResult.Get()->GetStatus(&status).ThrowOnFailure();

        // The compilation was successful, so we can extract the shader bytecode
        if (status == 0)
        {
            using ComPtr<IDxcBlob> dxcBlobBytecode = default;

            _ = dxcOperationResult.Get()->GetResult(dxcBlobBytecode.GetAddressOf()).ThrowOnFailure();

            byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
            int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());

            return new ReadOnlySpan<byte>(buffer, length).ToArray();
        }

        return ThrowHslsCompilationException(dxcOperationResult.Get());
    }

    /// <summary>
    /// Throws an exception when a shader compilation fails.
    /// </summary>
    /// <param name="dxcOperationResult">The input (faulting) operation.</param>
    /// <returns>This method always throws and never actually returs.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static byte[] ThrowHslsCompilationException(IDxcOperationResult* dxcOperationResult)
    {
        using ComPtr<IDxcBlobEncoding> dxcBlobEncodingError = default;

        _ = dxcOperationResult->GetErrorBuffer(dxcBlobEncodingError.GetAddressOf()).ThrowOnFailure();

        string message = new((sbyte*)dxcBlobEncodingError.Get()->GetBufferPointer());

        // The error message will be in a format like this:
        // "hlsl.hlsl:11:20: error: redefinition of 'float1' as different kind of symbol
        //     static const float float1 = asfloat(0xFFC00000);
        //                        ^
        // note: previous definition is here"
        // These regex-s try to match the unnecessary headers and remove them, if present.
        // This doesn't need to be bulletproof, and these regex-s should match all cases anyway.
        message = Regex.Replace(message, @"^hlsl\.hlsl:\d+:\d+: (\w+:)", static m => m.Groups[1].Value, RegexOptions.Multiline);

        // Add a trailing '.' if not present
        if (message is [.., not '.'])
        {
            message += '.';
        }

        throw new DxcCompilationException(message);
    }
}