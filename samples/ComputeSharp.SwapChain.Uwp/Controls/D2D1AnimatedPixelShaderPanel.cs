using ComputeSharp.SwapChain.Core.Shaders;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable enable

namespace ComputeSharp.SwapChain.Uwp.Views;

/// <summary>
/// A custom <see cref="Control"/> that can be used to render animated backgrounds via Win2D.
/// </summary>
[TemplatePart(Name = "PART_CanvasAnimatedControl", Type = typeof(CanvasAnimatedControl))]
public sealed class D2D1AnimatedPixelShaderPanel : Control
{
    /// <summary>
    /// The wrapped <see cref="CanvasAnimatedControl"/> instance used to render frames.
    /// </summary>
    private CanvasAnimatedControl? canvasAnimatedControl;

    /// <summary>
    /// The <see cref="ID2D1ShaderRunner"/> instance currently in use, if any.
    /// </summary>
    private volatile ID2D1ShaderRunner? shaderRunner;

    /// <summary>
    /// Creates a new <see cref="D2D1AnimatedPixelShaderPanel"/> instance.
    /// </summary>
    public D2D1AnimatedPixelShaderPanel()
    {
        DefaultStyleKey = typeof(D2D1AnimatedPixelShaderPanel);

        Loaded += D2D1AnimatedPixelShaderPanel_Loaded;
        Unloaded += D2D1AnimatedPixelShaderPanel_Unloaded;
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
            ShaderRunner is not null &&
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
    /// <inheritdoc cref="ID2D1ShaderRunner.Execute"/>
    private void CanvasAnimatedControl_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
    {
        this.shaderRunner?.Execute(sender, args);
    }

    /// <summary>
    /// Gets or sets the <see cref="ID2D1ShaderRunner"/> instance to use to render content.
    /// </summary>
    public ID2D1ShaderRunner? ShaderRunner
    {
        get => (ID2D1ShaderRunner?)GetValue(ShaderRunnerProperty);
        set => SetValue(ShaderRunnerProperty, value);
    }

    /// <summary>
    /// The <see cref="DependencyProperty"/> backing <see cref="ShaderRunner"/>.
    /// </summary>
    public static readonly DependencyProperty ShaderRunnerProperty = DependencyProperty.Register(
        nameof(ShaderRunner),
        typeof(ID2D1ShaderRunner),
        typeof(D2D1AnimatedPixelShaderPanel),
        new PropertyMetadata(null, OnShaderRunnerPropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnShaderRunnerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        D2D1AnimatedPixelShaderPanel @this = (D2D1AnimatedPixelShaderPanel)d;
        ID2D1ShaderRunner? shaderRunner = (ID2D1ShaderRunner?)e.NewValue;

        // Save the new shader runner for later (as the dependency property cannot be accessed by the render thread)
        @this.shaderRunner = shaderRunner;

        // Pause or start the render thread if a runner is available
        if (@this.canvasAnimatedControl is { } canvasAnimatedControl)
        {
            bool shouldRender = @this.IsLoaded && !@this.IsPaused && shaderRunner is not null;

            canvasAnimatedControl.Paused = !shouldRender;
        }
    }

    /// <summary>
    /// Gets or sets whether or not the rendering is paused.
    /// </summary>
    public bool IsPaused
    {
        get => (bool)GetValue(IsPausedProperty);
        set => SetValue(IsPausedProperty, value);
    }

    /// <summary>
    /// The <see cref="DependencyProperty"/> backing <see cref="IsPaused"/>.
    /// </summary>
    public static readonly DependencyProperty IsPausedProperty = DependencyProperty.Register(
        nameof(IsPaused),
        typeof(bool),
        typeof(D2D1AnimatedPixelShaderPanel),
        new PropertyMetadata(false, OnIsPausedPropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnIsPausedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        D2D1AnimatedPixelShaderPanel @this = (D2D1AnimatedPixelShaderPanel)d;
        bool isPaused = (bool)e.NewValue;

        if (@this.canvasAnimatedControl is { } canvasAnimatedControl)
        {
            bool shouldRender = @this.IsLoaded && !isPaused && @this.ShaderRunner is ID2D1ShaderRunner;

            canvasAnimatedControl.Paused = !shouldRender;
        }
    }
}