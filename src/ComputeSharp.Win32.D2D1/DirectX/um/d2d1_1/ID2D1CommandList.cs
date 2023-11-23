// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.22621.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID2D1CommandList : ID2D1Image")]
[NativeInheritance("ID2D1Image")]
internal unsafe partial struct ID2D1CommandList : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x19, 0x4A, 0xF3, 0xB4,
                0x83, 0x23,
                0x76, 0x4D,
                0x94,
                0xF6,
                0xEC,
                0x34,
                0x36,
                0x57,
                0xC3,
                0xDC
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;
}