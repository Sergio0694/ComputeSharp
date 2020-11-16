using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Helpers
{
    /// <summary>
    /// Helper methods to efficiently throw exceptions.
    /// </summary>
    [DebuggerStepThrough]
    internal static class ThrowHelper
    {
        /// <summary>
        /// Throws a <see cref="Win32Exception"/> if <paramref name="result"/> represents an error.
        /// </summary>
        /// <param name="result">The input <see cref="HRESULT"/> to check.</param>
        /// <exception cref="Win32Exception">Thrown if <paramref name="result"/> represents an error.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfFailed(int result)
        {
            if (result < 0)
            {
                static void Throw(int result) => throw new Win32Exception(result);

                Throw(result);
            }
        }

        /// <summary>
        /// Throws a <see cref="Win32Exception"/> if <paramref name="result"/> represents an error.
        /// </summary>
        /// <param name="result">The input <see cref="HRESULT"/> to check.</param>
        /// <param name="d3d3blobError">A <see cref="ID3DBlob"/> pointer representing an error message.</param>
        /// <exception cref="Win32Exception">Thrown if <paramref name="result"/> represents an error.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void ThrowIfFailed(int result, ID3DBlob* d3d3blobError)
        {
            if (result < 0)
            {
                // Gets an exception with an optional message with more info.
                // The input buffer transfers ownership and is disposed on return.
                static Win32Exception GetException(int result, ID3DBlob* d3d3blobError)
                {
                    if (d3d3blobError is null) return new Win32Exception(result);

                    char* p = (char*)d3d3blobError->GetBufferPointer();
                    int length = checked((int)unchecked(d3d3blobError->GetBufferSize() / sizeof(char)));
                    string message = new(p, 0, length);

                    d3d3blobError->Release();

                    return new Win32Exception(result, message);
                }

                static void Throw(int result, ID3DBlob* d3d3blobError) => throw GetException(result, d3d3blobError);

                Throw(result, d3d3blobError);
            }
        }

        /// <summary>
        /// Throws a <see cref="Win32Exception"/> if <paramref name="result"/> represents an error.
        /// </summary>
        /// <param name="result">The input <see cref="HRESULT"/> to check.</param>
        /// <param name="d3d3blobError">A <see cref="IDxcBlob"/> pointer representing an error message.</param>
        /// <exception cref="Win32Exception">Thrown if <paramref name="result"/> represents an error.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void ThrowIfFailed(int result, IDxcBlob* d3d3blobError)
        {
            ThrowIfFailed(result, (ID3DBlob*)d3d3blobError);
        }
    }
}
