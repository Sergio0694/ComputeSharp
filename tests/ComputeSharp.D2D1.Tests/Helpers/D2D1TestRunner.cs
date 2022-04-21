using System;
using ComputeSharp.D2D1.Interop;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Tests.Helpers;

/// <summary>
/// A test helper for D2D1 pixel shaders.
/// </summary>
internal static class D2D1TestRunner
{
    /// <summary>
    /// Executes a pixel shader to produce an image.
    /// </summary>
    /// <typeparam name="T">The shader type to execute.</typeparam>
    /// <param name="shader">The shader to run.</param>
    /// <param name="transformMapperFactory">A custom <see cref="ID2D1TransformMapper{T}"/> factory for the effect.</param>
    /// <param name="sourcePath">The source path for the image to run the shader on.</param>
    /// <param name="destinationPath">The destination path for the result.</param>
    public static unsafe void ExecutePixelShaderAndCompareResults<T>(
        in T shader,
        Func<ID2D1TransformMapper<T>>? transformMapperFactory,
        string sourcePath,
        string destinationPath)
        where T : unmanaged, ID2D1PixelShader
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1(d2D1Factory2.Get(), transformMapperFactory, out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<T>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetConstantBuffer(in shader, d2D1Effect.Get());

        using ComPtr<IWICBitmap> wicBitmap = WICHelper.LoadBitmapFromFile(sourcePath, out uint width, out uint height);
        using ComPtr<ID2D1Bitmap> d2D1BitmapSource = D2D1Helper.CreateD2D1BitmapAndSetAsSource(d2D1DeviceContext.Get(), wicBitmap.Get(), d2D1Effect.Get());
        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), width, height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        // Save the image
        WICHelper.SaveBitmapToFile(destinationPath, width, height, d2D1MappedRect.pitch, d2D1MappedRect.bits);
    }

    /// <summary>
    /// Executes a pixel shader to produce an image.
    /// </summary>
    /// <typeparam name="T">The shader type to execute.</typeparam>
    /// <param name="shader">The shader to run.</param>
    /// <param name="width">The resulting width.</param>
    /// <param name="height">The resulting height.</param>
    /// <param name="transformMapperFactory">A custom <see cref="ID2D1TransformMapper{T}"/> factory for the effect.</param>
    /// <param name="destinationPath">The destination path for the result.</param>
    public static unsafe void ExecutePixelShaderAndCompareResults<T>(
        in T shader,
        Func<ID2D1TransformMapper<T>>? transformMapperFactory,
        int width,
        int height,
        string destinationPath)
        where T : unmanaged, ID2D1PixelShader
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1(d2D1Factory2.Get(), transformMapperFactory, out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<T>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetConstantBuffer(in shader, d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)width, (uint)height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        // Save the image
        WICHelper.SaveBitmapToFile(destinationPath, (uint)width, (uint)height, d2D1MappedRect.pitch, d2D1MappedRect.bits);
    }
}
