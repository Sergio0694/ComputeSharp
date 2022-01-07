namespace ComputeSharp;

/// <summary>
/// An interface representing a pixel type and its normalized representation on the GPU side, when used within a shader.
/// </summary>
/// <typeparam name="T">The pixel type, when stored in memory (either on the CPU or the GPU side).</typeparam>
/// <typeparam name="TPixel">The type of pixel when normalized and used within a shader.</typeparam>
public interface IPixel<T, TPixel>
    where T : unmanaged, IPixel<T, TPixel>
    where TPixel : unmanaged
{
}
