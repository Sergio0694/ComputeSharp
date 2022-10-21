using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Interop;

/// <summary>
/// Base class for a <see cref="IDisposable"/> class (only to be used for internal types, as it can't leak in the public API surface).
/// </summary>
internal abstract partial class ReferenceTrackedObject : IReferenceTrackedObject
{
    /// <summary>
    /// The <see cref="ReferenceTracker"/> value for the current instance.
    /// </summary>
    private ReferenceTracker referenceTracker;

    /// <summary>
    /// Creates a new <see cref="ReferenceTrackedObject"/> instance.
    /// </summary>
    public ReferenceTrackedObject()
    {
        this.referenceTracker = new ReferenceTracker(this);
    }

    /// <summary>
    /// Releases unmanaged resources and performs other cleanup operations.
    /// </summary>
    ~ReferenceTrackedObject()
    {
        this.referenceTracker.Dispose();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.referenceTracker.Dispose();

        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref ReferenceTracker GetReferenceTracker()
    {
        return ref this.referenceTracker;
    }

    /// <inheritdoc cref="IReferenceTrackedObject.DangerousOnDispose"/>
    protected abstract void DangerousOnDispose();

    /// <inheritdoc/>
    void IReferenceTrackedObject.DangerousOnDispose()
    {
        DangerousOnDispose();
    }
}