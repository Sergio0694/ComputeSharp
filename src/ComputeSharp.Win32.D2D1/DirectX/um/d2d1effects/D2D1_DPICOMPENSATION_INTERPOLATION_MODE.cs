// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effects.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace ComputeSharp.Win32;

internal enum D2D1_DPICOMPENSATION_INTERPOLATION_MODE : uint
{
    D2D1_DPICOMPENSATION_INTERPOLATION_MODE_NEAREST_NEIGHBOR = 0,
    D2D1_DPICOMPENSATION_INTERPOLATION_MODE_LINEAR = 1,
    D2D1_DPICOMPENSATION_INTERPOLATION_MODE_CUBIC = 2,
    D2D1_DPICOMPENSATION_INTERPOLATION_MODE_MULTI_SAMPLE_LINEAR = 3,
    D2D1_DPICOMPENSATION_INTERPOLATION_MODE_ANISOTROPIC = 4,
    D2D1_DPICOMPENSATION_INTERPOLATION_MODE_HIGH_QUALITY_CUBIC = 5,
    D2D1_DPICOMPENSATION_INTERPOLATION_MODE_FORCE_DWORD = 0xffffffff,
}