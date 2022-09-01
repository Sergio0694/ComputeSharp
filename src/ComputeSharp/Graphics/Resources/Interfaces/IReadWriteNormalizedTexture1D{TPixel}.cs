namespace ComputeSharp;

/// <summary>
/// An interface representing a typed writeable 1D texture containing normalized pixel data stored on GPU memory.
/// This interface can only be used to wrap <see cref="ReadWriteTexture1D{T, TPixel}"/> instances.
/// </summary>
/// <typeparam name="TPixel">The type of normalized pixels used on the GPU side.</typeparam>
public interface IReadWriteNormalizedTexture1D<TPixel> : IGraphicsResource
    where TPixel : unmanaged
{
    /// <summary>
    /// Gets the width of the current texture.
    /// </summary>
    int Width { get; }

    /// <summary>
    /// Gets a single <typeparamref name="TPixel"/> value from the current writeable texture.
    /// </summary>
    /// <param name="x">The horizontal offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    ref TPixel this[int x] { get; }
}
