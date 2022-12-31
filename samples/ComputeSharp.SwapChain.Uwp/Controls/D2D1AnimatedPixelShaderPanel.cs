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
    }

    /// <inheritdoc/>
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        this.canvasAnimatedControl = (CanvasAnimatedControl)GetTemplateChild("PART_CanvasAnimatedControl")!;
        this.canvasAnimatedControl.Draw += CanvasAnimatedControl_Draw;
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
            canvasAnimatedControl.Paused = e.NewValue is not ID2D1ShaderRunner;
        }
    }
}