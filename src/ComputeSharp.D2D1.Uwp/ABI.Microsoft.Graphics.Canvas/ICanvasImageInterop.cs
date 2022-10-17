using System.Runtime.InteropServices;
using System;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using TerraFX.Interop;

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// An interop Win2D interface for all images and effects that can be drawn.
/// </summary>
[ComImport]
[Guid("E042D1F7-F9AD-4479-A713-67627EA31863")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal unsafe interface ICanvasImageInterop
{
    /// <summary>
    /// Gets the device that the effect is currently realized on, if any.
    /// </summary>
    /// <param name="device">The resulting device, if available ((as a marshalled <see cref="global::Microsoft.Graphics.Canvas.CanvasDevice"/>)).</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [PreserveSig]
    [return: NativeTypeName("HRESULT")]
    int GetDevice([NativeTypeName("ICanvasDevice**")] IUnknown** device);

    /// <summary>
    /// Gets an <see cref="ID2D1Image"/> from an <see cref="ICanvasImageInterop"/> instance.
    /// </summary>
    /// <param name="device">The input canvas device (as a marshalled <see cref="global::Microsoft.Graphics.Canvas.CanvasDevice"/>).</param>
    /// <param name="deviceContext">
    /// The device context in use. This value is is optional (but recommended), except when the
    /// <see cref="CanvasImageGetD2DImageFlags.ReadDpiFromDeviceContext"/> flag is specified. This is because
    /// not all callers of <see cref="GetD2DImage"/> have easy access to a context. It is always
    /// possible to get a resource creation context from the device, but the context is only
    /// actually necessary if a new effect realization needs to be created, so it is more efficient
    /// to have the implementation do this lookup only if/when it turns out to be needed.
    /// </param>
    /// <param name="flags">The flags to use to get the image.</param>
    /// <param name="targetDpi">
    /// The DPI of the target device context. This is used to determine when a <c>D2D1DpiCompensation</c>
    /// effect needs to be inserted. Behavior of this parameter can be overridden by the flag values
    /// <see cref="CanvasImageGetD2DImageFlags.ReadDpiFromDeviceContext"/>, <see cref="CanvasImageGetD2DImageFlags.AlwaysInsertDpiCompensation"/>
    /// or <see cref="CanvasImageGetD2DImageFlags.NeverInsertDpiCompensation"/>
    /// </param>
    /// <param name="realizeDpi">
    /// The DPI of a source bitmap, or zero if the image does not have a fixed DPI. A <c>D2D1DpiCompensation</c> effect
    /// will be inserted if <paramref name="targetDpi"/> and <paramref name="realizeDpi"/> are different (flags permitting).
    /// </param>
    /// <param name="ppImage">The resulting <see cref="ID2D1Image"/> for the effect.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [PreserveSig]
    [return: NativeTypeName("HRESULT")]
    int GetD2DImage(
        [NativeTypeName("ICanvasDevice*")] IUnknown* device,
        ID2D1DeviceContext* deviceContext,
        CanvasImageGetD2DImageFlags flags,
        float targetDpi,
        float* realizeDpi,
        ID2D1Image** ppImage);
}