using System;

#if WINDOWS_UWP
namespace ComputeSharp.Uwp;
#else
namespace ComputeSharp.WinUI;
#endif

/// <summary>
/// An interface for a shader runner to be used with <see cref="ComputeShaderPanel"/> or <see cref="AnimatedComputeShaderPanel"/>.
/// </summary>
public interface IShaderRunner
{
    /// <summary>
    /// Tries to render a single frame to a texture, optionally skipping the frame if needed.
    /// </summary>
    /// <param name="texture">The target texture to render the frame to.</param>
    /// <param name="timespan">The timespan for the current frame.</param>
    /// <param name="parameter">The input parameter for the frame being rendered.</param>
    /// <returns>Whether or not to present the current frame. If <see langword="false"/>, the frame will be skipped.</returns>
    /// <remarks>
    /// Any exceptions thrown by the runner will result in <see cref="ComputeShaderPanel.RenderingFailed"/>
    /// or <see cref="AnimatedComputeShaderPanel.RenderingFailed"/> to be raised.
    /// </remarks>
    bool TryExecute(IReadWriteTexture2D<Float4> texture, TimeSpan timespan, object? parameter);
}
