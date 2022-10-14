namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// Indicates a type of shader being processed.
/// </summary>
public enum ShaderType
{
    /// <summary>
    /// A compute shader.
    /// </summary>
    ComputeShader,

    /// <summary>
    /// A pixel shader.
    /// </summary>
    PixelShader
}