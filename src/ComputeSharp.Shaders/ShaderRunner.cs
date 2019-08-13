using System;
using System.Linq;
using ComputeSharp.Graphics;
using ComputeSharp.Shaders.Renderer;
using ComputeSharp.Shaders.Renderer.Models;
using ComputeSharp.Shaders.Translation;
using SharpDX.Direct3D12;
using CommandList = ComputeSharp.Graphics.Commands.CommandList;
using PipelineState = ComputeSharp.Graphics.Commands.PipelineState;

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
        /// <param name="x">The number of threads to run on the X axis</param>
        /// <param name="y">The number of threads to run on the Y axis</param>
        /// <param name="z">The number of threads to run on the Z axis</param>
        /// <param name="action">The input <see cref="Action{T}"/> representing the compute shader to run</param>
        public static void Run(GraphicsDevice device, int x, int y, int z, Action<ThreadIds> action)
        {
            // Calculate the optimized thread num and group values
            int
                threadsX = device.WavefrontSize,
                threadsY = y > 1 ? device.WavefrontSize : 1,
                threadsZ = z > 1 ? device.WavefrontSize : 1,
                groupsX = (x - 1) / device.WavefrontSize + 1,
                groupsY = (y - 1) / device.WavefrontSize + 1,
                groupsZ = (z - 1) / device.WavefrontSize + 1;

            // Load the input shader
            ShaderLoader shaderLoader = ShaderLoader.Load(action);

            // Render the loaded shader
            ShaderInfo shaderInfo = new ShaderInfo
            {
                FieldsList = shaderLoader.FieldsInfo,
                ThreadsX = x,
                ThreadsY = y,
                ThreadsZ = z,
                NumThreadsX = threadsX,
                NumThreadsY = threadsY,
                NumThreadsZ = threadsZ,
                ThreadsIdsVariableName = shaderLoader.ThreadsIdsVariableName,
                ShaderBody = shaderLoader.MethodBody
            };
            string shaderSource = ShaderRenderer.Instance.Render(shaderInfo);

            // Compile the loaded shader to HLSL bytecode
            ShaderBytecode shaderBytecode = ShaderCompiler.Instance.CompileShader(shaderSource);

            // Create the root signature for the pipeline and get the pipeline state
            RootSignatureDescription rootSignatureDescription = new RootSignatureDescription(RootSignatureFlags.None, shaderLoader.RootParameters);
            RootSignature rootSignature = device.CreateRootSignature(rootSignatureDescription);
            PipelineState pipelineState = new PipelineState(device, rootSignature, shaderBytecode);

            // Create the commands list and set the pipeline state
            using CommandList commandList = new CommandList(device, CommandListType.Compute);
            commandList.SetPipelineState(pipelineState);

            // Load the captured buffers
            foreach (var buffer in shaderLoader.Buffers.Select((r, i) => (Resource: r, Index: i)))
            {
                commandList.SetComputeRootDescriptorTable(buffer.Index, buffer.Resource);
            }

            // Dispatch and wait for completion
            commandList.Dispatch(groupsX, groupsY, groupsZ);
            commandList.Flush();
        }
    }
}
