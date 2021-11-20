using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Core.Extensions;

/// <summary>
/// Helper methods for working with the <see cref="ComPtr{T}"/> type.
/// </summary>
internal static class ComPtrExtensions
{
    /// <summary>
    /// Invokes <see cref="IUnknown.AddRef"/> on the wrapped object for an input <see cref="ComPtr{T}"/> value.
    /// </summary>
    /// <typeparam name="T">The type of the current <see cref="ComPtr{T}"/> instance.</typeparam>
    /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to increment the reference count for.</param>
    /// <returns>A <see cref="ComPtr{T}"/> instance of type <see cref="IUnknown"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe uint AddRef<T>(this ComPtr<T> ptr)
#if NET6_0_OR_GREATER
        where T : unmanaged, IUnknown.Interface
#else
        where T : unmanaged
#endif
    {
        if (ptr.Get() is not null)
        {
#if NET6_0_OR_GREATER
            return ptr.Get()->AddRef();
#else
            return ((IUnknown*)ptr.Get())->AddRef();
#endif

        }

        return 0;
    }

    /// <summary>
    /// Invokes <see cref="IUnknown.Release"/> on the wrapped object for an input <see cref="ComPtr{T}"/> value.
    /// If the object has been deleted, it also resets <paramref name="ptr"/>.
    /// </summary>
    /// <typeparam name="T">The type of the current <see cref="ComPtr{T}"/> instance.</typeparam>
    /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to release.</param>
    /// <returns>A <see cref="ComPtr{T}"/> instance of type <see cref="IUnknown"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe uint Release<T>(this ref ComPtr<T> ptr)
#if NET6_0_OR_GREATER
        where T : unmanaged, IUnknown.Interface
#else
        where T : unmanaged
#endif
    {
        if (ptr.Get() is not null)
        {
#if NET6_0_OR_GREATER
            uint count = ptr.Get()->Release();
#else
            uint count = ((IUnknown*)ptr.Get())->Release();
#endif

            if (count == 0)
            {
                ptr = default;
            }

            return count;
        }

        return 0;
    }

    /// <summary>
    /// Reinterprets the current <see cref="ComPtr{T}"/> instance as one of type <see cref="IUnknown"/>.
    /// </summary>
    /// <typeparam name="T">The type of the current <see cref="ComPtr{T}"/> instance.</typeparam>
    /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to upcast.</param>
    /// <returns>A <see cref="ComPtr{T}"/> instance of type <see cref="IUnknown"/>.</returns>
    /// <remarks>
    /// This method is meant to be used in a chained expression and it does not increment the reference
    /// count for the input pointer. Do not expose the return value of this extension to consumers.
    /// </remarks>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref readonly ComPtr<IUnknown> AsIUnknown<T>(this in ComPtr<T> ptr)
#if NET6_0_OR_GREATER
        where T : unmanaged, IUnknown.Interface
#else
        where T : unmanaged
#endif
    {
        return ref Unsafe.As<ComPtr<T>, ComPtr<IUnknown>>(ref Unsafe.AsRef(in ptr));
    }

    /// <summary>
    /// Gets the address of the current <see cref="ComPtr{T}"/> instance as a raw <see langword="void"/> double pointer.
    /// </summary>
    /// <typeparam name="T">The type to wrap in the current <see cref="ComPtr{T}"/> instance.</typeparam>
    /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to get the address for.</param>
    /// <returns>The raw pointer to the input <see cref="ComPtr{T}"/> instance.</returns>
    /// <remarks>This method is only valid when the current <see cref="ComPtr{T}"/> instance is on the stack or pinned.</remarks>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void** GetVoidAddressOf<T>(this in ComPtr<T> ptr)
#if NET6_0_OR_GREATER
        where T : unmanaged, IUnknown.Interface
#else
        where T : unmanaged
#endif
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
