// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/objidlbase.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

internal static partial class IID
{
    public static ref readonly Guid IID_ISequentialStream
    {
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
                0x30, 0x3A, 0x73, 0x0C,
                0x1C, 0x2A,
                0xCE, 0x11,
                0xAD,
                0xE5,
                0x00,
                0xAA,
                0x00,
                0x44,
                0x77,
                0x3D
            };

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public static ref readonly Guid IID_IStream
    {
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
                0x0C, 0x00, 0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0xC0,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x46
            };

            Debug.Assert(data.Length == Unsafe.SizeOf<Guid>());
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }
}