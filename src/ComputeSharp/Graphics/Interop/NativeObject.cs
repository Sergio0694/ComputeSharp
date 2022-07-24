using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Interop;

/// <summary>
/// Base class for a <see cref="IDisposable"/> class.
/// </summary>
public abstract class NativeObject : IDisposable, ReferenceTracker.ITrackedObject
{
    /// <summary>
    /// The owning <see cref="ReferenceTracker"/> object for the current instance.
    /// </summary>
    private ReferenceTracker referenceTracker;

    /// <summary>
    /// Creates a new <see cref="NativeObject"/> instance.
    /// </summary>
    private protected NativeObject()
    {
        this.referenceTracker = new ReferenceTracker(this);
    }

    /// <summary>
    /// Releases unmanaged resources and performs other cleanup operations.
    /// </summary>
    ~NativeObject()
    {
        this.referenceTracker.Dispose();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.referenceTracker.Dispose();

        GC.SuppressFinalize(this);
    }

    /// <inheritdoc cref="ReferenceTracker.ITrackedObject.GetReferenceTracker"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref ReferenceTracker GetReferenceTracker()
    {
        return ref this.referenceTracker;
    }

    /// <inheritdoc/>
    ref ReferenceTracker ReferenceTracker.ITrackedObject.GetReferenceTracker()
    {
        return ref this.referenceTracker;
    }

    /// <inheritdoc/>
    void ReferenceTracker.ITrackedObject.DangerousRelease()
    {
        OnDispose();
    }

    /// <inheritdoc cref="ReferenceTracker.ITrackedObject.DangerousRelease"/>
    private protected abstract void OnDispose();
}
