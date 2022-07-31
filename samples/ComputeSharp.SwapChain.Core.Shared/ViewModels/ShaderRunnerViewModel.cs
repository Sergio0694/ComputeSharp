using System;
using CommunityToolkit.Mvvm.ComponentModel;
#if WINDOWS_UWP
using ComputeSharp.Uwp;
#else
using ComputeSharp.WinUI;
#endif

namespace ComputeSharp.SwapChain.Core.ViewModels;

/// <summary>
/// A viewmodel for a compute shader.
/// </summary>
public sealed class ShaderRunnerViewModel : ObservableObject
{
    /// <summary>
    /// Creates a new <see cref="ShaderRunnerViewModel"/> instance.
    /// </summary>
    /// <param name="shaderType">The <see cref="Type"/> instance for the actual shader to execute.</param>
    /// <param name="shaderRunner">The <see cref="IShaderRunner"/> instance to execute.</param>
    public ShaderRunnerViewModel(Type shaderType, IShaderRunner shaderRunner)
    {
        ShaderType = shaderType;
        ShaderRunner = shaderRunner;
    }

    /// <summary>
    /// Gets the <see cref="Type"/> instance for the actual shader to execute.
    /// </summary>
    public Type ShaderType { get; }

    /// <summary>
    /// Gets the <see cref="IShaderRunner"/> instance to execute.
    /// </summary>
    public IShaderRunner ShaderRunner { get; }

    private bool isSelected;

    /// <summary>
    /// Gets or sets whether the current shader is selected.
    /// </summary>
    public bool IsSelected
    {
        get => this.isSelected;
        set => SetProperty(ref this.isSelected, value);
    }
}