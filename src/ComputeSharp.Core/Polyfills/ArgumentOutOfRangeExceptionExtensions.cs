using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System;

/// <summary>
/// Throw helper extensions for <see cref="ArgumentOutOfRangeException"/>.
/// </summary>
internal static class ArgumentOutOfRangeExceptionExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is not zero.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="value">The argument to validate as zero.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNotZero(this ArgumentOutOfRangeException? _, int value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value != 0)
        {
            Throw(parameterName, value);
        }
    }

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
            Throw(parameterName, value);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="value">The argument to validate as non-zero or non-negative.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNegativeOrZero(this ArgumentOutOfRangeException? _, int value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value <= 0)
        {
            Throw(parameterName, value);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than <paramref name="other"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="value">The argument to validate as less or equal than <paramref name="other"/>.</param>
    /// <param name="other">The value to compare with <paramref name="value"/>.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfGreaterThan(this ArgumentOutOfRangeException? _, int value, int other, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value > other)
        {
            Throw(parameterName, value);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than or equal to <paramref name="other"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="value">The argument to validate as less or equal than <paramref name="other"/>.</param>
    /// <param name="other">The value to compare with <paramref name="value"/>.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfGreaterThanOrEqual(this ArgumentOutOfRangeException? _, int value, int other, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value >= other)
        {
            Throw(parameterName, value);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is less than <paramref name="other"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="value">The argument to validate as less or equal than <paramref name="other"/>.</param>
    /// <param name="other">The value to compare with <paramref name="value"/>.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(this ArgumentOutOfRangeException? _, int value, int other, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value < other)
        {
            Throw(parameterName, value);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is less than <paramref name="other"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="value">The argument to validate as less or equal than <paramref name="other"/>.</param>
    /// <param name="other">The value to compare with <paramref name="value"/>.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfLessThan(this ArgumentOutOfRangeException? _, long value, long other, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value < other)
        {
            Throw(parameterName, value);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is not in the specified range.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="value">The argument to validate as non-zero or non-negative.</param>
    /// <param name="minimum">The inclusive starting value for the range.</param>
    /// <param name="maximum">The exclusive ending value for the range.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNotInRange(this ArgumentOutOfRangeException? _, int value, int minimum, int maximum, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value < minimum || value >= maximum)
        {
            Throw(parameterName, value);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is not equal to a given other value.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="value">The argument to validate.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNotEqual(this ArgumentOutOfRangeException? _, int value, int other, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value != other)
        {
            Throw(parameterName, value);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is not in the specified range.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="value">The argument to validate as non-zero or non-negative.</param>
    /// <param name="minimum">The inclusive starting value for the range.</param>
    /// <param name="maximum">The inclusive ending value for the range.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNotBetweenOrEqual(this ArgumentOutOfRangeException? _, int value, int minimum, int maximum, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value < minimum || value > maximum)
        {
            Throw(parameterName, value);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is not in the specified range.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="value">The argument to validate as non-zero or non-negative.</param>
    /// <param name="minimum">The inclusive starting value for the range.</param>
    /// <param name="maximum">The inclusive ending value for the range.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNotBetweenOrEqual(this ArgumentOutOfRangeException? _, long value, long minimum, long maximum, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value < minimum || value > maximum)
        {
            Throw(parameterName, value);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> for a given parameter name.
    /// </summary>
    /// <param name="parameterName">The name of the parameter to report in the exception.</param>
    /// <param name="value">The invalid value that was used.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown with <paramref name="parameterName"/>.</exception>
    [DoesNotReturn]
    private static void Throw(string? parameterName, int value)
    {
        throw new ArgumentOutOfRangeException(parameterName, value, null);
    }

    /// <inheritdoc cref="Throw(string?, int)"/>
    [DoesNotReturn]
    private static void Throw(string? parameterName, long value)
    {
        throw new ArgumentOutOfRangeException(parameterName, value, null);
    }
}