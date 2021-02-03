using System;

namespace ComputeSharp.Resources.Views
{
    /// <summary>
    /// A helper class to throw exceptions for view types.
    /// </summary>
    internal static class ThrowHelper
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> when the target span is too short.
        /// </summary>
        public static void ThrowArgumentExceptionForDestinationTooShort()
        {
            throw new ArgumentException("The target span is too short to copy all the current items to");
        }

        /// <summary>
        /// Throws an <see cref="IndexOutOfRangeException"/> when the a given coordinate is invalid.
        /// </summary>
        /// <remarks>
        /// Throwing <see cref="IndexOutOfRangeException"/> is technically discouraged in the docs, but
        /// we're doing that here for consistency with the official <see cref="Span{T}"/> type(s) from the BCL.
        /// </remarks>
        public static void ThrowIndexOutOfRangeException()
        {
            throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> when more than one parameter are invalid.
        /// </summary>
        public static void ThrowArgumentException()
        {
            throw new ArgumentException("One or more input parameters were invalid");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> when the "row" parameter is invalid.
        /// </summary>
        public static void ThrowArgumentOutOfRangeExceptionForRow()
        {
            throw new ArgumentOutOfRangeException("row");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> when the "column" parameter is invalid.
        /// </summary>
        public static void ThrowArgumentOutOfRangeExceptionForColumn()
        {
            throw new ArgumentOutOfRangeException("column");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> when the "height" parameter is invalid.
        /// </summary>
        public static void ThrowArgumentOutOfRangeExceptionForHeight()
        {
            throw new ArgumentOutOfRangeException("height");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> when the "width" parameter is invalid.
        /// </summary>
        public static void ThrowArgumentOutOfRangeExceptionForWidth()
        {
            throw new ArgumentOutOfRangeException("width");
        }
    }
}
