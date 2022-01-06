using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputeSharp.SwapChain.Core.Constants;
using ComputeSharp.SwapChain.Core.Models;
using ComputeSharp.SwapChain.Core.Services;
using ComputeSharp.SwapChain.Core.Shaders.Runners;
using ComputeSharp.SwapChain.Shaders;
#if WINDOWS_UWP
using ComputeSharp.Uwp;
#else
using ComputeSharp.WinUI;
#endif
using Microsoft.Toolkit.Diagnostics;

#nullable enable

namespace ComputeSharp.SwapChain.Core.ViewModels;

/// <summary>
/// The viewmodel for for a view that allows users to select shaders and customize settings.
/// </summary>
public sealed class MainViewModel : ObservableObject
{
    /// <summary>
    /// The <see cref="IAnalyticsService"/> instance currently in use.
    /// </summary>
    private readonly IAnalyticsService analyticsService;

    /// <summary>
    /// Creates a new <see cref="MainViewModel"/> instance.
    /// </summary>
    public MainViewModel(IAnalyticsService analyticsService)
    {
        Guard.IsNotNull(analyticsService, nameof(analyticsService));

        this.analyticsService = analyticsService;
        this.isDynamicResolutionEnabled = true;
        this.selectedResolutionScale = 100;
        this.selectedComputeShader = ComputeShaderOptions[0];
        this.selectedComputeShader.IsSelected = true;

        SetResolutionScaleCommand = new RelayCommand<int>(SetResolutionScale);
        ToggleRenderingPausedCommand = new RelayCommand(ToggleRenderingPaused);
    }

    /// <summary>
    /// Gets the available resolution scaling options (as percentage values).
    /// </summary>
    public IList<int> ResolutionScaleOptions { get; } = new[] { 25, 50, 75, 100 };

    private bool isDynamicResolutionEnabled;

    /// <summary>
    /// Gets or sets whether the dynamic resolution is enabled.
    /// </summary>
    public bool IsDynamicResolutionEnabled
    {
        get => this.isDynamicResolutionEnabled;
        set
        {
            if (SetProperty(ref this.isDynamicResolutionEnabled, value))
            {
                this.analyticsService.Log(Event.IsDynamicResolutionEnabledChanged, (nameof(value), value));
            }
        }
    }

    private int selectedResolutionScale;

    /// <summary>
    /// Gets the currently selected resolution scale setting (as percentage valaue).
    /// </summary>
    public int SelectedResolutionScale
    {
        get => this.selectedResolutionScale;
        private set
        {
            if (SetProperty(ref this.selectedResolutionScale, value))
            {
                this.analyticsService.Log(Event.SelectedResolutionScaleChanged, (nameof(value), value));
            }
        }
    }

    /// <summary>
    /// Gets the command that sets <see cref="SelectedResolutionScale"/>. 
    /// </summary>
    public IRelayCommand SetResolutionScaleCommand { get; }

    /// <summary>
    /// Sets <see cref="SelectedResolutionScale"/>.
    /// </summary>
    private void SetResolutionScale(int resolutionScale)
    {
        SelectedResolutionScale = resolutionScale;
    }

    /// <summary>
    /// Gets the collection of available compute shader.
    /// </summary>
    public IReadOnlyList<ComputeShader> ComputeShaderOptions { get; } = new ComputeShader[]
    {
        new("Colorful infinity", new ShaderRunner<ColorfulInfinity>(static time => new((float)time.TotalSeconds))),
        new("Extruded truchet", new ShaderRunner<ExtrudedTruchetPattern>(static time => new((float)time.TotalSeconds))),
        new("Fractal tiling", new ShaderRunner<FractalTiling>(static time => new((float)time.TotalSeconds))),
        new("Menger Journey", new ShaderRunner<MengerJourney>(static time => new((float)time.TotalSeconds))),
        new("Octagrams", new ShaderRunner<Octagrams>(static time => new((float)time.TotalSeconds))),
        new("Protean clouds", new ShaderRunner<ProteanClouds>(static time => new((float)time.TotalSeconds))),
        new("Two tiled truchet", new ShaderRunner<TwoTiledTruchet>(static time => new((float)time.TotalSeconds))),
        new("Pyramid pattern", new ShaderRunner<PyramidPattern>(static time => new((float)time.TotalSeconds))),
        new("Triangle grid contouring", new ShaderRunner<TriangleGridContouring>(static time => new((float)time.TotalSeconds))),
        new("Contoured layers", new ContouredLayersRunner())
    };

    private ComputeShader selectedComputeShader;

    /// <summary>
    /// Gets or sets the currently selected compute shader.
    /// </summary>
    public ComputeShader SelectedComputeShader
    {
        get => this.selectedComputeShader;
        set
        {
            this.selectedComputeShader.IsSelected = false;

            if (SetProperty(ref this.selectedComputeShader, value) &&
                value is not null)
            {
                this.analyticsService.Log(Event.SelectedComputeShaderChanged, (nameof(value.ShaderRunner), value.ShaderRunner));

                value.IsSelected = true;
            }
        }
    }

    private bool isRenderingPaused;

    /// <summary>
    /// Gets or sets whether the rendering is currently paused.
    /// </summary>
    public bool IsRenderingPaused
    {
        get => this.isRenderingPaused;
        set
        {
            if (SetProperty(ref this.isRenderingPaused, value))
            {
                this.analyticsService.Log(Event.IsRenderingPausedChanged, (nameof(value), value));
            }
        }
    }

    /// <summary>
    /// Gets the command that toggles <see cref="IsRenderingPaused"/>. 
    /// </summary>
    public IRelayCommand ToggleRenderingPausedCommand { get; }

    /// <summary>
    /// Toggles <see cref="IsRenderingPaused"/>.
    /// </summary>
    private void ToggleRenderingPaused()
    {
        IsRenderingPaused = !IsRenderingPaused;
    }
}
