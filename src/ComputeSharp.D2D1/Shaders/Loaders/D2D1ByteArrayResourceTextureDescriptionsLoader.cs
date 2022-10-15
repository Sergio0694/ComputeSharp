using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.Interop;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Shaders.Loaders;

/// <summary>
/// A resource texture descriptions loader for D2D1 pixel shaders.
/// </summary>
internal struct D2D1ByteArrayResourceTextureDescriptionsLoader : ID2D1ResourceTextureDescriptionsLoader
{
    /// <summary>
    /// The resulting <see cref="D2D1ResourceTextureDescription"/> array.
    /// </summary>
    private D2D1ResourceTextureDescription[]? resourceTextureDescriptions;

    /// <summary>
    /// Gets the resulting resource texture descriptions.
    /// </summary>
    /// <returns>A <see cref="D2D1ResourceTextureDescription"/> array with the available resource texture descriptions.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public D2D1ResourceTextureDescription[] GetResultingResourceTextureDescriptions()
    {
        return this.resourceTextureDescriptions!;
    }

    /// <inheritdoc/>
    void ID2D1ResourceTextureDescriptionsLoader.LoadResourceTextureDescriptions(ReadOnlySpan<byte> data)
    {
        this.resourceTextureDescriptions = MemoryMarshal.Cast<byte, D2D1ResourceTextureDescription>(data).ToArray();
    }
}