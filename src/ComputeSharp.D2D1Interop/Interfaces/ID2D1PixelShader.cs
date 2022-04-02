using ComputeSharp.D2D1Interop.__Internals;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1Interop;

/// <summary>
/// An <see langword="interface"/> representing a D2D1 pixel shader.
/// </summary>
public interface ID2D1PixelShader : ID2D1Shader
{
    /// <summary>
    /// Executes the current pixel shader.
    /// </summary>
    /// <returns>The pixel value for the current invocation.</returns>
    Float4 Execute();
}