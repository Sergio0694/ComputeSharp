using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputeSharp.SwapChain.Core.Enums;
using ComputeSharp.SwapChain.Core.Shaders;
using ComputeSharp.SwapChain.Core.Shaders.Runners;
using ComputeSharp.SwapChain.Shaders;
#if WINDOWS_UWP
using ComputeSharp.Uwp;
#else
using ComputeSharp.WinUI;
#endif

#pragma warning disable IDE0300 // CsWinRT does not support marshalling collection expression (https://github.com/microsoft/CsWinRT/issues/1661)

namespace ComputeSharp.SwapChain.Core.ViewModels;

/// <summary>
/// The viewmodel for for a view that allows users to select shaders and customize settings.
/// </summary>
public sealed partial class MainViewModel : ObservableObject
{
    /// <summary>
    /// Creates a new <see cref="MainViewModel"/> instance.
    /// </summary>
    public MainViewModel()
    {
        SelectedComputeShader = ComputeShaderOptions[0];
        SelectedComputeShader.IsSelected = true;
    }

    /// <summary>
    /// Gets or sets the selected rendering mode.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsResolutionScaleOptionEnabled))]
    public partial RenderingMode SelectedRenderingMode { get; set; } = RenderingMode.DirectX12;

    /// <summary>
    /// Gets or sets whether the vertical sync is enabled.
    /// </summary>
    [ObservableProperty]
    public partial bool IsVerticalSyncEnabled { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the dynamic resolution is enabled.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsResolutionScaleOptionEnabled))]
    public partial bool IsDynamicResolutionEnabled { get; set; } = true;

    /// <summary>
    /// Gets the currently selected resolution scale setting (as percentage value).
    /// </summary>
    [ObservableProperty]
    public partial int SelectedResolutionScale { get; set; } = 100;

    /// <summary>
    /// Gets or sets the currently selected compute shader.
    /// </summary>
    [ObservableProperty]
    public partial ShaderRunnerViewModel SelectedComputeShader { get; set; }

    /// <summary>
    /// Gets or sets whether the rendering is currently paused.
    /// </summary>
    [ObservableProperty]
    public partial bool IsRenderingPaused { get; set; }

    /// <summary>
    /// Gets the available resolution scaling options (as percentage values).
    /// </summary>
    public IList<int> ResolutionScaleOptions { get; } = new int[] { 25, 50, 75, 100 };

    /// <summary>
    /// Gets the collection of available compute shader.
    /// </summary>
    public IReadOnlyList<ShaderRunnerViewModel> ComputeShaderOptions { get; } = new ShaderRunnerViewModel[]
    {
        new(
            nameof(ColorfulInfinity),
            new ShaderRunner<ColorfulInfinity>(static time => new((float)time.TotalSeconds)),
            new PixelShaderEffect.For<SwapChain.Shaders.D2D1.ColorfulInfinity>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        new(
            nameof(FractalTiling),
            new ShaderRunner<FractalTiling>(static time => new((float)time.TotalSeconds)),
            new PixelShaderEffect.For<SwapChain.Shaders.D2D1.FractalTiling>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        new(
            nameof(MengerJourney),
            new ShaderRunner<MengerJourney>(static time => new((float)time.TotalSeconds)),
            new PixelShaderEffect.For<SwapChain.Shaders.D2D1.MengerJourney>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        new(
            nameof(Octagrams),
            new ShaderRunner<Octagrams>(static time => new((float)time.TotalSeconds)),
            new PixelShaderEffect.For<SwapChain.Shaders.D2D1.Octagrams>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        new(
            nameof(ProteanClouds),
            new ShaderRunner<ProteanClouds>(static time => new((float)time.TotalSeconds)),
            new PixelShaderEffect.For<SwapChain.Shaders.D2D1.ProteanClouds>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        new(
            nameof(TwoTiledTruchet),
            new ShaderRunner<TwoTiledTruchet>(static time => new((float)time.TotalSeconds)),
            new PixelShaderEffect.For<SwapChain.Shaders.D2D1.TwoTiledTruchet>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        new(
            nameof(PyramidPattern),
            new ShaderRunner<PyramidPattern>(static time => new((float)time.TotalSeconds)),
            new PixelShaderEffect.For<SwapChain.Shaders.D2D1.PyramidPattern>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        new(
            nameof(TriangleGridContouring),
            new ShaderRunner<TriangleGridContouring>(static time => new((float)time.TotalSeconds)),
            new PixelShaderEffect.For<SwapChain.Shaders.D2D1.TriangleGridContouring>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        new(nameof(ContouredLayers), new ContouredLayersRunner(), new D2D1ContouredLayersEffect()),
        new(
            nameof(TerracedHills),
            new ShaderRunner<TerracedHills>(static time => new((float)time.TotalSeconds)),
            new PixelShaderEffect.For<SwapChain.Shaders.D2D1.TerracedHills>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
    };

    /// <summary>
    /// Checks whether the resolution scale can currently be explicitly set.
    /// </summary>
    public bool IsResolutionScaleOptionEnabled => SelectedRenderingMode == RenderingMode.DirectX12 && !IsDynamicResolutionEnabled;

    /// <summary>
    /// Sets <see cref="SelectedRenderingMode"/>.
    /// </summary>
    [RelayCommand]
    private void SetRenderingMode(RenderingMode renderingMode)
    {
        SelectedRenderingMode = renderingMode;
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

    /// <inheritdoc/>
    partial void OnSelectedComputeShaderChanging(ShaderRunnerViewModel oldValue, ShaderRunnerViewModel newValue)
    {
        oldValue.IsSelected = false;
        newValue.IsSelected = true;
    }
}