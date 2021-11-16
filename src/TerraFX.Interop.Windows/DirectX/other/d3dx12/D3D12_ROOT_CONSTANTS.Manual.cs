// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3dx12.h in DirectX-Graphics-Samples commit a7a87f1853b5540f10920518021d91ae641033fb
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_ROOT_CONSTANTS
    {
        public D3D12_ROOT_CONSTANTS(uint num32BitValues, uint shaderRegister, uint registerSpace = 0)
        {
            Init(out this, num32BitValues, shaderRegister, registerSpace);
        }

        public void Init(uint num32BitValues, uint shaderRegister, uint registerSpace = 0)
        {
            Init(out this, num32BitValues, shaderRegister, registerSpace);
        }

        public static void Init([NativeTypeName("D3D12_ROOT_CONSTANTS &")] out D3D12_ROOT_CONSTANTS rootConstants, uint num32BitValues, uint shaderRegister, uint registerSpace = 0)
        {
            rootConstants.Num32BitValues = num32BitValues;
            rootConstants.ShaderRegister = shaderRegister;
            rootConstants.RegisterSpace = registerSpace;
        }
    }
}
