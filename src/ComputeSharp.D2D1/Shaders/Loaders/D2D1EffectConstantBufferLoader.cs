using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.DirectX;

namespace ComputeSharp.D2D1.Shaders.Loaders;

/// <summary>
/// A data loader for D2D1 pixel shaders dispatched via <see cref="ID2D1Effect"/>.
/// </summary>
internal readonly unsafe struct D2D1EffectConstantBufferLoader : ID2D1ConstantBufferLoader
{
    /// <summary>
    /// The <see cref="ID2D1Effect"/> object in use.
    /// </summary>
    private readonly ID2D1Effect* d2D1Effect;

    /// <summary>
    /// Creates a new <see cref="D2D1EffectConstantBufferLoader"/> instance.
    /// </summary>
    /// <param name="d2D1Effect">The <see cref="ID2D1Effect"/> object to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal D2D1EffectConstantBufferLoader(ID2D1Effect* d2D1Effect)
    {
        this.d2D1Effect = d2D1Effect;
    }

    /// <inheritdoc/>
    void ID2D1ConstantBufferLoader.LoadConstantBuffer(ReadOnlySpan<byte> data)
    {
        if (data.IsEmpty)
        {
            return;
        }

        this.d2D1Effect->SetValue(
            index: 0,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BLOB,
            data: (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data)),
            dataSize: (uint)data.Length).Assert();
    }
}