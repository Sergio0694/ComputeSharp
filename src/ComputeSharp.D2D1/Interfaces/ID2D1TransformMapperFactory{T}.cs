namespace ComputeSharp.D2D1;

/// <summary>
/// An <see langword="interface"/> acting as a factory for <see cref="ID2D1TransformMapper{T}"/> instances.
/// </summary>
/// <typeparam name="T">The type of shader this factory creates transform mapper instances for.</typeparam>
public interface ID2D1TransformMapperFactory<T>
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// Creates a new <see cref="ID2D1TransformMapper{T}"/> instance for a given shader.
    /// </summary>
    /// <returns>A new <see cref="ID2D1TransformMapper{T}"/> instance for the input shader.</returns>
    ID2D1TransformMapper<T> Create();
}