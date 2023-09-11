using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// A type containing numbers for the dispatch threads of a given D2D shader.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct D2D1DispatchThreadNumbers
{
    /// <summary>
    /// The number of threads dispatched along the X axis in each threads group.
    /// </summary>
    /// <remarks>See notes in <see cref="D2D1InputDescription"/> for why explicit fields are used here.</remarks>
    private readonly int threadsX;

    /// <summary>
    /// The number of threads dispatched along the Y axis in each threads group.
    /// </summary>
    private readonly int threadsY;

    /// <summary>
    /// The number of threads dispatched along the Z axis in each threads group.
    /// </summary>
    private readonly int threadsZ;

    /// <summary>
    /// Gets the number of threads dispatched along the X axis in each threads group.
    /// </summary>
    public int ThreadsX => this.threadsX;

    /// <summary>
    /// Gets the number of threads dispatched along the Y axis in each threads group.
    /// </summary>
    public int ThreadsY => this.threadsY;

    /// <summary>
    /// Gets the number of threads dispatched along the Z axis in each threads group.
    /// </summary>
    public int ThreadsZ => this.threadsZ;

    /// <summary>
    /// Deconstructs the current instance into the components for each axis.
    /// </summary>
    /// <param name="threadsX"><inheritdoc cref="threadsX" path="/summary/node()"/></param>
    /// <param name="threadsY"><inheritdoc cref="threadsY" path="/summary/node()"/></param>
    /// <param name="threadsZ"><inheritdoc cref="threadsZ" path="/summary/node()"/></param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out int threadsX, out int threadsY, out int threadsZ)
    {
        threadsX = this.threadsX;
        threadsY = this.threadsY;
        threadsZ = this.threadsZ;
    }
}