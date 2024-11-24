using ComputeSharp.Core.Intrinsics;

#pragma warning disable IDE0022

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
    [HlslIntrinsicName("bool", RequiresParametersMatching = true)]
    public static bool FloatToBool(float x) => default;

    /// <summary>
    /// Converts the input <see cref="Float2"/> value into a <see cref="Bool2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float2"/> value.</param>
    /// <returns>The converted <see cref="Bool2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2", RequiresParametersMatching = true)]
    public static Bool2 FloatToBool(Float2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float3"/> value into a <see cref="Bool3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float3"/> value.</param>
    /// <returns>The converted <see cref="Bool3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3", RequiresParametersMatching = true)]
    public static Bool3 FloatToBool(Float3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float4"/> value into a <see cref="Bool4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float4"/> value.</param>
    /// <returns>The converted <see cref="Bool4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4", RequiresParametersMatching = true)]
    public static Bool4 FloatToBool(Float4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float1x1"/> value into a <see cref="Bool1x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float1x1"/> value.</param>
    /// <returns>The converted <see cref="Bool1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x1", RequiresParametersMatching = true)]
    public static Bool1x1 FloatToBool(Float1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float1x2"/> value into a <see cref="Bool1x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float1x2"/> value.</param>
    /// <returns>The converted <see cref="Bool1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x2", RequiresParametersMatching = true)]
    public static Bool1x2 FloatToBool(Float1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float1x3"/> value into a <see cref="Bool1x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float1x3"/> value.</param>
    /// <returns>The converted <see cref="Bool1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x3", RequiresParametersMatching = true)]
    public static Bool1x3 FloatToBool(Float1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float1x4"/> value into a <see cref="Bool1x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float1x4"/> value.</param>
    /// <returns>The converted <see cref="Bool1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x4", RequiresParametersMatching = true)]
    public static Bool1x4 FloatToBool(Float1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float2x1"/> value into a <see cref="Bool2x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float2x1"/> value.</param>
    /// <returns>The converted <see cref="Bool2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x1", RequiresParametersMatching = true)]
    public static Bool2x1 FloatToBool(Float2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float2x2"/> value into a <see cref="Bool2x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float2x2"/> value.</param>
    /// <returns>The converted <see cref="Bool2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x2", RequiresParametersMatching = true)]
    public static Bool2x2 FloatToBool(Float2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float2x3"/> value into a <see cref="Bool2x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float2x3"/> value.</param>
    /// <returns>The converted <see cref="Bool2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x3", RequiresParametersMatching = true)]
    public static Bool2x3 FloatToBool(Float2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float2x4"/> value into a <see cref="Bool2x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float2x4"/> value.</param>
    /// <returns>The converted <see cref="Bool2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x4", RequiresParametersMatching = true)]
    public static Bool2x4 FloatToBool(Float2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float3x1"/> value into a <see cref="Bool3x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float3x1"/> value.</param>
    /// <returns>The converted <see cref="Bool3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x1", RequiresParametersMatching = true)]
    public static Bool3x1 FloatToBool(Float3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float3x2"/> value into a <see cref="Bool3x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float3x2"/> value.</param>
    /// <returns>The converted <see cref="Bool3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x2", RequiresParametersMatching = true)]
    public static Bool3x2 FloatToBool(Float3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float3x3"/> value into a <see cref="Bool3x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float3x3"/> value.</param>
    /// <returns>The converted <see cref="Bool3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x3", RequiresParametersMatching = true)]
    public static Bool3x3 FloatToBool(Float3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float3x4"/> value into a <see cref="Bool3x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float3x4"/> value.</param>
    /// <returns>The converted <see cref="Bool3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x4", RequiresParametersMatching = true)]
    public static Bool3x4 FloatToBool(Float3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float4x1"/> value into a <see cref="Bool4x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float4x1"/> value.</param>
    /// <returns>The converted <see cref="Bool4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x1", RequiresParametersMatching = true)]
    public static Bool4x1 FloatToBool(Float4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float4x2"/> value into a <see cref="Bool4x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float4x2"/> value.</param>
    /// <returns>The converted <see cref="Bool4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x2", RequiresParametersMatching = true)]
    public static Bool4x2 FloatToBool(Float4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float4x3"/> value into a <see cref="Bool4x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float4x3"/> value.</param>
    /// <returns>The converted <see cref="Bool4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x3", RequiresParametersMatching = true)]
    public static Bool4x3 FloatToBool(Float4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Float4x4"/> value into a <see cref="Bool4x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Float4x4"/> value.</param>
    /// <returns>The converted <see cref="Bool4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x4", RequiresParametersMatching = true)]
    public static Bool4x4 FloatToBool(Float4x4 x) => default;
    /// <summary>
    /// Converts the input <see cref="double"/> value into a <see cref="bool"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="double"/> value.</param>
    /// <returns>The converted <see cref="bool"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool", RequiresParametersMatching = true)]
    public static bool DoubleToBool(double x) => default;

    /// <summary>
    /// Converts the input <see cref="Double2"/> value into a <see cref="Bool2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double2"/> value.</param>
    /// <returns>The converted <see cref="Bool2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2", RequiresParametersMatching = true)]
    public static Bool2 DoubleToBool(Double2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double3"/> value into a <see cref="Bool3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double3"/> value.</param>
    /// <returns>The converted <see cref="Bool3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3", RequiresParametersMatching = true)]
    public static Bool3 DoubleToBool(Double3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double4"/> value into a <see cref="Bool4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double4"/> value.</param>
    /// <returns>The converted <see cref="Bool4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4", RequiresParametersMatching = true)]
    public static Bool4 DoubleToBool(Double4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double1x1"/> value into a <see cref="Bool1x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double1x1"/> value.</param>
    /// <returns>The converted <see cref="Bool1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x1", RequiresParametersMatching = true)]
    public static Bool1x1 DoubleToBool(Double1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double1x2"/> value into a <see cref="Bool1x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double1x2"/> value.</param>
    /// <returns>The converted <see cref="Bool1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x2", RequiresParametersMatching = true)]
    public static Bool1x2 DoubleToBool(Double1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double1x3"/> value into a <see cref="Bool1x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double1x3"/> value.</param>
    /// <returns>The converted <see cref="Bool1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x3", RequiresParametersMatching = true)]
    public static Bool1x3 DoubleToBool(Double1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double1x4"/> value into a <see cref="Bool1x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double1x4"/> value.</param>
    /// <returns>The converted <see cref="Bool1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x4", RequiresParametersMatching = true)]
    public static Bool1x4 DoubleToBool(Double1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double2x1"/> value into a <see cref="Bool2x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double2x1"/> value.</param>
    /// <returns>The converted <see cref="Bool2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x1", RequiresParametersMatching = true)]
    public static Bool2x1 DoubleToBool(Double2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double2x2"/> value into a <see cref="Bool2x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double2x2"/> value.</param>
    /// <returns>The converted <see cref="Bool2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x2", RequiresParametersMatching = true)]
    public static Bool2x2 DoubleToBool(Double2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double2x3"/> value into a <see cref="Bool2x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double2x3"/> value.</param>
    /// <returns>The converted <see cref="Bool2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x3", RequiresParametersMatching = true)]
    public static Bool2x3 DoubleToBool(Double2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double2x4"/> value into a <see cref="Bool2x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double2x4"/> value.</param>
    /// <returns>The converted <see cref="Bool2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x4", RequiresParametersMatching = true)]
    public static Bool2x4 DoubleToBool(Double2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double3x1"/> value into a <see cref="Bool3x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double3x1"/> value.</param>
    /// <returns>The converted <see cref="Bool3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x1", RequiresParametersMatching = true)]
    public static Bool3x1 DoubleToBool(Double3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double3x2"/> value into a <see cref="Bool3x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double3x2"/> value.</param>
    /// <returns>The converted <see cref="Bool3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x2", RequiresParametersMatching = true)]
    public static Bool3x2 DoubleToBool(Double3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double3x3"/> value into a <see cref="Bool3x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double3x3"/> value.</param>
    /// <returns>The converted <see cref="Bool3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x3", RequiresParametersMatching = true)]
    public static Bool3x3 DoubleToBool(Double3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double3x4"/> value into a <see cref="Bool3x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double3x4"/> value.</param>
    /// <returns>The converted <see cref="Bool3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x4", RequiresParametersMatching = true)]
    public static Bool3x4 DoubleToBool(Double3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double4x1"/> value into a <see cref="Bool4x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double4x1"/> value.</param>
    /// <returns>The converted <see cref="Bool4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x1", RequiresParametersMatching = true)]
    public static Bool4x1 DoubleToBool(Double4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double4x2"/> value into a <see cref="Bool4x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double4x2"/> value.</param>
    /// <returns>The converted <see cref="Bool4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x2", RequiresParametersMatching = true)]
    public static Bool4x2 DoubleToBool(Double4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double4x3"/> value into a <see cref="Bool4x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double4x3"/> value.</param>
    /// <returns>The converted <see cref="Bool4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x3", RequiresParametersMatching = true)]
    public static Bool4x3 DoubleToBool(Double4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Double4x4"/> value into a <see cref="Bool4x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Double4x4"/> value.</param>
    /// <returns>The converted <see cref="Bool4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x4", RequiresParametersMatching = true)]
    public static Bool4x4 DoubleToBool(Double4x4 x) => default;
    /// <summary>
    /// Converts the input <see cref="int"/> value into a <see cref="bool"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="int"/> value.</param>
    /// <returns>The converted <see cref="bool"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool", RequiresParametersMatching = true)]
    public static bool IntToBool(int x) => default;

    /// <summary>
    /// Converts the input <see cref="Int2"/> value into a <see cref="Bool2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int2"/> value.</param>
    /// <returns>The converted <see cref="Bool2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2", RequiresParametersMatching = true)]
    public static Bool2 IntToBool(Int2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int3"/> value into a <see cref="Bool3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int3"/> value.</param>
    /// <returns>The converted <see cref="Bool3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3", RequiresParametersMatching = true)]
    public static Bool3 IntToBool(Int3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int4"/> value into a <see cref="Bool4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int4"/> value.</param>
    /// <returns>The converted <see cref="Bool4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4", RequiresParametersMatching = true)]
    public static Bool4 IntToBool(Int4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int1x1"/> value into a <see cref="Bool1x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int1x1"/> value.</param>
    /// <returns>The converted <see cref="Bool1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x1", RequiresParametersMatching = true)]
    public static Bool1x1 IntToBool(Int1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int1x2"/> value into a <see cref="Bool1x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int1x2"/> value.</param>
    /// <returns>The converted <see cref="Bool1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x2", RequiresParametersMatching = true)]
    public static Bool1x2 IntToBool(Int1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int1x3"/> value into a <see cref="Bool1x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int1x3"/> value.</param>
    /// <returns>The converted <see cref="Bool1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x3", RequiresParametersMatching = true)]
    public static Bool1x3 IntToBool(Int1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int1x4"/> value into a <see cref="Bool1x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int1x4"/> value.</param>
    /// <returns>The converted <see cref="Bool1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x4", RequiresParametersMatching = true)]
    public static Bool1x4 IntToBool(Int1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int2x1"/> value into a <see cref="Bool2x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int2x1"/> value.</param>
    /// <returns>The converted <see cref="Bool2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x1", RequiresParametersMatching = true)]
    public static Bool2x1 IntToBool(Int2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int2x2"/> value into a <see cref="Bool2x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int2x2"/> value.</param>
    /// <returns>The converted <see cref="Bool2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x2", RequiresParametersMatching = true)]
    public static Bool2x2 IntToBool(Int2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int2x3"/> value into a <see cref="Bool2x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int2x3"/> value.</param>
    /// <returns>The converted <see cref="Bool2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x3", RequiresParametersMatching = true)]
    public static Bool2x3 IntToBool(Int2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int2x4"/> value into a <see cref="Bool2x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int2x4"/> value.</param>
    /// <returns>The converted <see cref="Bool2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x4", RequiresParametersMatching = true)]
    public static Bool2x4 IntToBool(Int2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int3x1"/> value into a <see cref="Bool3x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int3x1"/> value.</param>
    /// <returns>The converted <see cref="Bool3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x1", RequiresParametersMatching = true)]
    public static Bool3x1 IntToBool(Int3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int3x2"/> value into a <see cref="Bool3x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int3x2"/> value.</param>
    /// <returns>The converted <see cref="Bool3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x2", RequiresParametersMatching = true)]
    public static Bool3x2 IntToBool(Int3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int3x3"/> value into a <see cref="Bool3x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int3x3"/> value.</param>
    /// <returns>The converted <see cref="Bool3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x3", RequiresParametersMatching = true)]
    public static Bool3x3 IntToBool(Int3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int3x4"/> value into a <see cref="Bool3x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int3x4"/> value.</param>
    /// <returns>The converted <see cref="Bool3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x4", RequiresParametersMatching = true)]
    public static Bool3x4 IntToBool(Int3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int4x1"/> value into a <see cref="Bool4x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int4x1"/> value.</param>
    /// <returns>The converted <see cref="Bool4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x1", RequiresParametersMatching = true)]
    public static Bool4x1 IntToBool(Int4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int4x2"/> value into a <see cref="Bool4x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int4x2"/> value.</param>
    /// <returns>The converted <see cref="Bool4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x2", RequiresParametersMatching = true)]
    public static Bool4x2 IntToBool(Int4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int4x3"/> value into a <see cref="Bool4x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int4x3"/> value.</param>
    /// <returns>The converted <see cref="Bool4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x3", RequiresParametersMatching = true)]
    public static Bool4x3 IntToBool(Int4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Int4x4"/> value into a <see cref="Bool4x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="Int4x4"/> value.</param>
    /// <returns>The converted <see cref="Bool4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x4", RequiresParametersMatching = true)]
    public static Bool4x4 IntToBool(Int4x4 x) => default;
    /// <summary>
    /// Converts the input <see cref="uint"/> value into a <see cref="bool"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="uint"/> value.</param>
    /// <returns>The converted <see cref="bool"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool", RequiresParametersMatching = true)]
    public static bool UIntToBool(uint x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt2"/> value into a <see cref="Bool2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt2"/> value.</param>
    /// <returns>The converted <see cref="Bool2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2", RequiresParametersMatching = true)]
    public static Bool2 UIntToBool(UInt2 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt3"/> value into a <see cref="Bool3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt3"/> value.</param>
    /// <returns>The converted <see cref="Bool3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3", RequiresParametersMatching = true)]
    public static Bool3 UIntToBool(UInt3 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt4"/> value into a <see cref="Bool4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt4"/> value.</param>
    /// <returns>The converted <see cref="Bool4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4", RequiresParametersMatching = true)]
    public static Bool4 UIntToBool(UInt4 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt1x1"/> value into a <see cref="Bool1x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt1x1"/> value.</param>
    /// <returns>The converted <see cref="Bool1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x1", RequiresParametersMatching = true)]
    public static Bool1x1 UIntToBool(UInt1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt1x2"/> value into a <see cref="Bool1x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt1x2"/> value.</param>
    /// <returns>The converted <see cref="Bool1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x2", RequiresParametersMatching = true)]
    public static Bool1x2 UIntToBool(UInt1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt1x3"/> value into a <see cref="Bool1x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt1x3"/> value.</param>
    /// <returns>The converted <see cref="Bool1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x3", RequiresParametersMatching = true)]
    public static Bool1x3 UIntToBool(UInt1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt1x4"/> value into a <see cref="Bool1x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt1x4"/> value.</param>
    /// <returns>The converted <see cref="Bool1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool1x4", RequiresParametersMatching = true)]
    public static Bool1x4 UIntToBool(UInt1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt2x1"/> value into a <see cref="Bool2x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt2x1"/> value.</param>
    /// <returns>The converted <see cref="Bool2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x1", RequiresParametersMatching = true)]
    public static Bool2x1 UIntToBool(UInt2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt2x2"/> value into a <see cref="Bool2x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt2x2"/> value.</param>
    /// <returns>The converted <see cref="Bool2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x2", RequiresParametersMatching = true)]
    public static Bool2x2 UIntToBool(UInt2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt2x3"/> value into a <see cref="Bool2x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt2x3"/> value.</param>
    /// <returns>The converted <see cref="Bool2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x3", RequiresParametersMatching = true)]
    public static Bool2x3 UIntToBool(UInt2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt2x4"/> value into a <see cref="Bool2x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt2x4"/> value.</param>
    /// <returns>The converted <see cref="Bool2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool2x4", RequiresParametersMatching = true)]
    public static Bool2x4 UIntToBool(UInt2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt3x1"/> value into a <see cref="Bool3x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt3x1"/> value.</param>
    /// <returns>The converted <see cref="Bool3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x1", RequiresParametersMatching = true)]
    public static Bool3x1 UIntToBool(UInt3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt3x2"/> value into a <see cref="Bool3x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt3x2"/> value.</param>
    /// <returns>The converted <see cref="Bool3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x2", RequiresParametersMatching = true)]
    public static Bool3x2 UIntToBool(UInt3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt3x3"/> value into a <see cref="Bool3x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt3x3"/> value.</param>
    /// <returns>The converted <see cref="Bool3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x3", RequiresParametersMatching = true)]
    public static Bool3x3 UIntToBool(UInt3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt3x4"/> value into a <see cref="Bool3x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt3x4"/> value.</param>
    /// <returns>The converted <see cref="Bool3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool3x4", RequiresParametersMatching = true)]
    public static Bool3x4 UIntToBool(UInt3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt4x1"/> value into a <see cref="Bool4x1"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt4x1"/> value.</param>
    /// <returns>The converted <see cref="Bool4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x1", RequiresParametersMatching = true)]
    public static Bool4x1 UIntToBool(UInt4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt4x2"/> value into a <see cref="Bool4x2"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt4x2"/> value.</param>
    /// <returns>The converted <see cref="Bool4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x2", RequiresParametersMatching = true)]
    public static Bool4x2 UIntToBool(UInt4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt4x3"/> value into a <see cref="Bool4x3"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt4x3"/> value.</param>
    /// <returns>The converted <see cref="Bool4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x3", RequiresParametersMatching = true)]
    public static Bool4x3 UIntToBool(UInt4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="UInt4x4"/> value into a <see cref="Bool4x4"/> value (non-zero elements map to <see langword="true"/>, zero elements map to <see langword="false"/>).
    /// </summary>
    /// <param name="x">The input <see cref="UInt4x4"/> value.</param>
    /// <returns>The converted <see cref="Bool4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("bool4x4", RequiresParametersMatching = true)]
    public static Bool4x4 UIntToBool(UInt4x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="bool"/> value into a <see cref="float"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="bool"/> value.</param>
    /// <returns>The converted <see cref="float"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float", RequiresParametersMatching = true)]
    public static float BoolToFloat(bool x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2"/> value into a <see cref="Float2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2"/> value.</param>
    /// <returns>The converted <see cref="Float2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float2", RequiresParametersMatching = true)]
    public static Float2 BoolToFloat(Bool2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3"/> value into a <see cref="Float3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3"/> value.</param>
    /// <returns>The converted <see cref="Float3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float3", RequiresParametersMatching = true)]
    public static Float3 BoolToFloat(Bool3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4"/> value into a <see cref="Float4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4"/> value.</param>
    /// <returns>The converted <see cref="Float4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float4", RequiresParametersMatching = true)]
    public static Float4 BoolToFloat(Bool4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x1"/> value into a <see cref="Float1x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x1"/> value.</param>
    /// <returns>The converted <see cref="Float1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float1x1", RequiresParametersMatching = true)]
    public static Float1x1 BoolToFloat(Bool1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x2"/> value into a <see cref="Float1x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x2"/> value.</param>
    /// <returns>The converted <see cref="Float1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float1x2", RequiresParametersMatching = true)]
    public static Float1x2 BoolToFloat(Bool1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x3"/> value into a <see cref="Float1x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x3"/> value.</param>
    /// <returns>The converted <see cref="Float1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float1x3", RequiresParametersMatching = true)]
    public static Float1x3 BoolToFloat(Bool1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x4"/> value into a <see cref="Float1x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x4"/> value.</param>
    /// <returns>The converted <see cref="Float1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float1x4", RequiresParametersMatching = true)]
    public static Float1x4 BoolToFloat(Bool1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x1"/> value into a <see cref="Float2x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x1"/> value.</param>
    /// <returns>The converted <see cref="Float2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float2x1", RequiresParametersMatching = true)]
    public static Float2x1 BoolToFloat(Bool2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x2"/> value into a <see cref="Float2x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x2"/> value.</param>
    /// <returns>The converted <see cref="Float2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float2x2", RequiresParametersMatching = true)]
    public static Float2x2 BoolToFloat(Bool2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x3"/> value into a <see cref="Float2x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x3"/> value.</param>
    /// <returns>The converted <see cref="Float2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float2x3", RequiresParametersMatching = true)]
    public static Float2x3 BoolToFloat(Bool2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x4"/> value into a <see cref="Float2x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x4"/> value.</param>
    /// <returns>The converted <see cref="Float2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float2x4", RequiresParametersMatching = true)]
    public static Float2x4 BoolToFloat(Bool2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x1"/> value into a <see cref="Float3x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x1"/> value.</param>
    /// <returns>The converted <see cref="Float3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float3x1", RequiresParametersMatching = true)]
    public static Float3x1 BoolToFloat(Bool3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x2"/> value into a <see cref="Float3x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x2"/> value.</param>
    /// <returns>The converted <see cref="Float3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float3x2", RequiresParametersMatching = true)]
    public static Float3x2 BoolToFloat(Bool3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x3"/> value into a <see cref="Float3x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x3"/> value.</param>
    /// <returns>The converted <see cref="Float3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float3x3", RequiresParametersMatching = true)]
    public static Float3x3 BoolToFloat(Bool3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x4"/> value into a <see cref="Float3x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x4"/> value.</param>
    /// <returns>The converted <see cref="Float3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float3x4", RequiresParametersMatching = true)]
    public static Float3x4 BoolToFloat(Bool3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x1"/> value into a <see cref="Float4x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x1"/> value.</param>
    /// <returns>The converted <see cref="Float4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float4x1", RequiresParametersMatching = true)]
    public static Float4x1 BoolToFloat(Bool4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x2"/> value into a <see cref="Float4x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x2"/> value.</param>
    /// <returns>The converted <see cref="Float4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float4x2", RequiresParametersMatching = true)]
    public static Float4x2 BoolToFloat(Bool4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x3"/> value into a <see cref="Float4x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x3"/> value.</param>
    /// <returns>The converted <see cref="Float4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float4x3", RequiresParametersMatching = true)]
    public static Float4x3 BoolToFloat(Bool4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x4"/> value into a <see cref="Float4x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x4"/> value.</param>
    /// <returns>The converted <see cref="Float4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("float4x4", RequiresParametersMatching = true)]
    public static Float4x4 BoolToFloat(Bool4x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="bool"/> value into a <see cref="double"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="bool"/> value.</param>
    /// <returns>The converted <see cref="double"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double", RequiresParametersMatching = true)]
    public static double BoolToDouble(bool x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2"/> value into a <see cref="Double2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2"/> value.</param>
    /// <returns>The converted <see cref="Double2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double2", RequiresParametersMatching = true)]
    public static Double2 BoolToDouble(Bool2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3"/> value into a <see cref="Double3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3"/> value.</param>
    /// <returns>The converted <see cref="Double3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double3", RequiresParametersMatching = true)]
    public static Double3 BoolToDouble(Bool3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4"/> value into a <see cref="Double4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4"/> value.</param>
    /// <returns>The converted <see cref="Double4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double4", RequiresParametersMatching = true)]
    public static Double4 BoolToDouble(Bool4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x1"/> value into a <see cref="Double1x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x1"/> value.</param>
    /// <returns>The converted <see cref="Double1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double1x1", RequiresParametersMatching = true)]
    public static Double1x1 BoolToDouble(Bool1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x2"/> value into a <see cref="Double1x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x2"/> value.</param>
    /// <returns>The converted <see cref="Double1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double1x2", RequiresParametersMatching = true)]
    public static Double1x2 BoolToDouble(Bool1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x3"/> value into a <see cref="Double1x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x3"/> value.</param>
    /// <returns>The converted <see cref="Double1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double1x3", RequiresParametersMatching = true)]
    public static Double1x3 BoolToDouble(Bool1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x4"/> value into a <see cref="Double1x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x4"/> value.</param>
    /// <returns>The converted <see cref="Double1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double1x4", RequiresParametersMatching = true)]
    public static Double1x4 BoolToDouble(Bool1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x1"/> value into a <see cref="Double2x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x1"/> value.</param>
    /// <returns>The converted <see cref="Double2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double2x1", RequiresParametersMatching = true)]
    public static Double2x1 BoolToDouble(Bool2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x2"/> value into a <see cref="Double2x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x2"/> value.</param>
    /// <returns>The converted <see cref="Double2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double2x2", RequiresParametersMatching = true)]
    public static Double2x2 BoolToDouble(Bool2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x3"/> value into a <see cref="Double2x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x3"/> value.</param>
    /// <returns>The converted <see cref="Double2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double2x3", RequiresParametersMatching = true)]
    public static Double2x3 BoolToDouble(Bool2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x4"/> value into a <see cref="Double2x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x4"/> value.</param>
    /// <returns>The converted <see cref="Double2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double2x4", RequiresParametersMatching = true)]
    public static Double2x4 BoolToDouble(Bool2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x1"/> value into a <see cref="Double3x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x1"/> value.</param>
    /// <returns>The converted <see cref="Double3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double3x1", RequiresParametersMatching = true)]
    public static Double3x1 BoolToDouble(Bool3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x2"/> value into a <see cref="Double3x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x2"/> value.</param>
    /// <returns>The converted <see cref="Double3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double3x2", RequiresParametersMatching = true)]
    public static Double3x2 BoolToDouble(Bool3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x3"/> value into a <see cref="Double3x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x3"/> value.</param>
    /// <returns>The converted <see cref="Double3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double3x3", RequiresParametersMatching = true)]
    public static Double3x3 BoolToDouble(Bool3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x4"/> value into a <see cref="Double3x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x4"/> value.</param>
    /// <returns>The converted <see cref="Double3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double3x4", RequiresParametersMatching = true)]
    public static Double3x4 BoolToDouble(Bool3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x1"/> value into a <see cref="Double4x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x1"/> value.</param>
    /// <returns>The converted <see cref="Double4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double4x1", RequiresParametersMatching = true)]
    public static Double4x1 BoolToDouble(Bool4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x2"/> value into a <see cref="Double4x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x2"/> value.</param>
    /// <returns>The converted <see cref="Double4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double4x2", RequiresParametersMatching = true)]
    public static Double4x2 BoolToDouble(Bool4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x3"/> value into a <see cref="Double4x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x3"/> value.</param>
    /// <returns>The converted <see cref="Double4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double4x3", RequiresParametersMatching = true)]
    public static Double4x3 BoolToDouble(Bool4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x4"/> value into a <see cref="Double4x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x4"/> value.</param>
    /// <returns>The converted <see cref="Double4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("double4x4", RequiresParametersMatching = true)]
    public static Double4x4 BoolToDouble(Bool4x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="bool"/> value into a <see cref="int"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="bool"/> value.</param>
    /// <returns>The converted <see cref="int"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int", RequiresParametersMatching = true)]
    public static int BoolToInt(bool x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2"/> value into a <see cref="Int2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2"/> value.</param>
    /// <returns>The converted <see cref="Int2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int2", RequiresParametersMatching = true)]
    public static Int2 BoolToInt(Bool2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3"/> value into a <see cref="Int3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3"/> value.</param>
    /// <returns>The converted <see cref="Int3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int3", RequiresParametersMatching = true)]
    public static Int3 BoolToInt(Bool3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4"/> value into a <see cref="Int4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4"/> value.</param>
    /// <returns>The converted <see cref="Int4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int4", RequiresParametersMatching = true)]
    public static Int4 BoolToInt(Bool4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x1"/> value into a <see cref="Int1x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x1"/> value.</param>
    /// <returns>The converted <see cref="Int1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int1x1", RequiresParametersMatching = true)]
    public static Int1x1 BoolToInt(Bool1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x2"/> value into a <see cref="Int1x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x2"/> value.</param>
    /// <returns>The converted <see cref="Int1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int1x2", RequiresParametersMatching = true)]
    public static Int1x2 BoolToInt(Bool1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x3"/> value into a <see cref="Int1x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x3"/> value.</param>
    /// <returns>The converted <see cref="Int1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int1x3", RequiresParametersMatching = true)]
    public static Int1x3 BoolToInt(Bool1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x4"/> value into a <see cref="Int1x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x4"/> value.</param>
    /// <returns>The converted <see cref="Int1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int1x4", RequiresParametersMatching = true)]
    public static Int1x4 BoolToInt(Bool1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x1"/> value into a <see cref="Int2x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x1"/> value.</param>
    /// <returns>The converted <see cref="Int2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int2x1", RequiresParametersMatching = true)]
    public static Int2x1 BoolToInt(Bool2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x2"/> value into a <see cref="Int2x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x2"/> value.</param>
    /// <returns>The converted <see cref="Int2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int2x2", RequiresParametersMatching = true)]
    public static Int2x2 BoolToInt(Bool2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x3"/> value into a <see cref="Int2x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x3"/> value.</param>
    /// <returns>The converted <see cref="Int2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int2x3", RequiresParametersMatching = true)]
    public static Int2x3 BoolToInt(Bool2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x4"/> value into a <see cref="Int2x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x4"/> value.</param>
    /// <returns>The converted <see cref="Int2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int2x4", RequiresParametersMatching = true)]
    public static Int2x4 BoolToInt(Bool2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x1"/> value into a <see cref="Int3x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x1"/> value.</param>
    /// <returns>The converted <see cref="Int3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int3x1", RequiresParametersMatching = true)]
    public static Int3x1 BoolToInt(Bool3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x2"/> value into a <see cref="Int3x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x2"/> value.</param>
    /// <returns>The converted <see cref="Int3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int3x2", RequiresParametersMatching = true)]
    public static Int3x2 BoolToInt(Bool3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x3"/> value into a <see cref="Int3x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x3"/> value.</param>
    /// <returns>The converted <see cref="Int3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int3x3", RequiresParametersMatching = true)]
    public static Int3x3 BoolToInt(Bool3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x4"/> value into a <see cref="Int3x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x4"/> value.</param>
    /// <returns>The converted <see cref="Int3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int3x4", RequiresParametersMatching = true)]
    public static Int3x4 BoolToInt(Bool3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x1"/> value into a <see cref="Int4x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x1"/> value.</param>
    /// <returns>The converted <see cref="Int4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int4x1", RequiresParametersMatching = true)]
    public static Int4x1 BoolToInt(Bool4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x2"/> value into a <see cref="Int4x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x2"/> value.</param>
    /// <returns>The converted <see cref="Int4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int4x2", RequiresParametersMatching = true)]
    public static Int4x2 BoolToInt(Bool4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x3"/> value into a <see cref="Int4x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x3"/> value.</param>
    /// <returns>The converted <see cref="Int4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int4x3", RequiresParametersMatching = true)]
    public static Int4x3 BoolToInt(Bool4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x4"/> value into a <see cref="Int4x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x4"/> value.</param>
    /// <returns>The converted <see cref="Int4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("int4x4", RequiresParametersMatching = true)]
    public static Int4x4 BoolToInt(Bool4x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="bool"/> value into a <see cref="uint"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="bool"/> value.</param>
    /// <returns>The converted <see cref="uint"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint", RequiresParametersMatching = true)]
    public static uint BoolToUInt(bool x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2"/> value into a <see cref="UInt2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2"/> value.</param>
    /// <returns>The converted <see cref="UInt2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint2", RequiresParametersMatching = true)]
    public static UInt2 BoolToUInt(Bool2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3"/> value into a <see cref="UInt3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3"/> value.</param>
    /// <returns>The converted <see cref="UInt3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint3", RequiresParametersMatching = true)]
    public static UInt3 BoolToUInt(Bool3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4"/> value into a <see cref="UInt4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4"/> value.</param>
    /// <returns>The converted <see cref="UInt4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint4", RequiresParametersMatching = true)]
    public static UInt4 BoolToUInt(Bool4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x1"/> value into a <see cref="UInt1x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x1"/> value.</param>
    /// <returns>The converted <see cref="UInt1x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint1x1", RequiresParametersMatching = true)]
    public static UInt1x1 BoolToUInt(Bool1x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x2"/> value into a <see cref="UInt1x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x2"/> value.</param>
    /// <returns>The converted <see cref="UInt1x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint1x2", RequiresParametersMatching = true)]
    public static UInt1x2 BoolToUInt(Bool1x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x3"/> value into a <see cref="UInt1x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x3"/> value.</param>
    /// <returns>The converted <see cref="UInt1x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint1x3", RequiresParametersMatching = true)]
    public static UInt1x3 BoolToUInt(Bool1x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool1x4"/> value into a <see cref="UInt1x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool1x4"/> value.</param>
    /// <returns>The converted <see cref="UInt1x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint1x4", RequiresParametersMatching = true)]
    public static UInt1x4 BoolToUInt(Bool1x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x1"/> value into a <see cref="UInt2x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x1"/> value.</param>
    /// <returns>The converted <see cref="UInt2x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint2x1", RequiresParametersMatching = true)]
    public static UInt2x1 BoolToUInt(Bool2x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x2"/> value into a <see cref="UInt2x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x2"/> value.</param>
    /// <returns>The converted <see cref="UInt2x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint2x2", RequiresParametersMatching = true)]
    public static UInt2x2 BoolToUInt(Bool2x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x3"/> value into a <see cref="UInt2x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x3"/> value.</param>
    /// <returns>The converted <see cref="UInt2x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint2x3", RequiresParametersMatching = true)]
    public static UInt2x3 BoolToUInt(Bool2x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool2x4"/> value into a <see cref="UInt2x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool2x4"/> value.</param>
    /// <returns>The converted <see cref="UInt2x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint2x4", RequiresParametersMatching = true)]
    public static UInt2x4 BoolToUInt(Bool2x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x1"/> value into a <see cref="UInt3x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x1"/> value.</param>
    /// <returns>The converted <see cref="UInt3x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint3x1", RequiresParametersMatching = true)]
    public static UInt3x1 BoolToUInt(Bool3x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x2"/> value into a <see cref="UInt3x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x2"/> value.</param>
    /// <returns>The converted <see cref="UInt3x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint3x2", RequiresParametersMatching = true)]
    public static UInt3x2 BoolToUInt(Bool3x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x3"/> value into a <see cref="UInt3x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x3"/> value.</param>
    /// <returns>The converted <see cref="UInt3x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint3x3", RequiresParametersMatching = true)]
    public static UInt3x3 BoolToUInt(Bool3x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool3x4"/> value into a <see cref="UInt3x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool3x4"/> value.</param>
    /// <returns>The converted <see cref="UInt3x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint3x4", RequiresParametersMatching = true)]
    public static UInt3x4 BoolToUInt(Bool3x4 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x1"/> value into a <see cref="UInt4x1"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x1"/> value.</param>
    /// <returns>The converted <see cref="UInt4x1"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint4x1", RequiresParametersMatching = true)]
    public static UInt4x1 BoolToUInt(Bool4x1 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x2"/> value into a <see cref="UInt4x2"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x2"/> value.</param>
    /// <returns>The converted <see cref="UInt4x2"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint4x2", RequiresParametersMatching = true)]
    public static UInt4x2 BoolToUInt(Bool4x2 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x3"/> value into a <see cref="UInt4x3"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x3"/> value.</param>
    /// <returns>The converted <see cref="UInt4x3"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint4x3", RequiresParametersMatching = true)]
    public static UInt4x3 BoolToUInt(Bool4x3 x) => default;

    /// <summary>
    /// Converts the input <see cref="Bool4x4"/> value into a <see cref="UInt4x4"/> value (<see langword="true"/> elements map to 1, <see langword="false"/> map to 0).
    /// </summary>
    /// <param name="x">The input <see cref="Bool4x4"/> value.</param>
    /// <returns>The converted <see cref="UInt4x4"/> value.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [HlslIntrinsicName("uint4x4", RequiresParametersMatching = true)]
    public static UInt4x4 BoolToUInt(Bool4x4 x) => default;
}
