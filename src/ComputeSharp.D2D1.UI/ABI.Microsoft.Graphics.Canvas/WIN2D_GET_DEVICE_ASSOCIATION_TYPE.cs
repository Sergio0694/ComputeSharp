using System;

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// Options for fine-tuning the behavior of <see cref="ICanvasImageInterop.GetDevice"/>.
/// </summary>
[Flags]
internal enum WIN2D_GET_DEVICE_ASSOCIATION_TYPE : uint
{
    /// <summary>
    /// No device info is available, callers will handle the returned device with their own logic.
    /// </summary>
    WIN2D_GET_DEVICE_ASSOCIATION_TYPE_UNSPECIFIED = 0,

    /// <summary>
    /// The returned device is the one the image is currently realized on, if any.
    /// </summary>
    WIN2D_GET_DEVICE_ASSOCIATION_TYPE_REALIZATION_DEVICE = 1,

    /// <summary>
    /// The returned device is the one that created the image resource, and cannot change.
    /// </summary>
    WIN2D_GET_DEVICE_ASSOCIATION_TYPE_CREATION_DEVICE = 2,

    /// <summary>
    /// Ignored.
    /// </summary>
    WIN2D_GET_DEVICE_ASSOCIATION_TYPE_FORCE_DWORD = 0xFFFFFFFF
}