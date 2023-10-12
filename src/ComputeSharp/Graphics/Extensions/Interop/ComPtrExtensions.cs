using System.Runtime.CompilerServices;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Core.Extensions;

/// <summary>
/// Helper methods for working with the <see cref="ComPtr{T}"/> type.
/// </summary>
internal static class ComPtrExtensions
{
    /// <summary>
    /// Moves the current <see cref="ComPtr{T}"/> instance and resets it without releasing the reference.
    /// </summary>
    /// <typeparam name="T">The type to wrap in the current <see cref="ComPtr{T}"/> instance.</typeparam>
    /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to move.</param>
    /// <returns>The moved <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ComPtr<T> Move<T>(this in ComPtr<T> ptr)
#if NET6_0_OR_GREATER
        where T : unmanaged, IUnknown.Interface
#else
        where T : unmanaged
#endif
    {
        ComPtr<T> copy = default;

        Unsafe.AsRef(in ptr).Swap(ref copy);

        return copy;
    }
}