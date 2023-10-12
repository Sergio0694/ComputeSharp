using System;
using ComputeSharp.Core.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_COMPARISON_FUNC;
using static TerraFX.Interop.DirectX.D3D12_FILTER;
using static TerraFX.Interop.DirectX.D3D12_PIPELINE_STATE_FLAGS;
using static TerraFX.Interop.DirectX.D3D12_TEXTURE_ADDRESS_MODE;

namespace ComputeSharp.Shaders.Extensions;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="ID3D12Device"/> type.
/// </summary>
internal static unsafe class ID3D12DeviceExtensions
{
    /// <summary>
    /// Creates a new <see cref="ID3D12RootSignature"/> for a given device.
    /// </summary>
    /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to create the root signature.</param>
    /// <param name="d3D12Root32BitConstantsCount">The number of 32 bit root constants to load.</param>
    /// <param name="d3D12DescriptorRanges1">The input descriptor ranges for the signature to create.</param>
    /// <param name="isStaticSamplerUsed">Indicates whether or not a static sampler is used.</param>
    /// <returns>A pointer to the newly allocated <see cref="ID3D12RootSignature"/> instance.</returns>
    /// <exception cref="Exception">Thrown when the creation of the root signature fails.</exception>
    public static ComPtr<ID3D12RootSignature> CreateRootSignature(
        this ref ID3D12Device d3D12Device,
        int d3D12Root32BitConstantsCount,
        ReadOnlySpan<D3D12_DESCRIPTOR_RANGE1> d3D12DescriptorRanges1,
        bool isStaticSamplerUsed)
    {
        using ComPtr<ID3DBlob> d3D3Blob = default;
        using ComPtr<ID3DBlob> d3D3BlobError = default;
        using ComPtr<ID3D12RootSignature> d3D12RootSignature = default;

        fixed (D3D12_DESCRIPTOR_RANGE1* d3D12DescriptorRange1 = d3D12DescriptorRanges1)
        {
            D3D12_ROOT_PARAMETER1* d3D12RootParameters1 = stackalloc D3D12_ROOT_PARAMETER1[d3D12DescriptorRanges1.Length + 1];

            // The initial constant buffer is initialized with 32 bit root constants.
            // This avoids having to manage an extra buffer for each shader dispatch.
            D3D12_ROOT_PARAMETER1.InitAsConstants(out *d3D12RootParameters1, (uint)d3D12Root32BitConstantsCount, 0);

            // Pack each descriptor range into a root parameter with that single range as content
            for (int i = 0; i < d3D12DescriptorRanges1.Length; i++)
            {
                D3D12_ROOT_PARAMETER1.InitAsDescriptorTable(out d3D12RootParameters1[i + 1], 1, d3D12DescriptorRange1 + i);
            }

            D3D12_STATIC_SAMPLER_DESC d3D12SamplerDescription;

            // Create the linear static sampler if needed
            if (isStaticSamplerUsed)
            {
                D3D12_STATIC_SAMPLER_DESC.Init(
                    out d3D12SamplerDescription,
                    shaderRegister: 0,
                    filter: D3D12_FILTER_MIN_MAG_MIP_LINEAR,
                    addressU: D3D12_TEXTURE_ADDRESS_MODE_MIRROR,
                    addressV: D3D12_TEXTURE_ADDRESS_MODE_MIRROR,
                    addressW: D3D12_TEXTURE_ADDRESS_MODE_MIRROR,
                    comparisonFunc: D3D12_COMPARISON_FUNC_NEVER);
            }

            // Root signature description wrapping the packed collection of root parameters
            D3D12_VERSIONED_ROOT_SIGNATURE_DESC.Init_1_1(
                out D3D12_VERSIONED_ROOT_SIGNATURE_DESC d3D12VersionedRootSignatureDescription,
                numParameters: (uint)d3D12DescriptorRanges1.Length + 1,
                _pParameters: d3D12RootParameters1,
                numStaticSamplers: isStaticSamplerUsed ? 1u : 0u,
                _pStaticSamplers: isStaticSamplerUsed ? &d3D12SamplerDescription : null);

            // Serialize the root signature from the data just computed. When this is done
            // we just work with the resulting blobs, so the input data can be unpinned.
            DirectX.D3D12SerializeVersionedRootSignature(
                &d3D12VersionedRootSignatureDescription,
                d3D3Blob.GetAddressOf(),
                d3D3BlobError.GetAddressOf()).Assert();
        }

        d3D12Device.CreateRootSignature(
            0,
            d3D3Blob.Get()->GetBufferPointer(),
            d3D3Blob.Get()->GetBufferSize(),
            Windows.__uuidof<ID3D12RootSignature>(),
            (void**)d3D12RootSignature.GetAddressOf()).Assert();

        return d3D12RootSignature.Move();
    }

    /// <summary>
    /// Creates a new <see cref="ID3D12PipelineState"/> for a given device.
    /// </summary>
    /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to create the pipeline state.</param>
    /// <param name="d3D12RootSignature">The input root signature to use to create the pipeline state.</param>
    /// <param name="d3D12ShaderBytecode">The shader bytecode to use for the pipeline state.</param>
    /// <returns>A pointer to the newly allocated <see cref="ID3D12PipelineState"/> instance.</returns>
    /// <exception cref="Exception">Thrown when the creation of the pipeline state fails.</exception>
    public static ComPtr<ID3D12PipelineState> CreateComputePipelineState(
        this ref ID3D12Device d3D12Device,
        ID3D12RootSignature* d3D12RootSignature,
        D3D12_SHADER_BYTECODE d3D12ShaderBytecode)
    {
        using ComPtr<ID3D12PipelineState> d3D12PipelineState = default;

        D3D12_COMPUTE_PIPELINE_STATE_DESC d3D12ComputePipelineStateDescription;
        d3D12ComputePipelineStateDescription.pRootSignature = d3D12RootSignature;
        d3D12ComputePipelineStateDescription.CS = d3D12ShaderBytecode;
        d3D12ComputePipelineStateDescription.NodeMask = 0;
        d3D12ComputePipelineStateDescription.CachedPSO = default;
        d3D12ComputePipelineStateDescription.Flags = D3D12_PIPELINE_STATE_FLAG_NONE;

        d3D12Device.CreateComputePipelineState(
            &d3D12ComputePipelineStateDescription,
            Windows.__uuidof<ID3D12PipelineState>(),
            (void**)d3D12PipelineState.GetAddressOf()).Assert();

        return d3D12PipelineState.Move();
    }
}