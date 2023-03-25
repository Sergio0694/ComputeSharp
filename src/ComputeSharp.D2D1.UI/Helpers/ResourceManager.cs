using System;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.Windows;
using TerraFX.Interop.WinRT;
using Windows.Graphics.Effects;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.Helpers;
#else
namespace ComputeSharp.D2D1.WinUI.Helpers;

using WinRT = TerraFX.Interop.WinRT.WinRT;
#endif

/// <summary>
/// A helper type to replicate Win2D's <c>ResourceManager</c> type.
/// </summary>
/// <remarks>
/// This type provides an entry point to use the Win2D <see cref="ICanvasFactoryNative"/> activation factory.
/// </remarks>
internal static class ResourceManager
{
    /// <summary>
    /// Gets or creates an <see cref="IGraphicsEffectSource"/> instance for a native resource.
    /// </summary>
    /// <param name="device">The input canvas device (as a marshalled <see cref="Microsoft.Graphics.Canvas.CanvasDevice"/>).</param>
    /// <param name="resource">The input native resource to create a wrapper for.</param>
    /// <param name="dpi">The realization DPIs for <paramref name="resource"/></param>
    /// <returns>The resulting <see cref="IGraphicsEffectSource"/> wrapper instance.</returns>
    /// <exception cref="Exception">Thrown if the managed wrapper could not be retrieved.</exception>
    public static unsafe IGraphicsEffectSource GetOrCreate(ICanvasDevice* device, IUnknown* resource, float dpi)
    {
        using ComPtr<ICanvasFactoryNative> canvasFactoryNative = default;

        const string activatableClassId = "Microsoft.Graphics.Canvas.CanvasDevice";

        fixed (char* pActivatableClassId = activatableClassId)
        {
            HSTRING_HEADER hStringHeaderActivatableClassId;
            HSTRING hStringActivatableClass;

            // Create a fast-pass HSTRING for "Microsoft.Graphics.Canvas.CanvasDevice"
            WinRT.WindowsCreateStringReference(
                sourceString: (ushort*)pActivatableClassId,
                length: (uint)activatableClassId.Length,
                hstringHeader: &hStringHeaderActivatableClassId,
                @string: &hStringActivatableClass).Assert();

            Guid canvasFactoryNativeId = typeof(ICanvasFactoryNative).GUID;

            // Get the activation factory for CanvasDevice
            WinRT.RoGetActivationFactory(
                activatableClassId: hStringActivatableClass,
                iid: &canvasFactoryNativeId,
                factory: canvasFactoryNative.GetVoidAddressOf()).Assert();
        }

        using ComPtr<IUnknown> wrapperUnknown = default;

        // Get or create a WinRT wrapper for the resource
        canvasFactoryNative.Get()->GetOrCreate(
            device: device,
            resource: resource,
            dpi: dpi,
            wrapper: wrapperUnknown.GetVoidAddressOf()).Assert();

        // Get the runtime-provided RCW for the resulting WinRT wrapper
        return RcwMarshaller.GetOrCreateManagedObject<IGraphicsEffectSource>(wrapperUnknown.Get());
    }
}
