using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace ComputeSharp.BokehBlur.Processors
{
    /// <summary>
    /// Applies Gaussian blur processing to an image.
    /// </summary>
    /// <typeparam name="TPixel">The pixel format.</typeparam>
    internal class HlslGaussianBlurProcessor<TPixel> : ImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        /// <summary>
        /// The 1D kernel to apply
        /// </summary>
        private readonly float[] Kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class
        /// </summary>
        /// <param name="definition">The <see cref="HlslGaussianBlurProcessor"/> defining the processor parameters</param>
        /// <param name="configuration">The configuration which allows altering default behaviour or extending the library</param>
        /// <param name="source">The source <see cref="Image{TPixel}"/> for the current processor instance</param>
        /// <param name="sourceRectangle">The source area to process for the current processor instance</param>
        public HlslGaussianBlurProcessor(HlslGaussianBlurProcessor definition, Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
            : base(configuration, source, sourceRectangle)
        {
            int kernelSize = definition.Radius * 2 + 1;
            Kernel = CreateGaussianBlurKernel(kernelSize, definition.Sigma);
        }

        /// <summary>
        /// Creates a 1 dimensional Gaussian kernel using the Gaussian G(x) function
        /// </summary>
        private static float[] CreateGaussianBlurKernel(int size, float weight)
        {
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

            // Normalize kernel so that the sum of all weights equals 1
            for (int i = 0; i < size; i++)
            {
                Unsafe.Add(ref rKernel, i) /= sum;
            }

            return kernel;
        }

        /// <summary>
        /// Implementation of 1D Gaussian G(x) function
        /// </summary>
        /// <param name="x">The x provided to G(x)</param>
        /// <param name="sigma">The spread of the blur</param>
        /// <returns>The Gaussian G(x)</returns>
        private static float Gaussian(float x, float sigma)
        {
            const float Numerator = 1.0f;
            float denominator = MathF.Sqrt(2 * MathF.PI) * sigma;

            float exponentNumerator = -x * x;
            float exponentDenominator = 2 * sigma * sigma;

            float left = Numerator / denominator;
            float right = MathF.Exp(exponentNumerator / exponentDenominator);

            return left * right;
        }

        /// <inheritdoc/>
        protected override void OnFrameApply(ImageFrame<TPixel> source)
        {
            foreach (Memory<TPixel> memory in source.GetPixelMemoryGroup())
            {
                Span<TPixel> spanTPixel = memory.Span;

                using IMemoryOwner<Vector4> group4 = Configuration.MemoryAllocator.Allocate<Vector4>(spanTPixel.Length);

                Span<Vector4> spanVector4 = group4.Memory.Span;

                PixelOperations<TPixel>.Instance.ToVector4(Configuration, spanTPixel, spanVector4);

                using ReadWriteBuffer<Vector4> sourceBuffer = Gpu.Default.AllocateReadWriteBuffer(spanVector4);
                using ReadWriteBuffer<Vector4> firstPassBuffer = Gpu.Default.AllocateReadWriteBuffer<Vector4>(sourceBuffer.Size);
                using ReadOnlyBuffer<float> kernelBuffer = Gpu.Default.AllocateReadOnlyBuffer(Kernel);

                ApplyVerticalConvolution(sourceBuffer, firstPassBuffer, kernelBuffer);
                ApplyHorizontalConvolution(firstPassBuffer, sourceBuffer, kernelBuffer);

                sourceBuffer.GetData(spanVector4);

                PixelOperations<TPixel>.Instance.FromVector4Destructive(Configuration, spanVector4, spanTPixel);
            }
        }

        /// <summary>
        /// Performs a vertical 1D complex convolution with the specified parameters
        /// </summary>
        /// <param name="source">The source <see cref="ReadWriteBuffer{T}"/> to read data from</param>
        /// <param name="target">The target <see cref="ReadWriteBuffer{T}"/> to write the results to</param>
        /// <param name="kernel">The <see cref="ReadOnlyBuffer{T}"/> with the values for the current complex kernel</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ApplyVerticalConvolution(
            ReadWriteBuffer<Vector4> source,
            ReadWriteBuffer<Vector4> target,
            ReadOnlyBuffer<float> kernel)
        {
            int height = Source.Height;
            int width = Source.Width;

            Gpu.Default.For(width, height, new VerticalConvolutionProcessor
            {
                width = width,
                maxY = height - 1,
                maxX = width - 1,
                kernelLength = kernel.Size,
                source = source,
                target = target,
                kernel = kernel
            });
        }

        /// <summary>
        /// Kernel for <see cref="ApplyVerticalConvolution"/>
        /// </summary>
        private struct VerticalConvolutionProcessor : IComputeShader
        {
            public int width;
            public int maxY;
            public int maxX;
            public int kernelLength;

            public ReadWriteBuffer<Vector4> source;
            public ReadWriteBuffer<Vector4> target;
            public ReadOnlyBuffer<float> kernel;

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
        /// Performs an horizontal 1D complex convolution with the specified parameters
        /// </summary>
        /// <param name="source">The source <see cref="ReadWriteBuffer{T}"/> to read data from</param>
        /// <param name="target">The target <see cref="ReadWriteBuffer{T}"/> to write the results to</param>
        /// <param name="kernel">The <see cref="ReadOnlyBuffer{T}"/> with the values for the current complex kernel</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ApplyHorizontalConvolution(
            ReadWriteBuffer<Vector4> source,
            ReadWriteBuffer<Vector4> target,
            ReadOnlyBuffer<float> kernel)
        {
            int height = Source.Height;
            int width = Source.Width;

            Gpu.Default.For(width, height, new HorizontalConvolutionProcessor
            {
                width = width,
                maxY = height - 1,
                maxX = width - 1,
                kernelLength = kernel.Size,
                source = source,
                target = target,
                kernel = kernel
            });
        }

        /// <summary>
        /// Kernel for <see cref="ApplyHorizontalConvolution"/>
        /// </summary>
        private struct HorizontalConvolutionProcessor : IComputeShader
        {
            public int width;
            public int maxY;
            public int maxX;
            public int kernelLength;

            public ReadWriteBuffer<Vector4> source;
            public ReadWriteBuffer<Vector4> target;
            public ReadOnlyBuffer<float> kernel;

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
