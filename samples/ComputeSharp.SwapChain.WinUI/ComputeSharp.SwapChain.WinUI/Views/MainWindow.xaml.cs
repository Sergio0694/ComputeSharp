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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShaderSelectionPanel.Visibility = Visibility.Visible;
        }

        private void ShaderSelectionPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ShaderSelectionPanel.Visibility = Visibility.Collapsed;
        }
    }
}
