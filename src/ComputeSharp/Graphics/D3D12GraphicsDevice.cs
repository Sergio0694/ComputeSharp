using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Commands.Interop;
#if NET6_0_OR_GREATER
using ComputeSharp.Core.Extensions;
#endif
using ComputeSharp.Shaders.Dispatching;
using ComputeSharp.Shaders.Extensions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop;
using ComputeSharp.Shaders.Models;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_COMMAND_LIST_TYPE;
using static TerraFX.Interop.DirectX.D3D12_FEATURE;
using static TerraFX.Interop.DirectX.D3D12_FORMAT_SUPPORT1;
using static TerraFX.Interop.DirectX.DXGI_ADAPTER_FLAG;

#pragma warning disable CS0618

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that represents an <see cref="ID3D12Device"/> instance that can be used to run compute shaders.
/// </summary>
[DebuggerDisplay("{ToString(),raw}")]
public sealed unsafe partial class D3D12GraphicsDevice : GraphicsDevice, IReferenceTrackedObject
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
    /// A weak <see cref="GCHandle"/> to the current instance (used to support the device lost callback).
    /// </summary>
    private GCHandle deviceHandle;

    /// <summary>
    /// The event for the device removed callback.
    /// </summary>
    private readonly HANDLE deviceRemovedEvent;

    /// <summary>
    /// The wait handle for the device removed callback.
    /// </summary>
    private readonly HANDLE deviceRemovedWaitHandle;

    /// <summary>
    /// The reason the device was removed, if any.
    /// </summary>
    private HRESULT deviceRemovedReason;

    /// <summary>
    /// The list of cached <see cref="PipelineData"/> objects for the current device.
    /// </summary>
    /// <remarks>
    /// A new entry is added to this list every time a shader is dispatched using this device. These cached items
    /// are stored via a <see cref="ConditionalWeakTable{TKey, TValue}"/>, meaning they will be collected automatically
    /// when the device is collected. But, due to the fact that could happen at any time, a list is required to guarantee
    /// the additional references added by the native objects in the pipeline model can be released immediately.
    /// </remarks>
    private readonly List<PipelineData> cachedPipelineData;

    /// <summary>
    /// Raised whenever the device is lost.
    /// </summary>
    /// <remarks>
    /// <para>
    /// "Device lost" refers to a situation where the GPU graphics device becomes unusable for further operations. This can occur
    /// due to GPU hardware malfunction, driver bugs, driver software updates, or switching the app from one GPU to another.
    /// </para>
    /// <para>
    /// A lost device can no longer be used, and any attempt to do so will throw an exception. To recover from
    /// this situation, the app must create a new device and then recreate all its graphics resources.
    /// </para>
    /// <para>
    /// This event will only be raised at most once for a given <see cref="GraphicsDevice"/> instance. Additionally,
    /// the event is raised asynchronously with respect to the device being lost, and on a thread pool thread.
    /// </para>
    /// </remarks>
    public event EventHandler<DeviceLostEventArgs>? DeviceLost;

    /// <summary>
    /// Creates a new <see cref="GraphicsDevice"/> instance for the input <see cref="ID3D12Device"/>.
    /// </summary>
    /// <param name="d3D12Device">The <see cref="ID3D12Device"/> to use for the new <see cref="GraphicsDevice"/> instance.</param>
    /// <param name="dxgiAdapter">The <see cref="IDXGIAdapter"/> that <paramref name="d3D12Device"/> was created from.</param>
    /// <param name="dxgiDescription1">The available info for the new <see cref="GraphicsDevice"/> instance.</param>
    internal D3D12GraphicsDevice(ID3D12Device* d3D12Device, IDXGIAdapter* dxgiAdapter, DXGI_ADAPTER_DESC1* dxgiDescription1)
    {
        this.referenceTracker = new ReferenceTracker(this);

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

        D3D12_FEATURE_DATA_D3D12_OPTIONS1 d3D12Options1Data = d3D12Device->CheckFeatureSupport<D3D12_FEATURE_DATA_D3D12_OPTIONS1>(D3D12_FEATURE_D3D12_OPTIONS1);

        ComputeUnits = d3D12Options1Data.TotalLaneCount;
        WavefrontSize = d3D12Options1Data.WaveLaneCountMin;

        D3D12_FEATURE_DATA_ARCHITECTURE1 d3D12Architecture1Data = d3D12Device->CheckFeatureSupport<D3D12_FEATURE_DATA_ARCHITECTURE1>(D3D12_FEATURE_ARCHITECTURE1);

        IsCacheCoherentUMA = d3D12Architecture1Data.CacheCoherentUMA != 0;

#if NET6_0_OR_GREATER
        this.allocator = d3D12Device->CreateAllocator(dxgiAdapter);

        if (IsCacheCoherentUMA)
        {
            this.pool = this.allocator.Get()->CreatePoolForCacheCoherentUMA();
        }
#endif

        this.deviceRemovedReason = S.S_OK;
        this.cachedPipelineData = new List<PipelineData>();

        RegisterDeviceLostCallback(this, out this.deviceHandle, out this.deviceRemovedEvent, out this.deviceRemovedWaitHandle);
    }

    internal override unsafe void CreatePipelineData<T>(ICachedShader shaderData, out PipelineData pipelineData)
    {
        using ComPtr<ID3D12RootSignature> d3D12RootSignature = default;

        ShaderDispatchMetadataLoader metadataLoader = new(this.D3D12Device);

        Unsafe.SkipInit(out T shader);

        shader.LoadDispatchMetadata(ref metadataLoader, out *(IntPtr*)&d3D12RootSignature);

        using ComPtr<ID3D12PipelineState> d3D12PipelineState = D3D12Device->CreateComputePipelineState(d3D12RootSignature.Get(), shaderData.D3D12ShaderBytecode);

        pipelineData = new D3D12PipelineData(d3D12RootSignature.Get(), d3D12PipelineState.Get());

        // Register the newly created pipeline data to enable early disposal
        RegisterPipelineData(pipelineData);
    }

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
    /// Checks whether the current device supports double precision floating point operations in shaders.
    /// </summary>
    /// <returns>Whether the current device supports double precision floating point operations in shaders.</returns>
    public override bool IsDoublePrecisionSupportAvailable()
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        ThrowIfDeviceLost();

        D3D12_FEATURE_DATA_D3D12_OPTIONS d3D12OptionsData = this.d3D12Device.Get()->CheckFeatureSupport<D3D12_FEATURE_DATA_D3D12_OPTIONS>(D3D12_FEATURE_D3D12_OPTIONS);

        return d3D12OptionsData.DoublePrecisionFloatShaderOps;
    }

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadOnlyTexture1D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadOnlyTexture1D{T}"/> instances can be created by the current device.</returns>
    public override bool IsReadOnlyTexture1DSupportedForType<T>()
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        ThrowIfDeviceLost();

        return this.d3D12Device.Get()->IsDxgiFormatSupported(DXGIFormatHelper.GetForType<T>(), D3D12_FORMAT_SUPPORT1_TEXTURE1D);
    }

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadWriteTexture1D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadWriteTexture1D{T}"/> instances can be created by the current device.</returns>
    public override bool IsReadWriteTexture1DSupportedForType<T>()
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        ThrowIfDeviceLost();

        return this.d3D12Device.Get()->IsDxgiFormatSupported(
            DXGIFormatHelper.GetForType<T>(),
            D3D12_FORMAT_SUPPORT1_TEXTURE1D | D3D12_FORMAT_SUPPORT1_TYPED_UNORDERED_ACCESS_VIEW);
    }

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadOnlyTexture2D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadOnlyTexture2D{T}"/> instances can be created by the current device.</returns>
    public override bool IsReadOnlyTexture2DSupportedForType<T>()
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        ThrowIfDeviceLost();

        return this.d3D12Device.Get()->IsDxgiFormatSupported(DXGIFormatHelper.GetForType<T>(), D3D12_FORMAT_SUPPORT1_TEXTURE2D);
    }

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadWriteTexture2D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadWriteTexture2D{T}"/> instances can be created by the current device.</returns>
    public override bool IsReadWriteTexture2DSupportedForType<T>()
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        ThrowIfDeviceLost();

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
    public override bool IsReadOnlyTexture3DSupportedForType<T>()
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        ThrowIfDeviceLost();

        return this.d3D12Device.Get()->IsDxgiFormatSupported(DXGIFormatHelper.GetForType<T>(), D3D12_FORMAT_SUPPORT1_TEXTURE3D);
    }

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadWriteTexture3D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadWriteTexture3D{T}"/> instances can be created by the current device.</returns>
    public override bool IsReadWriteTexture3DSupportedForType<T>()
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        ThrowIfDeviceLost();

        return this.d3D12Device.Get()->IsDxgiFormatSupported(
            DXGIFormatHelper.GetForType<T>(),
            D3D12_FORMAT_SUPPORT1_TEXTURE3D | D3D12_FORMAT_SUPPORT1_TYPED_UNORDERED_ACCESS_VIEW);
    }

    /// <summary>
    /// Registers a <see cref="PipelineData"/> object for the current device to enable early disposal.
    /// </summary>
    /// <param name="pipelineData">The <see cref="PipelineData"/> instance just loaded to run a shader.</param>
    internal void RegisterPipelineData(PipelineData pipelineData)
    {
        // This is only used when first initializing a pipeline data for a given shader.
        // Adding an item is very quick, so we can just use a lock and not a concurrent
        // list. That would've added more overhead given the low contention in this case.
        lock (this.cachedPipelineData)
        {
            this.cachedPipelineData.Add(pipelineData);
        }
    }

    /// <inheritdoc cref="ID3D12DescriptorHandleAllocator.Rent"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void RentShaderResourceViewDescriptorHandles(out ID3D12ResourceDescriptorHandles d3D12ResourceDescriptorHandles)
    {
        this.shaderResourceViewDescriptorAllocator.Rent(out d3D12ResourceDescriptorHandles);
    }

    /// <inheritdoc cref="ID3D12DescriptorHandleAllocator.Return"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void ReturnShaderResourceViewDescriptorHandles(in ID3D12ResourceDescriptorHandles d3D12ResourceDescriptorHandles)
    {
        this.shaderResourceViewDescriptorAllocator.Return(in d3D12ResourceDescriptorHandles);
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
                default(ArgumentException).Throw(nameof(d3D12CommandListType));
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

    /// <inheritdoc/>
    void IReferenceTrackedObject.DangerousOnDispose()
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

        // On .NET 6, D3D12MA is used. In this case, the pool and allocator must be kept alive
        // until all associated resources are returned and destroyed. Because of this, when the
        // device is disposed (since there might be outstanding resources that are still alive),
        // the pool and allocator are only released, but not disposed. This allows reosurces to
        // also release them when disposed (since each resource keeps a reference back to the
        // parent device). When the last one is disposed, the pool and allocator will be deleted.
#if NET6_0_OR_GREATER
        this.pool.Release();
        this.allocator.Release();
#endif

        UnregisterDeviceLostCallback(this);

        // Explicitly release the cached pipeline objects as well.
        // Locking is not needed here, as this method is only invoked
        // when the device has been disposed and no other usage is active
        // for the device (ie. no lease exists), meaning that no other
        // thread could potentially add a new pipeline data concurrently.
        foreach (PipelineData pipelineData in this.cachedPipelineData)
        {
            pipelineData.Dispose();
        }
    }

}
