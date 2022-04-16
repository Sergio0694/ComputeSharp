using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Helpers;

namespace ComputeSharp.D2D1.Shaders.Interop.Buffers;

/// <summary>
/// A custom <see cref="MemoryManager{T}"/> that wraps a pinned memory buffer.
/// </summary>
internal sealed unsafe class PinnedBufferMemoryManager : MemoryManager<byte>
{
    /// <summary>
    /// A pointer to the buffer.
    /// </summary>
    private readonly byte* buffer;

    /// <summary>
    /// The length of the buffer.
    /// </summary>
    private readonly int length;

    /// <summary>
    /// Initializes a new instance of the <see cref="PinnedBufferMemoryManager"/> class with the specified parameters.
    /// </summary>
    /// <param name="span">A <see cref="ReadOnlySpan{T}"/> wrapping the input pinned buffer.</param>
    public PinnedBufferMemoryManager(ReadOnlySpan<byte> span)
    {
        this.buffer = (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(span));
        this.length = span.Length;
    }

    /// <inheritdoc/>
    public override Span<byte> GetSpan()
    {
        return new(this.buffer, this.length);
    }

    /// <inheritdoc/>
    public override unsafe MemoryHandle Pin(int elementIndex = 0)
    {
        if ((uint)elementIndex >= this.length)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(elementIndex));
        }

        return new(this.buffer + elementIndex);
    }

    /// <inheritdoc/>
    public override void Unpin()
    {
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
    }
}
