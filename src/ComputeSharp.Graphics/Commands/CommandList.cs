using System;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Commands.Abstract;
using SharpDX.Direct3D12;

namespace ComputeSharp.Graphics.Commands
{
    /// <summary>
    /// A <see langword="class"/> that represents a list of commands to issue to a GPU
    /// </summary>
    internal sealed class CommandList : CommandController
    {
        /// <inheritdoc/>
        public CommandList(GraphicsDevice device, CommandListType commandListType) : base(device, commandListType)
        {
            CommandAllocator = GetCommandAllocator();
            NativeCommandList = GraphicsDevice.NativeDevice.CreateCommandList(CommandListType, CommandAllocator, null);

            SetDescriptorHeaps(GraphicsDevice.ShaderResourceViewAllocator.DescriptorHeap);
        }


        /// <summary>
        /// Gets the <see cref="SharpDX.Direct3D12.CommandAllocator"/> object in use by the current instance
        /// </summary>
        public CommandAllocator CommandAllocator { get; private set; }

        /// <summary>
        /// Gets the <see cref="GraphicsCommandList"/> object in use by the current instance
        /// </summary>
        public GraphicsCommandList NativeCommandList { get; }

        public void CopyBufferRegion(GraphicsResource source, long sourceOffset, GraphicsResource destination, long destinationOffset, long numBytes)
        {
            NativeCommandList.CopyBufferRegion(destination.NativeResource, destinationOffset, source.NativeResource, sourceOffset, numBytes);
        }

        public void CopyResource(GraphicsResource source, GraphicsResource destination)
        {
            NativeCommandList.CopyResource(destination.NativeResource, source.NativeResource);
        }

        public void Dispatch(int threadGroupCountX, int threadGroupCountY, int threadGroupCountZ)
        {
            NativeCommandList.Dispatch(threadGroupCountX, threadGroupCountY, threadGroupCountZ);
        }

        public void Flush(bool wait = false)
        {
            Close();
            GraphicsDevice.ExecuteCommandLists(wait, this);
        }

        public void Reset()
        {
            CommandAllocator = GetCommandAllocator();
            NativeCommandList.Reset(CommandAllocator, null);

            SetDescriptorHeaps(GraphicsDevice.ShaderResourceViewAllocator.DescriptorHeap);
        }

        public void ResourceBarrierTransition(GraphicsResource resource, ResourceStates stateBefore, ResourceStates stateAfter)
        {
            NativeCommandList.ResourceBarrierTransition(resource.NativeResource, stateBefore, stateAfter);
        }

        public void SetDescriptorHeaps(params DescriptorHeap[] descriptorHeaps)
        {
            if (CommandListType != CommandListType.Copy)
            {
                NativeCommandList.SetDescriptorHeaps(descriptorHeaps);
            }
        }

        public void SetGraphicsRoot32BitConstant(int rootParameterIndex, int srcData, int destOffsetIn32BitValues)
        {
            NativeCommandList.SetGraphicsRoot32BitConstant(rootParameterIndex, srcData, destOffsetIn32BitValues);
        }

        public void SetGraphicsRootDescriptorTable(int rootParameterIndex, GraphicsResource resource)
        {
            if (resource.NativeGpuDescriptorHandle == null) throw new InvalidOperationException("Invalid graphics resource GPU descriptor");
            SetGraphicsRootDescriptorTable(rootParameterIndex, resource.NativeGpuDescriptorHandle.Value);
        }

        public void SetGraphicsRootDescriptorTable(int rootParameterIndex, GpuDescriptorHandle baseDescriptor)
        {
            NativeCommandList.SetGraphicsRootDescriptorTable(rootParameterIndex, baseDescriptor);
        }

        public void SetGraphicsRootSignature(RootSignature rootSignature)
        {
            NativeCommandList.SetGraphicsRootSignature(rootSignature);
        }

        public void SetComputeRoot32BitConstant(int rootParameterIndex, int srcData, int destOffsetIn32BitValues)
        {
            NativeCommandList.SetComputeRoot32BitConstant(rootParameterIndex, srcData, destOffsetIn32BitValues);
        }

        public void SetComputeRootDescriptorTable(int rootParameterIndex, GraphicsResource resource)
        {
            if (resource.NativeGpuDescriptorHandle == null) throw new InvalidOperationException("Invalid graphics resource GPU descriptor");
            SetComputeRootDescriptorTable(rootParameterIndex, resource.NativeGpuDescriptorHandle.Value);
        }

        public void SetComputeRootDescriptorTable(int rootParameterIndex, GpuDescriptorHandle baseDescriptor)
        {
            NativeCommandList.SetComputeRootDescriptorTable(rootParameterIndex, baseDescriptor);
        }

        public void SetComputeRootSignature(RootSignature rootSignature)
        {
            NativeCommandList.SetComputeRootSignature(rootSignature);
        }

        public void SetComputeRootUnorderedAccessView(int rootParameterIndex, GraphicsResource resource)
        {
            NativeCommandList.SetComputeRootUnorderedAccessView(rootParameterIndex, resource.NativeResource.GPUVirtualAddress);
        }

        public void SetPipelineState(PipelineState pipelineState)
        {
            SetComputeRootSignature(pipelineState.RootSignature);

            NativeCommandList.PipelineState = pipelineState.NativePipelineState;
        }

        private CommandAllocator GetCommandAllocator() => CommandListType switch
        {
            CommandListType.Bundle => GraphicsDevice.BundleAllocatorPool.GetCommandAllocator(),
            CommandListType.Compute => GraphicsDevice.ComputeAllocatorPool.GetCommandAllocator(),
            CommandListType.Copy => GraphicsDevice.CopyAllocatorPool.GetCommandAllocator(),
            CommandListType.Direct => GraphicsDevice.DirectAllocatorPool.GetCommandAllocator(),
            _ => throw new NotSupportedException("This command list type is not supported.")
        };

        public void Close() => NativeCommandList.Close();

        /// <inheritdoc/>
        public override void Dispose()
        {
            switch (CommandListType)
            {
                case CommandListType.Bundle:
                    GraphicsDevice.BundleAllocatorPool.Enqueue(CommandAllocator, GraphicsDevice.NextDirectFenceValue - 1);
                    break;
                case CommandListType.Direct:
                    GraphicsDevice.DirectAllocatorPool.Enqueue(CommandAllocator, GraphicsDevice.NextDirectFenceValue - 1);
                    break;
                case CommandListType.Compute:
                    GraphicsDevice.ComputeAllocatorPool.Enqueue(CommandAllocator, GraphicsDevice.NextComputeFenceValue - 1);
                    break;
                case CommandListType.Copy:
                    GraphicsDevice.CopyAllocatorPool.Enqueue(CommandAllocator, GraphicsDevice.NextCopyFenceValue - 1);
                    break;
                default:
                    throw new NotSupportedException("This command list type is not supported.");
            }

            NativeCommandList.Dispose();
        }
    }
}
