using ComputeSharp.SwapChain.WinUI.Views;
using Microsoft.UI.Xaml;

namespace ComputeSharp.SwapChain.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// The main application window
    /// </summary>
    private Window? mainWindow;

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    /// <inheritdoc/>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        mainWindow = new MainWindow();
        mainWindow.Activate();

        void OnClosed(object s, WindowEventArgs e)
        {
            mainWindow.Closed -= OnClosed;

            Exit();
        }

        mainWindow.Closed += OnClosed;

    }
}
