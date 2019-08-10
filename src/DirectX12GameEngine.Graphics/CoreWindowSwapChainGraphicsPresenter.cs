using SharpDX;
using SharpDX.DXGI;
using Windows.UI.Core;

namespace DirectX12GameEngine.Graphics
{
    public class CoreWindowSwapChainGraphicsPresenter : SwapChainGraphicsPresenter
    {
        private readonly CoreWindow coreWindow;

        public CoreWindowSwapChainGraphicsPresenter(GraphicsDevice device, PresentationParameters presentationParameters, CoreWindow coreWindow)
            : base(device, presentationParameters, CreateSwapChain(device, presentationParameters, coreWindow))
        {
            this.coreWindow = coreWindow;
        }

        private static SwapChain3 CreateSwapChain(GraphicsDevice device, PresentationParameters presentationParameters, CoreWindow coreWindow)
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

            using Factory4 factory = new Factory4();
            using ComObject window = new ComObject(coreWindow);
            using SwapChain1 tempSwapChain = new SwapChain1(factory, device.NativeDirectCommandQueue, window, ref swapChainDescription);

            return tempSwapChain.QueryInterface<SwapChain3>();
        }
    }
}
