namespace ComputeSharp;

/// <summary>
/// Indicates a reason a <see cref="GraphicsDevice"/> instance was lost.
/// </summary>
public enum DeviceLostReason
{
    /// <summary>
    /// The application's device failed due to badly formed commands sent by the application.
    /// This is an design-time issue that should be investigated and fixed.
    /// </summary>
    DeviceHung = unchecked((int)0x887A0006),

    /// <summary>
    /// The video card has been physically removed from the system, or a driver upgrade for
    /// the video card has occurred. The application should destroy and recreate the device.
    /// </summary>
    DeviceRemoved = unchecked((int)0x887A0005),

    /// <summary>
    /// The device failed due to a badly formed command. This is a run-time issue.
    /// The application should destroy and recreate the device.
    /// </summary>
    DeviceReset = unchecked((int)0x887A0007),

    /// <summary>
    /// The driver encountered a problem and was put into the device removed state.
    /// </summary>
    DriverInternalError = unchecked((int)0x887A0020),

    /// <summary>
    /// The application provided invalid parameter data.
    /// This must be debugged and fixed before the application is released.
    /// </summary>
    InvalidCall = unchecked((int)0x887A0001),

    /// <summary>
    /// The device was lost for an unspecified reason.
    /// </summary>
    UnspecifiedError = unchecked((int)0x80004005)
}
