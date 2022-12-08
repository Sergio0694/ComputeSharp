using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Helpers;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Shaders.Loaders;
using ComputeSharp.D2D1.Uwp.Buffers;

#pragma warning disable CS0618

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

        /// <summary>
        /// Gets the collection of valid resource texture indices for the current effect.
        /// </summary>
        internal static ImmutableArray<int> Indices { get; } = GetIndices();

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
                ValidateIndex(index);

                return Owner.GetResourceTextureManager(index);
            }
            set
            {
                if (value is null)
                {
                    ThrowHelper.ThrowArgumentNullException(nameof(value), "Input resource texture managers cannot be null.");
                }

                ValidateIndex(index);

                Owner.SetResourceTextureManager(value, index);
            }
        }

        /// <summary>
        /// The owning <see cref="PixelShaderEffect{T}"/> instance.
        /// </summary>
        internal PixelShaderEffect<T> Owner { get; }

        /// <summary>
        /// Gets a reference to the <see cref="ResourceTextureManagerBuffer"/> value containing the available <see cref="D2D1ResourceTextureManager"/>-s.
        /// </summary>
        internal ref ResourceTextureManagerBuffer Storage => ref this.fixedBuffer;

        /// <summary>
        /// Validates the input index for <see cref="this[int]"/>.
        /// </summary>
        /// <param name="index">The index to validate.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is out of range.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateIndex(int index)
        {
            if ((uint)index >= 16 || (IndexBitmask & (1 << index)) == 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(index), "The input index is not a valid resource texture manager index for the current effect.");
            }
        }

        /// <summary>
        /// Gets the bitmask of indices for resource textures in the target shader.
        /// </summary>
        /// <returns>The bitmask of indices for resource textures in the target shader.</returns>
        private static int GetIndexBitmask()
        {
            D2D1IndexBitmaskResourceTextureDescriptionsLoader bitmaskLoader = default;

            Unsafe.SkipInit(out T shader);

            shader.LoadResourceTextureDescriptions(ref bitmaskLoader);

            return bitmaskLoader.GetResultingIndexBitmask();
        }

        /// <summary>
        /// Gets the collection of valid indices for the current effect.
        /// </summary>
        /// <returns>The collection of valid indices for the current effect.</returns>
        private static ImmutableArray<int> GetIndices()
        {
            D2D1ImmutableArrayResourceTextureDescriptionsLoader indicesLoader = default;

            Unsafe.SkipInit(out T shader);

            shader.LoadResourceTextureDescriptions(ref indicesLoader);

            return indicesLoader.GetResultingIndices();
        }
    }
}