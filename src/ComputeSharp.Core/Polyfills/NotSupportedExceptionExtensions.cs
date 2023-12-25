using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System;

/// <summary>
/// Throw helper extensions for <see cref="NotSupportedException"/>.
/// </summary>
internal static class NotSupportedExceptionExtensions
{
    /// <summary>
    /// Throws an <see cref="NotSupportedException"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <exception cref="NotSupportedException">Thrown with no parameters.</exception>
    [DoesNotReturn]
    public static void Throw(this NotSupportedException? _)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Throws an <see cref="NotSupportedException"/> for a given parameter name.
    /// </summary>
    /// <typeparam name="T">The type to let the compiler assume it returns.</typeparam>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <returns>This method never returns any values.</returns>
    /// <exception cref="NotSupportedException">Thrown with no parameters.</exception>
    [DoesNotReturn]
    public static T Throw<T>(this NotSupportedException? _)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Throws an <see cref="NotSupportedException"/> if <paramref name="condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="condition">The condition to decide whether to throw the exception.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIf(this NotSupportedException? _, [DoesNotReturnIf(true)] bool condition)
    {
        if (condition)
        {
            Throw();
        }
    }

    /// <summary>
    /// Throws an <see cref="NotSupportedException"/> for a given parameter name.
    /// </summary>
    /// <exception cref="NotSupportedException">Always thrown with no parameters.</exception>
    [DoesNotReturn]
    private static void Throw()
    {
        throw new NotSupportedException();
    }
}