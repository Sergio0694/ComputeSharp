namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// Indicates the type of a given input for a D2D1 pixel shader.
/// </summary>
public enum D2D1PixelShaderInputType
{
    /// <summary>
    /// The input is of simple type.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A pixel shader is of simple type when the consuming pixel shader requires only a single input value to perform its computation. This value would normally
    /// come from sampling an input texture at the texture coordinate emitted by the vertex shader. Such a pixel shader is said to perform simple sampling.
    /// </para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct2d/effect-shader-linking"/>.</para>
    /// </remarks>
    Simple,

    /// <summary>
    /// The input is of complex type.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Some pixel shaders, such as a Gaussian blur, compute their output from multiple input samples
    /// rather than just a single sample. Such a pixel shader is said to perform complex sampling.
    /// </para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct2d/effect-shader-linking"/>.</para>
    /// </remarks>
    Complex
}