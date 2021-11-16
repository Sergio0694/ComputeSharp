// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3dx12.h in DirectX-Graphics-Samples commit a7a87f1853b5540f10920518021d91ae641033fb
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using static TerraFX.Interop.DirectX.D3D12_ROOT_DESCRIPTOR_FLAGS;

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_ROOT_DESCRIPTOR1
    {
        public D3D12_ROOT_DESCRIPTOR1(uint shaderRegister, uint registerSpace = 0, D3D12_ROOT_DESCRIPTOR_FLAGS flags = D3D12_ROOT_DESCRIPTOR_FLAG_NONE)
        {
            Init(out this, shaderRegister, registerSpace, flags);
        }

        public void Init(uint shaderRegister, uint registerSpace = 0, D3D12_ROOT_DESCRIPTOR_FLAGS flags = D3D12_ROOT_DESCRIPTOR_FLAG_NONE)
        {
            Init(out this, shaderRegister, registerSpace, flags);
        }

        public static void Init([NativeTypeName("D3D12_ROOT_DESCRIPTOR1 &")] out D3D12_ROOT_DESCRIPTOR1 table, uint shaderRegister, uint registerSpace = 0, D3D12_ROOT_DESCRIPTOR_FLAGS flags = D3D12_ROOT_DESCRIPTOR_FLAG_NONE)
        {
            table.ShaderRegister = shaderRegister;
            table.RegisterSpace = registerSpace;
            table.Flags = flags;
        }
    }
}
