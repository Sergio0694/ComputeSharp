// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/wincodec.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;

namespace ComputeSharp.Win32;

internal static partial class CLSID
{
    [NativeTypeName("const GUID")]
    public static ref readonly Guid CLSID_WICImagingFactory2
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xE8, 0x06, 0x7D, 0x31,
                0x24, 0x5F,
                0x3D, 0x43,
                0xBD,
                0xF7,
                0x79,
                0xCE,
                0x68,
                0xD8,
                0xAB,
                0xC2
            ];

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }
}