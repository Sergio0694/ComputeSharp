// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Win32;

[NativeTypeName("struct ID3D12DescriptorHeap : ID3D12Pageable")]
[NativeInheritance("ID3D12Pageable")]
internal unsafe partial struct ID3D12DescriptorHeap : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x1D, 0x47, 0xFB, 0x8E,
                0x6C, 0x61,
                0x49, 0x4F,
                0x90,
                0xF7,
                0x12,
                0x7B,
                0xB7,
                0x63,
                0xFA,
                0x51
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(9)]
    public D3D12_CPU_DESCRIPTOR_HANDLE GetCPUDescriptorHandleForHeapStart()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D12DescriptorHeap*, D3D12_CPU_DESCRIPTOR_HANDLE>)(lpVtbl[9]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(10)]
    public D3D12_GPU_DESCRIPTOR_HANDLE GetGPUDescriptorHandleForHeapStart()
    {
        return ((delegate* unmanaged[MemberFunction]<ID3D12DescriptorHeap*, D3D12_GPU_DESCRIPTOR_HANDLE>)(lpVtbl[10]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this));
    }
}