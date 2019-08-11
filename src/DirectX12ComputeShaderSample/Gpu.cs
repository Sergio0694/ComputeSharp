using System;
using System.Linq;
using DirectX12GameEngine.Graphics;
using DirectX12GameEngine.Shaders;
using DirectX12GameEngine.Shaders.Numerics;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D12;
using CommandList = DirectX12GameEngine.Graphics.CommandList;
using PipelineState = DirectX12GameEngine.Graphics.PipelineState;

namespace DirectX12ComputeShaderSample
{
    public static class Gpu
    {
        private static GraphicsDevice _Default;

        public static GraphicsDevice Default => _Default ??= new GraphicsDevice(true);
    }

    public static class GraphicsDeviceExtensions
    {
        public static void For(this GraphicsDevice device, int n, Action<UInt3> shader)
        {
            // Generate the compute shadr
            ShaderGenerator shaderGenerator = new ShaderGenerator(shader);
            ShaderGenerationResult result = shaderGenerator.GenerateShaderForLambda();

            byte[] shaderBytecode = ShaderCompiler.CompileShader(result.ShaderSource, ShaderVersion.ComputeShader, result.ComputeShader);

            // Create the root signature for the pipeline and the pipeline state
            RootSignatureDescription rootSignatureDescription = new RootSignatureDescription(RootSignatureFlags.None, shaderGenerator.RootParameters);
            RootSignature rootSignature = device.CreateRootSignature(rootSignatureDescription);
            PipelineState pipelineState = new PipelineState(device, rootSignature, shaderBytecode);

            // Create the commands list
            using CommandList commandList = new CommandList(device, CommandListType.Compute);
            commandList.SetPipelineState(pipelineState);

            // Load the captured buffers
            foreach (var item in shaderGenerator.ReadWriteBuffers.Select((r, i) => (Resource: r, Index: i)))
            {
                commandList.SetComputeRootDescriptorTable(item.Index, item.Resource);
            }

            // Dispatch and wait for completion
            commandList.Dispatch(2, 2, 1);
            commandList.Flush(true);
        }
    }
}
