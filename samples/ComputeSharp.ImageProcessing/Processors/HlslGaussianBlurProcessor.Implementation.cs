using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CommunityToolkit.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing.Processors;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.BokehBlur.Processors;

/// <inheritdoc/>
public sealed partial class HlslGaussianBlurProcessor
{
    /// <summary>
    /// Applies Gaussian blur processing to an image.
    /// </summary>
    internal sealed partial class Implementation : ImageProcessor<ImageSharpRgba32>
    {
        /// <summary>
        /// The <see cref="GraphicsDevice"/> instance in use.
        /// </summary>
        private readonly GraphicsDevice graphicsDevice;

        /// <summary>
        /// The 1D kernel to apply.
        /// </summary>
        private readonly float[] kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Implementation"/> class.
        /// </summary>
        /// <param name="definition">The <see cref="Implementation"/> defining the processor parameters.</param>
        /// <param name="configuration">The configuration which allows altering default behaviour or extending the library.</param>
        /// <param name="source">The source <see cref="Image{TPixel}"/> for the current processor instance.</param>
        /// <param name="sourceRectangle">The source area to process for the current processor instance.</param>
        public Implementation(HlslGaussianBlurProcessor definition, Configuration configuration, Image<ImageSharpRgba32> source, Rectangle sourceRectangle)
            : base(configuration, source, sourceRectangle)
        {
            int kernelSize = (definition.Radius * 2) + 1;

            this.graphicsDevice = definition.graphicsDevice;
            this.kernel = CreateGaussianBlurKernel(kernelSize, definition.Sigma);
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
            float denominator = (float)(Math.Sqrt(2 * Math.PI) * sigma);

            float exponentNumerator = -x * x;
            float exponentDenominator = 2 * sigma * sigma;

            float left = Numerator / denominator;
            float right = (float)Math.Exp(exponentNumerator / exponentDenominator);

            return left * right;
        }

        /// <inheritdoc/>
        protected override void OnFrameApply(ImageFrame<ImageSharpRgba32> source)
        {
            if (!source.DangerousTryGetSinglePixelMemory(out Memory<ImageSharpRgba32> pixelMemory))
            {
                ThrowHelper.ThrowInvalidOperationException("Cannot process image frames wrapping discontiguous memory.");
            }

            Span<Rgba32> span = MemoryMarshal.Cast<ImageSharpRgba32, Rgba32>(pixelMemory.Span);

            using ReadWriteTexture2D<Rgba32, float4> sourceTexture = this.graphicsDevice.AllocateReadWriteTexture2D<Rgba32, float4>(span, source.Width, source.Height);
            using ReadWriteTexture2D<Rgba32, float4> firstPassTexture = this.graphicsDevice.AllocateReadWriteTexture2D<Rgba32, float4>(source.Width, source.Height);
            using ReadOnlyBuffer<float> kernelBuffer = this.graphicsDevice.AllocateReadOnlyBuffer(this.kernel);

            using (ComputeContext context = this.graphicsDevice.CreateComputeContext())
            {
                context.For<VerticalConvolutionProcessor>(source.Width, source.Height, new(sourceTexture, firstPassTexture, kernelBuffer));
                context.Barrier(firstPassTexture);
                context.For<HorizontalConvolutionProcessor>(source.Width, source.Height, new(firstPassTexture, sourceTexture, kernelBuffer));
            }

            sourceTexture.CopyTo(span);
        }

        /// <summary>
        /// Kernel for the vertical convolution pass.
        /// </summary>
        [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
        [GeneratedComputeShaderDescriptor]
        internal readonly partial struct VerticalConvolutionProcessor(
            IReadWriteNormalizedTexture2D<float4> source,
            IReadWriteNormalizedTexture2D<float4> target,
            ReadOnlyBuffer<float> kernel) : IComputeShader
        {
            /// <inheritdoc/>
            public void Execute()
            {
                float4 result = float4.Zero;
                int maxY = source.Height - 1;
                int maxX = source.Width - 1;
                int kernelLength = kernel.Length;
                int radiusY = kernelLength >> 1;

                for (int i = 0; i < kernelLength; i++)
                {
                    int offsetY = Hlsl.Clamp(ThreadIds.Y + i - radiusY, 0, maxY);
                    int offsetX = Hlsl.Clamp(ThreadIds.X, 0, maxX);
                    float4 color = source[offsetX, offsetY];

                    result += kernel[i] * color;
                }

                target[ThreadIds.XY] = result;
            }
        }

        /// <summary>
        /// Kernel for the horizontal convolution pass.
        /// </summary>
        [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
        [GeneratedComputeShaderDescriptor]
        internal readonly partial struct HorizontalConvolutionProcessor(
            IReadWriteNormalizedTexture2D<float4> source,
            IReadWriteNormalizedTexture2D<float4> target,
            ReadOnlyBuffer<float> kernel) : IComputeShader
        {
            /// <inheritdoc/>
            public void Execute()
            {
                float4 result = float4.Zero;
                int maxY = source.Height - 1;
                int maxX = source.Width - 1;
                int kernelLength = kernel.Length;
                int radiusX = kernelLength >> 1;
                int offsetY = Hlsl.Clamp(ThreadIds.Y, 0, maxY);

                for (int i = 0; i < kernelLength; i++)
                {
                    int offsetX = Hlsl.Clamp(ThreadIds.X + i - radiusX, 0, maxX);
                    float4 color = source[offsetX, offsetY];

                    result += kernel[i] * color;
                }

                target[ThreadIds.XY] = result;
            }
        }
    }
}