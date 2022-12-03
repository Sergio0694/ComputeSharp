using System;
using System.Diagnostics.CodeAnalysis;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Uwp.Buffers;

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
        private ResourceTextureManagerBuffer fixedBuffer;

        /// <summary>
        /// Creates a new <see cref="ResourceTextureManagerCollection"/> instance with the specified parameters.
        /// </summary>
        /// <param name="owner">The owning <see cref="PixelShaderEffect{T}"/> instance.</param>
        internal ResourceTextureManagerCollection(PixelShaderEffect<T> owner)
        {
            Owner = owner;
        }

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
        /// The owning <see cref="PixelShaderEffect{T}"/> instance.
        /// </summary>
        internal PixelShaderEffect<T> Owner { get; }

        /// <summary>
        /// Gets a reference to the <see cref="ResourceTextureManagerBuffer"/> value containing the available <see cref="D2D1ResourceTextureManager"/>-s.
        /// </summary>
        internal ref ResourceTextureManagerBuffer Storage => ref this.fixedBuffer;
    }
}