using System.Runtime.CompilerServices;

namespace System;

/// <summary>
/// A polyfill type with extensions for <see cref="IntPtr"/> and <see cref="UIntPtr"/>.
/// </summary>
internal static class IntPtrExtensions
{
    /// <inheritdoc cref="IComparable{T}.CompareTo(T)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int CompareTo(this nint left, nint right)
    {
        if (sizeof(nint) == sizeof(int))
        {
            return ((int)left).CompareTo((int)right);
        }

        return ((long)left).CompareTo(right);
    }

    /// <inheritdoc cref="IComparable{T}.CompareTo(T)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int CompareTo(this nuint left, nuint right)
    {
        if (sizeof(nuint) == sizeof(uint))
        {
            return ((uint)left).CompareTo((uint)right);
        }

        return ((ulong)left).CompareTo(right);
    }
}