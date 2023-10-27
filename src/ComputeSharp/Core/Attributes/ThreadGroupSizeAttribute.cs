using System;

namespace ComputeSharp;

/// <summary>
/// An attribute that indicates the size of the thread groups to use to dispatch a given compute shader.
/// <para>
/// This attribute can be used to annotate shader types as follows:
/// <code>
/// // A compute shader that is dispatched on a target buffer
/// [ThreadGroupSize(DispatchAxis.X)]
/// struct MyShader : IComputeShader
/// {
/// }
/// </code>
/// Or similarly, for a pixel-like compute shader:
/// <code>
/// // A compute shader that is dispatched on a target texture
/// [ThreadGroupSize(DispatchAxis.XY)]
/// struct MyShader : IComputeShader&lt;float4&gt;
/// {
/// }
/// </code>
/// </para>
/// <para>
/// Using <see cref="DispatchAxis"/> is an easier way to precompile shaders when dispatching them over known dimensions. For more
/// fine grained control over the thread size values when dispatching, use <see cref="ThreadGroupSizeAttribute(int, int, int)"/>.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class ThreadGroupSizeAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="ThreadGroupSizeAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="dispatchAxis">The target dispatch axes for the shader to run.</param>
    public ThreadGroupSizeAttribute(DispatchAxis dispatchAxis)
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
            _ => default(ArgumentException).Throw<(int, int, int)>(nameof(dispatchAxis))
        };
#endif
    }

    /// <summary>
    /// Creates a new <see cref="ThreadGroupSizeAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
    /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
    /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
    public ThreadGroupSizeAttribute(int threadsX, int threadsY, int threadsZ)
    {
        ThreadsX = threadsX;
        ThreadsY = threadsY;
        ThreadsZ = threadsZ;
    }

    /// <summary>
    /// Gets the number of threads in each thread group for the X axis.
    /// </summary>
    public int ThreadsX { get; }

    /// <summary>
    /// Gets the number of threads in each thread group for the Y axis.
    /// </summary>
    public int ThreadsY { get; }

    /// <summary>
    /// Gets the number of threads in each thread group for the Z axis.
    /// </summary>
    public int ThreadsZ { get; }
}