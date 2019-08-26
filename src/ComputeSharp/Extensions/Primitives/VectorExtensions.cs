using System;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that contains extension methods for the <see cref="Vector2"/>, <see cref="Vector3"/> and <see cref="Vector4"/> types
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Converts a <see cref="Vector2"/> array into a <see cref="Span{T}"/> with items of type <see cref="Float2"/>
        /// </summary>
        /// <param name="array">The input <see cref="Vector2"/> array</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float2"/>, mapping the original array</returns>
        [Pure]
        public static Span<Float2> AsFloat2Span(this Vector2[] array) => array.AsSpan().As<Vector2, Float2>();

        /// <summary>
        /// Converts a <see cref="Vector2"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Float2"/>
        /// </summary>
        /// <param name="span">The input <see cref="Vector2"/> <see cref="Span{T}"/></param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float2"/>, mapping the original <see cref="Span{T}"/></returns>
        [Pure]
        public static Span<Float2> AsFloat2Span(this Span<Vector2> span) => span.As<Vector2, Float2>();

        /// <summary>
        /// Converts a <see cref="Vector3"/> array into a <see cref="Span{T}"/> with items of type <see cref="Float3"/>
        /// </summary>
        /// <param name="array">The input <see cref="Vector3"/> array</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float3"/>, mapping the original array</returns>
        [Pure]
        public static Span<Float3> AsFloat3Span(this Vector3[] array) => array.AsSpan().As<Vector3, Float3>();

        /// <summary>
        /// Converts a <see cref="Vector3"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Float3"/>
        /// </summary>
        /// <param name="span">The input <see cref="Vector3"/> <see cref="Span{T}"/></param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float3"/>, mapping the original <see cref="Span{T}"/></returns>
        [Pure]
        public static Span<Float3> AsFloat3Span(this Span<Vector3> span) => span.As<Vector3, Float3>();

        /// <summary>
        /// Converts a <see cref="Vector4"/> array into a <see cref="Span{T}"/> with items of type <see cref="Float4"/>
        /// </summary>
        /// <param name="array">The input <see cref="Vector4"/> array</param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float4"/>, mapping the original array</returns>
        [Pure]
        public static Span<Float4> AsFloat4Span(this Vector4[] array) => array.AsSpan().As<Vector4, Float4>();

        /// <summary>
        /// Converts a <see cref="Vector4"/> <see cref="Span{T}"/> into a <see cref="Span{T}"/> with items of type <see cref="Float4"/>
        /// </summary>
        /// <param name="span">The input <see cref="Vector4"/> <see cref="Span{T}"/></param>
        /// <returns>A <see cref="Span{T}"/> with items of type <see cref="Float4"/>, mapping the original <see cref="Span{T}"/></returns>
        [Pure]
        public static Span<Float4> AsFloat4Span(this Span<Vector4> span) => span.As<Vector4, Float4>();

        /// <summary>
        /// Converts a <see cref="Span{T}"/> value with items of type <typeparamref name="TFrom"/> to one with items of type <typeparamref name="TTo"/>
        /// </summary>
        /// <typeparam name="TFrom">The type of the input items</typeparam>
        /// <typeparam name="TTo">The type of items to convert to</typeparam>
        /// <param name="span">The input <see cref="Span{T}"/></param>
        /// <returns>A <see cref="Span{T}"/> with items of type <typeparamref name="TTo"/></returns>
        [Pure]
        private static Span<TTo> As<TFrom, TTo>(this Span<TFrom> span)
            where TFrom : unmanaged
            where TTo : unmanaged
        {

            if (span.Length == 0) return Span<TTo>.Empty;

            ref TTo r0 = ref Unsafe.As<TFrom, TTo>(ref span.GetPinnableReference());
            return MemoryMarshal.CreateSpan(ref r0, span.Length);
        }
    }
}
