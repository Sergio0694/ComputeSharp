using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.DependencyInjection;
using ComputeSharp.SwapChain.Core.Constants;
using ComputeSharp.SwapChain.Core.Services;
using ComputeSharp.SwapChain.Core.ViewModels;

#if WINDOWS_UWP
using ComputeSharp.Uwp;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
#else
using ComputeSharp.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
#endif

namespace ComputeSharp.SwapChain.Views;

/// <summary>
/// A view for <see cref="Core.ViewModels.MainViewModel"/>.
/// </summary>
public sealed partial class MainView : UserControl
{
    /// <summary>
    /// The mapping of currently alive shader panels.
    /// </summary>
    private readonly ConditionalWeakTable<AnimatedComputeShaderPanel, object?> shaderPanels = new();

    public MainView()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<MainViewModel>();
    }

    /// <summary>
    /// Gets the <see cref="MainViewModel"/> instance for the current view.
    /// </summary>
    public MainViewModel ViewModel => (MainViewModel)DataContext;

    // Opens the shader selection panel
    private void OpenShaderSelectionPanelButton_Click(object sender, RoutedEventArgs e)
    {
        Ioc.Default.GetRequiredService<IAnalyticsService>().Log(Event.OpenShaderSelectionPanel);

        _ = this.Root.Resources.Remove("ShaderSelectionPanel");
        this.Root.Children.Add(this.ShaderSelectionPanel);
    }

    // Hides the shader selection panel
    private void ShaderSelectionPanel_Tapped(object sender, TappedRoutedEventArgs e)
    {
        Ioc.Default.GetRequiredService<IAnalyticsService>().Log(Event.CloseShaderSelectionPanel);

        _ = this.Root.Children.Remove(this.ShaderSelectionPanel);
    }

    // Updates the size of the shaders list panel
    private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        this.ShadersListContainerPanel.Height = Math.Round(e.NewSize.Height * 0.35);
    }

    // Logs rendering failed in the main panel
    private void MainShaderPanel_RenderingFailed(AnimatedComputeShaderPanel sender, RenderingFailedEventArgs args)
    {
        Ioc.Default.GetRequiredService<IAnalyticsService>().Log(args.Exception, (nameof(Error), Error.RenderingFailedOnMainPanel));

        this.RenderingErrorInfoBar.IsOpen = true;
    }

    // Logs rendering failed in a secondary panel
    private void SelectionShaderPanel_RenderingFailed(AnimatedComputeShaderPanel sender, RenderingFailedEventArgs args)
    {
        Ioc.Default.GetRequiredService<IAnalyticsService>().Log(args.Exception, (nameof(Error), Error.RenderingFailedOnSelectionPanel));

        this.RenderingErrorInfoBar.IsOpen = true;
    }

    /// <summary>
    /// Stops all rendering when the application is closing.
    /// </summary>
    public void OnShutdown()
    {
        this.ShaderPanel.ShaderRunner = null;

        foreach ((AnimatedComputeShaderPanel panel, object _) in this.shaderPanels)
        {
            panel.ShaderRunner = null;
        }
    }

    // Tracks the current panel
    private void AnimatedComputeShaderPanel_Loaded(object sender, RoutedEventArgs e)
    {
        this.shaderPanels.AddOrUpdate((AnimatedComputeShaderPanel)sender, null);
    }
}