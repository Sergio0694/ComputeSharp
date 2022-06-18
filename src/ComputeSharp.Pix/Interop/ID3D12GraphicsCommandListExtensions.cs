using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;
using TerraFX.Interop.DirectX;

namespace ComputeSharp.Interop;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="ID3D12GraphicsCommandList"/> type.
/// </summary>
internal static class ID3D12GraphicsCommandListExtensions
{
    /// <summary>
    /// Sets a PIX marker on a target <see cref="ID3D12GraphicsCommandList"/> object.
    /// </summary>
    /// <param name="d3D12GraphicsCommandList">The target <see cref="ID3D12GraphicsCommandList"/> object.</param>
    /// <param name="message">The message to use for the marker.</param>
    public static unsafe void SetPixMarker(this ref ID3D12GraphicsCommandList d3D12GraphicsCommandList, string message)
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
            Pix.PIXSetMarkerOnCommandList((ID3D12GraphicsCommandList*)Unsafe.AsPointer(ref d3D12GraphicsCommandList), 0, (sbyte*)bufferPtr);
        }

        // Return the buffer (if an exception is thrown, it's fine to not do so)
        ArrayPool<byte>.Shared.Return(buffer);
    }
}
