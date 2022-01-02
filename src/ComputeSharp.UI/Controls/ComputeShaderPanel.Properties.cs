using System;
#if WINDOWS_UWP
using Windows.UI.Xaml;
#else
using Microsoft.UI.Xaml;
#endif

#if WINDOWS_UWP
namespace ComputeSharp.Uwp;
#else
namespace ComputeSharp.WinUI;
#endif

/// <inheritdoc cref="ComputeShaderPanel"/>
partial class ComputeShaderPanel
{
    /// <summary>
    /// Gets or sets the <see cref="IFrameRequestQueue"/> instance to use to request new frames.
    /// </summary>
    public IFrameRequestQueue? FrameRequestQueue
    {
        get => (IFrameRequestQueue)GetValue(FrameRequestQueueProperty);
        set => SetValue(FrameRequestQueueProperty, value);
    }

    /// <summary>
    /// The <see cref="DependencyProperty"/> backing <see cref="ShaderRunner"/>.
    /// </summary>
    public static readonly DependencyProperty FrameRequestQueueProperty = DependencyProperty.Register(
        nameof(FrameRequestQueue),
        typeof(IFrameRequestQueue),
        typeof(ComputeShaderPanel),
        new PropertyMetadata(null, OnFrameRequestQueueChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnFrameRequestQueueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var @this = (ComputeShaderPanel)d;
        var frameRequestQueue = (IFrameRequestQueue?)e.NewValue;

        if (@this.IsLoaded &&
            !@this.IsPaused &&
            @this.ShaderRunner is IShaderRunner shaderRunner)
        {
            @this.swapChainManager.StartRenderLoop(frameRequestQueue, shaderRunner);
        }
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
        typeof(ComputeShaderPanel),
        new PropertyMetadata(null, OnShaderRunnerPropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnShaderRunnerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var @this = (ComputeShaderPanel)d;
        var shaderRunner = (IShaderRunner?)e.NewValue;

        if (shaderRunner is null)
        {
            @this.swapChainManager.StopRenderLoop();
        }
        else if (@this.IsLoaded &&
                 !@this.IsPaused)
        {
            @this.swapChainManager.StartRenderLoop(@this.FrameRequestQueue, shaderRunner);
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
        typeof(ComputeShaderPanel),
        new PropertyMetadata(1.0, OnResolutionScalePropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnResolutionScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var @this = (ComputeShaderPanel)d;
        var resolutionScale = (double)e.NewValue;

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
        typeof(ComputeShaderPanel),
        new PropertyMetadata(true, OnIsDynamicResolutionEnabledPropertyChanged));

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnIsDynamicResolutionEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var @this = (ComputeShaderPanel)d;
        var isDynamicResolutionEnabled = (bool)e.NewValue;

        @this.swapChainManager.QueueDynamicResolutionModeChange(isDynamicResolutionEnabled);
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
            @this.swapChainManager.StopRenderLoop();
        }
        else if (@this.IsLoaded &&
                 @this.ShaderRunner is IShaderRunner shaderRunner)
        {
            @this.swapChainManager.StartRenderLoop(@this.FrameRequestQueue, shaderRunner);
        }
    }
}
