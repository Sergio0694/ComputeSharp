using ComputeSharp.Exceptions;

namespace ComputeSharp.D2D1;

/// <summary>
/// A <see langword="struct"/> representing a <see cref="Float4"/> readonly 1D texture stored on GPU memory.
/// This can be used from D2D1 pixel shaders to be able to pass arbitrary inputs to them.
/// </summary>
[AutoConstructorBehavior(AutoConstructorBehavior.IgnoreAndSetToDefault)]
public readonly struct D2D1TextureResource1D
{
    /// <summary>
    /// Gets the width of the current texture.
    /// </summary>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public int Width => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource1D)}.{nameof(Width)}");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture.
    /// </summary>
    /// <param name="x">The horizontal offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly Float4 this[int x] => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource2D)}[{typeof(int)}]");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="u">The horizontal normalized offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly Float4 this[float u] => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource2D)}[{typeof(float)}]");
}
