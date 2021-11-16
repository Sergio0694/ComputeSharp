// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_DISCARD_REGION
    {
        public uint NumRects;

        [NativeTypeName("const D3D12_RECT *")]
        public RECT* pRects;

        public uint FirstSubresource;

        public uint NumSubresources;
    }
}
