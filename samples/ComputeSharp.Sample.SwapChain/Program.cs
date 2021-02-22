using System;
using System.Drawing;
using ComputeSharp.Interop;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Sample.SwapChain
{
    class Program
    {
        static void Main(string[] args)
        {
            Win32ApplicationRunner.Run<FractalTilesApplication>();
        }
    }

    internal sealed class FractalTilesApplication : Win32Application
    {
        /// <summary>
        /// The <see cref="ID3D12Device"/> pointer for the device currently in use.
        /// </summary>
        private ComPtr<ID3D12Device> d3D12Device;

        /// <summary>
        /// The <see cref="ID3D12CommandQueue"/> instance to use for graphics operations.
        /// </summary>
        private ComPtr<ID3D12CommandQueue> d3D12CommandQueue;

        /// <summary>
        /// The <see cref="ID3D12Fence"/> instance used for graphics operations.
        /// </summary>
        private ComPtr<ID3D12Fence> d3D12Fence;

        /// <summary>
        /// The next fence value for graphics operations using <see cref="d3D12CommandQueue"/>.
        /// </summary>
        //private ulong nextD3D12FenceValue = 1;

        /// <summary>
        /// The <see cref="IDXGISwapChain1"/> instance used to display content onto the target window.
        /// </summary>
        private ComPtr<IDXGISwapChain1> dxgiSwapChain1;

        /// <summary>
        /// The first buffer within <see cref="dxgiSwapChain1"/>.
        /// </summary>
        private ComPtr<ID3D12Resource> d3D12Resource0;

        /// <summary>
        /// The second buffer within <see cref="dxgiSwapChain1"/>.
        /// </summary>
        private ComPtr<ID3D12Resource> d3D12Resource1;

        /// <summary>
        /// The index of the next buffer that can be used to present content.
        /// </summary>
        private uint currentBufferIndex;

        public override string Title => "Fractal tiles";

        public override unsafe void OnInitialize(Size size, HWND hwnd)
        {
            // Get the underlying ID3D12Device in use
            fixed (ID3D12Device** d3D12Device = this.d3D12Device)
            {
                _ = InteropServices.TryGetID3D12Device(Gpu.Default, FX.__uuidof<ID3D12Device>(), (void**)d3D12Device);
            }

            // Create the direct command queue to use
            fixed (ID3D12CommandQueue** d3D12CommandQueue = this.d3D12CommandQueue)
            {
                D3D12_COMMAND_QUEUE_DESC d3D12CommandQueueDesc;
                d3D12CommandQueueDesc.Type = D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT;
                d3D12CommandQueueDesc.Priority = (int)D3D12_COMMAND_QUEUE_PRIORITY.D3D12_COMMAND_QUEUE_PRIORITY_NORMAL;
                d3D12CommandQueueDesc.Flags = D3D12_COMMAND_QUEUE_FLAGS.D3D12_COMMAND_QUEUE_FLAG_NONE;
                d3D12CommandQueueDesc.NodeMask = 0;

                _ = d3D12Device.Get()->CreateCommandQueue(
                    &d3D12CommandQueueDesc,
                    FX.__uuidof<ID3D12CommandQueue>(),
                    (void**)d3D12CommandQueue);
            }

            // Create the direct fence
            fixed (ID3D12Fence** d3D12Fence = this.d3D12Fence)
            {
                _ = this.d3D12Device.Get()->CreateFence(
                    0,
                    D3D12_FENCE_FLAGS.D3D12_FENCE_FLAG_NONE,
                    FX.__uuidof<ID3D12Fence>(),
                    (void**)d3D12Fence);
            }

            // Create the swap chain to display frames
            fixed (IDXGISwapChain1** dxgiSwapChain1 = this.dxgiSwapChain1)
            {
                using ComPtr<IDXGIFactory2> dxgiFactory2 = default;

                _ = FX.CreateDXGIFactory2(FX.DXGI_CREATE_FACTORY_DEBUG, FX.__uuidof<IDXGIFactory2>(), (void**)dxgiFactory2.GetAddressOf());

                DXGI_SWAP_CHAIN_DESC1 dxgiSwapChainDesc1 = default;
                dxgiSwapChainDesc1.AlphaMode = DXGI_ALPHA_MODE.DXGI_ALPHA_MODE_IGNORE;
                dxgiSwapChainDesc1.BufferCount = 2;
                dxgiSwapChainDesc1.Flags = 0;
                dxgiSwapChainDesc1.Format = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM;
                dxgiSwapChainDesc1.Width = 0;
                dxgiSwapChainDesc1.Height = 0;
                dxgiSwapChainDesc1.SampleDesc = new DXGI_SAMPLE_DESC(count: 1, quality: 0);
                dxgiSwapChainDesc1.Scaling = DXGI_SCALING.DXGI_SCALING_STRETCH;
                dxgiSwapChainDesc1.Stereo = 0;
                dxgiSwapChainDesc1.SwapEffect = DXGI_SWAP_EFFECT.DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL;

                _ = dxgiFactory2.Get()->CreateSwapChainForHwnd(
                    (IUnknown*)d3D12CommandQueue.Get(),
                    hwnd,
                    &dxgiSwapChainDesc1,
                    null,
                    null,
                    dxgiSwapChain1);
            }

            // Retrieve the back buffers for the swap chain
            fixed (ID3D12Resource** d3D12Resource0 = this.d3D12Resource0)
            fixed (ID3D12Resource** d3D12Resource1 = this.d3D12Resource1)
            {
                _ = dxgiSwapChain1.Get()->GetBuffer(0, FX.__uuidof<ID3D12Resource>(), (void**)d3D12Resource0);
                _ = dxgiSwapChain1.Get()->GetBuffer(1, FX.__uuidof<ID3D12Resource>(), (void**)d3D12Resource1);
            }

            // Get the index of the current buffer
            using ComPtr<IDXGISwapChain3> dxgiSwapChain3 = default;

            _ = this.dxgiSwapChain1.CopyTo(dxgiSwapChain3.GetAddressOf());

            this.currentBufferIndex = dxgiSwapChain3.Get()->GetCurrentBackBufferIndex();
        }

        public override void OnResize(Size size)
        {
        }

        public override void OnUpdate(TimeSpan time)
        {
        }

        public override void Dispose()
        {
        }
    }
}
