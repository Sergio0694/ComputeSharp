using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Graphics.Canvas;
using Windows.Graphics.DirectX;
using Windows.Graphics.Imaging;
using Windows.UI.Core;

namespace ComputeSharp.SwapChain.D2D1.Backend;

/// <summary>
/// A simple <see cref="CoreWindow"/> application handling a D2D swapchain.
/// </summary>
internal sealed class CoreWindowApplication
{
    /// <summary>
    /// The <see cref="CanvasDevice"/> instance to use.
    /// </summary>
    private CanvasDevice? canvasDevice;

    /// <summary>
    /// The <see cref="CanvasSwapChain"/> instance to use to render frames.
    /// </summary>
    private CanvasSwapChain? canvasSwapChain;

    /// <summary>
    /// Whether or not the window has been resized and requires the buffers to be updated.
    /// </summary>
    private volatile bool isResizePending;

    /// <summary>
    /// The current screen width in raw pixels.
    /// </summary>
    private uint screenWidth;

    /// <summary>
    /// The current screen height in raw pixels.
    /// </summary>
    private uint screenHeight;

    /// <summary>
    /// Raised whenever a draw operation can be performed.
    /// </summary>
    public event EventHandler<DrawEventArgs>? Draw;

    /// <summary>
    /// Initializes the current application.
    /// </summary>
    /// <param name="window">The target window.</param>
    [MemberNotNull(nameof(canvasDevice))]
    [MemberNotNull(nameof(canvasSwapChain))]
    public unsafe void OnInitialize(CoreWindow window)
    {
        // Create a new canvas device, which will handle DX11/D2D initialization
        CanvasDevice canvasDevice = new();
        CanvasSwapChain canvasSwapChain = CanvasSwapChain.CreateForCoreWindow(canvasDevice, window, 144);

        // Save the Win2D objects for later use
        this.canvasDevice = canvasDevice;
        this.canvasSwapChain = canvasSwapChain;
    }

    /// <summary>
    /// Resizes the current application.
    /// </summary>
    public void OnResize()
    {
        this.isResizePending = true;
    }

    /// <summary>
    /// Updates the current application.
    /// </summary>
    /// <param name="time">The current time since the start of the application.</param>
    public void OnUpdate(TimeSpan time)
    {
        if (this.isResizePending)
        {
            // Resize the swapchain if needed (the size is calculated automatically)
            this.canvasSwapChain!.ResizeBuffers(
                newWidth: 0,
                newHeight: 0,
                newDpi: 96.0f,
                newFormat: DirectXPixelFormat.Unknown,
                bufferCount: 0);

            BitmapSize bitmapSize = this.canvasSwapChain!.SizeInPixels;

            this.screenWidth = bitmapSize.Width;
            this.screenHeight = bitmapSize.Height;

            this.isResizePending = false;
        }

        // Create the drawing session and invoke all registered draw handler
        using (CanvasDrawingSession canvasDrawingSession = this.canvasSwapChain!.CreateDrawingSession(default))
        {
            Draw?.Invoke(this, new DrawEventArgs
            {
                ScreenWidth = this.screenWidth,
                ScreenHeight = this.screenHeight,
                TotalTime = time,
                DrawingSession = canvasDrawingSession
            });
        }

        // Wait for v-sync
        this.canvasSwapChain.WaitForVerticalBlank();

        // Present the new frame
        this.canvasSwapChain.Present(syncInterval: 1);
    }

    /// <summary>
    /// Arguments for <see cref="Draw"/>.
    /// </summary>
    public sealed class DrawEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the screen width in raw pixels.
        /// </summary>
        public required uint ScreenWidth { get; init; }

        /// <summary>
        /// Gets the screen height in raw pixels.
        /// </summary>
        public required uint ScreenHeight { get; init; }

        /// <summary>
        /// Gets the total time for the rendering loop.
        /// </summary>
        public required TimeSpan TotalTime { get; init; }

        /// <summary>
        /// Gets the <see cref="CanvasDrawingSession"/> instance to use.
        /// </summary>
        public required CanvasDrawingSession DrawingSession { get; init; }
    }
}
