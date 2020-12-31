using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Rectangle = System.Drawing.Rectangle;

namespace ComputeSharp.ShareX
{
    /// <summary>
    /// Applies Gaussian blur processing to an image.
    /// </summary>
    internal sealed partial class HlslConvolutionManager
    {
        /// <summary>
        /// The 1D gaussian blur kernel to use.
        /// </summary>
        private readonly float[] kernel;

        /// <summary>
        /// Creates a new <see cref="HlslConvolutionManager"/> instance for the specific kernel.
        /// </summary>
        /// <param name="kernel">The convolution kernel to use.</param>
        private HlslConvolutionManager(float[] kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Creates a new <see cref="HlslConvolutionManager"/> instance with the specified parameters.
        /// </summary>
        /// <param name="size">The size of the convolution kernel.</param>
        /// <param name="sigma">The sigma parameter for the gaussian blur.</param>
        /// <returns>An <see cref="HlslConvolutionManager"/> instance to use to blur images.</returns>
        public static HlslConvolutionManager Create(int size, float sigma)
        {
            return new(CreateGaussianBlurKernel(size, sigma));
        }

        /// <summary>
        /// Creates a 1 dimensional Gaussian kernel using the Gaussian G(x) function.
        /// </summary>
        private static float[] CreateGaussianBlurKernel(int size, float weight)
        {
            static float Gaussian(float x, float sigma)
            {
                const float Numerator = 1.0f;
                float denominator = MathF.Sqrt(2 * MathF.PI) * sigma;

                float exponentNumerator = -x * x;
                float exponentDenominator = 2 * sigma * sigma;

                float left = Numerator / denominator;
                float right = MathF.Exp(exponentNumerator / exponentDenominator);

                return left * right;
            }

            float[] kernel = new float[size];
            ref float rKernel = ref kernel[0];

            float sum = 0F;
            float midpoint = (size - 1) / 2F;

            for (int i = 0; i < size; i++)
            {
                float x = i - midpoint;
                float gx = Gaussian(x, weight);
                sum += gx;
                Unsafe.Add(ref rKernel, i) = gx;
            }

            for (int i = 0; i < size; i++)
            {
                Unsafe.Add(ref rKernel, i) /= sum;
            }

            return kernel;
        }

        /// <summary>
        /// Applies the current convolution to a give bitmap.
        /// </summary>
        /// <param name="source">The input <see cref="Bitmap"/> instance to process.</param>
        /// <returns>A blurred copy of <paramref name="source"/>.</returns>
        public unsafe Bitmap Apply(Bitmap source)
        {
            Rectangle bounds = new(0, 0, source.Width, source.Height);
            Bitmap result = new(source.Width, source.Height);
            BitmapData
                sourceData = source.LockBits(bounds, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb),
                resultData = result.LockBits(bounds, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Span<Rgba32>
                sourceSpan = new((Rgba32*)sourceData.Scan0.ToPointer(), bounds.Width * bounds.Height),
                resultSpan = new((Rgba32*)resultData.Scan0.ToPointer(), sourceSpan.Length);
            Span<Vector4> vector4Span = new((void*)Marshal.AllocHGlobal(sizeof(Vector4) * sourceSpan.Length), sourceSpan.Length);

            try
            {
                PixelOperations<Rgba32>.Instance.ToVector4(Configuration.Default, sourceSpan, vector4Span);

                using ReadWriteBuffer<Vector4> leftBuffer = Gpu.Default.AllocateReadWriteBuffer<Vector4>(vector4Span);
                using ReadWriteBuffer<Vector4> rightBuffer = Gpu.Default.AllocateReadWriteBuffer<Vector4>(sourceSpan.Length);
                using ReadOnlyBuffer<float> kernelBuffer = Gpu.Default.AllocateReadOnlyBuffer(this.kernel);

                ApplyVerticalConvolution(leftBuffer, rightBuffer, kernelBuffer, source.Height, source.Width);
                ApplyHorizontalConvolution(rightBuffer, leftBuffer, kernelBuffer, source.Height, source.Width);

                leftBuffer.GetData(vector4Span);

                PixelOperations<Rgba32>.Instance.FromVector4Destructive(Configuration.Default, vector4Span, resultSpan);
            }
            finally
            {
                Marshal.FreeHGlobal((IntPtr)Unsafe.AsPointer(ref vector4Span.GetPinnableReference()));

                source.UnlockBits(sourceData);
                result.UnlockBits(resultData);
            }

            return result;
        }

        /// <summary>
        /// Performs a vertical 1D complex convolution with the specified parameters.
        /// </summary>
        /// <param name="source">The source <see cref="ReadWriteBuffer{T}"/> to read data from.</param>
        /// <param name="target">The target <see cref="ReadWriteBuffer{T}"/> to write the results to.</param>
        /// <param name="kernel">The <see cref="ReadOnlyBuffer{T}"/> with the values for the current complex kernel.</param>
        /// <param name="height">The height of the image to process.</param>
        /// <param name="width">The width of the image to process.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ApplyVerticalConvolution(
            ReadWriteBuffer<Vector4> source,
            ReadWriteBuffer<Vector4> target,
            ReadOnlyBuffer<float> kernel,
            int height,
            int width)
        {
            VerticalConvolutionProcessor shader = new(
                width,
                maxY: height - 1,
                maxX: width - 1,
                kernel.Length,
                source,
                target,
                kernel);

            Gpu.Default.For(width, height, in shader);
        }

        /// <summary>
        /// Kernel for the vertical convolution pass.
        /// </summary>
        [AutoConstructor]
        internal readonly partial struct VerticalConvolutionProcessor : IComputeShader
        {
            public readonly int width;
            public readonly int maxY;
            public readonly int maxX;
            public readonly int kernelLength;

            public readonly ReadWriteBuffer<Vector4> source;
            public readonly ReadWriteBuffer<Vector4> target;
            public readonly ReadOnlyBuffer<float> kernel;

            /// <inheritdoc/>
            public void Execute(ThreadIds ids)
            {
                Vector4 result = Vector4.Zero;
                int radiusY = kernelLength >> 1;
                int sourceOffsetColumnBase = ids.X;

                for (int i = 0; i < kernelLength; i++)
                {
                    int offsetY = Hlsl.Clamp(ids.Y + i - radiusY, 0, maxY);
                    int offsetX = Hlsl.Clamp(sourceOffsetColumnBase, 0, maxX);
                    Vector4 color = source[offsetY * width + offsetX];

                    result += kernel[i] * color;
                }

                int offsetXY = ids.Y * width + ids.X;
                target[offsetXY] = result;
            }
        }

        /// <summary>
        /// Performs an horizontal 1D complex convolution with the specified parameters.
        /// </summary>
        /// <param name="source">The source <see cref="ReadWriteBuffer{T}"/> to read data from.</param>
        /// <param name="target">The target <see cref="ReadWriteBuffer{T}"/> to write the results to.</param>
        /// <param name="kernel">The <see cref="ReadOnlyBuffer{T}"/> with the values for the current complex kernel.</param>
        /// <param name="height">The height of the image to process.</param>
        /// <param name="width">The width of the image to process.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ApplyHorizontalConvolution(
            ReadWriteBuffer<Vector4> source,
            ReadWriteBuffer<Vector4> target,
            ReadOnlyBuffer<float> kernel,
            int height,
            int width)
        {
            HorizontalConvolutionProcessor shader = new(
                width,
                maxY: height - 1,
                maxX: width - 1,
                kernel.Length,
                source,
                target,
                kernel);

            Gpu.Default.For(width, height, in shader);
        }

        /// <summary>
        /// Kernel for the horizontal convolution pass.
        /// </summary>
        [AutoConstructor]
        internal readonly partial struct HorizontalConvolutionProcessor : IComputeShader
        {
            public readonly int width;
            public readonly int maxY;
            public readonly int maxX;
            public readonly int kernelLength;

            public readonly ReadWriteBuffer<Vector4> source;
            public readonly ReadWriteBuffer<Vector4> target;
            public readonly ReadOnlyBuffer<float> kernel;

            /// <inheritdoc/>
            public void Execute(ThreadIds ids)
            {
                Vector4 result = Vector4.Zero;
                int radiusX = kernelLength >> 1;
                int sourceOffsetColumnBase = ids.X;
                int offsetY = Hlsl.Clamp(ids.Y, 0, maxY);
                int offsetXY;

                for (int i = 0; i < kernelLength; i++)
                {
                    int offsetX = Hlsl.Clamp(sourceOffsetColumnBase + i - radiusX, 0, maxX);
                    offsetXY = offsetY * width + offsetX;
                    Vector4 color = source[offsetXY];

                    result += kernel[i] * color;
                }

                offsetXY = ids.Y * width + ids.X;
                target[offsetXY] = result;
            }
        }
    }
}
