using System;
using ComputeSharp.D2D1.Interop.Effects;
using Microsoft.Graphics.Canvas;
using Windows.Graphics.Effects;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Configures the <see cref="ID2D1TransformMapperFactory{T}"/> to use for effects of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="d2D1DrawTransformMapperFactory">The factory of <see cref="ID2D1TransformMapper{T}"/> instances to use for each created effect.</param>
    /// <exception cref="InvalidOperationException">Thrown if initialization is attempted with a mismatched transform factory.</exception>
    /// <remarks>
    /// <para>Initialization can only be done once, and is shared across all effects of type <typeparamref name="T"/>.</para>
    /// <para>Initializing an effect is not necessary before using it: if no transform has been set, the default one will be used.</para>
    /// </remarks>
    public static void ConfigureD2D1TransformMapperFactory(ID2D1TransformMapperFactory<T>? d2D1DrawTransformMapperFactory)
    {
        PixelShaderEffect.For<T>.Initialize(d2D1DrawTransformMapperFactory);
    }

    /// <summary>
    /// Gets or sets the current <typeparamref name="T"/> value in use.
    /// </summary>
    public T Value
    {
        get => GetConstantBuffer();
        set => SetConstantBuffer(in value);
    }

    /// <summary>
    /// Gets the <see cref="SourceCollection"/> object associated with the current instance.
    /// </summary>
    public SourceCollection Sources { get; } = new();

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
    /// Represents the collection of <see cref="IGraphicsEffectSource"/> sources in a <see cref="PixelShaderEffect{T}"/> instance.
    /// </summary>
    public sealed class SourceCollection
    {
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