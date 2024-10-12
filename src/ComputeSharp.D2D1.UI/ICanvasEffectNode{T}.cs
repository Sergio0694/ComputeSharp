using Microsoft.Graphics.Canvas;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp;
#else
namespace ComputeSharp.D2D1.WinUI;
#endif

/// <summary>
/// A marker interface for a generic effect node that was registered from an <see cref="CanvasEffectGraph"/> value.
/// </summary>
/// <typeparam name="T">The covariant type of <see cref="ICanvasImage"/> associated with the current effect node.</typeparam>
/// <remarks>
/// <para>
/// This interface allows using <see cref="CanvasEffectGraph.GetNode"/> and <see cref="CanvasEffectGraph.SetOutputNode"/> in more scenarios,
/// including with ternary expressions returning multiple concrete <see cref="CanvasEffectNode{T}"/> instances with a common image type.
/// </para>
/// <para>
/// This interface only implemented by <see cref="CanvasEffectNode{T}"/> and it's not meant to be implemented by external types.
/// </para>
/// </remarks>
public interface ICanvasEffectNode<out T> : ICanvasEffectNode
    where T : ICanvasImage
{
}