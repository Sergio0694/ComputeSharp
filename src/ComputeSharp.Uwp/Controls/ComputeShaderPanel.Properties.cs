using System;
using Windows.UI.Xaml;

#nullable enable

namespace ComputeSharp.Uwp;

/// <inheritdoc cref="ComputeShaderPanel"/>
public sealed unsafe partial class ComputeShaderPanel
{
    /// <summary>
    /// Gets or sets the <see cref="IShaderRunner"/> instance to use to render content.
    /// </summary>
    public IShaderRunner? ShaderRunner
    {
        get => (IShaderRunner?)GetValue(ShaderFactoryProperty);
        set => SetValue(ShaderFactoryProperty, value);
    }

    /// <summary>
    /// The <see cref="DependencyProperty"/> backing <see cref="ShaderRunner"/>.
    /// </summary>
    public static readonly DependencyProperty ShaderFactoryProperty = DependencyProperty.Register(
        nameof(ShaderRunner),
        typeof(IShaderRunner),
        typeof(ComputeShaderPanel),
        new PropertyMetadata(null, static (d, e) => ((ComputeShaderPanel)d).shaderRunner = (IShaderRunner?)e.NewValue));

    /// <summary>
    /// Gets or sets the resolution scale to be used to render frames.
    /// This can help achieve higher framerates with more complex shaders.
    /// </summary>
    /// <remarks>The accepted range is [0.1 and 1] (default is 1, exceeding values will be clamped).</remarks>
    public double ResolutionScale
    {
        get => (double)GetValue(ResolutionScaleProperty);
        set => SetValue(ResolutionScaleProperty, Math.Clamp(value, 0.1, 1.0));
    }

    /// <summary>
    /// The <see cref="DependencyProperty"/> backing <see cref="ResolutionScale"/>.
    /// </summary>
    public static readonly DependencyProperty ResolutionScaleProperty = DependencyProperty.Register(
        nameof(ResolutionScale),
        typeof(double),
        typeof(ComputeShaderPanel),
        new PropertyMetadata(1.0, OnResolutionScalePropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnResolutionScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var @this = (ComputeShaderPanel)d;
        var resolutionScale = (double)e.NewValue;

        @this.targetResolutionScale = (float)resolutionScale;

        if (!@this.isDynamicResolutionEnabled)
        {
            @this.OnResize();
        }
    }

    /// <summary>
    /// Gets or sets whether dynamic resolution is enabled. If it is, the internal render
    /// resolution of the shader being run will be automatically adjusted to reach 60fps.
    /// <para>The default value for this property is <see langword="true"/>.</para>
    /// </summary>
    public bool IsDynamicResolutionEnabled
    {
        get => (bool)GetValue(IsDynamicResolutionEnabledProperty);
        set => SetValue(IsDynamicResolutionEnabledProperty, value);
    }

    /// <summary>
    /// The <see cref="DependencyProperty"/> backing <see cref="IsDynamicResolutionEnabled"/>.
    /// </summary>
    public static readonly DependencyProperty IsDynamicResolutionEnabledProperty = DependencyProperty.Register(
        nameof(IsDynamicResolutionEnabled),
        typeof(bool),
        typeof(ComputeShaderPanel),
        new PropertyMetadata(true, OnIsDynamicResolutionEnabledPropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnIsDynamicResolutionEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var @this = (ComputeShaderPanel)d;
        var isDynamicResolutionEnabled = (bool)e.NewValue;

        @this.isDynamicResolutionEnabled = isDynamicResolutionEnabled;
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
        typeof(ComputeShaderPanel),
        new PropertyMetadata(false, OnIsPausedPropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnIsPausedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var @this = (ComputeShaderPanel)d;
        var isPaused = (bool)e.NewValue;

        if (isPaused)
        {
            @this.OnStopRenderLoop();
        }
        else
        {
            @this.OnStartRenderLoop();
        }
    }
}