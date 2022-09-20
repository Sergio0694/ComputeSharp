using System.ComponentModel;
using System.Diagnostics;
#if NETCOREAPP3_1_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using System.Runtime.CompilerServices;

namespace ComputeSharp.D2D1.Tests.Extensions;

using HRESULT = Win32.HResult;

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
        if ((int)result < 0)
        {
            ThrowWin32Exception(result);
        }
    }

    /// <summary>
    /// Throws a <see cref="Win32Exception"/> if <paramref name="result"/> represents an error.
    /// </summary>
    /// <param name="result">The input <see cref="int"/> to check.</param>
    /// <exception cref="Win32Exception">Thrown if <paramref name="result"/> represents an error.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Assert(this int result)
    {
        if (result < 0)
        {
            ThrowWin32Exception(result);
        }
    }

    /// <summary>
    /// Throws a <see cref="Win32Exception"/>.
    /// </summary>
    /// <param name="result">The input return code.</param>
#if NETCOREAPP3_1_OR_GREATER
    [DoesNotReturn]
#endif
    private static void ThrowWin32Exception(int result)
    {
        throw new Win32Exception(result);
    }
}
