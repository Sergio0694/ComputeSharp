using CommunityToolkit.Diagnostics;
using ComputeSharp.Resources;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="ReadBackTexture1D{T}"/> type.
/// </summary>
public static class ReadBackTexture1DExtensions
{
    /// <summary>
    /// Reads the contents of a <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="ReadBackTexture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="ReadBackTexture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    public static void CopyFrom<T>(this ReadBackTexture1D<T> destination, Texture1D<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, 0, 0, source.Width);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="ReadBackTexture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="ReadBackTexture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyFrom<T>(this ReadBackTexture1D<T> destination, Texture1D<T> source, int sourceOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, sourceOffsetX, 0, width);
    }

    /// <summary>
    /// Reads the contents of a <see cref="Texture1D{T}"/> instance and writes them into a target <see cref="ReadBackTexture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="destination">The target <see cref="ReadBackTexture1D{T}"/> instance to write data to.</param>
    /// <param name="source">The input <see cref="Texture1D{T}"/> instance to read data from.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyFrom<T>(this ReadBackTexture1D<T> destination, Texture1D<T> source, int sourceOffsetX, int destinationOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(destination);
        Guard.IsNotNull(source);

        source.CopyTo(destination, sourceOffsetX, destinationOffsetX, width);
    }
}
