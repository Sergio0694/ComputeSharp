using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Helpers;
using ComputeSharp.D2D1.Interop;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Represents the collection of <see cref="D2D1ResourceTextureManager"/> objects in a <see cref="PixelShaderEffect{T}"/> instance.
    /// </summary>
    public sealed class ResourceTextureManagerCollection
    {
        /// <summary>
        /// The fixed buffer of <see cref="D2D1ResourceTextureManager"/> instances.
        /// </summary>
        private FixedBuffer fixedBuffer;

        /// <summary>
        /// Creates a new <see cref="ResourceTextureManagerCollection"/> instance with the specified parameters.
        /// </summary>
        /// <param name="owner">The owning <see cref="PixelShaderEffect{T}"/> instance.</param>
        internal ResourceTextureManagerCollection(PixelShaderEffect<T> owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// The owning <see cref="PixelShaderEffect{T}"/> instance.
        /// </summary>
        internal PixelShaderEffect<T> Owner { get; }

        /// <summary>
        /// Gets a reference to the <see cref="FixedBuffer"/> value containing the available <see cref="D2D1ResourceTextureManager"/>-s.
        /// </summary>
        internal ref FixedBuffer Storage => ref this.fixedBuffer;

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
            get => Owner.GetResourceTextureManager(index);
            set => Owner.SetResourceTextureManager(value, index);
        }

        /// <summary>
        /// A fixed buffer type containing 16 <see cref="D2D1ResourceTextureManager"/> fields.
        /// </summary>
        internal struct FixedBuffer
        {
            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 0.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager0;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 1.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager1;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 2.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager2;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 3.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager3;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 4.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager4;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 5.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager5;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 6.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager6;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 7.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager7;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 8.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager8;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 9.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager9;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 10.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager10;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 11.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager11;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 12.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager12;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 13.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager13;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 14.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager14;

            /// <summary>
            /// The <see cref="D2D1ResourceTextureManager"/> instance at index 15.
            /// </summary>
            public D2D1ResourceTextureManager? ResourceTextureManager15;

            /// <summary>
            /// Gets a reference to the <see cref="D2D1ResourceTextureManager"/> value at a given index.
            /// </summary>
            /// <param name="index">The index of the <see cref="D2D1ResourceTextureManager"/> value to get a reference to.</param>
            /// <returns>A reference to the <see cref="D2D1ResourceTextureManager"/> value at a given index.</returns>
            /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is not a valid index for the current effect.</exception>
            [UnscopedRef]
            public ref D2D1ResourceTextureManager? this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    if ((uint)index >= 16)
                    {
                        ThrowHelper.ThrowArgumentOutOfRangeException(nameof(index), "The index must be in the [0, 15] range.");
                    }

                    ref D2D1ResourceTextureManager? r0 = ref Unsafe.As<FixedBuffer, D2D1ResourceTextureManager?>(ref this);
                    ref D2D1ResourceTextureManager? r1 = ref Unsafe.Add(ref r0, index);

                    return ref r1;
                }
            }
        }
    }
}