using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Helpers;
using Windows.Graphics.Effects;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Represents the collection of <see cref="IGraphicsEffectSource"/> sources in a <see cref="PixelShaderEffect{T}"/> instance.
    /// </summary>
    public sealed class SourceCollection
    {
        /// <summary>
        /// The fixed buffer of <see cref="IGraphicsEffectSource"/> instances.
        /// </summary>
        private FixedBuffer fixedBuffer;

        /// <summary>
        /// Creates a new <see cref="SourceCollection"/> instance with the specified parameters.
        /// </summary>
        /// <param name="owner">The owning <see cref="PixelShaderEffect{T}"/> instance.</param>
        internal SourceCollection(PixelShaderEffect<T> owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Gets or sets the <see cref="IGraphicsEffectSource"/> source at a specified index.
        /// </summary>
        /// <param name="index">The index of the <see cref="IGraphicsEffectSource"/> source to get or set.</param>
        /// <returns>The <see cref="IGraphicsEffectSource"/> source at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is not a valid index for the current effect.</exception>
        public IGraphicsEffectSource this[int index]
        {
            get => null!;
            set { }
        }

        /// <summary>
        /// The owning <see cref="PixelShaderEffect{T}"/> instance.
        /// </summary>
        internal PixelShaderEffect<T> Owner { get; }

        /// <summary>
        /// Gets a reference to the <see cref="FixedBuffer"/> value containing the available <see cref="IGraphicsEffectSource"/>-s.
        /// </summary>
        internal ref FixedBuffer Storage => ref this.fixedBuffer;

        /// <summary>
        /// A fixed buffer type containing 16 <see cref="IGraphicsEffectSource"/> fields.
        /// </summary>
        internal struct FixedBuffer
        {
            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 0.
            /// </summary>
            public IGraphicsEffectSource? Source0;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 1.
            /// </summary>
            public IGraphicsEffectSource? Source1;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 2.
            /// </summary>
            public IGraphicsEffectSource? Source2;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 3.
            /// </summary>
            public IGraphicsEffectSource? Source3;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 4.
            /// </summary>
            public IGraphicsEffectSource? Source4;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 5.
            /// </summary>
            public IGraphicsEffectSource? Source5;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 6.
            /// </summary>
            public IGraphicsEffectSource? Source6;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 7.
            /// </summary>
            public IGraphicsEffectSource? Source7;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 8.
            /// </summary>
            public IGraphicsEffectSource? Source8;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 9.
            /// </summary>
            public IGraphicsEffectSource? Source9;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 10.
            /// </summary>
            public IGraphicsEffectSource? Source10;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 11.
            /// </summary>
            public IGraphicsEffectSource? Source11;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 12.
            /// </summary>
            public IGraphicsEffectSource? Source12;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 13.
            /// </summary>
            public IGraphicsEffectSource? Source13;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 14.
            /// </summary>
            public IGraphicsEffectSource? Source14;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance at index 15.
            /// </summary>
            public IGraphicsEffectSource? Source15;

            /// <summary>
            /// Gets a reference to the <see cref="IGraphicsEffectSource"/> value at a given index.
            /// </summary>
            /// <param name="index">The index of the <see cref="IGraphicsEffectSource"/> value to get a reference to.</param>
            /// <returns>A reference to the <see cref="IGraphicsEffectSource"/> value at a given index.</returns>
            /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is not a valid index for the current effect.</exception>
            [UnscopedRef]
            public ref IGraphicsEffectSource? this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    if ((uint)index >= 16)
                    {
                        ThrowHelper.ThrowArgumentOutOfRangeException(nameof(index), "The index must be in the [0, 15] range.");
                    }

                    ref IGraphicsEffectSource? r0 = ref Unsafe.As<FixedBuffer, IGraphicsEffectSource?>(ref this);
                    ref IGraphicsEffectSource? r1 = ref Unsafe.Add(ref r0, index);

                    return ref r1;
                }
            }
        }
    }
}