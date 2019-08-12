using DirectX12GameEngine.Shaders;
using System;
using System.Linq;
using DirectX12GameEngine.Graphics.Buffers;
using DirectX12GameEngine.Shaders.Primitives;
using Buffer = DirectX12GameEngine.Graphics.Buffers.Buffer;

namespace DirectX12ComputeShaderSample
{
    public class Program
    {
        private static void Main()
        {
            // Create the graphics buffer
            int width = 10;
            int height = 10;
            float[] array = new float[width * height];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }

            using Buffer<float> gpuBuffer = Buffer.UnorderedAccess.New(Gpu.Default, array.AsSpan());

            // Variables for closure
            uint size = (uint)width;
            var data = gpuBuffer.GetGpuResource();

            // Shader body
            Action<ThreadIds> action = id =>
            {
                data[id.X + id.Y * size] *= 2;
            };

            // Run the shader
            Gpu.Default.For(100, action);

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

