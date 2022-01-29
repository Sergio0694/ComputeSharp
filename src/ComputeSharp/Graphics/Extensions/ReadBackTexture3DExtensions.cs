using CommunityToolkit.Diagnostics;
using ComputeSharp.Resources;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="ReadBackTexture3D{T}"/> type.
/// </summary>
public static class ReadBackTexture3DExtensions
{
    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="ReadBackTexture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="ReadBackTexture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    public static void CopyFrom<T>(this ReadBackTexture3D<T> destination, Texture3D<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, 0, 0, 0, 0, 0, 0, source.Width, source.Height, source.Depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="ReadBackTexture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="ReadBackTexture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyFrom<T>(this ReadBackTexture3D<T> destination, Texture3D<T> source, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, sourceOffsetZ, 0, 0, 0, width, height, depth);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture3D{T}"/> instance and writes them into a target <see cref="ReadBackTexture3D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="ReadBackTexture3D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture3D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="sourceOffsetY">The vertical offset in the source texture.</param>
    /// <param name="sourceOffsetZ">The depthwise offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="destinationOffsetY">The vertical offset in the destination texture.</param>
    /// <param name="destinationOffsetZ">The depthwise offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    /// <param name="height">The height of the memory area to copy.</param>
    /// <param name="depth">The depth of the memory area to copy.</param>
    public static void CopyFrom<T>(this ReadBackTexture3D<T> destination, Texture3D<T> source, int sourceOffsetX, int sourceOffsetY, int sourceOffsetZ, int destinationOffsetX, int destinationOffsetY, int destinationOffsetZ, int width, int height, int depth)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, sourceOffsetZ, destinationOffsetX, destinationOffsetY, destinationOffsetZ, width, height, depth);
    }
}
