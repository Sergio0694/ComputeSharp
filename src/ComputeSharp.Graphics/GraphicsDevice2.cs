using ComputeSharp.Core.Interop;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Helpers;
using TerraFX.Interop;

namespace ComputeSharp.Graphics
{
    /// <summary>
    /// A <see langword="class"/> that represents a DX12.1-compatible GPU device that can be used to run compute shaders.
    /// </summary>
    public sealed unsafe class GraphicsDevice2 : NativeObject
    {
        /// <summary>
        /// The underlying <see cref="ID3D12Device"/> wrapped by the current instance.
        /// </summary>
        internal readonly ID3D12Device* D3D12Device;

        /// <summary>
        /// The <see cref="ID3D12CommandQueue"/> instance to use for compute operations.
        /// </summary>
        internal readonly ID3D12CommandQueue* D3D12ComputeCommandQueue;

        /// <summary>
        /// The <see cref="ID3D12CommandQueue"/> instance to use for copy operations.
        /// </summary>
        internal readonly ID3D12CommandQueue* D3D12CopyCommandQueue;

        /// <summary>
        /// The <see cref="ID3D12Fence"/> instance used for compute operations.
        /// </summary>
        internal readonly ID3D12Fence* D3D12ComputeFence;

        /// <summary>
        /// The <see cref="ID3D12Fence"/> instance used for copy operations.
        /// </summary>
        internal readonly ID3D12Fence* D3D12CopyFence;

        /// <summary>
        /// The <see cref="CommandAllocatorPool2"/> instance for compute operations.
        /// </summary>
        internal readonly CommandAllocatorPool2 ComputeCommandAllocatorPool;

        /// <summary>
        /// Gets the <see cref="CommandAllocatorPool2"/> instance for copy operations.
        /// </summary>
        internal readonly CommandAllocatorPool2 CopyCommandAllocatorPool;

        /// <summary>
        /// The <see cref="DescriptorAllocator2"/> instance to use when allocating new buffers.
        /// </summary>
        internal readonly DescriptorAllocator2 ShaderResourceViewDescriptorAllocator;

        /// <summary>
        /// The next fence value for compute operations using <see cref="D3D12ComputeCommandQueue"/>.
        /// </summary>
        internal long NextD3D12ComputeFenceValue = 1;

        /// <summary>
        /// The next fence value for copy operations using <see cref="D3D12CopyCommandQueue"/>.
        /// </summary>
        internal long NextD3D12CopyFenceValue = 1;

        /// <summary>
        /// Creates a new <see cref="GraphicsDevice2"/> instance for the input <see cref="ID3D12Device"/>.
        /// </summary>
        /// <param name="device">The <see cref="ID3D12Device"/> to use for the new <see cref="GraphicsDevice2"/> instance.</param>
        /// <param name="description">The available info for the new <see cref="GraphicsDevice2"/> instance.</param>
        internal GraphicsDevice2(ID3D12Device* d3d12device, DXGI_ADAPTER_DESC1* dxgiDescription1)
        {
            D3D12Device = d3d12device;
            D3D12ComputeCommandQueue = D3D12Helper.CreateCommandQueue(d3d12device, D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_COMPUTE);
            D3D12CopyCommandQueue = D3D12Helper.CreateCommandQueue(d3d12device, D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_COPY);
            D3D12ComputeFence = D3D12Helper.CreateFence(d3d12device);
            D3D12CopyFence = D3D12Helper.CreateFence(d3d12device);
            ComputeCommandAllocatorPool = new CommandAllocatorPool2(D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_COMPUTE);
            CopyCommandAllocatorPool = new CommandAllocatorPool2(D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_COPY);
            ShaderResourceViewDescriptorAllocator = new DescriptorAllocator2(d3d12device);

            Name = new string((char*)dxgiDescription1->Description);
            MemorySize = dxgiDescription1->DedicatedVideoMemory;

            var d3D12Options1Data = D3D12Helper.CheckFeatureSupport<D3D12_FEATURE_DATA_D3D12_OPTIONS1>(d3d12device, D3D12_FEATURE.D3D12_FEATURE_D3D12_OPTIONS1);

            ComputeUnits = d3D12Options1Data.TotalLaneCount;
            WavefrontSize = d3D12Options1Data.WaveLaneCountMin;
        }

        /// <summary>
        /// Gets the name of the current <see cref="GraphicsDevice"/> instance.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the size of the dedicated video memory for the current <see cref="GraphicsDevice"/> instance.
        /// </summary>
        public nuint MemorySize { get; }

        /// <summary>
        /// Gets the number of total lanes on the current device (eg. CUDA cores on an nVidia GPU).
        /// </summary>
        public uint ComputeUnits { get; }

        /// <summary>
        /// Gets the number of lanes in a SIMD wave on the current device (also known as "wavefront size" or "warp width").
        /// </summary>
        public uint WavefrontSize { get; }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            D3D12Device->Release();
            D3D12ComputeCommandQueue->Release();
            D3D12CopyCommandQueue->Release();
            D3D12ComputeFence->Release();
            D3D12CopyFence->Release();
            ComputeCommandAllocatorPool.Dispose();
            CopyCommandAllocatorPool.Dispose();
            ShaderResourceViewDescriptorAllocator.Dispose();
        }
    }
}
