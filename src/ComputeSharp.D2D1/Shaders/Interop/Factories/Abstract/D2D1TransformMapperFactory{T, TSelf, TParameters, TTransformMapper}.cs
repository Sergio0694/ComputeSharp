namespace ComputeSharp.D2D1.Shaders.Interop.Factories.Abstract;

/// <summary>
/// A base type for a transform mapper factory to be used in a D2D1 pixel shader effect.
/// </summary>
/// <typeparam name="T">The type of D2D1 pixel shader associated to the transform mapper.</typeparam>
/// <typeparam name="TSelf">The type of concrete transform mapper factory inheriting from this base type.</typeparam>
/// <typeparam name="TParameters">The type of parameters that the transform mapper will use.</typeparam>
/// <typeparam name="TTransformMapper">The type of concrete transform mapper to produce from this factory.</typeparam>
internal abstract class D2D1TransformMapperFactory<T, TSelf, TParameters, TTransformMapper> : ID2D1TransformMapperFactory<T>
    where T : unmanaged, ID2D1PixelShader
    where TSelf : D2D1TransformMapperFactory<T, TSelf, TParameters, TTransformMapper>, new()
    where TTransformMapper : D2D1TransformMapper<T, TParameters>, new()
{
    /// <summary>
    /// Creates a new <see cref="ID2D1TransformMapperFactory{T}"/> instance with the specified accessor.
    /// </summary>
    /// <param name="accessor">The input <see cref="D2D1TransformMapperParametersAccessor{T, TParameters}"/> instance to be used to retrieve parameters.</param>
    /// <returns>The new <see cref="ID2D1TransformMapperFactory{T}"/> instance with the specified accessor.</returns>
    public ID2D1TransformMapperFactory<T> Create(D2D1TransformMapperParametersAccessor<T, TParameters> accessor)
    {
        return new TSelf { Parameters = accessor };
    }

    /// <inheritdoc/>
    ID2D1TransformMapper<T> ID2D1TransformMapperFactory<T>.Create()
    {
        return new TTransformMapper { Parameters = Parameters };
    }

    /// <summary>
    /// The <see cref="D2D1TransformMapperParametersAccessor{T, TParameters}"/> instance to use to retrieve parameters for the transform mapper.
    /// </summary>
    public D2D1TransformMapperParametersAccessor<T, TParameters>? Parameters { get; init; }
}