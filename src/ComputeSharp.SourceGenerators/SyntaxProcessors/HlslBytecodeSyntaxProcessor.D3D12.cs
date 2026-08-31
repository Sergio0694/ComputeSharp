using System;
using System.Security.Cryptography;
using System.Threading;
using ComputeSharp.SourceGenerators.Dxc;
using ComputeSharp.SourceGenerators.Models;
using Windows.Win32;
using Windows.Win32.Graphics.Direct3D.Dxc;

namespace ComputeSharp.SourceGeneration.SyntaxProcessors;

/// <inheritdoc/>
partial class HlslBytecodeSyntaxProcessor
{
    /// <inheritdoc/>
    private static partial string GetRequiresDoublePrecisionSupportAttributeName()
    {
        return "ComputeSharp.RequiresDoublePrecisionSupportAttribute";
    }

    /// <inheritdoc/>
    private static partial void AppendDiskCacheKey(IncrementalHash hash, HlslBytecodeInfoKey key)
    {
        hash.AppendData("DXC"u8.ToArray());
        hash.AppendData(BitConverter.GetBytes((int)key.CompileOptions));
    }

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