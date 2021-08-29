using System;
using ComputeSharp.Graphics.Helpers;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="ReadBackTexture2D{T}"/> type.
/// </summary>
public static class ReadBackTexture2DExtensions
{
    /// <summary>
    /// Saves a texture to a specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="texture">The texture to save to an image.</param>
    /// <param name="filename">The filename of the image file to save.</param>
    public static void Save<T>(this ReadBackTexture2D<T> texture, ReadOnlySpan<char> filename)
        where T : unmanaged
    {
        WICHelper.Instance.SaveTexture(texture.View, filename);
    }
}
