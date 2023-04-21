using System;
using System.Diagnostics.CodeAnalysis;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.Windows;
#if WINDOWS_UWP
using TerraFX.Interop.WinRT;
#endif
using Windows.Graphics.Effects;
#if !WINDOWS_UWP
using WinRT;
#endif

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.Helpers;
#else
namespace ComputeSharp.D2D1.WinUI.Helpers;

using CanvasDevice = Microsoft.Graphics.Canvas.CanvasDevice;
using IInspectable = TerraFX.Interop.WinRT.IInspectable;
#endif

/// <summary>
/// A helper type to replicate Win2D's <c>ResourceManager</c> type.
/// </summary>
/// <remarks>
/// This type provides an entry point to use the Win2D <see cref="ICanvasFactoryNative"/> activation factory.
/// </remarks>
internal static unsafe class ResourceManager
{
    /// <summary>
    /// Gets or creates an <see cref="IGraphicsEffectSource"/> instance for a native resource.
    /// </summary>
    /// <param name="device">The input canvas device (as a marshalled <see cref="Microsoft.Graphics.Canvas.CanvasDevice"/>).</param>
    /// <param name="resource">The input native resource to create a wrapper for.</param>
    /// <param name="dpi">The realization DPIs for <paramref name="resource"/></param>
    /// <returns>The resulting <see cref="IGraphicsEffectSource"/> wrapper instance.</returns>
    /// <exception cref="Exception">Thrown if the managed wrapper could not be retrieved.</exception>
    public static IGraphicsEffectSource GetOrCreate(ICanvasDevice* device, IUnknown* resource, float dpi)
    {
        using ComPtr<ICanvasFactoryNative> canvasFactoryNative = default;

        GetActivationFactory(canvasFactoryNative.GetAddressOf());

        using ComPtr<IInspectable> wrapperInspectable = default;

        // Get or create a WinRT wrapper for the resource
        canvasFactoryNative.Get()->GetOrCreate(
            device: device,
            resource: resource,
            dpi: dpi,
            wrapper: wrapperInspectable.GetAddressOf()).Assert();

        // Get the runtime-provided RCW for the resulting WinRT wrapper
        return RcwMarshaller.GetOrCreateManagedObject<IGraphicsEffectSource>((IUnknown*)wrapperInspectable.Get());
    }

    /// <summary>
    /// Registers a managed wrapper for a given native resource.
    /// </summary>
    /// <param name="resource">The input native resource to register a wrapper for.</param>
    /// <param name="wrapper">The wrapper to register for <paramref name="resource"/>.</param>
    public static void RegisterWrapper<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.Interfaces)] T>(IUnknown* resource, T wrapper)
        where T : class
    {
        using ComPtr<ICanvasFactoryNative> canvasFactoryNative = default;

        GetActivationFactory(canvasFactoryNative.GetAddressOf());

        using ComPtr<IInspectable> wrapperInspectable = default;

        // Unwrap the input wrapper and get an IInspectable* object
        RcwMarshaller.GetNativeObject(wrapper, wrapperInspectable.GetAddressOf()).Assert();

        // Register this pair of native resource and inspectable wrapper
        canvasFactoryNative.Get()->RegisterWrapper(resource, wrapperInspectable.Get()).Assert();
    }

    /// <summary>
    /// Unregisters a native resource from Win2D's internal cache.
    /// </summary>
    /// <param name="resource">The input native resource to unregister a wrapper for.</param>
    public static void UnregisterWrapper(IUnknown* resource)
    {
        using ComPtr<ICanvasFactoryNative> canvasFactoryNative = default;

        GetActivationFactory(canvasFactoryNative.GetAddressOf());

        // Unregister the previously registered native resource
        canvasFactoryNative.Get()->UnregisterWrapper(resource).Assert();
    }

    /// <summary>
    /// Gets the <see cref="ICanvasFactoryNative"/> activation factory.
    /// </summary>
    /// <param name="factoryNative">A pointer to the resulting activation factory.</param>
    private static void GetActivationFactory(ICanvasFactoryNative** factoryNative)
    {
#if WINDOWS_UWP
        const string activatableClassId = "Microsoft.Graphics.Canvas.CanvasDevice";

        // On UWP, the WinRT types from Win2D are automatically registered for activation.
        // This means we can simply use RoGetActivationFactory to retrieve the factory.
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
                factory: (void**)factoryNative).Assert();
        }
#else
        // On WinUI 3, the types are not guaranteed to be registered for activation. Additionally,
        // for concistency with other types, we just use the built-in T.As<I>() method, which will
        // automatically handle fallback logic to resolve types to activate if they're not registered.
        // For instance, this will ensure the following call will work fine in unpackaged apps.
        ICanvasFactoryNative.Interface canvasDeviceActivationFactory = CanvasDevice.As<ICanvasFactoryNative.Interface>();

        *factoryNative = (ICanvasFactoryNative*)MarshalInspectable<ICanvasFactoryNative.Interface>.FromManaged(canvasDeviceActivationFactory);
#endif
    }
}
