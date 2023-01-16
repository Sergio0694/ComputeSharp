using System;
using ComputeSharp.SwapChain.Core.Constants;
using ComputeSharp.SwapChain.Core.Services;
using ComputeSharp.SwapChain.Core.ViewModels;
using ComputeSharp.Uwp;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace ComputeSharp.SwapChain.Uwp.Views;

/// <summary>
/// A view for <see cref="Core.ViewModels.MainViewModel"/>.
/// </summary>
public sealed partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        DataContext = App.Current.Services.GetRequiredService<MainViewModel>();
    }

    /// <summary>
    /// Gets the <see cref="MainViewModel"/> instance for the current view.
    /// </summary>
    public MainViewModel ViewModel => (MainViewModel)DataContext;

    // Opens the shader selection panel
    private void OpenShaderSelectionPanelButton_Click(object sender, RoutedEventArgs e)
    {
        App.Current.Services.GetRequiredService<IAnalyticsService>().Log(Event.OpenShaderSelectionPanel);

        _ = this.Root.Resources.Remove("ShaderSelectionPanel");

        this.Root.Children.Add(this.ShaderSelectionPanel);
    }

    // Hides the shader selection panel
    private void ShaderSelectionPanel_Tapped(object sender, TappedRoutedEventArgs e)
    {
        App.Current.Services.GetRequiredService<IAnalyticsService>().Log(Event.CloseShaderSelectionPanel);

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
        App.Current.Services.GetRequiredService<IAnalyticsService>().Log(args.Exception, (nameof(Error), Error.RenderingFailedOnMainPanel));

        this.RenderingErrorInfoBar.IsOpen = true;
    }

    // Logs rendering failed in a secondary panel
    private void SelectionShaderPanel_RenderingFailed(AnimatedComputeShaderPanel sender, RenderingFailedEventArgs args)
    {
        App.Current.Services.GetRequiredService<IAnalyticsService>().Log(args.Exception, (nameof(Error), Error.RenderingFailedOnSelectionPanel));

        this.RenderingErrorInfoBar.IsOpen = true;
    }
}