using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Shaders.Loaders;

/// <summary>
/// A data loader for D2D1 pixel shaders dispatched via <see cref="ID2D1DrawInfo"/>.
/// </summary>
internal readonly unsafe struct D2D1DrawInfoConstantBufferLoader : ID2D1ConstantBufferLoader
{
    /// <summary>
    /// The <see cref="ID2D1DrawInfo"/> object in use.
    /// </summary>
    private readonly ID2D1DrawInfo* d2D1DrawInfo;

    /// <summary>
    /// Creates a new <see cref="D2D1DrawInfoConstantBufferLoader"/> instance.
    /// </summary>
    /// <param name="d2D1DrawInfo">The <see cref="ID2D1DrawInfo"/> object to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal D2D1DrawInfoConstantBufferLoader(ID2D1DrawInfo* d2D1DrawInfo)
    {
        this.d2D1DrawInfo = d2D1DrawInfo;
    }

    /// <inheritdoc/>
    void ID2D1ConstantBufferLoader.LoadConstantBuffer(ReadOnlySpan<byte> data)
    {
        this.d2D1DrawInfo->SetPixelShaderConstantBuffer(
            buffer: (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data)),
            bufferCount: (uint)data.Length).Assert();
    }
}