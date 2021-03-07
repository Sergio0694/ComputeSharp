using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TerraFX.Interop;

namespace ComputeSharp.Interop
{
    /// <summary>
    /// A <see cref="ComPtr{T}"/>-equivalent type to safely work with pointers to public <see cref="D3D12MemAlloc"/> APIs.
    /// </summary>
    /// <typeparam name="T">Must be either <see cref="D3D12MA_Allocator"/>, <see cref="D3D12MA_Allocation"/>, <see cref="D3D12MA_Pool"/> or <see cref="D3D12MA_VirtualBlock"/>.</typeparam>
    /// <remarks>
    /// This type is not marked as <see langword="ref"/> so that it can also be used in fields, so make sure to
    /// avoid copying it and disposing it multiple times, as that would lead to a access violation exceptions.
    /// </remarks>
    internal unsafe struct UniquePtr<T> : IDisposable
        where T : unmanaged
    {
        /// <summary>
        /// The raw pointer to an <typeparamref name="T"/> object, if existing.
        /// </summary>
        private T* pointer;

        /// <summary>
        /// Creates a new <see cref="UniquePtr{T}"/> instance from a raw pointer.
        /// </summary>
        /// <param name="other">The raw pointer to wrap.</param>
        public UniquePtr(T* other)
        {
            pointer = other;
        }

        /// <summary>
        /// Unwraps a <see cref="UniquePtr{T}"/> instance and returns the internal raw pointer.
        /// </summary>
        /// <param name="other">The <see cref="UniquePtr{T}"/> instance to unwrap.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T*(UniquePtr<T> other)
        {
            return other.Get();
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            T* pointer = this.pointer;

            if (pointer != null)
            {
                this.pointer = null;

                if (typeof(T) == typeof(D3D12MA_Allocator)) ((D3D12MA_Allocator*)pointer)->Release();
                else if (typeof(T) == typeof(D3D12MA_Allocation)) ((D3D12MA_Allocation*)pointer)->Release();
                else if (typeof(T) == typeof(D3D12MA_Pool)) ((D3D12MA_Pool*)pointer)->Release();
                else if (typeof(T) == typeof(D3D12MA_VirtualBlock)) ((D3D12MA_VirtualBlock*)pointer)->Release();
                else throw new ArgumentException("Invalid pointer type");
            }
        }

        /// <summary>
        /// Gets the currently wrapped raw pointer to an <typeparamref name="T"/> object.
        /// </summary>
        /// <returns>The raw pointer wrapped by the current <typeparamref name="T"/> instance.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly T* Get()
        {
            return this.pointer;
        }

        /// <summary>
        /// Gets the address of the current <see cref="UniquePtr{T}"/> instance as a raw <typeparamref name="T"/> double pointer.
        /// </summary>
        /// <returns>The raw pointer to the current <see cref="UniquePtr{T}"/> instance.</returns>
        /// <remarks>This method is only valid when the current <see cref="UniquePtr{T}"/> instance is on the stack or pinned.</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly T** GetAddressOf()
        {
            return (T**)Unsafe.AsPointer(ref Unsafe.AsRef(in this));
        }

        /// <summary>
        /// Gets the address of the current <see cref="UniquePtr{T}"/> instance as a managed <typeparamref name="T"/> reference to pointer.
        /// </summary>
        /// <returns>The reference to the current <see cref="UniquePtr{T}"/> instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly ref T* GetPinnableReference()
        {
            fixed (T** ptr = &this.pointer)
            {
                return ref *ptr;
            }
        }

        /// <summary>
        /// Moves the current <see cref="UniquePtr{T}"/> instance and resets it without releasing the reference.
        /// </summary>
        /// <returns>The moved <see cref="UniquePtr{T}"/> instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniquePtr<T> Move()
        {
            UniquePtr<T> copy = this;

            this = default;

            return copy;
        }
    }
}
