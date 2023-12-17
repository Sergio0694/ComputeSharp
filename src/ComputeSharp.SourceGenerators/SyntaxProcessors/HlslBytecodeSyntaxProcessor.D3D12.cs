using System;
using System.Threading;
using ComputeSharp.SourceGenerators.Dxc;
using ComputeSharp.SourceGenerators.Models;
using Windows.Win32;
using Windows.Win32.Graphics.Direct3D.Dxc;

namespace ComputeSharp.SourceGeneration.SyntaxProcessors;

/// <inheritdoc/>
partial class HlslBytecodeSyntaxProcessor
{
    /// <summary>
    /// The name of the attribute to enable double precision support.
    /// </summary>
    private const string RequiresDoublePrecisionSupportAttributeName = "ComputeSharp.RequiresDoublePrecisionSupportAttribute";

    /// <inheritdoc/>
    private static partial ComPtr<IDxcBlob> Compile(HlslBytecodeInfoKey key, CancellationToken token)
    {
        // Try to load dxcompiler.dll and dxil.dll
        DxcLibraryLoader.LoadNativeDxcLibraries();

        token.ThrowIfCancellationRequested();

        // Compile the shader bytecode using DXC
        return DxcShaderCompiler.Instance.Compile(
            key.HlslSource.AsSpan(),
            key.CompileOptions,
            token);
    }

    /// <inheritdoc/>
    private static unsafe partial bool IsDoublePrecisionSupportRequired(IDxcBlob* d3DBlob)
    {
        return DxcShaderCompiler.Instance.IsDoublePrecisionSupportRequired(d3DBlob);
    }

    /// <inheritdoc/>
    private static partial string FixupErrorMessage(string message)
    {
        return DxcShaderCompiler.FixupExceptionMessage(message);
    }
}