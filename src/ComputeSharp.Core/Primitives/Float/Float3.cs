using System.Numerics;

#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <summary>
/// A <see langword="struct"/> that maps the <see langword="float3"/> HLSL type.
/// </summary>
public partial struct Float3
{
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
}