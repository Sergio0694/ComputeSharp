using System;

namespace ComputeSharp;

/// <summary>
/// An attribute that indicates the size of the thread groups to use to dispatch a given compute shader.
/// <para>
/// This attribute can be used to annotate shader types as follows:
/// <code>
/// // A compute shader that is dispatched on a target buffer
/// [ThreadGroupSize(DefaultThreadGroupSizes.X)]
/// struct MyShader : IComputeShader
/// {
/// }
/// </code>
/// Or similarly, for a pixel-like compute shader:
/// <code>
/// // A compute shader that is dispatched on a target texture
/// [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
/// struct MyShader : IComputeShader&lt;float4&gt;
/// {
/// }
/// </code>
/// </para>
/// <para>
/// Using <see cref="DefaultThreadGroupSizes"/> is an easier way to precompile shaders when dispatching them over known dimensions. For more
/// fine grained control over the thread size values when dispatching, use <see cref="ThreadGroupSizeAttribute(int, int, int)"/>.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class ThreadGroupSizeAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="ThreadGroupSizeAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="size">The default thread group size to use.</param>
    public ThreadGroupSizeAttribute(DefaultThreadGroupSizes size)
    {
#if !SOURCE_GENERATOR
        (ThreadsX, ThreadsY, ThreadsZ) = size switch
        {
            DefaultThreadGroupSizes.X => (64, 1, 1),
            DefaultThreadGroupSizes.Y => (1, 64, 1),
            DefaultThreadGroupSizes.Z => (1, 1, 64),
            DefaultThreadGroupSizes.XY => (8, 8, 1),
            DefaultThreadGroupSizes.XZ => (8, 1, 8),
            DefaultThreadGroupSizes.YZ => (1, 8, 8),
            DefaultThreadGroupSizes.XYZ => (4, 4, 4),
            _ => default(ArgumentException).Throw<(int, int, int)>(nameof(size))
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