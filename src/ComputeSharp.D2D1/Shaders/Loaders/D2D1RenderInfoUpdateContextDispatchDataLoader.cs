using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Shaders.Loaders;

/// <summary>
/// A data loader for D2D1 pixel shaders to be passed onto an <see cref="ID2D1RenderInfoUpdateContext"/> object.
/// </summary>
internal readonly unsafe struct D2D1RenderInfoUpdateContextDispatchDataLoader : ID2D1DispatchDataLoader
{
    /// <summary>
    /// The <see cref="ID2D1RenderInfoUpdateContext"/> object in use.
    /// </summary>
    private readonly ID2D1RenderInfoUpdateContext* d2D1RenderInfoUpdateContext;

    /// <summary>
    /// Creates a new <see cref="D2D1RenderInfoUpdateContextDispatchDataLoader"/> instance.
    /// </summary>
    /// <param name="d2D1RenderInfoUpdateContext">The <see cref="ID2D1RenderInfoUpdateContext"/> object to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal D2D1RenderInfoUpdateContextDispatchDataLoader(ID2D1RenderInfoUpdateContext* d2D1RenderInfoUpdateContext)
    {
        this.d2D1RenderInfoUpdateContext = d2D1RenderInfoUpdateContext;
    }

    /// <inheritdoc/>
    void ID2D1DispatchDataLoader.LoadConstantBuffer(ReadOnlySpan<byte> data)
    {
        this.d2D1RenderInfoUpdateContext->SetConstantBuffer(
            buffer: (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data)),
            bufferCount: (uint)data.Length).Assert();
    }
}