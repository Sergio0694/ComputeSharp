using CommunityToolkit.WinUI;
using ComputeSharp.SwapChain.Core.Shaders;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ComputeSharp.SwapChain.WinUI.Views;

/// <summary>
/// A custom <see cref="Control"/> that can be used to render animated backgrounds via Win2D.
/// </summary>
[TemplatePart(Name = "PART_CanvasAnimatedControl", Type = typeof(CanvasAnimatedControl))]
public sealed partial class D2D1AnimatedPixelShaderPanel : Control
{
    /// <summary>
    /// The wrapped <see cref="CanvasAnimatedControl"/> instance used to render frames.
    /// </summary>
    private CanvasAnimatedControl? canvasAnimatedControl;

    /// <summary>
    /// The <see cref="PixelShaderEffect"/> instance currently in use, if any.
    /// </summary>
    private volatile PixelShaderEffect? pixelShaderEffect;

    /// <summary>
    /// Creates a new <see cref="D2D1AnimatedPixelShaderPanel"/> instance.
    /// </summary>
    public D2D1AnimatedPixelShaderPanel()
    {
        DefaultStyleKey = typeof(D2D1AnimatedPixelShaderPanel);

        Loaded += D2D1AnimatedPixelShaderPanel_Loaded;
        Unloaded += D2D1AnimatedPixelShaderPanel_Unloaded;
    }

    /// <summary>
    /// Gets or sets the <see cref="Core.Shaders.PixelShaderEffect"/> instance to use to render content.
    /// </summary>
    [GeneratedDependencyProperty]
    public partial PixelShaderEffect? PixelShaderEffect { get; set; }

    /// <summary>
    /// Gets or sets whether or not the rendering is paused.
    /// </summary>
    [GeneratedDependencyProperty]
    public partial bool IsPaused { get; set; }

    /// <inheritdoc/>
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        this.canvasAnimatedControl = (CanvasAnimatedControl)GetTemplateChild("PART_CanvasAnimatedControl")!;
        this.canvasAnimatedControl.Draw += CanvasAnimatedControl_Draw;
    }

    // Ensure the panel is not paused when the control is loaded
    private void D2D1AnimatedPixelShaderPanel_Loaded(object sender, RoutedEventArgs e)
    {
        if (this.canvasAnimatedControl is { } canvasAnimatedControl &&
            PixelShaderEffect is not null &&
            !IsPaused)
        {
            canvasAnimatedControl.Paused = false;
        }
    }

    // Always pause rendering when the control is unloaded
    private void D2D1AnimatedPixelShaderPanel_Unloaded(object sender, RoutedEventArgs e)
    {
        if (this.canvasAnimatedControl is { } canvasAnimatedControl)
        {
            canvasAnimatedControl.Paused = true;
        }
    }

    /// <summary>
    /// Draws a new frame on the wrapped <see cref="CanvasAnimatedControl"/> instance.
    /// </summary>
    /// <param name="sender">The source <see cref="ICanvasAnimatedControl"/> instance.</param>
    /// <param name="args">The <see cref="CanvasAnimatedDrawEventArgs"/> object to perform drawing with.</param>
    private void CanvasAnimatedControl_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
    {
        // Set the effect properties
        this.pixelShaderEffect!.ElapsedTime = args.Timing.TotalTime;
        this.pixelShaderEffect.ScreenWidth = sender.ConvertDipsToPixels((float)sender.Size.Width, CanvasDpiRounding.Round);
        this.pixelShaderEffect.ScreenHeight = sender.ConvertDipsToPixels((float)sender.Size.Height, CanvasDpiRounding.Round);

        // Draw the effect
        args.DrawingSession.DrawImage(this.pixelShaderEffect);
    }

    /// <inheritdoc/>
    partial void OnPixelShaderEffectPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        PixelShaderEffect? pixelShaderEffect = (PixelShaderEffect?)e.NewValue;

        // Save the new pixel shader effect for later (as the dependency property cannot be accessed by the render thread)
        this.pixelShaderEffect = pixelShaderEffect;

        // Pause or start the render thread if an effect is available
        if (this.canvasAnimatedControl is { } canvasAnimatedControl)
        {
            bool shouldRender = IsLoaded && !IsPaused && pixelShaderEffect is not null;

            canvasAnimatedControl.Paused = !shouldRender;
        }
    }

    /// <inheritdoc/>
    partial void OnIsPausedPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if (this.canvasAnimatedControl is { } canvasAnimatedControl)
        {
            bool isPaused = (bool)e.NewValue;
            bool shouldRender = IsLoaded && !isPaused && PixelShaderEffect is not null;

            canvasAnimatedControl.Paused = !shouldRender;
        }
    }
}