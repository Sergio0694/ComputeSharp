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
                dxgiSwapChainDesc1.Format = DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM;
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

            // ==============
            // SAMPLE
            // ==============

            var desc = this.d3D12Resource0.Get()->GetDesc();

            int width = (int)desc.Width;
            int height = (int)desc.Height;

            using ReadWriteTexture2D<Rgba32, Float4> texture = Gpu.Default.AllocateReadWriteTexture2D<Rgba32, Float4>(width, height);

            Gpu.Default.For(texture.Width, texture.Height, new FractalTiling(texture, 0));

            ID3D12Resource* resource;

            _ = InteropServices.TryGetID3D12Resource(texture, FX.__uuidof<ID3D12Resource>(), (void**)&resource);

            using ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator = default;

            this.d3D12Device.Get()->CreateCommandAllocator(
                 D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT,
                 FX.__uuidof<ID3D12CommandAllocator>(),
                 (void**)d3D12CommandAllocator.GetAddressOf());

            using ComPtr<ID3D12GraphicsCommandList> d3D12GraphicsCommandList = default;

            this.d3D12Device.Get()->CreateCommandList(
                0,
                D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT,
                d3D12CommandAllocator,
                null,
                FX.__uuidof<ID3D12GraphicsCommandList>(),
                (void**)d3D12GraphicsCommandList.GetAddressOf());

            D3D12_RESOURCE_BARRIER d3D12ResourceBarrierBefore = D3D12_RESOURCE_BARRIER.InitTransition(
                resource,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_UNORDERED_ACCESS,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_SOURCE);

            d3D12GraphicsCommandList.Get()->ResourceBarrier(1, &d3D12ResourceBarrierBefore);

            D3D12_RESOURCE_BARRIER d3D12ResourceBarrierBefore0 = D3D12_RESOURCE_BARRIER.InitTransition(
                this.d3D12Resource0.Get(),
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COMMON,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_DEST);

            d3D12GraphicsCommandList.Get()->ResourceBarrier(1, &d3D12ResourceBarrierBefore0);

            d3D12GraphicsCommandList.Get()->CopyResource(this.d3D12Resource0.Get(), resource);

            D3D12_RESOURCE_BARRIER d3D12ResourceBarrierAfter = D3D12_RESOURCE_BARRIER.InitTransition(
                resource,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_SOURCE,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_UNORDERED_ACCESS);

            d3D12GraphicsCommandList.Get()->ResourceBarrier(1, &d3D12ResourceBarrierAfter);

            D3D12_RESOURCE_BARRIER d3D12ResourceBarrierAfter0 = D3D12_RESOURCE_BARRIER.InitTransition(
                this.d3D12Resource0.Get(),
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_DEST,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COMMON);

            d3D12GraphicsCommandList.Get()->ResourceBarrier(1, &d3D12ResourceBarrierAfter0);

            d3D12GraphicsCommandList.Get()->Close();

            this.d3D12CommandQueue.Get()->ExecuteCommandLists(1, (ID3D12CommandList**)d3D12GraphicsCommandList.GetAddressOf());

            this.dxgiSwapChain1.Get()->Present(0, 0);
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

    [AutoConstructor]
    internal readonly partial struct FractalTiling : IComputeShader
    {
        public readonly IReadWriteTexture2D<Float4> texture;
        public readonly float time;

        /// <inheritdoc/>
        public void Execute()
        {
            Float2 position = ((Float2)(256 * ThreadIds.XY)) / texture.Width + time;
            Float4 color = 0;

            for (int i = 0; i < 6; i++)
            {
                Float2 a = Hlsl.Floor(position);
                Float2 b = Hlsl.Frac(position);
                Float4 w = Hlsl.Frac(
                    (Hlsl.Sin(a.X * 7 + 31.0f * a.Y + 0.01f * time) +
                     new Float4(0.035f, 0.01f, 0, 0.7f))
                     * 13.545317f);

                color.XYZ += w.XYZ *
                       2.0f * Hlsl.SmoothStep(0.45f, 0.55f, w.W) *
                       Hlsl.Sqrt(16.0f * b.X * b.Y * (1.0f - b.X) * (1.0f - b.Y));

                position /= 2.0f;
                color /= 2.0f;
            }

            color.XYZ = Hlsl.Pow(color.XYZ, new Float3(0.7f, 0.8f, 0.5f));

            texture[ThreadIds.XY] = color;
        }
    }
}
