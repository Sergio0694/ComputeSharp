// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dcommon.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D2D1_ALPHA_MODE : uint
    {
        D2D1_ALPHA_MODE_UNKNOWN = 0,
        D2D1_ALPHA_MODE_PREMULTIPLIED = 1,
        D2D1_ALPHA_MODE_STRAIGHT = 2,
        D2D1_ALPHA_MODE_IGNORE = 3,
        D2D1_ALPHA_MODE_FORCE_DWORD = 0xffffffff,
    }
}
