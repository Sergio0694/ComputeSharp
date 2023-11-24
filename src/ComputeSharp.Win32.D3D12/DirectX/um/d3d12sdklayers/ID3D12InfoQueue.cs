// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12sdklayers.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID3D12InfoQueue : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct ID3D12InfoQueue : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x0B, 0xA9, 0x42, 0x07,
                0x87, 0xC3,
                0x3F, 0x48,
                0xB9,
                0x46,
                0x30,
                0xA7,
                0xE4,
                0xE6,
                0x14,
                0x58
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(4)]
    public void ClearStoredMessages()
    {
        ((delegate* unmanaged[MemberFunction]<ID3D12InfoQueue*, void>)(lpVtbl[4]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(5)]
    public HRESULT GetMessage([NativeTypeName("UINT64")] ulong MessageIndex, D3D12_MESSAGE* pMessage, [NativeTypeName("SIZE_T *")] nuint* pMessageByteLength)
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D12InfoQueue*, ulong, D3D12_MESSAGE*, nuint*, int>)(lpVtbl[5]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), MessageIndex, pMessage, pMessageByteLength);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(9)]
    [return: NativeTypeName("UINT64")]
    public ulong GetNumStoredMessagesAllowedByRetrievalFilter()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D12InfoQueue*, ulong>)(lpVtbl[9]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(25)]
    public HRESULT PushRetrievalFilter(D3D12_INFO_QUEUE_FILTER* pFilter)
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D12InfoQueue*, D3D12_INFO_QUEUE_FILTER*, int>)(lpVtbl[25]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), pFilter);
    }
}