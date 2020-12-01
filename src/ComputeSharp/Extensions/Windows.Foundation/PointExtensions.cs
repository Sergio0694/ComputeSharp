using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Windows.Foundation
{
    /// <summary>
    /// A <see langword="class"/> with some extension methods for the <see cref="Point"/> type.
    /// </summary>
    public static class PointExtensions
    {
        /// <summary>
        /// Converts a <see cref="Point"/> value to a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value">The input <see cref="Point"/> to convert.</param>
        /// <returns>A <see cref="Vector2"/> value representing the input <see cref="Point"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ToVector2(this Point value) => new((float)value.X, (float)value.Y);

        /// <summary>
        /// Converts a <see cref="Vector2"/> value to a <see cref="Point"/>.
        /// </summary>
        /// <param name="value">The input <see cref="Vector2"/> to convert.</param>
        /// <returns>A <see cref="Point"/> value representing the input <see cref="Vector2"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point ToPoint(this Vector2 value) => new(value.X, value.Y);
    }
}
