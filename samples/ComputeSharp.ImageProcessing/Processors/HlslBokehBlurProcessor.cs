using System;
using System.Runtime.CompilerServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

#pragma warning disable IDE0290

namespace ComputeSharp.BokehBlur.Processors;

/// <summary>
/// Applies bokeh blur processing to the image.
/// </summary>
public sealed partial class HlslBokehBlurProcessor : IImageProcessor
{
    /// <summary>
    /// The default radius used by the parameterless constructor.
    /// </summary>
    public const int DefaultRadius = 32;

    /// <summary>
    /// The default component count used by the parameterless constructor.
    /// </summary>
    public const int DefaultComponents = 2;

    /// <summary>
    /// Initializes a new instance of the <see cref="HlslBokehBlurProcessor"/> class.
    /// </summary>
    public HlslBokehBlurProcessor()
        : this(GraphicsDevice.GetDefault(), DefaultRadius, DefaultComponents)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HlslBokehBlurProcessor"/> class.
    /// </summary>
    /// <param name="radius">The size of the area to sample.</param>
    /// <param name="components">The number of components to use to approximate the original 2D bokeh blur convolution kernel.</param>
    public HlslBokehBlurProcessor(int radius, int components)
        : this(GraphicsDevice.GetDefault(), radius, components)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HlslBokehBlurProcessor"/> class.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="radius">The size of the area to sample.</param>
    /// <param name="components">The number of components to use to approximate the original 2D bokeh blur convolution kernel.</param>
    public HlslBokehBlurProcessor(GraphicsDevice device, int radius, int components)
    {
        GraphicsDevice = device;
        Radius = radius;
        Components = components;
    }

    /// <summary>
    /// Gets the <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.
    /// </summary>
    public GraphicsDevice GraphicsDevice { get; }

    /// <summary>
    /// Gets the radius.
    /// </summary>
    public int Radius { get; }

    /// <summary>
    /// Gets the number of components.
    /// </summary>
    public int Components { get; }

    /// <inheritdoc/>
    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (typeof(TPixel) != typeof(ImageSharpRgba32))
        {
            throw new NotSupportedException("This processor only supports the RGBA32 pixel format.");
        }

        Implementation processor = new(this, configuration, Unsafe.As<Image<ImageSharpRgba32>>(source), sourceRectangle);

        return Unsafe.As<IImageProcessor<TPixel>>(processor);
    }
}