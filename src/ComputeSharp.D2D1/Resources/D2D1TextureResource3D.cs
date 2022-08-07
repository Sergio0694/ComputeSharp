using ComputeSharp.Exceptions;

namespace ComputeSharp.D2D1;

/// <summary>
/// A <see langword="struct"/> representing a <see cref="Float4"/> readonly 3D texture stored on GPU memory.
/// This can be used from D2D1 pixel shaders to be able to pass arbitrary inputs to them.
/// </summary>
[AutoConstructorBehavior(AutoConstructorBehavior.IgnoreAndSetToDefault)]
public readonly struct D2D1TextureResource3D
{
    /// <summary>
    /// Gets the width of the current texture.
    /// </summary>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public int Width => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource3D)}.{nameof(Width)}");

    /// <summary>
    /// Gets the height of the current texture.
    /// </summary>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public int Height => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource3D)}.{nameof(Height)}");

    /// <summary>
    /// Gets the depth of the current texture.
    /// </summary>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public int Depth => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource3D)}.{nameof(Depth)}");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture.
    /// </summary>
    /// <param name="x">The horizontal offset of the value to get.</param>
    /// <param name="y">The vertical offset of the value to get.</param>
    /// <param name="z">The depthwise offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly Float4 this[int x, int y, int z] => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource3D)}[{typeof(int)}, {typeof(int)}, {typeof(int)}]");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture.
    /// </summary>
    /// <param name="xyz">The coordinates of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly Float4 this[Int3 xyz] => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource3D)}[{typeof(Int3)}]");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="u">The horizontal normalized offset of the value to get.</param>
    /// <param name="v">The vertical normalized offset of the value to get.</param>
    /// <param name="w">The depthwise offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly Float4 this[float u, float v, float w] => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource3D)}[{typeof(float)}, {typeof(float)}, {typeof(float)}]");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="uvw">The normalized coordinates of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly Float4 this[Float3 uvw] => throw new InvalidExecutionContextException($"{typeof(D2D1TextureResource3D)}[{typeof(Float3)}]");
}
