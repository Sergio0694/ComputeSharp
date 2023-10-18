// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace ComputeSharp.Win32;

internal enum D2D1_FILTER : uint
{
    D2D1_FILTER_MIN_MAG_MIP_POINT = 0x00,
    D2D1_FILTER_MIN_MAG_POINT_MIP_LINEAR = 0x01,
    D2D1_FILTER_MIN_POINT_MAG_LINEAR_MIP_POINT = 0x04,
    D2D1_FILTER_MIN_POINT_MAG_MIP_LINEAR = 0x05,
    D2D1_FILTER_MIN_LINEAR_MAG_MIP_POINT = 0x10,
    D2D1_FILTER_MIN_LINEAR_MAG_POINT_MIP_LINEAR = 0x11,
    D2D1_FILTER_MIN_MAG_LINEAR_MIP_POINT = 0x14,
    D2D1_FILTER_MIN_MAG_MIP_LINEAR = 0x15,
    D2D1_FILTER_ANISOTROPIC = 0x55,
    D2D1_FILTER_FORCE_DWORD = 0xffffffff
}