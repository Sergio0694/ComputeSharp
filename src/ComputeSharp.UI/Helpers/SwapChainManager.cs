using System;
using System.Diagnostics;
using System.Threading;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop;
#if !WINDOWS_UWP
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
#endif
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using TerraFX.Interop.WinRT;
#if WINDOWS_UWP
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.System;
#else
using Windows.Foundation;
using WinRT;
#endif
using Win32 = TerraFX.Interop.Windows.Windows;

#if WINDOWS_UWP
namespace ComputeSharp.Uwp.Helpers;
#else
namespace ComputeSharp.WinUI.Helpers;
#endif

/// <summary>
/// A type managing rendering on a target swap chain object.
/// </summary>
/// <typeparam name="TOwner">The type of the owner <see cref="SwapChainPanel"/> instance.</typeparam>
internal sealed unsafe partial class SwapChainManager<TOwner> : NativeObject
    where TOwner : SwapChainPanel
{
    /// <summary>
    /// The owning <typeparamref name="TOwner"/> instance.
    /// </summary>
    private readonly TOwner owner;

    /// <summary>
    /// The captured <see cref="SynchronizationContext"/> for the current instance.
    /// </summary>
    private readonly DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

    /// <summary>
    /// An <see cref="SemaphoreSlim"/> object for the render loop management.
    /// </summary>
    private readonly SemaphoreSlim setupSemaphore = new(1, 1);

    /// <summary>
    /// The <see cref="ID3D12Device"/> pointer for the device currently in use.
    /// </summary>
    private ComPtr<ID3D12Device> d3D12Device;

    /// <summary>
    /// The <see cref="ID3D12CommandQueue"/> instance to use for graphics operations.
    /// </summary>
    private ComPtr<ID3D12CommandQueue> d3D12CommandQueue;

    /// <summary>
    /// The <see cref="ID3D12Fence"/> instance used for graphics operations.
    /// </summary>
    private ComPtr<ID3D12Fence> d3D12Fence;

    /// <summary>
    /// The next fence value for graphics operations using <see cref="d3D12CommandQueue"/>.
    /// </summary>
    private ulong nextD3D12FenceValue;

    /// <summary>
    /// The <see cref="ID3D12CommandAllocator"/> object to create command lists.
    /// </summary>
    private ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator;

    /// <summary>
    /// The <see cref="ID3D12GraphicsCommandList"/> instance used to copy data to the back buffers.
    /// </summary>
    private ComPtr<ID3D12GraphicsCommandList> d3D12GraphicsCommandList;

    /// <summary>
    /// The <see cref="IDXGISwapChain3"/> instance used to display content onto the target window.
    /// </summary>
    private ComPtr<IDXGISwapChain3> dxgiSwapChain3;

    /// <summary>
    /// The awaitable object for <see cref="IDXGISwapChain3.Present"/> calls.
    /// </summary>
    private HANDLE frameLatencyWaitableObject;

    /// <summary>
    /// The first buffer within <see cref="dxgiSwapChain3"/>.
    /// </summary>
    private ComPtr<ID3D12Resource> d3D12Resource0;

    /// <summary>
    /// The second buffer within <see cref="dxgiSwapChain3"/>.
    /// </summary>
    private ComPtr<ID3D12Resource> d3D12Resource1;

    /// <summary>
    /// The index of the next buffer that can be used to present content.
    /// </summary>
    private volatile uint currentBufferIndex;

    /// <summary>
    /// The render thread in use, if any.
    /// </summary>
    private volatile Thread? renderThread;

    /// <summary>
    /// The <see cref="SemaphoreSlim"/> used to wait for render threads to complete.
    /// </summary>
    private volatile SemaphoreSlim renderSemaphore = new(1, 1);

    /// <summary>
    /// The <see cref="CancellationTokenSource"/> to use for <see cref="renderThread"/>.
    /// </summary>
    private volatile CancellationTokenSource? renderCancellationTokenSource;

    /// <summary>
    /// The <see cref="IFrameRequestQueue"/> instance used to get frame requests and parameters.
    /// </summary>
    private volatile IFrameRequestQueue? frameRequestQueue;

    /// <summary>
    /// The <see cref="IShaderRunner"/> instance used to create shaders to run.
    /// </summary>
    private volatile IShaderRunner? shaderRunner;

    /// <summary>
    /// The <see cref="ReadWriteTexture2D{T, TPixel}"/> instance used to prepare frames to display.
    /// </summary>
    private volatile ReadWriteTexture2D<Rgba32, Float4>? texture;

    /// <summary>
    /// Whether or not the window has been resized and requires the buffers to be updated.
    /// </summary>
    private volatile bool isResizePending;

    /// <summary>
    /// The backing store for the actual width for the render thread.
    /// </summary>
    private volatile float width;

    /// <summary>
    /// The backing store for the actual height for the render thread.
    /// </summary>
    private volatile float height;

    /// <summary>
    /// The backing store for the current composition scale on the X axis for the render thread.
    /// </summary>
    private volatile float compositionScaleX;

    /// <summary>
    /// The backing store for the current composition scale on the Y axis for the render thread.
    /// </summary>
    private volatile float compositionScaleY;

    /// <summary>
    /// The resolution scale used to render frames when explicitly controlled by the user.
    /// </summary>
    private volatile float resolutionScale;

    /// <summary>
    /// The resolution scale used to render frames, in either render mode.
    /// </summary>
    private volatile float targetResolutionScale;

    /// <summary>
    /// Whether dynamic resolution is currently enabled.
    /// </summary>
    private volatile bool isDynamicResolutionEnabled;

    /// <summary>
    /// The <see cref="Stopwatch"/> instance tracking time since the first rendered frame.
    /// </summary>
    private volatile Stopwatch? renderStopwatch;

    /// <summary>
    /// Raised whenever rendering starts.
    /// </summary>
    public event TypedEventHandler<TOwner, EventArgs>? RenderingStarted;

    /// <summary>
    /// Raised whenever rendering stops.
    /// </summary>
    public event TypedEventHandler<TOwner, EventArgs>? RenderingStopped;

    /// <summary>
    /// Raised whenever rendering fails.
    /// </summary>
    public event TypedEventHandler<TOwner, Exception>? RenderingFailed;

    /// <summary>
    /// Initializes a new instance of the <see cref="SwapChainManager{TOwner}"/> type.
    /// </summary>
    /// <param name="owner">The input swap chain instance being used.</param>
    public SwapChainManager(TOwner owner)
    {
        this.owner = owner;

        // Extract the ISwapChainPanelNative reference from the current panel, then query the
        // IDXGISwapChain reference just created and set that as the swap chain panel to use.
        using ComPtr<ISwapChainPanelNative> swapChainPanelNative = default;

#if WINDOWS_UWP
        IUnknown* swapChainPanel = (IUnknown*)Marshal.GetIUnknownForObject(owner);

        swapChainPanel->QueryInterface(
            Win32.__uuidof<ISwapChainPanelNative>(),
            (void**)&swapChainPanelNative).Assert();
#else
        IUnknown* swapChainPanel = (IUnknown*)((IWinRTObject)owner).NativeObject.ThisPtr;
        Guid iSwapChainPanelNativeUuid = new(0x63AAD0B8, 0x7C24, 0x40FF, 0x85, 0xA8, 0x64, 0x0D, 0x94, 0x4C, 0xC3, 0x25);

        swapChainPanel->QueryInterface(
            &iSwapChainPanelNativeUuid,
            (void**)&swapChainPanelNative).Assert();
#endif

        // Get the underlying ID3D12Device in use
        fixed (ID3D12Device** d3D12Device = this.d3D12Device)
        {
            GraphicsDevice.Default.D3D12Device->QueryInterface(Win32.__uuidof<ID3D12Device>(), (void**)d3D12Device).Assert();
        }

        // Create the direct command queue to use
        fixed (ID3D12CommandQueue** d3D12CommandQueue = this.d3D12CommandQueue)
        {
            D3D12_COMMAND_QUEUE_DESC d3D12CommandQueueDesc;
            d3D12CommandQueueDesc.Type = D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT;
            d3D12CommandQueueDesc.Priority = (int)D3D12_COMMAND_QUEUE_PRIORITY.D3D12_COMMAND_QUEUE_PRIORITY_NORMAL;
            d3D12CommandQueueDesc.Flags = D3D12_COMMAND_QUEUE_FLAGS.D3D12_COMMAND_QUEUE_FLAG_NONE;
            d3D12CommandQueueDesc.NodeMask = 0;

            this.d3D12Device.Get()->CreateCommandQueue(
                &d3D12CommandQueueDesc,
                Win32.__uuidof<ID3D12CommandQueue>(),
                (void**)d3D12CommandQueue).Assert();
        }

        // Create the direct fence
        fixed (ID3D12Fence** d3D12Fence = this.d3D12Fence)
        {
            this.d3D12Device.Get()->CreateFence(
                0,
                D3D12_FENCE_FLAGS.D3D12_FENCE_FLAG_NONE,
                Win32.__uuidof<ID3D12Fence>(),
                (void**)d3D12Fence).Assert();
        }

        // Create the swap chain to display frames
        using (ComPtr<IDXGIFactory6> dxgiFactory6 = default)
        {
            DeviceHelper.CreateDXGIFactory6(dxgiFactory6.GetAddressOf());

            DXGI_SWAP_CHAIN_DESC1 dxgiSwapChainDesc1 = default;
            dxgiSwapChainDesc1.AlphaMode = DXGI_ALPHA_MODE.DXGI_ALPHA_MODE_IGNORE;
            dxgiSwapChainDesc1.BufferCount = 2;
            dxgiSwapChainDesc1.Flags = (uint)DXGI_SWAP_CHAIN_FLAG.DXGI_SWAP_CHAIN_FLAG_FRAME_LATENCY_WAITABLE_OBJECT;
            dxgiSwapChainDesc1.Format = DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM;
            dxgiSwapChainDesc1.Width = (uint)Math.Max(Math.Ceiling(this.width * this.compositionScaleX * this.targetResolutionScale), 1.0);
            dxgiSwapChainDesc1.Height = (uint)Math.Max(Math.Ceiling(this.height * this.compositionScaleY * this.targetResolutionScale), 1.0);
            dxgiSwapChainDesc1.SampleDesc = new DXGI_SAMPLE_DESC(count: 1, quality: 0);
            dxgiSwapChainDesc1.Scaling = DXGI_SCALING.DXGI_SCALING_STRETCH;
            dxgiSwapChainDesc1.Stereo = 0;
            dxgiSwapChainDesc1.SwapEffect = DXGI_SWAP_EFFECT.DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL;
            dxgiSwapChainDesc1.BufferUsage = DXGI.DXGI_USAGE_RENDER_TARGET_OUTPUT;

            using ComPtr<IDXGISwapChain1> dxgiSwapChain1 = default;

            dxgiFactory6.Get()->CreateSwapChainForComposition(
                (IUnknown*)this.d3D12CommandQueue.Get(),
                &dxgiSwapChainDesc1,
                null,
                (IDXGISwapChain1**)&dxgiSwapChain1).Assert();

            fixed (IDXGISwapChain3** dxgiSwapChain3 = this.dxgiSwapChain3)
            {
                dxgiSwapChain1.CopyTo(dxgiSwapChain3).Assert();
            }
        }

        // Get the awaitable object to synchronizize present calls
        this.frameLatencyWaitableObject = this.dxgiSwapChain3.Get()->GetFrameLatencyWaitableObject();

        // Create the command allocator to use
        fixed (ID3D12CommandAllocator** d3D12CommandAllocator = this.d3D12CommandAllocator)
        {
            this.d3D12Device.Get()->CreateCommandAllocator(
                D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT,
                Win32.__uuidof<ID3D12CommandAllocator>(),
                (void**)d3D12CommandAllocator).Assert();
        }

        // Create the reusable command list to copy data to the back buffers
        fixed (ID3D12GraphicsCommandList** d3D12GraphicsCommandList = this.d3D12GraphicsCommandList)
        {
            this.d3D12Device.Get()->CreateCommandList(
                0,
                D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT,
                this.d3D12CommandAllocator,
                null,
                Win32.__uuidof<ID3D12GraphicsCommandList>(),
                (void**)d3D12GraphicsCommandList).Assert();
        }

        // Close the command list to prepare it for future use
        this.d3D12GraphicsCommandList.Get()->Close().Assert();

        using (ComPtr<IDXGISwapChain> idxgiSwapChain = default)
        {
            this.dxgiSwapChain3.CopyTo(&idxgiSwapChain).Assert();

            swapChainPanelNative.Get()->SetSwapChain(idxgiSwapChain.Get()).Assert();
        }

        GC.KeepAlive(this);
    }

    /// <summary>
    /// Applies the actual resize logic that was scheduled from <see cref="OnResize"/>.
    /// </summary>
    private unsafe void ApplyResize()
    {
        ulong updatedFenceValue = ++this.nextD3D12FenceValue;

        this.d3D12CommandQueue.Get()->Signal(this.d3D12Fence.Get(), updatedFenceValue).Assert();

        // Wait for the fence again to ensure there are no pending operations
        this.d3D12Fence.Get()->SetEventOnCompletion(updatedFenceValue, default).Assert();

        // Dispose the old buffers before resizing the buffer
        this.d3D12Resource0.Dispose();
        this.d3D12Resource1.Dispose();

        // Resize the swap chain buffers
        this.dxgiSwapChain3.Get()->ResizeBuffers(
            2,
            (uint)Math.Max(Math.Ceiling(this.width * this.compositionScaleX * this.targetResolutionScale), 1.0),
            (uint)Math.Max(Math.Ceiling(this.height * this.compositionScaleY * this.targetResolutionScale), 1.0),
            DXGI_FORMAT.DXGI_FORMAT_UNKNOWN,
            (uint)DXGI_SWAP_CHAIN_FLAG.DXGI_SWAP_CHAIN_FLAG_FRAME_LATENCY_WAITABLE_OBJECT).Assert();

        // Apply the necessary scale transform
        DXGI_MATRIX_3X2_F transformMatrix = default;
        transformMatrix._11 = (1 / this.compositionScaleX) * (1 / this.targetResolutionScale);
        transformMatrix._22 = (1 / this.compositionScaleY) * (1 / this.targetResolutionScale);

        this.dxgiSwapChain3.Get()->SetMatrixTransform(&transformMatrix).Assert();

        // Retrieve the back buffers for the swap chain
        fixed (ID3D12Resource** d3D12Resource0 = this.d3D12Resource0)
        fixed (ID3D12Resource** d3D12Resource1 = this.d3D12Resource1)
        {
            this.dxgiSwapChain3.Get()->GetBuffer(0, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource0).Assert();
            this.dxgiSwapChain3.Get()->GetBuffer(1, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource1).Assert();
        }

        // Get the index of the initial back buffer
        this.currentBufferIndex = this.dxgiSwapChain3.Get()->GetCurrentBackBufferIndex();

        this.texture?.Dispose();

        D3D12_RESOURCE_DESC d3D12Resource0Description = this.d3D12Resource0.Get()->GetDesc();

        // Create the 2D texture to use to generate frames to display
        this.texture = GraphicsDevice.Default.AllocateReadWriteTexture2D<Rgba32, Float4>(
            (int)d3D12Resource0Description.Width,
            (int)d3D12Resource0Description.Height);
    }

    /// <inheritdoc/>
    private unsafe partial void OnPresent()
    {
        // Wait for the swap chain to be ready for presenting
        _ = Win32.WaitForSingleObjectEx(frameLatencyWaitableObject, 1000, true);

        using ComPtr<ID3D12Resource> d3D12Resource = default;

        // Get the underlying ID3D12Resource pointer for the texture
        this.texture!.D3D12Resource->QueryInterface(Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf()).Assert();

        // Get the target back buffer to update
        ID3D12Resource* d3D12ResourceBackBuffer = this.currentBufferIndex switch
        {
            0 => this.d3D12Resource0.Get(),
            1 => this.d3D12Resource1.Get(),
            _ => null
        };

        this.currentBufferIndex ^= 1;

        // Reset the command list and command allocator
        this.d3D12CommandAllocator.Get()->Reset().Assert();
        this.d3D12GraphicsCommandList.Get()->Reset(this.d3D12CommandAllocator.Get(), null).Assert();

        D3D12_RESOURCE_BARRIER* d3D12ResourceBarriers = stackalloc D3D12_RESOURCE_BARRIER[]
        {
            D3D12_RESOURCE_BARRIER.InitTransition(
                d3D12Resource.Get(),
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_UNORDERED_ACCESS,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_SOURCE),
            D3D12_RESOURCE_BARRIER.InitTransition(
                d3D12ResourceBackBuffer,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COMMON,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_DEST)
        };

        // Transition the resources to COPY_DEST and COPY_SOURCE respectively
        this.d3D12GraphicsCommandList.Get()->ResourceBarrier(2, d3D12ResourceBarriers);

        // Copy the generated frame to the target back buffer
        this.d3D12GraphicsCommandList.Get()->CopyResource(d3D12ResourceBackBuffer, d3D12Resource.Get());

        d3D12ResourceBarriers[0] = D3D12_RESOURCE_BARRIER.InitTransition(
            d3D12Resource.Get(),
            D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_SOURCE,
            D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_UNORDERED_ACCESS);

        d3D12ResourceBarriers[1] = D3D12_RESOURCE_BARRIER.InitTransition(
            d3D12ResourceBackBuffer,
            D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_DEST,
            D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COMMON);

        // Transition the resources back to COMMON and UNORDERED_ACCESS respectively
        this.d3D12GraphicsCommandList.Get()->ResourceBarrier(2, d3D12ResourceBarriers);

        this.d3D12GraphicsCommandList.Get()->Close().Assert();

        // Execute the command list to perform the copy
        this.d3D12CommandQueue.Get()->ExecuteCommandLists(1, (ID3D12CommandList**)this.d3D12GraphicsCommandList.GetAddressOf());

        // Increment the fence value and signal the fence. Just like with normal dispatches,
        // the increment must be done after executing the command list and before signaling.
        ulong updatedFenceValue = ++this.nextD3D12FenceValue;

        this.d3D12CommandQueue.Get()->Signal(this.d3D12Fence.Get(), updatedFenceValue).Assert();

        // Present the new frame
        this.dxgiSwapChain3.Get()->Present(0, 0).Assert();

        if (updatedFenceValue > this.d3D12Fence.Get()->GetCompletedValue())
        {
            this.d3D12Fence.Get()->SetEventOnCompletion(updatedFenceValue, default).Assert();
        }
    }

    /// <inheritdoc/>
    protected override void OnDispose()
    {
        ThreadPool.UnsafeQueueUserWorkItem(static state =>
        {
            SwapChainManager<TOwner> @this = (SwapChainManager<TOwner>)state!;

            @this.UnsafeStopRenderLoopAndWait();

            @this.d3D12Device.Dispose();
            @this.d3D12CommandQueue.Dispose();
            @this.d3D12Fence.Dispose();
            @this.d3D12CommandAllocator.Dispose();
            @this.d3D12GraphicsCommandList.Dispose();
            @this.dxgiSwapChain3.Dispose();
            @this.d3D12Resource0.Dispose();
            @this.d3D12Resource1.Dispose();
            @this.texture?.Dispose();
        }, this);
    }
}
