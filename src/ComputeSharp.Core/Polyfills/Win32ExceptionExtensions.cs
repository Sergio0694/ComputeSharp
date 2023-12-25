using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.ComponentModel;

/// <summary>
/// Throw helper extensions for <see cref="Win32Exception"/>.
/// </summary>
internal static class Win32ExceptionExtensions
{
    /// <summary>
    /// Throws an <see cref="Win32Exception"/> for a given error code.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="hresult">The error code to report.</param>
    [DoesNotReturn]
    public static void Throw(this Win32Exception? _, int hresult)
    {
        throw new Win32Exception(hresult);
    }

    /// <summary>
    /// Throws an <see cref="Win32Exception"/> if <paramref name="condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="condition">The condition to decide whether to throw the exception.</param>
    /// <param name="hresult">The error code to report.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIf(this Win32Exception? _, [DoesNotReturnIf(true)] bool condition, int hresult)
    {
        if (condition)
        {
            Throw(hresult);
        }
    }

    /// <summary>
    /// Throws an <see cref="Win32Exception"/> if a given error code represents an error.
    /// </summary>
    /// <param name="_">Dummy value to invoke the extension upon (always pass <see langword="null"/>.</param>
    /// <param name="hresult">The error code to check.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfFailed(this Win32Exception? _, int hresult)
    {
        if (hresult < 0)
        {
            Throw(hresult);
        }
    }

    /// <summary>
    /// Throws an <see cref="Win32Exception"/> for a given error code.
    /// </summary>
    /// <param name="hresult">The error code to report.</param>
    /// <exception cref="Win32Exception">Thrown with the given error code.</exception>
    [DoesNotReturn]
    private static void Throw(int hresult)
    {
        throw new Win32Exception(hresult);
    }
}