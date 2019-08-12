using System.Diagnostics.Contracts;
using System.Numerics;

namespace Windows.Foundation
{
    /// <summary>
    /// A <see langword="class"/> with some extension methods for the <see cref="Rect"/> type
    /// </summary>
    public static class RectExtensions
    {
        /// <summary>
        /// Converts a <see cref="Rect"/> value to a <see cref="Vector2"/>
        /// </summary>
        /// <param name="value">The input <see cref="Rect"/> to convert</param>
        /// <returns>A <see cref="Vector2"/> value representing the input <see cref="Rect"/></returns>
        /// <remarks>The input <see cref="Rect"/> is read in the following order: <see cref="Rect.Left"/>, <see cref="Rect.Top"/>, <see cref="Rect.Right"/>, <see cref="Rect.Bottom"/></remarks>
        [Pure]
        public static Vector4 ToVector2(this in Rect value) => new Vector4((float)value.Left, (float)value.Top, (float)value.Right, (float)value.Bottom);

        /// <summary>
        /// Converts a <see cref="Vector2"/> value to a <see cref="Rect"/>
        /// </summary>
        /// <param name="value">The input <see cref="Vector2"/> to convert</param>
        /// <returns>A <see cref="Rect"/> value representing the input <see cref="Vector2"/></returns>
        /// <remarks>The input <see cref="Vector4"/> is assigned in the following order: <see cref="Rect.Left"/>, <see cref="Rect.Top"/>, <see cref="Rect.Right"/>, <see cref="Rect.Bottom"/></remarks>
        [Pure]
        public static Rect ToPoint(this in Vector4 value) => new Rect(value.X, value.Y, value.Z, value.W);
    }
}