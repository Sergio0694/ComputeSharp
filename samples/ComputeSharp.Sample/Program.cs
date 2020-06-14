using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Sample
{
    class Program
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

            // Print the initial matrix
            Console.WriteLine("===================== BEFORE =====================");
            PrintMatrix(array, width, height);
            Console.WriteLine("==================================================");
            Console.WriteLine();

            using ReadWriteBuffer<float> gpuBuffer = Gpu.Default.AllocateReadWriteBuffer(array);

            // Run the shader
            Gpu.Default.For(100, new MainKernel(width, gpuBuffer));

            // Get the data back
            gpuBuffer.GetData(array);

            // Print the updated matrix
            Console.WriteLine("===================== AFTER ======================");
            PrintMatrix(array, width, height);
            Console.WriteLine("==================================================");
        }

        /// <summary>
        /// Kernel for <see cref="Main"/>
        /// </summary>
        private readonly struct MainKernel : IComputeShader
        {
            private readonly int width;

            private readonly ReadWriteBuffer<float> buffer;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public MainKernel(int width, ReadWriteBuffer<float> buffer)
            {
                this.width = width;
                this.buffer = buffer;
            }

            /// <inheritdoc/>
            public void Execute(ThreadIds ids)
            {
                int offset = ids.X + ids.Y * width;
                buffer[offset] *= 2;
            }
        }

        /// <summary>
        /// Prints a matrix in a properly formatted way
        /// </summary>
        /// <param name="array">The input <see langword="float"/> array representing the matrix to print</param>
        /// <param name="width">The width of the array to print</param>
        /// <param name="height">The height of the array to print</param>
        private static void PrintMatrix(float[] array, int width, int height)
        {
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
        }
    }
}

