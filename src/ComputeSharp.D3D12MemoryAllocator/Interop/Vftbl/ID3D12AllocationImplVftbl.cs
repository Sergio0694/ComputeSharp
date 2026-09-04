using System;
using System.Runtime.InteropServices;
using ComputeSharp.Interop.Allocation;
using ComputeSharp.Win32;

namespace ComputeSharp.D3D12MemoryAllocator.Interop;

/// <summary>
/// The method table for <see cref="ID3D12Allocation"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ID3D12AllocationImplVftbl
{
    public delegate* unmanaged[MemberFunction]<ID3D12AllocationImpl*, Guid*, void**, int> QueryInterface;
    public delegate* unmanaged[MemberFunction]<ID3D12AllocationImpl*, uint> AddRef;
    public delegate* unmanaged[MemberFunction]<ID3D12AllocationImpl*, uint> Release;
    public delegate* unmanaged[MemberFunction]<ID3D12AllocationImpl*, ID3D12Resource**, int> GetD3D12Resource;
}