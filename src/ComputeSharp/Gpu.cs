using System;
using System.Linq;
using ComputeSharp.Graphics;
using ComputeSharp.Shaders;
using ComputeSharp.Shaders.Renderer;
using ComputeSharp.Shaders.Renderer.Models;
using ComputeSharp.Shaders.Translation;
using SharpDX.Direct3D12;
using CommandList = ComputeSharp.Graphics.CommandList;
using PipelineState = ComputeSharp.Graphics.PipelineState;
using ShaderBytecode = SharpDX.Direct3D12.ShaderBytecode;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that acts as an entry-point for all the library APIs, exposing the available GPU devices
    /// </summary>
    public static class Gpu
    {
        private static GraphicsDevice _Default;

        /// <summary>
        /// Gets the default <see cref="GraphicsDevice"/> instance for the current machine
        /// </summary>
        public static GraphicsDevice Default => _Default ??= new GraphicsDevice(true);
    }

    public static class GraphicsDeviceExtensions
    {
        public static void For(this GraphicsDevice device, int n, Action<ThreadIds> action)
        {
            // Load the input shader
            ShaderLoader shaderLoader = ShaderLoader.Load(action);

            // Render the loaded shader
            ShaderInfo shaderInfo = new ShaderInfo
            {
                FieldsList = shaderLoader.FieldsInfo,
                ThreadsX = n,
                ThreadsY = 1,
                ThreadsZ = 1,
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
            commandList.Dispatch(1, 1, 1);
            commandList.Flush(true);
        }
    }
}
