using ComputeSharp.Exceptions;

namespace ComputeSharp.D2D1;

/// <summary>
/// A <see langword="struct"/> representing a <see cref="Float4"/> readonly 1D texture stored on GPU memory.
/// This can be used from D2D1 pixel shaders to be able to pass arbitrary inputs to them.
/// </summary>
/// <typeparam name="T">The type of items stored on the texture (only <see langword="float"/> and <see cref="Float4"/> are supported).</typeparam>
[AutoConstructorBehavior(AutoConstructorBehavior.IgnoreAndSetToDefault)]
public readonly struct D2D1ResourceTexture1D<T>
    where T : unmanaged
{
    /// <summary>
    /// Gets the width of the current texture.
    /// </summary>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public int Width => throw new InvalidExecutionContextException($"{typeof(D2D1ResourceTexture1D<T>)}.{nameof(Width)}");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture.
    /// </summary>
    /// <param name="x">The horizontal offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly T this[int x] => throw new InvalidExecutionContextException($"{typeof(D2D1ResourceTexture1D<T>)}[{typeof(int)}]");

    /// <summary>
    /// Retrieves a single <see cref="Float4"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="u">The horizontal normalized offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly T Sample(float u) => throw new InvalidExecutionContextException($"{typeof(D2D1ResourceTexture1D<T>)}.{nameof(Sample)}({typeof(float)})");
}