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
    /// The <see cref="DependencyProperty"/> backing <see cref="PixelShaderEffect"/>.
    /// </summary>
    public static readonly DependencyProperty PixelShaderEffectProperty = DependencyProperty.Register(
        nameof(PixelShaderEffect),
        typeof(PixelShaderEffect),
        typeof(D2D1AnimatedPixelShaderPanel),
        new PropertyMetadata(null, OnPixelShaderEffectPropertyChanged));

    /// <summary>
    /// The <see cref="DependencyProperty"/> backing <see cref="IsPaused"/>.
    /// </summary>
    public static readonly DependencyProperty IsPausedProperty = DependencyProperty.Register(
        nameof(IsPaused),
        typeof(bool),
        typeof(D2D1AnimatedPixelShaderPanel),
        new PropertyMetadata(false, OnIsPausedPropertyChanged));

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
    public PixelShaderEffect? PixelShaderEffect
    {
        get => (PixelShaderEffect?)GetValue(PixelShaderEffectProperty);
        set => SetValue(PixelShaderEffectProperty, value);
    }

    /// <summary>
    /// Gets or sets whether or not the rendering is paused.
    /// </summary>
    public bool IsPaused
    {
        get => (bool)GetValue(IsPausedProperty);
        set => SetValue(IsPausedProperty, value);
    }

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

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnPixelShaderEffectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        D2D1AnimatedPixelShaderPanel @this = (D2D1AnimatedPixelShaderPanel)d;
        PixelShaderEffect? pixelShaderEffect = (PixelShaderEffect?)e.NewValue;

        // Save the new pixel shader effect for later (as the dependency property cannot be accessed by the render thread)
        @this.pixelShaderEffect = pixelShaderEffect;

        // Pause or start the render thread if an effect is available
        if (@this.canvasAnimatedControl is { } canvasAnimatedControl)
        {
            bool shouldRender = @this.IsLoaded && !@this.IsPaused && pixelShaderEffect is not null;

            canvasAnimatedControl.Paused = !shouldRender;
        }
    }

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnIsPausedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        D2D1AnimatedPixelShaderPanel @this = (D2D1AnimatedPixelShaderPanel)d;
        bool isPaused = (bool)e.NewValue;

        if (@this.canvasAnimatedControl is { } canvasAnimatedControl)
        {
            bool shouldRender = @this.IsLoaded && !isPaused && @this.PixelShaderEffect is not null;

            canvasAnimatedControl.Paused = !shouldRender;
        }
    }
}