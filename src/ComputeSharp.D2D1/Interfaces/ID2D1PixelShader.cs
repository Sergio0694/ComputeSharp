namespace ComputeSharp.D2D1;

/// <summary>
/// An <see langword="interface"/> representing a D2D1 pixel shader.
/// </summary>
public interface ID2D1PixelShader
{
    /// <summary>
    /// Executes the current pixel shader.
    /// </summary>
    /// <returns>The pixel value for the current invocation.</returns>
    Float4 Execute();
}