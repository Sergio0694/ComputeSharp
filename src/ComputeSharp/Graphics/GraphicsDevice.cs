using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Commands.Interop;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_COMMAND_LIST_TYPE;
using static TerraFX.Interop.DirectX.D3D12_FEATURE;
using static TerraFX.Interop.DirectX.D3D12_FORMAT_SUPPORT1;
using static TerraFX.Interop.DirectX.DXGI_ADAPTER_FLAG;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that represents an <see cref="ID3D12Device"/> instance that can be used to run compute shaders.
/// </summary>
[DebuggerDisplay("{ToString(),raw}")]
public sealed unsafe class GraphicsDevice : NativeObject
{
    /// <summary>
    /// The underlying <see cref="ID3D12Device"/> wrapped by the current instance.
    /// </summary>
    private ComPtr<ID3D12Device> d3D12Device;

    /// <summary>
    /// The <see cref="ID3D12CommandQueue"/> instance to use for compute operations.
    /// </summary>
    private ComPtr<ID3D12CommandQueue> d3D12ComputeCommandQueue;

    /// <summary>
    /// The <see cref="ID3D12CommandQueue"/> instance to use for copy operations.
    /// </summary>
    private ComPtr<ID3D12CommandQueue> d3D12CopyCommandQueue;

    /// <summary>
    /// The <see cref="ID3D12Fence"/> instance used for compute operations.
    /// </summary>
    private ComPtr<ID3D12Fence> d3D12ComputeFence;

    /// <summary>
    /// The <see cref="ID3D12Fence"/> instance used for copy operations.
    /// </summary>
    private ComPtr<ID3D12Fence> d3D12CopyFence;

    /// <summary>
    /// The <see cref="ID3D12DescriptorHandleAllocator"/> instance to use when allocating new buffers.
    /// </summary>
    private ID3D12DescriptorHandleAllocator shaderResourceViewDescriptorAllocator;

    /// <summary>
    /// The <see cref="ID3D12CommandListPool"/> instance for compute operations.
    /// </summary>
    private readonly ID3D12CommandListPool computeCommandListPool;

    /// <summary>
    /// Gets the <see cref="ID3D12CommandListPool"/> instance for copy operations.
    /// </summary>
    private readonly ID3D12CommandListPool copyCommandListPool;

    /// <summary>
    /// The next fence value for compute operations using <see cref="d3D12ComputeCommandQueue"/>.
    /// </summary>
    private ulong nextD3D12ComputeFenceValue;

    /// <summary>
    /// The next fence value for copy operations using <see cref="d3D12CopyCommandQueue"/>.
    /// </summary>
    private ulong nextD3D12CopyFenceValue;

#if NET6_0_OR_GREATER
    /// <summary>
    /// The <see cref="D3D12MA_Allocator"/> in use associated to the current device.
    /// </summary>
    private ComPtr<D3D12MA_Allocator> allocator;

    /// <summary>
    /// The <see cref="D3D12MA_Pool"/> instance in use, if <see cref="IsCacheCoherentUMA"/> is <see langword="true"/>.
    /// </summary>
    private ComPtr<D3D12MA_Pool> pool;
#endif

