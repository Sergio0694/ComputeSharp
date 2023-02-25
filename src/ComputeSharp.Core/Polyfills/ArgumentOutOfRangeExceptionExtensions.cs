using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System;

/// <summary>
/// Throw helper extensions for <see cref="ArgumentOutOfRangeException"/>.
/// </summary>
internal static class ArgumentOutOfRangeExceptionExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="value">The argument to validate as non-negative.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNegative(this ArgumentOutOfRangeException? _, int value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value < 0)
        {
            Throw(parameterName);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> for a given parameter name.
    /// </summary>
    /// <param name="parameterName">The name of the parameter to report in the exception.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown with <paramref name="parameterName"/>.</exception>
    [DoesNotReturn]
    private static void Throw(string? parameterName)
    {
        throw new ArgumentOutOfRangeException(parameterName);
    }
}
