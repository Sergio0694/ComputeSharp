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
/// <param name="shaderType">The name of the shader type to execute.</param>
/// <param name="shaderRunner">The <see cref="IShaderRunner"/> instance to execute.</param>
/// <param name="pixelShaderEffect">The <see cref="Shaders.PixelShaderEffect"/> instance to execute.</param>
public sealed partial class ShaderRunnerViewModel(
    string shaderType,
    IShaderRunner shaderRunner,
    PixelShaderEffect pixelShaderEffect) : ObservableObject
{
    /// <summary>
    /// Gets the name of the shader type to execute.
    /// </summary>
    public string ShaderType => shaderType;

    /// <summary>
    /// Gets the <see cref="IShaderRunner"/> instance to execute.
    /// </summary>
    public IShaderRunner ShaderRunner => shaderRunner;

    /// <summary>
    /// Gets the <see cref="PixelShaderEffect"/> instance to execute.
    /// </summary>
    public PixelShaderEffect PixelShaderEffect => pixelShaderEffect;

    /// <summary>
    /// Gets or sets whether the current shader is selected.
    /// </summary>
    [ObservableProperty]
    private bool isSelected;
}