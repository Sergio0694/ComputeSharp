using System.Runtime.InteropServices;
using System;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using TerraFX.Interop;

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// Internal Win2D interface for all images and effects that can be drawn.
/// </summary>
[ComImport]
[Guid("2F434224-053C-4978-87C4-CFAAFA2F4FAC")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal unsafe interface ICanvasImageInternal
{
    /// <summary>
    /// Gets an <see cref="ID2D1Image"/> from an <see cref="ICanvasImageInternal"/> instance.
    /// </summary>
    /// <param name="retBuf">A pointer to the return buffer to write the result into (needed for ABI matching).</param>
    /// <param name="device">The input canvas device (as pointer to internal interface).</param>
    /// <param name="deviceContext">
    /// The device context in use. This value is is optional (but recommended), except when the
    /// <see cref="GetImageFlags.ReadDpiFromDeviceContext"/> flag is specified. This is because
    /// not all callers of <see cref="GetD2DImage"/> have easy access to a context. It is always
    /// possible to get a resource creation context from the device, but the context is only
    /// actually necessary if a new effect realization needs to be created, so it is more efficient
    /// to have the implementation do this lookup only if/when it turns out to be needed.
    /// </param>
    /// <param name="flags">The flags to use to get the image.</param>
    /// <param name="targetDpi">
    /// The DPI of the target device context. This is used to determine when a <c>D2D1DpiCompensation</c>
    /// effect needs to be inserted. Behavior of this parameter can be overridden by the flag values
    /// <see cref="GetImageFlags.ReadDpiFromDeviceContext"/>, <see cref="GetImageFlags.AlwaysInsertDpiCompensation"/>
    /// or <see cref="GetImageFlags.NeverInsertDpiCompensation"/>
    /// </param>
    /// <param name="realizeDpi">
    /// The DPI of a source bitmap, or zero if the image does not have a fixed DPI. A <c>D2D1DpiCompensation</c> effect
    /// will be inserted if <paramref name="targetDpi"/> and <paramref name="realizeDpi"/> are different (flags permitting).
    /// </param>
    /// <returns>A pointer to the resulting image (this value is the same as <paramref name="retBuf"/>).</returns>
    [PreserveSig]
    ID2D1Image** GetD2DImage(
        ID2D1Image** retBuf,
        [NativeTypeName("ICanvasDevice*")] IUnknown* device,
        ID2D1DeviceContext* deviceContext,
        GetImageFlags flags,
        float targetDpi,
        float* realizeDpi);
}