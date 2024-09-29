using Microsoft.UI.Xaml.Controls;

namespace ComputeSharp.SwapChain.Core.Converters;

/// <summary>
/// A class with some static converters for rendering state.
/// </summary>
public static class RenderingPauseConverter
{
    /// <summary>
    /// Gets a symbol for an input rendering state.
    /// </summary>
    /// <param name="value">Whether or not the rendering is currently paused.</param>
    /// <returns>A symbol representing the next action for the rendering.</returns>
    public static Symbol ConvertPausedToSymbol(bool value)
    {
        return value ? Symbol.Play : Symbol.Pause;
    }
}