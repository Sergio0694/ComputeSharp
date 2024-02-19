using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649

namespace ComputeSharp;

/// <summary>
/// A locally unique identifier for a graphics device.
/// </summary>
public readonly struct Luid : IEquatable<Luid>, ISpanFormattable
{
    /// <summary>
    /// The low bits of the luid.
    /// </summary>
    private readonly uint lowPart;

    /// <summary>
    /// The high bits of the luid.
    /// </summary>
    private readonly int highPart;

    /// <summary>
    /// Creates a new <see cref="Luid"/> instance from a raw <see cref="LUID"/> value.
    /// </summary>
    /// <param name="luid">The input <see cref="LUID"/> value.</param>
    /// <returns>A <see cref="Luid"/> instance with the same value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static unsafe Luid FromLUID(LUID luid)
    {
        return *(Luid*)&luid;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Luid other)
    {
        return
            this.lowPart == other.lowPart &&
            this.highPart == other.highPart;
    }

    /// <inheritdoc/>
    public override bool Equals(object? other)
    {
        return other is Luid luid && Equals(luid);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(this.lowPart, this.highPart);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return ((((long)this.highPart) << 32) | this.lowPart).ToString();
    }

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ((((long)this.highPart) << 32) | this.lowPart).ToString(format, formatProvider);
    }

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return ((((long)this.highPart) << 32) | this.lowPart).TryFormat(destination, out charsWritten, format, provider);
    }

    /// <summary>
    /// Check whether two <see cref="Luid"/> values are equal.
    /// </summary>
    /// <param name="a">The first <see cref="Luid"/> value to compare.</param>
    /// <param name="b">The second <see cref="Luid"/> value to compare.</param>
    /// <returns>Whether <paramref name="a"/> and <paramref name="b"/> are the same.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Luid a, Luid b) => a.Equals(b);

    /// <summary>
    /// Check whether two <see cref="Luid"/> values are different.
    /// </summary>
    /// <param name="a">The first <see cref="Luid"/> value to compare.</param>
    /// <param name="b">The second <see cref="Luid"/> value to compare.</param>
    /// <returns>Whether <paramref name="a"/> and <paramref name="b"/> are different.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Luid a, Luid b) => !a.Equals(b);
}