#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <summary>
/// A <see langword="struct"/> that maps the <see langword="bool4"/> HLSL type.
/// </summary>
public partial struct Bool4
{
    /// <summary>
    /// Creates a new <see cref="Bool4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Bool4(bool x, bool y, bool z, bool w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Bool4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Bool4(Bool2 xy, bool z, bool w)
    {
        this.x = xy.X;
        this.y = xy.Y;
        this.z = z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Bool4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    /// <param name="zw">The value to assign to the third and fourth vector components.</param>
    public Bool4(bool x, bool y, Bool2 zw)
    {
        this.x = x;
        this.y = y;
        this.z = zw.X;
        this.w = zw.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Bool4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the third and fourth vector components.</param>
    public Bool4(Bool2 xy, Bool2 zw)
    {
        this.x = xy.X;
        this.y = xy.Y;
        this.z = zw.X;
        this.w = zw.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Bool4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xyz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Bool4(Bool3 xyz, bool w)
    {
        this.x = xyz.X;
        this.y = xyz.Y;
        this.z = xyz.Z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Bool4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yzw">The value to assign to the second, third and fourth vector components.</param>
    public Bool4(bool x, Bool3 yzw)
    {
        this.x = x;
        this.y = yzw.X;
        this.z = yzw.Y;
        this.w = yzw.Z;
    }

    /// <summary>
    /// Creates a new <see cref="Bool4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool4"/> instance.</param>
    public static implicit operator Bool4(bool x) => new(x, x, x, x);
}
