// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_ROOT_DESCRIPTOR_TABLE
    {
        public uint NumDescriptorRanges;

        [NativeTypeName("const D3D12_DESCRIPTOR_RANGE *")]
        public D3D12_DESCRIPTOR_RANGE* pDescriptorRanges;
    }
}
