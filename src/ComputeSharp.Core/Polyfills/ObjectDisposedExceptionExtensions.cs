using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System;

/// <summary>
/// Throw helper extensions for <see cref="ObjectDisposedException"/>.
/// </summary>
internal static class ObjectDisposedExceptionExtensions
{
    /// <summary>
    /// Throws an <see cref="ObjectDisposedException"/> if <paramref name="condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="condition">The condition indicating whether <paramref name="disposableInstance"/> has been disposed.</param>
    /// <param name="disposableInstance">The instance that can be signaled as being disposed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIf(this ObjectDisposedException? _, [DoesNotReturnIf(true)] bool condition, object disposableInstance)
    {
        if (condition)
        {
            Throw(disposableInstance);
        }
    }

    /// <summary>
    /// Throws an <see cref="ObjectDisposedException"/> if <paramref name="validationObject"/> is <see langword="null"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="validationObject">The instance to check to determine whether the target instance has been disposed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNull(this ObjectDisposedException? _, [NotNull] object? validationObject)
    {
        if (validationObject is null)
        {
            Throw();
        }
    }

    /// <summary>
    /// Throws an <see cref="ObjectDisposedException"/>.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown with no parameters.</exception>
    [DoesNotReturn]
    private static void Throw()
    {
        throw new ObjectDisposedException(null);
    }

    /// <summary>
    /// Throws an <see cref="ObjectDisposedException"/> for a given target instance.
    /// </summary>
    /// <param name="targetObject">The target instance that was disposed.</param>
    /// <exception cref="ObjectDisposedException">Thrown for <paramref name="targetObject"/>.</exception>
    [DoesNotReturn]
    private static void Throw(object targetObject)
    {
        throw new ObjectDisposedException(targetObject.ToString());
    }
}