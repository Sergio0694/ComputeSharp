using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Benchmark
{
    /// <summary>
    /// A <see langword="class"/> that benchmarks a fully connected layer forward pass on both CPU and GPU
    /// </summary>
    public class FullyConnectedLayerBenchmark
    {
        /// <summary>
        /// The number of samples
        /// </summary>
        private const int Count = 128;

        /// <summary>
        /// The nummber of rows in the <see cref="A"/> matrix
        /// </summary>
        private const int N = 100;

        /// <summary>
        /// The number of columns in the <see cref="A"/> matrix (same as the number of rows in the <see cref="B"/> matrix)
        /// </summary>
        private const int M = 100;

        /// <summary>
        /// The number of columns in the <see cref="B"/> matrix
        /// </summary>
        private const int P = 100;

        private readonly float[] A;

        private readonly float[] B;

        private readonly float[] C;

        private readonly float[] D;

        private static readonly Random Random = new Random();

        /// <summary>
        /// Creates a new <see cref="FullyConnectedLayerBenchmark"/> instance
        /// </summary>
        public FullyConnectedLayerBenchmark()
        {
            A = CreateRandomArray(Count * N * M);
            B = CreateRandomArray(M * P);
            C = CreateRandomArray(N * P);
            D = new float[Count * N * P];
        }

        /// <summary>
        /// Creates a new <see langword="float"/> array of the specified size
        /// </summary>
        /// <param name="size">The size of the new random array to create</param>
        [Pure]
        private static float[] CreateRandomArray(int size)
        {
            float[] array = new float[size];
            ref float r = ref array[0];
            for (int i = 0; i < size; i++)
            {
                Unsafe.Add(ref r, i) = (float)Random.NextDouble();
            }

            return array;
        }
    }
}
