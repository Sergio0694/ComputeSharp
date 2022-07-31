using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Interop;

#pragma warning disable CS0618

/// <inheritdoc/>
partial class NativeObject
{
    /// <summary>
    /// Adds an untracked reference to the underlying object. This needs to be released with <see cref="DangerousRelease"/>.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown if the tracked object has been disposed.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void DangerousAddRef()
    {
        _ = GetReferenceTrackingLease();
    }

    /// <summary>
    /// Decrements the reference count for the underlying object. A call to this API has to match a previous <see cref="DangerousAddRef"/> call.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void DangerousRelease()
    {
        ReturnReferenceTrackingLease();
    }

    /// <summary>
    /// Gets a <see cref="Lease"/> value to extend the lifetime of the tracked object.
    /// </summary>
    /// <returns>A <see cref="Lease"/> object that can extend the lifetime of the tracked object.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the tracked object has been disposed.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Lease GetReferenceTrackingLease()
    {
        Lease lease = TryGetReferenceTrackingLease(out bool leaseTaken);

        if (!leaseTaken)
        {
            // We can't use ThrowHelper here as we only want to invoke ToString if we
            // are about to throw an exception. The JIT will recognize this pattern
            // as this method has a single basic block that always throws an exception.
            static void Throw(object trackedObject) => throw new ObjectDisposedException(trackedObject.ToString());

            Throw(this);
        }

        return lease;
    }

    /// <summary>
    /// Tries to get a <see cref="Lease"/> value to extend the lifetime of the tracked object.
    /// </summary>
    /// <param name="leaseTaken">Whether or not the returned <see cref="Lease"/> value is enabled.</param>
    /// <returns>A <see cref="Lease"/> object that can extend the lifetime of the tracked object.</returns>
    internal Lease TryGetReferenceTrackingLease(out bool leaseTaken)
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

        NativeObject? nativeObject;

        if (success)
        {
            leaseTaken = true;
            nativeObject = this;
        }
        else
        {
            leaseTaken = false;
            nativeObject = null;
        }

        return new(nativeObject);
    }

    /// <inheritdoc/>
    private void RequestDispose()
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
            OnDispose();
        }
    }

    /// <summary>
    /// Returns a given lease (ie. decrements the ref count for the tracked object).
    /// </summary>
    private void ReturnReferenceTrackingLease()
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
            OnDispose();
        }
    }

    /// <summary>
    /// A reference tracking lease to extend the lifetime of a given <see cref="NativeObject"/> instance while in a given scope.
    /// </summary>
    internal struct Lease : IDisposable
    {
        /// <summary>
        /// The <see cref="NativeObject"/> instance being wrapped, if any.
        /// </summary>
        private NativeObject? nativeObject;

        /// <summary>
        /// Creates a new <see cref="Lease"/> instance with the specified parameters.
        /// </summary>
        /// <param name="nativeObject">The <see cref="NativeObject"/> instance being wrapped, if any.</param>
        [Obsolete("This constructor is only meant to be called from within NativeObject.")]
        public Lease(NativeObject? nativeObject)
        {
            this.nativeObject = nativeObject;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            NativeObject? nativeObject = this.nativeObject;

            this.nativeObject = null;

            if (nativeObject is not null)
            {
                nativeObject.ReturnReferenceTrackingLease();
            }
        }
    }
}
