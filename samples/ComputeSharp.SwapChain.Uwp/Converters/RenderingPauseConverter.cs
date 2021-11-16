using Windows.UI.Xaml.Controls;

namespace ComputeSharp.SwapChain.Uwp.Converters
{
    /// <summary>
    /// A class with some static converters for rendering state.
    /// </summary>
    public static class RenderingPauseConverter
    {
        /// <summary>
        /// Gets a symbol for an input rendering state.
        /// </summary>
        /// <param name="value">Whether or not the rendering is currently paaused.</param>
        /// <returns>A symbol representing the next action for the rendering.</returns>
        public static Symbol ConvertPausedToSymbol(bool value)
        {
            return value ? Symbol.Play : Symbol.Pause;
        }
    }
}