namespace ComputeSharp;

/// <summary>
/// An <see langword="interface"/> representing a compute shader writing into a target texture.
/// </summary>
/// <typeparam name="TPixel">The type of pixels being written by the compute shader.</typeparam>
/// <remarks>
/// This interface allows implementing logic analogous of pixel shaders, but via compute shaders.
/// </remarks>
public interface IComputeShader<TPixel>
    where TPixel : unmanaged
{
    /// <summary>
    /// Executes the current compute shader.
    /// </summary>
    /// <returns>The pixel value for the current invocation.</returns>
    TPixel Execute();
}