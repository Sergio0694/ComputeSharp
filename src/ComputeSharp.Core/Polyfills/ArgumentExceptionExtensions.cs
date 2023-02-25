using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System;

/// <summary>
/// Throw helper extensions for <see cref="ArgumentException"/>.
/// </summary>
internal static class ArgumentExceptionExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if <paramref name="condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="condition">The condition to decide whether to throw the exception.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="condition"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIf(this ArgumentException? _, bool condition, string? parameterName = null)
    {
        if (condition)
        {
            Throw(parameterName);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException"/> for a given parameter name.
    /// </summary>
    /// <param name="parameterName">The name of the parameter to report in the exception.</param>
    /// <exception cref="ArgumentException">Thrown with <paramref name="parameterName"/>.</exception>
    [DoesNotReturn]
    private static void Throw(string? parameterName)
    {
        throw new ArgumentException(parameterName);
    }
}
