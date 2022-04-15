using ComputeSharp.D2D1Interop.Tests.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1Interop.Tests.Helpers;

/// <summary>
/// A test helper for D2D1 pixel shaders.
/// </summary>
internal static class D2D1ShaderTestHelper
{
    /// <summary>
    /// Executes a pixel shader to produce an image.
    /// </summary>
    /// <typeparam name="T">The shader type to execute.</typeparam>
    /// <param name="sourcePath">The source path for the image to run the shader on.</param>
    /// <param name="destinationPath">The destination path for the result.</param>
    /// <param name="shader">The shader to run.</param>
    public static unsafe void ExecutePixelShaderAndCompareResults<T>(
        string sourcePath,
        string destinationPath,
        in T shader)
        where T : unmanaged, ID2D1PixelShader
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = default;

        D2D1_FACTORY_OPTIONS d2D1FactoryOptions = default;

        // Create a Direct2D factory
        DirectX.D2D1CreateFactory(
            factoryType: D2D1_FACTORY_TYPE.D2D1_FACTORY_TYPE_SINGLE_THREADED,
            riid: Windows.__uuidof<ID2D1Factory2>(),
            pFactoryOptions: &d2D1FactoryOptions,
            ppIFactory: (void**)d2D1Factory2.GetAddressOf()).Assert();

        using ComPtr<ID3D11Device> d3D11Device = default;

        uint creationFlags = (uint)D3D11_CREATE_DEVICE_FLAG.D3D11_CREATE_DEVICE_BGRA_SUPPORT;
        D3D_FEATURE_LEVEL* featureLevels = stackalloc[]
        {
            D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_1,
            D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0,
            D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_1,
            D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_0,
            D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_3,
            D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_2,
            D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_1
        };
        D3D_FEATURE_LEVEL d3DFeatureLevel;

        // Create the Direct3D 11 API device and context
        DirectX.D3D11CreateDevice(
            pAdapter: null,
            DriverType: D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_HARDWARE,
            Software: HMODULE.NULL,
            Flags: creationFlags,
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
        d2D1Factory2.Get()->CreateDevice(
            dxgiDevice: (IDXGIDevice*)dxgiDevice3.Get(),
            d2dDevice: d2D1Device.GetAddressOf());

        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = default;

        // Create a D2D1 device context
        d2D1Device.Get()->CreateDeviceContext(
            options: D2D1_DEVICE_CONTEXT_OPTIONS.D2D1_DEVICE_CONTEXT_OPTIONS_NONE,
            deviceContext: d2D1DeviceContext.GetAddressOf()).Assert();

        using ComPtr<IWICBitmap> wicBitmap = WICHelper.LoadBitmapFromFile(sourcePath, out uint width, out uint height);

        D2D_SIZE_U d2DSize;
        d2DSize.width = width;
        d2DSize.height = height;

        D2D1_BITMAP_PROPERTIES1 d2DBitmapProperties1Target = default;
        d2DBitmapProperties1Target.pixelFormat.format = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM;
        d2DBitmapProperties1Target.pixelFormat.alphaMode = D2D1_ALPHA_MODE.D2D1_ALPHA_MODE_PREMULTIPLIED;
        d2DBitmapProperties1Target.bitmapOptions =
            D2D1_BITMAP_OPTIONS.D2D1_BITMAP_OPTIONS_TARGET |
            D2D1_BITMAP_OPTIONS.D2D1_BITMAP_OPTIONS_CANNOT_DRAW;

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Target = default;

        // Create a target D2D1 bitmap
        d2D1DeviceContext.Get()->CreateBitmap(
            size: d2DSize,
            sourceData: null,
            pitch: 0,
            bitmapProperties: &d2DBitmapProperties1Target,
            bitmap: d2D1Bitmap1Target.GetAddressOf()).Assert();

        d2D1DeviceContext.Get()->SetTarget((ID2D1Image*)d2D1Bitmap1Target.Get());

        using ComPtr<ID2D1Bitmap> d2D1BitmapSource = default;

        // Create a source D2D1 bitmap from the WIC bitmap
        d2D1DeviceContext.Get()->CreateBitmapFromWicBitmap(
            wicBitmapSource: (IWICBitmapSource*)wicBitmap.Get(),
            bitmap: d2D1BitmapSource.GetAddressOf()).Assert();

        D2D1InteropServices.RegisterPixelShaderEffectForD2D1Factory1<T>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1InteropServices.CreatePixelShaderEffectFromD2D1DeviceContext<T>(
            d2D1DeviceContext.Get(),
            (void**)d2D1Effect.GetAddressOf());

        D2D1InteropServices.SetConstantBufferForD2D1Effect(in shader, d2D1Effect.Get());

        d2D1Effect.Get()->SetInput(0, (ID2D1Image*)d2D1BitmapSource.Get());

        d2D1DeviceContext.Get()->BeginDraw();

        // Draw the image with the effect
        d2D1DeviceContext.Get()->DrawImage(
            effect: d2D1Effect.Get(),
            targetOffset: null,
            imageRectangle: null,
            interpolationMode: D2D1_INTERPOLATION_MODE.D2D1_INTERPOLATION_MODE_NEAREST_NEIGHBOR,
            compositeMode: D2D1_COMPOSITE_MODE.D2D1_COMPOSITE_MODE_SOURCE_COPY);

        d2D1DeviceContext.Get()->EndDraw().Assert();

        D2D1_BITMAP_PROPERTIES1 d2DBitmapProperties1Buffer = default;
        d2DBitmapProperties1Buffer.pixelFormat.format = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM;
        d2DBitmapProperties1Buffer.pixelFormat.alphaMode = D2D1_ALPHA_MODE.D2D1_ALPHA_MODE_PREMULTIPLIED;
        d2DBitmapProperties1Buffer.bitmapOptions = D2D1_BITMAP_OPTIONS.D2D1_BITMAP_OPTIONS_CPU_READ | D2D1_BITMAP_OPTIONS.D2D1_BITMAP_OPTIONS_CANNOT_DRAW;

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = default;

        // Create a buffer D2D1 bitmap
        d2D1DeviceContext.Get()->CreateBitmap(
            size: d2DSize,
            sourceData: null,
            pitch: 0,
            bitmapProperties: &d2DBitmapProperties1Buffer,
            bitmap: d2D1Bitmap1Buffer.GetAddressOf()).Assert();

        D2D_POINT_2U d2DPointDestination = default;
        D2D_RECT_U d2DRectSource = new(0, 0, width, height);

        // Copy the image from the target to the readback bitmap
        d2D1Bitmap1Buffer.Get()->CopyFromBitmap(
            destPoint: &d2DPointDestination,
            bitmap: (ID2D1Bitmap*)d2D1Bitmap1Target.Get(),
            srcRect: &d2DRectSource);

        D2D1_MAPPED_RECT d2D1MappedRect;

        // Map the buffer bitmap
        d2D1Bitmap1Buffer.Get()->Map(
            options: D2D1_MAP_OPTIONS.D2D1_MAP_OPTIONS_READ,
            mappedRect: &d2D1MappedRect).Assert();

        // Save the image
        WICHelper.SaveBitmapToFile(destinationPath, width, height, d2D1MappedRect.pitch, d2D1MappedRect.bits);
    }
}
