using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.Tests.Extensions
{
    /// <summary>
    /// A helper class for loading images into resources.
    /// </summary>
    public static class ImagingExtensions
    {
        /// <summary>
        /// Allocates a new <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the specified texture data.
        /// </summary>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
        /// <param name="path">The path of the texture to load.</param>
        /// <returns>A texture with the data from the image at the specified path.</returns>
        [Pure]
        public static ReadOnlyTexture2D<Rgba32, Float4> LoadTexture(this GraphicsDevice device, string path)
        {
            using Image<ImageSharpRgba32> image = Image.Load<ImageSharpRgba32>(path);

            Assert.IsTrue(image.TryGetSinglePixelSpan(out Span<ImageSharpRgba32> span));

            Span<Rgba32> pixels = MemoryMarshal.Cast<ImageSharpRgba32, Rgba32>(span);

            return device.AllocateReadOnlyTexture2D<Rgba32, Float4>(pixels, image.Width, image.Height);
        }

        /// <summary>
        /// Creates a new <see cref="Image{TPixel}"/> instance with the specified texture data.
        /// </summary>
        /// <param name="texture">The source <see cref="ReadWriteTexture2D{T, TPixel}"/> instance to read data from.</param>
        /// <returns>An image with the data from the input texture.</returns>
        [Pure]
        public static Image<ImageSharpRgba32> ToImage(this ReadWriteTexture2D<Rgba32, Float4> texture)
        {
            Image<ImageSharpRgba32> image = new(texture.Width, texture.Height);

            Assert.IsTrue(image.TryGetSinglePixelSpan(out Span<ImageSharpRgba32> span));

            Span<Rgba32> pixels = MemoryMarshal.Cast<ImageSharpRgba32, Rgba32>(span);

            texture.CopyTo(pixels);

            return image;
        }
    }
}
