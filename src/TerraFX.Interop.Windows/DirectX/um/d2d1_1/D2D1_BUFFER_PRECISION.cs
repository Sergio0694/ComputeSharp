// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D2D1_BUFFER_PRECISION : uint
    {
        D2D1_BUFFER_PRECISION_UNKNOWN = 0,
        D2D1_BUFFER_PRECISION_8BPC_UNORM = 1,
        D2D1_BUFFER_PRECISION_8BPC_UNORM_SRGB = 2,
        D2D1_BUFFER_PRECISION_16BPC_UNORM = 3,
        D2D1_BUFFER_PRECISION_16BPC_FLOAT = 4,
        D2D1_BUFFER_PRECISION_32BPC_FLOAT = 5,
        D2D1_BUFFER_PRECISION_FORCE_DWORD = 0xffffffff,
    }
}
