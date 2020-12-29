using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace ComputeSharp.BokehBlur.Processors
{
    /// <summary>
    /// Defines Gaussian blur by a (Sigma, Radius) pair.
    /// </summary>
    public sealed partial class HlslGaussianBlurProcessor : IImageProcessor
    {
        /// <summary>
        /// The default value for <see cref="Sigma"/>.
        /// </summary>
        public const float DefaultSigma = 3f;

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class.
        /// </summary>
        public HlslGaussianBlurProcessor() : this(DefaultSigma, 9) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class.
        /// </summary>
        /// <param name="radius">The radius value representing the size of the area to sample.</param>
        public HlslGaussianBlurProcessor(int radius) : this(radius / 3F, radius) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class.
        /// </summary>
        /// <param name="sigma"> The sigma value representing the weight of the blur. </param>
        /// <param name="radius">The radius value representing the size of the area to sample (this should be at least twice the sigma value)</param>
        public HlslGaussianBlurProcessor(float sigma, int radius)
        {
            Sigma = sigma;
            Radius = radius;
        }

        /// <summary>
        /// Gets the sigma value representing the weight of the blur.
        /// </summary>
        public float Sigma { get; }

        /// <summary>
        /// Gets the radius defining the size of the area to sample.
        /// </summary>
        public int Radius { get; }

        /// <inheritdoc />
        public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
            where TPixel : unmanaged, IPixel<TPixel>
        {
            return new HlslGaussianBlurProcessor<TPixel>(this, configuration, source, sourceRectangle);
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
