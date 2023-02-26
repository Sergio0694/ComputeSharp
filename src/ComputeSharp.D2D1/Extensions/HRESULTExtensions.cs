using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Extensions;

/// <summary>
/// Helper methods to efficiently throw exceptions.
/// </summary>
/// <remarks>Trimmed down version of the same file in <c>ComputeSharp</c>.</remarks>
[DebuggerStepThrough]
internal static class HRESULTExtensions
{
    /// <summary>
    /// Throws a <see cref="Win32Exception"/> if <paramref name="result"/> represents an error.
    /// </summary>
    /// <param name="result">The input <see cref="HRESULT"/> to check.</param>
    /// <exception cref="Win32Exception">Thrown if <paramref name="result"/> represents an error.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Assert(this HRESULT result)
    {
        default(Win32Exception).ThrowIfFailed((int)result);
    }

    /// <summary>
    /// Throws a <see cref="Win32Exception"/> if <paramref name="result"/> represents an error.
    /// </summary>
    /// <param name="result">The input <see cref="int"/> to check.</param>
    /// <exception cref="Win32Exception">Thrown if <paramref name="result"/> represents an error.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Assert(this int result)
    {
        default(Win32Exception).ThrowIfFailed(result);
    }
}