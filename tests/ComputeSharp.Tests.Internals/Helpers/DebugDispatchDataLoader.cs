using System;
using System.Collections.Generic;
using ComputeSharp.__Internals;

namespace ComputeSharp.Tests.Internals.Helpers;

/// <summary>
/// A debug data loader for generic shaders.
/// </summary>
internal struct DebugDispatchDataLoader : IConstantBufferLoader, IGraphicsResourceLoader
{
    /// <summary>
    /// The constant buffer, if set.
    /// </summary>
    private byte[]? constantBuffer;

    /// <summary>
    /// The list of loaded resources.
    /// </summary>
    private readonly List<(IGraphicsResource Resource, uint Index)> graphicsResources = new();

    /// <summary>
    /// Creates a new <see cref="DebugDispatchDataLoader"/> instance.
    /// </summary>
    public DebugDispatchDataLoader()
    {
    }

    /// <summary>
    /// Gets the constant buffer.
    /// </summary>
    public byte[]? ConstantBuffer => this.constantBuffer;

    /// <summary>
    /// Gets the loaded resources.
    /// </summary>
    public IReadOnlyList<(IGraphicsResource Resource, uint Index)> GraphicsResources => this.graphicsResources;

    /// <inheritdoc/>
    void IConstantBufferLoader.LoadConstantBuffer(ReadOnlySpan<byte> data)
    {
        this.constantBuffer = data.ToArray();
    }

    /// <inheritdoc/>
    readonly void IGraphicsResourceLoader.LoadGraphicsResource(IGraphicsResource resource, uint index)
    {
        this.graphicsResources.Add((resource, index));
    }
}