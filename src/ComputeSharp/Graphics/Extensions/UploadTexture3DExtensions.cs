using System;
using ComputeSharp.Resources;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="UploadTexture3D{T}"/> type.
/// </summary>
public static class UploadTexture3DExtensions
{
    /// <summary>
    /// Reads the contents of a <see cref="UploadTexture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="UploadTexture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    public static void CopyTo<T>(this UploadTexture3D<T> source, Texture3D<T> destination)
        where T : unmanaged
    {
        default(ArgumentNullException).ThrowIfNull(source);
        default(ArgumentNullException).ThrowIfNull(destination);

        destination.CopyFrom(source, 0, 0, 0, 0, 0, 0, source.Width, source.Height, source.Depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="UploadTexture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="UploadTexture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyTo<T>(this UploadTexture3D<T> source, Texture3D<T> destination, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        default(ArgumentNullException).ThrowIfNull(source);
        default(ArgumentNullException).ThrowIfNull(destination);

        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, sourceOffsetZ, 0, 0, 0, width, height, depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="UploadTexture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="UploadTexture3D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="destinationOffsetZ">The depthwise offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyTo<T>(this UploadTexture3D<T> source, Texture3D<T> destination, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int destinationOffsetX, int destinationOffsetY, int destinationOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        default(ArgumentNullException).ThrowIfNull(source);
        default(ArgumentNullException).ThrowIfNull(destination);

        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, sourceOffsetZ, destinationOffsetX, destinationOffsetY, destinationOffsetZ, width, height, depth);
    }
}