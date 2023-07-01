using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Win32;

namespace ComputeSharp.Tests.Extensions;

/// <summary>
/// Helper methods for working with the <see cref="ComPtr{T}"/> type.
/// </summary>
internal static class ComPtrExtensions
{
    /// <summary>
    /// Gets the reference count of a given <see cref="ComPtr{T}"/> value.
    /// </summary>
    /// <typeparam name="T">The type to wrap in the current <see cref="ComPtr{T}"/> instance.</typeparam>
    /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to check.</param>
    /// <returns>The reference count value for <paramref name="ptr"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe uint GetReferenceCount<T>(this in ComPtr<T> ptr)
        where T : unmanaged
    {
        if (ptr.Get() is null)
        {
            return 0;
        }

        _ = Marshal.AddRef((nint)ptr.Get());

        // Here we use Marshal APIs as T isn't constrained to IUnknown.
        // It doesn't matter anyway since this is just a test helper.
        return (uint)Marshal.Release((nint)ptr.Get());
    }
}