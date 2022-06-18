using System;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Interop;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods using PIX APIs for the <see cref="ComputeContext"/> type.
/// </summary>
public static class ComputeContextExtensions
{
    /// <summary>
    /// Starts a PIX event on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to start the PIX event.</param>
    /// <param name="message">The message to use for the event.</param>
    public static unsafe void BeginEvent(this in ComputeContext context, string message)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Ends a PIX event on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to end the PIX event.</param>
    public static unsafe void EndEvent(this in ComputeContext context)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to insert the PIX marker.</param>
    /// <param name="message">The message to use for the marker.</param>
    public static unsafe void Log(this in ComputeContext context, string message)
    {
        ref CommandList commandList = ref ComputeContext.GetCommandList(in context, pipelineState: null);

        commandList.D3D12GraphicsCommandList->SetPixMarker(message);
    }
}
