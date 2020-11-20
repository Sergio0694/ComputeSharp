using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Helpers;
using HRESULT = System.Int32;

namespace ComputeSharp.Graphics.Helpers
{
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert(this HRESULT result)
        {
            if (result < 0)
            {
                ThrowHelper.ThrowWin32Exception(result);
            }
        }
    }
}
