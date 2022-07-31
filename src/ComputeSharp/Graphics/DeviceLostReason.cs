using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

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
    DeviceHung = DXGI.DXGI_ERROR_DEVICE_HUNG,

    /// <summary>
    /// The video card has been physically removed from the system, or a driver upgrade for
    /// the video card has occurred. The application should destroy and recreate the device.
    /// </summary>
    DeviceRemoved = DXGI.DXGI_ERROR_DEVICE_REMOVED,

    /// <summary>
    /// The device failed due to a badly formed command. This is a run-time issue.
    /// The application should destroy and recreate the device.
    /// </summary>
    DeviceReset = DXGI.DXGI_ERROR_DEVICE_RESET,

    /// <summary>
    /// The driver encountered a problem and was put into the device removed state.
    /// </summary>
    DriverInternalError = DXGI.DXGI_ERROR_DRIVER_INTERNAL_ERROR,

    /// <summary>
    /// The application provided invalid parameter data.
    /// This must be debugged and fixed before the application is released.
    /// </summary>
    InvalidCall = DXGI.DXGI_ERROR_INVALID_CALL,

    /// <summary>
    /// The device was removed after attempting an invalid operation (eg. trying to write to the wrong swapchain buffer).
    /// The application should destroy and recreate the device. This also likely indicates an application bug.
    /// </summary>
    AccessDenied = DXGI.DXGI_ERROR_ACCESS_DENIED,

    /// <summary>
    /// The device was lost for an unspecified reason.
    /// </summary>
    UnspecifiedError = E.E_FAIL
}
