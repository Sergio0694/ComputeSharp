// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D2D1_PRINT_FONT_SUBSET_MODE : uint
    {
        D2D1_PRINT_FONT_SUBSET_MODE_DEFAULT = 0,
        D2D1_PRINT_FONT_SUBSET_MODE_EACHPAGE = 1,
        D2D1_PRINT_FONT_SUBSET_MODE_NONE = 2,
        D2D1_PRINT_FONT_SUBSET_MODE_FORCE_DWORD = 0xffffffff,
    }
}
