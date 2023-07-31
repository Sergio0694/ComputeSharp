using System.Diagnostics.CodeAnalysis;
using ComputeSharp.SwapChain.D2D1.Backend;
using ComputeSharp.SwapChain.Shaders.D2D1;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

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
        PixelShaderEffect effect = new PixelShaderEffect.For<HelloWorld>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)));

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
}
