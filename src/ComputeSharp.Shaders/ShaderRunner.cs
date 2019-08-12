using System;
using System.Linq;
using ComputeSharp.Graphics;
using ComputeSharp.Shaders.Renderer;
using ComputeSharp.Shaders.Renderer.Models;
using ComputeSharp.Shaders.Translation;
using SharpDX.Direct3D12;
using CommandList = ComputeSharp.Graphics.CommandList;
using PipelineState = ComputeSharp.Graphics.PipelineState;

namespace ComputeSharp.Shaders
{
    /// <summary>
    /// A <see langword="class"/> responsible for performing all the necessary operations to compile and run a compute shader
    /// </summary>
    public static class ShaderRunner
    {
        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="numThreads">The <see cref="NumThreads"/> value that indicates the number of threads to run in each thread group</param>
        /// <param name="numGroups">The <see cref="NumThreads"/> value that indicates the number of thread groups to dispatch</param>
        /// <param name="action">The input <see cref="Action{T}"/> representing the compute shader to run</param>
        public static void Run(GraphicsDevice device, NumThreads numThreads, NumThreads numGroups, Action<ThreadIds> action)
        {
            // Load the input shader
            ShaderLoader shaderLoader = ShaderLoader.Load(action);

            // Render the loaded shader
            ShaderInfo shaderInfo = new ShaderInfo
            {
                FieldsList = shaderLoader.FieldsInfo,
                NumThreadsX = numThreads.X,
                NumThreadsY = numThreads.Y,
                NumThreadsZ = numThreads.Z,
                ThreadsIdsVariableName = shaderLoader.ThreadsIdsVariableName,
                ShaderBody = shaderLoader.MethodBody
            };
            string shaderSource = ShaderRenderer.Instance.Render(shaderInfo);

            // Compile the loaded shader to HLSL bytecode
            ShaderBytecode shaderBytecode = ShaderCompiler.Instance.CompileShader(shaderSource);

            // Create the root signature for the pipeline and the pipeline state
            RootSignatureDescription rootSignatureDescription = new RootSignatureDescription(RootSignatureFlags.None, shaderLoader.RootParameters);
            RootSignature rootSignature = device.CreateRootSignature(rootSignatureDescription);
            PipelineState pipelineState = new PipelineState(device, rootSignature, shaderBytecode);

            // Create the commands list
            using CommandList commandList = new CommandList(device, CommandListType.Compute);
            commandList.SetPipelineState(pipelineState);

            // Load the captured buffers
            foreach (var buffer in shaderLoader.ReadWriteBuffers.Select((r, i) => (Resource: r, Index: i)))
            {
                commandList.SetComputeRootDescriptorTable(buffer.Index, buffer.Resource);
            }

            // Dispatch and wait for completion
            commandList.Dispatch(numGroups.X, numGroups.Y, numGroups.Z);
            commandList.Flush(true);
        }
    }
}
