using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Win32;

namespace ComputeSharp.Graphics.Commands.Interop;

/// <summary>
/// A type that acts as a pool to get new <see cref="ID3D12CommandList"/> instances wheen needed.
/// </summary>
/// <param name="d3D12CommandListType">The command list type to use.</param>
internal readonly unsafe struct ID3D12CommandListPool(D3D12_COMMAND_LIST_TYPE d3D12CommandListType) : IDisposable
{
    /// <summary>
    /// The queue of <see cref="D3D12CommandListBundle"/> items with the available command lists.
    /// </summary>
    private readonly Queue<D3D12CommandListBundle> d3D12CommandListBundleQueue = [];

    /// <summary>
    /// Rents a <see cref="ID3D12GraphicsCommandList"/> and <see cref="ID3D12CommandAllocator"/> pair.
    /// </summary>
    /// <param name="d3D12Device">The <see cref="ID3D12Device"/> renting the command list.</param>
    /// <param name="d3D12PipelineState">The <see cref="ID3D12PipelineState"/> instance to use for the new command list.</param>
    /// <param name="d3D12CommandList">The resulting <see cref="ID3D12GraphicsCommandList"/> value.</param>
    /// <param name="d3D12CommandAllocator">The resulting <see cref="ID3D12CommandAllocator"/> value.</param>
    public void Rent(ID3D12Device* d3D12Device, ID3D12PipelineState* d3D12PipelineState, out ID3D12GraphicsCommandList* d3D12CommandList, out ID3D12CommandAllocator* d3D12CommandAllocator)
    {
        lock (this.d3D12CommandListBundleQueue)
        {
            if (this.d3D12CommandListBundleQueue.TryDequeue(out D3D12CommandListBundle d3D12CommandListBundle))
            {
                d3D12CommandList = d3D12CommandListBundle.D3D12CommandList;
                d3D12CommandAllocator = d3D12CommandListBundle.D3D12CommandAllocator;
            }
            else
            {
                d3D12CommandAllocator = null;
                d3D12CommandList = null;
            }
        }

        // Reset the command allocator and command list outside of the lock, or create a new pair if one to be reused
        // wasn't available. These operations are relatively expensive, so doing so here reduces thread contention
        // when multiple shader executions are being dispatched in parallel on the same device.
        if (d3D12CommandAllocator is not null)
        {
            d3D12CommandAllocator->Reset().Assert();
            d3D12CommandList->Reset(d3D12CommandAllocator, d3D12PipelineState).Assert();
        }
        else
        {
            CreateCommandListAndAllocator(d3D12Device, d3D12PipelineState, out d3D12CommandList, out d3D12CommandAllocator);
        }
    }

    /// <summary>
    /// Returns a <see cref="ID3D12GraphicsCommandList"/> and <see cref="ID3D12CommandAllocator"/> pair.
    /// </summary>
    /// <param name="d3D12CommandList">The returned <see cref="ID3D12GraphicsCommandList"/> value.</param>
    /// <param name="d3D12CommandAllocator">The returned <see cref="ID3D12CommandAllocator"/> value.</param>
    public void Return(ID3D12GraphicsCommandList* d3D12CommandList, ID3D12CommandAllocator* d3D12CommandAllocator)
    {
        lock (this.d3D12CommandListBundleQueue)
        {
            this.d3D12CommandListBundleQueue.Enqueue(new D3D12CommandListBundle(d3D12CommandList, d3D12CommandAllocator));
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        lock (this.d3D12CommandListBundleQueue)
        {
            foreach (D3D12CommandListBundle d3D12CommandListBundle in this.d3D12CommandListBundleQueue)
            {
                _ = d3D12CommandListBundle.D3D12CommandList->Release();
                _ = d3D12CommandListBundle.D3D12CommandAllocator->Release();
            }

            this.d3D12CommandListBundleQueue.Clear();
        }
    }

    /// <summary>
    /// Creates a new <see cref="ID3D12CommandList"/> and <see cref="ID3D12CommandAllocator"/> pair.
    /// </summary>
    /// <param name="d3D12Device">The <see cref="ID3D12Device"/> renting the command list.</param>
    /// <param name="d3D12PipelineState">The <see cref="ID3D12PipelineState"/> instance to use for the new command list.</param>
    /// <param name="d3D12CommandList">The resulting <see cref="ID3D12GraphicsCommandList"/> value.</param>
    /// <param name="d3D12CommandAllocator">The resulting <see cref="ID3D12CommandAllocator"/> value.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void CreateCommandListAndAllocator(ID3D12Device* d3D12Device, ID3D12PipelineState* d3D12PipelineState, out ID3D12GraphicsCommandList* d3D12CommandList, out ID3D12CommandAllocator* d3D12CommandAllocator)
    {
        using ComPtr<ID3D12CommandAllocator> d3D12CommandAllocatorComPtr = d3D12Device->CreateCommandAllocator(d3D12CommandListType);
        using ComPtr<ID3D12GraphicsCommandList> d3D12CommandListComPtr = d3D12Device->CreateCommandList(d3D12CommandListType, d3D12CommandAllocatorComPtr.Get(), d3D12PipelineState);

        fixed (ID3D12GraphicsCommandList** d3D12CommandListPtr = &d3D12CommandList)
        fixed (ID3D12CommandAllocator** d3D12CommandAllocatorPtr = &d3D12CommandAllocator)
        {
            _ = d3D12CommandListComPtr.CopyTo(d3D12CommandListPtr);
            _ = d3D12CommandAllocatorComPtr.CopyTo(d3D12CommandAllocatorPtr);
        }
    }

    /// <summary>
    /// A type representing a bundle of a cached command list and related allocator.
    /// </summary>
    /// <param name="d3D12CommandList">The <see cref="ID3D12GraphicsCommandList"/> value to wrap.</param>
    /// <param name="d3D12CommandAllocator">The <see cref="ID3D12CommandAllocator"/> value to wrap.</param>
    private readonly struct D3D12CommandListBundle(ID3D12GraphicsCommandList* d3D12CommandList, ID3D12CommandAllocator* d3D12CommandAllocator)
    {
        /// <summary>
        /// The <see cref="ID3D12GraphicsCommandList"/> value for the current entry.
        /// </summary>
        public readonly ID3D12GraphicsCommandList* D3D12CommandList = d3D12CommandList;

        /// <summary>
        /// The <see cref="ID3D12CommandAllocator"/> value for the current entry.
        /// </summary>
        public readonly ID3D12CommandAllocator* D3D12CommandAllocator = d3D12CommandAllocator;
    }
}