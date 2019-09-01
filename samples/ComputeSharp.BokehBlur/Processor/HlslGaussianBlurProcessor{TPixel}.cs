using System;
using System.Runtime.CompilerServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using SixLabors.ImageSharp.Processing.Processors.Convolution;
using SixLabors.Primitives;

namespace ComputeSharp.BokehBlur.Processor
{
    /// <summary>
    /// Applies Gaussian blur processing to an image.
    /// </summary>
    /// <typeparam name="TPixel">The pixel format.</typeparam>
    internal class HlslGaussianBlurProcessor<TPixel> : IImageProcessor<TPixel> where TPixel : struct, IPixel<TPixel>
    {
        /// <summary>
        /// The 1D kernel to apply
        /// </summary>
        private readonly float[] Kernel;

        /// <summary>
        /// The source <see cref="Image{TPixel}"/> instance to modify
        /// </summary>
        private readonly Image<TPixel> Source;

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class
        /// </summary>
        /// <param name="definition">The <see cref="HlslGaussianBlurProcessor"/> defining the processor parameters</param>
        /// <param name="source">The source <see cref="Image{TPixel}"/> for the current processor instance</param>
        /// <param name="sourceRectangle">The source area to process for the current processor instance</param>
        public HlslGaussianBlurProcessor(HlslGaussianBlurProcessor definition, Image<TPixel> source, Rectangle sourceRectangle)
        {
            int kernelSize = definition.Radius * 2 + 1;
            Kernel = CreateGaussianBlurKernel(kernelSize, definition.Sigma);
            Source = source;
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
        public void Apply()
        {

        }

        /// <inheritdoc/>
        public void Dispose() { }
    }
}
