using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.Toolkit.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.BokehBlur.Processors
{
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
            : this(DefaultRadius, DefaultComponents)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HlslBokehBlurProcessor"/> class.
        /// </summary>
        /// <param name="radius">The size of the area to sample.</param>
        /// <param name="components">The number of components to use to approximate the original 2D bokeh blur convolution kernel.</param>
        public HlslBokehBlurProcessor(int radius, int components)
        {
            Radius = radius;
            Components = components;
        }

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
                ThrowHelper.ThrowInvalidOperationException("This processor only supports the RGBA32 pixel format");
            }

            var processor = new Implementation(this, configuration, Unsafe.As<Image<ImageSharpRgba32>>(source), sourceRectangle);

            return Unsafe.As<IImageProcessor<TPixel>>(processor);
        }

        /// <summary>
        /// Kernel for the vertical convolution pass.
        /// </summary>
        [AutoConstructor]
        internal partial struct VerticalConvolutionProcessor : IComputeShader
        {
            public int width;
            public int maxY;
            public int maxX;
            public int kernelLength;

            public ReadOnlyBuffer<Vector4> source;
            public ReadWriteBuffer<ComplexVector4> target;
            public ReadOnlyBuffer<Complex64> kernel;

            /// <inheritdoc/>
            public void Execute(ThreadIds ids)
            {
                Vector4 real = Vector4.Zero;
                Vector4 imaginary = Vector4.Zero;
                int radiusY = kernelLength >> 1;
                int sourceOffsetColumnBase = ids.X;

                for (int i = 0; i < kernelLength; i++)
                {
                    int offsetY = Hlsl.Clamp(ids.Y + i - radiusY, 0, maxY);
                    int offsetX = Hlsl.Clamp(sourceOffsetColumnBase, 0, maxX);
                    Vector4 color = source[offsetY * width + offsetX];
                    Complex64 factors = kernel[i];

                    real += factors.Real * color;
                    imaginary += factors.Imaginary * color;
                }

                int offsetXY = ids.Y * width + ids.X;

                target[offsetXY].Real = real;
                target[offsetXY].Imaginary = imaginary;
            }
        }

        /// <summary>
        /// Kernel for the horizontal convolution pass.
        /// </summary>
        [AutoConstructor]
        internal partial struct HorizontalConvolutionAndAccumulatePartialsProcessor : IComputeShader
        {
            public int width;
            public int maxY;
            public int maxX;
            public int kernelLength;
            public float z;
            public float w;

            public ReadWriteBuffer<ComplexVector4> source;
            public ReadWriteBuffer<Vector4> target;
            public ReadOnlyBuffer<Complex64> kernel;

            /// <inheritdoc/>
            public void Execute(ThreadIds ids)
            {
                Vector4 real = Vector4.Zero;
                Vector4 imaginary = Vector4.Zero;
                int radiusX = kernelLength >> 1;
                int sourceOffsetColumnBase = ids.X;
                int offsetY = Hlsl.Clamp(ids.Y, 0, maxY);

                for (int i = 0; i < kernelLength; i++)
                {
                    int offsetX = Hlsl.Clamp(sourceOffsetColumnBase + i - radiusX, 0, maxX);
                    var offsetXY = offsetY * width + offsetX;
                    ComplexVector4 source4 = source[offsetXY];
                    Complex64 factors = kernel[i];

                    real += factors.Real * source4.Real - factors.Imaginary * source4.Imaginary;
                    imaginary += factors.Real * source4.Imaginary + factors.Imaginary * source4.Real;
                }

                target[ids.Y * width + ids.X] += real * z + imaginary * w;
            }
        }
    }
}
