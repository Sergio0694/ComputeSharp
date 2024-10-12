using System;
#if WINDOWS_UWP
using Windows.UI.Xaml;
#else
using Microsoft.UI.Xaml;
#endif
using Windows.Foundation;

#if WINDOWS_UWP
namespace ComputeSharp.Uwp;
#else
namespace ComputeSharp.WinUI;
#endif

/// <inheritdoc cref="AnimatedComputeShaderPanel"/>
partial class AnimatedComputeShaderPanel
{
    /// <summary>
    /// Raised whenever rendering starts.
    /// </summary>
    public event TypedEventHandler<AnimatedComputeShaderPanel, EventArgs>? RenderingStarted
    {
        add => this.swapChainManager.RenderingStarted += value;
        remove => this.swapChainManager.RenderingStarted -= value;
    }

    /// <summary>
    /// Raised whenever rendering stops.
    /// </summary>
    public event TypedEventHandler<AnimatedComputeShaderPanel, EventArgs>? RenderingStopped
    {
        add => this.swapChainManager.RenderingStopped += value;
        remove => this.swapChainManager.RenderingStopped -= value;
    }

    /// <summary>
    /// Raised whenever rendering fails.
    /// </summary>
    public event TypedEventHandler<AnimatedComputeShaderPanel, RenderingFailedEventArgs>? RenderingFailed
    {
        add => this.swapChainManager.RenderingFailed += value;
        remove => this.swapChainManager.RenderingFailed -= value;
    }

    /// <summary>
    /// Raised whenever the <see cref="AnimatedComputeShaderPanel"/> control is disposed and all underlying resources are released.
    /// </summary>
    public event TypedEventHandler<AnimatedComputeShaderPanel, EventArgs>? Disposed
    {
        add => this.swapChainManager.Disposed += value;
        remove => this.swapChainManager.Disposed -= value;
    }

    /// <summary>
    /// Gets or sets the <see cref="IShaderRunner"/> instance to use to render content.
    /// </summary>
    public IShaderRunner? ShaderRunner
    {
        get => (IShaderRunner?)GetValue(ShaderRunnerProperty);
        set => SetValue(ShaderRunnerProperty, value);
    }

    /// <summary>
    /// The <see cref="DependencyProperty"/> backing <see cref="ShaderRunner"/>.
    /// </summary>
    public static readonly DependencyProperty ShaderRunnerProperty = DependencyProperty.Register(
        nameof(ShaderRunner),
        typeof(IShaderRunner),
        typeof(AnimatedComputeShaderPanel),
        new PropertyMetadata(null, OnShaderRunnerPropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnShaderRunnerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedComputeShaderPanel @this = (AnimatedComputeShaderPanel)d;

        if (@this.IsLoaded &&
            !@this.IsPaused &&
            e.NewValue is IShaderRunner shaderRunner)
        {
            @this.swapChainManager.StartRenderLoop(null, shaderRunner);
        }
        else
        {
            @this.swapChainManager.StopRenderLoop();
        }
    }

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
        typeof(AnimatedComputeShaderPanel),
        new PropertyMetadata(1.0, OnResolutionScalePropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnResolutionScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedComputeShaderPanel @this = (AnimatedComputeShaderPanel)d;
        double resolutionScale = (double)e.NewValue;

        @this.swapChainManager.QueueResolutionScaleChange(resolutionScale);
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
        typeof(AnimatedComputeShaderPanel),
        new PropertyMetadata(true, OnIsDynamicResolutionEnabledPropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnIsDynamicResolutionEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedComputeShaderPanel @this = (AnimatedComputeShaderPanel)d;
        bool isDynamicResolutionEnabled = (bool)e.NewValue;

        @this.swapChainManager.QueueDynamicResolutionModeChange(isDynamicResolutionEnabled);
    }

    /// <summary>
    /// Gets or sets whether vertical sync is enabled.
    /// <para>The default value for this property is <see langword="true"/>.</para>
    /// </summary>
    public bool IsVerticalSyncEnabled
    {
        get => (bool)GetValue(IsVerticalSyncEnabledProperty);
        set => SetValue(IsVerticalSyncEnabledProperty, value);
    }

    /// <summary>
    /// The <see cref="DependencyProperty"/> backing <see cref="IsVerticalSyncEnabled"/>.
    /// </summary>
    public static readonly DependencyProperty IsVerticalSyncEnabledProperty = DependencyProperty.Register(
        nameof(IsVerticalSyncEnabled),
        typeof(bool),
        typeof(AnimatedComputeShaderPanel),
        new PropertyMetadata(true, OnIsVerticalSyncEnabledPropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnIsVerticalSyncEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedComputeShaderPanel @this = (AnimatedComputeShaderPanel)d;
        bool isVerticalSyncEnabled = (bool)e.NewValue;

        @this.swapChainManager.QueueVerticalSyncModeChange(isVerticalSyncEnabled);
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
        typeof(AnimatedComputeShaderPanel),
        new PropertyMetadata(false, OnIsPausedPropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnIsPausedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedComputeShaderPanel @this = (AnimatedComputeShaderPanel)d;

        if (@this.IsLoaded &&
            !(bool)e.NewValue &&
            @this.ShaderRunner is IShaderRunner shaderRunner)
        {
            @this.swapChainManager.StartRenderLoop(null, shaderRunner);
        }
        else
        {
            @this.swapChainManager.StopRenderLoop();
        }
    }
}