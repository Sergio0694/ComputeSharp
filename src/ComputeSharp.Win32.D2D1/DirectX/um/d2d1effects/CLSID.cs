// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effects.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows;

internal static partial class CLSID
{
    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_D2D1Flood
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
                0x20, 0x3C, 0xC2, 0x61,
                0x69, 0xAE,
                0x8E, 0x4D,
                0x94,
                0xCF,
                0x50,
                0x07,
                0x8D,
                0xF6,
                0x38,
                0xF2
            };

            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_D2D1DpiCompensation
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
                0xC7, 0xC5, 0x26, 0x6C,
                0xE0, 0x34,
                0xFC, 0x46,
                0x9C,
                0xFD,
                0xE5,
                0x82,
                0x37,
                0x06,
                0xE2,
                0x28
            };

            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }
}