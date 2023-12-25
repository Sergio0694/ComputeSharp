using System;
using Microsoft.UI.Xaml;
using Microsoft.VisualStudio.TestPlatform.TestExecutor;

namespace ComputeSharp.D2D1.WinUI.Tests;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Creates a new <see cref="App"/> instance.
    /// </summary>
    public App()
    {
        InitializeComponent();
    }

    /// <inheritdoc/>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        UnitTestClient.Run(Environment.CommandLine);
    }
}