using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Interop;
using ComputeSharp.Uwp.Helpers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using TerraFX.Interop.WinRT;
using Win32 = TerraFX.Interop.Windows.Windows;

#nullable enable

#pragma warning disable CS0420

namespace ComputeSharp.Uwp;

/// <inheritdoc cref="ComputeShaderPanel"/>
public sealed partial class ComputeShaderPanel
{
    /// <summary>
    /// The render thread in use, if any.
    /// </summary>
    private Thread? renderThread;

    /// <summary>
    /// The <see cref="IShaderRunner"/> instance used to create shaders to run.
    /// </summary>
    private IShaderRunner? shaderRunner;

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
    private ulong nextD3D12FenceValue = 1;

    /// <summary>
    /// The <see cref="ID3D12CommandAllocator"/> object to create command lists.
    /// </summary>
    private ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator;

    /// <summary>
    /// The <see cref="ID3D12GraphicsCommandList"/> instance used to copy data to the back buffers.
    /// </summary>
    private ComPtr<ID3D12GraphicsCommandList> d3D12GraphicsCommandList;

    /// <summary>
    /// The <see cref="IDXGISwapChain2"/> instance used to display content onto the target window.
    /// </summary>
    private ComPtr<IDXGISwapChain2> dxgiSwapChain2;

    /// <summary>
    /// The awaitable object for <see cref="IDXGISwapChain1.Present"/> calls.
    /// </summary>
    private HANDLE frameLatencyWaitableObject;

    /// <summary>
    /// The first buffer within <see cref="dxgiSwapChain2"/>.
    /// </summary>
    private ComPtr<ID3D12Resource> d3D12Resource0;

    /// <summary>
    /// The second buffer within <see cref="dxgiSwapChain2"/>.
    /// </summary>
    private ComPtr<ID3D12Resource> d3D12Resource1;

    /// <summary>
    /// The index of the next buffer that can be used to present content.
    /// </summary>
    private uint currentBufferIndex;

    /// <summary>
    /// The <see cref="ReadWriteTexture2D{T, TPixel}"/> instance used to prepare frames to display.
    /// </summary>
    private ReadWriteTexture2D<Rgba32, Float4>? texture;

    /// <summary>
    /// Whether or not the window has been resized and requires the buffers to be updated.
    /// </summary>
    private volatile bool isResizePending;

    /// <summary>
    /// The backing store for <see cref="Windows.UI.Xaml.FrameworkElement.ActualWidth"/> for the render thread.
    /// </summary>
    private volatile float width;

    /// <summary>
    /// The backing store for <see cref="Windows.UI.Xaml.FrameworkElement.ActualHeight"/> for the render thread.
    /// </summary>
    private volatile float height;

    /// <summary>
    /// The backing store for <see cref="Windows.UI.Xaml.Controls.SwapChainPanel.CompositionScaleX"/> for the render thread.
    /// </summary>
    private volatile float compositionScaleX;

    /// <summary>
    /// The backing store for <see cref="Windows.UI.Xaml.Controls.SwapChainPanel.CompositionScaleY"/> for the render thread.
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
    /// Indicates whether or not the rendering has been canceled.
    /// </summary>
    private volatile bool isCancellationRequested;

    /// <summary>
    /// The <see cref="Stopwatch"/> instance tracking time since the first rendered frame.
    /// </summary>
    private Stopwatch? renderStopwatch;

    /// <summary>
    /// Indicates whether or not <see cref="OnInitialize"/> has already been called.
    /// </summary>
    private bool isInitialized;

