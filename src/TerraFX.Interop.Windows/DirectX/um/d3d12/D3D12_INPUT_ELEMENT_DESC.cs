// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_INPUT_ELEMENT_DESC
    {
        [NativeTypeName("LPCSTR")]
        public sbyte* SemanticName;

        public uint SemanticIndex;

        public DXGI_FORMAT Format;

        public uint InputSlot;

        public uint AlignedByteOffset;

        public D3D12_INPUT_CLASSIFICATION InputSlotClass;

        public uint InstanceDataStepRate;
    }
}
