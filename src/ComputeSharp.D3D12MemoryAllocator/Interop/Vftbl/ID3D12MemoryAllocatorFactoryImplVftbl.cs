using System;
using System.Runtime.InteropServices;
using ComputeSharp.Interop.Allocation;
using ComputeSharp.Win32;

namespace ComputeSharp.D3D12MemoryAllocator.Interop;

/// <summary>
/// The method table for <see cref="ID3D12MemoryAllocatorFactory"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ID3D12MemoryAllocatorFactoryImplVftbl
{
    public delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorFactoryImpl*, Guid*, void**, int> QueryInterface;
    public delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorFactoryImpl*, uint> AddRef;
    public delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorFactoryImpl*, uint> Release;
    public delegate* unmanaged[MemberFunction]<ID3D12MemoryAllocatorFactoryImpl*, ID3D12Device*, IDXGIAdapter*, ID3D12MemoryAllocator**, int> CreateAllocator;
}