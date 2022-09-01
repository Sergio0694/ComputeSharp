using CommunityToolkit.Diagnostics;
using ComputeSharp.Resources;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="UploadTexture1D{T}"/> type.
/// </summary>
public static class UploadTexture1DExtensions
{
    /// <summary>
    /// Reads the contents of a <see cref="UploadTexture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="UploadTexture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    public static void CopyTo<T>(this UploadTexture1D<T> source, Texture1D<T> destination)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        destination.CopyFrom(source, 0, 0, source.Width);
    }

    /// <summary>
    /// Reads the contents of a <see cref="UploadTexture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="UploadTexture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyTo<T>(this UploadTexture1D<T> source, Texture1D<T> destination, int sourceOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        destination.CopyFrom(source, sourceOffsetX, 0, width);
    }

    /// <summary>
    /// Reads the contents of a <see cref="UploadTexture1D{T}"/> instance and writes them into a target <see cref="Texture1D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="source">The input <see cref="UploadTexture1D{T}"/> instance to read data from.</param>
    /// <param name="destination">The target <see cref="Texture1D{T}"/> instance to write data to.</param>
    /// <param name="sourceOffsetX">The horizontal offset in the source texture.</param>
    /// <param name="destinationOffsetX">The horizontal offset in the destination texture.</param>
    /// <param name="width">The width of the memory area to copy.</param>
    public static void CopyTo<T>(this UploadTexture1D<T> source, Texture1D<T> destination, int sourceOffsetX, int destinationOffsetX, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(source);
        Guard.IsNotNull(destination);

        destination.CopyFrom(source, sourceOffsetX, destinationOffsetX, width);
    }
}
