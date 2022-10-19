using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ComputeSharp.Interop;

#pragma warning disable CS0618

/// <summary>
/// An object acting as reference tracked for a given managed object.
/// </summary>
internal struct ReferenceTracker : IDisposable
{
    /// <summary>
    /// The target object to track.
    /// </summary>
    private readonly IReferenceTrackedObject trackedObject;

    /// <summary>
    /// A mask that indicates the state of the current object.
    /// The mask uses the following schema:
    /// <list type="bullet">
    ///     <item>[0, 30]: the number of existing reference tracking leases.</item>
    ///     <item>[31]: whether or not <see cref="IDisposable.Dispose"/> has been called.</item>
    /// </list>
    /// </summary>
    private volatile int referenceTrackingMask;

    /// <summary>
    /// Creates a new <see cref="ReferenceTracker"/> instance with the specified paramters.
    /// </summary>
    /// <param name="trackedObject">The input tracked object to wrap.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReferenceTracker(IReferenceTrackedObject trackedObject)
    {
        this.trackedObject = trackedObject;
        this.referenceTrackingMask = 0;
    }

    /// <summary>
    /// Adds an untracked reference to the underlying object. This needs to be released with <see cref="DangerousRelease"/>.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown if the tracked object has been disposed.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void DangerousAddRef()
    {
        _ = GetLease();
    }

    /// <summary>
    /// Decrements the reference count for the underlying object. A call to this API has to match a previous <see cref="DangerousAddRef"/> call.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void DangerousRelease()
    {
        ReturnLease();
    }

    /// <summary>
    /// Gets a <see cref="Lease"/> value to extend the lifetime of the tracked object.
    /// </summary>
    /// <returns>A <see cref="Lease"/> object that can extend the lifetime of the tracked object.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the tracked object has been disposed.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Lease GetLease()
    {
        Lease lease = TryGetLease(out bool leaseTaken);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Lease TryGetLease(out bool leaseTaken)
    {
        bool success = true;

        int currentValue;
        int originalValue;

        // To get a reference tracking lease, the procedure is as follows:
        //   - If the object has been disposed (ie. if Disposed() has been called),
        //     even if the object hasn't actually released the unmanaged resources
        //     yet, then getting a tracking lease will fail and have no effect.
        //   - If the object hasn't been disposed, the reference count is incremented.
        // This can be done without taking a look, as follows:
        //   - Do an interlocked read to get the current reference tracking mask.
        //   - If the object has been disposed, the 32nd bit will be set. Due to the
        //     mask being a signed integer in two-complement, we can just compare and
        //     check whether the mask is lower than 0. If that is the case, just bail.
        //   - Do an interlocked compare exchange incrementing the reference count by 1.
        //     If the original value is the same as the current one, it means no other
        //     thread performed a concurrent update between our read and write, so we can
        //     stop. Otherwise, just loop until a compare exchange completes successfully.
        // The assumption is contention will be extremely rare, given that taking and
        // returning a lease is incredibly fast compared to the time other operations need.
        do
        {
            currentValue = this.referenceTrackingMask;

            if (currentValue < 0)
            {
                success = false;

                break;
            }

            originalValue = Interlocked.CompareExchange(
                location1: ref this.referenceTrackingMask,
                value: currentValue + 1,
                comparand: currentValue);
        }
        while (currentValue != originalValue);

        IReferenceTrackedObject? trackedObject;

        // If the reference count was incremented, return a valid lease. Otherwise,
        // return one just wrapping a null instance, which will no-op when disposed.
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
        bool isDisposed = false;

        int currentValue;
        int originalValue;

        // To request a dispose operation, the procedure is as follows:
        //   - If the dispose bit has already been set, just do nothing. This means
        //     that another thread was the first to call Dispose(). In that case, the
        //     actual releasing of unmanaged resources will be performed either by that
        //     thread if there are no active leases, or by the last returned lease.
        //   - Do an interlocked compare exchange setting the dispose bit (32nd bit).
        //     Like above, if the original value doesn't match the current one, it means
        //     that another thread raced against this one, so the value is invalid, and
        //     another loop is executed. If the value matches, the loop just ends.
        // After this atomic update, we can then check whether (1) this was the first
        // thread to call Dispose() (ie. the dispose flag wasn't previously set and it
        // was set successfully by this call), and (2) there are no other active leases.
        // If both checks are true, the object is effectively dead and we can safely release
        // unmanaged resources. All other leases will just fail to be taken after this anyway.
        do
        {
            currentValue = this.referenceTrackingMask;

            if (currentValue < 0)
            {
                isDisposed = true;

                break;
            }

            originalValue = Interlocked.CompareExchange(
                location1: ref this.referenceTrackingMask,
                value: currentValue | (1 << 31),
                comparand: currentValue);
        }
        while (currentValue != originalValue);

        // Only release resources if this is the first time Dipose() has been called, and
        // there are no outstanding leases. If there is one, don't do anything now. The
        // tracked object will just be released once the last active lease is returned.
        if (!isDisposed && currentValue == 0)
        {
            this.trackedObject.DangerousRelease();
        }
    }

    /// <summary>
    /// Returns a given lease (ie. decrements the ref count for the tracked object).
    /// </summary>
    private void ReturnLease()
    {
        // To return a lease, we can simply do an interlocked decrement on the reference tracking
        // mask. Each lease is guaranteed to only be disposed once (the contract states to only
        // ever use it in a using statement), and a valid lease existing implies that the reference
        // counting mask had previously been incremented by 1. There is also no need to check for
        // disposal, becausse returning a lease on a disposed object is perfectly valid (given that
        // the actual disposal is deferred until all active leases are returned).
        int currentValue = Interlocked.Decrement(ref this.referenceTrackingMask);

        // If Dipose() has been called and this was the last lease, release the tracked object.
        // This is the case if the dispose bit is set (the 32nd one), and no other bit is set.
        if (currentValue == 1 << 31)
        {
            this.trackedObject.DangerousRelease();
        }
    }

    /// <summary>
    /// A reference tracking lease to extend the lifetime of a given <see cref="ReferenceTrackedObject"/> instance while in a given scope.
    /// </summary>
    /// <remarks>
    /// This type must always be used in a <see langword="using"/> statement and disposed properly. Not doing
    /// so is undefined behavior and may result in memory leaks and inability to correctly restore lost devices.
    /// </remarks>
    public struct Lease : IDisposable
    {
        /// <summary>
        /// The <see cref="ReferenceTrackedObject"/> instance being wrapped, if any.
        /// </summary>
        private IReferenceTrackedObject? trackedObject;

        /// <summary>
        /// Creates a new <see cref="Lease"/> instance with the specified parameters.
        /// </summary>
        /// <param name="trackedObject">The <see cref="IReferenceTrackedObject"/> instance being wrapped, if any.</param>
        [Obsolete("This constructor is only meant to be called from within ReferenceTracker.")]
        public Lease(IReferenceTrackedObject? trackedObject)
        {
            this.trackedObject = trackedObject;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            IReferenceTrackedObject? trackedObject = this.trackedObject;

            this.trackedObject = null;

            trackedObject?.GetReferenceTracker().ReturnLease();
        }
    }
}