using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Shaders.Loaders;

/// <summary>
/// A data loader for D2D1 pixel shaders to be passed onto an <see cref="ID2D1DrawInfoUpdateContext"/> object.
/// </summary>
internal readonly unsafe struct D2D1DrawInfoUpdateContextDispatchDataLoader : ID2D1DispatchDataLoader
{
    /// <summary>
    /// The <see cref="ID2D1DrawInfoUpdateContext"/> object in use.
    /// </summary>
    private readonly ID2D1DrawInfoUpdateContext* d2D1DrawInfoUpdateContext;

    /// <summary>
    /// Creates a new <see cref="D2D1DrawInfoUpdateContextDispatchDataLoader"/> instance.
    /// </summary>
    /// <param name="d2D1DrawInfoUpdateContext">The <see cref="ID2D1DrawInfoUpdateContext"/> object to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal D2D1DrawInfoUpdateContextDispatchDataLoader(ID2D1DrawInfoUpdateContext* d2D1DrawInfoUpdateContext)
    {
        this.d2D1DrawInfoUpdateContext = d2D1DrawInfoUpdateContext;
    }

    /// <inheritdoc/>
    void ID2D1DispatchDataLoader.LoadConstantBuffer(ReadOnlySpan<byte> data)
    {
        this.d2D1DrawInfoUpdateContext->SetConstantBuffer(
            buffer: (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data)),
            bufferCount: (uint)data.Length).Assert();
    }
}