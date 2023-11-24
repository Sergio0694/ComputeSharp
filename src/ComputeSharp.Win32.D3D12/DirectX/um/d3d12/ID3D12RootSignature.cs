// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID3D12RootSignature : ID3D12DeviceChild")]
[NativeInheritance("ID3D12DeviceChild")]
internal unsafe partial struct ID3D12RootSignature : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x66, 0x6B, 0x4A, 0xC5,
                0xDF, 0x72,
                0xE8, 0x4E,
                0x8B,
                0xE5,
                0xA9,
                0x46,
                0xA1,
                0x42,
                0x92,
                0x14
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;
}