using System;
using System.Numerics;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Shaders.Interop.Factories.Abstract;
using ComputeSharp.D2D1.Shaders.Interop.Helpers;

namespace ComputeSharp.D2D1.Shaders.Interop.Factories;

/// <summary>
/// A custom <see cref="D2D1TransformMapperFactory{T, TSelf, TParameters, TTransformMapper}"/> implementation for an affine transform.
/// </summary>
/// <typeparam name="T">The type of D2D1 pixel shader associated to the transform mapper.</typeparam>
internal sealed class D2D1AffineTransformMapperFactory<T> : D2D1TransformMapperFactory<T, D2D1AffineTransformMapperFactory<T>, Matrix3x2, D2D1AffineTransformMapperFactory<T>.TransformMapper>
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// A <see cref="D2D1TransformMapperParametersAccessor{T, TParameters}"/> implementation for a fixed affine transform matrix.
    /// </summary>
    public sealed class FixedMatrix : D2D1TransformMapperParametersAccessor<T, Matrix3x2>
    {
        /// <summary>
        /// Gets the fixed affine transform matrix.
        /// </summary>
        public Matrix3x2 Amount { get; init; }

        /// <inheritdoc/>
        public override Matrix3x2 Get(in T shader)
        {
            return Amount;
        }
    }

    /// <summary>
    /// A <see cref="D2D1TransformMapperParametersAccessor{T, TParameters}"/> implementation for a dynamic affine transform matrix.
    /// </summary>
    public sealed class DynamicMatrix : D2D1TransformMapperParametersAccessor<T, Matrix3x2>
    {
        /// <summary>
        /// Gets the <see cref="D2D1TransformMapperFactory{T}.Accessor{TResult}"/> instance to get the dynamic affine transform matrix.
        /// </summary>
        public D2D1TransformMapperFactory<T>.Accessor<Matrix3x2>? Accessor { get; init; }

        /// <inheritdoc/>
        public override Matrix3x2 Get(in T shader)
        {
            return Accessor!(in shader);
        }
    }

    /// <summary>
    /// A custom <see cref="D2D1TransformMapper{T, TParameters}"/> implementation for an affine transform.
    /// </summary>
    public sealed class TransformMapper : D2D1TransformMapper<T, Matrix3x2>
    {
        /// <inheritdoc/>
        protected override void TransformInputToOutput(in Matrix3x2 parameters, ref Rectangle64 rectangle)
        {
            Transform(in parameters, ref rectangle);
        }

        /// <inheritdoc/>
        protected override void TransformOutputToInput(in Matrix3x2 parameters, ref Rectangle64 rectangle)
        {
            if (!Matrix3x2.Invert(parameters, out Matrix3x2 matrix))
            {
                static void Throw()
                {
                    throw new ArgumentException("The input transform matrix can't be inverted, so it can't be used for a transform mapper.");
                }

                Throw();
            }

            Transform(in matrix, ref rectangle);
        }

        /// <summary>
        /// Transforms a target rectangle using a given transform matrix.
        /// </summary>
        /// <param name="matrix">The transform matrix to use.</param>
        /// <param name="rectangle">The rectangle to transform.</param>
        private static void Transform(in Matrix3x2 matrix, ref Rectangle64 rectangle)
        {
            // Get the rectangle corners
            Point64 topLeft = new(rectangle.Left, rectangle.Top);
            Point64 topRight = new(rectangle.Right, rectangle.Top);
            Point64 bottomLeft = new(rectangle.Left, rectangle.Bottom);
            Point64 bottomRight = new(rectangle.Right, rectangle.Bottom);

            // Transform them with the current matrix
            var transformedTopLeft = topLeft.Transform(in matrix);
            var transformedTopRight = topRight.Transform(in matrix);
            var transformedBottomLeft = bottomLeft.Transform(in matrix);
            var transformedBottomRight = bottomRight.Transform(in matrix);

            // Calculate the bounding box of the transformed points
            double left = Math.Min(Math.Min(transformedTopLeft.X, transformedTopRight.X), Math.Min(transformedBottomLeft.X, transformedBottomRight.X));
            double top = Math.Min(Math.Min(transformedTopLeft.Y, transformedTopRight.Y), Math.Min(transformedBottomLeft.Y, transformedBottomRight.Y));
            double right = Math.Max(Math.Max(transformedTopLeft.X, transformedTopRight.X), Math.Max(transformedBottomLeft.X, transformedBottomRight.X));
            double bottom = Math.Max(Math.Max(transformedTopLeft.Y, transformedTopRight.Y), Math.Max(transformedBottomLeft.Y, transformedBottomRight.Y));

            // Round each coordinate as needed and compute the rectangle bounds
            long x = (long)Math.Floor(left);
            long y = (long)Math.Floor(top);
            long width = (long)Math.Ceiling(right) - x;
            long height = (long)Math.Ceiling(bottom) - y;            

            rectangle = new(x, y, width, height);
        }
    }
}