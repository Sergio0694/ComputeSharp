using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.Interop;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Shaders.Loaders;

/// <summary>
/// A resource texture index array loader for D2D1 pixel shaders.
/// </summary>
internal struct D2D1ImmutableArrayResourceTextureDescriptionsLoader : ID2D1ResourceTextureDescriptionsLoader
{
    /// <summary>
    /// The array of indices for resource textures for the current effect.
    /// </summary>
    private ImmutableArray<int> indices;

    /// <summary>
    /// Gets the resulting array of indices for resource textures for the current effect.
    /// </summary>
    /// <returns>An array of indices for resource textures for the current effect.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ImmutableArray<int> GetResultingIndices()
    {
        return this.indices;
    }

    /// <inheritdoc/>
    unsafe void ID2D1ResourceTextureDescriptionsLoader.LoadResourceTextureDescriptions(ReadOnlySpan<byte> data)
    {
        if (data.IsEmpty)
        {
            this.indices = ImmutableArray<int>.Empty;
        }
        else
        {
            ReadOnlySpan<D2D1ResourceTextureDescription> descriptions = MemoryMarshal.Cast<byte, D2D1ResourceTextureDescription>(data);

            int[] indices = new int[descriptions.Length];

            for (int i = 0; i < descriptions.Length; i++)
            {
                indices[i] = descriptions[i].Index;
            }

            this.indices = *(ImmutableArray<int>*)&indices;
        }
    }
}