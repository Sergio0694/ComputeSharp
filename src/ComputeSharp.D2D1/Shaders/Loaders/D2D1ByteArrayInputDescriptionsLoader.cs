using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.Interop;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Shaders.Loaders;

/// <summary>
/// An input descriptions loader for D2D1 pixel shaders.
/// </summary>
internal struct D2D1ByteArrayInputDescriptionsLoader : ID2D1InputDescriptionsLoader
{
    /// <summary>
    /// The resulting <see cref="D2D1InputDescription"/> array.
    /// </summary>
    private D2D1InputDescription[]? inputDescriptions;

    /// <summary>
    /// Gets the resulting input descriptions.
    /// </summary>
    /// <returns>A <see cref="D2D1InputDescription"/> array with the available input descriptions.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public D2D1InputDescription[] GetResultingInputDescriptions()
    {
        return this.inputDescriptions!;
    }

    /// <inheritdoc/>
    void ID2D1InputDescriptionsLoader.LoadInputDescriptions(ReadOnlySpan<byte> data)
    {
        this.inputDescriptions = MemoryMarshal.Cast<byte, D2D1InputDescription>(data).ToArray();
    }
}