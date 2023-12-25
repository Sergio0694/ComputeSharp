#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <summary>
/// A <see langword="struct"/> that maps the <see langword="uint4"/> HLSL type.
/// </summary>
public partial struct UInt4
{
    /// <summary>
    /// Casts a <see cref="UInt4"/> value to a <see cref="Int4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="UInt4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator Int4(UInt4 xyzw) => default;

    /// <summary>
    /// Casts a <see cref="UInt4"/> value to a <see cref="Float4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="UInt4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float4(UInt4 xyzw) => default;

    /// <summary>
    /// Casts a <see cref="UInt4"/> value to a <see cref="Double4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="UInt4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double4(UInt4 xyzw) => default;
}