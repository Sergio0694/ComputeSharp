using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.__Internals;
using TerraFX.Interop;
using System.Runtime.InteropServices;

#pragma warning disable CS0618

namespace ComputeSharp.Shaders.Dispatching;

/// <summary>
/// A data loader for pixel shaders.
/// </summary>
internal readonly unsafe struct PixelShaderDispatchDataLoader : IDispatchDataLoader
{
    /// <summary>
    /// The <see cref="ID3D12GraphicsCommandList"/> object in use.
    /// </summary>
    private readonly ID3D12GraphicsCommandList* d3D12GraphicsCommandList;

    /// <summary>
    /// Creates a new <see cref="PixelShaderDispatchDataLoader"/> instance.
    /// </summary>
    /// <param name="d3D12GraphicsCommandList">The <see cref="ID3D12GraphicsCommandList"/> object to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal PixelShaderDispatchDataLoader(ID3D12GraphicsCommandList* d3D12GraphicsCommandList)
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
        uint offset = 2;

        // Skip the first target resource, as that is loaded explicitly by the shader runner using the
        // target texture passed in by the user, which is not actually captured by the shader itself.
        // The target root descriptor table index is therefore 2 in this case, as it needs to skip not
        // only the this implicit texture to draw on, but also the constant buffer with capture values.
        while (Unsafe.IsAddressLessThan(ref startRef, ref endRef))
        {
            d3D12GraphicsCommandList->SetComputeRootDescriptorTable(offset, startRef);

            startRef = ref Unsafe.Add(ref startRef, 1);
            offset++;
        }
    }
}
