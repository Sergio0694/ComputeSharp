using System.Runtime.CompilerServices;
using CommunityToolkit.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

#pragma warning disable IDE0290

namespace ComputeSharp.BokehBlur.Processors;

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
    public HlslGaussianBlurProcessor()
        : this(GraphicsDevice.GetDefault(), DefaultSigma, 9)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class.
    /// </summary>
    /// <param name="radius">The radius value representing the size of the area to sample.</param>
    public HlslGaussianBlurProcessor(int radius)
        : this(GraphicsDevice.GetDefault(), radius / 3F, radius)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="radius">The radius value representing the size of the area to sample.</param>
    public HlslGaussianBlurProcessor(GraphicsDevice device, int radius)
        : this(device, radius / 3F, radius)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HlslGaussianBlurProcessor"/> class.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="sigma"> The sigma value representing the weight of the blur. </param>
    /// <param name="radius">The radius value representing the size of the area to sample (this should be at least twice the sigma value)</param>
    public HlslGaussianBlurProcessor(GraphicsDevice device, float sigma, int radius)
    {
        this.graphicsDevice = device;

        Sigma = sigma;
        Radius = radius;
    }

    /// <summary>
    /// The <see cref="GraphicsDevice"/> instance in use.
    /// </summary>
    private readonly GraphicsDevice graphicsDevice;

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
        if (typeof(TPixel) != typeof(ImageSharpRgba32))
        {
            ThrowHelper.ThrowInvalidOperationException("This processor only supports the RGBA32 pixel format.");
        }

        Implementation processor = new(this, configuration, Unsafe.As<Image<ImageSharpRgba32>>(source), sourceRectangle);

        return Unsafe.As<IImageProcessor<TPixel>>(processor);
    }
}