using System;
using ComputeSharp.Interop;
using System.Runtime.CompilerServices;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
partial class PixelShaderEffect<T>
{
    /// <summary>
    /// Releases unmanaged resources and performs other cleanup operations.
    /// </summary>
    ~PixelShaderEffect()
    {
        this.referenceTracker.Dispose();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.referenceTracker.Dispose();

        GC.SuppressFinalize(this);
    }

    /// <inheritdoc cref="IReferenceTrackedObject.GetReferenceTracker"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref ReferenceTracker GetReferenceTracker()
    {
        return ref this.referenceTracker;
    }

    /// <inheritdoc/>
    ref ReferenceTracker IReferenceTrackedObject.GetReferenceTracker()
    {
        return ref this.referenceTracker;
    }

    /// <inheritdoc/>
    void IReferenceTrackedObject.DangerousOnDispose()
    {
        this.canvasDevice.Dispose();
        this.d2D1RealizationDevice.Dispose();
        this.d2D1Effect.Dispose();

        // Also release all native resources for the used sources.
        // The transform mapper and resource texture managers don't
        // need to be disposed, as those could be shared between effects.
        for (int i = 0; i < Sources.Count; i++)
        {
            Sources.Storage[i].Dispose();
        }
    }
}