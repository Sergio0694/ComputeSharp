using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE0060

namespace ComputeSharp;

/// <summary>
/// A <see langword="struct"/> that can be used in place of the <see cref="bool"/> type in HLSL shaders.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
public readonly struct Bool
{
    /// <summary>
    /// The wrapped <see cref="int"/> value for the current instance.
    /// </summary>
    [FieldOffset(0)]
    private readonly int value;

    /// <summary>
    /// Creates a new <see cref="Bool"/> instance for a given <see cref="bool"/> value.
    /// </summary>
    /// <param name="value">.</param>
    private Bool(bool value)
    {
        this.value = value ? 1 : 0;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Bool x && this.value == x.value;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return this.value.GetHashCode();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return ((bool)this).ToString();
    }

    /// <inheritdoc cref="bool.ToString(IFormatProvider?)"/>
    public string ToString(IFormatProvider? formatProvider)
    {
        return ((bool)this).ToString(formatProvider);
    }

#if !SOURCE_GENERATOR
    /// <inheritdoc cref="bool.TryFormat(Span{char}, out int)"/>
    public bool TryFormat(Span<char> destination, out int charsWritten)
    {
        return ((bool)this).TryFormat(destination, out charsWritten);
    }
#endif

    /// <summary>
    /// Inverts the <see cref="bool"/> value represented by a given <see cref="Bool"/> instance.
    /// </summary>
    /// <param name="x">The input <see cref="Bool"/> instance.</param>
    public static Bool operator !(Bool x) => new(!(bool)x);

    /// <summary>
    /// Ands two <see cref="Bool"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool"/> value to and.</param>
    /// <param name="right">The <see cref="Bool"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool operator &(Bool left, Bool right) => default;

    /// <summary>
    /// Ors two <see cref="Bool"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool"/> value to or.</param>
    /// <param name="right">The <see cref="Bool"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool operator |(Bool left, Bool right) => default;

    /// <summary>
    /// Xors two <see cref="Bool"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool operator ^(Bool left, Bool right) => default;

    /// <summary>
    /// Checks whether or not two <see cref="Bool"/> instances represent the same <see cref="bool"/> value.
    /// </summary>
    /// <param name="left">The left <see cref="Bool"/> instance.</param>
    /// <param name="right">The right <see cref="Bool"/> instance.</param>
    public static bool operator ==(Bool left, Bool right) => left.value == right.value;

    /// <summary>
    /// Checks whether or not two <see cref="Bool"/> instances represent a different <see cref="bool"/> value.
    /// </summary>
    /// <param name="left">The left <see cref="Bool"/> instance.</param>
    /// <param name="right">The right <see cref="Bool"/> instance.</param>
    public static bool operator !=(Bool left, Bool right) => left.value != right.value;

    /// <summary>
    /// Converts a given <see cref="Bool"/> instance to its corresponding <see cref="bool"/> value.
    /// </summary>
    /// <param name="x">The input <see cref="Bool"/> instance.</param>
    public static implicit operator bool(Bool x) => x.value != 0;

    /// <summary>
    /// Converts a <see cref="bool"/> value to a corresponding <see cref="Bool"/> instance.
    /// </summary>
    /// <param name="x">The input <see cref="bool"/> value.</param>
    public static implicit operator Bool(bool x) => new(x);
}