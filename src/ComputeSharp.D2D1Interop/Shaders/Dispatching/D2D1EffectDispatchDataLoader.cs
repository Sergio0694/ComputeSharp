﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1Interop.__Internals;
using ComputeSharp.D2D1Interop.Extensions;
using TerraFX.Interop.DirectX;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1Interop.Shaders.Dispatching;

/// <summary>
/// A data loader for D2D1 pixel shaders dispatched via <see cref="ID2D1Effect"/>.
/// </summary>
internal readonly unsafe struct D2D1EffectDispatchDataLoader : ID2D1DispatchDataLoader
{
    /// <summary>
    /// The <see cref="ID2D1Effect"/> object in use.
    /// </summary>
    private readonly ID2D1Effect* d2D1Effect;

    /// <summary>
    /// Creates a new <see cref="D2D1EffectDispatchDataLoader"/> instance.
    /// </summary>
    /// <param name="d2D1Effect">The <see cref="ID2D1Effect"/> object to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal D2D1EffectDispatchDataLoader(ID2D1Effect* d2D1Effect)
    {
        this.d2D1Effect = d2D1Effect;
    }

    /// <inheritdoc/>
    public void LoadConstantBuffer(ReadOnlySpan<uint> data)
    {
        this.d2D1Effect->SetValue(
            index: 0,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BLOB,
            data: (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data)),
            dataSize: (uint)data.Length * sizeof(uint)).Assert();
    }
}
