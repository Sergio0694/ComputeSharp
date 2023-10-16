using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ComputeSharp.Core.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Shaders.Translation;

/// <summary>
/// A <see langword="class"/> that uses the DXC APIs to compile compute shaders.
/// </summary>
internal sealed unsafe class ShaderCompiler
{
    /// <summary>
    /// The thread local <see cref="ShaderCompiler"/> instance.
    /// This is necessary because the DXC library is strictly single-threaded.
    /// </summary>
    [ThreadStatic]
    private static ShaderCompiler? instance;

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
    /// Creates a new <see cref="ShaderCompiler"/> instance.
    /// </summary>
    private ShaderCompiler()
    {
        using ComPtr<IDxcCompiler> dxcCompiler = default;
        using ComPtr<IDxcLibrary> dxcLibrary = default;
        using ComPtr<IDxcIncludeHandler> dxcIncludeHandler = default;

        DirectX.DxcCreateInstance(
            (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_DxcCompiler)),
            Windows.__uuidof<IDxcCompiler>(),
            (void**)dxcCompiler.GetAddressOf()).Assert();

        DirectX.DxcCreateInstance(
            (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_DxcLibrary)),
            Windows.__uuidof<IDxcLibrary>(),
            (void**)dxcLibrary.GetAddressOf()).Assert();

        dxcLibrary.Get()->CreateIncludeHandler(dxcIncludeHandler.GetAddressOf()).Assert();

        this.dxcCompiler = dxcCompiler.Move();
        this.dxcLibrary = dxcLibrary.Move();
        this.dxcIncludeHandler = dxcIncludeHandler.Move();
    }

    /// <summary>
    /// Destroys the current <see cref="ShaderCompiler"/> instance.
    /// </summary>
    ~ShaderCompiler()
    {
        this.dxcCompiler.Dispose();
        this.dxcLibrary.Dispose();
        this.dxcIncludeHandler.Dispose();
    }

    /// <summary>
    /// Gets a <see cref="ShaderCompiler"/> instance to use.
    /// </summary>
    public static ShaderCompiler Instance => instance ??= new();

    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <param name="source">The HLSL source code to compile.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    public ComPtr<IDxcBlob> Compile(ReadOnlySpan<char> source)
    {
        using ComPtr<IDxcBlobEncoding> dxcBlobEncoding = default;
        using ComPtr<IDxcOperationResult> dxcOperationResult = default;
        using ComPtr<IDxcBlob> dxcBlobBytecode = default;

        // Get the encoded blob from the source code
        fixed (char* p = source)
        {
            this.dxcLibrary.Get()->CreateBlobWithEncodingOnHeapCopy(
                p,
                (uint)source.Length * 2,
                1200,
                dxcBlobEncoding.GetAddressOf()).Assert();
        }

        // Try to compile the new compute shader
        fixed (char* shaderName = "")
        fixed (char* entryPoint = nameof(IComputeShader.Execute))
        fixed (char* shaderProfile = "cs_6_0")
        fixed (char* optimization = "-O3")
        fixed (char* rowMajor = "-Zpr")
        fixed (char* warningsAsErrors = "-Werror")
        {
            char** arguments = stackalloc char*[3] { optimization, rowMajor, warningsAsErrors };

            this.dxcCompiler.Get()->Compile(
                (IDxcBlob*)dxcBlobEncoding.Get(),
                (ushort*)shaderName,
                (ushort*)entryPoint,
                (ushort*)shaderProfile,
                (ushort**)arguments,
                3,
                null,
                0,
                this.dxcIncludeHandler.Get(),
                dxcOperationResult.GetAddressOf()).Assert();
        }

        HRESULT status;

        dxcOperationResult.Get()->GetStatus(&status).Assert();

        // The compilation was successful, so we can extract the shader bytecode
        if (status == 0)
        {
            dxcOperationResult.Get()->GetResult(dxcBlobBytecode.GetAddressOf()).Assert();

            return dxcBlobBytecode.Move();
        }

        return ThrowHslsCompilationException(dxcOperationResult.Get());
    }

    /// <summary>
    /// Throws an exception when a shader compilation fails.
    /// </summary>
    /// <param name="dxcOperationResult">The input (faulting) operation.</param>
    /// <returns>This method always throws and never actually returs.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static ComPtr<IDxcBlob> ThrowHslsCompilationException(IDxcOperationResult* dxcOperationResult)
    {
        using ComPtr<IDxcBlobEncoding> dxcBlobEncodingError = default;

        dxcOperationResult->GetErrorBuffer(dxcBlobEncodingError.GetAddressOf()).Assert();

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