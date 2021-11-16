using System;

namespace ComputeSharp.Uwp;

/// <summary>
/// An interface for a shader runner to be used with <see cref="ComputeShaderPanel"/>.
/// </summary>
public interface IShaderRunner
{
    /// <summary>
    /// Renders a single frame to a texture.
    /// </summary>
    /// <param name="texture">The target texture to render the frame to.</param>
    /// <param name="timespan">The timespan for the current frame.</param>
    public void Execute(IReadWriteTexture2D<Float4> texture, TimeSpan timespan);
}