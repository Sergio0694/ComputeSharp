using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Exceptions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using DirectX = TerraFX.Interop.DirectX.DirectX2;
#endif

namespace ComputeSharp.Shaders.Translation;

/// <summary>
/// A <see langword="class"/> that uses the DXC APIs to compile compute shaders.
/// </summary>
internal sealed unsafe partial class ShaderCompiler
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
    private readonly ComPtr<IDxcCompiler> DxcCompiler;

    /// <summary>
    /// The <see cref="IDxcLibrary"/> instance to use to work with <see cref="DxcCompiler"/>.
    /// </summary>
    private readonly ComPtr<IDxcLibrary> DxcLibrary;

    /// <summary>
    /// The <see cref="IDxcIncludeHandler"/> instance used to compile shaders with <see cref="DxcCompiler"/>.
    /// </summary>
    private readonly ComPtr<IDxcIncludeHandler> DxcIncludeHandler;

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
            dxcCompiler.GetVoidAddressOf()).Assert();

        DirectX.DxcCreateInstance(
            (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_DxcLibrary)),
            Windows.__uuidof<IDxcLibrary>(),
            dxcLibrary.GetVoidAddressOf()).Assert();

        dxcLibrary.Get()->CreateIncludeHandler(dxcIncludeHandler.GetAddressOf()).Assert();

        DxcCompiler = dxcCompiler.Move();
        DxcLibrary = dxcLibrary.Move();
        DxcIncludeHandler = dxcIncludeHandler.Move();
    }

    /// <summary>
    /// Destroys the current <see cref="ShaderCompiler"/> instance.
    /// </summary>
    ~ShaderCompiler()
    {
        DxcCompiler.Dispose();
        DxcLibrary.Dispose();
        DxcIncludeHandler.Dispose();
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
        fixed (char* optimization = "-O3")
        fixed (char* rowMajor = "-Zpr")
        fixed (char* warningsAsErrors = "-Werror")
        {
            const string profile = "cs_6_0";
            const string entryPoint = nameof(IComputeShader.Execute);

            ReadOnlySpan<IntPtr> arguments = stackalloc IntPtr[] { (IntPtr)optimization, (IntPtr)rowMajor, (IntPtr)warningsAsErrors };

            return Compile(source, profile.AsSpan(), entryPoint.AsSpan(), arguments);
        }
    }

    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <param name="source">The HLSL source code to compile.</param>
    /// <param name="profile">The profile to use for compilation.</param>
    /// <param name="entryPoint">The entry point for the shader.</param>
    /// <param name="arguments">The arguments to use for compilation (each item must be a <see cref="char"/>* to an individual argument).</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    public ComPtr<IDxcBlob> Compile(ReadOnlySpan<char> source, ReadOnlySpan<char> profile, ReadOnlySpan<char> entryPoint, ReadOnlySpan<IntPtr> arguments)
    {
        using ComPtr<IDxcBlobEncoding> dxcBlobEncoding = default;
        using ComPtr<IDxcOperationResult> dxcOperationResult = default;
        using ComPtr<IDxcBlob> dxcBlobBytecode = default;

        // Get the encoded blob from the source code
        fixed (char* p = source)
        {
            DxcLibrary.Get()->CreateBlobWithEncodingOnHeapCopy(
                p,
                (uint)source.Length * 2,
                1200,
                dxcBlobEncoding.GetAddressOf()).Assert();
        }

        // Try to compile the new shader
        fixed (char* shaderNamePtr = "")
        fixed (char* entryPointPtr = entryPoint)
        fixed (char* shaderProfilePtr = profile)
        fixed (void* argumentsPtr = arguments)
        {
            DxcCompiler.Get()->Compile(
                (IDxcBlob*)dxcBlobEncoding.Get(),
                (ushort*)shaderNamePtr,
                (ushort*)entryPointPtr,
                (ushort*)shaderProfilePtr,
                (ushort**)argumentsPtr,
                (uint)arguments.Length,
                null,
                0,
                DxcIncludeHandler.Get(),
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

        return ThrowHslsCompilationException(dxcOperationResult);
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

        throw new HlslCompilationException(message);
    }
}
