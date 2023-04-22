using System;
using System.Runtime.InteropServices;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp.Extensions;
using ComputeSharp.D2D1.Uwp.Helpers;
#else
using ComputeSharp.D2D1.WinUI.Extensions;
using ComputeSharp.D2D1.WinUI.Helpers;
#endif
using TerraFX.Interop;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using TerraFX.Interop.WinRT;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp;
#else
namespace ComputeSharp.D2D1.WinUI;
#endif

/// <inheritdoc/>
unsafe partial class PixelShaderEffect<T>
{
    /// <inheritdoc cref="ICanvasEffectFactoryNative.CreateWrapper"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("HRESULT")]
    private delegate int CreateWrapperDelegate(ICanvasDevice* device, ID2D1Effect* resource, float dpi, IInspectable** wrapper);

    /// <summary>
    /// A cached <see cref="CreateWrapperDelegate"/> instance wrapping <see cref="CreateWrapper"/>.
    /// </summary>
    private static readonly CreateWrapperDelegate CreateWrapperWrapper = CreateWrapper;

    /// <summary>
    /// Creates a new <see cref="PixelShaderEffect{T}"/> instance for an input D2D effect and marshals it as an <see cref="IInspectable"/> wrapper.
    /// </summary>
    /// <param name="device"><inheritdoc cref="ICanvasEffectFactoryNative.CreateWrapper" path="/param[@name='device']"/></param>
    /// <param name="resource"><inheritdoc cref="ICanvasEffectFactoryNative.CreateWrapper" path="/param[@name='resource']"/></param>
    /// <param name="dpi"><inheritdoc cref="ICanvasEffectFactoryNative.CreateWrapper" path="/param[@name='dpi']"/></param>
    /// <param name="wrapper"><inheritdoc cref="ICanvasEffectFactoryNative.CreateWrapper" path="/param[@name='wrapper']"/></param>
    /// <returns>This method always returns <see cref="S.S_OK"/> (any exceptions will be marshalled by the delegate stub).</returns>
    private static int CreateWrapper(ICanvasDevice* device, ID2D1Effect* resource, float dpi, IInspectable** wrapper)
    {
        PixelShaderEffect<T> @this = new();

        @this.InitializeFromNativeResource(device, resource);

        RcwMarshaller.GetNativeObject(@this, wrapper);

        return S.S_OK;
    }

    /// <summary>
    /// Initializes a <see cref="PixelShaderEffect{T}"/> instance from an external realized D2D1 effect.
    /// </summary>
    /// <param name="device"><inheritdoc cref="ICanvasEffectFactoryNative.CreateWrapper" path="/param[@name='device']"/></param>
    /// <param name="resource"><inheritdoc cref="ICanvasEffectFactoryNative.CreateWrapper" path="/param[@name='resource']"/></param>
    private void InitializeFromNativeResource(ICanvasDevice* device, ID2D1Effect* resource)
    {
        using ComPtr<ID2D1Device1> d2D1Device1 = default;

        // Get the underlying ID2D1Device1 instance for the input device
        device->GetD2DDevice(d2D1Device1.GetAddressOf()).Assert();

        // Initialize the effect state
        device->CopyTo(ref this.canvasDevice).Assert();
        d2D1Device1.CopyTo(ref this.d2D1RealizationDevice).Assert();
        resource->CopyTo(ref this.d2D1Effect).Assert();
    }
}