// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dwrite.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public partial struct DWRITE_LINE_METRICS
    {
        [NativeTypeName("UINT32")]
        public uint length;

        [NativeTypeName("UINT32")]
        public uint trailingWhitespaceLength;

        [NativeTypeName("UINT32")]
        public uint newlineLength;

        public float height;

        public float baseline;

        public BOOL isTrimmed;
    }
}
