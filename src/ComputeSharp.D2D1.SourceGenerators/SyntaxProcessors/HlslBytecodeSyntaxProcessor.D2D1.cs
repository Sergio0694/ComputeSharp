using System;
using System.Threading;
using ComputeSharp.D2D1.Shaders.Translation;
using ComputeSharp.D2D1.SourceGenerators.Models;
using Windows.Win32;
using Windows.Win32.Graphics.Direct3D;

namespace ComputeSharp.SourceGeneration.SyntaxProcessors;

/// <inheritdoc/>
partial class HlslBytecodeSyntaxProcessor
{
    /// <inheritdoc/>
    private static partial string GetRequiresDoublePrecisionSupportAttributeName()
    {
        return "ComputeSharp.D2D1.D2DRequiresDoublePrecisionSupportAttribute";
    }

    /// <inheritdoc/>
    private static partial ComPtr<ID3DBlob> Compile(HlslBytecodeInfoKey key, CancellationToken token)
    {
        return D3DCompiler.Compile(
            key.HlslSource.AsSpan(),
            key.ShaderProfile,
            key.CompileOptions,
            token);
    }

    /// <inheritdoc/>
    private static unsafe partial bool IsDoublePrecisionSupportRequired(ID3DBlob* d3DBlob)
    {
        return D3DCompiler.IsDoublePrecisionSupportRequired(d3DBlob);
    }

    /// <inheritdoc/>
    private static partial string FixupErrorMessage(string message)
    {
        return D3DCompiler.PrettifyFxcErrorMessage(message);
    }
}