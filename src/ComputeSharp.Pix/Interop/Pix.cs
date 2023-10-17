using System.Runtime.InteropServices;
using ComputeSharp.Win32;

namespace ComputeSharp.Interop;

/// <summary>
/// A collection of native bindings for PIX APIs (see <see href="https://devblogs.microsoft.com/pix/winpixeventruntime/"/>).
/// </summary>
internal static unsafe partial class Pix
{
    /// <summary>
    /// Starts a PIX event on a target <see cref="ID3D12GraphicsCommandList"/> object.
    /// <para>
    /// C export: <c>typedef void(WINAPI* BeginEventOnCommandList)(ID3D12GraphicsCommandList* commandList, UINT64 color, _In_ PCSTR formatString)</c>.
    /// </para>
    /// </summary>
    /// <param name="commandList">The target <see cref="ID3D12GraphicsCommandList"/> object.</param>
    /// <param name="color">The color to use for the event.</param>
    /// <param name="formatString">The format string to use for the event (in ANSI format, <see langword="null"/>-terminated).</param>
    [DllImport("WinPixEventRuntime", ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    public static extern void PIXBeginEventOnCommandList(ID3D12GraphicsCommandList* commandList, ulong color, sbyte* formatString);

    /// <summary>
    /// Ends a PIX event on a target <see cref="ID3D12GraphicsCommandList"/> object.
    /// <para>
    /// C export: <c>typedef void(WINAPI* EndEventOnCommandList)(ID3D12GraphicsCommandList* commandList)</c>.
    /// </para>
    /// </summary>
    /// <param name="commandList">The target <see cref="ID3D12GraphicsCommandList"/> object.</param>
    [DllImport("WinPixEventRuntime", ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    public static extern void PIXEndEventOnCommandList(ID3D12GraphicsCommandList* commandList);

    /// <summary>
    /// Sets a PIX marker on a target <see cref="ID3D12GraphicsCommandList"/> object.
    /// <para>
    /// C export: <c>typedef void(WINAPI* SetMarkerOnCommandList)(ID3D12GraphicsCommandList* commandList, UINT64 color, _In_ PCSTR formatString)</c>.
    /// </para>
    /// </summary>
    /// <param name="commandList">The target <see cref="ID3D12GraphicsCommandList"/> object.</param>
    /// <param name="color">The color to use for the marker.</param>
    /// <param name="formatString">The format string to use for the marker (in ANSI format, <see langword="null"/>-terminated).</param>
    [DllImport("WinPixEventRuntime", ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    public static extern void PIXSetMarkerOnCommandList(ID3D12GraphicsCommandList* commandList, ulong color, sbyte* formatString);
}