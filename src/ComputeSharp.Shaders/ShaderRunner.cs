using System;
using System.Collections.Generic;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Buffers.Extensions;
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
    internal static class ShaderRunner
    {
        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="x">The number of iterations to run on the X axis</param>
        /// <param name="y">The number of iterations to run on the Y axis</param>
        /// <param name="z">The number of iterations to run on the Z axis</param>
        /// <param name="action">The input <see cref="Action{T}"/> representing the compute shader to run</param>
        public static void Run(GraphicsDevice device, int x, int y, int z, Action<ThreadIds> action)
        {
            // Calculate the optimized thread num values
            int threadsX, threadsY = 1, threadsZ = 1;
            if (y == 1 && z == 1) threadsX = device.WavefrontSize;
            else if (z == 1) threadsX = threadsY = 8;
            else threadsX = threadsY = threadsZ = 4;

            Run(device, x, y, z, threadsX, threadsY, threadsZ, action);
        }

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="x">The number of iterations to run on the X axis</param>
        /// <param name="y">The number of iterations to run on the Y axis</param>
        /// <param name="z">The number of iterations to run on the Z axis</param>
        /// <param name="threadsX">The number of threads in each thread group for the X axis</param>
        /// <param name="threadsY">The number of threads in each thread group for the Y axis</param>
        /// <param name="threadsZ">The number of threads in each thread group for the Z axis</param>
        /// <param name="action">The input <see cref="Action{T}"/> representing the compute shader to run</param>
        public static void Run(
            GraphicsDevice device,
            int x, int y, int z,
            int threadsX, int threadsY, int threadsZ,
            Action<ThreadIds> action)
        {
            // Calculate the dispatch values
            int
                groupsX = x / threadsX + (x % threadsX == 0 ? 0 : 1),
                groupsY = y / threadsY + (y % threadsY == 0 ? 0 : 1),
                groupsZ = z / threadsZ + (z % threadsZ == 0 ? 0 : 1);

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
            foreach (var buffer in shaderLoader.Buffers)
            {
                commandList.SetComputeRootDescriptorTable(buffer.Index, buffer.Resource);
            }

            // Load the captured variables
            List<GraphicsResource> buffers = new List<GraphicsResource>();
            foreach (var variable in shaderLoader.CapturedConstantBufferValues)
            {
                GraphicsResource resource = device.AllocateReadOnlyBufferFromReflectedSingleValue(variable.Value);
                commandList.SetComputeRootDescriptorTable(variable.Index, resource);
                buffers.Add(resource);
            }

            // Dispatch and wait for completion
            commandList.Dispatch(groupsX, groupsY, groupsZ);
            commandList.Flush();

            // Free the allocated resources
            foreach (GraphicsResource resource in buffers)
            {
                resource.Dispose();
            }
        }
    }
}
