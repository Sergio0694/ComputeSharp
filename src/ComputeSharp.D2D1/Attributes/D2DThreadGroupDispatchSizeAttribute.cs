using System;

namespace ComputeSharp;

/// <summary>
/// An attribute that indicates the number of threads in each group of dispatched threads for a D2D compute shader.
/// </summary>
/// <remarks>
/// <para>This attribute directly maps to the <c>[numthreads]</c> HLSL attribute.</para>
/// <para>For more info, see <see href="https://learn.microsoft.com/en-us/windows/win32/direct2d/custom-effects"/>.</para>
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DThreadGroupDispatchSizeAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="D2DThreadGroupDispatchSizeAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
    /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
    /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
    public D2DThreadGroupDispatchSizeAttribute(int threadsX, int threadsY, int threadsZ)
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