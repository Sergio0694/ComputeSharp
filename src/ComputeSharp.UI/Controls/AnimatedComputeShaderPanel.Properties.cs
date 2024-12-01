using System;
using CommunityToolkit.WinUI;

#if !WINDOWS_UWP
using Microsoft.UI.Xaml;
#endif
using Windows.Foundation;
#if WINDOWS_UWP
using Windows.UI.Xaml;
#endif

#if WINDOWS_UWP
namespace ComputeSharp.Uwp;
#else
namespace ComputeSharp.WinUI;
#endif

/// <inheritdoc cref="AnimatedComputeShaderPanel"/>
partial class AnimatedComputeShaderPanel
{
    /// <summary>
    /// The <see cref="DependencyProperty"/> backing <see cref="ResolutionScale"/>.
    /// </summary>
    public static readonly DependencyProperty ResolutionScaleProperty = DependencyProperty.Register(
        nameof(ResolutionScale),
        typeof(double),
        typeof(AnimatedComputeShaderPanel),
        new PropertyMetadata(1.0, OnResolutionScalePropertyChanged));

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
    [GeneratedDependencyProperty]
    public partial IShaderRunner? ShaderRunner { get; set; }

    /// <summary>
    /// Gets or sets whether dynamic resolution is enabled. If it is, the internal render
    /// resolution of the shader being run will be automatically adjusted to reach 60fps.
    /// <para>The default value for this property is <see langword="true"/>.</para>
    /// </summary>
    [GeneratedDependencyProperty(DefaultValue = true)]
    public partial bool IsDynamicResolutionEnabled { get; set; }

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
    /// Gets or sets whether vertical sync is enabled.
    /// <para>The default value for this property is <see langword="true"/>.</para>
    /// </summary>
    [GeneratedDependencyProperty(DefaultValue = true)]
    public partial bool IsVerticalSyncEnabled { get; set; }

    /// <summary>
    /// Gets or sets whether or not the rendering is paused.
    /// </summary>
    [GeneratedDependencyProperty]
    public partial bool IsPaused { get; set; }

    /// <inheritdoc/>
    partial void OnShaderRunnerPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if (IsLoaded &&
            !IsPaused &&
            e.NewValue is IShaderRunner shaderRunner)
        {
            this.swapChainManager.StartRenderLoop(null, shaderRunner);
        }
        else
        {
            this.swapChainManager.StopRenderLoop();
        }
    }

    /// <inheritdoc/>
    partial void OnIsDynamicResolutionEnabledPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        bool isDynamicResolutionEnabled = (bool)e.NewValue;

        this.swapChainManager.QueueDynamicResolutionModeChange(isDynamicResolutionEnabled);
    }

    /// <inheritdoc cref="DependencyPropertyChangedCallback"/>
    private static void OnResolutionScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedComputeShaderPanel @this = (AnimatedComputeShaderPanel)d;
        double resolutionScale = (double)e.NewValue;

        @this.swapChainManager.QueueResolutionScaleChange(resolutionScale);
    }

    /// <inheritdoc/>
    partial void OnIsVerticalSyncEnabledPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        bool isVerticalSyncEnabled = (bool)e.NewValue;

        this.swapChainManager.QueueVerticalSyncModeChange(isVerticalSyncEnabled);
    }

    /// <inheritdoc/>
    partial void OnIsPausedPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if (IsLoaded &&
            !(bool)e.NewValue &&
            ShaderRunner is IShaderRunner shaderRunner)
        {
            this.swapChainManager.StartRenderLoop(null, shaderRunner);
        }
        else
        {
            this.swapChainManager.StopRenderLoop();
        }
    }
}