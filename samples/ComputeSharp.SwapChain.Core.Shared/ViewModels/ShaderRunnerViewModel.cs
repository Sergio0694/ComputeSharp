using System;
using CommunityToolkit.Mvvm.ComponentModel;
using ComputeSharp.SwapChain.Core.Shaders;
#if WINDOWS_UWP
using ComputeSharp.Uwp;
#else
using ComputeSharp.WinUI;
#endif

namespace ComputeSharp.SwapChain.Core.ViewModels;

/// <summary>
/// A viewmodel for a compute shader.
/// </summary>
public sealed partial class ShaderRunnerViewModel : ObservableObject
{
    /// <summary>
    /// Creates a new <see cref="ShaderRunnerViewModel"/> instance.
    /// </summary>
    /// <param name="shaderType">The <see cref="Type"/> instance for the actual shader to execute.</param>
    /// <param name="shaderRunner">The <see cref="IShaderRunner"/> instance to execute.</param>
    /// <param name="d2D1ShaderRunner">The <see cref="ID2D1ShaderRunner"/> instance to execute.</param>
    public ShaderRunnerViewModel(Type shaderType, IShaderRunner shaderRunner, ID2D1ShaderRunner d2D1ShaderRunner)
    {
        ShaderType = shaderType;
        ShaderRunner = shaderRunner;
        D2D1ShaderRunner = d2D1ShaderRunner;
    }

    /// <summary>
    /// Gets the <see cref="Type"/> instance for the actual shader to execute.
    /// </summary>
    public Type ShaderType { get; }

    /// <summary>
    /// Gets the <see cref="IShaderRunner"/> instance to execute.
    /// </summary>
    public IShaderRunner ShaderRunner { get; }

    /// <summary>
    /// Gets the <see cref="ID2D1ShaderRunner"/> instance to execute.
    /// </summary>
    public ID2D1ShaderRunner D2D1ShaderRunner { get; }

    /// <summary>
    /// Gets or sets whether the current shader is selected.
    /// </summary>
    [ObservableProperty]
    private bool isSelected;
}