using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using ComputeSharp.D2D1.Uwp;
using ComputeSharp.SwapChain.D2D1.Extensions;
using ComputeSharp.SwapChain.Shaders.D2D1;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

namespace ComputeSharp.SwapChain.D2D1;

/// <summary>
/// A sample app rendering D2D shaders into a <see cref="CoreWindow"/> instance.
/// </summary>
public sealed partial class App : IFrameworkViewSource, IFrameworkView
{
    /// <summary>
    /// The <see cref="CoreApplicationView"/> for the current app instance.
    /// </summary>
    private CoreApplicationView? applicationView;

    /// <summary>
    /// The <see cref="CoreWindow"/> used to display the app content.
    /// </summary>
    private CoreWindow? window;

    /// <summary>
    /// The entry point for the application.
    /// </summary>
    public static void Main()
    {
        CoreApplication.Run(new App());
    }

    /// <inheritdoc/>
    public IFrameworkView CreateView()
    {
        return this;
    }

    /// <inheritdoc/>
    [MemberNotNull(nameof(applicationView))]
    public void Initialize(CoreApplicationView applicationView)
    {
        this.applicationView = applicationView;
    }

    /// <inheritdoc/>
    [MemberNotNull(nameof(window))]
    public void SetWindow(CoreWindow window)
    {
        this.window = window;
    }

    /// <inheritdoc/>
    public void Load(string entryPoint)
    {
    }

    /// <inheritdoc/>
    public void Run()
    {
        this.applicationView!.TitleBar.ExtendViewIntoTitleBar = true;

        ApplicationView.GetForCurrentView().TitleBar.StyleTitleBarForExtendedIntoViewMode();

        CanvasSwapChain swapChain = CanvasSwapChain.CreateForCoreWindow(
            CanvasDevice.GetSharedDevice(),
            this.window,
            DisplayInformation.GetForCurrentView().LogicalDpi);

        int2 sizeInPixels = new((int)swapChain.SizeInPixels.Width, (int)swapChain.SizeInPixels.Height);
        CancellationTokenSource cancellationTokenSource = new();
        Stopwatch stopwatch = Stopwatch.StartNew();

        Thread thread = new(() =>
        {
            PixelShaderEffect<ColorfulInfinity> shader = new();
            PremultiplyEffect premultiply = new() { Source = shader };

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                using (CanvasDrawingSession session = swapChain.CreateDrawingSession(Colors.Transparent))
                {
                    shader.ConstantBuffer = new ColorfulInfinity((float)stopwatch.Elapsed.TotalSeconds, sizeInPixels);

                    session.DrawImage(premultiply);
                }

                swapChain.Present(syncInterval: 1);
            }
        });

        thread.Start();

        this.window!.Activate();

        this.window.Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessUntilQuit);
    }

    /// <inheritdoc/>
    public void Uninitialize()
    {
    }
}
