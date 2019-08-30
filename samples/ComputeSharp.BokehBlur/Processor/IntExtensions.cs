using System.Diagnostics.Contracts;

namespace System
{
    /// <summary>
    /// A <see langword="class"/> with some extension methods for the <see cref="int"/> type
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Clamps a number in a specified interval
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">The minimum value to return</param>
        /// <param name="max">The maximum value to return</param>
        [Pure]
        public static int Clamp(this int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