    /// <summary>
    /// Initializes the current application.
    /// </summary>
    private unsafe void OnInitialize()
    {
        if (this.isInitialized)
        {
            return;
        }

        this.isInitialized = true;

        // Get the underlying ID3D12Device in use
        fixed (ID3D12Device** d3D12Device = this.d3D12Device)
        {
            InteropServices.TryGetID3D12Device(Gpu.Default, Win32.__uuidof<ID3D12Device>(), (void**)d3D12Device).Assert();
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
        using (ComPtr<IDXGIFactory2> dxgiFactory2 = default)
        {
            DirectX.CreateDXGIFactory2(DXGI.DXGI_CREATE_FACTORY_DEBUG, Win32.__uuidof<IDXGIFactory2>(), (void**)dxgiFactory2.GetAddressOf()).Assert();

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

            dxgiFactory2.Get()->CreateSwapChainForComposition(
                (IUnknown*)this.d3D12CommandQueue.Get(),
                &dxgiSwapChainDesc1,
                null,
                (IDXGISwapChain1**)&dxgiSwapChain1).Assert();

            fixed (IDXGISwapChain2** dxgiSwapChain2 = this.dxgiSwapChain2)
            {
                dxgiSwapChain1.CopyTo(dxgiSwapChain2).Assert();
            }
        }

        // Get the awaitable object to synchronizize present calls
        this.frameLatencyWaitableObject = this.dxgiSwapChain2.Get()->GetFrameLatencyWaitableObject();

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

        // Extract the ISwapChainPanelNative reference from the current panel, then query the
        // IDXGISwapChain reference just created and set that as the swap chain panel to use.
        using ComPtr<ISwapChainPanelNative> swapChainPanelNative = default;

        IUnknown* swapChainPanel = (IUnknown*)Marshal.GetIUnknownForObject(this);

        swapChainPanel->QueryInterface(
            Win32.__uuidof<ISwapChainPanelNative>(),
            (void**)&swapChainPanelNative).Assert();

        using ComPtr<IDXGISwapChain> idxgiSwapChain = default;

        this.dxgiSwapChain2.CopyTo(&idxgiSwapChain).Assert();

        swapChainPanelNative.Get()->SetSwapChain(idxgiSwapChain.Get()).Assert();

        GC.KeepAlive(this);
    }

    /// <summary>
    /// Resizes the current application.
    /// </summary>
    private void OnResize()
    {
        this.isResizePending = true;
    }

    /// <summary>
    /// Applies the actual resize logic that was scheduled from <see cref="OnResize"/>.
    /// </summary>
    private unsafe void ApplyResize()
    {
        this.d3D12CommandQueue.Get()->Signal(this.d3D12Fence.Get(), this.nextD3D12FenceValue).Assert();

        // Wait for the fence again to ensure there are no pending operations
        this.d3D12Fence.Get()->SetEventOnCompletion(this.nextD3D12FenceValue, default).Assert();

        this.nextD3D12FenceValue++;

        // Dispose the old buffers before resizing the buffer
        this.d3D12Resource0.Dispose();
        this.d3D12Resource1.Dispose();

        // Resize the swap chain buffers
        this.dxgiSwapChain2.Get()->ResizeBuffers(
            2,
            (uint)Math.Max(Math.Ceiling(this.width * this.compositionScaleX * this.targetResolutionScale), 1.0),
            (uint)Math.Max(Math.Ceiling(this.height * this.compositionScaleY * this.targetResolutionScale), 1.0),
            DXGI_FORMAT.DXGI_FORMAT_UNKNOWN,
            (uint)DXGI_SWAP_CHAIN_FLAG.DXGI_SWAP_CHAIN_FLAG_FRAME_LATENCY_WAITABLE_OBJECT).Assert();

        // Apply the necessary scale transform
        DXGI_MATRIX_3X2_F transformMatrix = default;
        transformMatrix._11 = (1 / this.compositionScaleX) * (1 / this.targetResolutionScale);
        transformMatrix._22 = (1 / this.compositionScaleY) * (1 / this.targetResolutionScale);

        this.dxgiSwapChain2.Get()->SetMatrixTransform(&transformMatrix).Assert();

        // Retrieve the back buffers for the swap chain
        fixed (ID3D12Resource** d3D12Resource0 = this.d3D12Resource0)
        fixed (ID3D12Resource** d3D12Resource1 = this.d3D12Resource1)
        {
            this.dxgiSwapChain2.Get()->GetBuffer(0, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource0).Assert();
            this.dxgiSwapChain2.Get()->GetBuffer(1, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource1).Assert();
        }

        // Get the index of the initial back buffer
        using (ComPtr<IDXGISwapChain3> dxgiSwapChain3 = default)
        {
            this.dxgiSwapChain2.CopyTo(dxgiSwapChain3.GetAddressOf()).Assert();

            this.currentBufferIndex = dxgiSwapChain3.Get()->GetCurrentBackBufferIndex();
        }

        this.texture?.Dispose();

        D3D12_RESOURCE_DESC d3D12Resource0Description = this.d3D12Resource0.Get()->GetDesc();

        // Create the 2D texture to use to generate frames to display
        this.texture = Gpu.Default.AllocateReadWriteTexture2D<Rgba32, Float4>(
            (int)d3D12Resource0Description.Width,
            (int)d3D12Resource0Description.Height);
    }

    /// <summary>
    /// Updates the render resolution, if needed, and renders a new frame.
    /// </summary>
    /// <param name="time">The current time since the start of the application.</param>
    private unsafe void OnUpdate(TimeSpan time)
    {
        if (this.isResizePending)
        {
            ApplyResize();

            this.isResizePending = false;
        }

        // Skip if no factory is available
        if (this.shaderRunner is null)
        {
            return;
        }

        // Generate the new frame
        this.shaderRunner.Execute(this.texture!, time);
    }

    /// <summary>
    /// Presents the last rendered frame for the current application.
    /// </summary>
    private unsafe void OnPresent()
    {
        _ = Win32.WaitForSingleObjectEx(frameLatencyWaitableObject, 1000, 1);

        using ComPtr<ID3D12Resource> d3D12Resource = default;

        // Get the underlying ID3D12Resource pointer for the texture
        InteropServices.TryGetID3D12Resource(this.texture!, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf()).Assert();

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
        this.d3D12CommandQueue.Get()->Signal(this.d3D12Fence.Get(), this.nextD3D12FenceValue).Assert();

        // Present the new frame
        this.dxgiSwapChain2.Get()->Present(0, 0).Assert();

        if (this.nextD3D12FenceValue > this.d3D12Fence.Get()->GetCompletedValue())
        {
            this.d3D12Fence.Get()->SetEventOnCompletion(this.nextD3D12FenceValue, default).Assert();
        }

        this.nextD3D12FenceValue++;
    }

    /// <summary>
    /// Executes the render loop for the current panel.
    /// </summary>
    /// <remarks>This method assumes to be invoked from a background thread.</remarks>
    private void OnStartRenderLoop()
    {
        static void ExecuteRenderLoop(ComputeShaderPanel @this)
        {
            Stopwatch
                renderStopwatch = @this.renderStopwatch ??= new(),
                frameStopwatch = Stopwatch.StartNew();

            DynamicResolutionManager.Create(out DynamicResolutionManager frameTimeWatcher);

            bool isDynamicResolutionEnabled = @this.isDynamicResolutionEnabled;

            // Start the initial frame separately, before the timer starts. This ensures that
            // resuming after a pause correctly renders the first frame at the right time.
            @this.OnUpdate(renderStopwatch.Elapsed);
            @this.OnPresent();

            renderStopwatch.Start();

            const long targetFrameTimeInTicksFor61fps = 163934;

            // Main render loop, until cancellation is requested
            while (!@this.isCancellationRequested)
            {
                // Update the resolution mode, if needed
                if (isDynamicResolutionEnabled != @this.isDynamicResolutionEnabled)
                {
                    isDynamicResolutionEnabled = !isDynamicResolutionEnabled;

                    if (isDynamicResolutionEnabled)
                    {
                        DynamicResolutionManager.Create(out frameTimeWatcher);
                    }
                    else
                    {
                        @this.targetResolutionScale = @this.resolutionScale;
                    }
                }

                // Evaluate the dynamic resolution frame time step, if the mode is enabled
                if (isDynamicResolutionEnabled &&
                    frameTimeWatcher.Advance(frameStopwatch.ElapsedTicks, ref @this.targetResolutionScale))
                {
                    @this.isResizePending = true;
                }

                while (frameStopwatch.ElapsedTicks < targetFrameTimeInTicksFor61fps)
                {
                }

                frameStopwatch.Restart();

                @this.OnUpdate(renderStopwatch.Elapsed);
                @this.OnPresent();
            }

            renderStopwatch.Stop();
        }

        this.isCancellationRequested = false;
        this.renderThread = new Thread(static args => ExecuteRenderLoop((ComputeShaderPanel)args!));
        this.renderThread.Start(this);
    }

    /// <summary>
    /// Stops the current render loop, if one is running.
    /// </summary>
    private void OnStopRenderLoop()
    {
        this.isCancellationRequested = true;
    }

    /// <summary>
    /// Stops the current render loop, and releases all resources.
    /// </summary>
    private void OnDisposed()
    {
        this.isCancellationRequested = true;

        // Ensure the rendering has stopped
        if (this.renderThread is Thread renderThread)
        {
            renderThread.Join();
        }

        this.d3D12Device.Dispose();
        this.d3D12CommandQueue.Dispose();
        this.d3D12Fence.Dispose();
        this.d3D12CommandAllocator.Dispose();
        this.d3D12GraphicsCommandList.Dispose();
        this.dxgiSwapChain2.Dispose();
        this.d3D12Resource0.Dispose();
        this.d3D12Resource1.Dispose();
        this.texture?.Dispose();
    }
}