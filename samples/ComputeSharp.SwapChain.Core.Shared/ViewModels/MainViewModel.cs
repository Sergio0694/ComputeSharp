using System.Collections.Generic;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputeSharp.SwapChain.Core.Constants;
using ComputeSharp.SwapChain.Core.Services;
using ComputeSharp.SwapChain.Core.Shaders.Runners;
using ComputeSharp.SwapChain.Shaders;
#if WINDOWS_UWP
using ComputeSharp.Uwp;
#else
using ComputeSharp.WinUI;
#endif

#nullable enable

namespace ComputeSharp.SwapChain.Core.ViewModels;

/// <summary>
/// The viewmodel for for a view that allows users to select shaders and customize settings.
/// </summary>
public sealed partial class MainViewModel : ObservableObject
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
        Guard.IsNotNull(analyticsService);

        this.analyticsService = analyticsService;
        this.isVerticalSyncEnabled = true;
        this.isDynamicResolutionEnabled = true;
        this.selectedResolutionScale = 100;
        this.selectedComputeShader = ComputeShaderOptions[0];
        this.selectedComputeShader.IsSelected = true;
    }

    /// <summary>
    /// Gets the available resolution scaling options (as percentage values).
    /// </summary>
    public IList<int> ResolutionScaleOptions { get; } = new[] { 25, 50, 75, 100 };

    private bool isVerticalSyncEnabled;

    /// <summary>
    /// Gets or sets whether the vertical sync is enabled.
    /// </summary>
    public bool IsVerticalSyncEnabled
    {
        get => this.isVerticalSyncEnabled;
        set
        {
            if (SetProperty(ref this.isVerticalSyncEnabled, value))
            {
                this.analyticsService.Log(Event.IsVerticalSyncEnabledChanged, (nameof(value), value));
            }
        }
    }

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
    /// Gets the collection of available compute shader.
    /// </summary>
    public IReadOnlyList<ShaderRunnerViewModel> ComputeShaderOptions { get; } = new ShaderRunnerViewModel[]
    {
        new(typeof(ColorfulInfinity), new ShaderRunner<ColorfulInfinity>(static time => new((float)time.TotalSeconds))),
        new(typeof(ExtrudedTruchetPattern),new ShaderRunner<ExtrudedTruchetPattern>(static time => new((float)time.TotalSeconds))),
        new(typeof(FractalTiling),new ShaderRunner<FractalTiling>(static time => new((float)time.TotalSeconds))),
        new(typeof(MengerJourney),new ShaderRunner<MengerJourney>(static time => new((float)time.TotalSeconds))),
        new(typeof(Octagrams),new ShaderRunner<Octagrams>(static time => new((float)time.TotalSeconds))),
        new(typeof(ProteanClouds),new ShaderRunner<ProteanClouds>(static time => new((float)time.TotalSeconds))),
        new(typeof(TwoTiledTruchet),new ShaderRunner<TwoTiledTruchet>(static time => new((float)time.TotalSeconds))),
        new(typeof(PyramidPattern),new ShaderRunner<PyramidPattern>(static time => new((float)time.TotalSeconds))),
        new(typeof(TriangleGridContouring),new ShaderRunner<TriangleGridContouring>(static time => new((float)time.TotalSeconds))),
        new(typeof(ContouredLayers),new ContouredLayersRunner())
    };

    private ShaderRunnerViewModel selectedComputeShader;

    /// <summary>
    /// Gets or sets the currently selected compute shader.
    /// </summary>
    public ShaderRunnerViewModel SelectedComputeShader
    {
        get => this.selectedComputeShader;
        set
        {
            this.selectedComputeShader.IsSelected = false;

            if (SetProperty(ref this.selectedComputeShader, value) &&
                value is not null)
            {
                this.analyticsService.Log(Event.SelectedComputeShaderChanged, (nameof(value.ShaderType), value.ShaderType.Name));

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
    /// Sets <see cref="SelectedResolutionScale"/>.
    /// </summary>
    [RelayCommand]
    private void SetResolutionScale(int resolutionScale)
    {
        SelectedResolutionScale = resolutionScale;
    }

    /// <summary>
    /// Toggles <see cref="IsRenderingPaused"/>.
    /// </summary>
    [RelayCommand]
    private void ToggleRenderingPaused()
    {
        IsRenderingPaused = !IsRenderingPaused;
    }
}
