using System;
using System.Linq;
using ComputeSharp;
using ComputeSharp.Graphics.Buffers;
using ComputeSharp.Graphics.Buffers.Extensions;

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

            using ReadWriteBuffer<float> gpuBuffer = Gpu.Default.AllocateReadWriteBuffer(array.AsSpan());

            // Shader body
            Action<ThreadIds> action = id =>
            {
                gpuBuffer[id.X + id.Y * (uint)width] *= 2;
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

