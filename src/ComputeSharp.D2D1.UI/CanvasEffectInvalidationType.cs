#if !D2D1_UI_SOURCE_GENERATOR
using ABI.Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas;
#endif

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp;
#else
namespace ComputeSharp.D2D1.WinUI;
#endif

/// <summary>
/// Indicates the invalidation type to request when invoking <see cref="CanvasEffect.InvalidateEffectGraph"/> and related methods.
/// </summary>
public enum CanvasEffectInvalidationType : byte
{
    /// <summary>
    /// Invalidates the state of the effect graph, causing it to be configured again the next time it is used.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This will preserve the last returned <see cref="ICanvasImage"/> instance, if available, and it will only
    /// mark the internal state as being out of date, resulting in <see cref="CanvasEffect.ConfigureEffectGraph"/> to be called
    /// the next time the effect is used for drawing.
    /// </para>
    /// <para>
    /// This is much less expensive than creating the effect graph again, and should be preferred if possible.
    /// </para>
    /// </remarks>
    Update,

    /// <summary>
    /// Fully invalidates the effect graph, causing it to be created again the next time it is needed.
    /// </summary>
    /// <remarks>
    /// This will cause the last returned <see cref="ICanvasImage"/> instance to be disposed and discarded,
    /// and <see cref="CanvasEffect.BuildEffectGraph"/> to be called again the next time the effect is used for drawing.
    /// </remarks>
    Creation
}