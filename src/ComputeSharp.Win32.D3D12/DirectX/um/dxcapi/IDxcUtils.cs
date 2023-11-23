// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/dxcapi.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the University of Illinois Open Source License.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct IDxcUtils : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct IDxcUtils : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xCB, 0xC4, 0x05, 0x46,
                0x19, 0x20,
                0x2A, 0x49,
                0xAD,
                0xA4,
                0x65,
                0xF2,
                0x0B,
                0xB7,
                0xD6,
                0x7F
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, Guid*, void**, int>)(lpVtbl[0]))((IDxcUtils*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, uint>)(lpVtbl[1]))((IDxcUtils*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, uint>)(lpVtbl[2]))((IDxcUtils*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(13)]
    public HRESULT CreateReflection([NativeTypeName("const DxcBuffer *")] DxcBuffer* pData, [NativeTypeName("const IID &")] Guid* iid, void** ppvReflection)
    {
        return ((delegate* unmanaged[Stdcall]<IDxcUtils*, DxcBuffer*, Guid*, void**, int>)(lpVtbl[13]))((IDxcUtils*)Unsafe.AsPointer(ref this), pData, iid, ppvReflection);
    }
}