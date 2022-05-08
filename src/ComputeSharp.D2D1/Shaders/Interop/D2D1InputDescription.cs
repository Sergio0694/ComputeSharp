using System;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// A type containing info on a description for a given D2D1 pixel shader input resource.
/// </summary>
/// <remarks>
/// <para>This type exposes the values that can be set via <c>ID2D1RenderInfo::SetInputDescription</c>.</para>
/// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1renderinfo-setinputdescription"/>.</para>
/// </remarks>
public readonly struct D2D1InputDescription
{
    /// <summary>
    /// Creates a new <see cref="D2D1InputDescription"/> instance with the specified parameters.
    /// </summary>
    /// <param name="index">The index of the resource the description belongs to.</param>
    /// <param name="filter">The type of filter to apply to the input texture.</param>
    /// <param name="levelOfDetailCount">The mip level to retrieve from the upstream transform, if specified.</param>
    internal D2D1InputDescription(int index, D2D1Filter filter, int levelOfDetailCount)
    {
        Index = index;
        Filter = filter;
        LevelOfDetailCount = levelOfDetailCount;
    }

    /// <summary>
    /// Gets the index of the resource the description belongs to.
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Gets the type of filter to apply to the input texture.
    /// </summary>
    public D2D1Filter Filter { get; }

    /// <summary>
    /// Gets the mip level to retrieve from the upstream transform, if specified.
    /// </summary>
    public int LevelOfDetailCount { get; }
}
