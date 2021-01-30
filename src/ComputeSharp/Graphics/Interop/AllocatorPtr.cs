using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TerraFX.Interop;

namespace ComputeSharp.Graphics
{
    /// <summary>
    /// A <see cref="ComPtr{T}"/>-equivalent type to safely work with <see cref="Allocator"/> pointers.
    /// </summary>
    /// <remarks>
    /// While this type is not marked as <see langword="ref"/> so that it can also be used in fields, make sure to
    /// keep the reference counts properly tracked if you do store <see cref="ComPtr{T}"/> instances on the heap.
    /// </remarks>
    internal unsafe struct AllocatorPtr : IDisposable
    {
        /// <summary>
        /// The raw pointer to an <see cref="Allocator"/> object, if existing.
        /// </summary>
        private Allocator* pointer;

        /// <summary>
        /// Creates a new <see cref="ComPtr{T}"/> instance from a raw pointer.
        /// </summary>
        /// <param name="other">The raw pointer to wrap.</param>
        public AllocatorPtr(Allocator* other)
        {
            pointer = other;
        }

        /// <summary>
        /// Unwraps a <see cref="AllocatorPtr"/> instance and returns the internal raw pointer.
        /// </summary>
        /// <param name="other">The <see cref="AllocatorPtr"/> instance to unwrap.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Allocator*(AllocatorPtr other)
        {
            return other.Get();
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            Allocator* pointer = this.pointer;

            if (pointer != null)
            {
                this.pointer = null;

                pointer->Release();
            }
        }

        /// <summary>
        /// Gets the currently wrapped raw pointer to an <see cref="Allocator"/> object.
        /// </summary>
        /// <returns>The raw pointer wrapped by the current <see cref="Allocator"/> instance.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Allocator* Get()
        {
            return this.pointer;
        }

        /// <summary>
        /// Gets the address of the current <see cref="AllocatorPtr"/> instance as a raw <see cref="Allocator"/> double pointer.
        /// This method is only valid when the current <see cref="AllocatorPtr"/> instance is on the stack or pinned.
        /// </summary>
        /// <returns>The raw pointer to the current <see cref="AllocatorPtr"/> instance.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Allocator** GetAddressOf()
        {
            return (Allocator**)Unsafe.AsPointer(ref Unsafe.AsRef(in this));
        }

        /// <summary>
        /// Gets the address of the current <see cref="AllocatorPtr"/> instance as a managed <see cref="Allocator"/> reference to pointer.
        /// </summary>
        /// <returns>The reference to the current <see cref="AllocatorPtr"/> instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly ref Allocator* GetPinnableReference()
        {
            fixed (Allocator** ptr = &this.pointer)
            {
                return ref *ptr;
            }
        }

        /// <summary>
        /// Gets the address of the current <see cref="AllocatorPtr"/> instance as a raw <see langword="void"/> double pointer.
        /// </summary>
        /// <returns>The raw pointer to the input <see cref="AllocatorPtr"/> instance.</returns>
        /// <remarks>This method is only valid when the current <see cref="AllocatorPtr"/> instance is on the stack or pinned.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AllocatorPtr Move()
        {
            AllocatorPtr copy = this;

            this = default;

            return copy;
        }
    }
}
