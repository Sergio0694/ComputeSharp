using System;
using System.Threading;

namespace ComputeSharp.Graphics.Commands;

// The callback may run before RegisterWaitForSingleObject returns its handle.
// Exactly one of the publisher/callback owns cleanup, after both have arrived.
internal struct FenceWaitPublication
{
    private int state;

    public bool OnRegistered()
    {
        return Arrive(1, 2);
    }

    public bool OnSignaled()
    {
        return Arrive(2, 1);
    }

    private bool Arrive(int own, int other)
    {
        int previous = Interlocked.CompareExchange(ref this.state, own, 0);
        if (previous == 0)
        {
            return false;
        }

        if (previous == other && Interlocked.CompareExchange(ref this.state, 3, other) == other)
        {
            return true;
        }

        throw new InvalidOperationException("Fence wait completion/publication occurred more than once.");
    }

    public static bool UnregisterAccepted(bool success, int error, bool insideCallback)
    {
        return success || (insideCallback && error == 997); // ERROR_IO_PENDING: this callback is still executing.
    }
}