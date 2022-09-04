using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ComputeSharp.D2D1.Shaders.Interop.Helpers;

/// <summary>
/// A 64-bit version of the <see cref="Point"/> type.
/// </summary>
internal struct Point64 : IEquatable<Point64>
{
    /// <summary>
    /// The left coordinate of the current point.
    /// </summary>
    private long x;

    /// <summary>
    /// The top coordinate of the current point.
    /// </summary>
    private long y;

    /// <summary>
    /// Creates a new <see cref="Point64"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The horizontal coordinate of the point.</param>
    /// <param name="y">The vertical coordinate of the point.</param>
    public Point64(long x, long y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Gets the horizontal coordinate of the current point.
    /// </summary>
    public readonly long X
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.x;
    }

    /// <summary>
    /// Gets the vertical coordinate of the current point.
    /// </summary>
    public readonly long Y
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.y;
    }

    /// <summary>
    /// Transforms the current point using an input matrix.
    /// </summary>
    /// <param name="matrix">The transformation matrix to use.</param>
    public void Transform(in Matrix3x2 matrix)
    {
        // A 3x2 transformation matrix can be thought of a 2x2 matrix with an extra
        // 1x2 row representing absolute offsets. As such, these do not compute into
        // the actual cross product with the point (which would not be doable otherwise).
        //
        // Consider a 3x2 matrix as follows:
        //
        // [ M11          , M12           ]
        // [ M21          , M22           ]
        // [ OffsetX (M31), OffsetY (M32) ]
        //
        // We first transform the point by doint the cross product with the 2x2 sub-matrix:
        //
        //         [ M11, M12 ]
        // [ x, y] [ M21, M22 ] = [ x * M11 + y * M21, x * M12 + y * M22]
        //
        // Then we sum the absolute offsets, which results in the following formula:
        //
        //         [ M11, M12 ]
        //         [ M21, M22 ]
        // [ x, y] [ M31, M32 ] = [ x * M11 + y * M21 + M31, x * M12 + y * M22 + m32]
        this.x = (long)Math.Round(this.x * matrix.M11 + this.y * matrix.M21 + matrix.M31);
        this.y = (long)Math.Round(this.x * matrix.M12 + this.y * matrix.M22 + matrix.M32);
    }

    /// <inheritdoc/>
    public readonly bool Equals(Point64 other)
    {
        return this.x == other.x && this.y == other.y;
    }
}