using System;

namespace DirectX12GameEngine.Graphics
{
    public abstract class GraphicsPresenter : IDisposable
    {
        protected GraphicsPresenter(GraphicsDevice device, PresentationParameters presentationParameters)
        {
            GraphicsDevice = device;
            PresentationParameters = presentationParameters;
        }

        public GraphicsDevice GraphicsDevice { get; }

        public abstract object NativePresenter { get; }

        public PresentationParameters PresentationParameters { get; }

        public virtual void BeginDraw(CommandList commandList)
        {
        }

        public virtual void Dispose()
        {
        }

        public abstract void Present();

        public void Resize(int width, int height)
        {
            PresentationParameters.BackBufferWidth = width;
            PresentationParameters.BackBufferHeight = height;

            ResizeBackBuffer(width, height);
        }

        protected abstract void ResizeBackBuffer(int width, int height);
    }
}
