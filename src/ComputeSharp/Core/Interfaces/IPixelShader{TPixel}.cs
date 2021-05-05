namespace ComputeSharp
{
    /// <summary>
    /// An <see langword="interface"/> representing a pixel shader.
    /// </summary>
    public interface IPixelShader<TPixel>
        where TPixel : unmanaged
    {
        /// <summary>
        /// Executes the current pixel shader.
        /// </summary>
        /// <returns>The pixel value for the current invocation.</returns>
        TPixel Execute();
    }
}
