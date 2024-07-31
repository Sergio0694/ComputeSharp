using System.Diagnostics.CodeAnalysis;
using ComputeSharp.SwapChain.D2D1.Backend;
using ComputeSharp.SwapChain.D2D1.Extensions;
using ComputeSharp.SwapChain.Shaders.D2D1;
using Windows.ApplicationModel.Core;
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

        PixelShaderEffect effect = new PixelShaderEffect.For<ColorfulInfinity>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)));

        CoreWindowApplication application = new();

        application.Draw += (s, e) =>
        {
            // Set the effect properties
            effect.ElapsedTime = e.TotalTime;
            effect.ScreenWidth = (int)e.ScreenWidth;
            effect.ScreenHeight = (int)e.ScreenHeight;

            // Draw the effect
            e.DrawingSession.DrawImage(effect);
        };

        CoreWindowApplicationRunner.Run(application, this.window!);
    }

    /// <inheritdoc/>
    public void Uninitialize()
    {
    }
}
