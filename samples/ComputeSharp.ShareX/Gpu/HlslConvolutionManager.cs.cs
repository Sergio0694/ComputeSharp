using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.CompilerServices;
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
            Span<Bgra32>
                sourceSpan = new((Bgra32*)sourceData.Scan0.ToPointer(), bounds.Width * bounds.Height),
                resultSpan = new((Bgra32*)resultData.Scan0.ToPointer(), sourceSpan.Length);

            try
            {
                using ReadWriteTexture2D<Bgra32, Vector4> leftBuffer = Gpu.Default.AllocateReadWriteTexture2D<Bgra32, Vector4>(sourceSpan, source.Width, source.Height);
                using ReadWriteTexture2D<Vector4> rightBuffer = Gpu.Default.AllocateReadWriteTexture2D<Vector4>(source.Width, source.Height);
                using ReadOnlyBuffer<float> kernelBuffer = Gpu.Default.AllocateReadOnlyBuffer(this.kernel);

                ApplyVerticalConvolution(leftBuffer, rightBuffer, kernelBuffer, source.Height, source.Width);
                ApplyHorizontalConvolution(rightBuffer, leftBuffer, kernelBuffer, source.Height, source.Width);

                leftBuffer.GetData(resultSpan);
            }
            finally
            {
                source.UnlockBits(sourceData);
                result.UnlockBits(resultData);
            }

            return result;
        }

        /// <summary>
        /// Performs a vertical 1D complex convolution with the specified parameters.
        /// </summary>
        /// <param name="source">The source <see cref="ReadWriteTexture2D{T,TPixel}"/> to read data from.</param>
        /// <param name="target">The target <see cref="ReadWriteTexture2D{T}"/> to write the results to.</param>
        /// <param name="kernel">The <see cref="ReadOnlyBuffer{T}"/> with the values for the current complex kernel.</param>
        /// <param name="height">The height of the image to process.</param>
        /// <param name="width">The width of the image to process.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ApplyVerticalConvolution(
            ReadWriteTexture2D<Bgra32, Vector4> source,
            ReadWriteTexture2D<Vector4> target,
            ReadOnlyBuffer<float> kernel,
            int height,
            int width)
        {
            VerticalConvolutionProcessor shader = new(
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
            public readonly int maxY;
            public readonly int maxX;
            public readonly int kernelLength;

            public readonly ReadWriteTexture2D<Bgra32, Vector4> source;
            public readonly ReadWriteTexture2D<Vector4> target;
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
                    Vector4 color = source[offsetX, offsetY];

                    result += kernel[i] * color;
                }

                target[ids.XY] = result;
            }
        }

        /// <summary>
        /// Performs an horizontal 1D complex convolution with the specified parameters.
        /// </summary>
        /// <param name="source">The source <see cref="ReadWriteTexture2D{T}"/> to read data from.</param>
        /// <param name="target">The target <see cref="ReadWriteTexture2D{T,TPixel}"/> to write the results to.</param>
        /// <param name="kernel">The <see cref="ReadOnlyBuffer{T}"/> with the values for the current complex kernel.</param>
        /// <param name="height">The height of the image to process.</param>
        /// <param name="width">The width of the image to process.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ApplyHorizontalConvolution(
            ReadWriteTexture2D<Vector4> source,
            ReadWriteTexture2D<Bgra32, Vector4> target,
            ReadOnlyBuffer<float> kernel,
            int height,
            int width)
        {
            HorizontalConvolutionProcessor shader = new(
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
            public readonly int maxY;
            public readonly int maxX;
            public readonly int kernelLength;

            public readonly ReadWriteTexture2D<Vector4> source;
            public readonly ReadWriteTexture2D<Bgra32, Vector4> target;
            public readonly ReadOnlyBuffer<float> kernel;

            /// <inheritdoc/>
            public void Execute(ThreadIds ids)
            {
                Vector4 result = Vector4.Zero;
                int radiusX = kernelLength >> 1;
                int sourceOffsetColumnBase = ids.X;
                int offsetY = Hlsl.Clamp(ids.Y, 0, maxY);

                for (int i = 0; i < kernelLength; i++)
                {
                    int offsetX = Hlsl.Clamp(sourceOffsetColumnBase + i - radiusX, 0, maxX);
                    Vector4 color = source[offsetX, offsetY];

                    result += kernel[i] * color;
                }

                target[ids.XY] = result;
            }
        }
    }
}
