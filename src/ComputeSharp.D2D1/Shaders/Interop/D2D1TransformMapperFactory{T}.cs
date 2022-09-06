using System.Numerics;
using ComputeSharp.D2D1.Shaders.Interop.Factories;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// Provides built-in transform mapper factories for common transform mapping operations.
/// </summary>
/// <typeparam name="T">The type of D2D1 pixel shader associated to the transform mapper.</typeparam>
public static class D2D1TransformMapperFactory<T>
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// Creates an <see cref="ID2D1TransformMapperFactory{T}"/> instance for an inflate transform.
    /// </summary>
    /// <param name="amount">The fixed inflate amount to use (applies to all edges of the input area).</param>
    /// <returns>The resulting <see cref="ID2D1TransformMapperFactory{T}"/> instance for an inflate transform.</returns>
    public static ID2D1TransformMapperFactory<T> Inflate(int amount)
    {
        return new D2D1InflateTransformMapperFactory<T> { ParametersAccessor = new D2D1InflateTransformMapperFactory<T>.ConstantAmount { Amount = amount } };
    }

    /// <summary>
    /// Creates an <see cref="ID2D1TransformMapperFactory{T}"/> instance for an inflate transform.
    /// </summary>
    /// <param name="left">The fixed left inflate amount to use.</param>
    /// <param name="top">The fixed top inflate amount to use.</param>
    /// <param name="right">The fixed right inflate amount to use.</param>
    /// <param name="bottom">The fixed bottom inflate amount to use.</param>
    /// <returns>The resulting <see cref="ID2D1TransformMapperFactory{T}"/> instance for an inflate transform.</returns>
    public static ID2D1TransformMapperFactory<T> Inflate(int left, int top, int right, int bottom)
    {
        return new D2D1InflateTransformMapperFactory<T> { ParametersAccessor = new D2D1InflateTransformMapperFactory<T>.ConstantLeftTopRightBottomAmount { Left = left, Top = top, Right = right, Bottom = bottom } };
    }

    /// <summary>
    /// Creates an <see cref="ID2D1TransformMapperFactory{T}"/> instance for an inflate transform.
    /// </summary>
    /// <param name="accessor">The input <see cref="Accessor{TResult}"/> instance to retrieve the inflate amount to use (applies to all edges of the input area).</param>
    /// <returns>The resulting <see cref="ID2D1TransformMapperFactory{T}"/> instance for an inflate transform.</returns>
    public static ID2D1TransformMapperFactory<T> Inflate(Accessor<int> accessor)
    {
        return new D2D1InflateTransformMapperFactory<T> { ParametersAccessor = new D2D1InflateTransformMapperFactory<T>.DynamicAmount { Accessor = accessor } };
    }

    /// <summary>
    /// Creates an <see cref="ID2D1TransformMapperFactory{T}"/> instance for an inflate transform.
    /// </summary>
    /// <param name="accessor">The input <see cref="Accessor{TResult}"/> instance to retrieve the inflate amount to use.</param>
    /// <returns>The resulting <see cref="ID2D1TransformMapperFactory{T}"/> instance for an inflate transform.</returns>
    public static ID2D1TransformMapperFactory<T> Inflate(Accessor<(int Left, int Top, int Right, int Bottom)> accessor)
    {
        return new D2D1InflateTransformMapperFactory<T> { ParametersAccessor = new D2D1InflateTransformMapperFactory<T>.DynamicLeftTopRightBottomAmount { Accessor = accessor } };
    }

    /// <summary>
    /// Creates an <see cref="ID2D1TransformMapperFactory{T}"/> instance for an affine transform.
    /// </summary>
    /// <param name="matrix">The fixed transformation matrix to use.</param>
    /// <returns>The resulting <see cref="ID2D1TransformMapperFactory{T}"/> instance for an affine transform.</returns>
    public static ID2D1TransformMapperFactory<T> Transform(Matrix3x2 matrix)
    {
        return new D2D1AffineTransformMapperFactory<T> { ParametersAccessor = new D2D1AffineTransformMapperFactory<T>.ConstantMatrix { Amount = matrix } };
    }

    /// <summary>
    /// Creates an <see cref="ID2D1TransformMapperFactory{T}"/> instance for an affine transform.
    /// </summary>
    /// <param name="accessor">The input <see cref="Accessor{TResult}"/> instance to retrieve the transformation matrix to use.</param>
    /// <returns>The resulting <see cref="ID2D1TransformMapperFactory{T}"/> instance for an affine transform.</returns>
    public static ID2D1TransformMapperFactory<T> Transform(Accessor<Matrix3x2> accessor)
    {
        return new D2D1AffineTransformMapperFactory<T> { ParametersAccessor = new D2D1AffineTransformMapperFactory<T>.DynamicMatrix { Accessor = accessor } };
    }

    /// <summary>
    /// An accessor of parameters to be used in D2D1 transform mapping implementations.
    /// </summary>
    /// <typeparam name="TResult">The type of parameters that the transform mapper will use.</typeparam>
    /// <param name="shader">The input D2D1 pixel shader being executed on the current effect (this can be used to retrieve shader properties).</param>
    /// <returns>The resulting parameters to be used by the transform mapper.</returns>
    public delegate TResult Accessor<TResult>(in T shader);    
}
