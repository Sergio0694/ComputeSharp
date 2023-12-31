using System;
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
    public static unsafe void BeginEvent(this ref readonly ComputeContext context, Color color, string message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

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
    public static unsafe void BeginEvent(this ref readonly ComputeContext context, byte index, string message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

        uint pixColor = Pix.PIX_COLOR_INDEX(index);

        commandList.D3D12GraphicsCommandList->BeginEvent(pixColor, message);
    }

    /// <summary>
    /// Starts a PIX event on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to start the PIX event.</param>
    /// <param name="message">The message to use for the event.</param>
    [Conditional("USE_PIX")]
    public static unsafe void BeginEvent(this ref readonly ComputeContext context, string message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

        commandList.D3D12GraphicsCommandList->BeginEvent(0, message);
    }

    /// <summary>
    /// Starts a PIX event on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to start the PIX event.</param>
    /// <param name="color">The color to use for the event (the alpha channel will be ignored).</param>
    /// <param name="message">The message to use for the event (it's assumed to be a <see langword="null"/>-terminated ANSI characters sequence).</param>
    /// <remarks>
    /// <para>
    /// The <paramref name="message"/> parameter is not checked to ensure it has a <see langword="null"/>-terminator. This allows callers
    /// to pass an UTF8 string literal containing only ASCII characters (as the byte contents would be a valid ANSI sequence in that case).
    /// The compiler will automatically insert a terminator right past the end of the returned <see cref="ReadOnlySpan{T}"/> when doing so.
    /// </para>
    /// <para>
    /// This method offers greatly reduced overhead compared to the overlods taking a <see cref="string"/>, as there is no need to create the
    /// <see cref="string"/> instance nor to execute the transcoding at runtime. The behavior when the input is invalid though is undefined.
    /// Callers must take extra care to ensure the input message is in fact <see langword="null"/>-terminated when invoking this API.
    /// </para>
    /// </remarks>
    [Conditional("USE_PIX")]
    public static unsafe void BeginEventUnsafe(this ref readonly ComputeContext context, Color color, ReadOnlySpan<byte> message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

        int argb = color.ToArgb();
        uint pixColor = Pix.PIX_COLOR((byte)((argb & 0x00FF0000) >> 16), (byte)((argb & 0x0000FF00) >> 8), (byte)(argb & 0x000000FF));

        commandList.D3D12GraphicsCommandList->BeginEventUnsafe(pixColor, message);
    }

    /// <summary>
    /// Starts a PIX event on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to start the PIX event.</param>
    /// <param name="index">The index to identify the group this event belongs to (PIX will choose a color to represent all entries in the group).</param>
    /// <param name="message">The message to use for the event (it's assumed to be a <see langword="null"/>-terminated ANSI characters sequence).</param>
    /// <remarks>
    /// <para>
    /// The <paramref name="message"/> parameter is not checked to ensure it has a <see langword="null"/>-terminator. This allows callers
    /// to pass an UTF8 string literal containing only ASCII characters (as the byte contents would be a valid ANSI sequence in that case).
    /// The compiler will automatically insert a terminator right past the end of the returned <see cref="ReadOnlySpan{T}"/> when doing so.
    /// </para>
    /// <para>
    /// This method offers greatly reduced overhead compared to the overlods taking a <see cref="string"/>, as there is no need to create the
    /// <see cref="string"/> instance nor to execute the transcoding at runtime. The behavior when the input is invalid though is undefined.
    /// Callers must take extra care to ensure the input message is in fact <see langword="null"/>-terminated when invoking this API.
    /// </para>
    /// </remarks>
    [Conditional("USE_PIX")]
    public static unsafe void BeginEventUnsafe(this ref readonly ComputeContext context, byte index, ReadOnlySpan<byte> message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

        uint pixColor = Pix.PIX_COLOR_INDEX(index);

        commandList.D3D12GraphicsCommandList->BeginEventUnsafe(pixColor, message);
    }

    /// <summary>
    /// Starts a PIX event on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to start the PIX event.</param>
    /// <param name="message">The message to use for the event (it's assumed to be a <see langword="null"/>-terminated ANSI characters sequence).</param>
    /// <remarks>
    /// <para>
    /// The <paramref name="message"/> parameter is not checked to ensure it has a <see langword="null"/>-terminator. This allows callers
    /// to pass an UTF8 string literal containing only ASCII characters (as the byte contents would be a valid ANSI sequence in that case).
    /// The compiler will automatically insert a terminator right past the end of the returned <see cref="ReadOnlySpan{T}"/> when doing so.
    /// </para>
    /// <para>
    /// This method offers greatly reduced overhead compared to the overlods taking a <see cref="string"/>, as there is no need to create the
    /// <see cref="string"/> instance nor to execute the transcoding at runtime. The behavior when the input is invalid though is undefined.
    /// Callers must take extra care to ensure the input message is in fact <see langword="null"/>-terminated when invoking this API.
    /// </para>
    /// </remarks>
    [Conditional("USE_PIX")]
    public static unsafe void BeginEventUnsafe(this ref readonly ComputeContext context, ReadOnlySpan<byte> message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

        commandList.D3D12GraphicsCommandList->BeginEventUnsafe(0, message);
    }

    /// <summary>
    /// Ends a PIX event on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to end the PIX event.</param>
    [Conditional("USE_PIX")]
    public static unsafe void EndEvent(this ref readonly ComputeContext context)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

        Pix.PIXEndEventOnCommandList(commandList.D3D12GraphicsCommandList);
    }

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to insert the PIX marker.</param>
    /// <param name="color">The color to use for the log (the alpha channel will be ignored).</param>
    /// <param name="message">The message to use for the marker.</param>
    [Conditional("USE_PIX")]
    public static unsafe void Log(this ref readonly ComputeContext context, Color color, string message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

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
    public static unsafe void Log(this ref readonly ComputeContext context, byte index, string message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

        uint pixColor = Pix.PIX_COLOR_INDEX(index);

        commandList.D3D12GraphicsCommandList->SetPixMarker(pixColor, message);
    }

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to insert the PIX marker.</param>
    /// <param name="message">The message to use for the marker.</param>
    [Conditional("USE_PIX")]
    public static unsafe void Log(this ref readonly ComputeContext context, string message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

        commandList.D3D12GraphicsCommandList->SetPixMarker(0, message);
    }

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to insert the PIX marker.</param>
    /// <param name="color">The color to use for the log (the alpha channel will be ignored).</param>
    /// <param name="message">The message to use for the marker (it's assumed to be a <see langword="null"/>-terminated ANSI characters sequence).</param>
    /// <remarks>
    /// <para>
    /// The <paramref name="message"/> parameter is not checked to ensure it has a <see langword="null"/>-terminator. This allows callers
    /// to pass an UTF8 string literal containing only ASCII characters (as the byte contents would be a valid ANSI sequence in that case).
    /// The compiler will automatically insert a terminator right past the end of the returned <see cref="ReadOnlySpan{T}"/> when doing so.
    /// </para>
    /// <para>
    /// This method offers greatly reduced overhead compared to the overlods taking a <see cref="string"/>, as there is no need to create the
    /// <see cref="string"/> instance nor to execute the transcoding at runtime. The behavior when the input is invalid though is undefined.
    /// Callers must take extra care to ensure the input message is in fact <see langword="null"/>-terminated when invoking this API.
    /// </para>
    /// </remarks>
    [Conditional("USE_PIX")]
    public static unsafe void LogUnsafe(this ref readonly ComputeContext context, Color color, ReadOnlySpan<byte> message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

        int argb = color.ToArgb();
        uint pixColor = Pix.PIX_COLOR((byte)((argb & 0x00FF0000) >> 16), (byte)((argb & 0x0000FF00) >> 8), (byte)(argb & 0x000000FF));

        commandList.D3D12GraphicsCommandList->SetPixMarkerUnsafe(pixColor, message);
    }

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to insert the PIX marker.</param>
    /// <param name="index">The index to identify the group this log belongs to (PIX will choose a color to represent all entries in the group).</param>
    /// <param name="message">The message to use for the marker (it's assumed to be a <see langword="null"/>-terminated ANSI characters sequence).</param>
    /// <remarks>
    /// <para>
    /// The <paramref name="message"/> parameter is not checked to ensure it has a <see langword="null"/>-terminator. This allows callers
    /// to pass an UTF8 string literal containing only ASCII characters (as the byte contents would be a valid ANSI sequence in that case).
    /// The compiler will automatically insert a terminator right past the end of the returned <see cref="ReadOnlySpan{T}"/> when doing so.
    /// </para>
    /// <para>
    /// This method offers greatly reduced overhead compared to the overlods taking a <see cref="string"/>, as there is no need to create the
    /// <see cref="string"/> instance nor to execute the transcoding at runtime. The behavior when the input is invalid though is undefined.
    /// Callers must take extra care to ensure the input message is in fact <see langword="null"/>-terminated when invoking this API.
    /// </para>
    /// </remarks>
    [Conditional("USE_PIX")]
    public static unsafe void LogUnsafe(this ref readonly ComputeContext context, byte index, ReadOnlySpan<byte> message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

        uint pixColor = Pix.PIX_COLOR_INDEX(index);

        commandList.D3D12GraphicsCommandList->SetPixMarkerUnsafe(pixColor, message);
    }

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ComputeContext"/> object.
    /// </summary>
    /// <param name="context">The <see cref="ComputeContext"/> to use to insert the PIX marker.</param>
    /// <param name="message">The message to use for the marker (it's assumed to be a <see langword="null"/>-terminated ANSI characters sequence).</param>
    /// <remarks>
    /// <para>
    /// The <paramref name="message"/> parameter is not checked to ensure it has a <see langword="null"/>-terminator. This allows callers
    /// to pass an UTF8 string literal containing only ASCII characters (as the byte contents would be a valid ANSI sequence in that case).
    /// The compiler will automatically insert a terminator right past the end of the returned <see cref="ReadOnlySpan{T}"/> when doing so.
    /// </para>
    /// <para>
    /// This method offers greatly reduced overhead compared to the overlods taking a <see cref="string"/>, as there is no need to create the
    /// <see cref="string"/> instance nor to execute the transcoding at runtime. The behavior when the input is invalid though is undefined.
    /// Callers must take extra care to ensure the input message is in fact <see langword="null"/>-terminated when invoking this API.
    /// </para>
    /// </remarks>
    [Conditional("USE_PIX")]
    public static unsafe void LogUnsafe(this ref readonly ComputeContext context, ReadOnlySpan<byte> message)
    {
        ref CommandList commandList = ref context.GetCommandList(pipelineState: null);

        commandList.D3D12GraphicsCommandList->SetPixMarkerUnsafe(0, message);
    }
}