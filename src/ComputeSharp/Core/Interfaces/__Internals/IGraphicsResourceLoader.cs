using System;

namespace ComputeSharp.__Internals;

/// <summary>
/// A type representing a loader object for <see cref="IGraphicsResource"/> values used in compute shaders.
/// </summary>
public interface IGraphicsResourceLoader
{
    /// <summary>
    /// Loads a resource used by the shader to be dispatched.
    /// </summary>
    /// <param name="resource">The input resource to be loaded.</param>
    /// <param name="index">The index to use to bind the resource to the shader.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="resource"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="resource"/> is not a valid resource object.</exception>
    /// <exception cref="GraphicsDeviceMismatchException">Thrown if the resource isn't associated with the same device used to dispatch the shader.</exception>
    void LoadGraphicsResource(IGraphicsResource resource, uint index);
}