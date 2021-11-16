// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dcommon.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public partial struct D2D_RECT_U
    {
        [NativeTypeName("UINT32")]
        public uint left;

        [NativeTypeName("UINT32")]
        public uint top;

        [NativeTypeName("UINT32")]
        public uint right;

        [NativeTypeName("UINT32")]
        public uint bottom;
    }
}
