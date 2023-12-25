using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace Microsoft.Graphics.Canvas.UI.Xaml;

/// <summary>
/// XAML control intended for displaying animating content.
/// </summary>
public sealed class CanvasAnimatedControl : Grid, ICanvasAnimatedControl, IDisposable
{
    /// <summary>
    /// The <see cref="CanvasControl"/> instance to use.
    /// </summary>
    private readonly CanvasControl canvasControl;

    /// <summary>
    /// The <see cref="Stopwatch"/> used to track the elapsed time.
    /// </summary>
    private readonly Stopwatch stopwatch;

    /// <summary>
    /// The <see cref="Timer"/> used to render frames.
    /// </summary>
    private readonly Timer timer;

    /// <summary>
    /// Indicates whether rendering is paused.
    /// </summary>
    private bool paused;

    /// <summary>
    /// Indicates whether a new frame is currently being drawn.
    /// </summary>
    private volatile int isDrawing;

    /// <summary>
    /// Hook this event to draw the contents of the control.
    /// </summary>
    public TypedEventHandler<ICanvasAnimatedControl, CanvasAnimatedDrawEventArgs>? Draw;

    /// <summary>
    /// Creates a new <see cref="CanvasAnimatedControl"/> instance.
    /// </summary>
    public CanvasAnimatedControl()
    {
        this.canvasControl = new CanvasControl();
        this.stopwatch = new Stopwatch();
        this.timer = new Timer(
            callback: static canvasControl => ((CanvasControl)canvasControl!).Invalidate(),
            state: this.canvasControl,
            dueTime: TimeSpan.Zero,
            period: TimeSpan.FromSeconds(1 / 60.0));

        // Register the draw callback for the canvas control
        this.canvasControl.Draw += CanvasControl_Draw;

        // Start the render stopwatch (the render timer has already started)
        this.stopwatch.Start();

        // Add the canvas control to the visual tree
        Children.Add(this.canvasControl);
    }

    /// <summary>
    /// Gets or sets whether rendering has paused
    /// </summary>
    public bool Paused
    {
        get => this.paused;
        set
        {
            if (this.paused != value)
            {
                this.paused = value;

                // Start or stop the timers as needed
                if (value)
                {
                    _ = this.timer.Change(Timeout.Infinite, Timeout.Infinite);

                    this.stopwatch.Stop();
                }
                else
                {
                    _ = this.timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(1 / 60.0f));

                    this.stopwatch.Start();
                }
            }
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.timer.Dispose();
    }

    // Forward the Draw event as a CanvasAnimatedControl sender
    private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
    {
        // If another frame is already being concurrently drawn, do nothing
        if (Interlocked.CompareExchange(ref this.isDrawing, 1, 0) == 1)
        {
            return;
        }

        try
        {
            this.Draw?.Invoke(
                sender: this,
                args: new CanvasAnimatedDrawEventArgs
                {
                    DrawingSession = args.DrawingSession,
                    Timing = new CanvasTimingInformation { TotalTime = this.stopwatch.Elapsed }
                });
        }
        finally
        {
            this.isDrawing = 0;
        }
    }

    /// <inheritdoc/>
    Size ICanvasAnimatedControl.Size => this.canvasControl.Size;

    /// <inheritdoc/>
    int ICanvasAnimatedControl.ConvertDipsToPixels(float dips, CanvasDpiRounding dpiRounding)
    {
        return this.canvasControl.ConvertDipsToPixels(dips, dpiRounding);
    }
}