using System.Diagnostics.Contracts;
using System.Numerics;

namespace Windows.Foundation
{
    /// <summary>
    /// A <see langword="class"/> with some extension methods for the <see cref="Size"/> type
    /// </summary>
    public static class SizeExtensions
    {
        /// <summary>
        /// Converts a <see cref="Size"/> value to a <see cref="Vector2"/>
        /// </summary>
        /// <param name="value">The input <see cref="Size"/> to convert</param>
        /// <returns>A <see cref="Vector2"/> value representing the input <see cref="Size"/></returns>
        /// <remarks>The input <see cref="Size"/> is read in the following order: <see cref="Size.Width"/>, <see cref="Size.Height"/></remarks>
        [Pure]
        public static Vector2 ToVector2(this in Size value) => new Vector2((float)value.Width, (float)value.Height);

        /// <summary>
        /// Converts a <see cref="Vector2"/> value to a <see cref="Size"/>
        /// </summary>
        /// <param name="value">The input <see cref="Vector2"/> to convert</param>
        /// <returns>A <see cref="Point"/> value representing the input <see cref="Vector2"/></returns>
        /// <remarks>The input <see cref="Size"/> is assigned in the following order: <see cref="Size.Width"/>, <see cref="Size.Height"/></remarks>
        [Pure]
        public static Size ToPoint(this in Vector2 value) => new Size(value.X, value.Y);
    }
}