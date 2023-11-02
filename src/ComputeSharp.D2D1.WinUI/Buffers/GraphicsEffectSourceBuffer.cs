using System.Runtime.CompilerServices;
using Windows.Graphics.Effects;

#pragma warning disable IDE0051

namespace ComputeSharp.D2D1.WinUI.Buffers;

/// <summary>
/// A fixed buffer type containing 16 <see cref="IGraphicsEffectSource"/> fields.
/// </summary>
[InlineArray(16)]
internal struct GraphicsEffectSourceBuffer
{
    /// <summary>
    /// The <see cref="SourceReference"/> instance at index 0.
    /// </summary>
    private SourceReference source0;
}