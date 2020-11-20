using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TerraFX.Interop;

namespace ComputeSharp.Core.Extensions
{
    /// <summary>
    /// Helper methods for working with the <see cref="ComPtr{T}"/> type.
    /// </summary>
    public static class ComPtrExtensions
    {
        /// <summary>
        /// Gets whether the current <see cref="ComPtr{T}"/> instance as is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type to wrap in the current <see cref="ComPtr{T}"/> instance.</typeparam>
        /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to check.</param>
        /// <returns>Whether or not <paramref name="ptr"/> is not <see langword="null"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool Exists<T>(this ComPtr<T> ptr)
            where T : unmanaged
        {
            return ptr.Get() != null;
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
            where T : unmanaged
        {
            return (void**)Unsafe.AsPointer(ref Unsafe.AsRef(in ptr));
        }

        /// <summary>
        /// Gets the address of the current <see cref="ComPtr{T}"/> instance as a raw <see langword="void"/> double pointer.
        /// </summary>
        /// <typeparam name="T">The type to wrap in the current <see cref="ComPtr{T}"/> instance.</typeparam>
        /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to get the address for.</param>
        /// <returns>The raw pointer to the input <see cref="ComPtr{T}"/> instance.</returns>
        /// <remarks>This method is only valid when the current <see cref="ComPtr{T}"/> instance is on the stack or pinned.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ComPtr<T> Move<T>(this in ComPtr<T> ptr)
            where T : unmanaged
        {
            ComPtr<T> copy = default;

            Unsafe.AsRef(in ptr).Swap(ref copy);

            return copy;
        }

        /// <summary>
        /// Upcasts the current <see cref="ComPtr{T}"/> instance as a base type.
        /// </summary>
        /// <typeparam name="T">The type of the current <see cref="ComPtr{T}"/> instance.</typeparam>
        /// <typeparam name="U">The type to cast the input instance to.</typeparam>
        /// <param name="ptr">The input <see cref="ComPtr{T}"/> instance to upcast.</param>
        /// <returns>A <see cref="ComPtr{T}"/> instance of type <typeparamref name="U"/>.</returns>
        /// <remarks>
        /// This method is meant to be used in a chained expression and it does not increment the reference
        /// count for the input pointer. Do not expose the return value of this extension to consumers.
        /// </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref readonly ComPtr<U> Upcast<T, U>(this in ComPtr<T> ptr)
            where T : unmanaged
            where U : unmanaged
        {
            return ref Unsafe.As<ComPtr<T>, ComPtr<U>>(ref Unsafe.AsRef(in ptr));
        }
    }
}
