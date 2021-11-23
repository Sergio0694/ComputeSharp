using System;

namespace ComputeSharp;

/// <summary>
/// An attribute that indicates that a given shader should be precompiled at build time and embedded
/// directly into the containing assembly as static bytecode, to avoid compiling it at runtime.
/// <para>
/// This attribute can be used to annotate shader types as follows:
/// <code>
/// [EmbeddedBytecode(8, 8, 1)]
/// struct MyShader : IComputeShader
/// {
/// }
/// </code>
/// </para>
/// <para>
/// This attribute can only be used when the shader type being annotated does not require dynamic
/// features. Specifically, this attribute is not supported if the shader captures any delegates.
/// </para>
/// The runtime compilation will automatically be skipped if the shader is invoked using matching
/// thread count values, otherwise the usual runtime compilation will be used as fallback. In case
/// the fallback support is not needed and all shaders in use are being precompiled, it is possible
/// to skip bundling the native <c>dxcompiler.dll</c> and <c>dxil.dll</c> libraries included with
/// ComputeSharp entirely, resulting in a smaller binary size in consuming applications. To do this,
/// the <c>PackageReference</c> item for the library should be annotated with <c>ExcludeAssets="native"</c>.
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class EmbeddedBytecodeAttribute : Attribute
{
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
    public int ThreadsX { get; init; }

    /// <summary>
    /// Gets the number of threads in each thread group for the Y axis
    /// </summary>
    public int ThreadsY { get; init; }

    /// <summary>
    /// Gets the number of threads in each thread group for the Z axis
    /// </summary>
    public int ThreadsZ { get; init; }
}
