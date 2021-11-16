// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D2D1_GEOMETRY_RELATION : uint
    {
        D2D1_GEOMETRY_RELATION_UNKNOWN = 0,
        D2D1_GEOMETRY_RELATION_DISJOINT = 1,
        D2D1_GEOMETRY_RELATION_IS_CONTAINED = 2,
        D2D1_GEOMETRY_RELATION_CONTAINS = 3,
        D2D1_GEOMETRY_RELATION_OVERLAP = 4,
        D2D1_GEOMETRY_RELATION_FORCE_DWORD = 0xffffffff,
    }
}
