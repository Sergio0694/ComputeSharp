using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Shaders.Extensions;
using ComputeSharp.__Internals;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.Shaders.Dispatching;

/// <summary>
/// A data loader for compute shaders.
/// </summary>
internal readonly unsafe struct ShaderDispatchMetadataLoader : IDispatchMetadataLoader
{
    /// <summary>
    /// The <see cref="ID3D12Device"/> object in use.
    /// </summary>
    private readonly ID3D12Device* d3D12Device;

    /// <summary>
    /// Creates a new <see cref="ShaderDispatchMetadataLoader"/> instance.
    /// </summary>
    /// <param name="d3D12Device">The <see cref="ID3D12Device"/> object to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ShaderDispatchMetadataLoader(ID3D12Device* d3D12Device)
    {
        this.d3D12Device = d3D12Device;
    }

    /// <inheritdoc/>
    public unsafe void LoadMetadataHandle(ReadOnlySpan<byte> serializedMetadata, ReadOnlySpan<ResourceDescriptor> resourceDescriptors, out IntPtr result)
    {
        Guard.HasSizeEqualTo(serializedMetadata, 5, nameof(serializedMetadata));

        fixed (IntPtr* p = &result)
        {
            *(ComPtr<ID3D12RootSignature>*)p = this.d3D12Device->CreateRootSignature(
                MemoryMarshal.Read<int>(serializedMetadata),
                MemoryMarshal.Cast<ResourceDescriptor, D3D12_DESCRIPTOR_RANGE1>(resourceDescriptors),
                MemoryMarshal.Read<bool>(serializedMetadata.Slice(4)));
        }
    }
}
