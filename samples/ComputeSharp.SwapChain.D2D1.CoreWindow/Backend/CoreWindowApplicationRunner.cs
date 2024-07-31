using System.Diagnostics;
using System.Threading;
using Windows.UI.Core;

namespace ComputeSharp.SwapChain.D2D1.Backend;

/// <summary>
/// A helper class to manage the creation and execution of <see cref="CoreWindow"/> applications.
/// </summary>
internal static class CoreWindowApplicationRunner
{
    /// <summary>
    /// Runs a specified application and starts the main loop to update its state. This is the entry point for a given application,
    /// and it should be called as soon as the process is launched, excluding any other additional initialization needed.
    /// </summary>
    /// <param name="application">The input application instance to run.</param>
    /// <param name="window">The <see cref="CoreWindow"/> instance.</param>
    public static void Run(CoreWindowApplication application, CoreWindow window)
    {
        // Initialize the application
        application.OnInitialize(window);

        // Setup the render thread that enables smooth resizing of the window
        Thread renderThread = new(static args =>
        {
            (CoreWindowApplication application, CancellationToken token) = ((CoreWindowApplication, CancellationToken))args!;

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

        // Activate the window
        window.Activate();

        // Process any messages in the queue
        window.Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessUntilQuit);

        tokenSource.Cancel();

        // Wait for the render thread to stop before terminating
        renderThread.Join();
    }
}