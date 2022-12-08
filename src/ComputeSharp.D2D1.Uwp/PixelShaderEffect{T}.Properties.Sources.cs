using System;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Helpers;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Uwp.Buffers;
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
        private GraphicsEffectSourceBuffer fixedBuffer;

        /// <summary>
        /// Creates a new <see cref="SourceCollection"/> instance with the specified parameters.
        /// </summary>
        /// <param name="owner">The owning <see cref="PixelShaderEffect{T}"/> instance.</param>
        internal SourceCollection(PixelShaderEffect<T> owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Gets the actual input count for the current effect.
        /// </summary>
        internal static int Count { get; } = D2D1PixelShader.GetInputCount<T>();

        /// <summary>
        /// Gets or sets the <see cref="IGraphicsEffectSource"/> source at a specified index.
        /// </summary>
        /// <param name="index">The index of the <see cref="IGraphicsEffectSource"/> source to get or set.</param>
        /// <returns>The <see cref="IGraphicsEffectSource"/> source at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is not a valid index for the current effect.</exception>
        public IGraphicsEffectSource? this[int index]
        {
            get
            {
                ValidateIndex(index);

                return Owner.GetSource(index);
            }
            set
            {
                ValidateIndex(index);

                Owner.SetSource(value, index);
            }
        }

        /// <summary>
        /// The owning <see cref="PixelShaderEffect{T}"/> instance.
        /// </summary>
        internal PixelShaderEffect<T> Owner { get; }

        /// <summary>
        /// Gets a reference to the <see cref="GraphicsEffectSourceBuffer"/> value containing the available <see cref="IGraphicsEffectSource"/>-s.
        /// </summary>
        internal ref GraphicsEffectSourceBuffer Storage => ref this.fixedBuffer;

        /// <summary>
        /// Validates the input index for <see cref="this[int]"/>.
        /// </summary>
        /// <param name="index">The index to validate.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is out of range.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateIndex(int index)
        {
            if ((uint)index >= Count)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(index), "The input index is not a valid source index for the current effect.");
            }
        }
    }
}