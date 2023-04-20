using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Windows.Graphics.Effects;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.Buffers;
#else
namespace ComputeSharp.D2D1.WinUI.Buffers;
#endif

/// <summary>
/// A fixed buffer type containing 16 <see cref="IGraphicsEffectSource"/> fields.
/// </summary>
internal struct GraphicsEffectSourceBuffer
{
    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 0.
    /// </summary>
    public SourceReference Source0;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 1.
    /// </summary>
    public SourceReference Source1;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 2.
    /// </summary>
    public SourceReference Source2;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 3.
    /// </summary>
    public SourceReference Source3;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 4.
    /// </summary>
    public SourceReference Source4;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 5.
    /// </summary>
    public SourceReference Source5;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 6.
    /// </summary>
    public SourceReference Source6;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 7.
    /// </summary>
    public SourceReference Source7;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 8.
    /// </summary>
    public SourceReference Source8;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 9.
    /// </summary>
    public SourceReference Source9;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 10.
    /// </summary>
    public SourceReference Source10;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 11.
    /// </summary>
    public SourceReference Source11;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 12.
    /// </summary>
    public SourceReference Source12;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 13.
    /// </summary>
    public SourceReference Source13;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 14.
    /// </summary>
    public SourceReference Source14;

    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 15.
    /// </summary>
    public SourceReference Source15;

    /// <summary>
    /// Gets a reference to the <see cref="SourceReference"/> value at a given index.
    /// </summary>
    /// <param name="index">The index of the <see cref="SourceReference"/> value to get a reference to.</param>
    /// <returns>A reference to the <see cref="SourceReference"/> value at a given index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is not a valid index for the current effect.</exception>
    [UnscopedRef]
    public ref SourceReference this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            default(ArgumentOutOfRangeException).ThrowIfNotInRange(index, 0, 16);

            ref SourceReference r0 = ref Unsafe.As<GraphicsEffectSourceBuffer, SourceReference>(ref this);
            ref SourceReference r1 = ref Unsafe.Add(ref r0, index);

            return ref r1;
        }
    }
}