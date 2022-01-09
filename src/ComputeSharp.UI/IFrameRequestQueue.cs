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
    /// <remarks>
    /// <para>
    /// This method should block synchronously until a new frame is available, returning its
    /// parameters if any. The <paramref name="token"/> parameter should be used to cancel a
    /// blocking operation and return immediately, as it's cancelled when rendering is stopped.
    /// </para>
    /// <para>
    /// For instance, if the <see cref="IFrameRequestQueue"/> implementation is internally using a
    /// <see cref="System.Collections.Concurrent.BlockingCollection{T}"/> queue and calling
    /// <see cref="System.Collections.Concurrent.BlockingCollection{T}.Take()"/> to block and wait for
    /// a new frame parameter, the <see cref="System.Collections.Concurrent.BlockingCollection{T}.Take(CancellationToken)"/>
    /// overload should be used instead, to enable canceling the operation early if needed.
    /// </para>
    /// <para>
    /// If the runner throws an <see cref="System.OperationCanceledException"/>, this will just be recognized by the
    /// <see cref="ComputeShaderPanel"/> in use and result in rendering simply being stopped. If any other exceptions are thrown
    /// instead, they will be interpreted as a failure, and <see cref="ComputeShaderPanel.RenderingFailed"/> will be raised.
    /// </para>
    /// </remarks>
    void Dequeue(out object? parameter, CancellationToken token);
}
