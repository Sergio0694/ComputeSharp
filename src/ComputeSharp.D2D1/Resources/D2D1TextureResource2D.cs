using ComputeSharp.Exceptions;

namespace ComputeSharp.D2D1;

/// <summary>
/// A <see langword="struct"/> representing a <see cref="Float4"/> readonly 2D texture stored on GPU memory.
/// This can be used from D2D1 pixel shaders to be able to pass arbitrary inputs to them.
/// </summary>
[AutoConstructorBehavior(AutoConstructorBehavior.IgnoreAndSetToDefault)]
public readonly struct D2D1TextureResource2D
{
    /// <summary>
    /// Gets the width of the current texture.
    /// </summary>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public int Width => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource2D)}.{nameof(Width)}");

    /// <summary>
    /// Gets the height of the current texture.
    /// </summary>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public int Height => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource2D)}.{nameof(Height)}");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture.
    /// </summary>
    /// <param name="x">The horizontal offset of the value to get.</param>
    /// <param name="y">The vertical offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly Float4 this[int x, int y] => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource2D)}[{typeof(int)}, {typeof(int)}]");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture.
    /// </summary>
    /// <param name="xy">The coordinates of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly Float4 this[Int2 xy] => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource2D)}[{typeof(Int2)}]");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="u">The horizontal normalized offset of the value to get.</param>
    /// <param name="v">The vertical normalized offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly Float4 this[float u, float v] => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource2D)}[{typeof(float)}, {typeof(float)}]");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="uv">The normalized coordinates of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly Float4 this[Float2 uv] => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource2D)}[{typeof(Float2)}]");
}
