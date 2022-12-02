using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Helpers;
using ComputeSharp.D2D1.Uwp.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
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
            /// The <see cref="SourceReference"/> instance at index 0.
            /// </summary>
            public SourceReference Source0;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 1.
            /// </summary>
            public SourceReference Source1;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 2.
            /// </summary>
            public SourceReference Source2;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 3.
            /// </summary>
            public SourceReference Source3;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 4.
            /// </summary>
            public SourceReference Source4;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 5.
            /// </summary>
            public SourceReference Source5;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 6.
            /// </summary>
            public SourceReference Source6;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 7.
            /// </summary>
            public SourceReference Source7;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 8.
            /// </summary>
            public SourceReference Source8;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 9.
            /// </summary>
            public SourceReference Source9;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 10.
            /// </summary>
            public SourceReference Source10;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 11.
            /// </summary>
            public SourceReference Source11;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 12.
            /// </summary>
            public SourceReference Source12;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 13.
            /// </summary>
            public SourceReference Source13;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 14.
            /// </summary>
            public SourceReference Source14;

            /// <summary>
            /// The <see cref="SourceReference"/> instance at index 15.
            /// </summary>
            public SourceReference Source15;

            /// <summary>
            /// Gets a reference to the <see cref="SourceReference"/> value at a given index.
            /// </summary>
            /// <param name="index">The index of the <see cref="SourceReference"/> value to get a reference to.</param>
            /// <returns>A reference to the <see cref="SourceReference"/> value at a given index.</returns>
            /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is not a valid index for the current effect.</exception>
            [UnscopedRef]
            public ref SourceReference this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    if ((uint)index >= 16)
                    {
                        ThrowHelper.ThrowArgumentOutOfRangeException(nameof(index), "The index must be in the [0, 15] range.");
                    }

                    ref SourceReference r0 = ref Unsafe.As<FixedBuffer, SourceReference>(ref this);
                    ref SourceReference r1 = ref Unsafe.Add(ref r0, index);

                    return ref r1;
                }
            }
        }

        /// <summary>
        /// A wrapper type for an input <see cref="IGraphicsEffectSource"/> and its underlying native D2D images.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This type is meant to support these two common operations:
        /// <list type="bullet">
        ///     <item>Look up a D2D resource from a property of some other D2D interface.</item>
        ///     <item>Get a managed wrapper from a D2D interface.</item>
        /// </list>
        /// With this, the generated D2D objects can be associated with each original managed wrapper. Similarly,
        /// when the other way around is used, this type can also store the generated wrapper for future use.
        /// </para>
        /// <para>
        /// This gives the following improvements:
        /// <list type="bullet">
        ///     <item>Improved performance for the common case of repeated calls using the same D2D instance.</item>
        ///     <item>Maintaining a reference to the wrapper avoids it being repeatedly released and recreated.</item>
        /// </list>
        /// </para>
        /// <para>
        /// Just like with all other members of an effect, values in this object are considered authoritative when the
        /// effect is not realized, otherwise they just act as a cache to speedup the reverse managed wrapper lookups.
        /// </para>
        /// <para>
        /// For realized effects, <see cref="SourceReference"/> tracks both the <see cref="ID2D1Image"/> resource and its
        /// <see cref="IGraphicsEffectSource"/> wrapper (and optional <see cref="ID2D1Effect"/> being the DPI compensation effect,
        /// if one is needed. For unrealized effects, the resource is <see langword="null"/> and only the wrapper part is used.
        /// </para>
        /// </remarks>
        internal unsafe struct SourceReference
        {
            /// <summary>
            /// The <see cref="ID2D1Image"/> produced by calling <see cref="ABI.Microsoft.Graphics.Canvas.ICanvasImageInterop.Interface.GetD2DImage"/>
            /// on the input managed wrapper used as source for the effect (ie. <see cref="graphicsEffectSourceWrapper"/>).
            /// </summary>
            private ComPtr<ID2D1Image> d2D1ImageSource;

            /// <summary>
            /// The <see cref="ID2D1Effect"/> for DPI compensation wrapping <see cref="d2D1ImageSource"/>, if needed.
            /// </summary>
            private ComPtr<ID2D1Effect> d2D1EffectDpiCompensation;

            /// <summary>
            /// The <see cref="IGraphicsEffectSource"/> instance being a managed wrapper for the current effect input.
            /// </summary>
            private IGraphicsEffectSource? graphicsEffectSourceWrapper;

            /// <summary>
            /// Gets whether or not this source ference has a DPI compensation effect.
            /// </summary>
            public readonly bool HasDpiCompensationEffect
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => this.d2D1EffectDpiCompensation.Get() is not null;
            }

            /// <summary>
            /// Gets the <see cref="ID2D1Effect"/> object for the DPI compensation effect used, if any.
            /// </summary>
            /// <returns>The <see cref="ID2D1Effect"/> object for the DPI compensation effect used, if any.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ID2D1Effect* GetDpiCompensationEffect()
            {
                return this.d2D1EffectDpiCompensation.Get();
            }

            /// <summary>
            /// Sets the DPI compensation effet being used,
            /// </summary>
            /// <param name="d2D1Effect"></param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetDpiCompensationEffect(ID2D1Effect* d2D1Effect)
            {
                _ = this.d2D1EffectDpiCompensation.Reset();

                if (d2D1Effect is not null)
                {
                    this.d2D1EffectDpiCompensation = new ComPtr<ID2D1Effect>(d2D1Effect);
                }
            }

            /// <summary>
            /// Gets the <see cref="IGraphicsEffectSource"/> instance being a managed wrapper for the current effect input.
            /// </summary>
            /// <returns>The <see cref="IGraphicsEffectSource"/> instance being a managed wrapper for the current effect input.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly IGraphicsEffectSource? GetWrapper()
            {
                return this.graphicsEffectSourceWrapper;
            }

            /// <summary>
            /// Gets or creates an <see cref="IGraphicsEffectSource"/> wrapper for a given <see cref="ID2D1Image"/> object.
            /// </summary>
            /// <param name="canvasDevice">The input <see cref="ICanvasDevice"/> object the source effect is realized on.</param>
            /// <param name="d2D1Image">The input <see cref="ID2D1Image"/> object to get or create a wrapper for.</param>
            /// <returns>An <see cref="IGraphicsEffectSource"/> wrapper for a given <see cref="ID2D1Image"/> object.</returns>
            public IGraphicsEffectSource? GetOrCreateWrapper(ICanvasDevice* canvasDevice, ID2D1Image* d2D1Image)
            {
                // Check if there already is a matching managed wrapper for the input image
                if (!this.d2D1ImageSource.Get()->IsSameInstance(d2D1Image))
                {
                    // If that's not the case, create a new wrapper if the input image is not null
                    if (d2D1Image is not null)
                    {
                        // m_wrapper = ResourceManager::GetOrCreate<TWrapper>(device, resource);
                        throw new NotImplementedException();
                    }
                    else
                    {
                        _ = this.d2D1ImageSource.Reset();
                    }

                    _ = this.d2D1ImageSource.Reset();

                    this.d2D1ImageSource = new ComPtr<ID2D1Image>(d2D1Image);
                }

                return this.graphicsEffectSourceWrapper;
            }
        }
    }
}