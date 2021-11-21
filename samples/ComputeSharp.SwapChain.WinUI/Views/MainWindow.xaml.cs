using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace ComputeSharp.SwapChain.WinUI.Views;

/// <summary>
/// A view for <see cref="Core.ViewModels.MainViewModel"/>.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        this.ExtendsContentIntoTitleBar = true;

        SetTitleBar(TitleBarRectangle);
    }

    // Opens the shader selection panel
    private void OpenShaderSelectionPanelButton_Click(object sender, RoutedEventArgs e)
    {
        Root.Resources.Remove("ShaderSelectionPanel");
        Root.Children.Add(ShaderSelectionPanel);
    }

    // Hides the shader selection panel
    private void ShaderSelectionPanel_Tapped(object sender, TappedRoutedEventArgs e)
    {
        Root.Children.Remove(ShaderSelectionPanel);
    }

    // Updates the size of the shaders list panel
    private void UserControl_SizeChanged(object sender, WindowSizeChangedEventArgs e)
    {
        ShadersListContainerPanel.Height = Math.Round(e.Size.Height * 0.35);
    }

    // Closes the app
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Exit();
    }
}
