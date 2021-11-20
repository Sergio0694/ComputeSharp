// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("5B160D0F-AC1B-4185-8BA8-B3AE42A5A455")]
    [NativeTypeName("struct ID3D12GraphicsCommandList : ID3D12CommandList")]
    [NativeInheritance("ID3D12CommandList")]
    internal unsafe partial struct ID3D12GraphicsCommandList
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, Guid*, void**, int>)(lpVtbl[0]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint>)(lpVtbl[1]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint>)(lpVtbl[2]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetPrivateData([NativeTypeName("const GUID &")] Guid* guid, uint* pDataSize, void* pData)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, Guid*, uint*, void*, int>)(lpVtbl[3]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), guid, pDataSize, pData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT SetPrivateData([NativeTypeName("const GUID &")] Guid* guid, uint DataSize, [NativeTypeName("const void *")] void* pData)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, Guid*, uint, void*, int>)(lpVtbl[4]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), guid, DataSize, pData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT SetPrivateDataInterface([NativeTypeName("const GUID &")] Guid* guid, [NativeTypeName("const IUnknown *")] IUnknown* pData)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, Guid*, IUnknown*, int>)(lpVtbl[5]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), guid, pData);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public HRESULT SetName([NativeTypeName("LPCWSTR")] ushort* Name)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, ushort*, int>)(lpVtbl[6]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), Name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        public HRESULT GetDevice([NativeTypeName("const IID &")] Guid* riid, void** ppvDevice)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, Guid*, void**, int>)(lpVtbl[7]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), riid, ppvDevice);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public new D3D12_COMMAND_LIST_TYPE GetType()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, D3D12_COMMAND_LIST_TYPE>)(lpVtbl[8]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT Close()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, int>)(lpVtbl[9]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT Reset(ID3D12CommandAllocator* pAllocator, ID3D12PipelineState* pInitialState)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, ID3D12CommandAllocator*, ID3D12PipelineState*, int>)(lpVtbl[10]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), pAllocator, pInitialState);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public void ClearState(ID3D12PipelineState* pPipelineState)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, ID3D12PipelineState*, void>)(lpVtbl[11]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), pPipelineState);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public void DrawInstanced(uint VertexCountPerInstance, uint InstanceCount, uint StartVertexLocation, uint StartInstanceLocation)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, uint, uint, uint, void>)(lpVtbl[12]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), VertexCountPerInstance, InstanceCount, StartVertexLocation, StartInstanceLocation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public void DrawIndexedInstanced(uint IndexCountPerInstance, uint InstanceCount, uint StartIndexLocation, int BaseVertexLocation, uint StartInstanceLocation)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, uint, uint, int, uint, void>)(lpVtbl[13]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), IndexCountPerInstance, InstanceCount, StartIndexLocation, BaseVertexLocation, StartInstanceLocation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(14)]
        public void Dispatch(uint ThreadGroupCountX, uint ThreadGroupCountY, uint ThreadGroupCountZ)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, uint, uint, void>)(lpVtbl[14]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), ThreadGroupCountX, ThreadGroupCountY, ThreadGroupCountZ);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(15)]
        public void CopyBufferRegion(ID3D12Resource* pDstBuffer, [NativeTypeName("UINT64")] ulong DstOffset, ID3D12Resource* pSrcBuffer, [NativeTypeName("UINT64")] ulong SrcOffset, [NativeTypeName("UINT64")] ulong NumBytes)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, ID3D12Resource*, ulong, ID3D12Resource*, ulong, ulong, void>)(lpVtbl[15]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), pDstBuffer, DstOffset, pSrcBuffer, SrcOffset, NumBytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(16)]
        public void CopyTextureRegion([NativeTypeName("const D3D12_TEXTURE_COPY_LOCATION *")] D3D12_TEXTURE_COPY_LOCATION* pDst, uint DstX, uint DstY, uint DstZ, [NativeTypeName("const D3D12_TEXTURE_COPY_LOCATION *")] D3D12_TEXTURE_COPY_LOCATION* pSrc, [NativeTypeName("const D3D12_BOX *")] D3D12_BOX* pSrcBox)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, D3D12_TEXTURE_COPY_LOCATION*, uint, uint, uint, D3D12_TEXTURE_COPY_LOCATION*, D3D12_BOX*, void>)(lpVtbl[16]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), pDst, DstX, DstY, DstZ, pSrc, pSrcBox);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(17)]
        public void CopyResource(ID3D12Resource* pDstResource, ID3D12Resource* pSrcResource)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, ID3D12Resource*, ID3D12Resource*, void>)(lpVtbl[17]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), pDstResource, pSrcResource);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(19)]
        public void ResolveSubresource(ID3D12Resource* pDstResource, uint DstSubresource, ID3D12Resource* pSrcResource, uint SrcSubresource, DXGI_FORMAT Format)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, ID3D12Resource*, uint, ID3D12Resource*, uint, DXGI_FORMAT, void>)(lpVtbl[19]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), pDstResource, DstSubresource, pSrcResource, SrcSubresource, Format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(20)]
        public void IASetPrimitiveTopology([NativeTypeName("D3D12_PRIMITIVE_TOPOLOGY")] D3D_PRIMITIVE_TOPOLOGY PrimitiveTopology)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, D3D_PRIMITIVE_TOPOLOGY, void>)(lpVtbl[20]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), PrimitiveTopology);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(23)]
        public void OMSetBlendFactor([NativeTypeName("const FLOAT [4]")] float* BlendFactor)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, float*, void>)(lpVtbl[23]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), BlendFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(24)]
        public void OMSetStencilRef(uint StencilRef)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, void>)(lpVtbl[24]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), StencilRef);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(25)]
        public void SetPipelineState(ID3D12PipelineState* pPipelineState)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, ID3D12PipelineState*, void>)(lpVtbl[25]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), pPipelineState);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(26)]
        public void ResourceBarrier(uint NumBarriers, [NativeTypeName("const D3D12_RESOURCE_BARRIER *")] D3D12_RESOURCE_BARRIER* pBarriers)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, D3D12_RESOURCE_BARRIER*, void>)(lpVtbl[26]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), NumBarriers, pBarriers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(27)]
        public void ExecuteBundle(ID3D12GraphicsCommandList* pCommandList)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, ID3D12GraphicsCommandList*, void>)(lpVtbl[27]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), pCommandList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(28)]
        public void SetDescriptorHeaps(uint NumDescriptorHeaps, [NativeTypeName("ID3D12DescriptorHeap *const *")] ID3D12DescriptorHeap** ppDescriptorHeaps)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, ID3D12DescriptorHeap**, void>)(lpVtbl[28]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), NumDescriptorHeaps, ppDescriptorHeaps);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(29)]
        public void SetComputeRootSignature(ID3D12RootSignature* pRootSignature)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, ID3D12RootSignature*, void>)(lpVtbl[29]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), pRootSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(30)]
        public void SetGraphicsRootSignature(ID3D12RootSignature* pRootSignature)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, ID3D12RootSignature*, void>)(lpVtbl[30]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), pRootSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(31)]
        public void SetComputeRootDescriptorTable(uint RootParameterIndex, D3D12_GPU_DESCRIPTOR_HANDLE BaseDescriptor)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, D3D12_GPU_DESCRIPTOR_HANDLE, void>)(lpVtbl[31]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, BaseDescriptor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(32)]
        public void SetGraphicsRootDescriptorTable(uint RootParameterIndex, D3D12_GPU_DESCRIPTOR_HANDLE BaseDescriptor)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, D3D12_GPU_DESCRIPTOR_HANDLE, void>)(lpVtbl[32]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, BaseDescriptor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(33)]
        public void SetComputeRoot32BitConstant(uint RootParameterIndex, uint SrcData, uint DestOffsetIn32BitValues)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, uint, uint, void>)(lpVtbl[33]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, SrcData, DestOffsetIn32BitValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(34)]
        public void SetGraphicsRoot32BitConstant(uint RootParameterIndex, uint SrcData, uint DestOffsetIn32BitValues)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, uint, uint, void>)(lpVtbl[34]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, SrcData, DestOffsetIn32BitValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(35)]
        public void SetComputeRoot32BitConstants(uint RootParameterIndex, uint Num32BitValuesToSet, [NativeTypeName("const void *")] void* pSrcData, uint DestOffsetIn32BitValues)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, uint, void*, uint, void>)(lpVtbl[35]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, Num32BitValuesToSet, pSrcData, DestOffsetIn32BitValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(36)]
        public void SetGraphicsRoot32BitConstants(uint RootParameterIndex, uint Num32BitValuesToSet, [NativeTypeName("const void *")] void* pSrcData, uint DestOffsetIn32BitValues)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, uint, void*, uint, void>)(lpVtbl[36]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, Num32BitValuesToSet, pSrcData, DestOffsetIn32BitValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(37)]
        public void SetComputeRootConstantBufferView(uint RootParameterIndex, [NativeTypeName("D3D12_GPU_VIRTUAL_ADDRESS")] ulong BufferLocation)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, ulong, void>)(lpVtbl[37]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, BufferLocation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(38)]
        public void SetGraphicsRootConstantBufferView(uint RootParameterIndex, [NativeTypeName("D3D12_GPU_VIRTUAL_ADDRESS")] ulong BufferLocation)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, ulong, void>)(lpVtbl[38]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, BufferLocation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(39)]
        public void SetComputeRootShaderResourceView(uint RootParameterIndex, [NativeTypeName("D3D12_GPU_VIRTUAL_ADDRESS")] ulong BufferLocation)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, ulong, void>)(lpVtbl[39]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, BufferLocation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(40)]
        public void SetGraphicsRootShaderResourceView(uint RootParameterIndex, [NativeTypeName("D3D12_GPU_VIRTUAL_ADDRESS")] ulong BufferLocation)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, ulong, void>)(lpVtbl[40]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, BufferLocation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(41)]
        public void SetComputeRootUnorderedAccessView(uint RootParameterIndex, [NativeTypeName("D3D12_GPU_VIRTUAL_ADDRESS")] ulong BufferLocation)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, ulong, void>)(lpVtbl[41]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, BufferLocation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(42)]
        public void SetGraphicsRootUnorderedAccessView(uint RootParameterIndex, [NativeTypeName("D3D12_GPU_VIRTUAL_ADDRESS")] ulong BufferLocation)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12GraphicsCommandList*, uint, ulong, void>)(lpVtbl[42]))((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref this), RootParameterIndex, BufferLocation);
        }
    }
}
