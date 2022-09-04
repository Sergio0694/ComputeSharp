namespace ComputeSharp.D2D1.Shaders.Interop.Factories.Abstract;

/// <summary>
/// A base type for an accessor of parameters to be used in D2D1 transform mapping implementations.
/// </summary>
/// <typeparam name="T">The type of D2D1 pixel shader associated to the transform mapper.</typeparam>
/// <typeparam name="TParameters">The type of parameters that the transform mapper will use.</typeparam>
internal abstract class D2D1TransformMapperParametersAccessor<T, TParameters>
{
    /// <summary>
    /// Gets the parameters to be used by the transform mapper.
    /// </summary>
    /// <param name="shader">The input D2D1 pixel shader being executed on the current effect (this can be used to retrieve shader properties).</param>
    /// <returns>The resulting parameters to be used by the transform mapper.</returns>
    public abstract TParameters Get(in T shader);
}