namespace Microsoft.Graphics.Canvas.UI.Xaml;

/// <summary>
/// Provides data for the <see cref="CanvasAnimatedControl.Draw"/> event.
/// </summary>
public sealed class CanvasAnimatedDrawEventArgs
{
    /// <inheritdoc cref="CanvasDrawEventArgs.DrawingSession"/>
    public required CanvasDrawingSession DrawingSession { get; init; }

    /// <summary>
    /// Gets the control's timing information for use by the event handler.
    /// </summary>
    public required CanvasTimingInformation Timing { get; init; }
}