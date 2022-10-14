using System.Runtime.CompilerServices;

namespace System;

/// <summary>
/// A polyfill type with additional formatting extensions.
/// </summary>
internal static class FormatExtensions
{
    /// <inheritdoc cref="IFormattable.ToString(string?, IFormatProvider?)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString<T>(this T value, string? format)
    {
        throw new NotSupportedException("This API is not supported on this target.");
    }

    /// <inheritdoc cref="IFormattable.ToString(string?, IFormatProvider?)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString<T>(this T value, string? format, IFormatProvider? formatProvider)
    {
        throw new NotSupportedException("This API is not supported on this target.");
    }
}