    /// <summary>
    /// Creates a new <see cref="GraphicsDevice"/> instance for the input <see cref="ID3D12Device"/>.
    /// </summary>
    /// <param name="d3D12Device">The <see cref="ID3D12Device"/> to use for the new <see cref="GraphicsDevice"/> instance.</param>
    /// <param name="dxgiAdapter">The <see cref="IDXGIAdapter"/> that <paramref name="d3D12Device"/> was created from.</param>
    /// <param name="dxgiDescription1">The available info for the new <see cref="GraphicsDevice"/> instance.</param>
    internal GraphicsDevice(ID3D12Device* d3D12Device, IDXGIAdapter* dxgiAdapter, DXGI_ADAPTER_DESC1* dxgiDescription1)
    {
        this.d3D12Device = new ComPtr<ID3D12Device>(d3D12Device);

        this.d3D12ComputeCommandQueue = d3D12Device->CreateCommandQueue(D3D12_COMMAND_LIST_TYPE_COMPUTE);
        this.d3D12CopyCommandQueue = d3D12Device->CreateCommandQueue(D3D12_COMMAND_LIST_TYPE_COPY);
        this.d3D12ComputeFence = d3D12Device->CreateFence();
        this.d3D12CopyFence = d3D12Device->CreateFence();

        this.shaderResourceViewDescriptorAllocator = new ID3D12DescriptorHandleAllocator(d3D12Device);
        this.computeCommandListPool = new ID3D12CommandListPool(D3D12_COMMAND_LIST_TYPE_COMPUTE);
        this.copyCommandListPool = new ID3D12CommandListPool(D3D12_COMMAND_LIST_TYPE_COPY);

        Luid = Luid.FromLUID(dxgiDescription1->AdapterLuid);
        Name = new string((char*)dxgiDescription1->Description);
        DedicatedMemorySize = dxgiDescription1->DedicatedVideoMemory;
        SharedMemorySize = dxgiDescription1->SharedSystemMemory;
        IsHardwareAccelerated = (dxgiDescription1->Flags & (uint)DXGI_ADAPTER_FLAG_SOFTWARE) == 0;

        var d3D12Options1Data = d3D12Device->CheckFeatureSupport<D3D12_FEATURE_DATA_D3D12_OPTIONS1>(D3D12_FEATURE_D3D12_OPTIONS1);

        ComputeUnits = d3D12Options1Data.TotalLaneCount;
        WavefrontSize = d3D12Options1Data.WaveLaneCountMin;

        var d3D12Architecture1Data = d3D12Device->CheckFeatureSupport<D3D12_FEATURE_DATA_ARCHITECTURE1>(D3D12_FEATURE_ARCHITECTURE1);

        IsCacheCoherentUMA = d3D12Architecture1Data.CacheCoherentUMA != 0;

#if NET6_0_OR_GREATER
        this.allocator = d3D12Device->CreateAllocator(dxgiAdapter);

        if (IsCacheCoherentUMA)
        {
            this.pool = this.allocator.Get()->CreatePoolForCacheCoherentUMA();
        }
#endif
    }

    /// <summary>
    /// Gets the locally unique identifier for the current device.
    /// </summary>
    public Luid Luid { get; }

    /// <summary>
    /// Gets the name of the current <see cref="GraphicsDevice"/> instance.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the size of the dedicated memory for the current device.
    /// </summary>
    public nuint DedicatedMemorySize { get; }

    /// <summary>
    /// Gets the size of the shared system memory for the current device.
    /// </summary>
    public nuint SharedMemorySize { get; }

    /// <summary>
    /// Gets whether or not the current device is hardware accelerated.
    /// This value is <see langword="false"/> for software fallback devices.
    /// </summary>
    public bool IsHardwareAccelerated { get; }

    /// <summary>
    /// Gets the number of total lanes on the current device (eg. CUDA cores on an nVidia GPU).
    /// </summary>
    public uint ComputeUnits { get; }

    /// <summary>
    /// Gets the number of lanes in a SIMD wave on the current device (also known as "wavefront size" or "warp width").
    /// </summary>
    public uint WavefrontSize { get; }

    /// <summary>
    /// Gets the underlying <see cref="ID3D12Device"/> wrapped by the current instance.
    /// </summary>
    internal ID3D12Device* D3D12Device => this.d3D12Device;

#if NET6_0_OR_GREATER
    /// <summary>
    /// Gets the underlying <see cref="D3D12MA_Allocator"/> wrapped by the current instance.
    /// </summary>
    internal D3D12MA_Allocator* Allocator => this.allocator;

    /// <summary>
    /// Gets the underlying <see cref="D3D12MA_Pool"/> wrapped by the current instance, if any.
    /// </summary>
    internal D3D12MA_Pool* Pool => this.pool;
#endif

