// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3dx12.h in DirectX-Graphics-Samples commit a7a87f1853b5540f10920518021d91ae641033fb
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_SHADER_BYTECODE
    {
        public D3D12_SHADER_BYTECODE(ID3DBlob* pShaderBlob)
        {
            pShaderBytecode = pShaderBlob->GetBufferPointer();
            BytecodeLength = pShaderBlob->GetBufferSize();
        }

        public D3D12_SHADER_BYTECODE([NativeTypeName("const void *")] void* _pShaderBytecode, [NativeTypeName("SIZE_T")] nuint bytecodeLength)
        {
            pShaderBytecode = _pShaderBytecode;
            BytecodeLength = bytecodeLength;
        }
    }
}
