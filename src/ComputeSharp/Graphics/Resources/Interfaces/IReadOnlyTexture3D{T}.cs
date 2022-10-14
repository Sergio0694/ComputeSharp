namespace ComputeSharp;

/// <summary>
/// An interface representing a typed readonly 3D texture containing raw data stored on GPU memory.
/// This interface can only be used to wrap <see cref="ReadOnlyTexture3D{T}"/> instances.
/// </summary>
/// <typeparam name="T">The type of raw data used on the GPU side.</typeparam>
public interface IReadOnlyTexture3D<T> : IGraphicsResource
    where T : unmanaged
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
    /// Gets a single <typeparamref name="T"/> value from the current readonly texture.
    /// </summary>
    /// <param name="x">The horizontal offset of the value to get.</param>
    /// <param name="y">The vertical offset of the value to get.</param>
    /// <param name="z">The depthwise offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    ref readonly T this[int x, int y, int z] { get; }

    /// <summary>
    /// Gets a single <typeparamref name="T"/> value from the current readonly texture.
    /// </summary>
    /// <param name="xyz">The coordinates of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    ref readonly T this[Int3 xyz] { get; }

    /// <summary>
    /// Retrieves a single <typeparamref name="T"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="u">The horizontal normalized offset of the value to get.</param>
    /// <param name="v">The vertical normalized offset of the value to get.</param>
    /// <param name="w">The depthwise normalized offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    ref readonly T Sample(float u, float v, float w);

    /// <summary>
    /// Retrieves a single <typeparamref name="T"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="uvw">The normalized coordinates of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    ref readonly T Sample(Float3 uvw);
}