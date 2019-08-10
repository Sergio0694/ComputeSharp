using System;
using System.Numerics;
using SharpDX.Direct3D;
using SharpDX.Direct3D12;
using SharpDX.Mathematics.Interop;

namespace DirectX12GameEngine.Graphics
{
    public sealed class CommandList : IDisposable
    {
        private const int MaxRenderTargetCount = 8;
        private const int MaxViewportAndScissorRectangleCount = 16;

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

        public RawRectangle[] ScissorRectangles { get; private set; } = Array.Empty<RawRectangle>();

        public RawViewportF[] Viewports { get; private set; } = Array.Empty<RawViewportF>();

        public PrimitiveTopology PrimitiveTopology { set => currentCommandList.NativeCommandList.PrimitiveTopology = value; }

        public void BeginRenderPass(int numRenderTargets, RenderPassRenderTargetDescription[] renderTargetsRef, RenderPassDepthStencilDescription? depthStencilRef, RenderPassFlags flags)
        {
            using GraphicsCommandList4 commandList = currentCommandList.NativeCommandList.QueryInterface<GraphicsCommandList4>();
            commandList.BeginRenderPass(numRenderTargets, renderTargetsRef, depthStencilRef, flags);
        }

        public void EndRenderPass()
        {
            using GraphicsCommandList4 commandList = currentCommandList.NativeCommandList.QueryInterface<GraphicsCommandList4>();
            commandList.EndRenderPass();
        }

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

        public void CopyTextureRegion(TextureCopyLocation source, TextureCopyLocation destination)
        {
            currentCommandList.NativeCommandList.CopyTextureRegion(destination, 0, 0, 0, source, null);
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

        public void DrawIndexedInstanced(int indexCountPerInstance, int instanceCount, int startIndexLocation = 0, int baseVertexLocation = 0, int startInstanceLocation = 0)
        {
            currentCommandList.NativeCommandList.DrawIndexedInstanced(indexCountPerInstance, instanceCount, startIndexLocation, baseVertexLocation, startInstanceLocation);
        }

        public void DrawInstanced(int vertexCountPerInstance, int instanceCount, int startVertexLocation = 0, int startInstanceLocation = 0)
        {
            currentCommandList.NativeCommandList.DrawInstanced(vertexCountPerInstance, instanceCount, startVertexLocation, startInstanceLocation);
        }

        public void ExecuteBundle(CompiledCommandList commandList)
        {
            if (currentCommandList != commandList && commandList.Builder.CommandListType == CommandListType.Bundle)
            {
                currentCommandList.NativeCommandList.ExecuteBundle(commandList.NativeCommandList);
            }
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

        public void SetIndexBuffer(IndexBufferView? indexBufferView)
        {
            currentCommandList.NativeCommandList.SetIndexBuffer(indexBufferView);
        }

        public void SetPipelineState(PipelineState pipelineState)
        {
            if (pipelineState.IsCompute)
            {
                SetComputeRootSignature(pipelineState.RootSignature);
            }
            else
            {
                SetGraphicsRootSignature(pipelineState.RootSignature);
            }

            currentCommandList.NativeCommandList.PipelineState = pipelineState.NativePipelineState;
        }

        public void SetScissorRectangles(params RawRectangle[] scissorRectangles)
        {
            if (scissorRectangles.Length > MaxViewportAndScissorRectangleCount)
            {
                throw new ArgumentOutOfRangeException(nameof(scissorRectangles), scissorRectangles.Length, $"The maximum number of scissor rectangles is {MaxViewportAndScissorRectangleCount}.");
            }

            if (ScissorRectangles.Length != scissorRectangles.Length)
            {
                ScissorRectangles = new RawRectangle[scissorRectangles.Length];
            }

            scissorRectangles.CopyTo(ScissorRectangles, 0);

            currentCommandList.NativeCommandList.SetScissorRectangles(scissorRectangles);
        }

        public void SetVertexBuffers(int startSlot, params VertexBufferView[] vertexBufferViews)
        {
            currentCommandList.NativeCommandList.SetVertexBuffers(startSlot, vertexBufferViews);
        }

        public void SetViewports(params RawViewportF[] viewports)
        {
            if (viewports.Length > MaxViewportAndScissorRectangleCount)
            {
                throw new ArgumentOutOfRangeException(nameof(viewports), viewports.Length, $"The maximum number of viewporst is {MaxViewportAndScissorRectangleCount}.");
            }

            if (Viewports.Length != viewports.Length)
            {
                Viewports = new RawViewportF[viewports.Length];
            }

            viewports.CopyTo(Viewports, 0);

            currentCommandList.NativeCommandList.SetViewports(viewports);
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
