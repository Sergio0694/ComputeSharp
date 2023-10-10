using CommunityToolkit.Mvvm.ComponentModel;
using ComputeSharp.SwapChain.Core.Shaders;
using ComputeSharp.WinUI;

namespace ComputeSharp.SwapChain.Core.ViewModels;

/// <summary>
/// A viewmodel for a compute shader.
/// </summary>
public sealed partial class ShaderRunnerViewModel : ObservableObject
{
    /// <summary>
    /// Creates a new <see cref="ShaderRunnerViewModel"/> instance.
    /// </summary>
    /// <param name="shaderType">The name of the shader type to execute.</param>
    /// <param name="shaderRunner">The <see cref="IShaderRunner"/> instance to execute.</param>
    /// <param name="pixelShaderEffect">The <see cref="Shaders.PixelShaderEffect"/> instance to execute.</param>
    public ShaderRunnerViewModel(string shaderType, IShaderRunner shaderRunner, PixelShaderEffect pixelShaderEffect)
    {
        ShaderType = shaderType;
        ShaderRunner = shaderRunner;
        PixelShaderEffect = pixelShaderEffect;
    }

    /// <summary>
    /// Gets the name of the shader type to execute.
    /// </summary>
    public string ShaderType { get; }

    /// <summary>
    /// Gets the <see cref="IShaderRunner"/> instance to execute.
    /// </summary>
    public IShaderRunner ShaderRunner { get; }

    /// <summary>
    /// Gets the <see cref="PixelShaderEffect"/> instance to execute.
    /// </summary>
    public PixelShaderEffect PixelShaderEffect { get; }

    /// <summary>
    /// Gets or sets whether the current shader is selected.
    /// </summary>
    [ObservableProperty]
    private bool isSelected;
}