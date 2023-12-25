using System.Numerics;

#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <summary>
/// A <see langword="struct"/> that maps the <see langword="float3"/> HLSL type.
/// </summary>
public partial struct Float3
{
    /// <summary>
    /// Creates a new <see cref="Float3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Float3"/> instance.</param>
    public static implicit operator Float3(float x) => new(x, x, x);

    /// <summary>
    /// Casts a <see cref="Float3"/> value to a <see cref="Vector3"/> one.
    /// </summary>
    /// <param name="xyz">The input <see cref="Float3"/> value to cast.</param>
    public static unsafe implicit operator Vector3(Float3 xyz) => *(Vector3*)&xyz;

    /// <summary>
    /// Casts a <see cref="Vector3"/> value to a <see cref="Float3"/> one.
    /// </summary>
    /// <param name="xyz">The input <see cref="Vector3"/> value to cast.</param>
    public static unsafe implicit operator Float3(Vector3 xyz) => *(Float3*)&xyz;

    /// <summary>
    /// Casts a <see cref="Float3"/> value to a <see cref="Double3"/> one.
    /// </summary>
    /// <param name="xyz">The input <see cref="Float3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double3(Float3 xyz) => default;

    /// <summary>
    /// Casts a <see cref="Float3"/> value to a <see cref="Int3"/> one.
    /// </summary>
    /// <param name="xyz">The input <see cref="Float3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator Int3(Float3 xyz) => default;

    /// <summary>
    /// Casts a <see cref="Float3"/> value to a <see cref="UInt3"/> one.
    /// </summary>
    /// <param name="xyz">The input <see cref="Float3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt3(Float3 xyz) => default;
}