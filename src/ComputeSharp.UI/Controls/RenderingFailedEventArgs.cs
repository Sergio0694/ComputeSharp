using System;

#if WINDOWS_UWP
namespace ComputeSharp.Uwp;
#else
namespace ComputeSharp.WinUI;
#endif

/// <summary>
/// The arguments for the <see cref="AnimatedComputeShaderPanel.RenderingFailed"/> and <see cref="ComputeShaderPanel.RenderingFailed"/> events.
/// </summary>
public sealed class RenderingFailedEventArgs : EventArgs
{
    /// <summary>
    /// Creates a new <see cref="RenderingFailedEventArgs"/> instance with the specified arguments.
    /// </summary>
    /// <param name="exception">The <see cref="System.Exception"/> that caused the rendering to fail.</param>
    internal RenderingFailedEventArgs(Exception exception)
    {
        Exception = exception;
    }

    /// <summary>
    /// Gets the <see cref="System.Exception"/> that caused the rendering to fail.
    /// </summary>
    public Exception Exception { get; }
}