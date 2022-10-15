using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Shaders.Interop.Factories.Abstract;
using ComputeSharp.D2D1.Shaders.Interop.Helpers;

namespace ComputeSharp.D2D1.Shaders.Interop.Factories;

/// <summary>
/// A custom <see cref="D2D1TransformMapperFactory{T, TParameters, TTransformMapper}"/> implementation for an inflate transform.
/// </summary>
/// <typeparam name="T">The type of D2D1 pixel shader associated to the transform mapper.</typeparam>
internal sealed class D2D1InflateTransformMapperFactory<T> : D2D1TransformMapperFactory<T, (int, int, int, int), D2D1InflateTransformMapperFactory<T>.TransformMapper>
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// A <see cref="D2D1TransformMapperParametersAccessor{T, TParameters}"/> implementation for a constant inflate amount.
    /// </summary>
    public sealed class ConstantAmount : D2D1TransformMapperParametersAccessor<T, (int, int, int, int)>
    {
        /// <summary>
        /// Gets the fixed inflate amount.
        /// </summary>
        public int Amount { get; init; }

        /// <inheritdoc/>
        public override (int, int, int, int) Get(in T shader)
        {
            return (Amount, Amount, Amount, Amount);
        }
    }

    /// <summary>
    /// A <see cref="D2D1TransformMapperParametersAccessor{T, TParameters}"/> implementation for a constant inflate LTRB amount.
    /// </summary>
    public sealed class ConstantLeftTopRightBottomAmount : D2D1TransformMapperParametersAccessor<T, (int, int, int, int)>
    {
        /// <summary>
        /// The constant left inflate amount.
        /// </summary>
        public int Left { get; init; }

        /// <summary>
        /// The constant top inflate amount.
        /// </summary>
        public int Top { get; init; }

        /// <summary>
        /// The constant right inflate amount.
        /// </summary>
        public int Right { get; init; }

        /// <summary>
        /// The constant bottom inflate amount.
        /// </summary>
        public int Bottom { get; init; }

        /// <inheritdoc/>
        public override (int, int, int, int) Get(in T shader)
        {
            return (Left, Top, Right, Bottom);
        }
    }

    /// <summary>
    /// A <see cref="D2D1TransformMapperParametersAccessor{T, TParameters}"/> implementation for a dynamic inflate amount.
    /// </summary>
    public sealed class DynamicAmount : D2D1TransformMapperParametersAccessor<T, (int, int, int, int)>
    {
        /// <summary>
        /// Gets the <see cref="D2D1TransformMapperFactory{T}.Accessor{TResult}"/> instance to get the dynamic inflate amount.
        /// </summary>
        public required D2D1TransformMapperFactory<T>.Accessor<int> Accessor { get; init; }

        /// <inheritdoc/>
        public override (int, int, int, int) Get(in T shader)
        {
            int amount = Accessor!(in shader);

            return (amount, amount, amount, amount);
        }
    }

    /// <summary>
    /// A <see cref="D2D1TransformMapperParametersAccessor{T, TParameters}"/> implementation for a dynamic inflate LTRB amount.
    /// </summary>
    public sealed class DynamicLeftTopRightBottomAmount : D2D1TransformMapperParametersAccessor<T, (int, int, int, int)>
    {
        /// <summary>
        /// Gets the <see cref="D2D1TransformMapperFactory{T}.Accessor{TResult}"/> instance to get the dynamic inflate LTRB amount.
        /// </summary>
        public required D2D1TransformMapperFactory<T>.Accessor<(int, int, int, int)> Accessor { get; init; }

        /// <inheritdoc/>
        public override (int, int, int, int) Get(in T shader)
        {
            return Accessor!(in shader);
        }
    }

    /// <summary>
    /// A custom <see cref="D2D1TransformMapper{T, TParameters}"/> implementation for an inflate transform.
    /// </summary>
    public sealed class TransformMapper : D2D1TransformMapper<T, (int Left, int Top, int Right, int Bottom)>
    {
        /// <inheritdoc/>
        protected override void TransformInputToOutput(in (int Left, int Top, int Right, int Bottom) parameters, ref Rectangle64 rectangle)
        {
            rectangle.Inflate(parameters.Left, parameters.Top, parameters.Right, parameters.Bottom);
        }

        /// <inheritdoc/>
        protected override void TransformOutputToInput(in (int Left, int Top, int Right, int Bottom) parameters, ref Rectangle64 rectangle)
        {
            rectangle.Inflate(parameters.Left, parameters.Top, parameters.Right, parameters.Bottom);
        }
    }
}