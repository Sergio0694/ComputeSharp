using System;
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
        InitializeComponent();
    }

    // Opens the shader selection panel
    private void OpenShaderSelectionPanelButton_Click(object sender, RoutedEventArgs e)
    {
        _ = this.Root.Resources.Remove(nameof(this.ShaderSelectionPanel));

        this.Root.Children.Add(this.ShaderSelectionPanel);
    }

    // Hides the shader selection panel
    private void ShaderSelectionPanel_Tapped(object sender, TappedRoutedEventArgs e)
    {
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
        this.RenderingErrorInfoBar.IsOpen = true;
    }

    // Logs rendering failed in a secondary panel
    private void SelectionShaderPanel_RenderingFailed(AnimatedComputeShaderPanel sender, RenderingFailedEventArgs args)
    {
        this.RenderingErrorInfoBar.IsOpen = true;
    }
}