// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D2D1_INTERPOLATION_MODE_DEFINITION
    {
        D2D1_INTERPOLATION_MODE_DEFINITION_NEAREST_NEIGHBOR = 0,
        D2D1_INTERPOLATION_MODE_DEFINITION_LINEAR = 1,
        D2D1_INTERPOLATION_MODE_DEFINITION_CUBIC = 2,
        D2D1_INTERPOLATION_MODE_DEFINITION_MULTI_SAMPLE_LINEAR = 3,
        D2D1_INTERPOLATION_MODE_DEFINITION_ANISOTROPIC = 4,
        D2D1_INTERPOLATION_MODE_DEFINITION_HIGH_QUALITY_CUBIC = 5,
        D2D1_INTERPOLATION_MODE_DEFINITION_FANT = 6,
        D2D1_INTERPOLATION_MODE_DEFINITION_MIPMAP_LINEAR = 7,
    }
}
