using System;
using CommunityToolkit.Diagnostics;
using ComputeSharp.Graphics.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_DESCRIPTOR_HEAP_TYPE;
#if !NET6_0_OR_GREATER
using GC = ComputeSharp.NetStandard.System.GC;
#endif

namespace ComputeSharp.Graphics.Commands.Interop;

/// <summary>
/// A type that provides logic to create resource descriptors for a <see cref="GraphicsDevice"/> instance.
/// </summary>
internal unsafe struct ID3D12DescriptorHandleAllocator : IDisposable
{
    /// <summary>
    /// The default number of available descriptors per heap.
    /// </summary>
    private const uint DescriptorsPerHeap = 4096;

    /// <summary>
    /// The <see cref="ID3D12DescriptorHeap"/> in use for the current <see cref="ID3D12DescriptorHandleAllocator"/> instance.
    /// </summary>
    private ComPtr<ID3D12DescriptorHeap> d3D12DescriptorHeap;

    /// <summary>
    /// The non shader visible <see cref="ID3D12DescriptorHeap"/> in use for the current <see cref="ID3D12DescriptorHandleAllocator"/> instance.
    /// </summary>
    private ComPtr<ID3D12DescriptorHeap> d3D12DescriptorHeapNonShaderVisible;

    /// <summary>
    /// The array of <see cref="ID3D12ResourceDescriptorHandles"/> items with the available descriptor handles.
    /// </summary>
    private readonly ID3D12ResourceDescriptorHandles[] d3D12DescriptorHandlePairs;

    /// <summary>
    /// The head index for the queue.
    /// </summary>
    private int head;

    /// <summary>
    /// The tail index for the queue.
    /// </summary>
    private int tail;

    /// <summary>
    /// The current size of the queue (ie. the number of remaining pairs in <see cref="d3D12DescriptorHandlePairs"/>).
    /// </summary>
    private int size;

    /// <summary>
    /// Creates a new <see cref="ID3D12DescriptorHandleAllocator"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="ID3D12Device"/> instance to use.</param>
    public ID3D12DescriptorHandleAllocator(ID3D12Device* device)
    {
        this.d3D12DescriptorHeap = device->CreateDescriptorHeap(DescriptorsPerHeap, isShaderVisible: true);
        this.d3D12DescriptorHeapNonShaderVisible = device->CreateDescriptorHeap(DescriptorsPerHeap, isShaderVisible: false);
        this.d3D12DescriptorHandlePairs = GC.AllocateUninitializedArray<ID3D12ResourceDescriptorHandles>((int)DescriptorsPerHeap);
        this.head = 0;
        this.tail = 0;
        this.size = (int)DescriptorsPerHeap;

        D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandleStart = this.d3D12DescriptorHeap.Get()->GetCPUDescriptorHandleForHeapStart();
        D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandleStart = this.d3D12DescriptorHeap.Get()->GetGPUDescriptorHandleForHeapStart();
        D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandleNonShaderVisibleStart = this.d3D12DescriptorHeapNonShaderVisible.Get()->GetCPUDescriptorHandleForHeapStart();
        uint descriptorIncrementSize = device->GetDescriptorHandleIncrementSize(D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV);

        for (int i = 0; i < this.d3D12DescriptorHandlePairs.Length; i++)
        {
            D3D12_CPU_DESCRIPTOR_HANDLE.InitOffsetted(out D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle, in d3D12CpuDescriptorHandleStart, i, descriptorIncrementSize);
            D3D12_GPU_DESCRIPTOR_HANDLE.InitOffsetted(out D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle, in d3D12GpuDescriptorHandleStart, i, descriptorIncrementSize);
            D3D12_CPU_DESCRIPTOR_HANDLE.InitOffsetted(out D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandleNonShaderVisible, in d3D12CpuDescriptorHandleNonShaderVisibleStart, i, descriptorIncrementSize);

            this.d3D12DescriptorHandlePairs[i] = new ID3D12ResourceDescriptorHandles(
                d3D12CpuDescriptorHandle,
                d3D12GpuDescriptorHandle,
                d3D12CpuDescriptorHandleNonShaderVisible);
        }
    }

    /// <summary>
    /// Gets the <see cref="ID3D12DescriptorHeap"/> in use for the current <see cref="ID3D12DescriptorHandleAllocator"/> instance.
    /// </summary>
    public ID3D12DescriptorHeap* D3D12DescriptorHeap => this.d3D12DescriptorHeap;

    /// <summary>
    /// Rents a new bundle of CPU and GPU handles to use in a resource.
    /// </summary>
    /// <param name="d3D12ResourceDescriptorHandles">The resulting <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> value.</param>
    public void Rent(out ID3D12ResourceDescriptorHandles d3D12ResourceDescriptorHandles)
    {
        lock (this.d3D12DescriptorHandlePairs)
        {
            if (this.size <= 0)
            {
                ThrowHelper.ThrowInvalidOperationException("There are no descriptor handles left in the pool.");
            }

            d3D12ResourceDescriptorHandles = this.d3D12DescriptorHandlePairs[this.head++];

            if (this.head == DescriptorsPerHeap)
            {
                this.head = 0;
            }

            this.size--;
        }
    }

    /// <summary>
    /// Returns a CPU and GPU handle pair for later use.
    /// </summary>
    /// <param name="d3D12ResourceDescriptorHandles">The returned <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> value.</param>
    public void Return(in ID3D12ResourceDescriptorHandles d3D12ResourceDescriptorHandles)
    {
        lock (this.d3D12DescriptorHandlePairs)
        {
            if (this.size >= DescriptorsPerHeap)
            {
                ThrowHelper.ThrowInvalidOperationException("The pool of descriptor heaps is already full.");
            }

            this.d3D12DescriptorHandlePairs[this.tail++] = d3D12ResourceDescriptorHandles;

            if (this.tail == DescriptorsPerHeap)
            {
                this.tail = 0;
            }

            this.size++;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.d3D12DescriptorHeap.Dispose();
        this.d3D12DescriptorHeapNonShaderVisible.Dispose();
    }
}