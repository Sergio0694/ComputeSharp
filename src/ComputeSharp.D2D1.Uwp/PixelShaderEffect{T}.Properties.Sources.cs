using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Uwp.Buffers;
using ComputeSharp.D2D1.Uwp.Collections;
using Windows.Graphics.Effects;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Represents the collection of <see cref="IGraphicsEffectSource"/> sources in a <see cref="PixelShaderEffect{T}"/> instance.
    /// </summary>
    public sealed class SourceCollection : IList<IGraphicsEffectSource?>, IReadOnlyList<IGraphicsEffectSource?>, IList, IFixedCountList<IGraphicsEffectSource?>
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
            get => D2D1PixelShader.GetInputCount<T>();
        }

        /// <inheritdoc/>
        public IGraphicsEffectSource? this[int index]
        {
            get
            {
                default(ArgumentOutOfRangeException).ThrowIfNotInRange(index, 0, D2D1PixelShader.GetInputCount<T>(), nameof(index));

                return Owner.GetSource(index);
            }
            set
            {
                default(ArgumentOutOfRangeException).ThrowIfNotInRange(index, 0, D2D1PixelShader.GetInputCount<T>(), nameof(index));

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
        /// The owning <see cref="PixelShaderEffect{T}"/> instance.
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
        object ICollection.SyncRoot => throw new NotSupportedException("ICollection.SyncRoot is not supported for SourceCollection.");

        /// <inheritdoc/>
        void ICollection<IGraphicsEffectSource?>.Add(IGraphicsEffectSource? item)
        {
            throw new NotSupportedException("ICollection<T>.Add is not supported for SourceCollection.");
        }

        /// <inheritdoc/>
        int IList.Add(object value)
        {
            throw new NotSupportedException("IList.Add is not supported for SourceCollection.");
        }

        /// <inheritdoc/>
        void ICollection<IGraphicsEffectSource?>.Clear()
        {
            throw new NotSupportedException("ICollection<T>.Clear is not supported for SourceCollection.");
        }

        /// <inheritdoc/>
        void IList.Clear()
        {
            throw new NotSupportedException("IList.Clear is not supported for SourceCollection.");
        }

        /// <inheritdoc/>
        bool ICollection<IGraphicsEffectSource?>.Contains(IGraphicsEffectSource? item)
        {
            return FixedCountList<IGraphicsEffectSource?>.IndexOf(this, item) != -1;
        }

        /// <inheritdoc/>
        bool IList.Contains(object value)
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
        int IList.IndexOf(object value)
        {
            return FixedCountList<IGraphicsEffectSource?>.IndexOf(this, (IGraphicsEffectSource?)value);
        }

        /// <inheritdoc/>
        void IList<IGraphicsEffectSource?>.Insert(int index, IGraphicsEffectSource? item)
        {
            throw new NotSupportedException("IList<T>.Insert is not supported for SourceCollection.");
        }

        /// <inheritdoc/>
        void IList.Insert(int index, object value)
        {
            throw new NotSupportedException("IList.Insert is not supported for SourceCollection.");
        }

        /// <inheritdoc/>
        bool ICollection<IGraphicsEffectSource?>.Remove(IGraphicsEffectSource? item)
        {
            throw new NotSupportedException("ICollection<T>.Remove is not supported for SourceCollection.");
        }

        /// <inheritdoc/>
        void IList.Remove(object value)
        {
            throw new NotSupportedException("IList.Remove is not supported for SourceCollection.");
        }

        /// <inheritdoc/>
        void IList<IGraphicsEffectSource?>.RemoveAt(int index)
        {
            throw new NotSupportedException("IList<T>.RemoveAt is not supported for SourceCollection.");
        }

        /// <inheritdoc/>
        void IList.RemoveAt(int index)
        {
            throw new NotSupportedException("IList.RemoveAt is not supported for SourceCollection.");
        }

        /// <summary>
        /// Gets the collection of valid indices for the current effect.
        /// </summary>
        /// <returns>The collection of valid indices for the current effect.</returns>
        private static unsafe ImmutableArray<int> GetIndices()
        {
            int inputCount = D2D1PixelShader.GetInputCount<T>();

            if (inputCount == 0)
            {
                return ImmutableArray<int>.Empty;
            }

            int[] indices = new int[inputCount];

            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = i;
            }

            return *(ImmutableArray<int>*)&indices;
        }
    }
}