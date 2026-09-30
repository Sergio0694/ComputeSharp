using System;
using System.Threading;

namespace ComputeSharp.Graphics.Commands;

// The callback may run before RegisterWaitForSingleObject returns its handle.
// Exactly one of the publisher/callback owns cleanup, after both have arrived.
internal struct FenceWaitPublication
{
    // 0: neither participant arrived; 1: handle published; 2: callback arrived;
    // 3: cleanup claimed. The first arrival must not access the context again
    // after returning false, because the other participant may then free it.
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
        // Interlocked publishes the preceding context writes as well as the state.
        // Only the second arrival can claim cleanup, once the handle is available
        // and the callback has observed fence completion.
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
        // With a one-shot registration, this callback is the only outstanding
        // callback. Nonblocking cancellation may report it as still running;
        // the blocking publisher path must instead confirm completion.
        return success || (insideCallback && error == 997); // ERROR_IO_PENDING: this callback is still executing.
    }
}
