// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("8EFB471D-616C-4F49-90F7-127BB763FA51")]
    [NativeTypeName("struct ID3D12DescriptorHeap : ID3D12Pageable")]
    [NativeInheritance("ID3D12Pageable")]
    internal unsafe partial struct ID3D12DescriptorHeap
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12DescriptorHeap*, Guid*, void**, int>)(lpVtbl[0]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12DescriptorHeap*, uint>)(lpVtbl[1]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12DescriptorHeap*, uint>)(lpVtbl[2]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetPrivateData([NativeTypeName("const GUID &")] Guid* guid, uint* pDataSize, void* pData)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12DescriptorHeap*, Guid*, uint*, void*, int>)(lpVtbl[3]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this), guid, pDataSize, pData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT SetPrivateData([NativeTypeName("const GUID &")] Guid* guid, uint DataSize, [NativeTypeName("const void *")] void* pData)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12DescriptorHeap*, Guid*, uint, void*, int>)(lpVtbl[4]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this), guid, DataSize, pData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* guid, [NativeTypeName("const IUnknown *")] IUnknown* pData)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12DescriptorHeap*, Guid*, IUnknown*, int>)(lpVtbl[5]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this), guid, pData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT SetName([NativeTypeName("LPCWSTR")] ushort* Name)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12DescriptorHeap*, ushort*, int>)(lpVtbl[6]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this), Name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppvDevice)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12DescriptorHeap*, Guid*, void**, int>)(lpVtbl[7]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this), riid, ppvDevice);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public D3D12_DESCRIPTOR_HEAP_DESC GetDesc()
        {
            D3D12_DESCRIPTOR_HEAP_DESC result;
            return *((delegate* unmanaged[Stdcall]<ID3D12DescriptorHeap*, D3D12_DESCRIPTOR_HEAP_DESC*, D3D12_DESCRIPTOR_HEAP_DESC*>)(lpVtbl[8]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this), &result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public D3D12_CPU_DESCRIPTOR_HANDLE GetCPUDescriptorHandleForHeapStart()
        {
            D3D12_CPU_DESCRIPTOR_HANDLE result;
            return *((delegate* unmanaged[Stdcall]<ID3D12DescriptorHeap*, D3D12_CPU_DESCRIPTOR_HANDLE*, D3D12_CPU_DESCRIPTOR_HANDLE*>)(lpVtbl[9]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this), &result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public D3D12_GPU_DESCRIPTOR_HANDLE GetGPUDescriptorHandleForHeapStart()
        {
            D3D12_GPU_DESCRIPTOR_HANDLE result;
            return *((delegate* unmanaged[Stdcall]<ID3D12DescriptorHeap*, D3D12_GPU_DESCRIPTOR_HANDLE*, D3D12_GPU_DESCRIPTOR_HANDLE*>)(lpVtbl[10]))((ID3D12DescriptorHeap*)Unsafe.AsPointer(ref this), &result);
        }
    }
}
