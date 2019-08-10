using SharpDX;
using SharpDX.DXGI;
using Windows.UI.Xaml.Controls;

namespace DirectX12GameEngine.Graphics
{
    public class XamlSwapChainGraphicsPresenter : SwapChainGraphicsPresenter
    {
        private readonly SwapChainPanel swapChainPanel;

        public XamlSwapChainGraphicsPresenter(GraphicsDevice device, PresentationParameters presentationParameters, SwapChainPanel swapChainPanel)
            : base(device, presentationParameters, CreateSwapChain(device, presentationParameters, swapChainPanel))
        {
            this.swapChainPanel = swapChainPanel;
        }

        protected override void ResizeBackBuffer(int width, int height)
        {
            MatrixTransform = new System.Numerics.Matrix3x2
            {
                M11 = 1.0f / swapChainPanel.CompositionScaleX,
                M22 = 1.0f / swapChainPanel.CompositionScaleY
            };

            base.ResizeBackBuffer(width, height);
        }

        private static SwapChain3 CreateSwapChain(GraphicsDevice device, PresentationParameters presentationParameters, SwapChainPanel swapChainPanel)
        {
            SwapChainDescription1 swapChainDescription = new SwapChainDescription1
            {
                Width = presentationParameters.BackBufferWidth,
                Height = presentationParameters.BackBufferHeight,
                SampleDescription = new SampleDescription(1, 0),
                Stereo = presentationParameters.Stereo,
                Usage = Usage.RenderTargetOutput,
                BufferCount = BufferCount,
                Scaling = Scaling.Stretch,
                SwapEffect = SwapEffect.FlipSequential,
                Format = (Format)presentationParameters.BackBufferFormat,
                Flags = SwapChainFlags.None,
                AlphaMode = AlphaMode.Unspecified
            };

            swapChainDescription.AlphaMode = AlphaMode.Premultiplied;

            using Factory4 factory = new Factory4();
            using ISwapChainPanelNative nativePanel = ComObject.As<ISwapChainPanelNative>(swapChainPanel);
            using SwapChain1 tempSwapChain = new SwapChain1(factory, device.NativeDirectCommandQueue, ref swapChainDescription);

            SwapChain3 swapChain = tempSwapChain.QueryInterface<SwapChain3>();
            nativePanel.SwapChain = swapChain;

            swapChain.MatrixTransform = new SharpDX.Mathematics.Interop.RawMatrix3x2
            {
                M11 = 1.0f / swapChainPanel.CompositionScaleX,
                M22 = 1.0f / swapChainPanel.CompositionScaleY
            };

            return swapChain;
        }
    }
}
