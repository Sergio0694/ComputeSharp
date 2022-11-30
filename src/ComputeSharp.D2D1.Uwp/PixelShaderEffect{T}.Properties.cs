using System;
using System.Diagnostics.CodeAnalysis;
using ComputeSharp.D2D1.Interop;
using Microsoft.Graphics.Canvas;
using Windows.Graphics.Effects;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Gets or sets the current <typeparamref name="T"/> value in use.
    /// </summary>
    public T Value
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

    /// <summary>
    /// Represents the collection of <see cref="IGraphicsEffectSource"/> sources in a <see cref="PixelShaderEffect{T}"/> instance.
    /// </summary>
    public sealed class SourceCollection
    {
        /// <summary>
        /// Creates a new <see cref="SourceCollection"/> instance with the specified parameters.
        /// </summary>
        /// <param name="owner">The owning <see cref="PixelShaderEffect{T}"/> instance.</param>
        internal SourceCollection(PixelShaderEffect<T> owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// The owning <see cref="PixelShaderEffect{T}"/> instance.
        /// </summary>
        internal PixelShaderEffect<T> Owner { get; }

        /// <summary>
        /// Gets or sets the <see cref="IGraphicsEffectSource"/> source at a specified index.
        /// </summary>
        /// <param name="index">The index of the <see cref="IGraphicsEffectSource"/> source to get or set.</param>
        /// <returns>The <see cref="IGraphicsEffectSource"/> source at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is not a valid index for the current effect.</exception>
        public IGraphicsEffectSource this[int index]
        {
            get => null!;
            set { }
        }
    }
}