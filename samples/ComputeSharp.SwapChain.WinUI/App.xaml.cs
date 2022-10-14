using System;
using System.Threading;
using ComputeSharp.SwapChain.WinUI.Views;
using Microsoft.UI.Xaml;

#pragma warning disable CA1001

namespace ComputeSharp.SwapChain.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public sealed partial class App : Application
{
    /// <summary>
    /// The main application window
    /// </summary>
    private MainWindow? mainWindow;

    /// <summary>
    /// A timer to force shutdown after a given amount of time.
    /// </summary>
    /// <remarks>This field is needed to keep the timer alive.</remarks>
    private Timer? shutdownTimer;

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();
    }

    /// <inheritdoc/>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        this.mainWindow = new MainWindow();
        this.mainWindow.Closed += (_, _) =>
        {
            // Due to an issue (https://discord.com/channels/372137812037730304/663434534087426129/979543152853663744), the WinUI 3 app
            // will keep running after the window is closed if any render thread is still going. To work around this, each loaded shader
            // panel is tracked in a conditional weak table, and then when the window is closed they're all manually stopped by setting
            // their shader runners to null. After this, the app should just be able to close automatically when the panels are done.
            this.mainWindow.OnShutdown();

            // For good measure, also start a timer of one second. If the app is still alive by then (ie. if the timer callback is
            // executed at all), then just force the whole process to terminate, and exit with the E_APPLICATION_EXITING error code.
            this.shutdownTimer = new Timer(_ => Environment.Exit(unchecked((int)(0x8000001A))), this, 1000, Timeout.Infinite);
        };

        this.mainWindow.Activate();
    }
}
