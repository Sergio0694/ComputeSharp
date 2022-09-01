namespace ComputeSharp;

/// <summary>
/// An interface representing a typed readonly 1D texture containing raw data stored on GPU memory.
/// This interface can only be used to wrap <see cref="ReadOnlyTexture1D{T}"/> instances.
/// </summary>
/// <typeparam name="T">The type of raw data used on the GPU side.</typeparam>
public interface IReadOnlyTexture1D<T> : IGraphicsResource
    where T : unmanaged
{
    /// <summary>
    /// Gets the width of the current texture.
    /// </summary>
    int Width { get; }

    /// <summary>
    /// Gets a single <typeparamref name="T"/> value from the current readonly texture.
    /// </summary>
    /// <param name="x">The horizontal offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    ref readonly T this[int x] { get; }

    /// <summary>
    /// Retrieves a single <typeparamref name="T"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="u">The horizontal normalized offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    ref readonly T Sample(float u);
}
