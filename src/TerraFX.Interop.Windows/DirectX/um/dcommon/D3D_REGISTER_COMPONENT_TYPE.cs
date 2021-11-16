// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3dcommon.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D3D_REGISTER_COMPONENT_TYPE
    {
        D3D_REGISTER_COMPONENT_UNKNOWN = 0,
        D3D_REGISTER_COMPONENT_UINT32 = 1,
        D3D_REGISTER_COMPONENT_SINT32 = 2,
        D3D_REGISTER_COMPONENT_FLOAT32 = 3,
        D3D10_REGISTER_COMPONENT_UNKNOWN = D3D_REGISTER_COMPONENT_UNKNOWN,
        D3D10_REGISTER_COMPONENT_UINT32 = D3D_REGISTER_COMPONENT_UINT32,
        D3D10_REGISTER_COMPONENT_SINT32 = D3D_REGISTER_COMPONENT_SINT32,
        D3D10_REGISTER_COMPONENT_FLOAT32 = D3D_REGISTER_COMPONENT_FLOAT32,
    }
}
