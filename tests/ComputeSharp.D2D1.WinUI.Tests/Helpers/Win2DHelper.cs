using System;
using System.Runtime.InteropServices;
using Microsoft.Graphics.Canvas;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using WinRT;
using WinRT.Interop;

namespace ComputeSharp.D2D1.WinUI.Tests.Helpers;

/// <summary>
/// A helper type to marshal managed objects in unit tests.
/// </summary>
internal static class Win2DHelper
{
    /// <summary>
    /// Gets the managed wrapper for a given <see cref="ID2D1Image"/> object.
    /// </summary>
    /// <param name="d2D1Image">The input <see cref="ID2D1Image"/> object to get a wrapper for.</param>
    /// <param name="canvasDevice">The realization <see cref="CanvasDevice"/> instance to use.</param>
    /// <returns>The resulting managed wrapper for <paramref name="d2D1Image"/>.</returns>
    public static unsafe object GetOrCreate(ID2D1Image* d2D1Image, CanvasDevice? canvasDevice = null)
    {
        using ComPtr<IUnknown> activationFactoryUnknown = default;

        ICanvasFactoryNative activationFactory = CanvasDevice.As<ICanvasFactoryNative>();

        activationFactoryUnknown.Attach((IUnknown*)MarshalInterface<ICanvasFactoryNative>.FromManaged(activationFactory));

        using ComPtr<IUnknown> canvasFactoryNativeUnknown = default;

        Guid uuidOfCanvasFactoryNativeUnknown = new("695C440D-04B3-4EDD-BFD9-63E51E9F7202");

        //Get the ICanvasFactoryNative object
        int hresult = activationFactoryUnknown.CopyTo(&uuidOfCanvasFactoryNativeUnknown, (void**)canvasFactoryNativeUnknown.GetAddressOf());

        Marshal.ThrowExceptionForHR(hresult);

        using ComPtr<IUnknown> canvasDeviceUnknown = default;

        if (canvasDevice is not null)
        {
            canvasDeviceUnknown.Attach((IUnknown*)MarshalInspectable<CanvasDevice>.FromManaged(canvasDevice));
        }

        using ComPtr<IUnknown> canvasDeviceInterfaceUnknown = default;

        if (canvasDevice is not null)
        {
            Guid uuidOfCanvasDeviceInterface = new("A27F0B5D-EC2C-4D4F-948F-0AA1E95E33E6");

            // Get the ICanvasDevice object (as an IUnknown* as well)
            hresult = canvasDeviceUnknown.CopyTo(&uuidOfCanvasDeviceInterface, (void**)canvasDeviceInterfaceUnknown.GetAddressOf());
        }

        using ComPtr<IUnknown> wrapperUnknown = default;

        // Invoke GetOrCreate to get the inspectable wrapper
        hresult = ((delegate* unmanaged[MemberFunction]<IUnknown*, IUnknown*, IUnknown*, float, IUnknown**, int>)(*(void***)canvasFactoryNativeUnknown.Get())[6])(
            canvasFactoryNativeUnknown.Get(),
            canvasDeviceInterfaceUnknown.Get(),
            (IUnknown*)d2D1Image,
            0,
            wrapperUnknown.GetAddressOf());

        Marshal.ThrowExceptionForHR(hresult);

        // Get or create a managed object for the native object
        return MarshalInspectable<object>.FromAbi((IntPtr)wrapperUnknown.Get());
    }

    /// <summary>
    /// Gets the D2D image from a given <see cref="ICanvasImage"/> object.
    /// </summary>
    /// <param name="canvasImage">The input <see cref="ICanvasImage"/> object.</param>
    /// <param name="canvasDevice">The realization <see cref="CanvasDevice"/> instance to use.</param>
    /// <param name="d2D1Image">The resulting <see cref="ID2D1Image"/> object.</param>
    public static unsafe void GetD2DImage(ICanvasImage canvasImage, CanvasDevice canvasDevice, ID2D1Image** d2D1Image)
    {
        using ComPtr<IUnknown> effectUnknown = default;

        effectUnknown.Attach((IUnknown*)MarshalInterface<ICanvasImage>.FromManaged(canvasImage));

        using ComPtr<IUnknown> canvasImageInteropUnknown = default;

        Guid uuidOfCanvasImageInterop = new("E042D1F7-F9AD-4479-A713-67627EA31863");

        // Get the ICanvasImageInterop object (as an IUnknown*)
        int hresult = effectUnknown.CopyTo(&uuidOfCanvasImageInterop, (void**)canvasImageInteropUnknown.GetAddressOf());

        Marshal.ThrowExceptionForHR(hresult);

        using ComPtr<IUnknown> canvasDeviceUnknown = default;

        canvasDeviceUnknown.Attach((IUnknown*)MarshalInspectable<CanvasDevice>.FromManaged(canvasDevice));

        using ComPtr<IUnknown> canvasDeviceInterfaceUnknown = default;

        Guid uuidOfCanvasDeviceInterface = new("A27F0B5D-EC2C-4D4F-948F-0AA1E95E33E6");

        // Get the ICanvasDevice object (as an IUnknown* as well)
        hresult = canvasDeviceUnknown.CopyTo(&uuidOfCanvasDeviceInterface, (void**)canvasDeviceInterfaceUnknown.GetAddressOf());

        Marshal.ThrowExceptionForHR(hresult);

        // Invoke GetD2DImage (passing NEVER_INSERT_DPI_COMPENSATION | MINIMAL_REALIZATION | ALLOW_NULL_EFFECT_INPUTS)
        hresult = ((delegate* unmanaged[MemberFunction]<IUnknown*, IUnknown*, IUnknown*, uint, float, float*, ID2D1Image**, int>)(*(void***)canvasImageInteropUnknown.Get())[4])(
            canvasImageInteropUnknown.Get(),
            canvasDeviceInterfaceUnknown.Get(),
            null,
            4 | 8 | 16,
            0,
            null,
            d2D1Image);

        Marshal.ThrowExceptionForHR(hresult);
    }

    /// <summary>
    /// The managed interface for <see cref="ICanvasFactoryNative"/>.
    /// </summary>
    [Guid("695C440D-04B3-4EDD-BFD9-63E51E9F7202")]
    [WindowsRuntimeType]
    [WindowsRuntimeHelperType(typeof(ICanvasFactoryNative))]
    public interface ICanvasFactoryNative
    {
        /// <summary>
        /// The vtable type for <see cref="ICanvasFactoryNative"/>.
        /// </summary>
        [Guid("695C440D-04B3-4EDD-BFD9-63E51E9F7202")]
        public readonly struct Vftbl
        {
            /// <summary>
            /// Allows CsWinRT to retrieve a pointer to the projection vtable (the name is hardcoded by convention).
            /// </summary>
            public static readonly IntPtr AbiToProjectionVftablePtr = IUnknownVftbl.AbiToProjectionVftblPtr;
        }
    }
}