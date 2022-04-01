using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
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

#if NET6_0_OR_GREATER
    public static ComPtr<ID3DBlob> CompilePixelShader(ReadOnlySpan<char> source)
    {
        int maxLength = Encoding.ASCII.GetMaxByteCount(source.Length);
        byte[] buffer = ArrayPool<byte>.Shared.Rent(maxLength);
        int writtenBytes = Encoding.ASCII.GetBytes(source, buffer);

        using ComPtr<ID3DBlob> d3DBlobBytecode = default;
        using ComPtr<ID3DBlob> d3DBlobErrors = default;

        fixed (byte* bufferPtr = buffer)
        {
            // D2D_FULL_SHADER
            ReadOnlySpan<byte> d2DFullShaderName = new[]
            {
                (byte)'D', (byte)'2', (byte)'D', (byte)'_',
                (byte)'F', (byte)'U', (byte)'L', (byte)'L', (byte)'_',
                (byte)'S', (byte)'H', (byte)'A', (byte)'D', (byte)'E', (byte)'R', (byte)'\0'
            };

            // (Empty)
            ReadOnlySpan<byte> d2DFullShaderValue = new[] { (byte)'\0' };

            // D2D_ENTRY
            ReadOnlySpan<byte> d2DEntryName = new[]
            {
                (byte)'D', (byte)'2', (byte)'D', (byte)'_',
                (byte)'E', (byte)'N', (byte)'T', (byte)'R', (byte)'Y', (byte)'\0'
            };

            // Execute
            ReadOnlySpan<byte> d2DEntryValue = new[]
            {
                (byte)'E', (byte)'x', (byte)'e', (byte)'c', (byte)'u', (byte)'t', (byte)'e', (byte)'\0'
            };

            D3D_SHADER_MACRO* macros = stackalloc D3D_SHADER_MACRO[]
            {
                new()
                {
                    Name = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(d2DFullShaderName)),
                    Definition = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(d2DFullShaderValue))
                },
                new()
                {
                    Name = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(d2DEntryName)),
                    Definition = (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(d2DEntryValue))
                },
                new()
            };

            // ps_4_0
            ReadOnlySpan<byte> shaderProfile = new[]
            {
                (byte)'p', (byte)'s', (byte)'_', (byte)'5', (byte)'_', (byte)'0', (byte)'\0'
            };

            DirectX.D3DCompile(
                pSrcData: bufferPtr,
                SrcDataSize: (nuint)writtenBytes,
                pSourceName: null,
                pDefines: macros,
                pInclude: null,
                pEntrypoint: (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(d2DEntryValue)),
                pTarget: (sbyte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(shaderProfile)),
                Flags1: D3DCOMPILE.D3DCOMPILE_OPTIMIZATION_LEVEL3 | D3DCOMPILE.D3DCOMPILE_WARNINGS_ARE_ERRORS,
                Flags2: 0,
                ppCode: d3DBlobBytecode.GetAddressOf(),
                ppErrorMsgs: d3DBlobErrors.GetAddressOf()).Assert();
        }

        ArrayPool<byte>.Shared.Return(buffer);

        if (d3DBlobErrors.Get() is not null)
        {
            ThrowHslsCompilationException(d3DBlobErrors.Get());
        }

        return d3DBlobBytecode.Move();
    }
#endif

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
    /// <param name="d3DOperationResult">The input (faulting) operation.</param>
    /// <returns>This method always throws and never actually returs.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static ComPtr<IDxcBlob> ThrowHslsCompilationException(ID3DBlob* d3DOperationResult)
    {
        string message = new((sbyte*)d3DOperationResult->GetBufferPointer());

        throw new HlslCompilationException(message);
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
