using ComputeSharp.SwapChain.Shaders;
using ComputeSharp.WinUI;
using Microsoft.UI.Xaml;

namespace ComputeSharp.SwapChain.WinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void Colors_Click(object sender, RoutedEventArgs e)
        {
            this.ShaderPanel.ShaderRunner = new ShaderRunner<ColorfulInfinity>(static (texture, time) => new(texture, (float)time.TotalSeconds));
        }

        private void ExtrudedTruchet_Click(object sender, RoutedEventArgs e)
        {
            this.ShaderPanel.ShaderRunner = new ShaderRunner<ExtrudedTruchetPattern>(static (texture, time) => new(texture, (float)time.TotalSeconds));
        }

        private void Tiles_Click(object sender, RoutedEventArgs e)
        {
            this.ShaderPanel.ShaderRunner = new ShaderRunner<FractalTiling>(static (texture, time) => new(texture, (float)time.TotalSeconds));
        }

        private void Journey_Click(object sender, RoutedEventArgs e)
        {
            this.ShaderPanel.ShaderRunner = new ShaderRunner<MengerJourney>(static (texture, time) => new(texture, (float)time.TotalSeconds));
        }

        private void Octagrams_Click(object sender, RoutedEventArgs e)
        {
            this.ShaderPanel.ShaderRunner = new ShaderRunner<Octagrams>(static (texture, time) => new(texture, (float)time.TotalSeconds));
        }

        private void Clouds_Click(object sender, RoutedEventArgs e)
        {
            this.ShaderPanel.ShaderRunner = new ShaderRunner<ProteanClouds>(static (texture, time) => new(texture, (float)time.TotalSeconds));
        }

        private void TiledTruched_Click(object sender, RoutedEventArgs e)
        {
            this.ShaderPanel.ShaderRunner = new ShaderRunner<TwoTiledTruchet>(static (texture, time) => new(texture, (float)time.TotalSeconds));
        }
    }
}
