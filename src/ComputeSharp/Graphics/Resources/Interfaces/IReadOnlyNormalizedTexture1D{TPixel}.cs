namespace ComputeSharp;

/// <summary>
/// An interface representing a typed readonly 1D texture containing normalized pixel data stored on GPU memory.
/// This interface can only be used to wrap <see cref="ReadOnlyTexture1D{T, TPixel}"/> instances.
/// </summary>
/// <typeparam name="TPixel">The type of normalized pixels used on the GPU side.</typeparam>
public interface IReadOnlyNormalizedTexture1D<TPixel> : IGraphicsResource
    where TPixel : unmanaged
{
    /// <summary>
    /// Gets the width of the current texture.
    /// </summary>
    int Width { get; }

    /// <summary>
    /// Gets a single <typeparamref name="TPixel"/> value from the current readonly texture.
    /// </summary>
    /// <param name="x">The horizontal offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    ref readonly TPixel this[int x] { get; }

    /// <summary>
    /// Retrieves a single <typeparamref name="TPixel"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="u">The horizontal normalized offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    ref readonly TPixel Sample(float u);
}
