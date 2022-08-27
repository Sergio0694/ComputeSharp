#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <summary>
/// A <see langword="struct"/> that maps the <see langword="bool3"/> HLSL type.
/// </summary>
public partial struct Bool3
{
    /// <summary>
    /// Creates a new <see cref="Bool3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    public Bool3(bool x, bool y, bool z)
    {
        this.x = x ? 1 : 0;
        this.y = y ? 1 : 0;
        this.z = z ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    public Bool3(Bool2 xy, bool z)
    {
        this.x = xy.X ? 1 : 0;
        this.y = xy.Y ? 1 : 0;
        this.z = z ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yz">The value to assign to the second and thirt vector components.</param>
    public Bool3(bool x, Bool2 yz)
    {
        this.x = x ? 1 : 0;
        this.y = yz.X ? 1 : 0;
        this.z = yz.Y ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool3"/> instance.</param>
    public static implicit operator Bool3(bool x) => new(x, x, x);
}
