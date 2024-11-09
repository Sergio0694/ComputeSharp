using ComputeSharp.Core.Intrinsics.Attributes;

#pragma warning disable IDE0022

namespace ComputeSharp.D2D1;

/// <summary>
/// A <see langword="class"/> that maps the supported HLSL intrinsic functions specifically for D2D1 shaders.
/// </summary>
/// <remarks>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct2d/hlsl-helpers"/>.</remarks>
public static class D2D
{
    /// <summary>
    /// Returns the color of the input texture at a specific index, for the current coordinate.
    /// </summary>
    /// <param name="index">The index of the input texture to get the input from.</param>
    /// <returns>The color from the target input at the current coordinate, in <c>INPUTN</c> format.</returns>
    /// <remarks>This method is only available for simple inputs.</remarks>
    [HlslIntrinsicName("D2DGetInput")]
    public static Float4 GetInput(int index) => throw new InvalidExecutionContextException($"{typeof(D2D)}.{nameof(GetInput)}({typeof(int)})");

    /// <summary>
    /// Returns the coordinate of the input texture at a specific index.
    /// </summary>
    /// <param name="index">The index of the input texture to get the coordinate for.</param>
    /// <returns>The input coordinate, in <c>TEXCOORDN</c> format.</returns>
    /// <remarks>This method is only available for complex inputs.</remarks>
    [HlslIntrinsicName("D2DGetInputCoordinate")]
    public static Float4 GetInputCoordinate(int index) => throw new InvalidExecutionContextException($"{typeof(D2D)}.{nameof(GetInputCoordinate)}({typeof(int)})");

    /// <summary>
    /// Returns the value of the current scene position.
    /// </summary>
    /// <returns>The current scene position, in <c>SCENE_POSITION</c> format.</returns>
    /// <remarks>This method is only available when <see cref="D2DRequiresScenePositionAttribute"/> is used on the shader type.</remarks>
    [HlslIntrinsicName("D2DGetScenePosition")]
    public static Float4 GetScenePosition() => throw new InvalidExecutionContextException($"{typeof(D2D)}.{nameof(GetScenePosition)}()");

    /// <summary>
    /// Samples the input texture at a specified index and at a given position.
    /// </summary>
    /// <param name="index">The index of the input texture to sample.</param>
    /// <param name="uv">The normalized coordinate to use to sample the texture.</param>
    /// <returns>The sampled value from the texture, in <c>TEXCOORDN</c> format.</returns>
    /// <remarks>This method is only available for complex inputs.</remarks>
    [HlslIntrinsicName("D2DSampleInput")]
    public static Float4 SampleInput(int index, Float2 uv) => throw new InvalidExecutionContextException($"{typeof(D2D)}.{nameof(SampleInput)}({typeof(int)}, {typeof(Float2)})");

    /// <summary>
    /// Samples the input texture at a specified index and at a given offset from the input coordinate.
    /// </summary>
    /// <param name="index">The index of the input texture to sample.</param>
    /// <param name="offset">The normalized offset to use to sample the texture.</param>
    /// <returns>The sampled value from the texture, in <c>TEXCOORDN</c> format.</returns>
    /// <remarks>This method is only available for complex inputs.</remarks>
    [HlslIntrinsicName("D2DSampleInputAtOffset")]
    public static Float4 SampleInputAtOffset(int index, Float2 offset) => throw new InvalidExecutionContextException($"{typeof(D2D)}.{nameof(SampleInputAtOffset)}({typeof(int)}, {typeof(Float2)})");

    /// <summary>
    /// Samples the input texture at a specified index and at an absolute scene position (not an input-relative position).
    /// </summary>
    /// <param name="index">The index of the input texture to sample.</param>
    /// <param name="uv">The normalized coordinate to use to sample the texture.</param>
    /// <returns>The sampled value from the texture, in <c>TEXCOORDN</c> format.</returns>
    /// <remarks>This method is only available for complex inputs.</remarks>
    [HlslIntrinsicName("D2DSampleInputAtPosition")]
    public static Float4 SampleInputAtPosition(int index, Float2 uv) => throw new InvalidExecutionContextException($"{typeof(D2D)}.{nameof(SampleInputAtPosition)}({typeof(int)}, {typeof(Float2)})");
}