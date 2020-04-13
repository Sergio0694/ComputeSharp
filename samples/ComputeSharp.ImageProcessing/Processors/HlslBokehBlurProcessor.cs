using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace ComputeSharp.BokehBlur.Processors
{
    /// <summary>
    /// Applies bokeh blur processing to the image
    /// </summary>
    public sealed class HlslBokehBlurProcessor : IImageProcessor
    {
        /// <summary>
        /// The default radius used by the parameterless constructor
        /// </summary>
        public const int DefaultRadius = 32;

        /// <summary>
        /// The default component count used by the parameterless constructor
        /// </summary>
        public const int DefaultComponents = 2;

        /// <summary>
        /// The default gamma used by the parameterless constructor
        /// </summary>
        public const float DefaultGamma = 3f;

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslBokehBlurProcessor"/> class
        /// </summary>
        public HlslBokehBlurProcessor() : this(DefaultRadius, DefaultComponents, DefaultGamma) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslBokehBlurProcessor"/> class
        /// </summary>
        /// <param name="radius">The size of the area to sample</param>
        /// <param name="components">The number of components to use to approximate the original 2D bokeh blur convolution kernel</param>
        /// <param name="gamma">The gamma highlight factor to use to further process the image</param>
        public HlslBokehBlurProcessor(int radius, int components, float gamma)
        {
            Radius = radius;
            Components = components;
            Gamma = gamma;
        }

        /// <summary>
        /// Gets the radius
        /// </summary>
        public int Radius { get; }

        /// <summary>
        /// Gets the number of components
        /// </summary>
        public int Components { get; }

        /// <summary>
        /// Gets the gamma highlight factor to use when applying the effect
        /// </summary>
        public float Gamma { get; }

        /// <inheritdoc/>
        public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
            where TPixel : unmanaged, IPixel<TPixel>
        {
            return new HlslBokehBlurProcessor<TPixel>(this, configuration, source, sourceRectangle);
        }
    }
}
