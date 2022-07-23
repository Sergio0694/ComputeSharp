using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Interop;

/// <summary>
/// An object acting as reference tracked for a given managed object.
/// </summary>
internal struct ReferenceTracker : IDisposable
{
    /// <summary>
    /// The target object to track.
    /// </summary>
    private readonly ITrackedObject trackedObject;

    /// <summary>
    /// An object to use to synchronize the reference tracking operations.
    /// </summary>
    private readonly object lockObject;

    /// <summary>
    /// The number of existing reference tracking leases for <see cref="trackedObject"/>.
    /// </summary>
    private int leasesCount;

    /// <summary>
    /// Whether or not <see cref="Dispose"/> has been called on <see cref="trackedObject"/>.
    /// </summary>
    private bool isDisposeRequested;

    /// <summary>
    /// Creates a new <see cref="ReferenceTracker"/> instance with the specified paramters.
    /// </summary>
    /// <param name="trackedObject">The input tracked object to wrap.</param>
    public ReferenceTracker(ITrackedObject trackedObject)
    {
        this.trackedObject = trackedObject;
        this.lockObject = new object();
        this.leasesCount = 0;
        this.isDisposeRequested = false;
    }

    /// <summary>
    /// Gets a <see cref="Lease"/> value to extend the lifetime of the tracked object.
    /// </summary>
    /// <returns>A <see cref="Lease"/> object that can extend the lifetime of the tracked object.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the tracked object has been disposed.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Lease GetLease()
    {
        Lease lease = TryGetLease(out bool leaseTaken);

        if (!leaseTaken)
        {
            // We can't use ThrowHelper here as we only want to invoke ToString if we
            // are about to throw an exception. The JIT will recognize this pattern
            // as this method has a single basic block that always throws an exception.
            static void Throw(object trackedObject) => throw new ObjectDisposedException(trackedObject.ToString());

            Throw(this.trackedObject);
        }

        return lease;
    }

    /// <summary>
    /// Tries to get a <see cref="Lease"/> value to extend the lifetime of the tracked object.
    /// </summary>
    /// <param name="leaseTaken">Whether or not the returned <see cref="Lease"/> value is enabled.</param>
    /// <returns>A <see cref="Lease"/> object that can extend the lifetime of the tracked object.</returns>
    public Lease TryGetLease(out bool leaseTaken)
    {
        bool success = true;

        lock (this.lockObject)
        {
            if (this.isDisposeRequested)
            {
                success = false;
            }
            else
            {
                this.leasesCount++;
            }
        }

        ITrackedObject? trackedObject;

        if (success)
        {
            leaseTaken = true;
            trackedObject = this.trackedObject;
        }
        else
        {
            leaseTaken = false;
            trackedObject = null;
        }

        return new(trackedObject);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        bool shouldRelease = false;

        lock (this.lockObject)
        {
            if (!this.isDisposeRequested)
            {
                this.isDisposeRequested = true;

                // Only release resources if this is the first time Dipose() has been called, and
                // there are no outstanding leases. If there is one, don't do anything now. The
                // tracked object will just be released once the last active lease is returned.
                if (this.leasesCount == 0)
                {
                    shouldRelease = true;
                }
            }
        }

        if (shouldRelease)
        {
            this.trackedObject.DangerousRelease();
        }
    }

    /// <summary>
    /// Returns a given lease (ie. decrements the ref count for the tracked object).
    /// </summary>
    private void ReturnLease()
    {
        bool shouldRelease = false;

        lock (this.lockObject)
        {
            this.leasesCount--;

            // If Dipose() has been called and this was the last lease, release the tracked object
            if (this.isDisposeRequested && this.leasesCount == 0)
            {
                shouldRelease = true;
            }
        }

        if (shouldRelease)
        {
            this.trackedObject.DangerousRelease();
        }
    }

    /// <summary>
    /// A reference tracking lease to extend the lifetime of a given tracked object while in a given scope.
    /// </summary>
    public struct Lease
    {
        /// <summary>
        /// The tracked object being wrapped, if any.
        /// </summary>
        private ITrackedObject? trackedObject;

        /// <summary>
        /// Creates a new <see cref="Lease"/> instance with the specified parameters.
        /// </summary>
        /// <param name="trackedObject">The input tracked object to wrap, if any.</param>
        public Lease(ITrackedObject? trackedObject)
        {
            this.trackedObject = trackedObject;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            ITrackedObject? trackedObject = this.trackedObject;

            this.trackedObject = null;

            if (trackedObject is not null)
            {
                trackedObject.GetReferenceTracker().ReturnLease();
            }
        }
    }

    /// <summary>
    /// An interface for an object being tracked by an owning <see cref="ReferenceTracker"/> object.
    /// </summary>
    public interface ITrackedObject
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
}
