using System;

namespace ComputeSharp;

/// <summary>
/// The arguments for the <see cref="GraphicsDevice.DeviceLost"/> event.
/// </summary>
public sealed class DeviceLostEventArgs : EventArgs
{
    /// <summary>
    /// Creates a new <see cref="DeviceLostEventArgs"/> instance with the specified arguments.
    /// </summary>
    /// <param name="reason">The <see cref="DeviceLostReason"/> value for the event.</param>
    internal DeviceLostEventArgs(DeviceLostReason reason)
    {
        Reason = reason;
    }

    /// <summary>
    /// Gets the reason that caused the device to be lost.
    /// </summary>
    public DeviceLostReason Reason { get; }
}