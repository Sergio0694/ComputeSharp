using ComputeSharp.Core.Intrinsics;

#pragma warning disable IDE0022

namespace ComputeSharp;

/// <inheritdoc cref="Hlsl"/>
partial class Hlsl
{
    /// <summary>
    /// Submits an error message to the information queue and terminates the current draw or dispatch call being executed.
    /// </summary>
    /// <remarks>
    /// This operation does nothing on rasterizers that do not support it.
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/abort"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("abort")]
    public static void Abort() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Abort)}()");

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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-clip"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("clip")]
    public static void Clip(Float4x4 x) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(Clip)}({typeof(Float4x4)})");

    /// <summary>
    /// Performs a guaranteed atomic add of a value to a destination.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedadd"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedAdd(ref int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAdd)}({typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic add of a value to a destination.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedadd"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedAdd(ref uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAdd)}({typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Performs a guaranteed atomic add of a value to a destination.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedadd"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedAdd(ref int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAdd)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic add of a value to a destination.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedadd"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedAdd(ref uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAdd)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Performs a guaranteed atomic and of a value to a destination.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedand"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedAnd(ref int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAnd)}({typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic and of a value to a destination.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedand"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedAnd(ref uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAnd)}({typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Performs a guaranteed atomic and of a value to a destination.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedand"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedAnd(ref int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAnd)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic and of a value to a destination.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedand"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedAnd(ref uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAnd)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Atomically compares the destination with the comparison value. If they are identical, the destination
    /// is overwritten with the input value. The original value is set to the destination's original value.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="comparison">The comparison value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedcompareexchange"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedCompareExchange(ref int destination, int comparison, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedCompareExchange)}({typeof(int)}, {typeof(int)}, {typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Atomically compares the destination with the comparison value. If they are identical, the destination
    /// is overwritten with the input value. The original value is set to the destination's original value.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="comparison">The comparison value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedcompareexchange"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedCompareExchange(ref uint destination, uint comparison, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedCompareExchange)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Atomically compares the destination to the comparison value. If they
    /// are identical, the destination is overwritten with the input value.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="comparison">The comparison value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedcomparestore"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedCompareStore(ref int destination, int comparison, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedCompareStore)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Atomically compares the destination to the comparison value. If they
    /// are identical, the destination is overwritten with the input value.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="comparison">The comparison value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedcomparestore"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedCompareStore(ref uint destination, uint comparison, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedCompareStore)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Assigns value to dest and returns the original value.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedexchange"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedExchange(ref int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedExchange)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Assigns value to dest and returns the original value.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedexchange"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedExchange(ref uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedExchange)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Performs a guaranteed atomic max.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedmax"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedMax(ref int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMax)}({typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic max.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedmax"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedMax(ref uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMax)}({typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Performs a guaranteed atomic max.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedmax"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedMax(ref int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMax)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic max.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedmax"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedMax(ref uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMax)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Performs a guaranteed atomic min.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedmin"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedMin(ref int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMin)}({typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic min.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedmin"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedMin(ref uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMin)}({typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Performs a guaranteed atomic min.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedmin"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedMin(ref int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMin)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic min.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedmin"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedMin(ref uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMin)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Performs a guaranteed atomic or.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedor"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedOr(ref int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedOr)}({typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic or.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedor"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedOr(ref uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedOr)}({typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Performs a guaranteed atomic or.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedor"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedOr(ref int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedOr)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic or.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedor"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedOr(ref uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedOr)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Performs a guaranteed atomic xor.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedxor"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedXor(ref int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedXor)}({typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic xor.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedxor"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedXor(ref uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedXor)}({typeof(uint)}, {typeof(uint)})");

    /// <summary>
    /// Performs a guaranteed atomic xor.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedxor"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedXor(ref int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedXor)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

    /// <summary>
    /// Performs a guaranteed atomic xor.
    /// </summary>
    /// <param name="destination">The destination value.</param>
    /// <param name="value">The input value.</param>
    /// <param name="original">The original input value.</param>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/interlockedxor"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void InterlockedXor(ref uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedXor)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");
}
