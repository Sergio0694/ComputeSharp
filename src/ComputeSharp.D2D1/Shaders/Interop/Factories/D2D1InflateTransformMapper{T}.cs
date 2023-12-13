using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Shaders.Interop.Factories.Abstract;
using ComputeSharp.D2D1.Shaders.Interop.Helpers;

namespace ComputeSharp.D2D1.Shaders.Interop.Factories;

/// <summary>
/// A custom <see cref="D2D1DrawTransformMapper{T, TParameters}"/> implementation for an inflate transform.
/// </summary>
/// <typeparam name="T">The type of D2D1 pixel shader associated to the transform mapper.</typeparam>
internal abstract class D2D1InflateTransformMapper<T> : D2D1DrawTransformMapper<T, (int Left, int Top, int Right, int Bottom)>
    where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
{
    /// <inheritdoc/>
    protected sealed override void TransformInputToOutput(in (int Left, int Top, int Right, int Bottom) parameters, ref Rectangle64 rectangle)
    {
        rectangle.Inflate(parameters.Left, parameters.Top, parameters.Right, parameters.Bottom);
    }

    /// <inheritdoc/>
    protected sealed override void TransformOutputToInput(in (int Left, int Top, int Right, int Bottom) parameters, ref Rectangle64 rectangle)
    {
        rectangle.Inflate(parameters.Left, parameters.Top, parameters.Right, parameters.Bottom);
    }

    /// <summary>
    /// A <see cref="D2D1InflateTransformMapper{T}"/> implementation for a constant inflate amount.
    /// </summary>
    public sealed class ConstantAmount : D2D1InflateTransformMapper<T>
    {
        /// <summary>
        /// Gets the fixed inflate amount.
        /// </summary>
        public required int Amount { get; init; }

        /// <inheritdoc/>
        protected override (int Left, int Top, int Right, int Bottom) GetParameters(D2D1DrawInfoUpdateContext<T> drawInfoUpdateContext)
        {
            return (Amount, Amount, Amount, Amount);
        }
    }

    /// <summary>
    /// A <see cref="D2D1DrawTransformMapper{T, TParameters}"/> implementation for a constant inflate LTRB amount.
    /// </summary>
    public sealed class ConstantLeftTopRightBottomAmount : D2D1InflateTransformMapper<T>
    {
        /// <summary>
        /// The constant left inflate amount.
        /// </summary>
        public required int Left { get; init; }

        /// <summary>
        /// The constant top inflate amount.
        /// </summary>
        public required int Top { get; init; }

        /// <summary>
        /// The constant right inflate amount.
        /// </summary>
        public required int Right { get; init; }

        /// <summary>
        /// The constant bottom inflate amount.
        /// </summary>
        public required int Bottom { get; init; }

        /// <inheritdoc/>
        protected override (int Left, int Top, int Right, int Bottom) GetParameters(D2D1DrawInfoUpdateContext<T> drawInfoUpdateContext)
        {
            return (Left, Top, Right, Bottom);
        }
    }

    /// <summary>
    /// A <see cref="D2D1DrawTransformMapper{T, TParameters}"/> implementation for a dynamic inflate amount.
    /// </summary>
    public sealed class DynamicAmount : D2D1InflateTransformMapper<T>
    {
        /// <summary>
        /// Gets the <see cref="D2D1DrawTransformMapper{T}.Accessor{TResult}"/> instance to get the dynamic inflate amount.
        /// </summary>
        public required Accessor<int> Accessor { get; init; }

        /// <inheritdoc/>
        protected override (int Left, int Top, int Right, int Bottom) GetParameters(D2D1DrawInfoUpdateContext<T> drawInfoUpdateContext)
        {
            int amount = Accessor(drawInfoUpdateContext.GetConstantBuffer());

            return (amount, amount, amount, amount);
        }
    }

    /// <summary>
    /// A <see cref="D2D1DrawTransformMapper{T, TParameters}"/> implementation for a dynamic inflate LTRB amount.
    /// </summary>
    public sealed class DynamicLeftTopRightBottomAmount : D2D1InflateTransformMapper<T>
    {
        /// <summary>
        /// Gets the <see cref="D2D1DrawTransformMapper{T}.Accessor{TResult}"/> instance to get the dynamic inflate LTRB amount.
        /// </summary>
        public required Accessor<(int, int, int, int)> Accessor { get; init; }

        /// <inheritdoc/>
        protected override (int Left, int Top, int Right, int Bottom) GetParameters(D2D1DrawInfoUpdateContext<T> drawInfoUpdateContext)
        {
            return Accessor(drawInfoUpdateContext.GetConstantBuffer());
        }
    }
}