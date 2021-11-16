// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D2D1_COMBINE_MODE : uint
    {
        D2D1_COMBINE_MODE_UNION = 0,
        D2D1_COMBINE_MODE_INTERSECT = 1,
        D2D1_COMBINE_MODE_XOR = 2,
        D2D1_COMBINE_MODE_EXCLUDE = 3,
        D2D1_COMBINE_MODE_FORCE_DWORD = 0xffffffff,
    }
}
