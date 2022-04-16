﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.__Internals;
using TerraFX.Interop.DirectX;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Shaders.Dispatching;

/// <summary>
/// A data loader for D2D1 pixel shaders dispatched via <see cref="ID2D1DrawInfo"/>.
/// </summary>
internal struct D2D1ByteArrayDispatchDataLoader : ID2D1DispatchDataLoader
{
    /// <summary>
    /// The <see cref="ID2D1DrawInfo"/> object in use.
    /// </summary>
    private byte[]? data;

    /// <summary>
    /// Gets the resulting pixel shader constant buffer.
    /// </summary>
    /// <remarks>A <see cref="byte"/> array with the constant buffer for the current shader.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte[] GetResultingDispatchData()
    {
        return this.data!;
    }

    /// <inheritdoc/>
    public void LoadConstantBuffer(ReadOnlySpan<uint> data)
    {
        this.data = MemoryMarshal.AsBytes(data).ToArray();
    }
}
