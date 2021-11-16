// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/wincodec.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.Versioning;
using TerraFX.Interop.DirectX;

namespace TerraFX.Interop.Windows
{
    [SupportedOSPlatform("windows8.0")]
    public partial struct WICImageParameters
    {
        public D2D1_PIXEL_FORMAT PixelFormat;

        public float DpiX;

        public float DpiY;

        public float Top;

        public float Left;

        [NativeTypeName("UINT32")]
        public uint PixelWidth;

        [NativeTypeName("UINT32")]
        public uint PixelHeight;
    }
}
