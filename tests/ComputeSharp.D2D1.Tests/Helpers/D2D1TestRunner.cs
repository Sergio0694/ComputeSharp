using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Descriptors;
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
    /// <param name="transformMapper">A custom <see cref="D2D1DrawTransformMapper{T}"/> instance for the effect.</param>
    /// <param name="originalFileName">The name of the source image.</param>
    /// <param name="expectedFileName">The name of the expected result image.</param>
    /// <param name="destinationFileName">The name of the destination image to save results to.</param>
    /// <param name="shader">The shader to run.</param>
    /// <param name="threshold">The allowed difference threshold for the normalized delta.</param>
    public static void RunAndCompareShader<T>(
        in T shader,
        D2D1DrawTransformMapper<T>? transformMapper,
        string originalFileName,
        string expectedFileName,
        [CallerMemberName] string destinationFileName = "",
        float threshold = 0.00001f)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
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
            transformMapper,
            originalPath,
            destinationPath);

        // Compare the results
        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, threshold);
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
    /// <param name="threshold">The allowed difference threshold for the normalized delta.</param>
    /// <param name="resourceTextures">The additional resource textures to use to run the shader.</param>
    public static void RunAndCompareShader<T>(
        in T shader,
        int width,
        int height,
        string expectedFileName,
        [CallerMemberName] string destinationFileName = "",
        float threshold = 0.00001f,
        params (int Index, D2D1ResourceTextureManager ResourceTextureManager)[] resourceTextures)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
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
            destinationPath,
            resourceTextures);

        // Compare the results
        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, threshold);
    }

    /// <summary>
    /// Executes a pixel shader to produce an image.
    /// </summary>
    /// <typeparam name="T">The shader type to execute.</typeparam>
    /// <param name="shader">The shader to run.</param>
    /// <param name="transformMapper">A custom <see cref="D2D1DrawTransformMapper{T}"/> instance for the effect.</param>
    /// <param name="sourcePath">The source path for the image to run the shader on.</param>
    /// <param name="destinationPath">The destination path for the result.</param>
    private static unsafe void ExecutePixelShaderAndSaveToFile<T>(
        in T shader,
        D2D1DrawTransformMapper<T>? transformMapper,
        string sourcePath,
        string destinationPath)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<T>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<T>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), in shader);

        if (transformMapper is not null)
        {
            D2D1PixelShaderEffect.SetTransformMapperForD2D1Effect(d2D1Effect.Get(), transformMapper);
        }

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
    /// <param name="transformMapper">A custom <see cref="D2D1DrawTransformMapper{T}"/> instance for the effect.</param>
    /// <param name="destinationPath">The destination path for the result.</param>
    /// <param name="resourceTextures">The additional resource textures to use to run the shader.</param>
    private static unsafe void ExecutePixelShaderAndSaveToFile<T>(
        in T shader,
        D2D1DrawTransformMapper<T>? transformMapper,
        int width,
        int height,
        string destinationPath,
        params (int Index, D2D1ResourceTextureManager ResourceTextureManager)[] resourceTextures)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<T>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<T>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), in shader);

        if (transformMapper is not null)
        {
            D2D1PixelShaderEffect.SetTransformMapperForD2D1Effect(d2D1Effect.Get(), transformMapper);
        }

        foreach ((int index, D2D1ResourceTextureManager resourceTextureManager) in resourceTextures)
        {
            D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager, index);
        }

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)width, (uint)height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        // Save the image
        ImageHelper.SaveBitmapToFile(destinationPath, (uint)width, (uint)height, d2D1MappedRect.pitch, d2D1MappedRect.bits);
    }
}