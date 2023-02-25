using System.Diagnostics.CodeAnalysis;

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
}
