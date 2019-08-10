using System.Numerics;
using SharpDX.Direct3D12;
using SharpDX.DXGI;

using Resource = SharpDX.Direct3D12.Resource;

namespace DirectX12GameEngine.Graphics
{
    public class SwapChainGraphicsPresenter : GraphicsPresenter
    {
        protected const int BufferCount = 2;

        private readonly Texture[] renderTargets = new Texture[BufferCount];

        public SwapChainGraphicsPresenter(GraphicsDevice device, PresentationParameters presentationParameters, SwapChain3 swapChain)
            : base(device, presentationParameters)
        {
            SwapChain = swapChain;

            if (GraphicsDevice.RenderTargetViewAllocator.DescriptorHeap.Description.DescriptorCount != BufferCount)
            {
                GraphicsDevice.RenderTargetViewAllocator.Dispose();
                GraphicsDevice.RenderTargetViewAllocator = new DescriptorAllocator(GraphicsDevice, DescriptorHeapType.RenderTargetView, descriptorCount: BufferCount);
            }

            CreateRenderTargets();
        }

        public override Texture BackBuffer => renderTargets[SwapChain.CurrentBackBufferIndex];

        public Matrix3x2 MatrixTransform { get => SwapChain.MatrixTransform.ToMatrix3x2(); set => SwapChain.MatrixTransform = value.ToMatrix3x2(); }

        public override object NativePresenter => SwapChain;

        protected SwapChain3 SwapChain { get; }

        public override void Dispose()
        {
            SwapChain.Dispose();

            foreach (Texture renderTarget in renderTargets)
            {
                renderTarget.Dispose();
            }

            base.Dispose();
        }

        public override void Present()
        {
            SwapChain.Present(PresentationParameters.SyncInterval, PresentFlags.None, PresentationParameters.PresentParameters);
        }

        protected override void ResizeBackBuffer(int width, int height)
        {
            for (int i = 0; i < BufferCount; i++)
            {
                renderTargets[i].Dispose();
            }

            SwapChain.ResizeBuffers(BufferCount, width, height, (Format)PresentationParameters.BackBufferFormat, SwapChainFlags.None);

            CreateRenderTargets();
        }

        protected override void ResizeDepthStencilBuffer(int width, int height)
        {
            DepthStencilBuffer.Dispose();
            DepthStencilBuffer = CreateDepthStencilBuffer();
        }

        private void CreateRenderTargets()
        {
            for (int i = 0; i < BufferCount; i++)
            {
                renderTargets[i] = new Texture(GraphicsDevice).InitializeFrom(SwapChain.GetBackBuffer<Resource>(i));
            }
        }
    }

    internal static class MatrixExtensions
    {
        public static unsafe SharpDX.Mathematics.Interop.RawMatrix3x2 ToMatrix3x2(this Matrix3x2 value)
        {
            return *(SharpDX.Mathematics.Interop.RawMatrix3x2*)&value;
        }

        public static unsafe Matrix3x2 ToMatrix3x2(this SharpDX.Mathematics.Interop.RawMatrix3x2 value)
        {
            return *(Matrix3x2*)&value;
        }
    }
}
