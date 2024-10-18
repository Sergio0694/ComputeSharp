using System.Diagnostics;
using System.Threading;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Windowing;
using Windows.UI;

namespace ComputeSharp.SwapChain.D2D1.Backend;

/// <summary>
/// A helper class to manage the creation and execution of Win32 applications.
/// </summary>
internal static unsafe class Win32ApplicationRunner
{
    /// <summary>
    /// Runs a specified application and starts the main loop to update its state. This is the entry point for a given application,
    /// and it should be called as soon as the process is launched, excluding any other additional initialization needed.
    /// </summary>
    /// <param name="application">The input application instance to run.</param>
    /// <param name="windowTitle">The title to use for the window being opened.</param>
    public static void Run(Win32Application application, string windowTitle)
    {
        // Create the 'AppWindow' instance (a thin wrapper for an 'HWND')
        AppWindow appWindow = AppWindow.Create();

        appWindow.Title = windowTitle;
        appWindow.TitleBar.ExtendsContentIntoTitleBar = true;

        // Transparent colors for the title bar
        appWindow.TitleBar.ForegroundColor = Colors.Transparent;
        appWindow.TitleBar.BackgroundColor = Colors.Transparent;
        appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
        appWindow.TitleBar.InactiveBackgroundColor = Colors.Transparent;
        appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        // Theme aware colors for the title bar
        appWindow.TitleBar.ButtonForegroundColor = Colors.White;
        appWindow.TitleBar.ButtonHoverForegroundColor = Colors.White;
        appWindow.TitleBar.ButtonPressedForegroundColor = Colors.White;
        appWindow.TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(0x20, 0xFF, 0xFF, 0xFF);
        appWindow.TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(0x40, 0xFF, 0xFF, 0xFF);
        appWindow.TitleBar.ButtonInactiveForegroundColor = Color.FromArgb(0xC0, 0xFF, 0xFF, 0xFF);
        appWindow.TitleBar.InactiveForegroundColor = Color.FromArgb(0xA0, 0xA0, 0xA0, 0xA0);

        // Initialize the application
        application.OnInitialize(appWindow);

        // Setup the render thread that enables smooth resizing of the window
        Thread renderThread = new(static args =>
        {
            (Win32Application application, CancellationToken token) = ((Win32Application, CancellationToken))args!;

            Stopwatch stopwatch = Stopwatch.StartNew();

            while (!token.IsCancellationRequested)
            {
                application.OnUpdate(stopwatch.Elapsed);
            }
        });

        CancellationTokenSource tokenSource = new();

        // Start the render thread before activating the window.
        // This allows the app to show up while already drawing.
        renderThread.Start((application, tokenSource.Token));

        // Register a callback to monitor resize operations
        appWindow.Changed += (s, e) =>
        {
            if (e.DidSizeChange)
            {
                application.OnResize();
            }
        };

        DispatcherQueueController dispatcherQueueController = DispatcherQueueController.CreateOnCurrentThread();

        // Bind the window to a 'DispatcherQueue' instance, so it can flow the exit message
        appWindow.AssociateWithDispatcherQueue(dispatcherQueueController.DispatcherQueue);

        // Enqueue the exit message when the window is closed. This ensures that
        // the process actually terminates after closing the window, as expected.
        appWindow.Closing += static (s, e) => s.DispatcherQueue.EnqueueEventLoopExit();

        // Display the window
        appWindow.Show(activateWindow: true);

        // Process any messages in the queue
        dispatcherQueueController.DispatcherQueue.RunEventLoop(DispatcherRunOptions.QuitOnlyLocalLoop, deferral: null);

        tokenSource.Cancel();

        // Wait for the render thread to stop before terminating
        renderThread.Join();
    }
}