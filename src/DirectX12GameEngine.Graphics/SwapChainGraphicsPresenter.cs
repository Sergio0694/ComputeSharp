using System.Numerics;
using SharpDX.Direct3D12;
using SharpDX.DXGI;

using Resource = SharpDX.Direct3D12.Resource;

namespace DirectX12GameEngine.Graphics
{
    public class SwapChainGraphicsPresenter : GraphicsPresenter
    {
        protected const int BufferCount = 2;

        public SwapChainGraphicsPresenter(GraphicsDevice device, PresentationParameters presentationParameters, SwapChain3 swapChain)
            : base(device, presentationParameters)
        {
            SwapChain = swapChain;

            if (GraphicsDevice.RenderTargetViewAllocator.DescriptorHeap.Description.DescriptorCount != BufferCount)
            {
                GraphicsDevice.RenderTargetViewAllocator.Dispose();
                GraphicsDevice.RenderTargetViewAllocator = new DescriptorAllocator(GraphicsDevice, DescriptorHeapType.RenderTargetView, descriptorCount: BufferCount);
            }
        }

        public Matrix3x2 MatrixTransform { get => SwapChain.MatrixTransform.ToMatrix3x2(); set => SwapChain.MatrixTransform = value.ToMatrix3x2(); }

        public override object NativePresenter => SwapChain;

        protected SwapChain3 SwapChain { get; }

        public override void Dispose()
        {
            SwapChain.Dispose();

            base.Dispose();
        }

        public override void Present()
        {
            SwapChain.Present(PresentationParameters.SyncInterval, PresentFlags.None, PresentationParameters.PresentParameters);
        }

        protected override void ResizeBackBuffer(int width, int height)
        {

            SwapChain.ResizeBuffers(BufferCount, width, height, (Format)PresentationParameters.BackBufferFormat, SwapChainFlags.None);

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
