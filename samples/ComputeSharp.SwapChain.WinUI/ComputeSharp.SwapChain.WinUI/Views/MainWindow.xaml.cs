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
            ShaderSelectionPanel.Visibility = Visibility.Visible;
        }

        // Hides the shader selection panel
        private void ShaderSelectionPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ShaderSelectionPanel.Visibility = Visibility.Collapsed;
        }
    }
}
