namespace ComputeSharp
{
    /// <summary>
    /// An interface representing a typed writeable 2D texture containing normalized pixel data stored on GPU memory.
    /// This interface can only be used to wrap <see cref="ReadWriteTexture3D{T, TPixel}"/> instances.
    /// </summary>
    /// <typeparam name="TPixel">The type of normalized pixels used on the GPU side.</typeparam>
    public interface IReadWriteTexture3D<TPixel>
        where TPixel : unmanaged
    {
        /// <summary>
        /// Gets the width of the current texture.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the current texture.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the depth of the current texture.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// Gets a single <typeparamref name="TPixel"/> value from the current writeable texture.
        /// </summary>
        /// <param name="x">The horizontal offset of the value to get.</param>
        /// <param name="y">The vertical offset of the value to get.</param>
        /// <param name="z">The depthwise offset of the value to get.</param>
        /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
        ref TPixel this[int x, int y, int z] { get; }

        /// <summary>
        /// Gets a single <typeparamref name="TPixel"/> value from the current writeable texture.
        /// </summary>
        /// <param name="xyz">The coordinates of the value to get.</param>
        /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
        ref TPixel this[Int3 xyz] { get; }
    }
}
