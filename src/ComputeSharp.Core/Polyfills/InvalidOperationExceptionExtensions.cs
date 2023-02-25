using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System;

/// <summary>
/// Throw helper extensions for <see cref="InvalidOperationException"/>.
/// </summary>
internal static class InvalidOperationExceptionExtensions
{
    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> for a given parameter name.
    /// </summary>
    /// <typeparam name="T">The type to let the compiler assume it returns.</typeparam>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <returns>This method never returns any values.</returns>
    /// <exception cref="InvalidOperationException">Thrown with no parameters.</exception>
    [DoesNotReturn]
    public static T Throw<T>(this InvalidOperationException? _)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if <paramref name="condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="condition">The condition to decide whether to throw the exception.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIf(this InvalidOperationException? _, [DoesNotReturnIf(true)] bool condition)
    {
        if (condition)
        {
            Throw();
        }
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> for a given parameter name.
    /// </summary>
    /// <exception cref="InvalidOperationException">Always thrown with no parameters.</exception>
    [DoesNotReturn]
    private static void Throw()
    {
        throw new InvalidOperationException();
    }
}
