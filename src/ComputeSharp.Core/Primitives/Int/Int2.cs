#pragma warning disable CS0660, CS0661, IDE0290

namespace ComputeSharp;

/// <summary>
/// A <see langword="struct"/> that maps the <see langword="int2"/> HLSL type.
/// </summary>
public partial struct Int2
{
    /// <summary>
    /// Casts a <see cref="Int2"/> value to a <see cref="UInt2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Int2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt2(Int2 xy) => default;

    /// <summary>
    /// Casts a <see cref="Int2"/> value to a <see cref="Float2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Int2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float2(Int2 xy) => default;

    /// <summary>
    /// Casts a <see cref="Int2"/> value to a <see cref="Double2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Int2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double2(Int2 xy) => default;
}