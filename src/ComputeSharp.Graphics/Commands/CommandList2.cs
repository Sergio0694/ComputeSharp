using System;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Extensions;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;

namespace ComputeSharp.Graphics.Commands
{
    /// <summary>
    /// A command list to set and execute operaations on the GPU.
    /// </summary>
    internal readonly unsafe ref struct CommandList2
    {
        /// <summary>
        /// The <see cref="GraphicsDevice2"/> instance associated with the current command list.
        /// </summary>
        private readonly GraphicsDevice2 device;

        /// <summary>
        /// The command list type being used by the current instance.
        /// </summary>
        private readonly D3D12_COMMAND_LIST_TYPE d3d12CommandListType;

        /// <summary>
        /// The <see cref="ID3D12CommandAllocator"/> object in use by the current instance.
        /// </summary>
        private readonly ID3D12CommandAllocator* d3D12CommandAllocator;

        /// <summary>
        /// The <see cref="ID3D12GraphicsCommandList"/> object in use by the current instance.
        /// </summary>
        public readonly ID3D12GraphicsCommandList* D3D12GraphicsCommandList;

        /// <summary>
        /// Creates a new <see cref="CommandList2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The target <see cref="GraphicsDevice2"/> instance to use.</param>
        /// <param name="d3d12CommandListType">The type of command list to create.</param>
        public CommandList2(GraphicsDevice2 device, D3D12_COMMAND_LIST_TYPE d3d12CommandListType)
        {
            this.device = device;
            this.d3d12CommandListType = d3d12CommandListType;
            this.d3D12CommandAllocator = d3d12CommandListType switch
            {
                D3D12_COMMAND_LIST_TYPE_COMPUTE => device.ComputeCommandAllocatorPool.GetCommandAllocator(device.D3D12Device, device.D3D12ComputeFence),
                D3D12_COMMAND_LIST_TYPE_COPY => device.ComputeCommandAllocatorPool.GetCommandAllocator(device.D3D12Device, device.D3D12CopyFence),
                _ => throw null!
            };

            D3D12GraphicsCommandList = device.D3D12Device->CreateCommandList(d3d12CommandListType, d3D12CommandAllocator);

            // Set the heap descriptor if the command list is not for copy operations
            if (d3d12CommandListType is not D3D12_COMMAND_LIST_TYPE_COPY)
            {
                ID3D12DescriptorHeap* d3D12DescriptorHeap = device.ShaderResourceViewDescriptorAllocator.D3D12DescriptorHeap;

                D3D12GraphicsCommandList->SetDescriptorHeaps(1, &d3D12DescriptorHeap);
            }
        }

        /// <summary>
        /// Copies a memory region from one resource to another.
        /// </summary>
        /// <param name="d3D12ResourceSource">The source <see cref="ID3D12Resource"/> to read from.</param>
        /// <param name="sourceOffset">The starting offset to read the source resource from.</param>
        /// <param name="d3d12ResourceDestination">The destination <see cref="ID3D12Resource"/> to write to.</param>
        /// <param name="destinationOffset">The starting offset to write the destination resource from.</param>
        /// <param name="numBytes">The total number of bytes to copy from one resource to another.</param>
        public void CopyBufferRegion(ID3D12Resource* d3D12ResourceSource, int sourceOffset, ID3D12Resource* d3d12ResourceDestination, int destinationOffset, int numBytes)
        {
            D3D12GraphicsCommandList->CopyBufferRegion(d3d12ResourceDestination, (uint)destinationOffset, d3D12ResourceSource, (uint)sourceOffset, (uint)numBytes);
        }

        /// <summary>
        /// Binds an input <see cref="GraphicsResource"/> object to a specified root parameter.
        /// </summary>
        /// <param name="rootParameterIndex">The root parameter index to bind to the input resource.</param>
        /// <param name="resource">The input <see cref="GraphicsResource"/> instance to bind.</param>
        public void SetComputeRootDescriptorTable(int rootParameterIndex, D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle)
        {
            D3D12GraphicsCommandList->SetComputeRootDescriptorTable((uint)rootParameterIndex, d3D12GpuDescriptorHandle);
        }

        /// <summary>
        /// Sets a given <see cref="PipelineState"/> object ready to be executed.
        /// </summary>
        /// <param name="pipelineState">The input <see cref="PipelineState"/> to setup.</param>
        public void SetPipelineState(PipelineState pipelineState)
        {
            // TODO: D3D12GraphicsCommandList->SetComputeRootSignature()
            // TODO: D3D12GraphicsCommandList->SetPipelineState()
        }

        /// <summary>
        /// Dispatches the pending shader using the specified thread group values.
        /// </summary>
        /// <param name="threadGroupCountX">The number of thread groups to schedule for the X axis.</param>
        /// <param name="threadGroupCountY">The number of thread groups to schedule for the Y axis.</param>
        /// <param name="threadGroupCountZ">The number of thread groups to schedule for the Z axis.</param>
        public void Dispatch(int threadGroupCountX, int threadGroupCountY, int threadGroupCountZ)
        {
            D3D12GraphicsCommandList->Dispatch((uint)threadGroupCountX, (uint)threadGroupCountY, (uint)threadGroupCountZ);
        }

        /// <summary>
        /// Executes the commands in the current commands list, and waits for completion.
        /// </summary>
        public void ExecuteAndWaitForCompletion()
        {
            D3D12GraphicsCommandList->Close();

            // TODO: GraphicsDevice.ExecuteCommandList(this);
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            switch (this.d3d12CommandListType)
            {
                case D3D12_COMMAND_LIST_TYPE_COMPUTE:
                    device.ComputeCommandAllocatorPool.Enqueue(this.d3D12CommandAllocator, device.NextD3D12ComputeFenceValue);
                    break;
                case D3D12_COMMAND_LIST_TYPE_COPY:
                    device.CopyCommandAllocatorPool.Enqueue(this.d3D12CommandAllocator, device.NextD3D12CopyFenceValue);
                    break;
            };

            D3D12GraphicsCommandList->Release();
        }
    }
}
