#pragma warning disable CS0660, CS0661, IDE0290

namespace ComputeSharp;

/// <summary>
/// A <see langword="struct"/> that maps the <see langword="bool2"/> HLSL type.
/// </summary>
public partial struct Bool2
{
    /// <summary>
    /// Creates a new <see cref="Bool2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool2"/> instance.</param>
    public static implicit operator Bool2(bool x) => new(x, x);
}