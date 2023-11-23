// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable IDE0055

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID2D1TransformNode : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct ID2D1TransformNode : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xE7, 0xE1, 0xEF, 0xB2,
                0x9F, 0x72,
                0x02, 0x41,
                0x94,
                0x9F,
                0x50,
                0x5F,
                0xA2,
                0x1B,
                0xF6,
                0x66
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;
}