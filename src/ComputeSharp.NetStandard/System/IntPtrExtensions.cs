using System.Runtime.CompilerServices;

namespace System;

/// <summary>
/// A polyfill type with extensions for <see cref="IntPtr"/> and <see cref="UIntPtr"/>.
/// </summary>
internal static class IntPtrExtensions
{
    /// <inheritdoc cref="IComparable{T}.CompareTo(T)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CompareTo(this nint left, nint right)
    {
        return left.CompareTo(right);
    }

    /// <inheritdoc cref="IComparable{T}.CompareTo(T)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CompareTo(this nuint left, nuint right)
    {
        return left.CompareTo(right);
    }
}