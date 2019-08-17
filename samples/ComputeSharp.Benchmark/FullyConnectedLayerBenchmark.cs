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
        private const int C = 128;

        /// <summary>
        /// The nummber of rows in the <see cref="X"/> matrix
        /// </summary>
        private const int N = 100;

        /// <summary>
        /// The number of columns in the <see cref="X"/> matrix (same as the number of rows in the <see cref="W"/> matrix)
        /// </summary>
        private const int M = 100;

        /// <summary>
        /// The number of columns in the <see cref="W"/> matrix
        /// </summary>
        private const int P = 100;

        /// <summary>
        /// The input tensor
        /// </summary>
        private readonly float[] X;

        /// <summary>
        /// The weights tensor
        /// </summary>
        private readonly float[] W;

        /// <summary>
        /// The bias tensor
        /// </summary>
        private readonly float[] B;

        /// <summary>
        /// The result tensor
        /// </summary>
        private readonly float[] Y;

        /// <summary>
        /// A <see cref="System.Random"/> instance to initialize the tensors
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// Creates a new <see cref="FullyConnectedLayerBenchmark"/> instance
        /// </summary>
        public FullyConnectedLayerBenchmark()
        {
            X = CreateRandomArray(C * N * M);
            W = CreateRandomArray(M * P);
            B = CreateRandomArray(P);
            Y = CreateRandomArray(C * N * P);
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
