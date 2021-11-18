using System;
using System.Buffers;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace ComputeSharp.__Internals;

/// <summary>
/// A helper type that implements a pooled buffer writer for <see cref="char"/> values.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This type is not intended to be used directly by user code")]
[DebuggerTypeProxy(typeof(DebugView))]
public ref struct ArrayPoolStringBuilder
{
    /// <summary>
    /// The underlying <see cref="char"/> array.
    /// </summary>
    private char[] array;

    /// <summary>
    /// The starting offset within <see cref="array"/>.
    /// </summary>
    private int index;

    /// <summary>
    /// Creates a new <see cref="ArrayPoolStringBuilder"/> instance ready to use.
    /// </summary>
    /// <param name="sizeHint">The size hint for the internal buffer to allocate.</param>
    /// <returns>A new <see cref="ArrayPoolStringBuilder"/> instance with default values.</returns>
    [Pure]
    public static ArrayPoolStringBuilder Create(int sizeHint)
    {
        ArrayPoolStringBuilder builder;

        builder.array = ArrayPool<char>.Shared.Rent(sizeHint);
        builder.index = 0;

        return builder;
    }

    /// <inheritdoc/>
    public ReadOnlySpan<char> WrittenSpan
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(this.array, 0, this.index);
    }

    /// <summary>
    /// Appends the input character to the current buffer.
    /// </summary>
    /// <param name="value">The input character to write.</param>
    public void Append(char value)
    {
        EnsureCapacity(1);

        this.array[this.index++] = value;
    }

    /// <summary>
    /// Appends the input sequence of characters to the current buffer.
    /// </summary>
    /// <param name="value">The input characters to write.</param>
    public void Append(string value)
    {
        EnsureCapacity(value.Length);

#if NET6_0_OR_GREATER
        value.CopyTo(this.array.AsSpan(this.index));
#else
        value.AsSpan().CopyTo(this.array.AsSpan(this.index));
#endif

        this.index += value.Length;
    }

    /// <summary>
    /// Appends the text representation of the input value to the current buffer.
    /// </summary>
    /// <param name="value">The input value to write.</param>
    public void Append(int value)
    {
#if NET6_0_OR_GREATER
        int charsWritten;

        while (!value.TryFormat(this.array.AsSpan(this.index), out charsWritten))
        {
            EnsureCapacity(10);
        }

        this.index += charsWritten;
#else
        Append(value.ToString());
#endif
    }

    /// <summary>
    /// Ensures that <see cref="array"/> has enough free space to contain a given number of new items.
    /// </summary>
    /// <param name="requestedSize">The minimum number of items to ensure space for in <see cref="array"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureCapacity(int requestedSize)
    {
        if (requestedSize > this.array.Length - this.index)
        {
            ResizeBuffer(requestedSize);
        }
    }

    /// <summary>
    /// Resizes <see cref="array"/> to ensure it can fit the specified number of new items.
    /// </summary>
    /// <param name="sizeHint">The minimum number of items to ensure space for in <see cref="array"/>.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void ResizeBuffer(int sizeHint)
    {
        int minimumSize = this.index + sizeHint;

        // Rent a new array with the specified size, and copy as many items from the current array
        // as possible to the new array. This mirrors the behavior of the Array.Resize API from
        // the BCL: if the new size is greater than the length of the current array, copy all the
        // items from the original array into the new one. Otherwise, copy as many items as possible,
        // until the new array is completely filled, and ignore the remaining items in the first array.
        char[] newArray = ArrayPool<char>.Shared.Rent(minimumSize);
        int itemsToCopy = Math.Min(this.array.Length, minimumSize);

        Array.Copy(this.array, 0, newArray, 0, itemsToCopy);

        ArrayPool<char>.Shared.Return(array);

        this.array = newArray;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ArrayPool<char>.Shared.Return(this.array);
    }

    /// <summary>
    /// A debug proxy used for displaying debug info.
    /// </summary>
    internal sealed class DebugView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugView"/> class with the specified parameters.
        /// </summary>
        /// <param name="builder">The input <see cref="ArrayPoolStringBuilder"/> instance to display.</param>
        public DebugView(ArrayPoolStringBuilder builder)
        {
            Text = builder.WrittenSpan.ToString();
        }

        /// <summary>
        /// Gets the text to display for the current instance.
        /// </summary>
        public string Text { get; }
    }
}
