using System.Threading;

#if WINDOWS_UWP
namespace ComputeSharp.Uwp;
#else
namespace ComputeSharp.WinUI;
#endif

/// <summary>
/// An interface for a shader runner to be used with <see cref="ComputeShaderPanel"/>.
/// </summary>
public interface IFrameRequestQueue
{
    /// <summary>
    /// Waits for a new frame request to be available, retrieving its parameter, if any.
    /// </summary>
    /// <param name="parameter">The input parameter for the frame to render.</param>
    /// <param name="token">A token to cancel waiting for a new frame.</param>
    void Dequeue(out object? parameter, CancellationToken token);
}
