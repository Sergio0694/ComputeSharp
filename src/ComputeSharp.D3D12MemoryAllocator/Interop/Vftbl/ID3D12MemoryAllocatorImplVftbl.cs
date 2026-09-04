using System;
using System.Runtime.InteropServices;
using ComputeSharp.Interop.Allocation;
using ComputeSharp.Win32;

namespace ComputeSharp.D3D12MemoryAllocator.Interop;

/// <summary>
/// The method table for <see cref="ID3D12MemoryAllocator"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ID3D12MemoryAllocatorImplVftbl
{
    public delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorImpl*, Guid*, void**, int> QueryInterface;
    public delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorImpl*, uint> AddRef;
    public delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorImpl*, uint> Release;
    public delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorImpl*, D3D12_RESOURCE_DESC*, D3D12_HEAP_TYPE, D3D12_RESOURCE_STATES, BOOL, ID3D12Allocation**, int> AllocateResource;
}