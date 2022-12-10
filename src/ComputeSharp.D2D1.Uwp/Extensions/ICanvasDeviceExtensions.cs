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
    /// Creates a new <see cref="ID2D1DeviceContext"/> from an input <see cref="ICanvasDevice"/>.
    /// </summary>
    /// <param name="canvasDevice">The input <see cref="ICanvasDevice"/> object.</param>
    /// <param name="d2D1DeviceContext">The resulting <see cref="ID2D1DeviceContext"/> object.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static HRESULT CreateD2DDeviceContext(this ref ICanvasDevice canvasDevice, ID2D1DeviceContext** d2D1DeviceContext)
    {
        using ComPtr<ID2D1Device1> d2D1Device1 = default;

        // Get the underlying ID2D1Device1 object
        canvasDevice.GetD2DDevice(d2D1Device1.GetAddressOf()).Assert();

        // Create the new device context
        HRESULT hresult = d2D1Device1.Get()->CreateDeviceContext(
            options: D2D1_DEVICE_CONTEXT_OPTIONS.D2D1_DEVICE_CONTEXT_OPTIONS_NONE,
            deviceContext: d2D1DeviceContext);

        return hresult;
    }
}