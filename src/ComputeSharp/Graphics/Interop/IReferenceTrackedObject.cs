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
    /// <remarks>
    /// This method <b>must never be called directly</b>. It is invoked automatically by <see cref="ReferenceTracker"/>
    /// when a lease is returned, there are no outstanding leases and dispose has been requested on the target object. This ensures
    /// that <see cref="DangerousOnDispose"/> will only be called once per object when there are no active callsites using it anymore.
    /// </remarks>
    void DangerousOnDispose();
}