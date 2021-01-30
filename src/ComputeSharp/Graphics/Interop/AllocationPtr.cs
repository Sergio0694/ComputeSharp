using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Interop
{
    /// <summary>
    /// A <see cref="ComPtr{T}"/>-equivalent type to safely work with <see cref="Allocation"/> pointers.
    /// </summary>
    /// <remarks>
    /// While this type is not marked as <see langword="ref"/> so that it can also be used in fields, make sure to
    /// keep the reference counts properly tracked if you do store <see cref="ComPtr{T}"/> instances on the heap.
    /// </remarks>
    internal unsafe struct AllocationPtr : IDisposable
    {
        /// <summary>
        /// The raw pointer to an <see cref="Allocation"/> object, if existing.
        /// </summary>
        private Allocation* pointer;

        /// <summary>
        /// Creates a new <see cref="ComPtr{T}"/> instance from a raw pointer.
        /// </summary>
        /// <param name="other">The raw pointer to wrap.</param>
        public AllocationPtr(Allocation* other)
        {
            pointer = other;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            Allocation* pointer = this.pointer;

            if (pointer != null)
            {
                this.pointer = null;

                pointer->Release();
            }
        }

        /// <summary>
        /// Gets the currently wrapped raw pointer to an <see cref="Allocation"/> object.
        /// </summary>
        /// <returns>The raw pointer wrapped by the current <see cref="Allocation"/> instance.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Allocation* Get()
        {
            return this.pointer;
        }

        /// <summary>
        /// Gets the address of the current <see cref="AllocationPtr"/> instance as a raw <see cref="Allocation"/> double pointer.
        /// This method is only valid when the current <see cref="AllocationPtr"/> instance is on the stack or pinned.
        /// </summary>
        /// <returns>The raw pointer to the current <see cref="AllocationPtr"/> instance.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Allocation** GetAddressOf()
        {
            return (Allocation**)Unsafe.AsPointer(ref Unsafe.AsRef(in this));
        }

        /// <summary>
        /// Gets the address of the current <see cref="AllocationPtr"/> instance as a raw <see langword="void"/> double pointer.
        /// </summary>
        /// <returns>The raw pointer to the input <see cref="AllocationPtr"/> instance.</returns>
        /// <remarks>This method is only valid when the current <see cref="AllocationPtr"/> instance is on the stack or pinned.</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void** GetVoidAddressOf<T>()
            where T : unmanaged
        {
            return (void**)Unsafe.AsPointer(ref Unsafe.AsRef(in this));
        }

        /// <summary>
        /// Gets the address of the current <see cref="AllocationPtr"/> instance as a raw <see langword="void"/> double pointer.
        /// </summary>
        /// <returns>The raw pointer to the input <see cref="AllocationPtr"/> instance.</returns>
        /// <remarks>This method is only valid when the current <see cref="AllocationPtr"/> instance is on the stack or pinned.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AllocationPtr Move()
        {
            AllocationPtr copy = this;

            this = default;

            return copy;
        }
    }
}
