using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="Vector2"/>, <see cref="Vector3"/> and <see cref="Vector4"/> types.
/// </summary>
public static class VectorExtensions
{
    /// <summary>
    /// Converts a <see cref="Vector2"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Float2"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Vector2"/> <see cref="Span{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float2"/>, mapping the original <see cref="Span{T}"/>.</returns>
    public static Span<Float2> AsFloat2(this Span<Vector2> span)
    {
        return MemoryMarshal.Cast<Vector2, Float2>(span);
    }

    /// <summary>
    /// Converts a <see cref="Vector2"/> <see cref="ReadOnlySpan{T}"/> into a <see cref="ReadOnlySpan{T}"/> with items of type <see cref="Float2"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Vector2"/> <see cref="ReadOnlySpan{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float2"/>, mapping the original <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlySpan<Float2> AsFloat2(this ReadOnlySpan<Vector2> span)
    {
        return MemoryMarshal.Cast<Vector2, Float2>(span);
    }

    /// <summary>
    /// Converts a <see cref="Vector3"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Float3"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Vector3"/> <see cref="Span{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float3"/>, mapping the original <see cref="Span{T}"/>.</returns>
    public static Span<Float3> AsFloat3(this Span<Vector3> span)
    {
        return MemoryMarshal.Cast<Vector3, Float3>(span);
    }

    /// <summary>
    /// Converts a <see cref="Vector3"/> <see cref="ReadOnlySpan{T}"/> into a <see cref="ReadOnlySpan{T}"/> with items of type <see cref="Float3"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Vector3"/> <see cref="ReadOnlySpan{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float3"/>, mapping the original <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlySpan<Float3> AsFloat3(this ReadOnlySpan<Vector3> span)
    {
        return MemoryMarshal.Cast<Vector3, Float3>(span);
    }

    /// <summary>
    /// Converts a <see cref="Vector4"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Float4"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Vector4"/> <see cref="Span{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float4"/>, mapping the original <see cref="Span{T}"/>.</returns>
    public static Span<Float4> AsFloat4(this Span<Vector4> span)
    {
        return MemoryMarshal.Cast<Vector4, Float4>(span);
    }

    /// <summary>
    /// Converts a <see cref="Vector4"/> <see cref="ReadOnlySpan{T}"/> into a <see cref="ReadOnlySpan{T}"/> with items of type <see cref="Float4"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Vector4"/> <see cref="ReadOnlySpan{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float4"/>, mapping the original <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlySpan<Float4> AsFloat4(this ReadOnlySpan<Vector4> span)
    {
        return MemoryMarshal.Cast<Vector4, Float4>(span);
    }
}