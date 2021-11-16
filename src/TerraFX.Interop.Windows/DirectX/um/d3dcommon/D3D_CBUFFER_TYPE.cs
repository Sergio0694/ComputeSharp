// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3dcommon.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D3D_CBUFFER_TYPE
    {
        D3D_CT_CBUFFER = 0,
        D3D_CT_TBUFFER = (D3D_CT_CBUFFER + 1),
        D3D_CT_INTERFACE_POINTERS = (D3D_CT_TBUFFER + 1),
        D3D_CT_RESOURCE_BIND_INFO = (D3D_CT_INTERFACE_POINTERS + 1),
        D3D10_CT_CBUFFER = D3D_CT_CBUFFER,
        D3D10_CT_TBUFFER = D3D_CT_TBUFFER,
        D3D11_CT_CBUFFER = D3D_CT_CBUFFER,
        D3D11_CT_TBUFFER = D3D_CT_TBUFFER,
        D3D11_CT_INTERFACE_POINTERS = D3D_CT_INTERFACE_POINTERS,
        D3D11_CT_RESOURCE_BIND_INFO = D3D_CT_RESOURCE_BIND_INFO,
    }
}
