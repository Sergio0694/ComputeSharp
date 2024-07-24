using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.WinUI.Buffers;
using ComputeSharp.D2D1.WinUI.Collections;

#pragma warning disable CS8767

namespace ComputeSharp.D2D1.WinUI;

/// <inheritdoc/>
partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Represents the collection of <see cref="D2D1ResourceTextureManager"/> objects in a <see cref="PixelShaderEffect{T}"/> instance.
    /// </summary>
    /// <remarks><inheritdoc cref="EffectFactory" path="/remarks/node()"/></remarks>
    public sealed partial class ResourceTextureManagerCollection : IList<D2D1ResourceTextureManager?>, IReadOnlyList<D2D1ResourceTextureManager?>, IList, IFixedCountList<D2D1ResourceTextureManager?>
    {
        /// <summary>
        /// The bitmask of valid indices for resource texture managers in the current shader type.
        /// </summary>
        private static readonly int IndexBitmask = GetIndexBitmask();

        /// <summary>
        /// The fixed buffer of <see cref="D2D1ResourceTextureManager"/> instances.
        /// </summary>
        private ResourceTextureManagerBuffer fixedBuffer;

        /// <summary>
        /// Creates a new <see cref="ResourceTextureManagerCollection"/> instance with the specified parameters.
        /// </summary>
        /// <param name="owner">The owning <see cref="PixelShaderEffect{T}"/> instance.</param>
        internal ResourceTextureManagerCollection(PixelShaderEffect<T> owner)
        {
            Owner = owner;
        }

        /// <inheritdoc/>
        public int Count => Indices.Length;

        /// <summary>
        /// Gets or sets the <see cref="D2D1ResourceTextureManager"/> object at a specified index.
        /// </summary>
        /// <param name="index">The index of the <see cref="D2D1ResourceTextureManager"/> source to get or set.</param>
        /// <returns>The <see cref="D2D1ResourceTextureManager"/> object at the specified index.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the value is set to <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is not a valid index for the current effect.</exception>
        [DisallowNull]
        public D2D1ResourceTextureManager? this[int index]
        {
            get
            {
                default(ArgumentOutOfRangeException).ThrowIfNotInRange(index, 0, 16, nameof(index));
                default(ArgumentOutOfRangeException).ThrowIf((IndexBitmask & (1 << index)) == 0, nameof(index));

                return Owner.GetResourceTextureManager(index);
            }
            set
            {
                default(ArgumentOutOfRangeException).ThrowIfNotInRange(index, 0, 16, nameof(index));
                default(ArgumentOutOfRangeException).ThrowIf((IndexBitmask & (1 << index)) == 0, nameof(index));
                default(ArgumentNullException).ThrowIfNull(value);

                Owner.SetResourceTextureManager(value, index);
            }
        }

        /// <inheritdoc/>
        object? IList.this[int index]
        {
            get => this[index];
            set => this[index] = (D2D1ResourceTextureManager?)value!;
        }

        /// <summary>
        /// Gets the collection of valid resource texture indices for the current effect.
        /// </summary>
        internal static ImmutableArray<int> Indices { get; } = GetIndices();

        /// <summary>
        /// Gets the owning <see cref="PixelShaderEffect{T}"/> instance.
        /// </summary>
        internal PixelShaderEffect<T> Owner { get; }

        /// <summary>
        /// Gets a reference to the <see cref="ResourceTextureManagerBuffer"/> value containing the available <see cref="D2D1ResourceTextureManager"/>-s.
        /// </summary>
        internal ref ResourceTextureManagerBuffer Storage => ref this.fixedBuffer;

        /// <inheritdoc/>
        ImmutableArray<int> IFixedCountList<D2D1ResourceTextureManager?>.Indices => Indices;

        /// <inheritdoc/>
        bool ICollection<D2D1ResourceTextureManager?>.IsReadOnly => false;

        /// <inheritdoc/>
        bool IList.IsReadOnly => false;

        /// <inheritdoc/>
        bool IList.IsFixedSize => true;

        /// <inheritdoc/>
        bool ICollection.IsSynchronized => true;

        /// <inheritdoc/>
        object ICollection.SyncRoot => default(NotSupportedException).Throw<object>();

        /// <inheritdoc/>
        void ICollection<D2D1ResourceTextureManager?>.Add(D2D1ResourceTextureManager? item)
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        int IList.Add(object? value)
        {
            return default(NotSupportedException).Throw<int>();
        }

        /// <inheritdoc/>
        void ICollection<D2D1ResourceTextureManager?>.Clear()
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        void IList.Clear()
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        bool ICollection<D2D1ResourceTextureManager?>.Contains(D2D1ResourceTextureManager? item)
        {
            return FixedCountList<D2D1ResourceTextureManager?>.IndexOf(this, item) != -1;
        }

        /// <inheritdoc/>
        bool IList.Contains(object? value)
        {
            return FixedCountList<D2D1ResourceTextureManager?>.IndexOf(this, (D2D1ResourceTextureManager?)value) != -1;
        }

        /// <inheritdoc/>
        void ICollection<D2D1ResourceTextureManager?>.CopyTo(D2D1ResourceTextureManager?[] array, int arrayIndex)
        {
            FixedCountList<D2D1ResourceTextureManager?>.CopyTo(this, array, arrayIndex);
        }

        /// <inheritdoc/>
        void ICollection.CopyTo(Array array, int index)
        {
            FixedCountList<D2D1ResourceTextureManager?>.CopyTo(this, array, index);
        }

        /// <inheritdoc/>
        IEnumerator<D2D1ResourceTextureManager?> IEnumerable<D2D1ResourceTextureManager?>.GetEnumerator()
        {
            return FixedCountList<D2D1ResourceTextureManager?>.GetEnumerator(this);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return FixedCountList<D2D1ResourceTextureManager?>.GetEnumerator(this);
        }

        /// <inheritdoc/>
        int IList<D2D1ResourceTextureManager?>.IndexOf(D2D1ResourceTextureManager? item)
        {
            return FixedCountList<D2D1ResourceTextureManager?>.IndexOf(this, item);
        }

        /// <inheritdoc/>
        int IList.IndexOf(object? value)
        {
            return FixedCountList<D2D1ResourceTextureManager?>.IndexOf(this, (D2D1ResourceTextureManager?)value);
        }

        /// <inheritdoc/>
        void IList<D2D1ResourceTextureManager?>.Insert(int index, D2D1ResourceTextureManager? item)
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        void IList.Insert(int index, object? value)
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        bool ICollection<D2D1ResourceTextureManager?>.Remove(D2D1ResourceTextureManager? item)
        {
            return default(NotSupportedException).Throw<bool>();
        }

        /// <inheritdoc/>
        void IList.Remove(object? value)
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        void IList<D2D1ResourceTextureManager?>.RemoveAt(int index)
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        void IList.RemoveAt(int index)
        {
            default(NotSupportedException).Throw();
        }

        /// <summary>
        /// Gets the bitmask of indices for resource textures in the target shader.
        /// </summary>
        /// <returns>The bitmask of indices for resource textures in the target shader.</returns>
        private static int GetIndexBitmask()
        {
            int indexBitmask = 0;

            foreach (ref readonly D2D1ResourceTextureDescription description in T.ResourceTextureDescriptions.Span)
            {
                indexBitmask |= 1 << description.Index;
            }

            return indexBitmask;
        }

        /// <summary>
        /// Gets the collection of valid indices for the current effect.
        /// </summary>
        /// <returns>The collection of valid indices for the current effect.</returns>
        private static ImmutableArray<int> GetIndices()
        {
            ReadOnlySpan<D2D1ResourceTextureDescription> descriptions = T.ResourceTextureDescriptions.Span;

            // Skip the array allocation if there are no resource texture descriptions
            if (descriptions.IsEmpty)
            {
                return [];
            }

            int[] indices = new int[descriptions.Length];

            for (int i = 0; i < descriptions.Length; i++)
            {
                indices[i] = descriptions[i].Index;
            }

            return ImmutableCollectionsMarshal.AsImmutableArray(indices);
        }
    }
}