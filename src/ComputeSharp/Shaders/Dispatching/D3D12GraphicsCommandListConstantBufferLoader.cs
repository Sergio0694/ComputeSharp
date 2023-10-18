using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Descriptors;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Win32;

namespace ComputeSharp.Shaders.Dispatching;

/// <summary>
/// An <see cref="IConstantBufferLoader"/> implementation for compute shaders dispatched via <see cref="ID3D12GraphicsCommandList"/>.
/// </summary>
internal readonly unsafe struct D3D12GraphicsCommandListConstantBufferLoader : IConstantBufferLoader
{
    /// <summary>
    /// The <see cref="ID3D12GraphicsCommandList"/> object in use.
    /// </summary>
    private readonly ID3D12GraphicsCommandList* d3D12GraphicsCommandList;

    /// <summary>
    /// Creates a new <see cref="D3D12GraphicsCommandListConstantBufferLoader"/> instance.
    /// </summary>
    /// <param name="d3D12GraphicsCommandList">The <see cref="ID3D12GraphicsCommandList"/> object to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal D3D12GraphicsCommandListConstantBufferLoader(ID3D12GraphicsCommandList* d3D12GraphicsCommandList)
    {
        this.d3D12GraphicsCommandList = d3D12GraphicsCommandList;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void LoadConstantBuffer(ReadOnlySpan<byte> data)
    {
        default(ArgumentException).ThrowIf((data.Length & 3) != 0, nameof(data));

        this.d3D12GraphicsCommandList->SetComputeRoot32BitConstants(data);
    }
}