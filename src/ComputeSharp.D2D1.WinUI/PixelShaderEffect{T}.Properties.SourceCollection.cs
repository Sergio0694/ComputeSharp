using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.WinUI.Buffers;
using ComputeSharp.D2D1.WinUI.Collections;
using Windows.Graphics.Effects;

namespace ComputeSharp.D2D1.WinUI;

/// <inheritdoc/>
partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Represents the collection of <see cref="IGraphicsEffectSource"/> sources in a <see cref="PixelShaderEffect{T}"/> instance.
    /// </summary>
    /// <remarks><inheritdoc cref="EffectFactory" path="/remarks/node()"/></remarks>
    public sealed partial class SourceCollection : IList<IGraphicsEffectSource?>, IReadOnlyList<IGraphicsEffectSource?>, IList, IFixedCountList<IGraphicsEffectSource?>
    {
        /// <summary>
        /// The collection of valid resource texture indices for the current effect.
        /// </summary>
        private static readonly ImmutableArray<int> Indices = GetIndices();

        /// <summary>
        /// The fixed buffer of <see cref="IGraphicsEffectSource"/> instances.
        /// </summary>
        private GraphicsEffectSourceBuffer fixedBuffer;

        /// <summary>
        /// Creates a new <see cref="SourceCollection"/> instance with the specified parameters.
        /// </summary>
        /// <param name="owner">The owning <see cref="PixelShaderEffect{T}"/> instance.</param>
        internal SourceCollection(PixelShaderEffect<T> owner)
        {
            Owner = owner;
        }

        /// <inheritdoc/>
        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => T.InputCount;
        }

        /// <inheritdoc/>
        public IGraphicsEffectSource? this[int index]
        {
            get
            {
                default(ArgumentOutOfRangeException).ThrowIfNotInRange(index, 0, T.InputCount, nameof(index));

                return Owner.GetSource(index);
            }
            set
            {
                default(ArgumentOutOfRangeException).ThrowIfNotInRange(index, 0, T.InputCount, nameof(index));

                Owner.SetSource(value, index);
            }
        }

        /// <inheritdoc/>
        object? IList.this[int index]
        {
            get => this[index];
            set => this[index] = (IGraphicsEffectSource?)value;
        }

        /// <summary>
        /// Gets the owning <see cref="PixelShaderEffect{T}"/> instance.
        /// </summary>
        internal PixelShaderEffect<T> Owner { get; }

        /// <summary>
        /// Gets a reference to the <see cref="GraphicsEffectSourceBuffer"/> value containing the available <see cref="IGraphicsEffectSource"/>-s.
        /// </summary>
        internal ref GraphicsEffectSourceBuffer Storage => ref this.fixedBuffer;

        /// <inheritdoc/>
        ImmutableArray<int> IFixedCountList<IGraphicsEffectSource?>.Indices => Indices;

        /// <inheritdoc/>
        bool ICollection<IGraphicsEffectSource?>.IsReadOnly => false;

        /// <inheritdoc/>
        bool IList.IsReadOnly => false;

        /// <inheritdoc/>
        bool IList.IsFixedSize => true;

        /// <inheritdoc/>
        bool ICollection.IsSynchronized => true;

        /// <inheritdoc/>
        object ICollection.SyncRoot => default(NotSupportedException).Throw<object>();

        /// <inheritdoc/>
        void ICollection<IGraphicsEffectSource?>.Add(IGraphicsEffectSource? item)
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        int IList.Add(object? value)
        {
            return default(NotSupportedException).Throw<int>();
        }

        /// <inheritdoc/>
        void ICollection<IGraphicsEffectSource?>.Clear()
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        void IList.Clear()
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        bool ICollection<IGraphicsEffectSource?>.Contains(IGraphicsEffectSource? item)
        {
            return FixedCountList<IGraphicsEffectSource?>.IndexOf(this, item) != -1;
        }

        /// <inheritdoc/>
        bool IList.Contains(object? value)
        {
            return FixedCountList<IGraphicsEffectSource?>.IndexOf(this, (IGraphicsEffectSource?)value) != -1;
        }

        /// <inheritdoc/>
        void ICollection<IGraphicsEffectSource?>.CopyTo(IGraphicsEffectSource?[] array, int arrayIndex)
        {
            FixedCountList<IGraphicsEffectSource?>.CopyTo(this, array, arrayIndex);
        }

        /// <inheritdoc/>
        void ICollection.CopyTo(Array array, int index)
        {
            FixedCountList<IGraphicsEffectSource?>.CopyTo(this, array, index);
        }

        /// <inheritdoc/>
        IEnumerator<IGraphicsEffectSource?> IEnumerable<IGraphicsEffectSource?>.GetEnumerator()
        {
            return FixedCountList<IGraphicsEffectSource?>.GetEnumerator(this);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return FixedCountList<IGraphicsEffectSource?>.GetEnumerator(this);
        }

        /// <inheritdoc/>
        int IList<IGraphicsEffectSource?>.IndexOf(IGraphicsEffectSource? item)
        {
            return FixedCountList<IGraphicsEffectSource?>.IndexOf(this, item);
        }

        /// <inheritdoc/>
        int IList.IndexOf(object? value)
        {
            return FixedCountList<IGraphicsEffectSource?>.IndexOf(this, (IGraphicsEffectSource?)value);
        }

        /// <inheritdoc/>
        void IList<IGraphicsEffectSource?>.Insert(int index, IGraphicsEffectSource? item)
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        void IList.Insert(int index, object? value)
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        bool ICollection<IGraphicsEffectSource?>.Remove(IGraphicsEffectSource? item)
        {
            return default(NotSupportedException).Throw<bool>();
        }

        /// <inheritdoc/>
        void IList.Remove(object? value)
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        void IList<IGraphicsEffectSource?>.RemoveAt(int index)
        {
            default(NotSupportedException).Throw();
        }

        /// <inheritdoc/>
        void IList.RemoveAt(int index)
        {
            default(NotSupportedException).Throw();
        }

        /// <summary>
        /// Gets the collection of valid indices for the current effect.
        /// </summary>
        /// <returns>The collection of valid indices for the current effect.</returns>
        private static ImmutableArray<int> GetIndices()
        {
            int inputCount = T.InputCount;

            if (inputCount == 0)
            {
                return [];
            }

            int[] indices = new int[inputCount];

            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = i;
            }

            return ImmutableCollectionsMarshal.AsImmutableArray(indices);
        }
    }
}