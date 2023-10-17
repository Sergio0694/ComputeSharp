using System;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Shaders.Loaders;

/// <summary>
/// A data loader for D2D1 pixel shaders dispatched via <see cref="ID2D1DrawInfo"/>.
/// </summary>
internal struct D2D1ByteArrayConstantBufferLoader : ID2D1ConstantBufferLoader
{
    /// <summary>
    /// The resulting array with the constant buffer to use.
    /// </summary>
    private byte[]? data;

    /// <summary>
    /// Gets the resulting pixel shader constant buffer.
    /// </summary>
    /// <returns>A <see cref="byte"/> array with the constant buffer for the current shader.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte[] GetResultingDispatchData()
    {
        return this.data!;
    }

    /// <inheritdoc/>
    void ID2D1ConstantBufferLoader.LoadConstantBuffer(ReadOnlySpan<byte> data)
    {
        this.data = data.ToArray();
    }
}