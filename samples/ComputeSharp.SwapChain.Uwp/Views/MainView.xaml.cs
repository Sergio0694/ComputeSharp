using System;
using CommunityToolkit.Mvvm.DependencyInjection;
using ComputeSharp.SwapChain.Core.Constants;
using ComputeSharp.SwapChain.Core.Services;
using ComputeSharp.SwapChain.Core.ViewModels;
using ComputeSharp.Uwp;
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
        this.InitializeComponent();
        this.DataContext = Ioc.Default.GetRequiredService<MainViewModel>();
    }

    /// <summary>
    /// Gets the <see cref="MainViewModel"/> instance for the current view.
    /// </summary>
    public MainViewModel ViewModel => (MainViewModel)DataContext;

    // Opens the shader selection panel
    private void OpenShaderSelectionPanelButton_Click(object sender, RoutedEventArgs e)
    {
        Ioc.Default.GetRequiredService<IAnalyticsService>().Log(Event.OpenShaderSelectionPanel);

        Root.Resources.Remove("ShaderSelectionPanel");
        Root.Children.Add(ShaderSelectionPanel);
    }

    // Hides the shader selection panel
    private void ShaderSelectionPanel_Tapped(object sender, TappedRoutedEventArgs e)
    {
        Ioc.Default.GetRequiredService<IAnalyticsService>().Log(Event.CloseShaderSelectionPanel);

        Root.Children.Remove(ShaderSelectionPanel);
    }

    // Updates the size of the shaders list panel
    private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        ShadersListContainerPanel.Height = Math.Round(e.NewSize.Height * 0.35);
    }

    // Logs rendering failed in the main panel
    private void MainShaderPanel_RenderingFailed(AnimatedComputeShaderPanel sender, Exception args)
    {
        Ioc.Default.GetRequiredService<IAnalyticsService>().Log(Event.RenderingFailedOnMainPanel, args);
    }

    // Logs rendering failed in a secondary panel
    private void SelectionShaderPanel_RenderingFailed(AnimatedComputeShaderPanel sender, Exception args)
    {
        Ioc.Default.GetRequiredService<IAnalyticsService>().Log(Event.RenderingFailedOnSelectionPanel, args);
    }
}
