using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.Interop;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Shaders.Loaders;

/// <summary>
/// A resource texture index bitmask loader for D2D1 pixel shaders.
/// </summary>
internal struct D2D1IndexBitmaskResourceTextureDescriptionsLoader : ID2D1ResourceTextureDescriptionsLoader
{
    /// <summary>
    /// The bitmask of indices for resource textures in the target shader.
    /// </summary>
    private int indexBitmask;

    /// <summary>
    /// Gets the resulting bitmask of indices for resource textures in the target shader.
    /// </summary>
    /// <returns>A bitmask of indices for resource textures in the target shader.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetResultingIndexBitmask()
    {
        return this.indexBitmask;
    }

    /// <inheritdoc/>
    void ID2D1ResourceTextureDescriptionsLoader.LoadResourceTextureDescriptions(ReadOnlySpan<byte> data)
    {
        int indexBitmask = 0;

        foreach (ref readonly D2D1ResourceTextureDescription description in MemoryMarshal.Cast<byte, D2D1ResourceTextureDescription>(data))
        {
            indexBitmask |= 1 << description.Index;
        }

        this.indexBitmask = indexBitmask;
    }
}