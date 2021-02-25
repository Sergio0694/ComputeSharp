using ComputeSharp.Resources;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that contains extension methods for the <see cref="UploadTexture3D{T}"/> type.
    /// </summary>
    public static class UploadTexture3DExtensions
    {
        /// <summary>
        /// Reads the contents of a <see cref="UploadTexture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="UploadTexture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        public static void CopyTo<T>(this UploadTexture3D<T> texture, Texture3D<T> destination)
            where T : unmanaged
        {
            destination.CopyFrom(texture, 0, 0, 0, 0, 0, 0, texture.Width, texture.Height, texture.Depth);
        }

        /// <summary>
        /// Reads the contents of a <see cref="UploadTexture3D{T}"/> instance and writes them into a target <see cref="Texture3D{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items stored on the texture.</typeparam>
        /// <param name="texture">The input <see cref="UploadTexture3D{T}"/> instance to read data from.</param>
        /// <param name="destination">The target <see cref="Texture3D{T}"/> instance to write data to.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="z">The depthwise offset in the source texture.</param>
        /// <param name="width">The width of the memory area to copy.</param>
        /// <param name="height">The height of the memory area to copy.</param>
        /// <param name="depth">The depth of the memory area to copy.</param>
        public static void CopyTo<T>(this UploadTexture3D<T> texture, Texture3D<T> destination, int x, int y, int z, int width, int height, int depth)
            where T : unmanaged
        {
            destination.CopyFrom(texture, x, y, z, 0, 0, 0, width, height, depth);
        }
    }
}
