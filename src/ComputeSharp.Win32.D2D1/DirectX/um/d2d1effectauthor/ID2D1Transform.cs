// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID2D1Transform : ID2D1TransformNode")]
[NativeInheritance("ID2D1TransformNode")]
internal unsafe partial struct ID2D1Transform : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x7D, 0x28, 0x1A, 0xEF,
                0x2A, 0x34,
                0x76, 0x4F,
                0x8F,
                0xDB,
                0xDA,
                0x0D,
                0x6E,
                0xA9,
                0xF9,
                0x2B
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;
}