using System.Numerics;

#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <inheritdoc cref="Float3x2"/>
partial struct Float3x2
{
    /// <summary>
    /// Casts a <see cref="Float3x2"/> value to a <see cref="Vector2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Float3x2"/> value to cast.</param>
    public static unsafe implicit operator Matrix3x2(Float3x2 xy) => *(Matrix3x2*)&xy;

    /// <summary>
    /// Casts a <see cref="Vector2"/> value to a <see cref="Float3x2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Vector2"/> value to cast.</param>
    public static unsafe implicit operator Float3x2(Matrix3x2 xy) => *(Float3x2*)&xy;
}

/// <inheritdoc cref="Float4x4"/>
partial struct Float4x4
{
    /// <summary>
    /// Casts a <see cref="Float4x4"/> value to a <see cref="Vector2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Float4x4"/> value to cast.</param>
    public static unsafe implicit operator Matrix4x4(Float4x4 xy) => *(Matrix4x4*)&xy;

    /// <summary>
    /// Casts a <see cref="Vector2"/> value to a <see cref="Float4x4"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Vector2"/> value to cast.</param>
    public static unsafe implicit operator Float4x4(Matrix4x4 xy) => *(Float4x4*)&xy;
}