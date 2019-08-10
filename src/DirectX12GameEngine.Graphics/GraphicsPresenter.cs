using System;

namespace DirectX12GameEngine.Graphics
{
    public abstract class GraphicsPresenter : IDisposable
    {
        protected GraphicsPresenter(GraphicsDevice device, PresentationParameters presentationParameters)
        {
            GraphicsDevice = device;
            PresentationParameters = presentationParameters;

            DepthStencilBuffer = CreateDepthStencilBuffer();
        }

        public abstract Texture BackBuffer { get; }

        public GraphicsDevice GraphicsDevice { get; }

        public abstract object NativePresenter { get; }

        public PresentationParameters PresentationParameters { get; }

        public Texture DepthStencilBuffer { get; protected set; }

        public virtual void BeginDraw(CommandList commandList)
        {
        }

        public virtual void Dispose()
        {
            DepthStencilBuffer.Dispose();
        }

        public abstract void Present();

        public void Resize(int width, int height)
        {
            PresentationParameters.BackBufferWidth = width;
            PresentationParameters.BackBufferHeight = height;

            ResizeBackBuffer(width, height);
            ResizeDepthStencilBuffer(width, height);
        }

        protected virtual Texture CreateDepthStencilBuffer()
        {
            return Texture.New2D(GraphicsDevice,
                PresentationParameters.BackBufferWidth, PresentationParameters.BackBufferHeight, PresentationParameters.DepthStencilFormat,
                TextureFlags.DepthStencil, 1, PresentationParameters.Stereo ? 2 : 1);
        }

        protected abstract void ResizeBackBuffer(int width, int height);

        protected abstract void ResizeDepthStencilBuffer(int width, int height);
    }
}
