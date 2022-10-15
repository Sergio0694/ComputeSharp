// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices;

/// <summary>
/// Provides a handler used by the language compiler to format interpolated strings into character spans.
/// </summary>
[InterpolatedStringHandler]
internal ref struct TryWriteFormatInterpolatedStringHandler
{
    /// <summary>
    /// The destination buffer.
    /// </summary>
    private readonly Span<char> destination;

    /// <summary>
    /// The format to use to print individual values to the target buffer.
    /// </summary>
    private readonly ReadOnlySpan<char> format;

    /// <summary>
    /// The value of <see cref="format"/>, as a <see cref="string"/>.
    /// </summary>
    private string? formatAsString;

    /// <summary>
    /// Optional provider to pass to <see cref="IFormattable.ToString(string?, IFormatProvider?)"/> calls.
    /// </summary>
    private readonly IFormatProvider? formatProvider;

    /// <summary>
    /// The number of characters written to <see cref="destination"/>.
    /// </summary>
    private int position;

    /// <summary>
    /// Indicates whether all formatting operations have succeeded.
    /// </summary>
    private bool success;

    /// <summary>
    /// Whether <see cref="formatProvider"/> provides an <see cref="ICustomFormatter"/>.
    /// </summary>
    private readonly bool hasCustomFormatter;

    /// <summary>
    /// Creates a handler used to write an interpolated string into a <see cref="Span{Char}"/>.
    /// </summary>
    /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
    /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
    /// <param name="destination">The destination buffer.</param>
    /// <param name="format">The format to use, if available.</param>
    /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
    /// <param name="shouldAppend">Upon return, true if the destination may be long enough to support the formatting, or false if it won't be.</param>
    /// <remarks>This is intended to be called only by compiler-generated code. Arguments are not validated as they'd otherwise be for members intended to be used directly.</remarks>
    public TryWriteFormatInterpolatedStringHandler(int literalLength, int formattedCount, Span<char> destination, ReadOnlySpan<char> format, IFormatProvider? formatProvider, out bool shouldAppend)
    {
        this.destination = destination;
        this.format = format;
        this.formatAsString = null;
        this.formatProvider = formatProvider;
        this.position = 0;
        this.success = shouldAppend = destination.Length >= literalLength;
        this.hasCustomFormatter = formatProvider is not null && FormatInterpolatedStringHandler.HasCustomFormatter(formatProvider);
    }

    /// <summary>
    /// Writes the specified interpolated string to the character span.
    /// </summary>
    /// <param name="destination">The span to which the interpolated string should be formatted.</param>
    /// <param name="format">The format to use, if available.</param>
    /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
    /// <param name="handler">The interpolated string.</param>
    /// <param name="charsWritten">The number of characters written to the span.</param>
    /// <returns>true if the entire interpolated string could be formatted successfully; otherwise, false.</returns>
    public static bool TryWrite(
        Span<char> destination,
        ReadOnlySpan<char> format,
        IFormatProvider? formatProvider,
        [InterpolatedStringHandlerArgument("destination", "format", "formatProvider")] ref TryWriteFormatInterpolatedStringHandler handler,
        out int charsWritten)
    {
        if (handler.success)
        {
            charsWritten = handler.position;

            return true;
        }

        charsWritten = 0;

        return false;
    }

    /// <summary>
    /// Writes the specified <see cref="string"/> to the handler.
    /// </summary>
    /// <param name="value">The <see cref="string"/> to write.</param>
    /// <returns>Whether the value could be formatted to the span.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool AppendLiteral(string value)
    {
        if (value.Length == 1)
        {
            Span<char> destination = this.destination;
            int pos = this.position;

            if ((uint)pos < (uint)destination.Length)
            {
                destination[pos] = value[0];

                this.position = pos + 1;

                return true;
            }

            return Fail();
        }

        if (value.Length == 2)
        {
            Span<char> destination = this.destination;
            int pos = this.position;

            if ((uint)pos < destination.Length - 1)
            {
                Unsafe.WriteUnaligned(
                    ref Unsafe.As<char, byte>(ref Unsafe.Add(ref MemoryMarshal.GetReference(destination), pos)),
#if NET6_0_OR_GREATER
                    Unsafe.ReadUnaligned<int>(ref Unsafe.As<char, byte>(ref Unsafe.AsRef(in value.GetPinnableReference()))));
#else
                    Unsafe.ReadUnaligned<int>(ref Unsafe.As<char, byte>(ref MemoryMarshal.GetReference(value.AsSpan()))));
#endif

                this.position = pos + 2;

                return true;
            }

            return Fail();
        }

        return AppendStringDirect(value);
    }

    /// <summary>
    /// Writes the specified string to the handler.
    /// </summary>
    /// <param name="value">The string to write.</param>
    /// <returns>Whether the value could be appended to the span.</returns>
    private bool AppendStringDirect(string value)
    {
#if NET6_0_OR_GREATER
        if (value.TryCopyTo(this.destination.Slice(this.position)))
#else
        if (value.AsSpan().TryCopyTo(this.destination.Slice(this.position)))
#endif
        {
            this.position += value.Length;
            return true;
        }

        return Fail();
    }

    /// <summary>
    /// Writes the specified value to the handler.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public bool AppendFormatted<T>(T value)
#if NET6_0_OR_GREATER
        where T : ISpanFormattable
#else
        where T : IFormattable
#endif
    {
        if (this.hasCustomFormatter)
        {
            return AppendCustomFormatter(value, this.formatAsString ??= this.format.ToString());
        }

#if NET6_0_OR_GREATER
        if (value.TryFormat(this.destination.Slice(this.position), out int charsWritten, this.format, this.formatProvider))
        {
            this.position += charsWritten;

            return true;
        }

        return Fail();
#else
        return AppendStringDirect(value.ToString(this.formatAsString ??= this.format.ToString(), this.formatProvider));
#endif
    }

    /// <summary>
    /// Writes the specified value to the handler.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public bool AppendFormatted(string? value)
    {
        if (this.hasCustomFormatter)
        {
            return AppendCustomFormatter(value, format: null);
        }

        if (value is null)
        {
            return true;
        }

#if NET6_0_OR_GREATER
        if (value.TryCopyTo(this.destination.Slice(this.position)))
#else
        if (value.AsSpan().TryCopyTo(this.destination.Slice(this.position)))
#endif
        {
            this.position += value.Length;

            return true;
        }

        return Fail();
    }

    /// <summary>
    /// Formats the value using the custom formatter from the provider.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="format">The format string.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private bool AppendCustomFormatter<T>(T value, string? format)
    {
        ICustomFormatter? formatter = (ICustomFormatter?)this.formatProvider?.GetFormat(typeof(ICustomFormatter));

        if (formatter is not null && formatter.Format(format, value, this.formatProvider) is string customFormatted)
        {
            return AppendStringDirect(customFormatted);
        }

        return true;
    }

    /// <summary>
    /// Marks formatting as having failed and returns false.
    /// </summary>
    private bool Fail()
    {
        this.success = false;

        return false;
    }
}