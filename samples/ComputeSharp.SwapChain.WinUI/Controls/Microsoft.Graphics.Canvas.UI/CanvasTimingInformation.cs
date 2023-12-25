using System;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace Microsoft.Graphics.Canvas.UI;

/// <summary>
/// Contains information about a <see cref="CanvasAnimatedControl"/>'s timer.
/// </summary>
public readonly struct CanvasTimingInformation
{
    /// <summary>
    /// Represents the elapsed time, in ticks, for which this control has ever been running.
    /// </summary>
    public required TimeSpan TotalTime { get; init; }
}