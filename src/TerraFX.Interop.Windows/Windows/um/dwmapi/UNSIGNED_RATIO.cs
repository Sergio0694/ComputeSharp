// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dwmapi.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal partial struct UNSIGNED_RATIO
{
    [NativeTypeName("UINT32")]
    public uint uiNumerator;

    [NativeTypeName("UINT32")]
    public uint uiDenominator;
}