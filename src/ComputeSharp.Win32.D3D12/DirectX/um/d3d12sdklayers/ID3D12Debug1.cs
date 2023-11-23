// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12sdklayers.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID3D12Debug1 : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe partial struct ID3D12Debug1 : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xCA, 0xA4, 0xFA, 0xAF,
                0xFE, 0x63,
                0x8E, 0x4D,
                0xB8,
                0xAD,
                0x15,
                0x90,
                0x00,
                0xAF,
                0x43,
                0x04
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, Guid*, void**, int>)(lpVtbl[0]))((ID3D12Debug1*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, uint>)(lpVtbl[1]))((ID3D12Debug1*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, uint>)(lpVtbl[2]))((ID3D12Debug1*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public void EnableDebugLayer()
    {
        ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, void>)(lpVtbl[3]))((ID3D12Debug1*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(4)]
    public void SetEnableGPUBasedValidation(BOOL Enable)
    {
        ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, BOOL, void>)(lpVtbl[4]))((ID3D12Debug1*)Unsafe.AsPointer(ref this), Enable);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(5)]
    public void SetEnableSynchronizedCommandQueueValidation(BOOL Enable)
    {
        ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, BOOL, void>)(lpVtbl[5]))((ID3D12Debug1*)Unsafe.AsPointer(ref this), Enable);
    }
}