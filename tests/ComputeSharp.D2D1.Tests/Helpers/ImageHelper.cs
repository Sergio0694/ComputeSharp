using System;
using System.Buffers;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputeSharp.D2D1.Tests.Helpers;

/// <summary>
/// A <see langword="class"/> with helpers to load and save images.
/// </summary>
internal static class ImageHelper
{
    /// <summary>
    /// Loads a <see cref="ReadOnlyMemory{T}"/> instance from a given path.
    /// </summary>
    /// <param name="filename">The path to load the image from.</param>
    /// <param name="width">The resulting image width.</param>
    /// <param name="height">The resulting image height.</param>
    /// <returns>The resulting <see cref="ReadOnlyMemory{T}"/> instance.</returns>
    public static unsafe ReadOnlyMemory<byte> LoadBitmapFromFile(string filename, out uint width, out uint height)
    {
        using Image<Bgra32> image = Image.Load<Bgra32>(filename);

        width = (uint)image.Width;
        height = (uint)image.Height;

        if (!image.DangerousTryGetSinglePixelMemory(out Memory<Bgra32> memory))
        {
            Assert.Inconclusive();
        }

        return MemoryMarshal.AsBytes(memory.Span).ToArray();
    }

    /// <summary>
    /// Saves a given image buffer to a target path.
    /// </summary>
    /// <param name="filename">The path to save the image to.</param>
    /// <param name="width">The image width.</param>
    /// <param name="height">The image height.</param>
    /// <param name="strideInBytes">The image stride in bytes.</param>
    /// <param name="buffer">The image pixel buffer.</param>
    public static unsafe void SaveBitmapToFile(string filename, uint width, uint height, uint strideInBytes, byte* buffer)
    {
        byte[] pixelData = ArrayPool<byte>.Shared.Rent((int)width * (int)height * sizeof(Bgra32));

        for (int i = 0; i < (int)height; i++)
        {
            ReadOnlySpan<byte> source = new(buffer + (i * (int)strideInBytes), (int)width * sizeof(Bgra32));

            source.CopyTo(pixelData.AsSpan(i * (int)width * sizeof(Bgra32)));
        }

        using (Image<Bgra32> image = Image.WrapMemory<Bgra32>(pixelData, (int)width, (int)height))
        {
            image.Save(filename);
        }

        ArrayPool<byte>.Shared.Return(pixelData);
    }
}