    /// <summary>
    /// Gets whether or not the current device has a cache coherent UMA architecture.
    /// </summary>
    internal bool IsCacheCoherentUMA { get; }

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadOnlyTexture2D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadOnlyTexture2D{T}"/> instances can be created by the current device.</returns>
    [Pure]
    public bool IsReadOnlyTexture2DSupportedForType<T>()
        where T : unmanaged
    {
        ThrowIfDisposed();

        return this.d3D12Device.Get()->IsDxgiFormatSupported(DXGIFormatHelper.GetForType<T>(), D3D12_FORMAT_SUPPORT1_TEXTURE2D);
    }

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadWriteTexture2D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadWriteTexture2D{T}"/> instances can be created by the current device.</returns>
    [Pure]
    public bool IsReadWriteTexture2DSupportedForType<T>()
        where T : unmanaged
    {
        ThrowIfDisposed();

        return this.d3D12Device.Get()->IsDxgiFormatSupported(
            DXGIFormatHelper.GetForType<T>(),
            D3D12_FORMAT_SUPPORT1_TEXTURE2D | D3D12_FORMAT_SUPPORT1_TYPED_UNORDERED_ACCESS_VIEW);
    }

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadOnlyTexture3D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadOnlyTexture3D{T}"/> instances can be created by the current device.</returns>
    [Pure]
    public bool IsReadOnlyTexture3DSupportedForType<T>()
        where T : unmanaged
    {
        ThrowIfDisposed();

        return this.d3D12Device.Get()->IsDxgiFormatSupported(DXGIFormatHelper.GetForType<T>(), D3D12_FORMAT_SUPPORT1_TEXTURE3D);
    }

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadWriteTexture3D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadWriteTexture3D{T}"/> instances can be created by the current device.</returns>
    [Pure]
    public bool IsReadWriteTexture3DSupportedForType<T>()
        where T : unmanaged
    {
        ThrowIfDisposed();

        return this.d3D12Device.Get()->IsDxgiFormatSupported(
            DXGIFormatHelper.GetForType<T>(),
            D3D12_FORMAT_SUPPORT1_TEXTURE3D | D3D12_FORMAT_SUPPORT1_TYPED_UNORDERED_ACCESS_VIEW);
    }

    /// <summary>
    /// Registers that a new resource has been allocated on the current device.
    /// </summary>
    internal void RegisterAllocatedResource()
    {
#if NET6_0_OR_GREATER
        this.pool.AddRef();
        this.allocator.AddRef();
#endif
    }

    /// <summary>
    /// Unregisters a generic resource that was allocated on the current device.
    /// </summary>
    internal void UnregisterAllocatedResource()
    {
#if NET6_0_OR_GREATER
        this.pool.Release();
        this.allocator.Release();
#endif
    }

