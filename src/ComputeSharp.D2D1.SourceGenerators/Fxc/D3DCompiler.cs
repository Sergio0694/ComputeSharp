using System;
using Windows.Win32;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D11;
using DirectX = Windows.Win32.PInvoke;

namespace ComputeSharp.D2D1.Shaders.Translation;

/// <inheritdoc/>
partial class D3DCompiler
{
    /// <summary>
    /// Checks whether double precision support is required.
    /// </summary>
    /// <param name="d3DBlob">The input HLSL bytecode to inspect.</param>
    /// <returns>Whether double precision support is required for <paramref name="d3DBlob"/>.</returns>
    public static unsafe bool IsDoublePrecisionSupportRequired(ID3DBlob* d3DBlob)
    {
        using ComPtr<ID3D11ShaderReflection> d3D11ShaderReflection = default;

        Guid iidOfID3D11ShaderReflection = ID3D11ShaderReflection.IID_Guid;

        DirectX.D3DReflect(
            pSrcData: d3DBlob->GetBufferPointer(),
            SrcDataSize: d3DBlob->GetBufferSize(),
            pInterface: &iidOfID3D11ShaderReflection,
            ppReflector: (void**)d3D11ShaderReflection.GetAddressOf()).Assert();

        const ulong doublePrecisionFlags = DirectX.D3D_SHADER_REQUIRES_DOUBLES | DirectX.D3D_SHADER_REQUIRES_11_1_DOUBLE_EXTENSIONS;

        return (d3D11ShaderReflection.Get()->GetRequiresFlags() & doublePrecisionFlags) != 0;
    }
}