using System.Diagnostics.Contracts;
using ComputeSharp;
using static System.Runtime.InteropServices.MemoryMarshal;

namespace System.Numerics
{
    /// <summary>
    /// A <see langword="class"/> that contains extension methods for the <see cref="Float2"/>, <see cref="Float3"/> and <see cref="Float4"/> types.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Converts a <see cref="Float2"/> array into a <see cref="Span{T}"/> with items of type <see cref="Vector2"/>.
        /// </summary>
        /// <param name="array">The input <see cref="Float2"/> array.</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector2"/>, mapping the original array.</returns>
        [Pure]
        public static Span<Vector2> AsVector2Span(this Float2[] array) => Cast<Float2, Vector2>(array);

        /// <summary>
        /// Converts a <see cref="Float2"/> <see cref="ReadOnlySpan{T}"/> into a <see cref="ReadOnlySpan{T}"/> with items of type <see cref="Vector2"/>.
        /// </summary>
        /// <param name="span">The input <see cref="Float2"/> <see cref="ReadOnlySpan{T}"/>.</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector2"/>, mapping the original <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadOnlySpan<Vector2> AsVector2Span(this ReadOnlySpan<Float2> span) => Cast<Float2, Vector2>(span);

        /// <summary>
        /// Converts a <see cref="Float2"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Vector2"/>.
        /// </summary>
        /// <param name="span">The input <see cref="Float2"/> <see cref="Span{T}"/>.</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector2"/>, mapping the original <see cref="Span{T}"/>.</returns>
        [Pure]
        public static Span<Vector2> AsVector2Span(this Span<Float2> span) => Cast<Float2, Vector2>(span);

        /// <summary>
        /// Converts a <see cref="Float3"/> array into a <see cref="Span{T}"/> with items of type <see cref="Vector3"/>.
        /// </summary>
        /// <param name="array">The input <see cref="Float3"/> array.</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector3"/>, mapping the original array.</returns>
        [Pure]
        public static Span<Vector3> AsVector3Span(this Float3[] array) => Cast<Float3, Vector3>(array);

        /// <summary>
        /// Converts a <see cref="Float3"/> <see cref="ReadOnlySpan{T}"/> into a <see cref="ReadOnlySpan{T}"/> with items of type <see cref="Vector3"/>.
        /// </summary>
        /// <param name="span">The input <see cref="Float3"/> <see cref="ReadOnlySpan{T}"/>.</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector3"/>, mapping the original <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadOnlySpan<Vector3> AsVector3Span(this ReadOnlySpan<Float3> span) => Cast<Float3, Vector3>(span);

        /// <summary>
        /// Converts a <see cref="Float3"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Vector3"/>.
        /// </summary>
        /// <param name="span">The input <see cref="Float3"/> <see cref="Span{T}"/>.</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector3"/>, mapping the original <see cref="Span{T}"/>.</returns>
        [Pure]
        public static Span<Vector3> AsVector3Span(this Span<Float3> span) => Cast<Float3, Vector3>(span);

        /// <summary>
        /// Converts a <see cref="Float4"/> array into a <see cref="Span{T}"/> with items of type <see cref="Vector4"/>.
        /// </summary>
        /// <param name="array">The input <see cref="Float4"/> array.</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector4"/>, mapping the original array.</returns>
        [Pure]
        public static Span<Vector4> AsVector4Span(this Float4[] array) => Cast<Float4, Vector4>(array);

        /// <summary>
        /// Converts a <see cref="Vector4"/> <see cref="ReadOnlySpan{T}"/> into a <see cref="ReadOnlySpan{T}"/> with items of type <see cref="Vector4"/>.
        /// </summary>
        /// <param name="span">The input <see cref="Vector4"/> <see cref="ReadOnlySpan{T}"/>.</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector4"/>, mapping the original <see cref="ReadOnlySpan{T}"/>.</returns>
        [Pure]
        public static ReadOnlySpan<Vector4> AsVector4Span(this ReadOnlySpan<Float4> span) => Cast<Float4, Vector4>(span);

        /// <summary>
        /// Converts a <see cref="Vector4"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Vector4"/>.
        /// </summary>
        /// <param name="span">The input <see cref="Vector4"/> <see cref="Span{T}"/>.</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Vector4"/>, mapping the original <see cref="Span{T}"/>.</returns>
        [Pure]
        public static Span<Vector4> AsVector4Span(this Span<Float4> span) => Cast<Float4, Vector4>(span);
    }
}
