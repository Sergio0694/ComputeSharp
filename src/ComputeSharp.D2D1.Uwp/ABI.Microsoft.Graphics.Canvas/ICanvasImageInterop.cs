using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using TerraFX.Interop;

#pragma warning disable CS0649, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// An interop Win2D interface for all images and effects that can be drawn.
/// </summary>
[Guid("E042D1F7-F9AD-4479-A713-67627EA31863")]
[NativeTypeName("class ICanvasImageInterop : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe struct ICanvasImageInterop
{
    public void** lpVtbl;

    /// <summary>
    /// Gets the device that the effect is currently realized on, if any.
    /// </summary>
    /// <param name="device">The resulting device, if available ((as a marshalled <see cref="global::Microsoft.Graphics.Canvas.CanvasDevice"/>)).</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public HRESULT GetDevice(ICanvasDevice** device)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasImageInterop*, ICanvasDevice**, int>)this.lpVtbl[3])((ICanvasImageInterop*)Unsafe.AsPointer(ref this), device);
    }

    /// <summary>
    /// Gets an <see cref="ID2D1Image"/> from an <see cref="ICanvasImageInterop.Interface"/> instance.
    /// </summary>
    /// <param name="device">The input canvas device (as a marshalled <see cref="global::Microsoft.Graphics.Canvas.CanvasDevice"/>).</param>
    /// <param name="deviceContext">
    /// The device context in use. This value is is optional (but recommended), except when the
    /// <see cref="WIN2D_GET_D2D_IMAGE_FLAGS.WIN2D_GET_D2D_IMAGE_FLAGS_READ_DPI_FROM_DEVICE_CONTEXT"/> flag is specified. This is because
    /// not all callers of <see cref="GetD2DImage"/> have easy access to a context. It is always
    /// possible to get a resource creation context from the device, but the context is only
    /// actually necessary if a new effect realization needs to be created, so it is more efficient
    /// to have the implementation do this lookup only if/when it turns out to be needed.
    /// </param>
    /// <param name="flags">The flags to use to get the image.</param>
    /// <param name="targetDpi">
    /// The DPI of the target device context. This is used to determine when a <c>D2D1DpiCompensation</c>
    /// effect needs to be inserted. Behavior of this parameter can be overridden by the flag values
    /// <see cref="WIN2D_GET_D2D_IMAGE_FLAGS.WIN2D_GET_D2D_IMAGE_FLAGS_READ_DPI_FROM_DEVICE_CONTEXT"/>,
    /// <see cref="WIN2D_GET_D2D_IMAGE_FLAGS.WIN2D_GET_D2D_IMAGE_FLAGS_ALWAYS_INSERT_DPI_COMPENSATION"/>
    /// or <see cref="WIN2D_GET_D2D_IMAGE_FLAGS.WIN2D_GET_D2D_IMAGE_FLAGS_NEVER_INSERT_DPI_COMPENSATION"/>
    /// </param>
    /// <param name="realizeDpi">
    /// The DPI of a source bitmap, or zero if the image does not have a fixed DPI. A <c>D2D1DpiCompensation</c> effect
    /// will be inserted if <paramref name="targetDpi"/> and <paramref name="realizeDpi"/> are different (flags permitting).
    /// </param>
    /// <param name="ppImage">The resulting <see cref="ID2D1Image"/> for the effect.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(4)]
    public HRESULT GetD2DImage(
        ICanvasDevice* device,
        ID2D1DeviceContext* deviceContext,
        WIN2D_GET_D2D_IMAGE_FLAGS flags,
        float targetDpi,
        float* realizeDpi,
        ID2D1Image** ppImage)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasImageInterop*, ICanvasDevice*, ID2D1DeviceContext*, WIN2D_GET_D2D_IMAGE_FLAGS, float, float*, ID2D1Image**, int>)this.lpVtbl[4])(
            (ICanvasImageInterop*)Unsafe.AsPointer(ref this),
            device,
            deviceContext,
            flags,
            targetDpi,
            realizeDpi,
            ppImage);
    }

    /// <summary>
    /// The <see cref="ComImportAttribute"/> implementation of <see cref="ICanvasImageInterop"/>.
    /// </summary>
    [ComImport]
    [Guid("E042D1F7-F9AD-4479-A713-67627EA31863")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface Interface
    {
        /// <inheritdoc cref="ICanvasImageInterop.GetDevice"/>
        [PreserveSig]
        [return: NativeTypeName("HRESULT")]
        int GetDevice(ICanvasDevice** device);

        /// <inheritdoc cref="ICanvasImageInterop.GetD2DImage"/>
        [PreserveSig]
        [return: NativeTypeName("HRESULT")]
        int GetD2DImage(
            ICanvasDevice* device,
            ID2D1DeviceContext* deviceContext,
            WIN2D_GET_D2D_IMAGE_FLAGS flags,
            float targetDpi,
            float* realizeDpi,
            ID2D1Image** ppImage);
    }
}