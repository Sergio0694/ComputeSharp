using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using Win32 = TerraFX.Interop.Windows.Windows;

namespace ComputeSharp.D2D1.Uwp.Extensions;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="ICanvasDevice"/> type.
/// </summary>
internal static unsafe class ICanvasDeviceExtensions
{
    /// <summary>
    /// Gets the underlying <see cref="ID2D1Device1"/> object from an <see cref="ICanvasDevice"/> object.
    /// </summary>
    /// <param name="canvasDevice">The input <see cref="ICanvasDevice"/> object.</param>
    /// <param name="d2D1Device1">The underlying <see cref="ID2D1Device1"/> object for <paramref name="canvasDevice"/>.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static HRESULT GetD2DDevice(this ref ICanvasDevice canvasDevice, ID2D1Device1** d2D1Device1)
    {
        using ComPtr<ICanvasResourceWrapperNative> canvasDeviceResourceWrapperNative = default;

        // Get the ICanvasResourceWrapperNative wrapper around the input device
        canvasDevice.QueryInterface(
            riid: Win32.__uuidof<ICanvasResourceWrapperNative>(),
            ppvObject: canvasDeviceResourceWrapperNative.GetVoidAddressOf()).Assert();

        // Get the underlying ID2D1Device1 instance in use
        HRESULT hresult = canvasDeviceResourceWrapperNative.Get()->GetNativeResource(
            device: null,
            dpi: 0,
            iid: Win32.__uuidof<ID2D1Device1>(),
            resource: (void**)d2D1Device1);

        return hresult;
    }

    /// <summary>
    /// Gets a new <see cref="ID2D1DeviceContextLease"/> from an input <see cref="ICanvasDevice"/>.
    /// </summary>
    /// <param name="canvasDevice">The input <see cref="ICanvasDevice"/> object.</param>
    /// <param name="d2D1DeviceContextLease">The resulting <see cref="ID2D1DeviceContextLease"/> object.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static HRESULT GetD2DDeviceContextLease(this ref ICanvasDevice canvasDevice, ID2D1DeviceContextLease** d2D1DeviceContextLease)
    {
        using ComPtr<ID2D1DeviceContextPool> d2D1DeviceContextPool = default;

        // Get the ID2D1DeviceContextLease interface reference
        canvasDevice.QueryInterface(
            riid: Win32.__uuidof<ID2D1DeviceContextPool>(),
            ppvObject: d2D1DeviceContextPool.GetVoidAddressOf()).Assert();

        // Create the new lease
        HRESULT hresult = d2D1DeviceContextPool.Get()->GetDeviceContextLease(d2D1DeviceContextLease);

        return hresult;
    }
}