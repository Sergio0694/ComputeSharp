using ComputeSharp.Core.Intrinsics.Attributes;

namespace ComputeSharp;

/// <inheritdoc cref="Hlsl"/>
partial class Hlsl
{
    /// <summary>
    /// Converts the input <see cref="float"/> value into a <see cref="bool"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="float"/> value.</param>
    /// <returns>The converted <see cref="bool"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool")]
    public static bool FloatToBool(float x) => default;

    /// <summary>
    /// Converts the input <see cref="Float2"/> value into a <see cref="Bool2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float2"/> value.</param>
    /// <returns>The converted <see cref="Bool2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2")]
    public static Bool2 FloatToBool(Float2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float3"/> value into a <see cref="Bool3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float3"/> value.</param>
    /// <returns>The converted <see cref="Bool3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3")]
    public static Bool3 FloatToBool(Float3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float4"/> value into a <see cref="Bool4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float4"/> value.</param>
    /// <returns>The converted <see cref="Bool4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4")]
    public static Bool4 FloatToBool(Float4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float1x1"/> value into a <see cref="Bool1x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float1x1"/> value.</param>
    /// <returns>The converted <see cref="Bool1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x1")]
    public static Bool1x1 FloatToBool(Float1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float1x2"/> value into a <see cref="Bool1x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float1x2"/> value.</param>
    /// <returns>The converted <see cref="Bool1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x2")]
    public static Bool1x2 FloatToBool(Float1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float1x3"/> value into a <see cref="Bool1x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float1x3"/> value.</param>
    /// <returns>The converted <see cref="Bool1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x3")]
    public static Bool1x3 FloatToBool(Float1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float1x4"/> value into a <see cref="Bool1x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float1x4"/> value.</param>
    /// <returns>The converted <see cref="Bool1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x4")]
    public static Bool1x4 FloatToBool(Float1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float2x1"/> value into a <see cref="Bool2x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float2x1"/> value.</param>
    /// <returns>The converted <see cref="Bool2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x1")]
    public static Bool2x1 FloatToBool(Float2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float2x2"/> value into a <see cref="Bool2x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float2x2"/> value.</param>
    /// <returns>The converted <see cref="Bool2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x2")]
    public static Bool2x2 FloatToBool(Float2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float2x3"/> value into a <see cref="Bool2x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float2x3"/> value.</param>
    /// <returns>The converted <see cref="Bool2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x3")]
    public static Bool2x3 FloatToBool(Float2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float2x4"/> value into a <see cref="Bool2x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float2x4"/> value.</param>
    /// <returns>The converted <see cref="Bool2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x4")]
    public static Bool2x4 FloatToBool(Float2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float3x1"/> value into a <see cref="Bool3x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float3x1"/> value.</param>
    /// <returns>The converted <see cref="Bool3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x1")]
    public static Bool3x1 FloatToBool(Float3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float3x2"/> value into a <see cref="Bool3x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float3x2"/> value.</param>
    /// <returns>The converted <see cref="Bool3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x2")]
    public static Bool3x2 FloatToBool(Float3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float3x3"/> value into a <see cref="Bool3x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float3x3"/> value.</param>
    /// <returns>The converted <see cref="Bool3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x3")]
    public static Bool3x3 FloatToBool(Float3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float3x4"/> value into a <see cref="Bool3x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float3x4"/> value.</param>
    /// <returns>The converted <see cref="Bool3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x4")]
    public static Bool3x4 FloatToBool(Float3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float4x1"/> value into a <see cref="Bool4x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float4x1"/> value.</param>
    /// <returns>The converted <see cref="Bool4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x1")]
    public static Bool4x1 FloatToBool(Float4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float4x2"/> value into a <see cref="Bool4x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float4x2"/> value.</param>
    /// <returns>The converted <see cref="Bool4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x2")]
    public static Bool4x2 FloatToBool(Float4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float4x3"/> value into a <see cref="Bool4x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float4x3"/> value.</param>
    /// <returns>The converted <see cref="Bool4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x3")]
    public static Bool4x3 FloatToBool(Float4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float4x4"/> value into a <see cref="Bool4x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float4x4"/> value.</param>
    /// <returns>The converted <see cref="Bool4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x4")]
    public static Bool4x4 FloatToBool(Float4x4 x) => default;
    /// <summary>
    /// Converts the input <see cref="double"/> value into a <see cref="bool"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="double"/> value.</param>
    /// <returns>The converted <see cref="bool"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool")]
    public static bool DoubleToBool(double x) => default;

    /// <summary>
    /// Converts the input <see cref="Double2"/> value into a <see cref="Bool2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double2"/> value.</param>
    /// <returns>The converted <see cref="Bool2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2")]
    public static Bool2 DoubleToBool(Double2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double3"/> value into a <see cref="Bool3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double3"/> value.</param>
    /// <returns>The converted <see cref="Bool3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3")]
    public static Bool3 DoubleToBool(Double3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double4"/> value into a <see cref="Bool4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double4"/> value.</param>
    /// <returns>The converted <see cref="Bool4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4")]
    public static Bool4 DoubleToBool(Double4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double1x1"/> value into a <see cref="Bool1x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double1x1"/> value.</param>
    /// <returns>The converted <see cref="Bool1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x1")]
    public static Bool1x1 DoubleToBool(Double1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double1x2"/> value into a <see cref="Bool1x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double1x2"/> value.</param>
    /// <returns>The converted <see cref="Bool1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x2")]
    public static Bool1x2 DoubleToBool(Double1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double1x3"/> value into a <see cref="Bool1x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double1x3"/> value.</param>
    /// <returns>The converted <see cref="Bool1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x3")]
    public static Bool1x3 DoubleToBool(Double1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double1x4"/> value into a <see cref="Bool1x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double1x4"/> value.</param>
    /// <returns>The converted <see cref="Bool1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x4")]
    public static Bool1x4 DoubleToBool(Double1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double2x1"/> value into a <see cref="Bool2x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double2x1"/> value.</param>
    /// <returns>The converted <see cref="Bool2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x1")]
    public static Bool2x1 DoubleToBool(Double2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double2x2"/> value into a <see cref="Bool2x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double2x2"/> value.</param>
    /// <returns>The converted <see cref="Bool2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x2")]
    public static Bool2x2 DoubleToBool(Double2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double2x3"/> value into a <see cref="Bool2x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double2x3"/> value.</param>
    /// <returns>The converted <see cref="Bool2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x3")]
    public static Bool2x3 DoubleToBool(Double2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double2x4"/> value into a <see cref="Bool2x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double2x4"/> value.</param>
    /// <returns>The converted <see cref="Bool2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x4")]
    public static Bool2x4 DoubleToBool(Double2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double3x1"/> value into a <see cref="Bool3x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double3x1"/> value.</param>
    /// <returns>The converted <see cref="Bool3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x1")]
    public static Bool3x1 DoubleToBool(Double3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double3x2"/> value into a <see cref="Bool3x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double3x2"/> value.</param>
    /// <returns>The converted <see cref="Bool3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x2")]
    public static Bool3x2 DoubleToBool(Double3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double3x3"/> value into a <see cref="Bool3x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double3x3"/> value.</param>
    /// <returns>The converted <see cref="Bool3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x3")]
    public static Bool3x3 DoubleToBool(Double3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double3x4"/> value into a <see cref="Bool3x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double3x4"/> value.</param>
    /// <returns>The converted <see cref="Bool3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x4")]
    public static Bool3x4 DoubleToBool(Double3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double4x1"/> value into a <see cref="Bool4x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double4x1"/> value.</param>
    /// <returns>The converted <see cref="Bool4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x1")]
    public static Bool4x1 DoubleToBool(Double4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double4x2"/> value into a <see cref="Bool4x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double4x2"/> value.</param>
    /// <returns>The converted <see cref="Bool4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x2")]
    public static Bool4x2 DoubleToBool(Double4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double4x3"/> value into a <see cref="Bool4x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double4x3"/> value.</param>
    /// <returns>The converted <see cref="Bool4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x3")]
    public static Bool4x3 DoubleToBool(Double4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double4x4"/> value into a <see cref="Bool4x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double4x4"/> value.</param>
    /// <returns>The converted <see cref="Bool4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x4")]
    public static Bool4x4 DoubleToBool(Double4x4 x) => default;
    /// <summary>
    /// Converts the input <see cref="int"/> value into a <see cref="bool"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="int"/> value.</param>
    /// <returns>The converted <see cref="bool"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool")]
    public static bool IntToBool(int x) => default;

    /// <summary>
    /// Converts the input <see cref="Int2"/> value into a <see cref="Bool2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int2"/> value.</param>
    /// <returns>The converted <see cref="Bool2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2")]
    public static Bool2 IntToBool(Int2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int3"/> value into a <see cref="Bool3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int3"/> value.</param>
    /// <returns>The converted <see cref="Bool3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3")]
    public static Bool3 IntToBool(Int3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int4"/> value into a <see cref="Bool4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int4"/> value.</param>
    /// <returns>The converted <see cref="Bool4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4")]
    public static Bool4 IntToBool(Int4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int1x1"/> value into a <see cref="Bool1x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int1x1"/> value.</param>
    /// <returns>The converted <see cref="Bool1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x1")]
    public static Bool1x1 IntToBool(Int1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int1x2"/> value into a <see cref="Bool1x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int1x2"/> value.</param>
    /// <returns>The converted <see cref="Bool1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x2")]
    public static Bool1x2 IntToBool(Int1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int1x3"/> value into a <see cref="Bool1x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int1x3"/> value.</param>
    /// <returns>The converted <see cref="Bool1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x3")]
    public static Bool1x3 IntToBool(Int1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int1x4"/> value into a <see cref="Bool1x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int1x4"/> value.</param>
    /// <returns>The converted <see cref="Bool1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x4")]
    public static Bool1x4 IntToBool(Int1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int2x1"/> value into a <see cref="Bool2x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int2x1"/> value.</param>
    /// <returns>The converted <see cref="Bool2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x1")]
    public static Bool2x1 IntToBool(Int2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int2x2"/> value into a <see cref="Bool2x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int2x2"/> value.</param>
    /// <returns>The converted <see cref="Bool2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x2")]
    public static Bool2x2 IntToBool(Int2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int2x3"/> value into a <see cref="Bool2x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int2x3"/> value.</param>
    /// <returns>The converted <see cref="Bool2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x3")]
    public static Bool2x3 IntToBool(Int2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int2x4"/> value into a <see cref="Bool2x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int2x4"/> value.</param>
    /// <returns>The converted <see cref="Bool2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x4")]
    public static Bool2x4 IntToBool(Int2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int3x1"/> value into a <see cref="Bool3x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int3x1"/> value.</param>
    /// <returns>The converted <see cref="Bool3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x1")]
    public static Bool3x1 IntToBool(Int3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int3x2"/> value into a <see cref="Bool3x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int3x2"/> value.</param>
    /// <returns>The converted <see cref="Bool3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x2")]
    public static Bool3x2 IntToBool(Int3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int3x3"/> value into a <see cref="Bool3x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int3x3"/> value.</param>
    /// <returns>The converted <see cref="Bool3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x3")]
    public static Bool3x3 IntToBool(Int3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int3x4"/> value into a <see cref="Bool3x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int3x4"/> value.</param>
    /// <returns>The converted <see cref="Bool3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x4")]
    public static Bool3x4 IntToBool(Int3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int4x1"/> value into a <see cref="Bool4x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int4x1"/> value.</param>
    /// <returns>The converted <see cref="Bool4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x1")]
    public static Bool4x1 IntToBool(Int4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int4x2"/> value into a <see cref="Bool4x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int4x2"/> value.</param>
    /// <returns>The converted <see cref="Bool4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x2")]
    public static Bool4x2 IntToBool(Int4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int4x3"/> value into a <see cref="Bool4x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int4x3"/> value.</param>
    /// <returns>The converted <see cref="Bool4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x3")]
    public static Bool4x3 IntToBool(Int4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int4x4"/> value into a <see cref="Bool4x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int4x4"/> value.</param>
    /// <returns>The converted <see cref="Bool4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x4")]
    public static Bool4x4 IntToBool(Int4x4 x) => default;
    /// <summary>
    /// Converts the input <see cref="uint"/> value into a <see cref="bool"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="uint"/> value.</param>
    /// <returns>The converted <see cref="bool"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool")]
    public static bool UIntToBool(uint x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt2"/> value into a <see cref="Bool2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt2"/> value.</param>
    /// <returns>The converted <see cref="Bool2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2")]
    public static Bool2 UIntToBool(UInt2 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt3"/> value into a <see cref="Bool3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt3"/> value.</param>
    /// <returns>The converted <see cref="Bool3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3")]
    public static Bool3 UIntToBool(UInt3 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt4"/> value into a <see cref="Bool4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt4"/> value.</param>
    /// <returns>The converted <see cref="Bool4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4")]
    public static Bool4 UIntToBool(UInt4 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt1x1"/> value into a <see cref="Bool1x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt1x1"/> value.</param>
    /// <returns>The converted <see cref="Bool1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x1")]
    public static Bool1x1 UIntToBool(UInt1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt1x2"/> value into a <see cref="Bool1x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt1x2"/> value.</param>
    /// <returns>The converted <see cref="Bool1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x2")]
    public static Bool1x2 UIntToBool(UInt1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt1x3"/> value into a <see cref="Bool1x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt1x3"/> value.</param>
    /// <returns>The converted <see cref="Bool1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x3")]
    public static Bool1x3 UIntToBool(UInt1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt1x4"/> value into a <see cref="Bool1x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt1x4"/> value.</param>
    /// <returns>The converted <see cref="Bool1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x4")]
    public static Bool1x4 UIntToBool(UInt1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt2x1"/> value into a <see cref="Bool2x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt2x1"/> value.</param>
    /// <returns>The converted <see cref="Bool2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x1")]
    public static Bool2x1 UIntToBool(UInt2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt2x2"/> value into a <see cref="Bool2x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt2x2"/> value.</param>
    /// <returns>The converted <see cref="Bool2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x2")]
    public static Bool2x2 UIntToBool(UInt2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt2x3"/> value into a <see cref="Bool2x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt2x3"/> value.</param>
    /// <returns>The converted <see cref="Bool2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x3")]
    public static Bool2x3 UIntToBool(UInt2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt2x4"/> value into a <see cref="Bool2x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt2x4"/> value.</param>
    /// <returns>The converted <see cref="Bool2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x4")]
    public static Bool2x4 UIntToBool(UInt2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt3x1"/> value into a <see cref="Bool3x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt3x1"/> value.</param>
    /// <returns>The converted <see cref="Bool3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x1")]
    public static Bool3x1 UIntToBool(UInt3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt3x2"/> value into a <see cref="Bool3x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt3x2"/> value.</param>
    /// <returns>The converted <see cref="Bool3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x2")]
    public static Bool3x2 UIntToBool(UInt3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt3x3"/> value into a <see cref="Bool3x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt3x3"/> value.</param>
    /// <returns>The converted <see cref="Bool3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x3")]
    public static Bool3x3 UIntToBool(UInt3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt3x4"/> value into a <see cref="Bool3x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt3x4"/> value.</param>
    /// <returns>The converted <see cref="Bool3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x4")]
    public static Bool3x4 UIntToBool(UInt3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt4x1"/> value into a <see cref="Bool4x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt4x1"/> value.</param>
    /// <returns>The converted <see cref="Bool4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x1")]
    public static Bool4x1 UIntToBool(UInt4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt4x2"/> value into a <see cref="Bool4x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt4x2"/> value.</param>
    /// <returns>The converted <see cref="Bool4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x2")]
    public static Bool4x2 UIntToBool(UInt4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt4x3"/> value into a <see cref="Bool4x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt4x3"/> value.</param>
    /// <returns>The converted <see cref="Bool4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x3")]
    public static Bool4x3 UIntToBool(UInt4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt4x4"/> value into a <see cref="Bool4x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt4x4"/> value.</param>
    /// <returns>The converted <see cref="Bool4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x4")]
    public static Bool4x4 UIntToBool(UInt4x4 x) => default;
}
