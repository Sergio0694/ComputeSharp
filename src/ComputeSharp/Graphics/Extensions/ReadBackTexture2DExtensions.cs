using System;
using System.Buffers;
using System.IO;
using CommunityToolkit.Diagnostics;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Resources;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="ReadBackTexture2D{T}"/> type.
/// </summary>
public static class ReadBackTexture2DExtensions
{
    /// <summary>
    /// Reads the contents of a <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="ReadBackTexture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="ReadBackTexture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    public static void CopyFrom<T>(this ReadBackTexture2D<T> destination, Texture2D<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, 0, 0, 0, 0, source.Width, source.Height);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="ReadBackTexture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="ReadBackTexture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyFrom<T>(this ReadBackTexture2D<T> destination, Texture2D<T> source, int sourceOffsetX, int sourceOffsetY, int width, int height)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, 0, 0, width, height);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture2D{T}"/> instance and writes them into a target <see cref="ReadBackTexture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="ReadBackTexture2D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture2D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyFrom<T>(this ReadBackTexture2D<T> destination, Texture2D<T> source, int sourceOffsetX, int sourceOffsetY, int destinationOffsetX, int destinationOffsetY, int width, int height)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, destinationOffsetX, destinationOffsetY, width, height);
    }

    /// <summary>
    /// Saves a texture to a specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="texture">The texture to save to an image.</param>
    /// <param name="filename">The filename of the image file to save.</param>
    public static void Save<T>(this ReadBackTexture2D<T> texture, string filename)
        where T : unmanaged
    {
        Guard.IsNotNull(texture);
        Guard.IsNotNull(filename);

        WICHelper.Instance.SaveTexture(texture.View, filename.AsSpan());
    }

    /// <summary>
    /// Saves a texture to a specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="texture">The texture to save to an image.</param>
    /// <param name="filename">The filename of the image file to save.</param>
    public static void Save<T>(this ReadBackTexture2D<T> texture, ReadOnlySpan<char> filename)
        where T : unmanaged
    {
        Guard.IsNotNull(texture);

        WICHelper.Instance.SaveTexture(texture.View, filename);
    }

    /// <summary>
    /// Saves a texture to a specified stream.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="texture">The texture to save to an image.</param>
    /// <param name="stream">The stream to save the image to.</param>
    /// <param name="format">The target image format to use.</param>
    public static void Save<T>(this ReadBackTexture2D<T> texture, Stream stream, ImageFormat format)
        where T : unmanaged
    {
        Guard.IsNotNull(texture);
        Guard.IsNotNull(stream);

        WICHelper.Instance.SaveTexture(texture.View, stream, format);
    }

    /// <summary>
    /// Saves a texture to a specified buffer writer.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="texture">The texture to save to an image.</param>
    /// <param name="writer">The buffer writer to save the image to.</param>
    /// <param name="format">The target image format to use.</param>
    public static void Save<T>(this ReadBackTexture2D<T> texture, IBufferWriter<byte> writer, ImageFormat format)
        where T : unmanaged
    {
        Guard.IsNotNull(texture);
        Guard.IsNotNull(writer);

        WICHelper.Instance.SaveTexture(texture.View, writer, format);
    }
}