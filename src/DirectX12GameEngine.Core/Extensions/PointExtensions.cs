using System.Diagnostics.Contracts;
using System.Numerics;

namespace Windows.Foundation
{
    /// <summary>
    /// A <see langword="class"/> with some extension methods for the <see cref="Point"/> type
    /// </summary>
    public static class PointExtensions
    {
        /// <summary>
        /// Converts a <see cref="Point"/> value to a <see cref="Vector2"/>
        /// </summary>
        /// <param name="value">The input <see cref="Point"/> to convert</param>
        /// <returns>A <see cref="Vector2"/> value representing the input <see cref="Point"/></returns>
        [Pure]
        public static Vector2 ToVector2(this in Point value) => new Vector2((float)value.X, (float)value.Y);

        /// <summary>
        /// Converts a <see cref="Vector2"/> value to a <see cref="Point"/>
        /// </summary>
        /// <param name="value">The input <see cref="Vector2"/> to convert</param>
        /// <returns>A <see cref="Point"/> value representing the input <see cref="Vector2"/></returns>
        [Pure]
        public static Point ToPoint(this in Vector2 value) => new Point(value.X, value.Y);
    }
}
