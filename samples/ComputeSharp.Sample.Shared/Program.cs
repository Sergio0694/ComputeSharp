using System;
using System.Globalization;
using System.Linq;

namespace ComputeSharp.Sample
{
    partial class Program
    {
        static void Main()
        {
            float[] array = Enumerable.Range(1, 100).Select(static i => (float)i).ToArray();

            // Create the graphics buffer
            using ReadWriteBuffer<float> gpuBuffer = Gpu.Default.AllocateReadWriteBuffer(array);

            // Run the shader
            Gpu.Default.For(100, new MainKernel(gpuBuffer));

            // Print the initial matrix
            PrintMatrix(array, 10, 10, "BEFORE");

            // Get the data back
            gpuBuffer.CopyTo(array);

            // Print the updated matrix
            PrintMatrix(array, 10, 10, "AFTER");
        }

        /// <summary>
        /// Kernel for <see cref="Main"/>.
        /// </summary>
        [AutoConstructor]
        internal readonly partial struct MainKernel : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;

            /// <inheritdoc/>
            public void Execute()
            {
                buffer[ThreadIds.X] *= 2;
            }
        }

        /// <summary>
        /// Prints a matrix in a properly formatted way.
        /// </summary>
        /// <param name="array">The input <see langword="float"/> array representing the matrix to print.</param>
        /// <param name="width">The width of the array to print.</param>
        /// <param name="height">The height of the array to print.</param>
        /// <param name="name">The name of the matrix to print.</param>
        private static void PrintMatrix(float[] array, int width, int height, string name)
        {
            int pad = 48 - name.Length;
            string title = $"{new string('=', pad / 2)} {name} {new string('=', (pad + 1) / 2)}";

            Console.WriteLine(title);

            int numberWidth = Math.Max(array.Max().ToString(CultureInfo.InvariantCulture).Length, 4);

            for (int i = 0; i < height; i++)
            {
                var row = array[(i * width)..((i + 1) * width)];
                var text = string.Join(",", row.Select(x => x.ToString(CultureInfo.InvariantCulture).PadLeft(numberWidth)));

                Console.WriteLine(text);
            }

            Console.WriteLine(new string('=', 50));
        }
    }
}

