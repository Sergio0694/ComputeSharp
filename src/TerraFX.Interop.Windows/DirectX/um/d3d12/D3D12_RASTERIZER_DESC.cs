// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public partial struct D3D12_RASTERIZER_DESC
    {
        public D3D12_FILL_MODE FillMode;

        public D3D12_CULL_MODE CullMode;

        public BOOL FrontCounterClockwise;

        public int DepthBias;

        public float DepthBiasClamp;

        public float SlopeScaledDepthBias;

        public BOOL DepthClipEnable;

        public BOOL MultisampleEnable;

        public BOOL AntialiasedLineEnable;

        public uint ForcedSampleCount;

        public D3D12_CONSERVATIVE_RASTERIZATION_MODE ConservativeRaster;
    }
}
