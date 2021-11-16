// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("696442BE-A72E-4059-BC79-5B5C98040FAD")]
    [NativeTypeName("struct ID3D12Resource : ID3D12Pageable")]
    [NativeInheritance("ID3D12Pageable")]
    public unsafe partial struct ID3D12Resource : ID3D12Resource.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<ID3D12Resource*, Guid*, void**, int>)(lpVtbl[0]))((ID3D12Resource*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<ID3D12Resource*, uint>)(lpVtbl[1]))((ID3D12Resource*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<ID3D12Resource*, uint>)(lpVtbl[2]))((ID3D12Resource*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetPrivateData([NativeTypeName("const GUID &")] Guid* guid, uint* pDataSize, void* pData)
        {
            return ((delegate* unmanaged<ID3D12Resource*, Guid*, uint*, void*, int>)(lpVtbl[3]))((ID3D12Resource*)Unsafe.AsPointer(ref this), guid, pDataSize, pData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT SetPrivateData([NativeTypeName("const GUID &")] Guid* guid, uint DataSize, [NativeTypeName("const void *")] void* pData)
        {
            return ((delegate* unmanaged<ID3D12Resource*, Guid*, uint, void*, int>)(lpVtbl[4]))((ID3D12Resource*)Unsafe.AsPointer(ref this), guid, DataSize, pData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* guid, [NativeTypeName("const IUnknown *")] IUnknown* pData)
        {
            return ((delegate* unmanaged<ID3D12Resource*, Guid*, IUnknown*, int>)(lpVtbl[5]))((ID3D12Resource*)Unsafe.AsPointer(ref this), guid, pData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT SetName([NativeTypeName("LPCWSTR")] ushort* Name)
        {
            return ((delegate* unmanaged<ID3D12Resource*, ushort*, int>)(lpVtbl[6]))((ID3D12Resource*)Unsafe.AsPointer(ref this), Name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppvDevice)
        {
            return ((delegate* unmanaged<ID3D12Resource*, Guid*, void**, int>)(lpVtbl[7]))((ID3D12Resource*)Unsafe.AsPointer(ref this), riid, ppvDevice);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT Map(uint Subresource, [NativeTypeName("const D3D12_RANGE *")] D3D12_RANGE* pReadRange, void** ppData)
        {
            return ((delegate* unmanaged<ID3D12Resource*, uint, D3D12_RANGE*, void**, int>)(lpVtbl[8]))((ID3D12Resource*)Unsafe.AsPointer(ref this), Subresource, pReadRange, ppData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public void Unmap(uint Subresource, [NativeTypeName("const D3D12_RANGE *")] D3D12_RANGE* pWrittenRange)
        {
            ((delegate* unmanaged<ID3D12Resource*, uint, D3D12_RANGE*, void>)(lpVtbl[9]))((ID3D12Resource*)Unsafe.AsPointer(ref this), Subresource, pWrittenRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public D3D12_RESOURCE_DESC GetDesc()
        {
            D3D12_RESOURCE_DESC result;
            return *((delegate* unmanaged<ID3D12Resource*, D3D12_RESOURCE_DESC*, D3D12_RESOURCE_DESC*>)(lpVtbl[10]))((ID3D12Resource*)Unsafe.AsPointer(ref this), &result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        [return: NativeTypeName("D3D12_GPU_VIRTUAL_ADDRESS")]
        public ulong GetGPUVirtualAddress()
        {
            return ((delegate* unmanaged<ID3D12Resource*, ulong>)(lpVtbl[11]))((ID3D12Resource*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public HRESULT WriteToSubresource(uint DstSubresource, [NativeTypeName("const D3D12_BOX *")] D3D12_BOX* pDstBox, [NativeTypeName("const void *")] void* pSrcData, uint SrcRowPitch, uint SrcDepthPitch)
        {
            return ((delegate* unmanaged<ID3D12Resource*, uint, D3D12_BOX*, void*, uint, uint, int>)(lpVtbl[12]))((ID3D12Resource*)Unsafe.AsPointer(ref this), DstSubresource, pDstBox, pSrcData, SrcRowPitch, SrcDepthPitch);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public HRESULT ReadFromSubresource(void* pDstData, uint DstRowPitch, uint DstDepthPitch, uint SrcSubresource, [NativeTypeName("const D3D12_BOX *")] D3D12_BOX* pSrcBox)
        {
            return ((delegate* unmanaged<ID3D12Resource*, void*, uint, uint, uint, D3D12_BOX*, int>)(lpVtbl[13]))((ID3D12Resource*)Unsafe.AsPointer(ref this), pDstData, DstRowPitch, DstDepthPitch, SrcSubresource, pSrcBox);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(14)]
        public HRESULT GetHeapProperties(D3D12_HEAP_PROPERTIES* pHeapProperties, D3D12_HEAP_FLAGS* pHeapFlags)
        {
            return ((delegate* unmanaged<ID3D12Resource*, D3D12_HEAP_PROPERTIES*, D3D12_HEAP_FLAGS*, int>)(lpVtbl[14]))((ID3D12Resource*)Unsafe.AsPointer(ref this), pHeapProperties, pHeapFlags);
        }

        public interface Interface : ID3D12Pageable.Interface
        {
            [VtblIndex(8)]
            HRESULT Map(uint Subresource, [NativeTypeName("const D3D12_RANGE *")] D3D12_RANGE* pReadRange, void** ppData);

            [VtblIndex(9)]
            void Unmap(uint Subresource, [NativeTypeName("const D3D12_RANGE *")] D3D12_RANGE* pWrittenRange);

            [VtblIndex(10)]
            D3D12_RESOURCE_DESC GetDesc();

            [VtblIndex(11)]
            [return: NativeTypeName("D3D12_GPU_VIRTUAL_ADDRESS")]
            ulong GetGPUVirtualAddress();

            [VtblIndex(12)]
            HRESULT WriteToSubresource(uint DstSubresource, [NativeTypeName("const D3D12_BOX *")] D3D12_BOX* pDstBox, [NativeTypeName("const void *")] void* pSrcData, uint SrcRowPitch, uint SrcDepthPitch);

            [VtblIndex(13)]
            HRESULT ReadFromSubresource(void* pDstData, uint DstRowPitch, uint DstDepthPitch, uint SrcSubresource, [NativeTypeName("const D3D12_BOX *")] D3D12_BOX* pSrcBox);

            [VtblIndex(14)]
            HRESULT GetHeapProperties(D3D12_HEAP_PROPERTIES* pHeapProperties, D3D12_HEAP_FLAGS* pHeapFlags);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, uint> Release;

            [NativeTypeName("HRESULT (const GUID &, UINT *, void *) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, Guid*, uint*, void*, int> GetPrivateData;

            [NativeTypeName("HRESULT (const GUID &, UINT, const void *) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, Guid*, uint, void*, int> SetPrivateData;

            [NativeTypeName("HRESULT (const GUID &, const IUnknown *) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, Guid*, IUnknown*, int> SetPrivateDataInterface;

            [NativeTypeName("HRESULT (LPCWSTR) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, ushort*, int> SetName;

            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, Guid*, void**, int> GetDevice;

            [NativeTypeName("HRESULT (UINT, const D3D12_RANGE *, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, uint, D3D12_RANGE*, void**, int> Map;

            [NativeTypeName("void (UINT, const D3D12_RANGE *) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, uint, D3D12_RANGE*, void> Unmap;

            [NativeTypeName("D3D12_RESOURCE_DESC () __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, D3D12_RESOURCE_DESC*, D3D12_RESOURCE_DESC*> GetDesc;

            [NativeTypeName("D3D12_GPU_VIRTUAL_ADDRESS () __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, ulong> GetGPUVirtualAddress;

            [NativeTypeName("HRESULT (UINT, const D3D12_BOX *, const void *, UINT, UINT) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, uint, D3D12_BOX*, void*, uint, uint, int> WriteToSubresource;

            [NativeTypeName("HRESULT (void *, UINT, UINT, UINT, const D3D12_BOX *) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, void*, uint, uint, uint, D3D12_BOX*, int> ReadFromSubresource;

            [NativeTypeName("HRESULT (D3D12_HEAP_PROPERTIES *, D3D12_HEAP_FLAGS *) __attribute__((stdcall))")]
            public delegate* unmanaged<ID3D12Resource*, D3D12_HEAP_PROPERTIES*, D3D12_HEAP_FLAGS*, int> GetHeapProperties;
        }
    }
}
