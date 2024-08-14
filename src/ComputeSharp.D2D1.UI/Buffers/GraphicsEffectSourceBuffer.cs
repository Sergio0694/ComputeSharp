using System.Runtime.CompilerServices;
using Windows.Graphics.Effects;

#pragma warning disable IDE0051

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.Buffers;
#else
namespace ComputeSharp.D2D1.WinUI.Buffers;
#endif

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