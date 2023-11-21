using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
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
    /// The <see cref="IDxcCompiler3"/> instance to use to create the bytecode for HLSL sources.
    /// </summary>
    private readonly ComPtr<IDxcCompiler3> dxcCompiler3;

    /// <summary>
    /// The <see cref="IDxcUtils"/> instance to use to work with <see cref="dxcCompiler3"/>.
    /// </summary>
    private readonly ComPtr<IDxcUtils> dxcUtils;

    /// <summary>
    /// Creates a new <see cref="DxcShaderCompiler"/> instance.
    /// </summary>
    private DxcShaderCompiler()
    {
        using ComPtr<IDxcCompiler3> dxcCompiler = default;
        using ComPtr<IDxcUtils> dxcUtils = default;

        PInvoke.DxcCreateInstance(
            PInvoke.CLSID_DxcCompiler,
            IDxcCompiler3.IID_Guid,
            out *(void**)dxcCompiler.GetAddressOf()).Assert();

        PInvoke.DxcCreateInstance(
            PInvoke.CLSID_DxcLibrary,
            IDxcUtils.IID_Guid,
            out *(void**)dxcUtils.GetAddressOf()).Assert();

        this.dxcCompiler3 = new ComPtr<IDxcCompiler3>(dxcCompiler.Get());
        this.dxcUtils = new ComPtr<IDxcUtils>(dxcUtils.Get());
    }

    /// <summary>
    /// Destroys the current <see cref="DxcShaderCompiler"/> instance.
    /// </summary>
    ~DxcShaderCompiler()
    {
        this.dxcCompiler3.Dispose();
        this.dxcUtils.Dispose();
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
        using ComPtr<IDxcResult> dxcResult = default;

        // We can't use stackalloc here, because the portable Span<T> type has a bug that causes
        // the constructor taking a pointer to throw if T is a type that contains a pointer field.
        // So we can just rent a buffer from the pool and use that instead, at least for now.
        PCWSTR[] arguments = ArrayPool<PCWSTR>.Shared.Rent(3);

        // Try to compile the new compute shader
        fixed (char* pSourceName = "")
        fixed (char* pEntryPoint = "Execute")
        fixed (char* pTargetProfile = "cs_6_0")
        fixed (char* optimization = "-O3")
        fixed (char* rowMajor = "-Zpr")
        fixed (char* warningsAsErrors = "-Werror")
        fixed (PCWSTR* pArguments = arguments)
        {
            pArguments[0] = optimization;
            pArguments[1] = rowMajor;
            pArguments[2] = warningsAsErrors;

            using ComPtr<IDxcCompilerArgs> dxcCompilerArgs = default;

            // Create the arguments to append others to
            this.dxcUtils.Get()->BuildArguments(
                pSourceName: pSourceName,
                pEntryPoint: pEntryPoint,
                pTargetProfile: pTargetProfile,
                pArguments: pArguments,
                argCount: 3,
                pDefines: null,
                defineCount: 0,
                ppArgs: dxcCompilerArgs.GetAddressOf()).Assert();

            fixed (char* pSource = source)
            {
                // Prepare the buffer with the HLSL source
                DxcBuffer dxcBuffer;
                dxcBuffer.Ptr = pSource;
                dxcBuffer.Size = (uint)(source.Length * sizeof(char));
                dxcBuffer.Encoding = (uint)DXC_CP.DXC_CP_UTF16;

                Guid dxcResultIid = IDxcResult.IID_Guid;

                // Try to actually compile the HLSL source
                HRESULT hresult = this.dxcCompiler3.Get()->Compile(
                    pSource: &dxcBuffer,
                    pArguments: dxcCompilerArgs.Get()->GetArguments(),
                    argCount: dxcCompilerArgs.Get()->GetCount(),
                    pIncludeHandler: null,
                    riid: &dxcResultIid,
                    ppResult: (void**)dxcResult.GetAddressOf());

                ArrayPool<PCWSTR>.Shared.Return(arguments);

                // Only assert the HRESULT after returning the array from the pool. Since the method itself
                // can return a failure but doesn't actually throw, there's no need to throw the buffer away.
                hresult.Assert();
            }
        }

        token.ThrowIfCancellationRequested();

        HRESULT status;

        dxcResult.Get()->GetStatus(&status).Assert();

        // The compilation was successful, so we can extract the shader bytecode
        if (status == 0)
        {
            using ComPtr<IDxcBlob> dxcBlobBytecode = default;

            dxcResult.Get()->GetResult(dxcBlobBytecode.GetAddressOf()).Assert();

            byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
            int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());

            return new ReadOnlySpan<byte>(buffer, length).ToArray();
        }

        return ThrowHslsCompilationException(dxcResult.Get());
    }

    /// <summary>
    /// Throws an exception when a shader compilation fails.
    /// </summary>
    /// <param name="dxcResult">The input (faulting) operation.</param>
    /// <returns>This method always throws and never actually returs.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static byte[] ThrowHslsCompilationException(IDxcResult* dxcResult)
    {
        using ComPtr<IDxcBlobEncoding> dxcBlobEncodingError = default;

        dxcResult->GetErrorBuffer(dxcBlobEncodingError.GetAddressOf()).Assert();

        string message = new((sbyte*)dxcBlobEncodingError.Get()->GetBufferPointer());

        // The error message will be in a format like this:
        // "hlsl.hlsl:11:20: error: redefinition of 'float1' as different kind of symbol
        //     static const float float1 = asfloat(0xFFC00000);
        //                        ^
        // note: previous definition is here"
        // These regex-s try to match the unnecessary headers and remove them, if present.
        // This doesn't need to be bulletproof, and these regex-s should match all cases anyway.
        message = Regex.Replace(message.Trim(), @"^hlsl\.hlsl:\d+:\d+: (\w+:)", static m => m.Groups[1].Value, RegexOptions.Multiline).Trim();

        // Add a trailing '.' if not present
        if (message is [.., not '.'])
        {
            message += '.';
        }

        throw new DxcCompilationException(message);
    }
}