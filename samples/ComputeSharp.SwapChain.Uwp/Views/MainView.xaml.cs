using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace ComputeSharp.SwapChain.Uwp.Views;

public sealed partial class MainView : UserControl
{
    public MainView()
    {
        this.InitializeComponent();
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
}
