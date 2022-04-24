using ComputeSharp.Core.Intrinsics.Attributes;
using ComputeSharp.Exceptions;

namespace ComputeSharp;

/// <inheritdoc cref="Hlsl"/>
partial class Hlsl
{
    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(float x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(float)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float2 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float2)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float3 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float3)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float4 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float4)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float1x1 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float1x1)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float1x2 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float1x2)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float1x3 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float1x3)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float1x4 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float1x4)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float2x1 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float2x1)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float2x2 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float2x2)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float2x3 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float2x3)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float2x4 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float2x4)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float3x1 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float3x1)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float3x2 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float3x2)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float3x3 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float3x3)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float3x4 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float3x4)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float4x1 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float4x1)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float4x2 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float4x2)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float4x3 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float4x3)})");

    /// <summary>
    /// Discards the current pixel if the specified value is less than zero.
    /// </summary>
    /// <param name="x">The specified value.</param>
    /// <remarks>
    /// <para>Use the clip HLSL intrinsic function to simulate clipping planes if each component of the x parameter represents the distance from a plane.</para>
    /// <para>Also, use the clip function to test for alpha behavior, as shown in the following example:</para>
    /// <code>
    /// Hlsl.Clip(color.A &lt; 0.1f ? -1 : 1);
    /// </code>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float4x4 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float4x4)})");
}
