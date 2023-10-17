using System;
using ComputeSharp.Interop;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.WinUI.Helpers;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.WinUI;

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
    unsafe void IReferenceTrackedObject.DangerousOnDispose()
    {
        // When the effect is disposed, we also unregister it from the resource manager cache. This
        // is analogous to what is done when manually unrealizing the effect (eg. when using a new
        // device. Without this call, the resource manager would keep registered effects alive for
        // the entire process duration, even if their original wrappers are gone (ie. collected).
        if (this.d2D1Effect.Get() is not null)
        {
            ResourceManager.UnregisterWrapper((IUnknown*)this.d2D1Effect.Get());
        }

        // Release the native D2D objects that were set during realization
        this.canvasDevice.Dispose();
        this.d2D1RealizationDevice.Dispose();
        this.d2D1Effect.Dispose();

        // Also release all native resources for the used sources. The transform mapper and
        // resource texture managers don't need to be disposed, as those could be shared
        // between effects. Note that SourceReference.Dispose() will only release the native
        // resources (ie. D2D images and effects), but it will not also dispose the managed
        // wrappers (ie. IGraphicsEffectSource objects), as those can be shared as well.
        for (int i = 0; i < Sources.Count; i++)
        {
            Sources.Storage[i].Dispose();
        }
    }
}