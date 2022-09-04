﻿using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ComputeSharp.D2D1.Shaders.Interop.Helpers;

/// <summary>
/// A 64-bit version of the <see cref="Rectangle"/> type.
/// </summary>
internal struct Rectangle64 : IEquatable<Rectangle64>
{
    /// <summary>
    /// The left coordinate of the current rectangle.
    /// </summary>
    private long x;

    /// <summary>
    /// The top coordinate of the current rectangle.
    /// </summary>
    private long y;

    /// <summary>
    /// The width of the current rectangle.
    /// </summary>
    private long width;

    /// <summary>
    /// The height of the current rectangle.
    /// </summary>
    private long height;

    /// <summary>
    /// Creates a new <see cref="Rectangle64"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The left coordinate of the rectangle.</param>
    /// <param name="y">The top coordinate of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    public Rectangle64(long x, long y, long width, long height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    /// <summary>
    /// Gets the left coordinate of the current rectangle.
    /// </summary>
    public readonly long Left
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.x;
    }

    /// <summary>
    /// Gets the top coordinate of the current rectangle.
    /// </summary>
    public readonly long Top
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.y;
    }

    /// <summary>
    /// Gets the right coordinate of the current rectangle.
    /// </summary>
    public readonly long Right
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.x + this.width;
    }

    /// <summary>
    /// Gets the bottom coordinate of the current rectangle.
    /// </summary>
    public readonly long Bottom
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.y + this.height;
    }

    /// <summary>
    /// Gets whether the current instance represents an empty rectangle.
    /// </summary>
    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.x == long.MaxValue && this.y == long.MaxValue && this.width == long.MinValue && this.height == long.MinValue;
    }

    /// <summary>
    /// Gets a <see cref="Rectangle64"/> instance representing an empty rectangle.
    /// </summary>
    /// <remarks>
    /// The returned instance is such that computing the union of it with any other rectangle yields the other
    /// rectangle, and computing the intersection of it with any other rectangle yields the empty rectangle.
    /// </remarks>
    public static Rectangle64 Empty => new(long.MaxValue, long.MaxValue, long.MinValue, long.MinValue);

    /// <summary>
    /// Updates the current rectangle to be the union of itself and a given input rectangle.
    /// </summary>
    /// <param name="rectangle">The input <see cref="Rectangle64"/> instance to compute the union with.</param>
    public void Union(Rectangle64 rectangle)
    {
        long left = Math.Min(this.Left, rectangle.Left);
        long top = Math.Min(this.Top, rectangle.Top);
        long right = Math.Max(this.Right, rectangle.Right);
        long bottom = Math.Max(this.Bottom, rectangle.Bottom);

        this.x = left;
        this.y = top;
        this.width = right - left;
        this.height = bottom - top;
    }

    /// <summary>
    /// Updates the current rectangle to be the intersection of itself and a given input rectangle.
    /// </summary>
    /// <param name="rectangle">The input <see cref="Rectangle64"/> instance to compute the intersection with.</param>
    public void Intersect(Rectangle64 rectangle)
    {
        long maxLeft = Math.Max(this.x, rectangle.x);
        long maxTop = Math.Max(this.y, rectangle.y);
        long width = Math.Max(Math.Min(this.Right, rectangle.Right) - maxLeft, 0);
        long height = Math.Max(Math.Min(this.Bottom, rectangle.Bottom) - maxTop, 0);

        this.x = maxLeft;
        this.y = maxTop;
        this.width = width;
        this.height = height;
    }

    /// <summary>
    /// Inflates the current rectangle by a specified amount.
    /// </summary>
    /// <param name="x">The horizontal inflation amount.</param>
    /// <param name="y">The vertical inflation amount.</param>
    public void Inflate(long x, long y)
    {
        this.x -= x;
        this.y -= y;
        this.width += (2 * x);
        this.height += (2 * y);
    }

    /// <summary>
    /// Inflates the current rectangle by a specified amount.
    /// </summary>
    /// <param name="left">The amount fo use to inflate the left edge.</param>
    /// <param name="top"></param>
    /// <param name="right"></param>
    /// <param name="bottom"></param>
    public void Inflate(long left, long top, long right, long bottom)
    {
        this.x -= left;
        this.y -= top;
        this.width += left + right;
        this.height += top + bottom;
    }

    /// <summary>
    /// Creates a new <see cref="Rectangle"/> from the current <see cref="Rectangle64"/> instance, clamping to a D2D1 logically infinite rectangle.
    /// </summary>
    /// <returns>The <see cref="Rectangle"/> equivalent of the current instance.</returns>
    public Rectangle ToRectangleWithD2D1LogicallyInfiniteClamping()
    {
        Intersect(new Rectangle64(int.MinValue / 2, int.MinValue / 2, int.MaxValue, int.MaxValue));

        return new((int)this.x, (int)this.y, (int)this.width, (int)this.height);
    }

    /// <inheritdoc/>
    public readonly bool Equals(Rectangle64 other)
    {
        return this.x == other.x && this.y == other.y && this.width == other.width && this.height == other.height;
    }

    /// <summary>
    /// Creates a new <see cref="Rectangle64"/> instance from an input <see cref="Rectangle"/> value.
    /// </summary>
    /// <param name="rectangle">The input <see cref="Rectangle"/> value.</param>
    /// <returns>The resulting <see cref="Rectangle64"/> instance representing <paramref name="rectangle"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle64 FromRectangle(Rectangle rectangle)
    {
        return new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
    }
}