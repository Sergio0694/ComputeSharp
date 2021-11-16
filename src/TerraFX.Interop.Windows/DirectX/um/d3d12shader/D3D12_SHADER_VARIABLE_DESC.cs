// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12shader.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct D3D12_SHADER_VARIABLE_DESC
    {
        [NativeTypeName("LPCSTR")]
        public sbyte* Name;

        public uint StartOffset;

        public uint Size;

        public uint uFlags;

        [NativeTypeName("LPVOID")]
        public void* DefaultValue;

        public uint StartTexture;

        public uint TextureSize;

        public uint StartSampler;

        public uint SamplerSize;
    }
}
