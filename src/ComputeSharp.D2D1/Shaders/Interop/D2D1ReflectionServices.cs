using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Shaders.Loaders;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// Provides methods to extract reflection info on D2D1 shaders generated using this library.
/// </summary>
public static class D2D1ReflectionServices
{
    /// <summary>
    /// Gets the shader info associated with a given D2D1 shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to retrieve info for.</typeparam>
    /// <returns>The resulting <see cref="D2D1ShaderInfo"/> instance.</returns>
    /// <remarks>
    /// If the target shader type <typeparamref name="T"/> is precompiled (ie. it's annotated with <see cref="D2DShaderProfileAttribute"/>), that bytecode will
    /// be reused to reflect on the shader, so the same shader profile and compile options will be reused, which can make the returned metadata more accurate.
    /// <para>
    /// If the shader is not precompiled, it will be compiled on the fly using the default settings for both the shader profile and the compile options. That is,
    /// <see cref="D2D1ShaderProfile.PixelShader50"/> and <see cref="D2D1CompileOptions.Default"/> (with linking optionally enabled if supported by the shader type).
    /// </para>
    /// </remarks>
    public static unsafe D2D1ShaderInfo GetShaderInfo<T>()
        where T : struct, ID2D1PixelShader
    {
        Unsafe.SkipInit(out T shader);

        shader.BuildHlslSource(out string hlslSource);

        D2D1ShaderBytecodeLoader bytecodeLoader = default;

        D2D1ShaderProfile? shaderProfile = null;
        D2D1CompileOptions? compileOptions = null;

        shader.LoadBytecode(ref bytecodeLoader, ref shaderProfile, ref compileOptions);

        using ComPtr<ID3DBlob> dynamicBytecode = bytecodeLoader.GetResultingShaderBytecode(out ReadOnlySpan<byte> precompiledBytecode);

        byte* bytecodePtr;
        int bytecodeSize;

        if (!precompiledBytecode.IsEmpty)
        {
            bytecodePtr = (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(precompiledBytecode));
            bytecodeSize = precompiledBytecode.Length;
        }
        else
        {
            bytecodePtr = (byte*)dynamicBytecode.Get()->GetBufferPointer();
            bytecodeSize = (int)dynamicBytecode.Get()->GetBufferSize();
        }

        using ComPtr<ID3D11ShaderReflection> d3D11ShaderReflection = default;

        DirectX.D3DReflect(
            pSrcData: bytecodePtr,
            SrcDataSize: (nuint)bytecodeSize,
            pInterface: Windows.__uuidof<ID3D11ShaderReflection>(),
            ppReflector: d3D11ShaderReflection.GetVoidAddressOf()).Assert();

        D3D11_SHADER_DESC d3D11ShaderDescription;

        d3D11ShaderReflection.Get()->GetDesc(&d3D11ShaderDescription).Assert();

        D3D_FEATURE_LEVEL d3DFeatureLevel = default;

        d3D11ShaderReflection.Get()->GetMinFeatureLevel(&d3DFeatureLevel).Assert();

        return new(
            CompilerVersion: new string(d3D11ShaderDescription.Creator),
            HlslSource: hlslSource,
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
            RequiresDoublePrecisionSupport: (d3D11ShaderReflection.Get()->GetRequiresFlags() & (D3D.D3D_SHADER_REQUIRES_DOUBLES | D3D.D3D_SHADER_REQUIRES_11_1_DOUBLE_EXTENSIONS)) != 0,
            MinimumFeatureLevel: (D3DFeatureLevel)d3DFeatureLevel);
    }
}