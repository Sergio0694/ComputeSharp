using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Shaders.Models;

/// <summary>
/// A <see langword="struct"/> representing a key for a given shader.
/// </summary>
internal readonly struct ShaderKey : IEquatable<ShaderKey>
{
    /// <summary>
    /// The hashcode of the current shader type.
    /// </summary>
    private readonly int id;

    /// <summary>
    /// The number of iterations to run on the X axis.
    /// </summary>
    private readonly int threadsX;

    /// <summary>
    /// The number of iterations to run on the Y axis.
    /// </summary>
    private readonly int threadsY;

    /// <summary>
    /// The number of iterations to run on the Z axis.
    /// </summary>
    private readonly int threadsZ;

    /// <summary>
    /// Creates a new <see cref="ShaderKey"/> instance with the specified parameters.
    /// </summary>
    /// <param name="id">The hashcode of the current shader type.</param>
    /// <param name="threadsX">The number of iterations to run on the X axis.</param>
    /// <param name="threadsY">The number of iterations to run on the Y axis.</param>
    /// <param name="threadsZ">The number of iterations to run on the Z axis.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ShaderKey(int id, int threadsX, int threadsY, int threadsZ)
    {
        this.id = id;
        this.threadsX = threadsX;
        this.threadsY = threadsY;
        this.threadsZ = threadsZ;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ShaderKey other)
    {
        return
            this.id == other.id &&
            this.threadsX == other.threadsX &&
            this.threadsY == other.threadsY &&
            this.threadsZ == other.threadsZ;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is ShaderKey other && Equals(other);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(this.id, this.threadsX, this.threadsY, this.threadsZ);
    }
}