using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using ComputeSharp.SourceGeneration.Extensions;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D.Dxc;
using Windows.Win32.Graphics.Direct3D12;
using DirectX = Windows.Win32.PInvoke;

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

        fixed (Guid* pDxcCompiler = &DirectX.CLSID_DxcCompiler)
        fixed (Guid* pDxcCompiler3 = &IDxcCompiler3.IID_Guid)
        {
            DirectX.DxcCreateInstance(
                pDxcCompiler,
                pDxcCompiler3,
                (void**)dxcCompiler.GetAddressOf()).Assert();
        }

        fixed (Guid* pDxcLibrary = &DirectX.CLSID_DxcLibrary)
        fixed (Guid* pDxcUtils = &IDxcUtils.IID_Guid)
        {
            DirectX.DxcCreateInstance(
                pDxcLibrary,
                pDxcUtils,
                (void**)dxcUtils.GetAddressOf()).Assert();
        }

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
    /// <param name="compileOptions">The compile options to use.</param>
    /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    public ComPtr<IDxcBlob> Compile(ReadOnlySpan<char> source, CompileOptions compileOptions, CancellationToken token)
    {
        using ComPtr<IDxcResult> dxcResult = default;

        using (ComPtr<IDxcCompilerArgs> dxcCompilerArgs = default)
        {
            // Create the arguments to append others to
            fixed (char* pSourceName = "")
            fixed (char* pEntryPoint = "Execute")
            fixed (char* pTargetProfile = "cs_6_0")
            fixed (char* pRowMajor = "-Zpr")
            {
                // The row major argument is the only one always set automatically.
                // All the others are dynamically added based on the selected options.
                PCWSTR rowMajorArgument = pRowMajor;

                this.dxcUtils.Get()->BuildArguments(
                    pSourceName: pSourceName,
                    pEntryPoint: pEntryPoint,
                    pTargetProfile: pTargetProfile,
                    pArguments: &rowMajorArgument,
                    argCount: 1,
                    pDefines: null,
                    defineCount: 0,
                    ppArgs: dxcCompilerArgs.GetAddressOf()).Assert();
            }

            // Add any other optional arguments
            fixed (char* pAllResourcesBound = "-all-resources-bound")
            fixed (char* pDisableValidation = "-Vd")
            fixed (char* pDisableOptimization = "-Od")
            fixed (char* pAvoidFlowControl = "-Gfa")
            fixed (char* pPreferFlowControl = "-Gfp")
            fixed (char* pEnableStrictness = "-Ges")
            fixed (char* pIeeeStrictness = "-Gis")
            fixed (char* pEnableBackwardsCompatibility = "-Gec")
            fixed (char* pOptimizationLevel0 = "-O0")
            fixed (char* pOptimizationLevel1 = "-O1")
            fixed (char* pOptimizationLevel2 = "-O2")
            fixed (char* pOptimizationLevel3 = "-O3")
            fixed (char* pWarningsAreErrors = "-WX")
            fixed (char* pResourcesMayAlias = "-res-may-alias")
            fixed (char* pStripReflectionData = "-Qstrip_reflect")
            {
                // Helper to add a new argument
                static void AddArgument(IDxcCompilerArgs* args, char* pArgument)
                {
                    PCWSTR argument = pArgument;

                    args->AddArguments(&argument, 1).Assert();
                }

                if (compileOptions.HasFlag(CompileOptions.AllResourcesBound))
                {
                    AddArgument(dxcCompilerArgs.Get(), pAllResourcesBound);
                }

                if (compileOptions.HasFlag(CompileOptions.DisableValidation))
                {
                    AddArgument(dxcCompilerArgs.Get(), pDisableValidation);
                }

                if (compileOptions.HasFlag(CompileOptions.DisableOptimization))
                {
                    AddArgument(dxcCompilerArgs.Get(), pDisableOptimization);
                }

                if (compileOptions.HasFlag(CompileOptions.AvoidFlowControl))
                {
                    AddArgument(dxcCompilerArgs.Get(), pAvoidFlowControl);
                }

                if (compileOptions.HasFlag(CompileOptions.PreferFlowControl))
                {
                    AddArgument(dxcCompilerArgs.Get(), pPreferFlowControl);
                }

                if (compileOptions.HasFlag(CompileOptions.EnableStrictness))
                {
                    AddArgument(dxcCompilerArgs.Get(), pEnableStrictness);
                }

                if (compileOptions.HasFlag(CompileOptions.IeeeStrictness))
                {
                    AddArgument(dxcCompilerArgs.Get(), pIeeeStrictness);
                }

                if (compileOptions.HasFlag(CompileOptions.EnableBackwardsCompatibility))
                {
                    AddArgument(dxcCompilerArgs.Get(), pEnableBackwardsCompatibility);
                }

                if (compileOptions.HasFlag(CompileOptions.OptimizationLevel0))
                {
                    AddArgument(dxcCompilerArgs.Get(), pOptimizationLevel0);
                }

                if (compileOptions.HasFlag(CompileOptions.OptimizationLevel1))
                {
                    AddArgument(dxcCompilerArgs.Get(), pOptimizationLevel1);
                }

                if (compileOptions.HasFlag(CompileOptions.OptimizationLevel2))
                {
                    AddArgument(dxcCompilerArgs.Get(), pOptimizationLevel2);
                }

                if (compileOptions.HasFlag(CompileOptions.OptimizationLevel3))
                {
                    AddArgument(dxcCompilerArgs.Get(), pOptimizationLevel3);
                }

                if (compileOptions.HasFlag(CompileOptions.WarningsAreErrors))
                {
                    AddArgument(dxcCompilerArgs.Get(), pWarningsAreErrors);
                }

                if (compileOptions.HasFlag(CompileOptions.ResourcesMayAlias))
                {
                    AddArgument(dxcCompilerArgs.Get(), pResourcesMayAlias);
                }

                if (compileOptions.HasFlag(CompileOptions.StripReflectionData))
                {
                    AddArgument(dxcCompilerArgs.Get(), pStripReflectionData);
                }
            }

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

                // Only assert the HRESULT after returning the array from the pool. Since the method itself
                // can return a failure but doesn't actually throw, there's no need to throw the buffer away.
                hresult.Assert();
            }
        }

        token.ThrowIfCancellationRequested();

        HRESULT status;

        dxcResult.Get()->GetStatus(&status).Assert();

        // Throw an exception with the input messages, if the compilation fails
        if (status != 0)
        {
            ThrowHslsCompilationException(dxcResult.Get());
        }

        using ComPtr<IDxcBlob> dxcBlobBytecode = default;

        // The compilation was successful, so we can extract the shader bytecode
        dxcResult.Get()->GetResult(dxcBlobBytecode.GetAddressOf()).Assert();

        return dxcBlobBytecode.Move();
    }

    /// <inheritdoc cref="SourceGeneration.SyntaxProcessors.HlslBytecodeSyntaxProcessor.IsDoublePrecisionSupportRequired"/>
    public bool IsDoublePrecisionSupportRequired(IDxcBlob* dxcBlob)
    {
        using ComPtr<ID3D12ShaderReflection> d3D12ShaderReflection = default;

        Guid iidOfID3D12ShaderReflection = ID3D12ShaderReflection.IID_Guid;

        DxcBuffer dxcBuffer = default;
        dxcBuffer.Ptr = dxcBlob->GetBufferPointer();
        dxcBuffer.Size = dxcBlob->GetBufferSize();

        this.dxcUtils.Get()->CreateReflection(
            &dxcBuffer,
            &iidOfID3D12ShaderReflection,
            (void**)d3D12ShaderReflection.GetAddressOf()).Assert();

        const ulong doublePrecisionFlags = DirectX.D3D_SHADER_REQUIRES_DOUBLES | DirectX.D3D_SHADER_REQUIRES_11_1_DOUBLE_EXTENSIONS;

        return (d3D12ShaderReflection.Get()->GetRequiresFlags() & doublePrecisionFlags) != 0;
    }

    /// <inheritdoc cref="SourceGeneration.SyntaxProcessors.HlslBytecodeSyntaxProcessor.FixupErrorMessage"/>
    public static string FixupExceptionMessage(string message)
    {
        // Add square brackets around error headers
        message = Regex.Replace(message, @"^(error|warning):", static m => $"[{m.Groups[1].Value}]:", RegexOptions.Multiline);

        // Remove lines with notes
        message = Regex.Replace(message, @"^note:.+", string.Empty, RegexOptions.Multiline);

        // Remove syntax error indicators
        message = Regex.Replace(message, @"^ +\^", string.Empty, RegexOptions.Multiline);

        return message.NormalizeToSingleLine();
    }

    /// <summary>
    /// Throws an exception when a shader compilation fails.
    /// </summary>
    /// <param name="dxcResult">The input (faulting) operation.</param>
    /// <returns>This method always throws and never actually returs.</returns>
    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowHslsCompilationException(IDxcResult* dxcResult)
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