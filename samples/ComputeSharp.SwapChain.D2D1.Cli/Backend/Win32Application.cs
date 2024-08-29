using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.UI.Windowing;

namespace ComputeSharp.SwapChain.D2D1.Backend;

using BitmapSize = Windows.Graphics.Imaging.BitmapSize;
using CanvasDevice = Microsoft.Graphics.Canvas.CanvasDevice;
using CanvasDrawingSession = Microsoft.Graphics.Canvas.CanvasDrawingSession;
using CanvasSwapChain = Microsoft.Graphics.Canvas.CanvasSwapChain;
using DirectXPixelFormat = Windows.Graphics.DirectX.DirectXPixelFormat;

/// <summary>
/// A simple Win32 application handling a D2D swapchain.
/// </summary>
internal sealed class Win32Application
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
    /// <param name="appWindow">The window to show.</param>
    [MemberNotNull(nameof(canvasDevice))]
    [MemberNotNull(nameof(canvasSwapChain))]
    public unsafe void OnInitialize(AppWindow appWindow)
    {
        // Create a new canvas device, which will handle DX11/D2D initialization
        CanvasDevice canvasDevice = new();
        CanvasSwapChain canvasSwapChain = CanvasSwapChain.CreateForWindowId(
            resourceCreator: canvasDevice,
            windowId: appWindow.Id,
            width: this.screenWidth = 1,
            height: this.screenHeight = 1,
            dpi: 96.0f);

        // Save the Win2D objects for later use
        this.canvasDevice = canvasDevice;
        this.canvasSwapChain = canvasSwapChain;

        // Request a resize before the first draw operation
        this.isResizePending = true;
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