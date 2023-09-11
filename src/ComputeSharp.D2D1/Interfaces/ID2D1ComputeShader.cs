using System.ComponentModel;
using System;
using ComputeSharp.D2D1.__Internals;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1;

/// <summary>
/// An <see langword="interface"/> representing a D2D1 compute shader.
/// </summary>
public interface ID2D1ComputeShader : ID2D1Shader
{
    /// <summary>
    /// Gets the number of threads in each dispatched threads group.
    /// </summary>
    /// <param name="sizeX">The number of threads dispatched along the X axis in each threads group.</param>
    /// <param name="sizeY">The number of threads dispatched along the Y axis in each threads group.</param>
    /// <param name="sizeZ">The number of threads dispatched along the Z axis in each threads group.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    void GetDispatchTreads(out int sizeX, out int sizeY, out int sizeZ);

    /// <summary>
    /// Executes the current compute shader.
    /// </summary>
    void Execute();
}