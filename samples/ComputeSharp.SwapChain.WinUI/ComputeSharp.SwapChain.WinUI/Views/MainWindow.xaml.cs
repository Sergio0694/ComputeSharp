using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace ComputeSharp.SwapChain.WinUI.Views
{
    /// <summary>
    /// A view for <see cref="MainViewModel"/>.
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
    }
}
