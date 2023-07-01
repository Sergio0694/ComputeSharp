using System.Runtime.CompilerServices;
using Win32;

namespace ComputeSharp.D2D1.Tests.Extensions;

/// <summary>
/// Helper methods for working with the <see cref="ComPtr{T}"/> type.
/// </summary>
/// <remarks>Trimmed down version of the same file in <c>ComputeSharp</c>.</remarks>
internal static class ComPtrExtensions
{
    /// <summary>
    /// Moves the current <see cref="ComPtr{T}"/> instance and resets it without releasing the reference.
    /// </summary>
    /// <typeparam name="T">The type to wrap in the current <see cref="ComPtr{T}"/> instance.</typeparam>
    /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to move.</param>
    /// <returns>The moved <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComPtr<T> Move<T>(this in ComPtr<T> ptr)
        where T : unmanaged
    {
        ComPtr<T> copy = default;

        Unsafe.AsRef(in ptr).Swap(ref copy);

        return copy;
    }
}