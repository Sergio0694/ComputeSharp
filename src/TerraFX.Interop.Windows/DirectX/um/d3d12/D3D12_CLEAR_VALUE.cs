// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.InteropServices;

namespace TerraFX.Interop.DirectX
{
    internal unsafe partial struct D3D12_CLEAR_VALUE
    {
        public DXGI_FORMAT Format;

        [NativeTypeName("D3D12_CLEAR_VALUE::(anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/d3d12.h:2710:5)")]
        public _Anonymous_e__Union Anonymous;

        [StructLayout(LayoutKind.Explicit)]
        internal unsafe partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("FLOAT [4]")]
            public fixed float Color[4];

            [FieldOffset(0)]
            public D3D12_DEPTH_STENCIL_VALUE DepthStencil;
        }
    }
}