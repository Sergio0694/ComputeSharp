// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/dxgi1_3.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.Versioning;

namespace TerraFX.Interop.DirectX
{
    [SupportedOSPlatform("windows8.1")]
    internal partial struct DXGI_MATRIX_3X2_F
    {
        public float _11;

        public float _12;

        public float _21;

        public float _22;

        public float _31;

        public float _32;
    }
}
