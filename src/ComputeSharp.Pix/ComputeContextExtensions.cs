using System.Diagnostics;
using System.Drawing;
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
    /// <param name="color">The color to use for the event (the alpha channel will be ignored).</param>
    /// <param name="message">The message to use for the event.</param>
    [Conditional("USE_PIX")]
    public static unsafe void BeginEvent(this in ComputeContext context, Color color, string message)
    {
        ref CommandList commandList = ref ComputeContext.GetCommandList(in context, pipelineState: null);

        int argb = color.ToArgb();
        uint pixColor = Pix.PIX_COLOR((byte)((argb & 0x00FF0000) >> 16), (byte)((argb & 0x0000FF00) >> 8), (byte)(argb & 0x000000FF));

        commandList.D3D12GraphicsCommandList->BeginEvent(pixColor, message);
    }

    /// <summary>
    /// Starts a PIX event on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to start the PIX event.</param>
    /// <param name="index">The index to identify the group this event belongs to (PIX will choose a color to represent all entries in the group).</param>
    /// <param name="message">The message to use for the event.</param>
    [Conditional("USE_PIX")]
    public static unsafe void BeginEvent(this in ComputeContext context, byte index, string message)
    {
        ref CommandList commandList = ref ComputeContext.GetCommandList(in context, pipelineState: null);

        uint pixColor = Pix.PIX_COLOR_INDEX(index);

        commandList.D3D12GraphicsCommandList->BeginEvent(pixColor, message);
    }

    /// <summary>
    /// Starts a PIX event on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to start the PIX event.</param>
    /// <param name="message">The message to use for the event.</param>
    [Conditional("USE_PIX")]
    public static unsafe void BeginEvent(this in ComputeContext context, string message)
    {
        ref CommandList commandList = ref ComputeContext.GetCommandList(in context, pipelineState: null);

        commandList.D3D12GraphicsCommandList->BeginEvent(0, message);
    }

    /// <summary>
    /// Ends a PIX event on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to end the PIX event.</param>
    [Conditional("USE_PIX")]
    public static unsafe void EndEvent(this in ComputeContext context)
    {
        ref CommandList commandList = ref ComputeContext.GetCommandList(in context, pipelineState: null);

        Pix.PIXEndEventOnCommandList(commandList.D3D12GraphicsCommandList);
    }

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to insert the PIX marker.</param>
    /// <param name="color">The color to use for the log (the alpha channel will be ignored).</param>
    /// <param name="message">The message to use for the marker.</param>
    [Conditional("USE_PIX")]
    public static unsafe void Log(this in ComputeContext context, Color color, string message)
    {
        ref CommandList commandList = ref ComputeContext.GetCommandList(in context, pipelineState: null);

        int argb = color.ToArgb();
        uint pixColor = Pix.PIX_COLOR((byte)((argb & 0x00FF0000) >> 16), (byte)((argb & 0x0000FF00) >> 8), (byte)(argb & 0x000000FF));

        commandList.D3D12GraphicsCommandList->SetPixMarker(pixColor, message);
    }

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to insert the PIX marker.</param>
    /// <param name="index">The index to identify the group this log belongs to (PIX will choose a color to represent all entries in the group).</param>
    /// <param name="message">The message to use for the marker.</param>
    [Conditional("USE_PIX")]
    public static unsafe void Log(this in ComputeContext context, byte index, string message)
    {
        ref CommandList commandList = ref ComputeContext.GetCommandList(in context, pipelineState: null);

        uint pixColor = Pix.PIX_COLOR_INDEX(index);

        commandList.D3D12GraphicsCommandList->SetPixMarker(pixColor, message);
    }

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to insert the PIX marker.</param>
    /// <param name="message">The message to use for the marker.</param>
    [Conditional("USE_PIX")]
    public static unsafe void Log(this in ComputeContext context, string message)
    {
        ref CommandList commandList = ref ComputeContext.GetCommandList(in context, pipelineState: null);

        commandList.D3D12GraphicsCommandList->SetPixMarker(0, message);
    }
}
