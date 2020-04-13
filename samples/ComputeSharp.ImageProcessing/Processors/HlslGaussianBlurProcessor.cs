using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace ComputeSharp.BokehBlur.Processors
{
    /// <summary>
    /// Defines Gaussian blur by a (Sigma, Radius) pair
    /// </summary>
    public sealed class HlslGaussianBlurProcessor : IImageProcessor
    {
        /// <summary>
        /// The default value for <see cref="Sigma"/>.
        /// </summary>
        public const float DefaultSigma = 3f;

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class
        /// </summary>
        public HlslGaussianBlurProcessor() : this(DefaultSigma, 9) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class
        /// </summary>
        /// <param name="radius">The radius value representing the size of the area to sample</param>
        public HlslGaussianBlurProcessor(int radius) : this(radius / 3F, radius) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class
        /// </summary>
        /// <param name="sigma"> The sigma value representing the weight of the blur </param>
        /// <param name="radius">The radius value representing the size of the area to sample (this should be at least twice the sigma value)</param>
        public HlslGaussianBlurProcessor(float sigma, int radius)
        {
            Sigma = sigma;
            Radius = radius;
        }

        /// <summary>
        /// Gets the sigma value representing the weight of the blur
        /// </summary>
        public float Sigma { get; }

        /// <summary>
        /// Gets the radius defining the size of the area to sample
        /// </summary>
        public int Radius { get; }

        /// <inheritdoc />
        public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
            where TPixel : unmanaged, IPixel<TPixel>
        {
            return new HlslGaussianBlurProcessor<TPixel>(this, configuration, source, sourceRectangle);
        }
    }
}
