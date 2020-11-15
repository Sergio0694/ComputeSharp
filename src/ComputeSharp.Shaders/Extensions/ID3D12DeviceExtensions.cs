using System;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_PIPELINE_STATE_FLAGS;
using static TerraFX.Interop.D3D12_ROOT_PARAMETER_TYPE;
using static TerraFX.Interop.D3D12_SHADER_VISIBILITY;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Shaders.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with extensions for the <see cref="ID3D12Device"/> type.
    /// </summary>
    internal static unsafe class ID3D12DeviceExtensions
    {
        /// <summary>
        /// Creates a new <see cref="ID3D12RootSignature"/> for a given device.
        /// </summary>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to create the root signature.</param>
        /// <param name="d3D12DescriptorRanges1">The input descriptor ranges for the signature to create.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12RootSignature"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the root signature fails.</exception>
        public static ID3D12RootSignature* CreateRootSignature(
            this ref ID3D12Device d3d12device,
            ReadOnlySpan<D3D12_DESCRIPTOR_RANGE1> d3D12DescriptorRanges1)
        {
            ID3DBlob* d3D3Blob, d3D3BlobError;
            int result;

            fixed (D3D12_DESCRIPTOR_RANGE1* d3D12DescriptorRange1 = d3D12DescriptorRanges1)
            {
                D3D12_ROOT_PARAMETER1* d3D12RootParameters1 = stackalloc D3D12_ROOT_PARAMETER1[d3D12DescriptorRanges1.Length];

                // Pack each descriptor range into a root parameter with that single range as content
                for (int i = 0; i < d3D12DescriptorRanges1.Length; i++)
                {
                    D3D12_ROOT_PARAMETER1* d3D12RootParameter1 = d3D12RootParameters1 + i;
                    d3D12RootParameter1->ParameterType = D3D12_ROOT_PARAMETER_TYPE_DESCRIPTOR_TABLE;
                    d3D12RootParameter1->ShaderVisibility = D3D12_SHADER_VISIBILITY_ALL;

                    D3D12_ROOT_DESCRIPTOR_TABLE1.Init(out d3D12RootParameter1->DescriptorTable, 1, d3D12DescriptorRange1 + i);
                }

                // Root signature description wrapping the packed collection of root parameters
                D3D12_VERSIONED_ROOT_SIGNATURE_DESC.Init_1_1(
                    out D3D12_VERSIONED_ROOT_SIGNATURE_DESC d3d12VersionedRootSignatureDescription,
                    (uint)d3D12DescriptorRanges1.Length,
                    d3D12RootParameters1);

                // Serialize the root signature from the data just computed. When this is done
                // we just work with the resulting blobs, so the input data can be unpinned.
                result = FX.D3D12SerializeVersionedRootSignature(
                    &d3d12VersionedRootSignatureDescription,
                    &d3D3Blob,
                    &d3D3BlobError);

                if (FX.FAILED(result))
                {
                    if (d3D3BlobError != null) d3D3BlobError->Release();

                    Marshal.ThrowExceptionForHR(result);
                }
            }

            Guid d3D12RootSignatureGuid = FX.IID_ID3D12RootSignature;
            ID3D12RootSignature* d3D12RootSignature;

            result = d3d12device.CreateRootSignature(
                0,
                d3D3Blob->GetBufferPointer(),
                d3D3Blob->GetBufferSize(),
                &d3D12RootSignatureGuid,
                (void**)&d3D12RootSignature);

            if (FX.FAILED(result))
            {
                if (d3D3Blob != null) d3D3Blob->Release();

                Marshal.ThrowExceptionForHR(result);
            }

            return d3D12RootSignature;
        }

        /// <summary>
        /// Creates a new <see cref="ID3D12PipelineState"/> for a given device.
        /// </summary>
        /// <param name="d3d12device">The target <see cref="ID3D12Device"/> to use to create the pipeline state.</param>
        /// <param name="d3D12RootSignature">The input root signature to use to create the pipeline state.</param>
        /// <param name="d3d12ShaderBytecode">The shader bytecode to use for the pipeline state.</param>
        /// <returns>A pointer to the newly allocated <see cref="ID3D12PipelineState"/> instance.</returns>
        /// <exception cref="Exception">Thrown when the creation of the pipeline state fails.</exception>
        public static ID3D12PipelineState* CreateComputePipelineState(
            this ref ID3D12Device d3d12device,
            ID3D12RootSignature* d3D12RootSignature,
            D3D12_SHADER_BYTECODE d3d12ShaderBytecode)
        {
            D3D12_COMPUTE_PIPELINE_STATE_DESC d3d12ComputePipelineStateDescription;
            d3d12ComputePipelineStateDescription.pRootSignature = d3D12RootSignature;
            d3d12ComputePipelineStateDescription.CS = d3d12ShaderBytecode;
            d3d12ComputePipelineStateDescription.NodeMask = 0;
            d3d12ComputePipelineStateDescription.CachedPSO = default;
            d3d12ComputePipelineStateDescription.Flags = D3D12_PIPELINE_STATE_FLAG_NONE;
            Guid d3d12ComputePipelineStateGuid = FX.IID_ID3D12PipelineState;
            ID3D12PipelineState* d3D12PipelineState;

            int result = d3d12device.CreateComputePipelineState(
                &d3d12ComputePipelineStateDescription,
                &d3d12ComputePipelineStateGuid,
                (void**)&d3D12PipelineState);

            if (FX.FAILED(result)) Marshal.ThrowExceptionForHR(result);

            return d3D12PipelineState;
        }
    }
}
