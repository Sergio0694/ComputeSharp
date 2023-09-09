// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX;

internal partial struct D2D1_FEATURE_DATA_D3D10_X_HARDWARE_OPTIONS
{
    [NativeTypeName("BOOL")]
    public int computeShaders_Plus_RawAndStructuredBuffers_Via_Shader_4_x;
}