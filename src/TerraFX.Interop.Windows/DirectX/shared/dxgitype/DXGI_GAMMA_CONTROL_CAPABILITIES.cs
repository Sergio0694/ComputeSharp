// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/dxgitype.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public unsafe partial struct DXGI_GAMMA_CONTROL_CAPABILITIES
    {
        public BOOL ScaleAndOffsetSupported;

        public float MaxConvertedValue;

        public float MinConvertedValue;

        public uint NumGammaControlPoints;

        [NativeTypeName("float [1025]")]
        public fixed float ControlPointPositions[1025];
    }
}
