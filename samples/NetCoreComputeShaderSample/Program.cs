using System;
using System.Linq;
using ComputeSharp;
using ComputeSharp.Graphics.Buffers;

namespace NetCoreComputeShaderSample
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

            // Print the initial matrix
            Console.WriteLine("Before:");
            PrintMatrix(array, width, height);

            using ReadWriteBuffer<float> gpuBuffer = Gpu.Default.AllocateReadWriteBuffer(array);

            // Shader body
            Action<ThreadIds> action = id =>
            {
                uint offset = id.X + id.Y * (uint)width;
                gpuBuffer[offset] *= 2;
            };

            // Run the shader
            Gpu.Default.For(100, action);

            // Get the data back
            gpuBuffer.GetData(array);

            // Print the updated matrix
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

