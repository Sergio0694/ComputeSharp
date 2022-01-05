using System;
#if !SOURCE_GENERATOR
using Microsoft.Toolkit.Diagnostics;
#endif

namespace ComputeSharp;

/// <summary>
/// An attribute that indicates that a given shader should be precompiled at build time and embedded
/// directly into the containing assembly as static bytecode, to avoid compiling it at runtime.
/// <para>
/// This attribute can be used to annotate shader types as follows:
/// <code>
/// // A compute shader that is dispatched on a target buffer
/// [EmbeddedBytecode(DispatchAxis.X)]
/// struct MyShader : IComputeShader
/// {
/// }
/// </code>
/// Or similarly, for a pixel shader:
/// <code>
/// // A pixel shader that is dispatched on a target texture
/// [EmbeddedBytecode(DispatchAxis.XY)]
/// struct MyShader : IPixelShader&lt;float4&gt;
/// {
/// }
/// </code>
/// </para>
/// <para>
/// Using <see cref="DispatchAxis"/> is an easier way to precompile shaders when dispatching them over known dimensions. For more
/// fine grained control over the thread size values when dispatching, use <see cref="EmbeddedBytecodeAttribute(int, int, int)"/>.
/// </para>
/// <para>
/// </para>
/// The runtime compilation will automatically be skipped if the shader is invoked using matching thread count values,
/// otherwise the usual runtime compilation will be used as fallback if the <c>ComputeSharp.Dynamic</c> library is
/// referenced. If not, <see cref="NotSupportedException"/> will be thrown when trying to dispatch the shader.
/// </summary>
/// <remarks>
/// <para>
/// Using this attribute is mandatory when not referencing <c>ComputeSharp.Dynamic</c>.
/// </para>
/// <para>
/// This attribute can only be used when the shader type being annotated does not require dynamic
/// features. Specifically, this attribute is not supported if the shader captures any delegates.
/// </para>
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class EmbeddedBytecodeAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="EmbeddedBytecodeAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="dispatchAxis">The target dispatch axes for the shader to run.</param>
    public EmbeddedBytecodeAttribute(DispatchAxis dispatchAxis)
    {
#if !SOURCE_GENERATOR
        (ThreadsX, ThreadsY, ThreadsZ) = dispatchAxis switch
        {
            DispatchAxis.X => (64, 1, 1),
            DispatchAxis.Y => (1, 64, 1),
            DispatchAxis.Z => (1, 1, 64),
            DispatchAxis.XY => (8, 8, 1),
            DispatchAxis.XZ => (8, 1, 8),
            DispatchAxis.YZ => (1, 8, 8),
            DispatchAxis.XYZ => (4, 4, 4),
            _ => ThrowHelper.ThrowArgumentException<(int, int, int)>(nameof(dispatchAxis), "Invalid dispatch axis value.")
        };
#endif
    }

    /// <summary>
    /// Creates a new <see cref="EmbeddedBytecodeAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
    /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
    /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
    public EmbeddedBytecodeAttribute(int threadsX, int threadsY, int threadsZ)
    {
        ThreadsX = threadsX;
        ThreadsY = threadsY;
        ThreadsZ = threadsZ;
    }

    /// <summary>
    /// Gets the number of threads in each thread group for the X axis
    /// </summary>
    public int ThreadsX { get; }

    /// <summary>
    /// Gets the number of threads in each thread group for the Y axis
    /// </summary>
    public int ThreadsY { get; }

    /// <summary>
    /// Gets the number of threads in each thread group for the Z axis
    /// </summary>
    public int ThreadsZ { get; }
}
