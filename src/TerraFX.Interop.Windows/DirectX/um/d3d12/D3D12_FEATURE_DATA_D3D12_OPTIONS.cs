// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public partial struct D3D12_FEATURE_DATA_D3D12_OPTIONS
    {
        public BOOL DoublePrecisionFloatShaderOps;

        public BOOL OutputMergerLogicOp;

        public D3D12_SHADER_MIN_PRECISION_SUPPORT MinPrecisionSupport;

        public D3D12_TILED_RESOURCES_TIER TiledResourcesTier;

        public D3D12_RESOURCE_BINDING_TIER ResourceBindingTier;

        public BOOL PSSpecifiedStencilRefSupported;

        public BOOL TypedUAVLoadAdditionalFormats;

        public BOOL ROVsSupported;

        public D3D12_CONSERVATIVE_RASTERIZATION_TIER ConservativeRasterizationTier;

        public uint MaxGPUVirtualAddressBitsPerResource;

        public BOOL StandardSwizzle64KBSupported;

        public D3D12_CROSS_NODE_SHARING_TIER CrossNodeSharingTier;

        public BOOL CrossAdapterRowMajorTextureSupported;

        public BOOL VPAndRTArrayIndexFromAnyShaderFeedingRasterizerSupportedWithoutGSEmulation;

        public D3D12_RESOURCE_HEAP_TIER ResourceHeapTier;
    }
}
