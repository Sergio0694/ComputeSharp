using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.Tests.Helpers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Tests.Helpers;

/// <summary>
/// A test helper for D2D1 pixel shaders.
/// </summary>
internal static class D2D1TestRunner
{
    /// <summary>
    /// Executes a pixel shader and compares the expected results.
    /// </summary>
    /// <typeparam name="T">The type of pixel shader to run.</typeparam>
    /// <param name="transformMapperFactory">A custom <see cref="ID2D1TransformMapper{T}"/> factory for the effect.</param>
    /// <param name="originalFileName">The name of the source image.</param>
    /// <param name="expectedFileName">The name of the expected result image.</param>
    /// <param name="destinationFileName">The name of the destination image to save results to.</param>
    /// <param name="shader">The shader to run.</param>
    public static void RunAndCompareShader<T>(
        in T shader,
        ID2D1TransformMapperFactory<T>? transformMapperFactory,
        string originalFileName,
        string expectedFileName,
        [CallerMemberName] string destinationFileName = "")
        where T : unmanaged, ID2D1PixelShader
    {
        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");
        string temporaryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "temp");

        _ = Directory.CreateDirectory(temporaryPath);

        string originalPath = Path.Combine(assetsPath, originalFileName);
        string expectedPath = Path.Combine(assetsPath, expectedFileName);
        string destinationPath = Path.Combine(temporaryPath, $"{destinationFileName}.png");

        // Run the shader
        ExecutePixelShaderAndSaveToFile(
            in shader,
            transformMapperFactory,
            originalPath,
            destinationPath);

        // Compare the results
        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
    }

    /// <summary>
    /// Executes a pixel shader and compares the expected results.
    /// </summary>
    /// <typeparam name="T">The type of pixel shader to run.</typeparam>
    /// <param name="width">The resulting width.</param>
    /// <param name="height">The resulting height.</param>
    /// <param name="expectedFileName">The name of the expected result image.</param>
    /// <param name="destinationFileName">The name of the destination image to save results to.</param>
    /// <param name="shader">The shader to run.</param>
    public static void RunAndCompareShader<T>(
        in T shader,
        int width,
        int height,
        string expectedFileName,
        [CallerMemberName] string destinationFileName = "")
        where T : unmanaged, ID2D1PixelShader
    {
        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");
        string temporaryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "temp");

        _ = Directory.CreateDirectory(temporaryPath);

        string expectedPath = Path.Combine(assetsPath, expectedFileName);
        string destinationPath = Path.Combine(temporaryPath, $"{destinationFileName}.png");

        // Run the shader
        ExecutePixelShaderAndSaveToFile(
            in shader,
            null,
            width,
            height,
            destinationPath);

        // Compare the results
        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
    }

    /// <summary>
    /// Executes a pixel shader to produce an image.
    /// </summary>
    /// <typeparam name="T">The shader type to execute.</typeparam>
    /// <param name="shader">The shader to run.</param>
    /// <param name="transformMapperFactory">A custom <see cref="ID2D1TransformMapper{T}"/> factory for the effect.</param>
    /// <param name="sourcePath">The source path for the image to run the shader on.</param>
    /// <param name="destinationPath">The destination path for the result.</param>
    private static unsafe void ExecutePixelShaderAndSaveToFile<T>(
        in T shader,
        ID2D1TransformMapperFactory<T>? transformMapperFactory,
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

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(in shader, d2D1Effect.Get());

        ReadOnlyMemory<byte> pixels = ImageHelper.LoadBitmapFromFile(sourcePath, out uint width, out uint height);

        using ComPtr<ID2D1Bitmap> d2D1BitmapSource = D2D1Helper.CreateD2D1BitmapAndSetAsSource(d2D1DeviceContext.Get(), pixels, width, height, d2D1Effect.Get());
        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), width, height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        // Save the image
        ImageHelper.SaveBitmapToFile(destinationPath, width, height, d2D1MappedRect.pitch, d2D1MappedRect.bits);
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
    private static unsafe void ExecutePixelShaderAndSaveToFile<T>(
        in T shader,
        ID2D1TransformMapperFactory<T>? transformMapperFactory,
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

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(in shader, d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)width, (uint)height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        // Save the image
        ImageHelper.SaveBitmapToFile(destinationPath, (uint)width, (uint)height, d2D1MappedRect.pitch, d2D1MappedRect.bits);
    }
}
