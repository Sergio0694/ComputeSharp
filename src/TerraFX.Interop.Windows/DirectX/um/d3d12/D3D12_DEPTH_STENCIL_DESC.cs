// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public partial struct D3D12_DEPTH_STENCIL_DESC
    {
        public BOOL DepthEnable;

        public D3D12_DEPTH_WRITE_MASK DepthWriteMask;

        public D3D12_COMPARISON_FUNC DepthFunc;

        public BOOL StencilEnable;

        [NativeTypeName("UINT8")]
        public byte StencilReadMask;

        [NativeTypeName("UINT8")]
        public byte StencilWriteMask;

        public D3D12_DEPTH_STENCILOP_DESC FrontFace;

        public D3D12_DEPTH_STENCILOP_DESC BackFace;
    }
}