    /// <inheritdoc cref="ID3D12DescriptorHandleAllocator.Rent"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void RentShaderResourceViewDescriptorHandles(
        out D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle,
        out D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle)
    {
        this.shaderResourceViewDescriptorAllocator.Rent(out d3D12CpuDescriptorHandle, out d3D12GpuDescriptorHandle);
    }

    /// <inheritdoc cref="ID3D12DescriptorHandleAllocator.Return"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void ReturnShaderResourceViewDescriptorHandles(
        D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle,
        D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle)
    {
        this.shaderResourceViewDescriptorAllocator.Return(d3D12CpuDescriptorHandle, d3D12GpuDescriptorHandle);
    }

    /// <inheritdoc cref="ID3D12CommandListPool.Rent"/>
    /// <param name="d3D12CommandListType">The type of command allocator to rent.</param>
    /// <param name="d3D12CommandList">The resulting <see cref="ID3D12GraphicsCommandList"/> value.</param>
    /// <param name="d3D12CommandAllocator">The resulting <see cref="ID3D12CommandAllocator"/> value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void GetCommandListAndAllocator(
        D3D12_COMMAND_LIST_TYPE d3D12CommandListType,
        out ID3D12GraphicsCommandList* d3D12CommandList,
        out ID3D12CommandAllocator* d3D12CommandAllocator)
    {
        switch (d3D12CommandListType)
        {
            case D3D12_COMMAND_LIST_TYPE_COMPUTE:
                this.computeCommandListPool.Rent(this.d3D12Device.Get(), null, out d3D12CommandList, out d3D12CommandAllocator);
                break;
            case D3D12_COMMAND_LIST_TYPE_COPY:
                this.copyCommandListPool.Rent(this.d3D12Device.Get(), null, out d3D12CommandList, out d3D12CommandAllocator);
                break;
            default:
                ThrowHelper.ThrowArgumentException<ComPtr<ID3D12CommandAllocator>>();
                d3D12CommandList = null;
                d3D12CommandAllocator = null;
                break;
        }
    }

    /// <inheritdoc cref="ID3D12CommandListPool.Rent"/>
    /// <param name="d3D12PipelineState">The <see cref="ID3D12PipelineState"/> instance to use for the new command list.</param>
    /// <param name="d3D12CommandList">The resulting <see cref="ID3D12GraphicsCommandList"/> value.</param>
    /// <param name="d3D12CommandAllocator">The resulting <see cref="ID3D12CommandAllocator"/> value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void GetCommandListAndAllocator(
        ID3D12PipelineState* d3D12PipelineState,
        out ID3D12GraphicsCommandList* d3D12CommandList,
        out ID3D12CommandAllocator* d3D12CommandAllocator)
    {
        this.computeCommandListPool.Rent(this.d3D12Device.Get(), d3D12PipelineState, out d3D12CommandList, out d3D12CommandAllocator);
    }

    /// <summary>
    /// Sets the descriptor heap for a given <see cref="ID3D12GraphicsCommandList"/> instance.
    /// </summary>
    /// <param name="d3D12GraphicsCommandList">The input <see cref="ID3D12GraphicsCommandList"/> instance to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void SetDescriptorHeapForCommandList(ID3D12GraphicsCommandList* d3D12GraphicsCommandList)
    {
        ID3D12DescriptorHeap* d3D12DescriptorHeap = this.shaderResourceViewDescriptorAllocator.D3D12DescriptorHeap;

        d3D12GraphicsCommandList->SetDescriptorHeaps(1, &d3D12DescriptorHeap);
    }

    /// <summary>
    /// Executes a given command list and waits for the operation to be completed.
    /// </summary>
    /// <param name="commandList">The input <see cref="CommandList"/> to execute.</param>
    internal void ExecuteCommandList(ref CommandList commandList)
    {
        ref readonly ID3D12CommandListPool commandListPool = ref Unsafe.NullRef<ID3D12CommandListPool>();
        ID3D12CommandQueue* d3D12CommandQueue;
        ID3D12Fence* d3D12Fence;
        ref ulong d3D12FenceValue = ref Unsafe.NullRef<ulong>();

        // Get the target command queue, fence and pool for the list type
        switch (commandList.D3D12CommandListType)
        {
            case D3D12_COMMAND_LIST_TYPE_COMPUTE:
                commandListPool = ref this.computeCommandListPool;
                d3D12CommandQueue = this.d3D12ComputeCommandQueue;
                d3D12Fence = this.d3D12ComputeFence;
                d3D12FenceValue = ref this.nextD3D12ComputeFenceValue;
                break;
            case D3D12_COMMAND_LIST_TYPE_COPY:
                commandListPool = ref this.copyCommandListPool;
                d3D12CommandQueue = this.d3D12CopyCommandQueue;
                d3D12Fence = this.d3D12CopyFence;
                d3D12FenceValue = ref this.nextD3D12CopyFenceValue;
                break;
            default: ThrowHelper.ThrowArgumentException(); return;
        }

        // Execute the command list
        d3D12CommandQueue->ExecuteCommandLists(1, commandList.GetD3D12CommandListAddressOf());

#if NET6_0_OR_GREATER
        ulong updatedFenceValue = Interlocked.Increment(ref d3D12FenceValue);
#else
        ulong updatedFenceValue = (ulong)Interlocked.Increment(ref Unsafe.As<ulong, long>(ref d3D12FenceValue));
#endif

        // Signal to the target fence with the updated value. Note that incrementing the
        // target fence value to signal must be done after executing the command list.
        d3D12CommandQueue->Signal(d3D12Fence, updatedFenceValue).Assert();

        // If the fence value hasn't been reached, wait until the operation completes
        if (updatedFenceValue > d3D12Fence->GetCompletedValue())
        {
            d3D12Fence->SetEventOnCompletion(updatedFenceValue, default).Assert();
        }

        // Return the rented command list and command allocator so that they can be reused
        commandListPool.Return(commandList.DetachD3D12CommandList(), commandList.DetachD3D12CommandAllocator());
    }

    /// <inheritdoc/>
    protected override void OnDispose()
    {
        DeviceHelper.NotifyDisposedDevice(this);

        this.d3D12Device.Dispose();
        this.d3D12ComputeCommandQueue.Dispose();
        this.d3D12CopyCommandQueue.Dispose();
        this.d3D12ComputeFence.Dispose();
        this.d3D12CopyFence.Dispose();
        this.computeCommandListPool.Dispose();
        this.copyCommandListPool.Dispose();
        this.shaderResourceViewDescriptorAllocator.Dispose();
#if NET6_0_OR_GREATER
        this.pool.Dispose();
        this.allocator.Dispose();
#endif
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[{Luid}] {Name}";
    }
}
