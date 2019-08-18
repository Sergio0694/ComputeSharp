using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers;

namespace ComputeSharp.Benchmark
{
    /// <summary>
    /// A <see langword="class"/> that benchmarks a fully connected layer forward pass on both CPU and GPU
    /// </summary>
    internal sealed class FullyConnectedLayerBenchmark : IDisposable
    {
        /// <summary>
        /// The number of samples
        /// </summary>
        private const int C = 128;

        /// <summary>
        /// The nummber of rows in the <see cref="X"/> matrix
        /// </summary>
        private const int N = 512;

        /// <summary>
        /// The number of columns in the <see cref="X"/> matrix (same as the number of rows in the <see cref="W"/> matrix)
        /// </summary>
        private const int M = 512;

        /// <summary>
        /// The number of columns in the <see cref="W"/> matrix
        /// </summary>
        private const int P = 256;

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
        /// The input tensor
        /// </summary>
        private readonly ReadOnlyBuffer<float> BufferX;

        /// <summary>
        /// The weights tensor
        /// </summary>
        private readonly ReadOnlyBuffer<float> BufferW;

        /// <summary>
        /// The bias tensor
        /// </summary>
        private readonly ReadOnlyBuffer<float> BufferB;

        /// <summary>
        /// The result tensor
        /// </summary>
        private readonly ReadWriteBuffer<float> BufferY;

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

            BufferX = Gpu.Default.AllocateReadOnlyBuffer(X);
            BufferW = Gpu.Default.AllocateReadOnlyBuffer(W);
            BufferB = Gpu.Default.AllocateReadOnlyBuffer(B);
            BufferY = Gpu.Default.AllocateReadWriteBuffer(Y);
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

        /// <summary>
        /// Runs a fully connected forward operation on the CPU
        /// </summary>
        public void Cpu() => Dnn.FullyConnectedForwardCpu(C, N, M, P, X, W, B, Y);

        /// <summary>
        /// Runs a fully connected forward operation on the GPU
        /// </summary>
        public void GpuWithNoTemporaryBuffers() => Dnn.FullyConnectedForwardGpu(C, N, M, P, BufferX, BufferW, BufferB, BufferY);

        /// <summary>
        /// Runs a fully connected forward operation on the GPU, creating temporary GPU buffers to perform the operations
        /// </summary>
        public void GpuWithTemporaryBuffers()
        {
            using ReadOnlyBuffer<float> x = Gpu.Default.AllocateReadOnlyBuffer(X);
            using ReadOnlyBuffer<float> w = Gpu.Default.AllocateReadOnlyBuffer(W);
            using ReadOnlyBuffer<float> b = Gpu.Default.AllocateReadOnlyBuffer(B);
            using ReadWriteBuffer<float> y = Gpu.Default.AllocateReadWriteBuffer(Y);

            Dnn.FullyConnectedForwardGpu(C, N, M, P, x, w, b, y);

            y.GetData(Y);
        }

        /// <summary>
        /// Checks whether or not thhe CPU and GPU implementations produce the same output
        /// </summary>
        public bool EnsureImplementationsMatch()
        {
            // Run the CPU implementation and copy the result
            Cpu();
            float[] y_cpu = new float[Y.Length];
            Y.AsSpan().CopyTo(y_cpu);

            // Run the GPU implementation with temporary buffers
            GpuWithTemporaryBuffers();

            // Compare the two results
            ref float r_cpu = ref y_cpu[0];
            ref float r_gpu = ref Y[0];
            for (int i = 0; i < Y.Length; i++)
                if (MathF.Abs(Unsafe.Add(ref r_cpu, i) - Unsafe.Add(ref r_gpu, i)) > 0.0001f)
                    return false;

            return true;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            BufferX.Dispose();
            BufferW.Dispose();
            BufferB.Dispose();
            BufferY.Dispose();
        }
    }
}
