using System;
using System.Numerics;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Interop.Effects;
using ComputeSharp.D2D1.Uwp.Extensions;
using ComputeSharp.Interop;
using Microsoft.Graphics.Canvas;
using Windows.Foundation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Uwp;

/// <summary>
/// A custom <see cref="ICanvasImage"/> implementation powered by a supplied shader type.
/// </summary>
/// <typeparam name="T">The type of shader to use to render frames.</typeparam>
public sealed partial class PixelShaderEffect<T> : IReferenceTrackedObject, ICanvasImage, ICanvasImageInterop
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// The <see cref="ReferenceTracker"/> value for the current instance.
    /// </summary>
    private ReferenceTracker referenceTracker;

    /// <summary>
    /// Lock object used to synchronize calls to <see cref="ICanvasImageInterop"/> APIs.
    /// </summary>
    private readonly object lockObject = new();

    /// <summary>
    /// Flag to track whether a given call is recursively invoked by <see cref="ICanvasImageInterop.GetD2DImage"/>, to avoid graph cycles.
    /// </summary>
    private volatile int isInsideGetD2DImage;

    /// <summary>
    /// The current device the effect is realized on, if one is available (this is the underlying COM object for <see cref="CanvasDevice"/>).
    /// </summary>
    private ComPtr<ICanvasDevice> canvasDevice;

    /// <summary>
    /// The underlying D2D1 device the effect is realized on, if one is available (this is the D2D device backing <see cref="canvasDevice"/>).
    /// </summary>
    private ComPtr<ID2D1Device1> d2D1RealizationDevice;

    /// <summary>
    /// The current realized <see cref="ID2D1Effect"/>, if one is available.
    /// </summary>
    private ComPtr<ID2D1Effect> d2D1Effect;

    /// <summary>
    /// The backing field for <see cref="CacheOutput"/>.
    /// </summary>
    private bool cacheOutput;

    /// <summary>
    /// The backing field for <see cref="BufferPrecision"/>.
    /// </summary>
    private D2D1_BUFFER_PRECISION d2D1BufferPrecision;

    /// <summary>
    /// The current <typeparamref name="T"/> value in use.
    /// </summary>
    public T Value;

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
    /// Gets or sets a value indicating whether to enable caching the output from drawing this effect.
    /// </summary>
    public unsafe bool CacheOutput
    {
        get
        {
            using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

            lock (this.lockObject)
            {
                // If the effect is realized, get the latest value from it
                if (this.d2D1Effect.Get() is not null)
                {
                    this.cacheOutput = this.d2D1Effect.Get()->GetCachedProperty();
                }

                return this.cacheOutput;
            }
        }
        set
        {
            using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

            lock (this.lockObject)
            {
                this.cacheOutput = value;

                // If the effect is realized, set the property on the underlying ID2D1Effect object too
                if (this.d2D1Effect.Get() is not null)
                {
                    this.d2D1Effect.Get()->SetCachedProperty(value);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the precision to use for intermediate buffers when drawing this effect.
    /// </summary>
    /// <remarks>If <see langword="null"/>, the default precision for effects will be used.</remarks>
    public unsafe CanvasBufferPrecision? BufferPrecision
    {
        get
        {
            using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

            lock (this.lockObject)
            {
                // If the effect is realized, get the latest value from it
                if (this.d2D1Effect.Get() is not null)
                {
                    this.d2D1BufferPrecision = this.d2D1Effect.Get()->GetPrecisionProperty();
                }

                // Map from D2D1_BUFFER_PRECISION to CanvasBufferPrecision, and return null for D2D1_BUFFER_PRECISION_UNKNOWN
                return this.d2D1BufferPrecision switch
                {
                    D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_8BPC_UNORM => CanvasBufferPrecision.Precision8UIntNormalized,
                    D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_8BPC_UNORM_SRGB => CanvasBufferPrecision.Precision8UIntNormalizedSrgb,
                    D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_16BPC_UNORM => CanvasBufferPrecision.Precision16UIntNormalized,
                    D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_16BPC_FLOAT => CanvasBufferPrecision.Precision16Float,
                    D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_32BPC_FLOAT => CanvasBufferPrecision.Precision32Float,
                    _ => null
                };
            }
        }
        set
        {
            using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

            lock (this.lockObject)
            {
                // Map back from CanvasBufferPrecision? to D2D1_BUFFER_PRECISION (null results in D2D1_BUFFER_PRECISION_UNKNOWN)
                this.d2D1BufferPrecision = value switch
                {
                    CanvasBufferPrecision.Precision8UIntNormalized => D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_8BPC_UNORM,
                    CanvasBufferPrecision.Precision8UIntNormalizedSrgb => D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_8BPC_UNORM_SRGB,
                    CanvasBufferPrecision.Precision16UIntNormalized => D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_16BPC_UNORM,
                    CanvasBufferPrecision.Precision16Float => D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_16BPC_FLOAT,
                    CanvasBufferPrecision.Precision32Float => D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_32BPC_FLOAT,
                    _ => D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_UNKNOWN
                };

                // If the effect is realized, set the property on the underlying ID2D1Effect object too
                if (this.d2D1Effect.Get() is not null)
                {
                    this.d2D1Effect.Get()->SetPrecisionProperty(this.d2D1BufferPrecision);
                }
            }
        }
    }

    /// <inheritdoc/>
    Rect ICanvasImage.GetBounds(ICanvasResourceCreator resourceCreator)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc/>
    Rect ICanvasImage.GetBounds(ICanvasResourceCreator resourceCreator, Matrix3x2 transform)
    {
        throw new NotSupportedException();
    }
}