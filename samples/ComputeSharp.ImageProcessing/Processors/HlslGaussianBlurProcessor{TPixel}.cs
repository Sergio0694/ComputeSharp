using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Toolkit.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.BokehBlur.Processors
{
    /// <summary>
    /// Applies Gaussian blur processing to an image.
    /// </summary>
    /// <typeparam name="TPixel">The pixel format.</typeparam>
    internal sealed class HlslGaussianBlurProcessor<TPixel> : ImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        /// <summary>
        /// The 1D kernel to apply.
        /// </summary>
        private readonly float[] Kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class.
        /// </summary>
        /// <param name="definition">The <see cref="HlslGaussianBlurProcessor"/> defining the processor parameters.</param>
        /// <param name="configuration">The configuration which allows altering default behaviour or extending the library.</param>
        /// <param name="source">The source <see cref="Image{TPixel}"/> for the current processor instance.</param>
        /// <param name="sourceRectangle">The source area to process for the current processor instance.</param>
        public HlslGaussianBlurProcessor(HlslGaussianBlurProcessor definition, Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
            : base(configuration, source, sourceRectangle)
        {
            int kernelSize = definition.Radius * 2 + 1;
            Kernel = CreateGaussianBlurKernel(kernelSize, definition.Sigma);
        }

        /// <summary>
        /// Creates a 1 dimensional Gaussian kernel using the Gaussian G(x) function.
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
        /// Implementation of 1D Gaussian G(x) function.
        /// </summary>
        /// <param name="x">The x provided to G(x)</param>
        /// <param name="sigma">The spread of the blur.</param>
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
            ImageFrame<ImageSharpRgba32> frame = (ImageFrame<ImageSharpRgba32>)(object)source;

            if (!frame.TryGetSinglePixelSpan(out Span<ImageSharpRgba32> pixelSpan))
            {
                ThrowHelper.ThrowInvalidOperationException("Cannot process image frames wrapping discontiguous memory");
            }

            Span<Rgba32> span = MemoryMarshal.Cast<ImageSharpRgba32, Rgba32>(pixelSpan);

            using ReadWriteTexture2D<Rgba32, Vector4> sourceTexture = Gpu.Default.AllocateReadWriteTexture2D<Rgba32, Vector4>(span, frame.Width, frame.Height);
            using ReadWriteTexture2D<Vector4> firstPassTexture = Gpu.Default.AllocateReadWriteTexture2D<Vector4>(frame.Width, frame.Height);
            using ReadOnlyBuffer<float> kernelBuffer = Gpu.Default.AllocateReadOnlyBuffer(Kernel);

            ApplyVerticalConvolution(sourceTexture, firstPassTexture, kernelBuffer);
            ApplyHorizontalConvolution(firstPassTexture, sourceTexture, kernelBuffer);

            sourceTexture.GetData(span);
        }

        /// <summary>
        /// Performs a vertical 1D complex convolution with the specified parameters.
        /// </summary>
        /// <param name="source">The source <see cref="ReadWriteTexture2D{T, TPixel}"/> to read data from.</param>
        /// <param name="target">The target <see cref="ReadWriteTexture2D{T}"/> to write the results to.</param>
        /// <param name="kernel">The <see cref="ReadOnlyBuffer{T}"/> with the values for the current complex kernel.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ApplyVerticalConvolution(
            ReadWriteTexture2D<Rgba32, Vector4> source,
            ReadWriteTexture2D<Vector4> target,
            ReadOnlyBuffer<float> kernel)
        {
            int height = Source.Height;
            int width = Source.Width;

            HlslGaussianBlurProcessor.VerticalConvolutionProcessor shader = new(
                maxY: height - 1,
                maxX: width - 1,
                kernel.Length,
                source,
                target,
                kernel);

            Gpu.Default.For(width, height, in shader);
        }

        /// <summary>
        /// Performs an horizontal 1D complex convolution with the specified parameters.
        /// </summary>
        /// <param name="source">The source <see cref="ReadWriteTexture2D{T}"/> to read data from.</param>
        /// <param name="target">The target <see cref="ReadWriteTexture2D{T, TPixel}"/> to write the results to.</param>
        /// <param name="kernel">The <see cref="ReadOnlyBuffer{T}"/> with the values for the current complex kernel.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ApplyHorizontalConvolution(
            ReadWriteTexture2D<Vector4> source,
            ReadWriteTexture2D<Rgba32, Vector4> target,
            ReadOnlyBuffer<float> kernel)
        {
            int height = Source.Height;
            int width = Source.Width;

            HlslGaussianBlurProcessor.HorizontalConvolutionProcessor shader = new(
                maxY: height - 1,
                maxX: width - 1,
                kernel.Length,
                source,
                target,
                kernel);

            Gpu.Default.For(width, height, in shader);
        }
    }
}
