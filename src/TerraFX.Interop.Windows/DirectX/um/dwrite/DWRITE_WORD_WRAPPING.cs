// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dwrite.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum DWRITE_WORD_WRAPPING
    {
        DWRITE_WORD_WRAPPING_WRAP = 0,
        DWRITE_WORD_WRAPPING_NO_WRAP = 1,
        DWRITE_WORD_WRAPPING_EMERGENCY_BREAK = 2,
        DWRITE_WORD_WRAPPING_WHOLE_WORD = 3,
        DWRITE_WORD_WRAPPING_CHARACTER = 4,
    }
}
