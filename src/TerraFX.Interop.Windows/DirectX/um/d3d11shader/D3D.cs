// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d11shader.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    internal static partial class D3D
    {
        [NativeTypeName("#define D3D_SHADER_REQUIRES_DOUBLES 0x00000001")]
        public const int D3D_SHADER_REQUIRES_DOUBLES = 0x00000001;

        [NativeTypeName("#define D3D_SHADER_REQUIRES_11_1_DOUBLE_EXTENSIONS 0x00000020")]
        public const int D3D_SHADER_REQUIRES_11_1_DOUBLE_EXTENSIONS = 0x00000020;
    }
}