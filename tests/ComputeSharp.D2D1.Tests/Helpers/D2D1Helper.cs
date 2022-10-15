using System;
using System.Drawing;
using ComputeSharp.D2D1.Tests.Extensions;
using Win32;
using Win32.Graphics.Direct2D;
using Win32.Graphics.Direct3D11;
using Win32.Graphics.Dxgi;

namespace ComputeSharp.D2D1.Tests.Helpers;

using D2D1_ALPHA_MODE = Win32.Graphics.Direct2D.Common.AlphaMode;
using D2D1_BITMAP_OPTIONS = Win32.Graphics.Direct2D.BitmapOptions;
using D2D1_DEVICE_CONTEXT_OPTIONS = Win32.Graphics.Direct2D.DeviceContextOptions;
using D2D1_FACTORY_OPTIONS = Win32.Graphics.Direct2D.FactoryOptions;
using D2D1_FACTORY_TYPE = Win32.Graphics.Direct2D.FactoryType;
using D2D1_INTERPOLATION_MODE = Win32.Graphics.Direct2D.InterpolationMode;
using D2D1_COMPOSITE_MODE = Win32.Graphics.Direct2D.Common.CompositeMode;
using D2D1_MAP_OPTIONS = Win32.Graphics.Direct2D.MapOptions;
using D2D_RECT_U = Win32.Graphics.Direct2D.Common.RectU;
using D2D1_MAPPED_RECT = Win32.Graphics.Direct2D.MappedRect;
using D2D1_BITMAP_PROPERTIES = Win32.Graphics.Direct2D.BitmapProperties;
using D2D1_BITMAP_PROPERTIES1 = Win32.Graphics.Direct2D.BitmapProperties1;
using D3D_FEATURE_LEVEL = Win32.Graphics.Direct3D.FeatureLevel;
using D3D_DRIVER_TYPE = Win32.Graphics.Direct3D.DriverType;
using D3D11_CREATE_DEVICE_FLAG = Win32.Graphics.Direct3D11.CreateDeviceFlags;
using DXGI_FORMAT = Win32.Graphics.Dxgi.Common.Format;
using Win32 = Win32.Apis;
using D2D1 = Win32.Graphics.Direct2D.Apis;
using D3D11 = Win32.Graphics.Direct3D11.Apis;

/// <summary>
/// A <see langword="class"/> that uses the D2D1 APIs to configure and run effects.
/// </summary>
internal static class D2D1Helper
{
    /// <summary>
    /// Creates an <see cref="ID2D1Factory2"/> instance.
    /// </summary>
    /// <param name="singleThreaded">Indicates whether or not the factory should be created as single threaded.</param>
    /// <returns>A new <see cref="ID2D1Factory2"/> instance.</returns>
    public static unsafe ComPtr<ID2D1Factory2> CreateD2D1Factory2(bool singleThreaded = false)
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = default;

        D2D1_FACTORY_OPTIONS d2D1FactoryOptions = default;

        // Create a Direct2D factory
        D2D1.D2D1CreateFactory(
            factoryType: singleThreaded ? D2D1_FACTORY_TYPE.SingleThreaded : D2D1_FACTORY_TYPE.MultiThreaded,
            riid: Win32.__uuidof<ID2D1Factory2>(),
            pFactoryOptions: &d2D1FactoryOptions,
            ppIFactory: (void**)d2D1Factory2.GetAddressOf()).Assert();

