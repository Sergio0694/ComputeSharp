namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// A type containing info on a description for a given D2D1 pixel shader resource resource.
/// </summary>
public readonly struct D2D1ResourceTextureDescription
{
    /// <summary>
    /// Creates a new <see cref="D2D1ResourceTextureDescription"/> instance with the specified parameters.
    /// </summary>
    /// <param name="index">The index of the resource texture the description belongs to.</param>
    /// <param name="rank">The rank of the resource texture the description belongs to.</param>
    internal D2D1ResourceTextureDescription(int index, int rank)
    {
        Index = index;
        Rank = rank;
    }

    /// <summary>
    /// Gets the index of the resource texture the description belongs to.
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Gets the rank of the resource texture the description belongs to.
    /// </summary>
    public int Rank { get; }
}
