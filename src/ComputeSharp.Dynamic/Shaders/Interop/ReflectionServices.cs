using System;
using System.Runtime.CompilerServices;
using ComputeSharp.__Internals;
using ComputeSharp.Core.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using DirectX = TerraFX.Interop.DirectX.DirectX2;
#endif

#pragma warning disable CS0618

namespace ComputeSharp.Interop;

/// <summary>
/// Provides methods to extract reflection info on compute shaders generated using this library.
/// </summary>
public static class ReflectionServices
{
    /// <summary>
    /// Gets the shader info associated with a given compute shader.
    /// <para>
    /// This overload can be used for simplicity when the compute shader being inspected does not rely on captured
    /// objects to be processed correctly. This is the case when it does not contain any <see cref="Delegate"/>-s.
    /// </para>
    /// </summary>
    /// <typeparam name="T">The type of compute shader to retrieve info for.</typeparam>
    /// <returns>The resulting <see cref="ShaderInfo"/> instance.</returns>
    /// <remarks>
    /// The thread group sizes will always be set to (1, 1, 1) in the returned shader. This is done to
    /// avoid having to compiler multiple shaders just to get reflection info for them. When using any of
    /// the APIs to dispatch a shader, the thread sizes would actually be set to a proper value insead.
    /// </remarks>
    public static ShaderInfo GetShaderInfo<T>()
        where T : struct, IComputeShader
    {
        return GetNonGenericShaderInfo(default(T));
    }

    /// <summary>
    /// Gets the shader info associated with a given compute shader.
    /// </summary>
    /// <typeparam name="T">The type of compute shader to retrieve info for.</typeparam>
    /// <param name="shader">The input compute shader to retrieve info for.</param>
    /// <returns>The resulting <see cref="ShaderInfo"/> instance.</returns>
    /// <remarks>
    /// The thread group sizes will always be set to (1, 1, 1) in the returned shader. This is done to
    /// avoid having to compiler multiple shaders just to get reflection info for them. When using any of
    /// the APIs to dispatch a shader, the thread sizes would actually be set to a proper value insead.
    /// </remarks>
    public static unsafe ShaderInfo GetShaderInfo<T>(in T shader)
        where T : struct, IComputeShader
    {
        return GetNonGenericShaderInfo(in shader);
    }

    /// <summary>
    /// Gets the shader info associated with a given pixel shader.
    /// <para>
    /// This overload can be used for simplicity when the pixel shader being inspected does not rely on captured
    /// objects to be processed correctly. This is the case when it does not contain any <see cref="Delegate"/>-s.
    /// </para>
    /// </summary>
    /// <typeparam name="T">The type of pixel shader to retrieve info for.</typeparam>
    /// <typeparam name="TPixel">The type of pixels being processed by the shader.</typeparam>
    /// <returns>The resulting <see cref="ShaderInfo"/> instance.</returns>
    /// <remarks>
    /// The thread group sizes will always be set to (1, 1, 1) in the returned shader. This is done to
    /// avoid having to compiler multiple shaders just to get reflection info for them. When using any of
    /// the APIs to dispatch a shader, the thread sizes would actually be set to a proper value insead.
    /// </remarks>
    public static ShaderInfo GetShaderInfo<T, TPixel>()
        where T : struct, IPixelShader<TPixel>
        where TPixel : unmanaged
    {
        return GetNonGenericShaderInfo(default(T));
    }

    /// <summary>
    /// Gets the shader info associated with a given pixel shader.
    /// </summary>
    /// <typeparam name="T">The type of pixel shader to retrieve info for.</typeparam>
    /// <typeparam name="TPixel">The type of pixels being processed by the shader.</typeparam>
    /// <param name="shader">The input pixel shader to retrieve info for.</param>
    /// <returns>The resulting <see cref="ShaderInfo"/> instance.</returns>
    /// <remarks>
    /// The thread group sizes will always be set to (1, 1, 1) in the returned shader. This is done to
    /// avoid having to compiler multiple shaders just to get reflection info for them. When using any of
    /// the APIs to dispatch a shader, the thread sizes would actually be set to a proper value insead.
    /// </remarks>
    public static unsafe ShaderInfo GetShaderInfo<T, TPixel>(in T shader)
        where T : struct, IPixelShader<TPixel>
        where TPixel : unmanaged
    {
        return GetNonGenericShaderInfo(in shader);
    }

    /// <summary>
    /// Gets the shader info associated with a given shader of any type.
    /// </summary>
    /// <typeparam name="T">The type of shader to retrieve info for.</typeparam>
    /// <param name="shader">The input shader to retrieve info for.</param>
    /// <returns>The resulting <see cref="ShaderInfo"/> instance.</returns>
    private static unsafe ShaderInfo GetNonGenericShaderInfo<T>(in T shader)
        where T : struct, IShader
    {
        Unsafe.AsRef(in shader).BuildHlslSource(out ArrayPoolStringBuilder shaderSource, 1, 1, 1);

        using ComPtr<IDxcBlob> dxcBlobBytecode = Shaders.Translation.ShaderCompiler.Instance.Compile(shaderSource.WrittenSpan);

        shaderSource.Dispose();

        using ComPtr<IDxcUtils> dxcUtils = default;

        DirectX.DxcCreateInstance(
            (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_DxcLibrary)),
            Windows.__uuidof<IDxcUtils>(),
            dxcUtils.GetVoidAddressOf()).Assert();

        using ComPtr<ID3D12ShaderReflection> d3D12ShaderReflection = default;

        DxcBuffer dxcBuffer = default;
        dxcBuffer.Ptr = dxcBlobBytecode.Get()->GetBufferPointer();
        dxcBuffer.Size = dxcBlobBytecode.Get()->GetBufferSize();

        dxcUtils.Get()->CreateReflection(
            &dxcBuffer,
            Windows.__uuidof<ID3D12ShaderReflection>(),
            d3D12ShaderReflection.GetVoidAddressOf()).Assert();

        D3D12_SHADER_DESC d3D12ShaderDescription;

        d3D12ShaderReflection.Get()->GetDesc(&d3D12ShaderDescription).Assert();

        return new(
            CompilerVersion: new string(d3D12ShaderDescription.Creator),
            HlslSource: shaderSource.WrittenSpan.ToString(),
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