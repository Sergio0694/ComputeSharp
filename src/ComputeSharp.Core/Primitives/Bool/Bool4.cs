#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <summary>
/// A <see langword="struct"/> that maps the <see langword="bool4"/> HLSL type.
/// </summary>
public partial struct Bool4
{
    /// <summary>
    /// Creates a new <see cref="Bool4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool4"/> instance.</param>
    public static implicit operator Bool4(bool x) => new(x, x, x, x);
}