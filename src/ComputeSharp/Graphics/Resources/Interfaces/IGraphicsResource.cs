namespace ComputeSharp;

/// <summary>
/// An interface representing a graphics resource associated to a given <see cref="GraphicsDevice"/> instance.
/// </summary>
public interface IGraphicsResource
{
    /// <summary>
    /// Gets the <see cref="ComputeSharp.GraphicsDevice"/> instance associated with the current resource.
    /// </summary>
    GraphicsDevice GraphicsDevice { get; }
}