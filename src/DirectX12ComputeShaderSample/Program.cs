using DirectX12GameEngine.Graphics;
using DirectX12GameEngine.Shaders;
using DirectX12GameEngine.Shaders.Numerics;
using System;
using System.Linq;
using DirectX12GameEngine.Graphics.Buffers;
using SharpDX.Direct3D12;
using Buffer = DirectX12GameEngine.Graphics.Buffers.Buffer;
using CommandList = DirectX12GameEngine.Graphics.CommandList;
using PipelineState = DirectX12GameEngine.Graphics.PipelineState;

namespace DirectX12ComputeShaderSample
{
    public class Program
    {
        private static void Main()
        {
            // Create the graphics device
            using GraphicsDevice device = new GraphicsDevice(true);

            // Create the graphics buffer
            int width = 10;
            int height = 10;
            float[] array = new float[width * height];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }

            using Buffer<float> gpuBuffer = Buffer.UnorderedAccess.New(device, array.AsSpan());
            using Buffer<uint> widthBuffer = Buffer.Constant.New(device, (uint)width);

            // Variables for closure
            uint Width = (uint)width;
            var data = new RWBufferResource<float>();

            // Shader body
            Action<UInt3> action = id =>
            {
                data[id.X + id.Y * Width] *= 2;
            };

            // Generate computer shader
            ShaderGenerator shaderGenerator = new ShaderGenerator(action);
            ShaderGenerationResult result = shaderGenerator.GenerateShaderForLambda();

            byte[] shaderBytecode = ShaderCompiler.CompileShader(result.ShaderSource, SharpDX.D3DCompiler.ShaderVersion.ComputeShader, result.ComputeShader);

            // Create the pipeline state

            RootParameter[] rootParameters =
            {
                new RootParameter(ShaderVisibility.All, new DescriptorRange(DescriptorRangeType.ConstantBufferView, 1, 0)),
                new RootParameter(ShaderVisibility.All, new DescriptorRange(DescriptorRangeType.UnorderedAccessView, 1, 0))
            };

            var rootSignatureDescription = new RootSignatureDescription(RootSignatureFlags.None, rootParameters);
            var rootSignature = device.CreateRootSignature(rootSignatureDescription);

            PipelineState pipelineState = new PipelineState(device, rootSignature, shaderBytecode);

            

            // Execute computer shader

            using (CommandList commandList = new CommandList(device, CommandListType.Compute))
            {
                commandList.SetPipelineState(pipelineState);

                commandList.SetComputeRootDescriptorTable(0, widthBuffer);
                commandList.SetComputeRootDescriptorTable(1, gpuBuffer);

                commandList.Dispatch(2, 2, 1);
                commandList.Flush(true);
            }

            // Print matrix

            Console.WriteLine("Before:");
            PrintMatrix(array, width, height);

            gpuBuffer.GetData(array.AsSpan());

            Console.WriteLine();
            Console.WriteLine("After:");
            PrintMatrix(array, width, height);
        }

        private static void PrintMatrix(float[] array, int width, int height)
        {
            int numberWidth = array.Max().ToString().Length;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(array[x + y * width].ToString().PadLeft(numberWidth));
                    Console.Write(", ");
                }

                Console.WriteLine();
            }
        }
    }
}

