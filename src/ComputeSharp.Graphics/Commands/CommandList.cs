using System;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Commands.Abstract;
using SharpGen.Runtime;
using Vortice.Direct3D12;

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
            CommandAllocator = CommandListType switch
            {
                CommandListType.Compute => GraphicsDevice.ComputeAllocatorPool.GetCommandAllocator(),
                CommandListType.Copy => GraphicsDevice.CopyAllocatorPool.GetCommandAllocator(),
                CommandListType.Direct => GraphicsDevice.DirectAllocatorPool.GetCommandAllocator(),
                _ => throw new NotSupportedException($"Unsupported command list type with value {CommandListType}")
            };

            Result result = GraphicsDevice.NativeDevice.CreateCommandList(0, commandListType, CommandAllocator, null, out ID3D12GraphicsCommandList? nativeCommandList);

            if (result.Failure)
            {
                throw new COMException("Failed to create the commands list", result.Code);
            }

            NativeCommandList = nativeCommandList!;

            // Set the heap descriptor if the command list is not for copy operations
            if (CommandListType != CommandListType.Copy)
            {
                NativeCommandList.SetDescriptorHeaps(1, new[] { GraphicsDevice.ShaderResourceViewAllocator.DescriptorHeap });
            }
        }


        /// <summary>
        /// Gets the <see cref="ID3D12CommandAllocator"/> object in use by the current instance
        /// </summary>
        public ID3D12CommandAllocator CommandAllocator { get; }

        /// <summary>
        /// Gets the <see cref="ID3D12GraphicsCommandList"/> object in use by the current instance
        /// </summary>
        public ID3D12GraphicsCommandList NativeCommandList { get; }

        /// <summary>
        /// Copies a memory region from one resource to another
        /// </summary>
        /// <param name="source">The source <see cref="GraphicsResource"/> to read from</param>
        /// <param name="sourceOffset">The starting offset to read the source resource from</param>
        /// <param name="destination">The destination <see cref="GraphicsResource"/> to write to</param>
        /// <param name="destinationOffset">The starting offset to write the destination resource from</param>
        /// <param name="numBytes">The total number of bytes to copy from one resource to another</param>
        public void CopyBufferRegion(GraphicsResource source, long sourceOffset, GraphicsResource destination, long destinationOffset, long numBytes)
        {
            NativeCommandList.CopyBufferRegion(destination.NativeResource, destinationOffset, source.NativeResource, sourceOffset, numBytes);
        }

        /// <summary>
        /// Binds an input <see cref="GraphicsResource"/> object to a specified root parameter
        /// </summary>
        /// <param name="rootParameterIndex">The root parameter index to bind to the input resource</param>
        /// <param name="resource">The input <see cref="GraphicsResource"/> instance to bind</param>
        public void SetComputeRootDescriptorTable(int rootParameterIndex, GraphicsResource resource)
        {
            if (resource.NativeGpuDescriptorHandle == null) throw new InvalidOperationException("Invalid graphics resource GPU descriptor");
            if (resource.GraphicsDevice != GraphicsDevice) throw new GraphicsDeviceMismatchException(GraphicsDevice, resource);

            NativeCommandList.SetComputeRootDescriptorTable(rootParameterIndex, resource.NativeGpuDescriptorHandle.Value);
        }

        /// <summary>
        /// Sets a given <see cref="PipelineState"/> object ready to be executed
        /// </summary>
        /// <param name="pipelineState">The input <see cref="PipelineState"/> to setup</param>
        public void SetPipelineState(PipelineState pipelineState)
        {
            NativeCommandList.SetComputeRootSignature(pipelineState.RootSignature);
            NativeCommandList.SetPipelineState(pipelineState.NativePipelineState);
        }

        /// <summary>
        /// Dispatches the pending shader using the specified thread group values
        /// </summary>
        /// <param name="threadGroupCountX">The number of thread groups to schedule for the X axis</param>
        /// <param name="threadGroupCountY">The number of thread groups to schedule for the Y axis</param>
        /// <param name="threadGroupCountZ">The number of thread groups to schedule for the Z axis</param>
        public void Dispatch(int threadGroupCountX, int threadGroupCountY, int threadGroupCountZ)
        {
            NativeCommandList.Dispatch(threadGroupCountX, threadGroupCountY, threadGroupCountZ);
        }

        /// <summary>
        /// Executes the commands in the current commands list, and waits for completion
        /// </summary>
        public void ExecuteAndWaitForCompletion()
        {
            NativeCommandList.Close();

            GraphicsDevice.ExecuteCommandList(this);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            switch (CommandListType)
            {
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
                    throw new NotSupportedException($"Unsupported command list type with value {CommandListType}");
            }

            NativeCommandList.Dispose();
        }
    }
}
