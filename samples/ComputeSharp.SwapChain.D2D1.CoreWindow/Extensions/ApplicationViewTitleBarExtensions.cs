using Windows.UI;
using Windows.UI.ViewManagement;

namespace ComputeSharp.SwapChain.D2D1.Extensions;

/// <summary>
/// Extensions for <see cref="ApplicationViewTitleBar"/>.
/// </summary>
internal static class ApplicationViewTitleBarExtensions
{
    /// <summary>
    /// Styles the title bar to look correctly when the view is expanded into it.
    /// </summary>
    /// <param name="titleBar">The input title bar.</param>
    public static void StyleTitleBarForExtendedIntoViewMode(this ApplicationViewTitleBar titleBar)
    {
        // Transparent colors
        titleBar.ForegroundColor = Colors.Transparent;
        titleBar.BackgroundColor = Colors.Transparent;
        titleBar.ButtonBackgroundColor = Colors.Transparent;
        titleBar.InactiveBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        // Theme aware colors
        titleBar.ButtonForegroundColor = Colors.White;
        titleBar.ButtonHoverForegroundColor = Colors.White;
        titleBar.ButtonPressedForegroundColor = Colors.White;
        titleBar.ButtonHoverBackgroundColor = Color.FromArgb(0x20, 0xFF, 0xFF, 0xFF);
        titleBar.ButtonPressedBackgroundColor = Color.FromArgb(0x40, 0xFF, 0xFF, 0xFF);
        titleBar.ButtonInactiveForegroundColor = Color.FromArgb(0xC0, 0xFF, 0xFF, 0xFF);
        titleBar.InactiveForegroundColor = Color.FromArgb(0xA0, 0xA0, 0xA0, 0xA0);
    }
}
