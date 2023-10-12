// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3dcommon.h in Microsoft.Direct3D.D3D12 v1.600.10
// Original source is Copyright © Microsoft. Licensed under the MIT license

namespace TerraFX.Interop.DirectX;

internal unsafe partial struct D3D_SHADER_MACRO
{
    [NativeTypeName("LPCSTR")]
    public sbyte* Name;

    [NativeTypeName("LPCSTR")]
    public sbyte* Definition;
}