// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Buffers;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices;

/// <summary>
/// Provides a handler used by the language compiler to process interpolated strings into <see cref="string"/> instances.
/// </summary>
[InterpolatedStringHandler]
public ref struct FormatInterpolatedStringHandler
{
    /// <summary>
    /// Minimum size array to rent from the pool.
    /// </summary>
    private const int MinimumArrayPoolLength = 256;

    /// <summary>
    /// The format to use to print individual values to the target buffer.
    /// </summary>
    private readonly string? format;

    /// <summary>
    /// Optional provider to pass to <see cref="IFormattable.ToString(string?, IFormatProvider?)"/> or <see cref="ISpanFormattable.TryFormat(Span{char}, out int, ReadOnlySpan{char}, IFormatProvider?)"/> calls.
    /// </summary>
    private readonly IFormatProvider? formatProvider;

    /// <summary>
    /// Array rented from the array pool and used to back <see cref="characters"/>.
    /// </summary>
    private char[]? arrayToReturnToPool;

    /// <summary>
    /// The span to write into.
    /// </summary>
    private Span<char> characters;

    /// <summary>
    /// Position at which to write the next character.
    /// </summary>
    private int position;

    /// <summary>
    /// Whether <see cref="formatProvider"/> provides an <see cref="ICustomFormatter"/>.
    /// </summary>
    private readonly bool hasCustomFormatter;

    /// <summary>
    /// Creates a handler used to translate an interpolated string into a <see cref="string"/>.
    /// </summary>
    /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
    /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
    /// <param name="format">The format to use, if available.</param>
    /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
    /// <param name="initialBuffer">A buffer temporarily transferred to the handler for use as part of its formatting. Contents may be overwritten.</param>
    /// <remarks>This is intended to be called only by compiler-generated code. Arguments are not validated as they'd otherwise be for members intended to be used directly.</remarks>
    public FormatInterpolatedStringHandler(int literalLength, int formattedCount, string? format, IFormatProvider? formatProvider, Span<char> initialBuffer)
    {
        this.format = format;
        this.formatProvider = formatProvider;
        characters = initialBuffer;
        arrayToReturnToPool = null;
        position = 0;
        hasCustomFormatter = formatProvider is not null && HasCustomFormatter(formatProvider);
    }

    /// <summary>
    /// Creates a new <see cref="string"/> with the specified arguments.
    /// </summary>
    /// <param name="format">The format to use, if available.</param>
    /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
    /// <param name="initialBuffer">A buffer temporarily transferred to the handler for use as part of its formatting. Contents may be overwritten.</param>
    /// <param name="handler">The input <see cref="FormatInterpolatedStringHandler"/> instance responsible for formatting the resulting <see cref="string"/>.</param>
    /// <returns>The resulting formatted <see cref="string"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Create(
        string? format,
        IFormatProvider? formatProvider,
        Span<char> initialBuffer,
        [InterpolatedStringHandlerArgument("format", "formatProvider", "initialBuffer")] ref FormatInterpolatedStringHandler handler)
    {
        return handler.ToStringAndClear();
    }

    /// <summary>Gets the built <see cref="string"/> and clears the handler.</summary>
    /// <returns>The built string.</returns>
    /// <remarks>
    /// This releases any resources used by the handler. The method should be invoked only
    /// once and as the last thing performed on the handler. Subsequent use is erroneous, ill-defined,
    /// and may destabilize the process, as may using any other copies of the handler after ToStringAndClear
    /// is called on any one of them.
    /// </remarks>
    public string ToStringAndClear()
    {
        string result = new(characters.Slice(0, position));

        Clear();

        return result;
    }

    /// <summary>
    /// Clears the handler, returning any rented array to the pool.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Clear()
    {
        char[]? toReturn = arrayToReturnToPool;

        this = default;

        if (toReturn is not null)
        {
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }

    /// <summary>
    /// Writes the specified string to the handler.
    /// </summary>
    /// <param name="value">The string to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLiteral(string value)
    {
        if (value.Length == 1)
        {
            Span<char> chars = characters;
            int pos = position;

            if ((uint)pos < (uint)chars.Length)
            {
                chars[pos] = value[0];
                position = pos + 1;
            }
            else
            {
                GrowThenCopyString(value);
            }
            return;
        }

        if (value.Length == 2)
        {
            Span<char> chars = characters;
            int pos = position;

            if ((uint)pos < chars.Length - 1)
            {
                Unsafe.WriteUnaligned(
                    ref Unsafe.As<char, byte>(ref Unsafe.Add(ref MemoryMarshal.GetReference(chars), pos)),
                    Unsafe.ReadUnaligned<int>(ref Unsafe.As<char, byte>(ref Unsafe.AsRef(in value.GetPinnableReference()))));

                position = pos + 2;
            }
            else
            {
                GrowThenCopyString(value);
            }
            return;
        }

        AppendStringDirect(value);
    }

    /// <summary>
    /// Writes the specified string to the handler.
    /// </summary>
    /// <param name="value">The string to write.</param>
    private void AppendStringDirect(string value)
    {
        if (value.TryCopyTo(characters.Slice(position)))
        {
            position += value.Length;
        }
        else
        {
            GrowThenCopyString(value);
        }
    }

    /// <summary>
    /// Writes the specified value to the handler.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void AppendFormatted(string value)
    {
        AppendLiteral(value);
    }

    /// <summary>
    /// Writes the specified value to the handler.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void AppendFormatted<T>(T value)
        where T : ISpanFormattable
    {
        if (hasCustomFormatter)
        {
            AppendCustomFormatter(value, format);
            return;
        }

        int charsWritten;

        while (!value.TryFormat(characters.Slice(position), out charsWritten, format, formatProvider))
        {
            Grow();
        }

        position += charsWritten;
    }

    /// <summary>
    /// Gets whether the provider provides a custom formatter.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool HasCustomFormatter(IFormatProvider provider)
    {
        return
            provider.GetType() != typeof(CultureInfo) &&
            provider.GetFormat(typeof(ICustomFormatter)) != null;
    }

    /// <summary>
    /// Formats the value using the custom formatter from the provider.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="format">The format string.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void AppendCustomFormatter<T>(T value, string? format)
        where T : ISpanFormattable
    {
        ICustomFormatter? formatter = (ICustomFormatter?)formatProvider?.GetFormat(typeof(ICustomFormatter));

        if (formatter is not null && formatter.Format(format, value, formatProvider) is string customFormatted)
        {
            AppendStringDirect(customFormatted);
        }
    }

    /// <summary>
    /// Fallback for fast path in <see cref="AppendStringDirect"/> when there's not enough space in the destination.
    /// </summary>
    /// <param name="value">The string to write.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void GrowThenCopyString(string value)
    {
        Grow(value.Length);

        value.CopyTo(characters.Slice(position));

        position += value.Length;
    }

    /// <summary>
    /// Grows <see cref="characters"/> to have the capacity to store at least <paramref name="additionalChars"/> beyond <see cref="position"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Grow(int additionalChars)
    {
        GrowCore((uint)position + (uint)additionalChars);
    }

    /// <summary>
    /// Grows the size of <see cref="characters"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Grow()
    {
        GrowCore((uint)characters.Length + 1);
    }

    /// <summary>
    /// Grow the size of <see cref="characters"/> to at least the specified <paramref name="requiredMinCapacity"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void GrowCore(uint requiredMinCapacity)
    {
        uint newCapacity = Math.Max(requiredMinCapacity, Math.Min((uint)characters.Length * 2, 0x3FFFFFDF));
        int arraySize = (int)Math.Clamp(newCapacity, MinimumArrayPoolLength, int.MaxValue);
        char[] newArray = ArrayPool<char>.Shared.Rent(arraySize);

        characters.Slice(0, position).CopyTo(newArray);

        char[]? toReturn = arrayToReturnToPool;

        characters = arrayToReturnToPool = newArray;

        if (toReturn is not null)
        {
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }
}