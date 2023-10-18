using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;
using ComputeSharp.Win32;

namespace ComputeSharp.Interop;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="ID3D12GraphicsCommandList"/> type.
/// </summary>
internal static class ID3D12GraphicsCommandListExtensions
{
    /// <summary>
    /// Begins a PIX event on a target <see cref="ID3D12GraphicsCommandList"/> object.
    /// </summary>
    /// <param name="d3D12GraphicsCommandList">The target <see cref="ID3D12GraphicsCommandList"/> object.</param>
    /// <param name="color">The color to use for the event.</param>
    /// <param name="message">The message to use for the event.</param>
    public static unsafe void BeginEvent(this ref ID3D12GraphicsCommandList d3D12GraphicsCommandList, uint color, string message)
    {
        // Rent a byte buffer with space for the null-terminator too
        int maxByteLength = Encoding.ASCII.GetMaxByteCount(message.Length) + 1;
        byte[] buffer = ArrayPool<byte>.Shared.Rent(maxByteLength);
        int bytesWritten;

        fixed (byte* bufferPtr = buffer)
        {
            // Transcode from unicode to ASCII
            fixed (char* messagePtr = message)
            {
                bytesWritten = Encoding.ASCII.GetBytes(messagePtr, message.Length, bufferPtr, buffer.Length);
            }

            // Add the null terminator
            buffer[bytesWritten] = 0;

            // Write the marker with the null-terminated ANSI message
            Pix.PIXBeginEventOnCommandList((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref d3D12GraphicsCommandList), color, (sbyte*)bufferPtr);
        }

        // Return the buffer (if an exception is thrown, it's fine to not do so)
        ArrayPool<byte>.Shared.Return(buffer);
    }

    /// <summary>
    /// Begins a PIX event on a target <see cref="ID3D12GraphicsCommandList"/> object.
    /// </summary>
    /// <param name="d3D12GraphicsCommandList">The target <see cref="ID3D12GraphicsCommandList"/> object.</param>
    /// <param name="color">The color to use for the event.</param>
    /// <param name="message">The message to use for the event (it's assumed to be a <see langword="null"/>-terminated ANSI characters sequence).</param>
    public static unsafe void BeginEventUnsafe(this ref ID3D12GraphicsCommandList d3D12GraphicsCommandList, uint color, ReadOnlySpan<byte> message)
    {
        fixed (byte* messagePtr = message)
        {
            Pix.PIXBeginEventOnCommandList((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref d3D12GraphicsCommandList), color, (sbyte*)messagePtr);
        }
    }

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ID3D12GraphicsCommandList"/> object.
    /// </summary>
    /// <param name="d3D12GraphicsCommandList">The target <see cref="ID3D12GraphicsCommandList"/> object.</param>
    /// <param name="color">The color to use for the marker.</param>
    /// <param name="message">The message to use for the marker.</param>
    public static unsafe void SetPixMarker(this ref ID3D12GraphicsCommandList d3D12GraphicsCommandList, uint color, string message)
    {
        // Rent a byte buffer with space for the null-terminator too
        int maxByteLength = Encoding.ASCII.GetMaxByteCount(message.Length) + 1;
        byte[] buffer = ArrayPool<byte>.Shared.Rent(maxByteLength);
        int bytesWritten;

        fixed (byte* bufferPtr = buffer)
        {
            // Transcode from unicode to ASCII
            fixed (char* messagePtr = message)
            {
                bytesWritten = Encoding.ASCII.GetBytes(messagePtr, message.Length, bufferPtr, buffer.Length);
            }

            // Add the null terminator
            buffer[bytesWritten] = 0;

            // Write the marker with the null-terminated ANSI message
            Pix.PIXSetMarkerOnCommandList((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref d3D12GraphicsCommandList), color, (sbyte*)bufferPtr);
        }

        // Return the buffer (if an exception is thrown, it's fine to not do so)
        ArrayPool<byte>.Shared.Return(buffer);
    }

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ID3D12GraphicsCommandList"/> object.
    /// </summary>
    /// <param name="d3D12GraphicsCommandList">The target <see cref="ID3D12GraphicsCommandList"/> object.</param>
    /// <param name="color">The color to use for the marker.</param>
    /// <param name="message">The message to use for the event (it's assumed to be a <see langword="null"/>-terminated ANSI characters sequence).</param>
    public static unsafe void SetPixMarkerUnsafe(this ref ID3D12GraphicsCommandList d3D12GraphicsCommandList, uint color, ReadOnlySpan<byte> message)
    {
        fixed (byte* messagePtr = message)
        {
            Pix.PIXSetMarkerOnCommandList((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref d3D12GraphicsCommandList), color, (sbyte*)messagePtr);
        }
    }
}