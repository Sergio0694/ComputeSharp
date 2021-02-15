using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Sample
{
    partial class Program
    {
        static void Main()
        {
            // Create the graphics buffer
            int width = 10;
            int height = 10;
            float[] array = new float[width * height];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }

            using ReadWriteBuffer<float> gpuBuffer = Gpu.Default.AllocateReadWriteBuffer(array);

            // Run the shader
            Gpu.Default.For(100, new MainKernel(gpuBuffer));

            // Print the initial matrix
            PrintMatrix(array, width, height, "BEFORE");

            // Get the data back
            gpuBuffer.CopyTo(array);

            // Print the updated matrix
            PrintMatrix(array, width, height, "AFTER");
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

            int length = width * height;
            int numberWidth = array.Max().ToString(CultureInfo.InvariantCulture).Length;
            ref float r = ref array[0];

            for (int i = 0; i < length; i++)
            {
                Console.Write(r.ToString(CultureInfo.InvariantCulture).PadLeft(numberWidth));
                r = ref Unsafe.Add(ref r, 1);

                if (i < length - 1) Console.Write(", ");
                if (i > 0 && (i + 1) % width == 0) Console.WriteLine();
            }

            Console.WriteLine(new string('=', 50));
        }
    }
}

