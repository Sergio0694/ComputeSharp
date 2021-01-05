using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ComputeSharp.Graphics;

namespace ComputeSharp.Benchmark.Blas
{
    /// <summary>
    /// A <see langword="class"/> that contains primitives to run certain operations of a neural network.
    /// </summary>
    internal static partial class BlasHelpers
    {
        /// <summary>
        /// Executes the forward pass on a fully connected layer on the CPU.
        /// </summary>
        /// <param name="c">The number of samples in the input tensor.</param>
        /// <param name="n">The number of rows in the input matrix.</param>
        /// <param name="m">The number of columns in the input matrix.</param>
        /// <param name="p">The number of columns in the output matrix.</param>
        /// <param name="x">The input tensor.</param>
        /// <param name="w">The weights tensor.</param>
        /// <param name="b">The bias tensor.</param>
        /// <param name="y">The result tensor.</param>
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
        /// Executes the forward pass on a fully connected layer on the GPU.
        /// </summary>
        /// <param name="device">The device to run the computation on.</param>
        /// <param name="c">The number of samples in the input tensor.</param>
        /// <param name="n">The number of rows in the input matrix.</param>
        /// <param name="m">The number of columns in the input matrix.</param>
        /// <param name="p">The number of columns in the output matrix.</param>
        /// <param name="x">The input tensor.</param>
        /// <param name="w">The weights tensor.</param>
        /// <param name="b">The bias tensor.</param>
        /// <param name="y">The result tensor.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FullyConnectedForwardGpu(
            GraphicsDevice device,
            int c,
            int n,
            int m,
            int p,
            ReadOnlyBuffer<float> x,
            ReadOnlyBuffer<float> w,
            ReadOnlyBuffer<float> b,
            ReadWriteBuffer<float> y)
        {
            device.For(c, n, p, new FullyConnectedForwardKernel(n, m, p, x, w, b, y));
        }

        /// <summary>
        /// Kernel for <see cref="FullyConnectedForwardGpu"/>.
        /// </summary>
        [AutoConstructor]
        public readonly partial struct FullyConnectedForwardKernel : IComputeShader
        {
            public readonly int n;
            public readonly int m;
            public readonly int p;
            public readonly ReadOnlyBuffer<float> x;
            public readonly ReadOnlyBuffer<float> w;
            public readonly ReadOnlyBuffer<float> b;
            public readonly ReadWriteBuffer<float> y;

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
