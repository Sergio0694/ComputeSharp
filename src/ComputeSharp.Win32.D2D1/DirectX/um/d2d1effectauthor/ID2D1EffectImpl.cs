// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1effectauthor.h in the Windows SDK for Windows 10.0.22000.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID2D1EffectImpl : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct ID2D1EffectImpl : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x3F, 0xFD, 0x48, 0xA2,
                0x6C, 0x3E,
                0x63, 0x4E,
                0x9F,
                0x03,
                0x7F,
                0x68,
                0xEC,
                0xC9,
                0x1D,
                0xB9
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1EffectImpl*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1EffectImpl*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1EffectImpl*, uint>)(lpVtbl[1]))((ID2D1EffectImpl*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1EffectImpl*, uint>)(lpVtbl[2]))((ID2D1EffectImpl*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public HRESULT Initialize(ID2D1EffectContext* effectContext, ID2D1TransformGraph* transformGraph)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1EffectImpl*, ID2D1EffectContext*, ID2D1TransformGraph*, int>)(lpVtbl[3]))((ID2D1EffectImpl*)Unsafe.AsPointer(ref this), effectContext, transformGraph);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(4)]
    public HRESULT PrepareForRender(D2D1_CHANGE_TYPE changeType)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1EffectImpl*, D2D1_CHANGE_TYPE, int>)(lpVtbl[4]))((ID2D1EffectImpl*)Unsafe.AsPointer(ref this), changeType);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(5)]
    public HRESULT SetGraph(ID2D1TransformGraph* transformGraph)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1EffectImpl*, ID2D1TransformGraph*, int>)(lpVtbl[5]))((ID2D1EffectImpl*)Unsafe.AsPointer(ref this), transformGraph);
    }
}