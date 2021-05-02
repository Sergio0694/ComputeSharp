using System.Collections.Generic;
using ComputeSharp.SwapChain.Shaders;
using ComputeSharp.WinUI;
using CommunityToolkit.Mvvm.ComponentModel;
using ComputeSharp.SwapChain.WinUI.Models;
using CommunityToolkit.Mvvm.Input;

namespace ComputeSharp.SwapChain.WinUI.ViewModels
{
    /// <summary>
    /// The viewmodel for for a view that allows users to select shaders and customize settings.
    /// </summary>
    public sealed class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Creates a new <see cref="MainViewModel"/> instance.
        /// </summary>
        public MainViewModel()
        {
            this.selectedResolutionScale = 100;
            this.selectedComputeShader = ComputeShaderOptions[0];
            this.selectedComputeShader.IsSelected = true;

            ToggleRenderingPausedCommand = new RelayCommand(ToggleRenderingPaused);
        }

        /// <summary>
        /// Gets the available resolution scaling options (as percentage values).
        /// </summary>
        public IReadOnlyList<int> ResolutionScaleOptions { get; } = new[] { 25, 50, 75, 100 };

        private int selectedResolutionScale;

        /// <summary>
        /// Gets or sets the currently selected resolution scale setting (as percentage valaue).
        /// </summary>
        public int SelectedResolutionScale
        {
            get => this.selectedResolutionScale;
            set => SetProperty(ref this.selectedResolutionScale, value);
        }

        /// <summary>
        /// Gets the collection of available compute shader.
        /// </summary>
        public IReadOnlyList<ComputeShader> ComputeShaderOptions { get; } = new ComputeShader[]
        {
            new("Colorful infinity", new ShaderRunner<ColorfulInfinity>(static (texture, time) => new(texture, (float)time.TotalSeconds))),
            new("Extruded truchet", new ShaderRunner<ExtrudedTruchetPattern>(static (texture, time) => new(texture, (float)time.TotalSeconds))),
            new("Fractal tiling", new ShaderRunner<FractalTiling>(static (texture, time) => new(texture, (float)time.TotalSeconds))),
            new("Menger Journey", new ShaderRunner<MengerJourney>(static (texture, time) => new(texture, (float)time.TotalSeconds))),
            new("Octagrams", new ShaderRunner<Octagrams>(static (texture, time) => new(texture, (float)time.TotalSeconds))),
            new("Protean clouds", new ShaderRunner<ProteanClouds>(static (texture, time) => new(texture, (float)time.TotalSeconds))),
            new("Two tiled truchet", new ShaderRunner<TwoTiledTruchet>(static (texture, time) => new(texture, (float)time.TotalSeconds)))
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
            set => SetProperty(ref this.isRenderingPaused, value);
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
}
