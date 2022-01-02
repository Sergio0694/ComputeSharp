using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Toolkit.Diagnostics;

#pragma warning disable CS0420

#if WINDOWS_UWP
namespace ComputeSharp.Uwp.Helpers;
#else
namespace ComputeSharp.WinUI.Helpers;
#endif

/// <inheritdoc/>
partial class SwapChainManager
{
    /// <summary>
    /// Starts the current render loop.
    /// </summary>
    /// <param name="shaderRunner">The <see cref="IShaderRunner"/> instance to use to render frames.</param>
    public void StartRenderLoop(IShaderRunner shaderRunner)
    {
        ThrowIfDisposed();

        Guard.IsNotNull(shaderRunner, nameof(shaderRunner));

        lock (this.renderLock)
        {
            this.renderCancellationTokenSource?.Cancel();
            this.renderThread?.Join();

            Thread newRenderThread = new(static args => ((SwapChainManager)args!).SwitchAndStartRenderLoop());

            this.shaderRunner = shaderRunner;
            this.renderCancellationTokenSource = new CancellationTokenSource();
            this.renderThread = newRenderThread;

            newRenderThread.Start(this);
        }
    }

    /// <summary>
    /// Stops the current render loop, if one is running.
    /// </summary>
    public void StopRenderLoop()
    {
        ThrowIfDisposed();

        lock (this.renderLock)
        {
            this.renderCancellationTokenSource?.Cancel();
            this.renderThread?.Join();
        }
    }

    /// <summary>
    /// Queues a change in the dynamic resolution mode.
    /// </summary>
    /// <param name="isDynamicResolutionEnabled">Whether or not to use dynamic resolution.</param>
    public void QueueDynamicResolutionModeChange(bool isDynamicResolutionEnabled)
    {
        ThrowIfDisposed();

        lock (this.renderLock)
        {
            // If there is a render thread currently running, stop it and restart it
            if (this.renderCancellationTokenSource?.IsCancellationRequested == false)
            {
                this.renderCancellationTokenSource?.Cancel();
                this.renderThread?.Join();

                Thread newRenderThread = new(static args => ((SwapChainManager)args!).SwitchAndStartRenderLoop());

                this.renderCancellationTokenSource = new CancellationTokenSource();
                this.renderThread = newRenderThread;
                this.isDynamicResolutionEnabled = isDynamicResolutionEnabled;

                newRenderThread.Start(this);
            }
            else
            {
                this.isDynamicResolutionEnabled = isDynamicResolutionEnabled;
            }
        }
    }

    /// <summary>
    /// Queues a resize operation.
    /// </summary>
    /// <param name="width">The width of the render resolution.</param>
    /// <param name="height">The height of the render resolution.</param>
    public void QueueResize(double width, double height)
    {
        ThrowIfDisposed();

        this.width = (float)width;
        this.height = (float)height;

        this.isResizePending = true;
    }

    /// <summary>
    /// Queues a change in the composition scale factors.
    /// </summary>
    /// <param name="compositionScaleX">The composition scale on the X axis.</param>
    /// <param name="compositionScaleY">The composition scale on the Y axis</param>
    public void QueueCompositionScaleChange(double compositionScaleX, double compositionScaleY)
    {
        ThrowIfDisposed();

        this.compositionScaleX = (float)compositionScaleX;
        this.compositionScaleY = (float)compositionScaleY;

        this.isResizePending = true;
    }

    /// <summary>
    /// Queues a change in the resolution scale factor.
    /// </summary>
    /// <param name="resolutionScale">The resolution scale factor to use.</param>
    public void QueueResolutionScaleChange(double resolutionScale)
    {
        ThrowIfDisposed();

        this.resolutionScale = (float)resolutionScale;

        this.isResizePending = true;
    }

    /// <summary>
    /// Selects the right render loop to start.
    /// </summary>
    private void SwitchAndStartRenderLoop()
    {
        if (this.isDynamicResolutionEnabled)
        {
            this.targetResolutionScale = 1.0f;

            RenderLoopWithDynamicResolution();
        }
        else
        {
            this.targetResolutionScale = this.resolutionScale;

            RenderLoop();
        }
    }

    /// <summary>
    /// The core render loop.
    /// </summary>
    private void RenderLoop()
    {
        Stopwatch renderStopwatch = this.renderStopwatch ??= new();
        CancellationToken cancellationToken = this.renderCancellationTokenSource!.Token;

        // Start the initial frame separately, before the timer starts. This ensures that
        // resuming after a pause correctly renders the first frame at the right time.
        this.OnResize();
        this.OnUpdate(renderStopwatch.Elapsed);
        this.OnPresent();

        renderStopwatch.Start();

        // Main render loop, until cancellation is requested
        while (!cancellationToken.IsCancellationRequested)
        {
            this.OnResize();
            this.OnUpdate(renderStopwatch.Elapsed);
            this.OnPresent();
        }

        renderStopwatch.Stop();
    }

    /// <summary>
    /// The core render loop with dynamic resolution.
    /// </summary>
    private void RenderLoopWithDynamicResolution()
    {
        Stopwatch renderStopwatch = this.renderStopwatch ??= new();
        Stopwatch frameStopwatch = Stopwatch.StartNew();
        CancellationToken cancellationToken = this.renderCancellationTokenSource!.Token;

        DynamicResolutionManager.Create(out DynamicResolutionManager frameTimeWatcher);

        // Start the initial frame separately, before the timer starts. This ensures that
        // resuming after a pause correctly renders the first frame at the right time.
        this.OnResize();
        this.OnUpdate(renderStopwatch.Elapsed);
        this.OnPresent();

        renderStopwatch.Start();

        // Main render loop, until cancellation is requested
        while (!cancellationToken.IsCancellationRequested)
        {
            // Evaluate the dynamic resolution frame time step, if the mode is enabled
            if (frameTimeWatcher.Advance(frameStopwatch.ElapsedTicks, ref this.targetResolutionScale))
            {
                this.isResizePending = true;
            }

            frameStopwatch.Restart();

            this.OnResize();
            this.OnUpdate(renderStopwatch.Elapsed);
            this.OnPresent();
        }

        renderStopwatch.Stop();
    }

    /// <summary>
    /// Resizes the current application, if needed.
    /// </summary>
    private void OnResize()
    {
        if (this.isResizePending)
        {
            ApplyResize();

            this.isResizePending = false;
        }
    }

    /// <summary>
    /// Updates the render resolution, if needed, and renders a new frame.
    /// </summary>
    /// <param name="time">The current time since the start of the application.</param>
    private void OnUpdate(TimeSpan time)
    {
        this.shaderRunner!.Execute(this.texture!, time);
    }

    /// <summary>
    /// Presents the last rendered frame for the current application.
    /// </summary>
    private unsafe partial void OnPresent();

    /// <summary>
    /// Stops the current render loop, if one is running, and waits for it.
    /// </summary>
    /// <remarks>This method doesn't check for disposal.</remarks>
    private void UnsafeStopRenderLoopAndWait()
    {
        ThrowIfDisposed();

        lock (this.renderLock)
        {
            this.renderCancellationTokenSource?.Cancel();
        }
    }
}
