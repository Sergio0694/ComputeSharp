using DirectX12GameEngine.Graphics;
using DirectX12GameEngine.Shaders;
using DirectX12GameEngine.Shaders.Numerics;
using System;
using System.Linq;

using Buffer = DirectX12GameEngine.Graphics.Buffer;

namespace DirectX12ComputeShaderSample
{
    public class MyComputeShader
    {
        [ConstantBufferResource] public uint Width;

        [ShaderResource] public RWBufferResource<float> Data;

        [ShaderMethod]
        [Shader("compute")]
        [NumThreads(5, 5, 1)]
        public void CSMain([SystemDispatchThreadIdSemantic] UInt3 id)
        {
            Data[id.X + id.Y * Width] *= 2;
        }
    }

    public class Program
    {
        private static void Main()
        {
            // Generate computer shader

            MyComputeShader myComputeShader = new MyComputeShader();
            ShaderGenerator shaderGenerator = new ShaderGenerator(myComputeShader);
            ShaderGenerationResult result = shaderGenerator.GenerateShader();

            byte[] shaderBytecode = ShaderCompiler.CompileShader(result.ShaderSource, SharpDX.D3DCompiler.ShaderVersion.ComputeShader, result.ComputeShader);

            // Create graphics device and pipeline state

            using GraphicsDevice device = new GraphicsDevice(true);

            SharpDX.Direct3D12.RootParameter[] rootParameters = new SharpDX.Direct3D12.RootParameter[]
            {
                new SharpDX.Direct3D12.RootParameter(SharpDX.Direct3D12.ShaderVisibility.All,
                    new SharpDX.Direct3D12.RootConstants(0, 0, 1)),
                new SharpDX.Direct3D12.RootParameter(SharpDX.Direct3D12.ShaderVisibility.All,
                    new SharpDX.Direct3D12.DescriptorRange(SharpDX.Direct3D12.DescriptorRangeType.UnorderedAccessView, 1, 0))
            };

            var rootSignatureDescription = new SharpDX.Direct3D12.RootSignatureDescription(SharpDX.Direct3D12.RootSignatureFlags.None, rootParameters);
            var rootSignature = device.CreateRootSignature(rootSignatureDescription);

            PipelineState pipelineState = new PipelineState(device, rootSignature, shaderBytecode);

            // Create graphics buffer

            int width = 10;
            int height = 10;

            float[] array = new float[width * height];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }

            using Buffer<float> gpuBuffer = Buffer.UnorderedAccess.New(device, array.AsSpan());

            // Execute computer shader

            using (CommandList commandList = new CommandList(device, CommandListType.Compute))
            {
                commandList.SetPipelineState(pipelineState);

                commandList.SetComputeRoot32BitConstant(0, width, 0);
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

