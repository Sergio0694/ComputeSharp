using System;
using System.Numerics;
using ComputeSharp.D2D1.Shaders.Interop.Factories;

namespace ComputeSharp.D2D1.Interop;

/// <inheritdoc cref="D2D1DrawTransformMapper{T}"/>
partial class D2D1DrawTransformMapper<T>
{
    /// <summary>
    /// Creates an <see cref="D2D1DrawTransformMapper{T}"/> instance for an inflate transform.
    /// </summary>
    /// <param name="amount">The fixed inflate amount to use (applies to all edges of the input area).</param>
    /// <returns>The resulting <see cref="D2D1DrawTransformMapper{T}"/> instance for an inflate transform.</returns>
    public static D2D1DrawTransformMapper<T> Inflate(int amount)
    {
        return new D2D1InflateTransformMapper<T>.ConstantAmount { Amount = amount };
    }

    /// <summary>
    /// Creates an <see cref="D2D1DrawTransformMapper{T}"/> instance for an inflate transform.
    /// </summary>
    /// <param name="left">The fixed left inflate amount to use.</param>
    /// <param name="top">The fixed top inflate amount to use.</param>
    /// <param name="right">The fixed right inflate amount to use.</param>
    /// <param name="bottom">The fixed bottom inflate amount to use.</param>
    /// <returns>The resulting <see cref="D2D1DrawTransformMapper{T}"/> instance for an inflate transform.</returns>
    public static D2D1DrawTransformMapper<T> Inflate(int left, int top, int right, int bottom)
    {
        return new D2D1InflateTransformMapper<T>.ConstantLeftTopRightBottomAmount { Left = left, Top = top, Right = right, Bottom = bottom };
    }

    /// <summary>
    /// Creates an <see cref="D2D1DrawTransformMapper{T}"/> instance for an inflate transform.
    /// </summary>
    /// <param name="accessor">The input <see cref="Accessor{TResult}"/> instance to retrieve the inflate amount to use (applies to all edges of the input area).</param>
    /// <returns>The resulting <see cref="D2D1DrawTransformMapper{T}"/> instance for an inflate transform.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="accessor"/> is <see langword="null"/>.</exception>
    public static D2D1DrawTransformMapper<T> Inflate(Accessor<int> accessor)
    {
        default(ArgumentNullException).ThrowIfNull(accessor);

        return new D2D1InflateTransformMapper<T>.DynamicAmount { Accessor = accessor };
    }

    /// <summary>
    /// Creates an <see cref="D2D1DrawTransformMapper{T}"/> instance for an inflate transform.
    /// </summary>
    /// <param name="accessor">The input <see cref="Accessor{TResult}"/> instance to retrieve the inflate amount to use.</param>
    /// <returns>The resulting <see cref="D2D1DrawTransformMapper{T}"/> instance for an inflate transform.</returns>
    public static D2D1DrawTransformMapper<T> Inflate(Accessor<(int Left, int Top, int Right, int Bottom)> accessor)
    {
        default(ArgumentNullException).ThrowIfNull(accessor);

        return new D2D1InflateTransformMapper<T>.DynamicLeftTopRightBottomAmount { Accessor = accessor };
    }

    /// <summary>
    /// Creates an <see cref="D2D1DrawTransformMapper{T}"/> instance for an affine transform.
    /// </summary>
    /// <param name="matrix">The fixed transformation matrix to use.</param>
    /// <returns>The resulting <see cref="D2D1DrawTransformMapper{T}"/> instance for an affine transform.</returns>
    public static D2D1DrawTransformMapper<T> Transform(Matrix3x2 matrix)
    {
        return new D2D1AffineTransformMapper<T>.ConstantMatrix { Amount = matrix };
    }

    /// <summary>
    /// Creates an <see cref="D2D1DrawTransformMapper{T}"/> instance for an affine transform.
    /// </summary>
    /// <param name="accessor">The input <see cref="Accessor{TResult}"/> instance to retrieve the transformation matrix to use.</param>
    /// <returns>The resulting <see cref="D2D1DrawTransformMapper{T}"/> instance for an affine transform.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="accessor"/> is <see langword="null"/>.</exception>
    public static D2D1DrawTransformMapper<T> Transform(Accessor<Matrix3x2> accessor)
    {
        default(ArgumentNullException).ThrowIfNull(accessor);

        return new D2D1AffineTransformMapper<T>.DynamicMatrix { Accessor = accessor };
    }

    /// <summary>
    /// An accessor of parameters to be used in D2D1 transform mapping implementations.
    /// </summary>
    /// <typeparam name="TResult">The type of parameters that the transform mapper will use.</typeparam>
    /// <param name="shader">The input D2D1 pixel shader being executed on the current effect (this can be used to retrieve shader properties).</param>
    /// <returns>The resulting parameters to be used by the transform mapper.</returns>
    public delegate TResult Accessor<TResult>(in T shader);
}