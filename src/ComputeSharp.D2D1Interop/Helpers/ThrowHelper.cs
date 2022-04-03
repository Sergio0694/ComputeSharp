using System;
using System.Diagnostics.CodeAnalysis;

namespace ComputeSharp.D2D1Interop.Helpers;

/// <summary>
/// Helper methods to efficiently throw exceptions.
/// </summary>
internal static partial class ThrowHelper
{
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