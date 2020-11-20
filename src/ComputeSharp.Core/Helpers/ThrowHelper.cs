using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace ComputeSharp.Core.Helpers
{
    /// <summary>
    /// Helper methods to efficiently throw exceptions.
    /// </summary>
    internal static partial class ThrowHelper
    {
        /// <summary>
        /// Throws a new <see cref="ArgumentException"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown with no parameters.</exception>
        [DoesNotReturn]
        public static void ThrowArgumentException()
        {
            throw new ArgumentException();
        }

        /// <summary>
        /// Throws a new <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="ArgumentException">Thrown with the specified parameter.</exception>
        [DoesNotReturn]
        public static void ThrowArgumentException(string message)
        {
            throw new ArgumentException(message);
        }

        /// <summary>
        /// Throws a new <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="InvalidOperationException">Thrown with the specified parameter.</exception>
        [DoesNotReturn]
        public static void ThrowInvalidOperationException(string message)
        {
            throw new InvalidOperationException(message);
        }

        /// <summary>
        /// Throws a new <see cref="Win32Exception"/>.
        /// </summary>
        /// <param name="error">The Win32 error code associated with this exception.</param>
        /// <exception cref="Win32Exception">Thrown with the specified parameter.</exception>
        [DoesNotReturn]
        public static void ThrowWin32Exception(int error)
        {
            throw new Win32Exception(error);
        }

        /// <summary>
        /// Throws a new <see cref="ArgumentException"/>.
        /// </summary>
        /// <typeparam name="T">The type of expected result.</typeparam>
        /// <exception cref="ArgumentException">Thrown with no parameters.</exception>
        /// <returns>This method always throws, so it actually never returns a value.</returns>
        [DoesNotReturn]
        public static T ThrowArgumentException<T>()
        {
            throw new ArgumentException();
        }

        /// <summary>
        /// Throws a new <see cref="ArgumentException"/>.
        /// </summary>
        /// <typeparam name="T">The type of expected result.</typeparam>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="ArgumentException">Thrown with the specified parameter.</exception>
        /// <returns>This method always throws, so it actually never returns a value.</returns>
        [DoesNotReturn]
        public static T ThrowArgumentException<T>(string message)
        {
            throw new ArgumentException(message);
        }

        /// <summary>
        /// Throws a new <see cref="NotSupportedException"/>.
        /// </summary>
        /// <typeparam name="T">The type of expected result.</typeparam>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="NotSupportedException">Thrown with the specified parameter.</exception>
        /// <returns>This method always throws, so it actually never returns a value.</returns>
        [DoesNotReturn]
        public static T ThrowNotSupportedException<T>(string message)
        {
            throw new NotSupportedException(message);
        }
    }
}