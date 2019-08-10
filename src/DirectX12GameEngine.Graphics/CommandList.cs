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

        public Texture? DepthStencilBuffer { get; private set; }

        public GraphicsDevice GraphicsDevice { get; }

        public Texture[] RenderTargets { get; private set; } = Array.Empty<Texture>();

        public RawRectangle[] ScissorRectangles { get; private set; } = Array.Empty<RawRectangle>();

        public RawViewportF[] Viewports { get; private set; } = Array.Empty<RawViewportF>();

        public PrimitiveTopology PrimitiveTopology { set => currentCommandList.NativeCommandList.PrimitiveTopology = value; }

        public void BeginRenderPass()
        {
            BeginRenderPass(DepthStencilBuffer, RenderTargets);
        }

        public void BeginRenderPass(Texture? depthStencilView, params Texture[] renderTargetViews)
        {
            RenderPassRenderTargetDescription[] renderPassRenderTargetDescriptions = new RenderPassRenderTargetDescription[renderTargetViews.Length];

            RenderPassBeginningAccess renderPassBeginningAccessPreserve = new RenderPassBeginningAccess { Type = RenderPassBeginningAccessType.Preserve };
            RenderPassEndingAccess renderPassEndingAccessPreserve = new RenderPassEndingAccess { Type = RenderPassEndingAccessType.Preserve };

            for (int i = 0; i < renderTargetViews.Length; i++)
            {
                RenderPassRenderTargetDescription renderPassRenderTargetDescription = new RenderPassRenderTargetDescription
                {
                    BeginningAccess = renderPassBeginningAccessPreserve,
                    EndingAccess = renderPassEndingAccessPreserve,
                    CpuDescriptor = renderTargetViews[i].NativeCpuDescriptorHandle
                };

                renderPassRenderTargetDescriptions[i] = renderPassRenderTargetDescription;
            }

            RenderPassBeginningAccess renderPassBeginningAccessNoAccess = new RenderPassBeginningAccess { Type = RenderPassBeginningAccessType.NoAccess };
            RenderPassEndingAccess renderPassEndingAccessNoAccess = new RenderPassEndingAccess { Type = RenderPassEndingAccessType.NoAccess };

            RenderPassDepthStencilDescription? renderPassDepthStencilDescription = null;

            if (depthStencilView != null)
            {
                renderPassDepthStencilDescription = new RenderPassDepthStencilDescription
                {
                    DepthBeginningAccess = renderPassBeginningAccessNoAccess,
                    DepthEndingAccess = renderPassEndingAccessNoAccess,
                    StencilBeginningAccess = renderPassBeginningAccessNoAccess,
                    StencilEndingAccess = renderPassEndingAccessNoAccess,
                    CpuDescriptor = depthStencilView.NativeCpuDescriptorHandle,
                };
            }

            BeginRenderPass(renderTargetViews.Length, renderPassRenderTargetDescriptions, renderPassDepthStencilDescription, RenderPassFlags.None);
        }

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

        public void Clear(Texture depthStencilBuffer, ClearFlags clearFlags, float depth = 1, byte stencil = 0)
        {
            currentCommandList.NativeCommandList.ClearDepthStencilView(depthStencilBuffer.NativeCpuDescriptorHandle, (SharpDX.Direct3D12.ClearFlags)clearFlags, depth, stencil);
        }

        public unsafe void Clear(Texture renderTarget, Vector4 color)
        {
            currentCommandList.NativeCommandList.ClearRenderTargetView(renderTarget.NativeCpuDescriptorHandle, *(RawColor4*)&color);
        }

        public void ClearState()
        {
            Array.Clear(Viewports, 0, Viewports.Length);
            Array.Clear(ScissorRectangles, 0, ScissorRectangles.Length);

            Texture? depthStencilBuffer = GraphicsDevice.Presenter?.DepthStencilBuffer;
            Texture? backBuffer = GraphicsDevice.Presenter?.BackBuffer;

            if (backBuffer != null)
            {
                SetRenderTargets(depthStencilBuffer, backBuffer);
                SetViewports(new RawViewportF { X = 0, Y = 0, Width = backBuffer.Width, Height = backBuffer.Height, MaxDepth = 1.0f });
                SetScissorRectangles(new RawRectangle { Right = backBuffer.Width, Bottom = backBuffer.Height });
            }
            else if (depthStencilBuffer != null)
            {
                SetRenderTargets(depthStencilBuffer);
                SetViewports(new RawViewportF { X = 0, Y = 0, Width = depthStencilBuffer.Width, Height = depthStencilBuffer.Height, MaxDepth = 1.0f });
                SetScissorRectangles(new RawRectangle { Right = depthStencilBuffer.Width, Bottom = depthStencilBuffer.Height });
            }
            else
            {
                SetRenderTargets(null);
            }
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

        public void SetRenderTargets(Texture? depthStencilView, params Texture[] renderTargetViews)
        {
            DepthStencilBuffer = depthStencilView;

            if (renderTargetViews.Length > MaxRenderTargetCount)
            {
                throw new ArgumentOutOfRangeException(nameof(renderTargetViews), renderTargetViews.Length, $"The maximum number of render targets is {MaxRenderTargetCount}.");
            }

            if (RenderTargets.Length != renderTargetViews.Length)
            {
                RenderTargets = new Texture[renderTargetViews.Length];
            }

            renderTargetViews.CopyTo(RenderTargets, 0);

            CpuDescriptorHandle[] renderTargetDescriptors = new CpuDescriptorHandle[renderTargetViews.Length];

            for (int i = 0; i < renderTargetViews.Length; i++)
            {
                //ResourceBarrierTransition(renderTargetViews[i], ResourceStates.Present, ResourceStates.RenderTarget);
                renderTargetDescriptors[i] = renderTargetViews[i].NativeCpuDescriptorHandle;
            }

            currentCommandList.NativeCommandList.SetRenderTargets(renderTargetDescriptors, depthStencilView?.NativeCpuDescriptorHandle);
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
