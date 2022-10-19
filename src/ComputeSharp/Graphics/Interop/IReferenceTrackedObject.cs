using System;

namespace ComputeSharp.Interop;

/// <summary>
/// An interface for an object being tracked by an owning <see cref="ReferenceTracker"/> object.
/// </summary>
internal interface IReferenceTrackedObject : IDisposable
{
    /// <summary>
    /// Gets a reference to the owning <see cref="ReferenceTracker"/> for the object.
    /// </summary>
    /// <returns>A reference to the owning <see cref="ReferenceTracker"/> for the object.</returns>
    ref ReferenceTracker GetReferenceTracker();

    /// <summary>
    /// Releases all resources (including unmanaged ones) for the tracked object.
    /// </summary>
    /// <remarks>This method is guaranteed to only ever be called once for a given tracked object.</remarks>
    void DangerousRelease();
}