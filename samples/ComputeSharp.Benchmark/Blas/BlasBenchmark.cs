using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace ComputeSharp.Benchmark.Blas
{
    /// <summary>
    /// A <see langword="class"/> that benchmarks the APIs in the <see cref="BlasHelpers"/> class, on both CPU and GPU.
    /// </summary>
    [SimpleJob(RunStrategy.Monitoring)]
    public class BlasBenchmark : IDisposable
    {
        /// <summary>
        /// The number of samples.
        /// </summary>
        private const int C = 128;

        /// <summary>
        /// The nummber of rows in the <see cref="X"/> matrix.
        /// </summary>
        private const int N = 512;

        /// <summary>
        /// The number of columns in the <see cref="X"/> matrix (same as the number of rows in the <see cref="W"/> matrix)
        /// </summary>
        private const int M = 512;

        /// <summary>
        /// The number of columns in the <see cref="W"/> matrix.
        /// </summary>
        private const int P = 256;

        /// <summary>
        /// The input tensor.
        /// </summary>
        private float[] X;

        /// <summary>
        /// The weights tensor.
        /// </summary>
        private float[] W;

        /// <summary>
        /// The bias tensor.
        /// </summary>
        private float[] B;

        /// <summary>
        /// The result tensor.
        /// </summary>
        private float[] Y;

        /// <summary>
        /// The input tensor (GPU).
        /// </summary>
        private ReadOnlyBuffer<float> BufferX;

        /// <summary>
        /// The weights tensor (GPU).
        /// </summary>
        private ReadOnlyBuffer<float> BufferW;

        /// <summary>
        /// The bias tensor (GPU).
        /// </summary>
        private ReadOnlyBuffer<float> BufferB;

        /// <summary>
        /// The result tensor (GPU).
        /// </summary>
        private ReadWriteBuffer<float> BufferY;

        /// <summary>
        /// A <see cref="System.Random"/> instance to initialize the tensors.
        /// </summary>
        private readonly Random Random = new Random();

        /// <summary>
        /// Initial setup for a benchmarking session.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            // Creates a new random normalized float[] array of a given size
            float[] CreateRandomArray(int size)
            {
                float[] array = ArrayPool<float>.Shared.Rent(size);
                ref float r = ref array[0];
                for (int i = 0; i < size; i++)
                {
                    Unsafe.Add(ref r, i) = (float)Random.NextDouble();
                }

                return array;
            }

            X = CreateRandomArray(C * N * M);
            W = CreateRandomArray(M * P);
            B = CreateRandomArray(P);
            Y = CreateRandomArray(C * N * P);

            BufferX = Gpu.Default.AllocateReadOnlyBuffer(X);
            BufferW = Gpu.Default.AllocateReadOnlyBuffer(W);
            BufferB = Gpu.Default.AllocateReadOnlyBuffer(B);
            BufferY = Gpu.Default.AllocateReadWriteBuffer(Y);

            Cpu();
            GpuWithNoTemporaryBuffers();
            GpuWithTemporaryBuffers();
        }

        /// <inheritdoc/>
        [GlobalCleanup]
        public void Dispose()
        {
            ArrayPool<float>.Shared.Return(X);
            ArrayPool<float>.Shared.Return(W);
            ArrayPool<float>.Shared.Return(B);
            ArrayPool<float>.Shared.Return(Y);

            BufferX.Dispose();
            BufferW.Dispose();
            BufferB.Dispose();
            BufferY.Dispose();
        }

        /// <summary>
        /// Runs a fully connected forward operation on the CPU.
        /// </summary>
        [Benchmark(Baseline = true)]
        public void Cpu()
        {
            BlasHelpers.FullyConnectedForwardCpu(C, N, M, P, X, W, B, Y);
        }

        /// <summary>
        /// Runs a fully connected forward operation on the GPU.
        /// </summary>
        [Benchmark]
        public void GpuWithNoTemporaryBuffers()
        {
            BlasHelpers.FullyConnectedForwardGpu(Gpu.Default, C, N, M, P, BufferX, BufferW, BufferB, BufferY);
        }

        /// <summary>
        /// Runs a fully connected forward operation on the GPU, creating temporary GPU buffers to perform the operations.
        /// </summary>
        [Benchmark]
        public void GpuWithTemporaryBuffers()
        {
            using ReadOnlyBuffer<float> x = Gpu.Default.AllocateReadOnlyBuffer(X);
            using ReadOnlyBuffer<float> w = Gpu.Default.AllocateReadOnlyBuffer(W);
            using ReadOnlyBuffer<float> b = Gpu.Default.AllocateReadOnlyBuffer(B);
            using ReadWriteBuffer<float> y = Gpu.Default.AllocateReadWriteBuffer<float>(Y.Length);

            BlasHelpers.FullyConnectedForwardGpu(Gpu.Default, C, N, M, P, x, w, b, y);

            y.CopyTo(Y);
        }
    }
}
