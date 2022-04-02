using System;
using System.Runtime.CompilerServices;
using ComputeSharp.__Internals;
using ComputeSharp.Core.Extensions;
using ComputeSharp.D2D1Interop;
using ComputeSharp.D2D1Interop.Shaders.Translation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.Interop;

/// <summary>
/// Provides methods to extract reflection info on D2D1 shaders generated using this library.
/// </summary>
public static class D2D1ReflectionServices
{
    /// <summary>
    /// Gets the shader info associated with a given D2D1 shader.
    /// <para>
    /// This overload can be used for simplicity when the D2D1 shader being inspected does not rely on captured
    /// objects to be processed correctly. This is the case when it does not contain any <see cref="Delegate"/>-s.
    /// </para>
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to retrieve info for.</typeparam>
    /// <returns>The resulting <see cref="D2D1ShaderInfo"/> instance.</returns>
    public static D2D1ShaderInfo GetShaderInfo<T>()
        where T : struct, ID2D1PixelShader
    {
        return GetShaderInfo(default(T));
    }

    /// <summary>
    /// Gets the shader info associated with a given D2D1 shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to retrieve info for.</typeparam>
    /// <param name="shader">The input D2D1 shader to retrieve info for.</param>
    /// <returns>The resulting <see cref="D2D1ShaderInfo"/> instance.</returns>
    public static unsafe D2D1ShaderInfo GetShaderInfo<T>(in T shader)
        where T : struct, ID2D1PixelShader
    {
        Unsafe.AsRef(in shader).BuildHlslString(out ArrayPoolStringBuilder shaderSource);

        using ComPtr<ID3DBlob> d3DBlobBytecode = D2D1ShaderCompiler.Compile(shaderSource.WrittenSpan, enableLinkingSupport: false);

        shaderSource.Dispose();

        using ComPtr<ID3D11ShaderReflection> d3D11ShaderReflection = default;

        DirectX.D3DReflect(
            pSrcData: d3DBlobBytecode.Get()->GetBufferPointer(),
            SrcDataSize: d3DBlobBytecode.Get()->GetBufferSize(),
            pInterface: Windows.__uuidof<ID3D11ShaderReflection>(),
            ppReflector: d3D11ShaderReflection.GetVoidAddressOf()).Assert();

        D3D11_SHADER_DESC d3D11ShaderDescription;

        d3D11ShaderReflection.Get()->GetDesc(&d3D11ShaderDescription).Assert();

        return new(
            CompilerVersion: new string(d3D11ShaderDescription.Creator),
            HlslSource: shaderSource.WrittenSpan.ToString(),
            ConstantBufferCount: d3D11ShaderDescription.ConstantBuffers,
            BoundResourceCount: d3D11ShaderDescription.BoundResources,
            InstructionCount: d3D11ShaderDescription.InstructionCount,
            TemporaryRegisterCount: d3D11ShaderDescription.TempRegisterCount,
            TemporaryArrayCount: d3D11ShaderDescription.TempArrayCount,
            ConstantDefineCount: d3D11ShaderDescription.DefCount,
            DeclarationCount: d3D11ShaderDescription.DclCount,
            TextureNormalInstructions: d3D11ShaderDescription.TextureNormalInstructions,
            TextureLoadInstructionCount: d3D11ShaderDescription.TextureLoadInstructions,
            TextureStoreInstructionCount: d3D11ShaderDescription.cTextureStoreInstructions,
            FloatInstructionCount: d3D11ShaderDescription.FloatInstructionCount,
            IntInstructionCount: d3D11ShaderDescription.IntInstructionCount,
            UIntInstructionCount: d3D11ShaderDescription.UintInstructionCount,
            StaticFlowControlInstructionCount: d3D11ShaderDescription.StaticFlowControlCount,
            DynamicFlowControlInstructionCount: d3D11ShaderDescription.DynamicFlowControlCount,
            EmitInstructionCount: d3D11ShaderDescription.EmitInstructionCount,
            BarrierInstructionCount: d3D11ShaderDescription.cBarrierInstructions,
            InterlockedInstructionCount: d3D11ShaderDescription.cInterlockedInstructions,
            BitwiseInstructionCount: d3D11ShaderReflection.Get()->GetBitwiseInstructionCount(),
            MovcInstructionCount: d3D11ShaderReflection.Get()->GetMovcInstructionCount(),
            MovInstructionCount: d3D11ShaderReflection.Get()->GetMovInstructionCount(),
            InterfaceSlotCount: d3D11ShaderReflection.Get()->GetNumInterfaceSlots(),
            RequiresDoublePrecisionSupport: (d3D11ShaderReflection.Get()->GetRequiresFlags() & (D3D.D3D_SHADER_REQUIRES_DOUBLES | D3D.D3D_SHADER_REQUIRES_11_1_DOUBLE_EXTENSIONS)) != 0);
    }
}
