#pragma warning disable IDE0022

namespace ComputeSharp.D2D1;

/// <summary>
/// A <see langword="struct"/> representing a <see cref="Float4"/> readonly 2D texture stored on GPU memory.
/// This can be used from D2D1 pixel shaders to be able to pass arbitrary inputs to them.
/// </summary>
/// <typeparam name="T">The type of items stored on the texture (only <see langword="float"/> and <see cref="Float4"/> are supported).</typeparam>
[AutoConstructorBehavior(AutoConstructorBehavior.IgnoreAndSetToDefault)]
public readonly struct D2D1ResourceTexture2D<T>
    where T : unmanaged
{
    /// <summary>
    /// Gets the width of the current texture.
    /// </summary>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public int Width => throw new InvalidExecutionContextException($"{typeof(D2D1ResourceTexture2D<T>)}.{nameof(Width)}");

    /// <summary>
    /// Gets the height of the current texture.
    /// </summary>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public int Height => throw new InvalidExecutionContextException($"{typeof(D2D1ResourceTexture2D<T>)}.{nameof(Height)}");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture.
    /// </summary>
    /// <param name="x">The horizontal offset of the value to get.</param>
    /// <param name="y">The vertical offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly T this[int x, int y] => throw new InvalidExecutionContextException($"{typeof(D2D1ResourceTexture2D<T>)}[{typeof(int)}, {typeof(int)}]");

    /// <summary>
    /// Gets a single <see cref="Float4"/> value from the current readonly texture.
    /// </summary>
    /// <param name="xy">The coordinates of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly T this[Int2 xy] => throw new InvalidExecutionContextException($"{typeof(D2D1ResourceTexture2D<T>)}[{typeof(Int2)}]");

    /// <summary>
    /// Retrieves a single <see cref="Float4"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="u">The horizontal normalized offset of the value to get.</param>
    /// <param name="v">The vertical normalized offset of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly T Sample(float u, float v) => throw new InvalidExecutionContextException($"{typeof(D2D1ResourceTexture2D<T>)}.{nameof(Sample)}({typeof(float)}, {typeof(float)})");

    /// <summary>
    /// Retrieves a single <see cref="Float4"/> value from the current readonly texture with linear sampling.
    /// </summary>
    /// <param name="uv">The normalized coordinates of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly T Sample(Float2 uv) => throw new InvalidExecutionContextException($"{typeof(D2D1ResourceTexture2D<T>)}.{nameof(Sample)}({typeof(Float2)})");
}