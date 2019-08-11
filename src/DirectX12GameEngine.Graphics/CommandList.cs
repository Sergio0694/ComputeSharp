using System;
using DirectX12GameEngine.Graphics.Primitives.Abstract;
using SharpDX.Direct3D12;

namespace DirectX12GameEngine.Graphics
{
    public sealed class CommandList : IDisposable
    {
        private readonly CompiledCommandList currentCommandList;

        public CommandList(GraphicsDevice device, CommandListType commandListType)
        {
            GraphicsDevice = device;
            CommandListType = commandListType;

            CommandAllocator commandAllocator = GetCommandAllocator();

            GraphicsCommandList nativeCommandList = GraphicsDevice.NativeDevice.CreateCommandList((SharpDX.Direct3D12.CommandListType)CommandListType, commandAllocator, null);
            currentCommandList = new CompiledCommandList(this, commandAllocator, nativeCommandList);

            SetDescriptorHeaps(GraphicsDevice.ShaderResourceViewAllocator.DescriptorHeap);
        }

        public CommandListType CommandListType { get; }

        public GraphicsDevice GraphicsDevice { get; }

        public CompiledCommandList Close()
        {
            //foreach (var renderTarget in RenderTargets)
            //{
            //    ResourceBarrierTransition(renderTarget, ResourceStates.RenderTarget, ResourceStates.Present);
            //}

            currentCommandList.NativeCommandList.Close();

            return currentCommandList;
        }

        public void CopyBufferRegion(GraphicsResource source, long sourceOffset, GraphicsResource destination, long destinationOffset, long numBytes)
        {
            currentCommandList.NativeCommandList.CopyBufferRegion(destination.NativeResource, destinationOffset, source.NativeResource, sourceOffset, numBytes);
        }

        public void CopyResource(GraphicsResource source, GraphicsResource destination)
        {
            currentCommandList.NativeCommandList.CopyResource(destination.NativeResource, source.NativeResource);
        }

        public void Dispatch(int threadGroupCountX, int threadGroupCountY, int threadGroupCountZ)
        {
            currentCommandList.NativeCommandList.Dispatch(threadGroupCountX, threadGroupCountY, threadGroupCountZ);
        }

        public void Dispose()
        {
            switch (CommandListType)
            {
                case CommandListType.Bundle:
                    GraphicsDevice.BundleAllocatorPool.Enqueue(currentCommandList.NativeCommandAllocator, GraphicsDevice.NextDirectFenceValue - 1);
                    break;
                case CommandListType.Direct:
                    GraphicsDevice.DirectAllocatorPool.Enqueue(currentCommandList.NativeCommandAllocator, GraphicsDevice.NextDirectFenceValue - 1);
                    break;
                case CommandListType.Compute:
                    GraphicsDevice.ComputeAllocatorPool.Enqueue(currentCommandList.NativeCommandAllocator, GraphicsDevice.NextComputeFenceValue - 1);
                    break;
                case CommandListType.Copy:
                    GraphicsDevice.CopyAllocatorPool.Enqueue(currentCommandList.NativeCommandAllocator, GraphicsDevice.NextCopyFenceValue - 1);
                    break;
                default:
                    throw new NotSupportedException("This command list type is not supported.");
            }

            currentCommandList.NativeCommandList.Dispose();
        }

        public void Flush(bool wait = false)
        {
            GraphicsDevice.ExecuteCommandLists(wait, Close());
        }

        public void Reset()
        {
            CommandAllocator commandAllocator = GetCommandAllocator();

            currentCommandList.NativeCommandAllocator = commandAllocator;
            currentCommandList.NativeCommandList.Reset(currentCommandList.NativeCommandAllocator, null);

            SetDescriptorHeaps(GraphicsDevice.ShaderResourceViewAllocator.DescriptorHeap);
        }

        public void ResourceBarrierTransition(GraphicsResource resource, ResourceStates stateBefore, ResourceStates stateAfter)
        {
            currentCommandList.NativeCommandList.ResourceBarrierTransition(resource.NativeResource, stateBefore, stateAfter);
        }

        public void SetDescriptorHeaps(params DescriptorHeap[] descriptorHeaps)
        {
            if (CommandListType != CommandListType.Copy)
            {
                currentCommandList.NativeCommandList.SetDescriptorHeaps(descriptorHeaps);
            }
        }

        public void SetGraphicsRoot32BitConstant(int rootParameterIndex, int srcData, int destOffsetIn32BitValues)
        {
            currentCommandList.NativeCommandList.SetGraphicsRoot32BitConstant(rootParameterIndex, srcData, destOffsetIn32BitValues);
        }

        public void SetGraphicsRootDescriptorTable(int rootParameterIndex, GraphicsResource resource)
        {
            SetGraphicsRootDescriptorTable(rootParameterIndex, resource.NativeGpuDescriptorHandle);
        }

        public void SetGraphicsRootDescriptorTable(int rootParameterIndex, GpuDescriptorHandle baseDescriptor)
        {
            currentCommandList.NativeCommandList.SetGraphicsRootDescriptorTable(rootParameterIndex, baseDescriptor);
        }

        public void SetGraphicsRootSignature(RootSignature rootSignature)
        {
            currentCommandList.NativeCommandList.SetGraphicsRootSignature(rootSignature);
        }

        public void SetComputeRoot32BitConstant(int rootParameterIndex, int srcData, int destOffsetIn32BitValues)
        {
            currentCommandList.NativeCommandList.SetComputeRoot32BitConstant(rootParameterIndex, srcData, destOffsetIn32BitValues);
        }

        public void SetComputeRootDescriptorTable(int rootParameterIndex, GraphicsResource resource)
        {
            SetComputeRootDescriptorTable(rootParameterIndex, resource.NativeGpuDescriptorHandle);
        }

        public void SetComputeRootDescriptorTable(int rootParameterIndex, GpuDescriptorHandle baseDescriptor)
        {
            currentCommandList.NativeCommandList.SetComputeRootDescriptorTable(rootParameterIndex, baseDescriptor);
        }

        public void SetComputeRootSignature(RootSignature rootSignature)
        {
            currentCommandList.NativeCommandList.SetComputeRootSignature(rootSignature);
        }

        public void SetComputeRootUnorderedAccessView(int rootParameterIndex, GraphicsResource resource)
        {
            currentCommandList.NativeCommandList.SetComputeRootUnorderedAccessView(rootParameterIndex, resource.NativeResource.GPUVirtualAddress);
        }

        public void SetPipelineState(PipelineState pipelineState)
        {
            SetComputeRootSignature(pipelineState.RootSignature);

            currentCommandList.NativeCommandList.PipelineState = pipelineState.NativePipelineState;
        }

        private CommandAllocator GetCommandAllocator() => CommandListType switch
        {
            CommandListType.Bundle => GraphicsDevice.BundleAllocatorPool.GetCommandAllocator(),
            CommandListType.Compute => GraphicsDevice.ComputeAllocatorPool.GetCommandAllocator(),
            CommandListType.Copy => GraphicsDevice.CopyAllocatorPool.GetCommandAllocator(),
            CommandListType.Direct => GraphicsDevice.DirectAllocatorPool.GetCommandAllocator(),
            _ => throw new NotSupportedException("This command list type is not supported.")
        };
    }
}
