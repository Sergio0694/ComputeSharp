using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="Float2"/>, <see cref="Float3"/> and <see cref="Float4"/> types.
/// </summary>
public static class FloatExtensions
{
    /// <summary>
    /// Converts a <see cref="Float2"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Vector2"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Float2"/> <see cref="Span{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector2"/>, mapping the original <see cref="Span{T}"/>.</returns>
    public static Span<Vector2> AsVector2(this Span<Float2> span)
    {
        return MemoryMarshal.Cast<Float2, Vector2>(span);
    }

    /// <summary>
    /// Converts a <see cref="Float2"/> <see cref="ReadOnlySpan{T}"/> into a <see cref="ReadOnlySpan{T}"/> with items of type <see cref="Vector2"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Float2"/> <see cref="ReadOnlySpan{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector2"/>, mapping the original <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlySpan<Vector2> AsVector2(this ReadOnlySpan<Float2> span)
    {
        return MemoryMarshal.Cast<Float2, Vector2>(span);
    }

    /// <summary>
    /// Converts a <see cref="Float3"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Vector3"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Float3"/> <see cref="Span{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector3"/>, mapping the original <see cref="Span{T}"/>.</returns>
    public static Span<Vector3> AsVector3(this Span<Float3> span)
    {
        return MemoryMarshal.Cast<Float3, Vector3>(span);
    }

    /// <summary>
    /// Converts a <see cref="Float3"/> <see cref="ReadOnlySpan{T}"/> into a <see cref="ReadOnlySpan{T}"/> with items of type <see cref="Vector3"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Float3"/> <see cref="ReadOnlySpan{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector3"/>, mapping the original <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlySpan<Vector3> AsVector3(this ReadOnlySpan<Float3> span)
    {
        return MemoryMarshal.Cast<Float3, Vector3>(span);
    }

    /// <summary>
    /// Converts a <see cref="Vector4"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Vector4"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Vector4"/> <see cref="Span{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector4"/>, mapping the original <see cref="Span{T}"/>.</returns>
    public static Span<Vector4> AsVector4(this Span<Float4> span)
    {
        return MemoryMarshal.Cast<Float4, Vector4>(span);
    }

    /// <summary>
    /// Converts a <see cref="Vector4"/> <see cref="ReadOnlySpan{T}"/> into a <see cref="ReadOnlySpan{T}"/> with items of type <see cref="Vector4"/>.
    /// </summary>
    /// <param name="span">The input <see cref="Vector4"/> <see cref="ReadOnlySpan{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector4"/>, mapping the original <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlySpan<Vector4> AsVector4(this ReadOnlySpan<Float4> span)
    {
        return MemoryMarshal.Cast<Float4, Vector4>(span);
    }
}