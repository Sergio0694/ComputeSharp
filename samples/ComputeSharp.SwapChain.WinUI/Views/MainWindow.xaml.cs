using System;
using System.Runtime.CompilerServices;
using ComputeSharp.SwapChain.Core.Services;
using ComputeSharp.SwapChain.Core.ViewModels;
using ComputeSharp.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace ComputeSharp.SwapChain.WinUI.Views;

/// <summary>
/// A view for <see cref="MainViewModel"/>.
/// </summary>
public sealed partial class MainWindow : Window
{
    /// <summary>
    /// The mapping of currently alive shader panels.
    /// </summary>
    private readonly ConditionalWeakTable<AnimatedComputeShaderPanel, object?> shaderPanels = new();

    public MainWindow()
    {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(this.TitleBarRectangle);

        this.Root.DataContext = new MainViewModel(new DebugAnalyticsService());
    }

    /// <summary>
    /// Gets the <see cref="MainViewModel"/> instance for the current view.
    /// </summary>
    public MainViewModel ViewModel => (MainViewModel)this.Root.DataContext;

    /// <summary>
    /// Stops all rendering when the application is closing.
    /// </summary>
    public void OnShutdown()
    {
        this.ShaderPanel.ShaderRunner = null;

        foreach ((AnimatedComputeShaderPanel panel, object? _) in this.shaderPanels)
        {
            panel.ShaderRunner = null;
        }
    }

    // Opens the shader selection panel
    private void OpenShaderSelectionPanelButton_Click(object sender, RoutedEventArgs e)
    {
        _ = this.Root.Resources.Remove("ShaderSelectionPanel");

        this.Root.Children.Add(this.ShaderSelectionPanel);
    }

    // Hides the shader selection panel
    private void ShaderSelectionPanel_Tapped(object sender, TappedRoutedEventArgs e)
    {
        _ = this.Root.Children.Remove(this.ShaderSelectionPanel);
    }

    // Updates the size of the shaders list panel
    private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs e)
    {
        this.ShadersListContainerPanel.Height = Math.Round(e.Size.Height * 0.35);
    }

    // Tracks the current panel
    private void AnimatedComputeShaderPanel_Loaded(object sender, RoutedEventArgs e)
    {
        this.shaderPanels.AddOrUpdate((AnimatedComputeShaderPanel)sender, null);
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