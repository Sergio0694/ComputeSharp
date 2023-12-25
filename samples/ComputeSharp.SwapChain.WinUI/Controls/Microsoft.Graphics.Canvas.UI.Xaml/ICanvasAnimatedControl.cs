using Windows.Foundation;

namespace Microsoft.Graphics.Canvas.UI.Xaml;

/// <summary>
/// XAML control intended for displaying animating content.
/// </summary>
public interface ICanvasAnimatedControl
{
    /// <inheritdoc cref="CanvasControl.Size"/>
    Size Size { get; }

    /// <inheritdoc cref="CanvasControl.ConvertDipsToPixels"/>
    int ConvertDipsToPixels(float dips, CanvasDpiRounding dpiRounding);
}