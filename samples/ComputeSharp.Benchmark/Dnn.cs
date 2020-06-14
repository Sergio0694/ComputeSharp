using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ComputeSharp.Benchmark
{
    /// <summary>
    /// A <see langword="class"/> that contains primitives to run certain operations of a neural network
    /// </summary>
    internal static class Dnn
    {
        /// <summary>
        /// Executes the forward pass on a fully connected layer on the CPU
        /// </summary>
        /// <param name="c">The number of samples in the input tensor</param>
        /// <param name="n">The number of rows in the input matrix</param>
        /// <param name="m">The number of columns in the input matrix</param>
        /// <param name="p">The number of columns in the output matrix</param>
        /// <param name="x">The input tensor</param>
        /// <param name="w">The weights tensor</param>
        /// <param name="b">The bias tensor</param>
        /// <param name="y">The result tensor</param>
        public static void FullyConnectedForwardCpu(int c, int n, int m, int p, float[] x, float[] w, float[] b, float[] y)
        {
            void Kernel(int s)
            {
                ref float rx = ref x[s * n * m];
                ref float rw = ref w[0];
                ref float rb = ref b[0];
                ref float ry = ref y[s * n * p];

                for (int i = 0; i < n; i++)
                {
                    int x_offset = i * m;
                    int y_offset = i * p;

                    for (int j = 0; j < p; j++)
                    {
                        float result = 0f;

                        for (int k = 0; k < m; k++)
                        {
                            result += Unsafe.Add(ref rx, x_offset + k) * Unsafe.Add(ref rw, k * p + j);
                        }

                        Unsafe.Add(ref ry, y_offset + j) = result + Unsafe.Add(ref rb, j);
                    }
                }
            }

            Parallel.For(0, c, Kernel);
        }

        /// <summary>
        /// Executes the forward pass on a fully connected layer on the GPU
        /// </summary>
        /// <param name="c">The number of samples in the input tensor</param>
        /// <param name="n">The number of rows in the input matrix</param>
        /// <param name="m">The number of columns in the input matrix</param>
        /// <param name="p">The number of columns in the output matrix</param>
        /// <param name="x">The input tensor</param>
        /// <param name="w">The weights tensor</param>
        /// <param name="b">The bias tensor</param>
        /// <param name="y">The result tensor</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FullyConnectedForwardGpu(
            int c, int n, int m, int p,
            ReadOnlyBuffer<float> x, ReadOnlyBuffer<float> w, ReadOnlyBuffer<float> b, ReadWriteBuffer<float> y)
        {
            Gpu.Default.For(c, n, p, new FullyConnectedForwardKernel(n, m, p, x, w, b, y));
        }

        /// <summary>
        /// Kernel for <see cref="FullyConnectedForwardGpu"/>
        /// </summary>
        private readonly struct FullyConnectedForwardKernel : IComputeShader
        {
            private readonly int n;
            private readonly int m;
            private readonly int p;

            private readonly ReadOnlyBuffer<float> x;
            private readonly ReadOnlyBuffer<float> w;
            private readonly ReadOnlyBuffer<float> b;
            private readonly ReadWriteBuffer<float> y;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public FullyConnectedForwardKernel(
                int n,
                int m,
                int p,
                ReadOnlyBuffer<float> x,
                ReadOnlyBuffer<float> w,
                ReadOnlyBuffer<float> b,
                ReadWriteBuffer<float> y)
            {
                this.n = n;
                this.m = m;
                this.p = p;
                this.x = x;
                this.w = w;
                this.b = b;
                this.y = y;
            }

            /// <inheritdoc/>
            public void Execute(ThreadIds ids)
            {
                int x_offset = ids.X * n * p + ids.Y * m;
                float result = 0f;

                for (int k = 0; k < m; k++)
                {
                    result += x[x_offset + k] * w[k * p + ids.Z];
                }

                y[ids.X * n * p + ids.Y * p + ids.Z] = result + b[ids.Z];
            }
        }
    }
}
