using System;
using System.Diagnostics.CodeAnalysis;
using ComputeSharp.D2D1.Interop;
using Microsoft.Graphics.Canvas;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Gets or sets the current <typeparamref name="T"/> value in use.
    /// </summary>
    public T ConstantBuffer
    {
        get => GetConstantBuffer();
        set => SetConstantBuffer(in value);
    }

    /// <summary>
    /// Gets or sets the <see cref="D2D1TransformMapper{T}"/> instance to use for the effect, if any.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if the value is set to <see langword="null"/>.</exception>
    [DisallowNull]
    public D2D1TransformMapper<T>? TransformMapper
    {
        get => GetTransformMapper();
        set => SetTransformMapper(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether to enable caching the output from drawing this effect.
    /// </summary>
    public bool CacheOutput
    {
        get => GetCacheOutput();
        set => SetCacheOutput(value);
    }

    /// <summary>
    /// Gets or sets the precision to use for intermediate buffers when drawing this effect.
    /// </summary>
    /// <remarks>If <see langword="null"/>, the default precision for effects will be used.</remarks>
    public CanvasBufferPrecision? BufferPrecision
    {
        get => GetBufferPrecision();
        set => SetBufferPrecision(value);
    }

    /// <summary>
    /// Gets the <see cref="SourceCollection"/> object associated with the current instance.
    /// </summary>
    public SourceCollection Sources { get; }

    /// <summary>
    /// Gets the <see cref="ResourceTextureManagerCollection"/> object associated with the current instance.
    /// </summary>
    public ResourceTextureManagerCollection ResourceTextureManagers { get; }
}