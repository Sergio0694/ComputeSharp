using System;
using ComputeSharp.Interop;
using Microsoft.UI.Xaml.Controls;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.WinUI
{
    /// <inheritdoc cref="ComputeShaderPanel"/>
    public sealed partial class ComputeShaderPanel : SwapChainPanel
    {
        /// <summary>
        /// The <see cref="IShaderRunner"/> instance used to create shaders to run.
        /// </summary>
        private IShaderRunner? shaderRunner;

        /// <summary>
        /// The resolution scale used to render frames.
        /// </summary>
        private double resolutionScale;

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
        private ulong nextD3D12FenceValue = 1;

        /// <summary>
        /// The <see cref="ID3D12CommandAllocator"/> object to create command lists.
        /// </summary>
        private ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator;

        /// <summary>
        /// The <see cref="ID3D12GraphicsCommandList"/> instance used to copy data to the back buffers.
        /// </summary>
        private ComPtr<ID3D12GraphicsCommandList> d3D12GraphicsCommandList;

        /// <summary>
        /// The <see cref="IDXGISwapChain1"/> instance used to display content onto the target window.
        /// </summary>
        public ComPtr<IDXGISwapChain1> dxgiSwapChain1;

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

        /// <summary>
        /// The <see cref="ReadWriteTexture2D{T, TPixel}"/> instance used to prepare frames to display.
        /// </summary>
        private ReadWriteTexture2D<Rgba32, Float4>? texture;

        /// <summary>
        /// Whether or not the window has been resized and requires the buffers to be updated.
        /// </summary>
        private bool isResizePending;

        /// <summary>
        /// Initializes the current application.
        /// </summary>
        /// <param name="hwnd">The handle for the window.</param>
        private unsafe void OnInitialize(HWND hwnd)
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

            // Create the command allocator to use
            fixed (ID3D12CommandAllocator** d3D12CommandAllocator = this.d3D12CommandAllocator)
            {
                this.d3D12Device.Get()->CreateCommandAllocator(
                    D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT,
                    FX.__uuidof<ID3D12CommandAllocator>(),
                    (void**)d3D12CommandAllocator);
            }

            // Create the reusable command list to copy data to the back buffers
            fixed (ID3D12GraphicsCommandList** d3D12GraphicsCommandList = this.d3D12GraphicsCommandList)
            {
                this.d3D12Device.Get()->CreateCommandList(
                    0,
                    D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT,
                    d3D12CommandAllocator,
                    null,
                    FX.__uuidof<ID3D12GraphicsCommandList>(),
                    (void**)d3D12GraphicsCommandList);
            }

            // Close the command list to prepare it for future use
            this.d3D12GraphicsCommandList.Get()->Close();
        }

        /// <summary>
        /// Resizes the current application.
        /// </summary>
        private void OnResize()
        {
            this.isResizePending = true;
        }

        /// <summary>
        /// Applies the actual resize logic that was scheduled from <see cref="OnResize"/>.
        /// </summary>
        private unsafe void ApplyResize()
        {
            this.d3D12CommandQueue.Get()->Signal(this.d3D12Fence.Get(), this.nextD3D12FenceValue);

            // Wait for the fence again to ensure there are no pending operations
            this.d3D12Fence.Get()->SetEventOnCompletion(this.nextD3D12FenceValue, default);

            this.nextD3D12FenceValue++;

            // Dispose the old buffers before resizing the buffer
            this.d3D12Resource0.Dispose();
            this.d3D12Resource1.Dispose();

            // Resize the swap chain buffers
            this.dxgiSwapChain1.Get()->ResizeBuffers(0, 0, 0, DXGI_FORMAT.DXGI_FORMAT_UNKNOWN, 0);

            if (this.resolutionScale == 1.0)
            {
                // Retrieve the back buffers for the swap chain
                fixed (ID3D12Resource** d3D12Resource0 = this.d3D12Resource0)
                fixed (ID3D12Resource** d3D12Resource1 = this.d3D12Resource1)
                {
                    _ = dxgiSwapChain1.Get()->GetBuffer(0, FX.__uuidof<ID3D12Resource>(), (void**)d3D12Resource0);
                    _ = dxgiSwapChain1.Get()->GetBuffer(1, FX.__uuidof<ID3D12Resource>(), (void**)d3D12Resource1);
                }
            }
            else
            {
                D3D12_RESOURCE_DESC d3D12ResourceDescription;

                // This is a workaround to detect the right scaled resolution for the back buffers.
                // First, IDXGISwapChain.Resize is called with (0, 0) as parameters for the size: this will cause
                // it to automatically resize to a 1:1 scaling factor with the available space. Then we get the
                // first backbuffer and use it to find the current resolution, scale it, and resize again to that.
                using (ComPtr<ID3D12Resource> d3D12Resource = default)
                {
                    _ = dxgiSwapChain1.Get()->GetBuffer(0, FX.__uuidof<ID3D12Resource>(), (void**)&d3D12Resource);

                    d3D12ResourceDescription = d3D12Resource.Get()->GetDesc();
                }

                uint
                    scaledWidth = (uint)(d3D12ResourceDescription.Width * this.resolutionScale),
                    scaledHeight = (uint)(d3D12ResourceDescription.Height * this.resolutionScale);

                this.dxgiSwapChain1.Get()->ResizeBuffers(0, scaledWidth, scaledHeight, DXGI_FORMAT.DXGI_FORMAT_UNKNOWN, 0);

                // Finally retrieve the scaled back buffers
                fixed (ID3D12Resource** d3D12Resource0 = this.d3D12Resource0)
                fixed (ID3D12Resource** d3D12Resource1 = this.d3D12Resource1)
                {
                    _ = dxgiSwapChain1.Get()->GetBuffer(0, FX.__uuidof<ID3D12Resource>(), (void**)d3D12Resource0);
                    _ = dxgiSwapChain1.Get()->GetBuffer(1, FX.__uuidof<ID3D12Resource>(), (void**)d3D12Resource1);
                }
            }            

            // Get the index of the initial back buffer
            using (ComPtr<IDXGISwapChain3> dxgiSwapChain3 = default)
            {
                _ = this.dxgiSwapChain1.CopyTo(dxgiSwapChain3.GetAddressOf());

                this.currentBufferIndex = dxgiSwapChain3.Get()->GetCurrentBackBufferIndex();
            }

            this.texture?.Dispose();

            D3D12_RESOURCE_DESC d3D12Resource0Description = this.d3D12Resource0.Get()->GetDesc();

            // Create the 2D texture to use to generate frames to display
            this.texture = Gpu.Default.AllocateReadWriteTexture2D<Rgba32, Float4>(
                (int)d3D12Resource0Description.Width,
                (int)d3D12Resource0Description.Height);
        }

        /// <summary>
        /// Updates the current application.
        /// </summary>
        /// <param name="time">The current time since the start of the application.</param>
        private unsafe void OnUpdate(TimeSpan time)
        {
            if (this.isResizePending)
            {
                ApplyResize();

                this.isResizePending = false;
            }

            // Skip if no factory is available
            if (this.shaderRunner is null)
            {
                return;
            }

            // Generate the new frame
            this.shaderRunner.Execute(this.texture!, time);

            using ComPtr<ID3D12Resource> d3D12Resource = default;

            // Get the underlying ID3D12Resource pointer for the texture
            _ = InteropServices.TryGetID3D12Resource(this.texture!, FX.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

            // Get the target back buffer to update
            ID3D12Resource* d3D12ResourceBackBuffer = this.currentBufferIndex switch
            {
                0 => this.d3D12Resource0.Get(),
                1 => this.d3D12Resource1.Get(),
                _ => null
            };

            this.currentBufferIndex ^= 1;

            // Reset the command list and command allocator
            this.d3D12CommandAllocator.Get()->Reset();
            this.d3D12GraphicsCommandList.Get()->Reset(this.d3D12CommandAllocator.Get(), null);

            D3D12_RESOURCE_BARRIER* d3D12ResourceBarriers = stackalloc D3D12_RESOURCE_BARRIER[]
            {
                D3D12_RESOURCE_BARRIER.InitTransition(
                    d3D12Resource.Get(),
                    D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_UNORDERED_ACCESS,
                    D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_SOURCE),
                D3D12_RESOURCE_BARRIER.InitTransition(
                    d3D12ResourceBackBuffer,
                    D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COMMON,
                    D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_DEST)
            };

            // Transition the resources to COPY_DEST and COPY_SOURCE respectively
            d3D12GraphicsCommandList.Get()->ResourceBarrier(2, d3D12ResourceBarriers);

            // Copy the generated frame to the target back buffer
            d3D12GraphicsCommandList.Get()->CopyResource(d3D12ResourceBackBuffer, d3D12Resource.Get());

            d3D12ResourceBarriers[0] = D3D12_RESOURCE_BARRIER.InitTransition(
                d3D12Resource.Get(),
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_SOURCE,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_UNORDERED_ACCESS);

            d3D12ResourceBarriers[1] = D3D12_RESOURCE_BARRIER.InitTransition(
                d3D12ResourceBackBuffer,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_DEST,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COMMON);

            // Transition the resources back to COMMON and UNORDERED_ACCESS respectively
            d3D12GraphicsCommandList.Get()->ResourceBarrier(2, d3D12ResourceBarriers);

            d3D12GraphicsCommandList.Get()->Close();

            // Execute the command list to perform the copy
            this.d3D12CommandQueue.Get()->ExecuteCommandLists(1, (ID3D12CommandList**)d3D12GraphicsCommandList.GetAddressOf());
            this.d3D12CommandQueue.Get()->Signal(this.d3D12Fence.Get(), this.nextD3D12FenceValue);

            // Present the new frame
            this.dxgiSwapChain1.Get()->Present(0, 0);

            if (this.nextD3D12FenceValue > this.d3D12Fence.Get()->GetCompletedValue())
            {
                this.d3D12Fence.Get()->SetEventOnCompletion(this.nextD3D12FenceValue, default);
            }

            this.nextD3D12FenceValue++;
        }
    }
}
