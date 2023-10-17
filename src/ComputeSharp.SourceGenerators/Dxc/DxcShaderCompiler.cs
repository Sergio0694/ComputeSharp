using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

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
        using ComPtr<IDxcIncludeHandler> dxcIncludeHandler = default;

        Marshal.ThrowExceptionForHR(DirectX.DxcCreateInstance(
            (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_DxcCompiler)),
            Windows.__uuidof<IDxcCompiler>(),
            (void**)dxcCompiler.GetAddressOf()));

        Marshal.ThrowExceptionForHR(DirectX.DxcCreateInstance(
            (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_DxcLibrary)),
            Windows.__uuidof<IDxcLibrary>(),
            (void**)dxcLibrary.GetAddressOf()));

        Marshal.ThrowExceptionForHR(dxcLibrary.Get()->CreateIncludeHandler(dxcIncludeHandler.GetAddressOf()));

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
            int hresult = this.dxcLibrary.Get()->CreateBlobWithEncodingOnHeapCopy(
                p,
                (uint)source.Length * 2,
                1200,
                dxcBlobEncoding.GetAddressOf());

            Marshal.ThrowExceptionForHR(hresult);
        }

        token.ThrowIfCancellationRequested();

        // Try to compile the new compute shader
        fixed (char* shaderName = "")
        fixed (char* entryPoint = nameof(IComputeShader.Execute))
        fixed (char* shaderProfile = "cs_6_0")
        fixed (char* optimization = "-O3")
        fixed (char* rowMajor = "-Zpr")
        fixed (char* warningsAsErrors = "-Werror")
        {
            char** arguments = stackalloc char*[3] { optimization, rowMajor, warningsAsErrors };

            int hresult = this.dxcCompiler.Get()->Compile(
                (IDxcBlob*)dxcBlobEncoding.Get(),
                (ushort*)shaderName,
                (ushort*)entryPoint,
                (ushort*)shaderProfile,
                (ushort**)arguments,
                3,
                null,
                0,
                this.dxcIncludeHandler.Get(),
                dxcOperationResult.GetAddressOf());

            Marshal.ThrowExceptionForHR(hresult);
        }

        token.ThrowIfCancellationRequested();

        HRESULT status;

        Marshal.ThrowExceptionForHR(dxcOperationResult.Get()->GetStatus(&status));

        // The compilation was successful, so we can extract the shader bytecode
        if (status == 0)
        {
            using ComPtr<IDxcBlob> dxcBlobBytecode = default;

            Marshal.ThrowExceptionForHR(dxcOperationResult.Get()->GetResult(dxcBlobBytecode.GetAddressOf()));

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

        Marshal.ThrowExceptionForHR(dxcOperationResult->GetErrorBuffer(dxcBlobEncodingError.GetAddressOf()));

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