        return d2D1Factory2.Move();
    }

    /// <summary>
    /// Creates an <see cref="ID2D1Device"/> instance.
    /// </summary>
    /// <param name="d2D1Factory2">The input <see cref="ID2D1Factory2"/> instance to use to create the device.</param>
    /// <returns>A new <see cref="ID2D1Device"/> instance.</returns>
    public static unsafe ComPtr<ID2D1Device> CreateD2D1Device(ID2D1Factory2* d2D1Factory2)
    {
        using ComPtr<ID3D11Device> d3D11Device = default;

        D3D_FEATURE_LEVEL* featureLevels = stackalloc[]
        {
            D3D_FEATURE_LEVEL.Level_11_1,
            D3D_FEATURE_LEVEL.Level_11_0,
            D3D_FEATURE_LEVEL.Level_10_1,
            D3D_FEATURE_LEVEL.Level_10_0,
            D3D_FEATURE_LEVEL.Level_9_3,
            D3D_FEATURE_LEVEL.Level_9_2,
            D3D_FEATURE_LEVEL.Level_9_1
        };
        D3D_FEATURE_LEVEL d3DFeatureLevel;

        // Create the Direct3D 11 API device and context
        D3D11.D3D11CreateDevice(
            pAdapter: null,
            DriverType: D3D_DRIVER_TYPE.Hardware,
            Software: IntPtr.Zero,
            Flags: D3D11_CREATE_DEVICE_FLAG.BgraSupport,
            pFeatureLevels: featureLevels,
            FeatureLevels: 7,
            SDKVersion: D3D11.D3D11_SDK_VERSION,
            ppDevice: d3D11Device.GetAddressOf(),
            pFeatureLevel: &d3DFeatureLevel,
            ppImmediateContext: null).Assert();

        using ComPtr<IDXGIDevice3> dxgiDevice3 = default;

        // Get a DXGI device from the D3D11 device
        d3D11Device.CopyTo(dxgiDevice3.GetAddressOf()).Assert();

        using ComPtr<ID2D1Device> d2D1Device = default;

        // Create a D2D1 device
        _ = d2D1Factory2->CreateDevice(
            dxgiDevice: (IDXGIDevice*)dxgiDevice3.Get(),
            d2dDevice: d2D1Device.GetAddressOf());

        return d2D1Device.Move();
    }

    /// <summary>
    /// Creates an <see cref="ID2D1DeviceContext"/> instance.
    /// </summary>
    /// <param name="d2D1Device">The input <see cref="ID2D1Device"/> instance to use to create the context.</param>
    /// <returns>A new <see cref="ID2D1DeviceContext"/> instance.</returns>
    public static unsafe ComPtr<ID2D1DeviceContext> CreateD2D1DeviceContext(ID2D1Device* d2D1Device)
    {
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = default;

        // Create a D2D1 device context
        d2D1Device->CreateDeviceContext(
            options: D2D1_DEVICE_CONTEXT_OPTIONS.None,
            deviceContext: d2D1DeviceContext.GetAddressOf()).Assert();

        return d2D1DeviceContext.Move();
    }

    /// <summary>
    /// Creates an <see cref="ID2D1Bitmap"/> instance.
    /// </summary>
    /// <param name="d2D1DeviceContext">The input <see cref="ID2D1DeviceContext"/> instance to use to create the bitmap source.</param>
    /// <param name="pixels">The loaded pixel data.</param>
    /// <param name="width">The width of the bitmap.</param>
    /// <param name="height">The height of the bitmap.</param>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> to set the source for.</param>
    /// <returns>A new <see cref="ID2D1Bitmap"/> instance.</returns>
    public static unsafe ComPtr<ID2D1Bitmap> CreateD2D1BitmapAndSetAsSource(
        ID2D1DeviceContext* d2D1DeviceContext,
        ReadOnlyMemory<byte> pixels,
        uint width,
        uint height,
        ID2D1Effect* d2D1Effect)
    {
        using ComPtr<ID2D1Bitmap> d2D1BitmapSource = default;

        Size d2DSize = new((int)width, (int)height);

        D2D1_BITMAP_PROPERTIES d2DBitmapProperties = default;
        d2DBitmapProperties.pixelFormat.format = DXGI_FORMAT.B8G8R8A8Unorm;
        d2DBitmapProperties.pixelFormat.alphaMode = D2D1_ALPHA_MODE.Premultiplied;
        d2DBitmapProperties.dpiX = 96;
        d2DBitmapProperties.dpiY = 96;

        fixed (byte* p = pixels.Span)
        {
            // Create a source D2D1 bitmap from the image data
            d2D1DeviceContext->CreateBitmap(
                size: d2DSize,
                srcData: p,
                pitch: width * 4,
                bitmapProperties: &d2DBitmapProperties,
                bitmap: d2D1BitmapSource.GetAddressOf()).Assert();
        }

        d2D1Effect->SetInput(0, (ID2D1Image*)d2D1BitmapSource.Get());

        return d2D1BitmapSource.Move();
    }

    /// <summary>
    /// Creates an <see cref="ID2D1Bitmap"/> instance.
    /// </summary>
    /// <param name="d2D1DeviceContext">The input <see cref="ID2D1DeviceContext"/> instance to use to create the bitmap source.</param>
    /// <param name="width">The width of the bitmap to create.</param>
    /// <param name="height">The height of the bitmap to create.</param>
    /// <returns>A new <see cref="ID2D1Bitmap"/> instance.</returns>
    public static unsafe ComPtr<ID2D1Bitmap> CreateD2D1BitmapAndSetAsTarget(ID2D1DeviceContext* d2D1DeviceContext, uint width, uint height)
    {
        Size d2DSize = new((int)width, (int)height);

        D2D1_BITMAP_PROPERTIES1 d2DBitmapProperties1Target = default;
        d2DBitmapProperties1Target.pixelFormat.format = DXGI_FORMAT.B8G8R8A8Unorm;
        d2DBitmapProperties1Target.pixelFormat.alphaMode = D2D1_ALPHA_MODE.Premultiplied;
        d2DBitmapProperties1Target.bitmapOptions = D2D1_BITMAP_OPTIONS.Target | D2D1_BITMAP_OPTIONS.CannotDraw;

        using ComPtr<ID2D1Bitmap> d2D1Bitmap1Target = default;

        // Create a target D2D1 bitmap
        d2D1DeviceContext->CreateBitmap(
            size: d2DSize,
            sourceData: null,
            pitch: 0,
            bitmapProperties: &d2DBitmapProperties1Target,
            bitmap: (ID2D1Bitmap1**)d2D1Bitmap1Target.GetAddressOf()).Assert();

        d2D1DeviceContext->SetTarget((ID2D1Image*)d2D1Bitmap1Target.Get());

        return d2D1Bitmap1Target.Move();
    }

    /// <summary>
    /// Creates an <see cref="ID2D1Bitmap1"/> instance and copies data from a source bitmap.
    /// </summary>
    /// <param name="d2D1DeviceContext">The input <see cref="ID2D1DeviceContext"/> instance to use to create the bitmap.</param>
    /// <param name="d2D1Bitmap">The input <see cref="ID2D1Bitmap1"/> to read data from.</param>
    /// <param name="d2D1MappedRect">The resulting <see cref="D2D1_MAPPED_RECT"/> for the bitmap.</param>
    /// <returns>A new <see cref="ID2D1Bitmap1"/> instance.</returns>
    public static unsafe ComPtr<ID2D1Bitmap1> CreateD2D1Bitmap1Buffer(ID2D1DeviceContext* d2D1DeviceContext, ID2D1Bitmap* d2D1Bitmap, out D2D1_MAPPED_RECT d2D1MappedRect)
    {
        Size d2DSize = d2D1Bitmap->GetPixelSize();

        D2D1_BITMAP_PROPERTIES1 d2DBitmapProperties1Buffer = default;
        d2DBitmapProperties1Buffer.pixelFormat.format = DXGI_FORMAT.B8G8R8A8Unorm;
        d2DBitmapProperties1Buffer.pixelFormat.alphaMode = D2D1_ALPHA_MODE.Premultiplied;
        d2DBitmapProperties1Buffer.bitmapOptions = D2D1_BITMAP_OPTIONS.CpuRead | D2D1_BITMAP_OPTIONS.CannotDraw;

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = default;

        // Create a buffer D2D1 bitmap
        d2D1DeviceContext->CreateBitmap(
            size: d2DSize,
            sourceData: null,
            pitch: 0,
            bitmapProperties: &d2DBitmapProperties1Buffer,
            bitmap: d2D1Bitmap1Buffer.GetAddressOf()).Assert();

        Point d2DPointDestination = default;
        D2D_RECT_U d2DRectSource = default;
        d2DRectSource.top = 0;
        d2DRectSource.left = 0;
        d2DRectSource.right = (uint)d2DSize.Width;
        d2DRectSource.bottom = (uint)d2DSize.Height;

        // Copy the image from the target to the readback bitmap
        _ = d2D1Bitmap1Buffer.Get()->CopyFromBitmap(
            destPoint: &d2DPointDestination,
            bitmap: d2D1Bitmap,
            srcRect: &d2DRectSource);

        fixed (D2D1_MAPPED_RECT* d2D1MappedRectPtr = &d2D1MappedRect)
        {
            // Map the buffer bitmap
            d2D1Bitmap1Buffer.Get()->Map(
                options: D2D1_MAP_OPTIONS.Read,
                mappedRect: d2D1MappedRectPtr).Assert();
        }

        return d2D1Bitmap1Buffer.Move();
    }

    /// <summary>
    /// Draws a given effect onto a context.
    /// </summary>
    /// <param name="d2D1DeviceContext">The input <see cref="ID2D1DeviceContext"/> instance to use to draw the effect.</param>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> to draw.</param>
    public static unsafe void DrawEffect(ID2D1DeviceContext* d2D1DeviceContext, ID2D1Effect* d2D1Effect)
    {
        d2D1DeviceContext->BeginDraw();

        // Draw the image with the effect
        d2D1DeviceContext->DrawImage(
            effect: d2D1Effect,
            targetOffset: null,
            imageRectangle: null,
            interpolationMode: D2D1_INTERPOLATION_MODE.NearestNeighbor,
            compositeMode: D2D1_COMPOSITE_MODE.SourceCopy);

        d2D1DeviceContext->EndDraw().Assert();
    }
}