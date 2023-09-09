using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.DirectX;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Shaders.Loaders;

/// <summary>
/// A data loader for D2D1 pixel shaders dispatched via <see cref="ID2D1ComputeInfo"/>.
/// </summary>
internal readonly unsafe struct D2D1ComputeInfoDispatchDataLoader : ID2D1DispatchDataLoader
{
    /// <summary>
    /// The <see cref="ID2D1ComputeInfo"/> object in use.
    /// </summary>
    private readonly ID2D1ComputeInfo* d2D1ComputeInfo;

    /// <summary>
    /// Creates a new <see cref="D2D1DrawInfoDispatchDataLoader"/> instance.
    /// </summary>
    /// <param name="d2D1ComputeInfo">The <see cref="ID2D1ComputeInfo"/> object to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal D2D1ComputeInfoDispatchDataLoader(ID2D1ComputeInfo* d2D1ComputeInfo)
    {
        this.d2D1ComputeInfo = d2D1ComputeInfo;
    }

    /// <inheritdoc/>
    void ID2D1DispatchDataLoader.LoadConstantBuffer(ReadOnlySpan<byte> data)
    {
        this.d2D1ComputeInfo->SetComputeShaderConstantBuffer(
            buffer: (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data)),
            bufferCount: (uint)data.Length).Assert();
    }
}