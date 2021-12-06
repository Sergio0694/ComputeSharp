using System.ComponentModel;
using System.Diagnostics;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Core.Extensions;

/// <summary>
/// Helper methods to efficiently throw exceptions.
/// </summary>
[DebuggerStepThrough]
internal static class HResultExtensions
{
    /// <summary>
    /// Throws a <see cref="Win32Exception"/> if <paramref name="result"/> represents an error.
    /// </summary>
    /// <param name="result">The input <see cref="HRESULT"/> to check.</param>
    /// <exception cref="Win32Exception">Thrown if <paramref name="result"/> represents an error.</exception>
    public static void Assert(this HRESULT result)
    {
        Assert((int)result);
    }

    /// <summary>
    /// Throws a <see cref="Win32Exception"/> if <paramref name="result"/> represents an error.
    /// </summary>
    /// <param name="result">The input <see cref="int"/> to check.</param>
    /// <exception cref="Win32Exception">Thrown if <paramref name="result"/> represents an error.</exception>
    public static void Assert(this int result)
    {
        if (result < 0)
        {
            throw new Win32Exception(result);
        }
    }
}
