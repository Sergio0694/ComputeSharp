using System.Diagnostics.CodeAnalysis;
using ComputeSharp.SwapChain.D2D1.Backend;
using ComputeSharp.SwapChain.Shaders.D2D1;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

namespace ComputeSharp.SwapChain.D2D1.Uwp;

/// <summary>
/// Provides an implementation of the application type for the current app.
/// </summary>
public sealed class App : IFrameworkViewSource, IFrameworkView
{
    /// <summary>
    /// The <see cref="CoreWindow"/> instance for the current app.
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
    public void Initialize(CoreApplicationView applicationView)
    {
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
        StyleTitleBar();
        ExpandViewIntoTitleBar();

        PixelShaderEffect effect = new PixelShaderEffect.For<ColorfulInfinity>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)));

        CoreWindowApplication coreWindowApplication = new();

        coreWindowApplication.Draw += (_, e) =>
        {
            // Set the effect properties
            effect.ElapsedTime = e.TotalTime;
            effect.ScreenWidth = (int)e.ScreenWidth;
            effect.ScreenHeight = (int)e.ScreenHeight;

            // Draw the effect
            e.DrawingSession.DrawImage(effect);
        };

        CoreWindowApplicationRunner.Run(this.window!, coreWindowApplication);
    }

    /// <inheritdoc/>
    public void Uninitialize()
    {
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
