#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp;
#else
namespace ComputeSharp.D2D1.WinUI;
#endif

/// <summary>
/// A marker interface for an effect node that was registered from an <see cref="CanvasEffectGraph"/> value.
/// </summary>
/// <remarks>
/// <para>
/// This interface allows using <see cref="CanvasEffectGraph.GetNode"/> and <see cref="CanvasEffectGraph.SetOutputNode"/> in more scenarios,
/// including where the concrete type of the image being used (eg. to set the output node) is not known (or needed) by the caller.
/// </para>
/// <para>
/// This interface only implemented by <see cref="CanvasEffectNode{T}"/> and it's not meant to be implemented by external types.
/// </para>
/// </remarks>
public interface ICanvasEffectNode
{
}