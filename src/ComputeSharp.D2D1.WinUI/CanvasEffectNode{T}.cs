using Microsoft.Graphics.Canvas;

namespace ComputeSharp.D2D1.WinUI;

/// <summary>
/// A marker type for an effect node that can be registered and retrieved from an <see cref="CanvasEffectGraph"/> value.
/// </summary>
/// <typeparam name="T">The type of <see cref="ICanvasImage"/> associated with the current effect node.</typeparam>
public sealed class CanvasEffectNode<T> : ICanvasEffectNode<T>
    where T : class, ICanvasImage;