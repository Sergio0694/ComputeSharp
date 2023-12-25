using System.Numerics;

#pragma warning disable CS0660, CS0661, IDE0290

namespace ComputeSharp;

/// <summary>
/// A <see langword="struct"/> that maps the <see langword="float2"/> HLSL type.
/// </summary>
public partial struct Float2
{
    /// <summary>
    /// Casts a <see cref="Float2"/> value to a <see cref="Vector2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Float2"/> value to cast.</param>
    public static unsafe implicit operator Vector2(Float2 xy) => *(Vector2*)&xy;

    /// <summary>
    /// Casts a <see cref="Vector2"/> value to a <see cref="Float2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Vector2"/> value to cast.</param>
    public static unsafe implicit operator Float2(Vector2 xy) => *(Float2*)&xy;
}