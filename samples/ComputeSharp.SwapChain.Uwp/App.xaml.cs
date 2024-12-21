using ComputeSharp.SwapChain.Uwp.Views;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace ComputeSharp.SwapChain.Uwp;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
sealed partial class App : Application
{
    /// <summary>
    /// Initializes the singleton application object. This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();
    }

    /// <inheritdoc/>
    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        if (Window.Current.Content is not MainView)
        {
            StyleTitleBar();
            ExpandViewIntoTitleBar();

            Window.Current.Content = new MainView();
        }

        if (!e.PrelaunchActivated)
        {
            Window.Current.Activate();
        }
    }

    /// <summary>
    /// Styles the title bar buttons according to the theme in use
    /// </summary>
    private static void StyleTitleBar()
    {
        ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

        // Transparent colors
        titleBar.ForegroundColor = Colors.Transparent;
        titleBar.BackgroundColor = Colors.Transparent;
        titleBar.ButtonBackgroundColor = Colors.Transparent;
        titleBar.InactiveBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        // Theme aware colors
        titleBar.ButtonForegroundColor = titleBar.ButtonHoverForegroundColor = titleBar.ButtonPressedForegroundColor = Colors.White;
        titleBar.ButtonHoverBackgroundColor = Color.FromArgb(0x20, 0xFF, 0xFF, 0xFF);
        titleBar.ButtonPressedBackgroundColor = Color.FromArgb(0x40, 0xFF, 0xFF, 0xFF);
        titleBar.ButtonInactiveForegroundColor = Color.FromArgb(0xC0, 0xFF, 0xFF, 0xFF);
        titleBar.InactiveForegroundColor = Color.FromArgb(0xA0, 0xA0, 0xA0, 0xA0);
    }

    /// <summary>
    /// Sets up the app UI to be expanded into the title bar
    /// </summary>
    private static void ExpandViewIntoTitleBar()
    {
        CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

        coreTitleBar.ExtendViewIntoTitleBar = true;
    }
}