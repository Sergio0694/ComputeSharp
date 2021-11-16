// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/wincodec.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.Windows
{
    public enum WICBitmapPaletteType
    {
        WICBitmapPaletteTypeCustom = 0,
        WICBitmapPaletteTypeMedianCut = 0x1,
        WICBitmapPaletteTypeFixedBW = 0x2,
        WICBitmapPaletteTypeFixedHalftone8 = 0x3,
        WICBitmapPaletteTypeFixedHalftone27 = 0x4,
        WICBitmapPaletteTypeFixedHalftone64 = 0x5,
        WICBitmapPaletteTypeFixedHalftone125 = 0x6,
        WICBitmapPaletteTypeFixedHalftone216 = 0x7,
        WICBitmapPaletteTypeFixedWebPalette = WICBitmapPaletteTypeFixedHalftone216,
        WICBitmapPaletteTypeFixedHalftone252 = 0x8,
        WICBitmapPaletteTypeFixedHalftone256 = 0x9,
        WICBitmapPaletteTypeFixedGray4 = 0xa,
        WICBitmapPaletteTypeFixedGray16 = 0xb,
        WICBitmapPaletteTypeFixedGray256 = 0xc,
        WICBITMAPPALETTETYPE_FORCE_DWORD = 0x7fffffff,
    }
}
