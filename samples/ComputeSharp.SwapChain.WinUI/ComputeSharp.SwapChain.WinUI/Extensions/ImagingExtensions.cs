using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.SwapChain.WinUI.Extensions
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

            _ = image.TryGetSinglePixelSpan(out Span<ImageSharpRgba32> span);

            Span<Rgba32> pixels = MemoryMarshal.Cast<ImageSharpRgba32, Rgba32>(span);

            return device.AllocateReadOnlyTexture2D<Rgba32, Float4>(pixels, image.Width, image.Height);
        }
    }
}
