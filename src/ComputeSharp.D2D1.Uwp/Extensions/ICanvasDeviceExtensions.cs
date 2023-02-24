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
    /// Retrieves a usable <see cref="ID2D1DeviceContext"/> from an input <see cref="ICanvasDevice"/> if one is not available.
    /// If a new one had to be rented from the pool, it also returns the owning <see cref="ID2D1DeviceContextLease"/> instance.
    /// </summary>
    /// <param name="canvasDevice">The input <see cref="ICanvasDevice"/> object.</param>
    /// <param name="d2D1DeviceContext">The current <see cref="ID2D1DeviceContext"/> object, if available.</param>
    /// <param name="d2D1DeviceContextEffective">The resulting <see cref="ID2D1DeviceContext"/> instance to use.</param>
    /// <param name="d2D1DeviceContextLease">The resulting <see cref="ID2D1DeviceContextLease"/> object, if used.</param>
    /// <remarks>
    /// When this method returns, <paramref name="d2D1DeviceContextEffective"/> is guaranteed to not be <see langword="null"/>.
    /// </remarks>
    public static void GetEffectiveD2DDeviceContextWithOptionalLease(
        this ref ICanvasDevice canvasDevice,
        ID2D1DeviceContext* d2D1DeviceContext,
        ID2D1DeviceContext** d2D1DeviceContextEffective,
        ID2D1DeviceContextLease** d2D1DeviceContextLease)
    {
        // If there is no input device context, just create a new one from the input canvas device
        if (d2D1DeviceContext is null)
        {
            using (ComPtr<ID2D1DeviceContextPool> d2D1DeviceContextPool = default)
            {
                // Get the ID2D1DeviceContextPool interface reference
                canvasDevice.QueryInterface(
                    riid: Win32.__uuidof<ID2D1DeviceContextPool>(),
                    ppvObject: d2D1DeviceContextPool.GetVoidAddressOf()).Assert();

                // Get a new ID2D1DeviceContextLease object from the retrieved pool
                d2D1DeviceContextPool.Get()->GetDeviceContextLease(d2D1DeviceContextLease).Assert();
            }

            // Get the underlying device context from the lease (this is much faster than creating a new device context)
            (*d2D1DeviceContextLease)->GetD2DDeviceContext(d2D1DeviceContextEffective).Assert();
        }
        else
        {
            // Otherwise, just use the input device context
            *d2D1DeviceContextEffective = new ComPtr<ID2D1DeviceContext>(d2D1DeviceContext);
        }
    }
}