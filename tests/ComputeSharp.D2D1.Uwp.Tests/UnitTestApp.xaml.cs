using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ComputeSharp.D2D1.Uwp.Tests;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public sealed partial class App : Application
{
    /// <summary>
    /// Creates a new <see cref="App"/> instance.
    /// </summary>
    public App()
    {
        InitializeComponent();
    }

    /// <inheritdoc/>
    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        Window.Current.Content ??= new Frame();

        Microsoft.VisualStudio.TestPlatform.TestExecutor.UnitTestClient.CreateDefaultUI();

        Window.Current.Activate();

        Microsoft.VisualStudio.TestPlatform.TestExecutor.UnitTestClient.Run(e.Arguments);
    }
}