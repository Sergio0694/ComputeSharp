using ComputeSharp.D2D1.Uwp.Extensions;
using ComputeSharp.Interop;
using Microsoft.Graphics.Canvas;
using TerraFX.Interop.DirectX;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
unsafe partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Gets the marshalled value for <see cref="CacheOutput"/>.
    /// </summary>
    /// <returns>The marshalled value for <see cref="CacheOutput"/>.</returns>
    private bool GetCacheOutput()
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

    /// <summary>
    /// Sets the marshalled value for <see cref="CacheOutput"/>.
    /// </summary>
    /// <param name="value">The value to set for <see cref="CacheOutput"/>.</param>
    private void SetCacheOutput(bool value)
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

    /// <summary>
    /// Gets the marshalled value for <see cref="BufferPrecision"/>.
    /// </summary>
    /// <returns>The marshalled value for <see cref="BufferPrecision"/>.</returns>
    private CanvasBufferPrecision? GetBufferPrecision()
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