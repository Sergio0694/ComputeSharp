using System.Runtime.InteropServices;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// A type containing info on a description for a given D2D1 pixel shader resource resource.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct D2D1ResourceTextureDescription
{
    /// <summary>
    /// The index of the resource texture the description belongs to.
    /// </summary>
    /// <remarks>See notes in <see cref="D2D1InputDescription"/> for why explicit fields are used here.</remarks>
    private readonly int index;

    /// <summary>
    /// The number of dimensions of the resource texture the description belongs to.
    /// </summary>
    private readonly int dimensions;

    /// <summary>
    /// Creates a new <see cref="D2D1ResourceTextureDescription"/> instance with the specified parameters.
    /// </summary>
    /// <param name="index">The index of the resource texture the description belongs to.</param>
    /// <param name="dimensions">The number of dimensions of the resource texture the description belongs to.</param>
    public D2D1ResourceTextureDescription(int index, int dimensions)
    {
        this.index = index;
        this.dimensions = dimensions;
    }

    /// <summary>
    /// Gets the index of the resource texture the description belongs to.
    /// </summary>
    public int Index => this.index;

    /// <summary>
    /// Gets the rank of the resource texture the description belongs to.
    /// </summary>
    public int Dimensions => this.dimensions;
}