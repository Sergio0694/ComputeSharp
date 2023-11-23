// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12sdklayers.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID3D12Debug : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct ID3D12Debug : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xB7, 0x88, 0x44, 0x34,
                0x46, 0x68,
                0x4B, 0x47,
                0xB9,
                0x89,
                0xF0,
                0x27,
                0x44,
                0x82,
                0x45,
                0xE0
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12Debug*, Guid*, void**, int>)(lpVtbl[0]))((ID3D12Debug*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12Debug*, uint>)(lpVtbl[1]))((ID3D12Debug*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12Debug*, uint>)(lpVtbl[2]))((ID3D12Debug*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public void EnableDebugLayer()
    {
        ((delegate* unmanaged[Stdcall]<ID3D12Debug*, void>)(lpVtbl[3]))((ID3D12Debug*)Unsafe.AsPointer(ref this));
    }
}