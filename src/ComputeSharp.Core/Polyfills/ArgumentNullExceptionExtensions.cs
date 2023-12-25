using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System;

/// <summary>
/// Throw helper extensions for <see cref="ArgumentNullException"/>.
/// </summary>
internal static class ArgumentNullExceptionExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> for a given parameter name.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="parameterName">The name of the parameter to report in the exception.</param>
    /// <exception cref="ArgumentNullException">Thrown with <paramref name="parameterName"/>.</exception>
    [DoesNotReturn]
    public static void Throw(this ArgumentNullException? _, string? parameterName)
    {
        throw new ArgumentNullException(parameterName);
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if <paramref name="argument"/> is <see langword="null"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="argument">The reference type argument to validate as non-<see langword="null"/>.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="argument"/> is <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNull(this ArgumentNullException? _, [NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? parameterName = null)
    {
        if (argument is null)
        {
            Throw(parameterName);
        }
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if <paramref name="argument"/> is <see langword="null"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="argument">The pointer argument to validate as non-<see langword="null"/>.</param>
    /// <param name="parameterName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="argument"/> is <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void ThrowIfNull(this ArgumentNullException? _, [NotNull] void* argument, [CallerArgumentExpression(nameof(argument))] string? parameterName = null)
    {
        if (argument is null)
        {
            Throw(parameterName);
        }
    }

    /// <inheritdoc cref="Throw(ArgumentNullException?, string?)"/>
    [DoesNotReturn]
    private static void Throw(string? parameterName)
    {
        throw new ArgumentNullException(parameterName);
    }
}