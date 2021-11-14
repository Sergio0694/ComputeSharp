using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.__Internals;
using TerraFX.Interop.DirectX;
using System.Runtime.InteropServices;

#pragma warning disable CS0618

namespace ComputeSharp.Shaders.Dispatching;

/// <summary>
/// A data loader for compute shaders.
/// </summary>
internal readonly unsafe struct ComputeShaderDispatchDataLoader : IDispatchDataLoader
{
    /// <summary>
    /// The <see cref="ID3D12GraphicsCommandList"/> object in use.
    /// </summary>
    private readonly ID3D12GraphicsCommandList* d3D12GraphicsCommandList;

    /// <summary>
    /// Creates a new <see cref="ComputeShaderDispatchDataLoader"/> instance.
    /// </summary>
    /// <param name="d3D12GraphicsCommandList">The <see cref="ID3D12GraphicsCommandList"/> object to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ComputeShaderDispatchDataLoader(ID3D12GraphicsCommandList* d3D12GraphicsCommandList)
    {
        this.d3D12GraphicsCommandList = d3D12GraphicsCommandList;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void LoadCapturedValues(ReadOnlySpan<uint> data)
    {
        this.d3D12GraphicsCommandList->SetComputeRoot32BitConstants(data);
    }

    /// <inheritdoc/>
    public void LoadCapturedResources(ReadOnlySpan<ulong> data)
    {
        ID3D12GraphicsCommandList* d3D12GraphicsCommandList = this.d3D12GraphicsCommandList;
        ref ulong resourcesRef = ref MemoryMarshal.GetReference(data);
        ref D3D12_GPU_DESCRIPTOR_HANDLE startRef = ref Unsafe.As<ulong, D3D12_GPU_DESCRIPTOR_HANDLE>(ref resourcesRef);
        ref D3D12_GPU_DESCRIPTOR_HANDLE endRef = ref Unsafe.Add(ref startRef, data.Length);
        uint offset = 1;

        while (Unsafe.IsAddressLessThan(ref startRef, ref endRef))
        {
            d3D12GraphicsCommandList->SetComputeRootDescriptorTable(offset, startRef);

            startRef = ref Unsafe.Add(ref startRef, 1);
            offset++;
        }
    }
}
