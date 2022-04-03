using System.Runtime.CompilerServices;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1Interop.Extensions;

/// <summary>
/// Helper methods for working with the <see cref="ComPtr{T}"/> type.
/// </summary>
/// <remarks>Trimmed down version of the same file in <c>ComputeSharp</c>.</remarks>
internal static class ComPtrExtensions
{
    /// <summary>
    /// Gets the address of the current <see cref="ComPtr{T}"/> instance as a raw <see langword="void"/> double pointer.
    /// </summary>
    /// <typeparam name="T">The type to wrap in the current <see cref="ComPtr{T}"/> instance.</typeparam>
    /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to get the address for.</param>
    /// <returns>The raw pointer to the input <see cref="ComPtr{T}"/> instance.</returns>
    /// <remarks>This method is only valid when the current <see cref="ComPtr{T}"/> instance is on the stack or pinned.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void** GetVoidAddressOf<T>(this in ComPtr<T> ptr)
        where T : unmanaged, IUnknown.Interface
    {
        return (void**)Unsafe.AsPointer(ref Unsafe.AsRef(in ptr));
    }

    /// <summary>
    /// Moves the current <see cref="ComPtr{T}"/> instance and resets it without releasing the reference.
    /// </summary>
    /// <typeparam name="T">The type to wrap in the current <see cref="ComPtr{T}"/> instance.</typeparam>
    /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to move.</param>
    /// <returns>The moved <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ComPtr<T> Move<T>(this in ComPtr<T> ptr)
        where T : unmanaged, IUnknown.Interface
    {
        ComPtr<T> copy = default;

        Unsafe.AsRef(in ptr).Swap(ref copy);

        return copy;
    }
}
