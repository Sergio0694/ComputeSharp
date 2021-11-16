// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_SAMPLER_DESC
    {
        public D3D12_FILTER Filter;

        public D3D12_TEXTURE_ADDRESS_MODE AddressU;

        public D3D12_TEXTURE_ADDRESS_MODE AddressV;

        public D3D12_TEXTURE_ADDRESS_MODE AddressW;

        public float MipLODBias;

        public uint MaxAnisotropy;

        public D3D12_COMPARISON_FUNC ComparisonFunc;

        [NativeTypeName("FLOAT [4]")]
        public fixed float BorderColor[4];

        public float MinLOD;

        public float MaxLOD;
    }
}
