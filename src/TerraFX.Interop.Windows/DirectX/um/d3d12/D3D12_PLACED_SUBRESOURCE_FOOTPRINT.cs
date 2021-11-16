// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public partial struct D3D12_PLACED_SUBRESOURCE_FOOTPRINT
    {
        [NativeTypeName("UINT64")]
        public ulong Offset;

        public D3D12_SUBRESOURCE_FOOTPRINT Footprint;
    }
}
