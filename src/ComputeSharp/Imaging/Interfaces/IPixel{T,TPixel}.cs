namespace ComputeSharp;

/// <summary>
/// An interface representing a pixel type and its normalized representation on the GPU side, when used within a shader.
/// </summary>
/// <typeparam name="T">The pixel type, when stored in memory (either on the CPU or the GPU side).</typeparam>
/// <typeparam name="TPixel">The type of pixel when normalized and used within a shader.</typeparam>
/// <remarks>This interface is not meant to be implemented by user defined types.</remarks>
public interface IPixel<T, TPixel>
    where T : unmanaged, IPixel<T, TPixel>
    where TPixel : unmanaged
{
    /// <summary>
    /// Converts the current <typeparamref name="T"/> value into its normalized <typeparamref name="TPixel"/> representation.
    /// </summary>
    /// <returns>The <typeparamref name="TPixel"/> representation for the current value.</returns>
    /// <remarks>
    /// This method is primarily meant to be used to support
    /// <see cref="ComputeContextExtensions.Fill{TPixel}(ref readonly ComputeContext, IReadWriteNormalizedTexture2D{TPixel}, TPixel)"/>
    /// and <see cref="ComputeContextExtensions.Fill{TPixel}(ref readonly ComputeContext, IReadWriteNormalizedTexture3D{TPixel}, TPixel)"/>.
    /// </remarks>
    TPixel ToPixel();
}