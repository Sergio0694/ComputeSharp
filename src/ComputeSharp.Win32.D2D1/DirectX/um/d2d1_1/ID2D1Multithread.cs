// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID2D1Multithread : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct ID2D1Multithread : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xBC, 0xE7, 0xE6, 0x31,
                0xFF, 0xE0,
                0x46, 0x4D,
                0x8C,
                0x64,
                0xA0,
                0xA8,
                0xC4,
                0x1C,
                0x15,
                0xD3
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1Multithread*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1Multithread*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1Multithread*, uint>)(lpVtbl[1]))((ID2D1Multithread*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release" />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1Multithread*, uint>)(lpVtbl[2]))((ID2D1Multithread*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public int GetMultithreadProtected()
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1Multithread*, int>)(lpVtbl[3]))((ID2D1Multithread*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(4)]
    public void Enter()
    {
        ((delegate* unmanaged[MemberFunction]<ID2D1Multithread*, void>)(lpVtbl[4]))((ID2D1Multithread*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(5)]
    public void Leave()
    {
        ((delegate* unmanaged[MemberFunction]<ID2D1Multithread*, void>)(lpVtbl[5]))((ID2D1Multithread*)Unsafe.AsPointer(ref this));
    }
}