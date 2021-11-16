// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public partial struct D2D1_BRUSH_PROPERTIES
    {
        public float opacity;

        [NativeTypeName("D2D1_MATRIX_3X2_F")]
        public D2D_MATRIX_3X2_F transform;
    }
}
