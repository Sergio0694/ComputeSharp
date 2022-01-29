using System;
using System.IO;
using CommunityToolkit.Diagnostics;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Resources;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="UploadTexture2D{T}"/> type.
/// </summary>
public static class UploadTexture2DExtensions
{
    /// <summary>
    /// Reads the contents of a <see cref="UploadTexture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="UploadTexture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    public static void CopyTo<T>(this UploadTexture2D<T> source, Texture2D<T> destination)
        where T : unmanaged
    {
        Guard.IsNotNull(source, nameof(source));
        Guard.IsNotNull(destination, nameof(destination));

        destination.CopyFrom(source, 0, 0, 0, 0, source.Width, source.Height);
    }

    /// <summary>
    /// Reads the contents of a <see cref="UploadTexture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="UploadTexture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyTo<T>(this UploadTexture2D<T> source, Texture2D<T> destination, int sourceOffsetX, int sourceOffsetY, int width, int height)
        where T : unmanaged
    {
        Guard.IsNotNull(source, nameof(source));
        Guard.IsNotNull(destination, nameof(destination));

        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, 0, 0, width, height);
    }

    /// <summary>
    /// Reads the contents of a <see cref="UploadTexture2D{T}"/> instance and writes them into a target <see cref="Texture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="UploadTexture2D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture2D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    public static void CopyTo<T>(this UploadTexture2D<T> source, Texture2D<T> destination, int sourceOffsetX, int sourceOffsetY, int destinationOffsetX, int destinationOffsetY, int width, int height)
        where T : unmanaged
    {
        Guard.IsNotNull(source, nameof(source));
        Guard.IsNotNull(destination, nameof(destination));

        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, destinationOffsetX, destinationOffsetY, width, height);
    }

    /// <summary>
    /// Loads a texture from a specified file.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The texture to decode the image into.</param>
    /// <param name="filename">The filename of the image file to load.</param>
    public static void Load<T>(this UploadTexture2D<T> texture, string filename)
        where T : unmanaged
    {
        Guard.IsNotNull(texture, nameof(texture));
        Guard.IsNotNull(filename, nameof(filename));

        texture. Load(filename.AsSpan());
    }

    /// <summary>
    /// Loads a texture from a specified file.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The texture to decode the image into.</param>
    /// <param name="filename">The filename of the image file to load.</param>
    public static void Load<T>(this UploadTexture2D<T> texture, ReadOnlySpan<char> filename)
        where T : unmanaged
    {
        Guard.IsNotNull(texture, nameof(texture));

        WICHelper.Instance.LoadTexture(texture.View, filename);
    }

    /// <summary>
    /// LLoads a texture from a specified buffer.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The texture to decode the image into.</param>
    /// <param name="span">The buffer with the image data to load and decode into the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the contents of the specified file.</returns>
    public static void Load<T>(this UploadTexture2D<T> texture, ReadOnlySpan<byte> span)
        where T : unmanaged
    {
        Guard.IsNotNull(texture, nameof(texture));

        WICHelper.Instance.LoadTexture(texture.View, span);
    }

    /// <summary>
    /// Loads a texture from a specified stream.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The texture to decode the image into.</param>
    /// <param name="stream">The stream to load the image from.</param>
    public static void Load<T>(this UploadTexture2D<T> texture, Stream stream)
        where T : unmanaged
    {
        Guard.IsNotNull(texture, nameof(texture));
        Guard.IsNotNull(stream, nameof(stream));

        WICHelper.Instance.LoadTexture(texture.View, stream);
    }
}
