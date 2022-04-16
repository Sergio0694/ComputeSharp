using System;
using System.Diagnostics.CodeAnalysis;

namespace ComputeSharp.D2D1.Helpers;

/// <summary>
/// Helper methods to efficiently throw exceptions.
/// </summary>
internal static partial class ThrowHelper
{
    /// <summary>
    /// Throws a new <see cref="ArgumentException"/>.
    /// </summary>
    /// <typeparam name="T">The type of expected result.</typeparam>
    /// <param name="name">The argument name.</param>
    /// <param name="message">The message to include in the exception.</param>
    /// <exception cref="ArgumentException">Thrown with the specified parameters.</exception>
    /// <returns>This method always throws, so it actually never returns a value.</returns>
    [DoesNotReturn]
    public static T ThrowArgumentException<T>(string? name, string? message)
    {
        throw new ArgumentException(message, name);
    }

    /// <summary>
    /// Throws a new <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    /// <param name="name">The argument name.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown with the specified parameter.</exception>
    [DoesNotReturn]
    public static void ThrowArgumentOutOfRangeException(string? name)
    {
        throw new ArgumentOutOfRangeException(name);
    }

    /// <summary>
    /// Throws a new <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <param name="message">The message to include in the exception.</param>
    /// <exception cref="InvalidOperationException">Thrown with the specified parameter.</exception>
    [DoesNotReturn]
    public static void ThrowInvalidOperationException(string? message)
    {
        throw new InvalidOperationException(message);
    }

    /// <summary>
    /// Throws a new <see cref="NotSupportedException"/>.
    /// </summary>
    /// <param name="message">The message to include in the exception.</param>
    /// <exception cref="NotSupportedException">Thrown with the specified parameter.</exception>
    [DoesNotReturn]
    public static void ThrowNotSupportedException(string? message)
    {
        throw new NotSupportedException(message);
    }
}