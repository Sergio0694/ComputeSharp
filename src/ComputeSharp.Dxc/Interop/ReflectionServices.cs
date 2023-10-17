using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Descriptors;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using DirectX = TerraFX.Interop.DirectX.DirectX2;
#endif

namespace ComputeSharp.Interop;

/// <summary>
/// Provides methods to extract reflection info on compute shaders generated using this library.
/// </summary>
public static partial class ReflectionServices
{
    /// <summary>
    /// Gets the shader info associated with a given compute shader.
    /// </summary>
    /// <typeparam name="T">The type of compute shader to retrieve info for.</typeparam>
    /// <returns>The resulting <see cref="ShaderInfo"/> instance.</returns>
    public static ShaderInfo GetShaderInfo<T>()
        where T : struct, IComputeShader, IComputeShaderDescriptor<T>
    {
        return GetNonGenericShaderInfo<T>();
    }

    /// <summary>
    /// Gets the shader info associated with a given pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of pixel shader to retrieve info for.</typeparam>
    /// <typeparam name="TPixel">The type of pixels being processed by the shader.</typeparam>
    /// <returns>The resulting <see cref="ShaderInfo"/> instance.</returns>
    public static ShaderInfo GetShaderInfo<T, TPixel>()
        where T : struct, IComputeShader<TPixel>, IComputeShaderDescriptor<T>
        where TPixel : unmanaged
    {
        return GetNonGenericShaderInfo<T>();
    }

    /// <summary>
    /// Gets the shader info associated with a given shader of any type.
    /// </summary>
    /// <typeparam name="T">The type of shader to retrieve info for.</typeparam>
    /// <returns>The resulting <see cref="ShaderInfo"/> instance.</returns>
    private static unsafe ShaderInfo GetNonGenericShaderInfo<T>()
        where T : struct, IComputeShaderDescriptor<T>
    {
        Unsafe.SkipInit(out T shader);

        ReadOnlyMemory<byte> hlslBytecode = shader.HlslBytecode;

        using ComPtr<IDxcUtils> dxcUtils = default;

        // Load the DXC compiler (the package will bundle the necessary .dll-s)
        DirectX.DxcCreateInstance(
            (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_DxcLibrary)),
            Windows.__uuidof<IDxcUtils>(),
            (void**)dxcUtils.GetAddressOf()).Assert();

        using ComPtr<ID3D12ShaderReflection> d3D12ShaderReflection = default;

        // Reflect over the shader (this will fail if reflection metadata has been stripped).
        // Since that's just opt-in from users, that is the expected behavior, so not an issue.
        fixed (byte* hlslBytecodePtr = hlslBytecode.Span)
        {
            DxcBuffer dxcBuffer = default;
            dxcBuffer.Ptr = hlslBytecodePtr;
            dxcBuffer.Size = (uint)hlslBytecode.Length;

            dxcUtils.Get()->CreateReflection(
                &dxcBuffer,
                Windows.__uuidof<ID3D12ShaderReflection>(),
                (void**)d3D12ShaderReflection.GetAddressOf()).Assert();
        }

        D3D12_SHADER_DESC d3D12ShaderDescription;

        d3D12ShaderReflection.Get()->GetDesc(&d3D12ShaderDescription).Assert();

        return new(
            CompilerVersion: new string(d3D12ShaderDescription.Creator),
            HlslSource: shader.HlslSource,
            HlslBytecode: hlslBytecode,
            ConstantBufferCount: d3D12ShaderDescription.ConstantBuffers,
            BoundResourceCount: d3D12ShaderDescription.BoundResources,
            InstructionCount: d3D12ShaderDescription.InstructionCount,
            TemporaryRegisterCount: d3D12ShaderDescription.TempRegisterCount,
            TemporaryArrayCount: d3D12ShaderDescription.TempArrayCount,
            ConstantDefineCount: d3D12ShaderDescription.DefCount,
            DeclarationCount: d3D12ShaderDescription.DclCount,
            TextureNormalInstructions: d3D12ShaderDescription.TextureNormalInstructions,
            TextureLoadInstructionCount: d3D12ShaderDescription.TextureLoadInstructions,
            TextureStoreInstructionCount: d3D12ShaderDescription.cTextureStoreInstructions,
            FloatInstructionCount: d3D12ShaderDescription.FloatInstructionCount,
            IntInstructionCount: d3D12ShaderDescription.IntInstructionCount,
            UIntInstructionCount: d3D12ShaderDescription.UintInstructionCount,
            StaticFlowControlInstructionCount: d3D12ShaderDescription.StaticFlowControlCount,
            DynamicFlowControlInstructionCount: d3D12ShaderDescription.DynamicFlowControlCount,
            EmitInstructionCount: d3D12ShaderDescription.EmitInstructionCount,
            BarrierInstructionCount: d3D12ShaderDescription.cBarrierInstructions,
            InterlockedInstructionCount: d3D12ShaderDescription.cInterlockedInstructions,
            BitwiseInstructionCount: d3D12ShaderReflection.Get()->GetBitwiseInstructionCount(),
            MovcInstructionCount: d3D12ShaderReflection.Get()->GetMovcInstructionCount(),
            MovInstructionCount: d3D12ShaderReflection.Get()->GetMovInstructionCount(),
            InterfaceSlotCount: d3D12ShaderReflection.Get()->GetNumInterfaceSlots(),
            RequiresDoublePrecisionSupport: (d3D12ShaderReflection.Get()->GetRequiresFlags() & (D3D.D3D_SHADER_REQUIRES_DOUBLES | D3D.D3D_SHADER_REQUIRES_11_1_DOUBLE_EXTENSIONS)) != 0);
    }
}