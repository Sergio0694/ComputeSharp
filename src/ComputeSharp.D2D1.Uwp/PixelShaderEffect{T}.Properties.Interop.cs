using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Uwp.Buffers;
using ComputeSharp.D2D1.Uwp.Extensions;
using ComputeSharp.Interop;
using Microsoft.Graphics.Canvas;
using TerraFX.Interop.DirectX;
using Windows.Graphics.Effects;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
unsafe partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Gets the marshalled value for <see cref="CacheOutput"/>.
    /// </summary>
    /// <returns>The marshalled value for <see cref="CacheOutput"/>.</returns>
    private T GetConstantBuffer()
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            return this.d2D1Effect.Get() switch
            {
                not null => this.d2D1Effect.Get()->GetConstantBuffer<T>(),
                _ => this.value
            };
        }
    }

    /// <summary>
    /// Sets the marshalled value for <see cref="Value"/>.
    /// </summary>
    /// <param name="value">The value to set for <see cref="Value"/>.</param>
    private void SetConstantBuffer(in T value)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            // If the effect is realized, set the property on the underlying ID2D1Effect object
            if (this.d2D1Effect.Get() is not null)
            {
                D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(this.d2D1Effect.Get(), in value);
            }
            else
            {
                // Otherwise, just store the value locally
                this.value = value;
            }
        }
    }

    /// <summary>
    /// Gets the marshalled value for <see cref="TransformMapper"/>.
    /// </summary>
    /// <returns>The marshalled value for <see cref="TransformMapper"/>.</returns>
    private D2D1TransformMapper<T>? GetTransformMapper()
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            return this.d2D1Effect.Get() switch
            {
                not null => this.d2D1Effect.Get()->GetTransformMapper<T>(),
                _ => this.transformMapper
            };
        }
    }

    /// <summary>
    /// Sets the marshalled value for <see cref="TransformMapper"/>.
    /// </summary>
    /// <param name="value">The value to set for <see cref="TransformMapper"/>.</param>
    private void SetTransformMapper(D2D1TransformMapper<T> value)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            if (this.d2D1Effect.Get() is not null)
            {
                D2D1PixelShaderEffect.SetTransformMapperForD2D1Effect(this.d2D1Effect.Get(), value);
            }
            else
            {
                this.transformMapper = value;
            }
        }
    }

    /// <summary>
    /// Gets the marshalled value for <see cref="Sources"/>.
    /// </summary>
    /// <param name="index">The index of the <see cref="Sources"/> source to get or set.</param>
    /// <returns>The marshalled value for <see cref="Sources"/>.</returns>
    private IGraphicsEffectSource? GetSource(int index)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            ref SourceReference source = ref Sources.Storage[index];

            return this.d2D1Effect.Get() switch
            {
                not null => this.d2D1Effect.Get()->GetSource(this.canvasDevice.Get(), ref source, index),
                _ => source.GetWrapper()
            };
        }
    }

    /// <summary>
    /// Sets the marshalled value for <see cref="Sources"/>.
    /// </summary>
    /// <param name="value">The value to set for <see cref="Sources"/>.</param>
    /// <param name="index">The index of the <see cref="IGraphicsEffectSource"/> source to get or set.</param>
    private void SetSource(IGraphicsEffectSource? value, int index)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            ref SourceReference source = ref Sources.Storage[index];

            if (this.d2D1Effect.Get() is not null)
            {
                const GetD2DImageFlags sourceFlags =
                    GetD2DImageFlags.MinimalRealization |
                    GetD2DImageFlags.AllowNullEffectInputs |
                    GetD2DImageFlags.UnrealizeOnFailure;

                // Try to set the source, and unrealize if the operation failed
                if (!this.d2D1Effect.Get()->TrySetSource(
                    canvasDevice: this.canvasDevice.Get(),
                    deviceContext: null,
                    flags: sourceFlags,
                    targetDpi: 0,
                    value: value,
                    source: ref source,
                    index: index))
                {
                    Unrealize();
                }
            }
            else
            {
                source.SetWrapper(value);
            }
        }
    }

    /// <summary>
    /// Gets the marshalled value for <see cref="ResourceTextureManagers"/>.
    /// </summary>
    /// <param name="index">The index of the <see cref="D2D1ResourceTextureManager"/> source to get or set.</param>
    /// <returns>The marshalled value for <see cref="ResourceTextureManagers"/>.</returns>
    private D2D1ResourceTextureManager? GetResourceTextureManager(int index)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            D2D1ResourceTextureManager? resourceTextureManager = ResourceTextureManagers.Storage[index];

            return this.d2D1Effect.Get() switch
            {
                not null => this.d2D1Effect.Get()->GetResourceTextureManager(resourceTextureManager, index),
                _ => resourceTextureManager
            };
        }
    }

    /// <summary>
    /// Sets the marshalled value for <see cref="ResourceTextureManagers"/>.
    /// </summary>
    /// <param name="value">The value to set for <see cref="ResourceTextureManagers"/>.</param>
    /// <param name="index">The index of the <see cref="D2D1ResourceTextureManager"/> source to get or set.</param>
    private void SetResourceTextureManager(D2D1ResourceTextureManager value, int index)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            if (this.d2D1Effect.Get() is not null)
            {
                D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(this.d2D1Effect.Get(), value, index);
            }
            else
            {
                ResourceTextureManagers.Storage[index] = value;
            }
        }
    }

    /// <summary>
    /// Gets the marshalled value for <see cref="CacheOutput"/>.
    /// </summary>
    /// <returns>The marshalled value for <see cref="CacheOutput"/>.</returns>
    private bool GetCacheOutput()
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            return this.d2D1Effect.Get() switch
            {
                not null => this.d2D1Effect.Get()->GetCachedProperty(),
                _ => this.cacheOutput
            };
        }
    }

    /// <summary>
    /// Sets the marshalled value for <see cref="CacheOutput"/>.
    /// </summary>
    /// <param name="value">The value to set for <see cref="CacheOutput"/>.</param>
    private void SetCacheOutput(bool value)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            if (this.d2D1Effect.Get() is not null)
            {
                this.d2D1Effect.Get()->SetCachedProperty(value);
            }
            else
            {
                this.cacheOutput = value;
            }
        }
    }

    /// <summary>
    /// Gets the marshalled value for <see cref="BufferPrecision"/>.
    /// </summary>
    /// <returns>The marshalled value for <see cref="BufferPrecision"/>.</returns>
    private CanvasBufferPrecision? GetBufferPrecision()
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        lock (this.lockObject)
        {
            D2D1_BUFFER_PRECISION d2D1BufferPrecision = this.d2D1Effect.Get() switch
            {
                not null => this.d2D1Effect.Get()->GetPrecisionProperty(),
                _ => this.d2D1BufferPrecision
            };

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

    /// <summary>
    /// Sets the marshalled value for <see cref="BufferPrecision"/>.
    /// </summary>
    /// <param name="value">The value to set for <see cref="BufferPrecision"/>.</param>
    private void SetBufferPrecision(CanvasBufferPrecision? value)
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