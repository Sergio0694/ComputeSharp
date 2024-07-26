using System.Diagnostics.CodeAnalysis;
using Windows.ApplicationModel.Core;

#pragma warning disable IDE0052

namespace ComputeSharp.SwapChain.D2D1.CoreWindow;

using Windows.UI.Core;

/// <summary>
/// A sample app rendering D2D shaders into a <see cref="CoreWindow"/> instance.
/// </summary>
public sealed partial class App : IFrameworkViewSource, IFrameworkView
{
    private CoreApplicationView? applicationView;
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
        this.window!.Activate();

        this.window.Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessUntilQuit);
    }

    /// <inheritdoc/>
    public void Uninitialize()
    {
    }
}
