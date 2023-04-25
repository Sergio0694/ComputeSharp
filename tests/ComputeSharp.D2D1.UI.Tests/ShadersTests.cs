using System;
using System.IO;
using System.Threading.Tasks;
using ComputeSharp.D2D1.UI.Tests.Helpers;
using ComputeSharp.D2D1.Uwp;
using ComputeSharp.SwapChain.Shaders.D2D1;
using Microsoft.Graphics.Canvas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.Storage;
using Windows.UI;

namespace ComputeSharp.D2D1.UI.Tests;

[TestClass]
[TestCategory("Shaders")]
public class ShadersTests
{
    [TestMethod]
    public async Task HelloWorld()
    {
        await RunTestAsync<HelloWorld>();
    }

    [TestMethod]
    public async Task ColorfulInfinity()
    {
        await RunTestAsync<ColorfulInfinity>();
    }

    [TestMethod]
    public async Task FractalTiling()
    {
        await RunTestAsync<FractalTiling>();
    }

    [TestMethod]
    public async Task MengerJourney()
    {
        await RunTestAsync<MengerJourney>(0.000011f);
    }

    [TestMethod]
    public async Task Octagrams()
    {
        await RunTestAsync<Octagrams>();
    }

    [TestMethod]
    public async Task ProteanClouds()
    {
        await RunTestAsync<ProteanClouds>();
    }

    [TestMethod]
    public async Task TerracedHills()
    {
        await RunTestAsync<TerracedHills>(0.000026f);
    }

    private static async Task RunTestAsync<T>(float threshold = 0.00001f)
        where T : unmanaged, ID2D1PixelShader
    {
        T shader = (T)Activator.CreateInstance(typeof(T), 0f, new int2(1280, 720))!;
        PixelShaderEffect<T> effect = new() { ConstantBuffer = shader };

        Color[] pixelColors;

        using CanvasDevice canvasDevice = new();

        // Compute the resulting image
        using (CanvasRenderTarget renderTarget = new(canvasDevice, 1280, 720, 96.0f))
        {
            // Draw the shader on the render target
            using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
            {
                drawingSession.DrawImage(effect);
            }

            // Get the BGRA32 pixel data from the render target
            pixelColors = renderTarget.GetPixelColors();
        }

        StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/Shaders/{typeof(T).Name}.png"));

        // Retrieve the expected image
        using Stream stream = await imageFile.OpenStreamForReadAsync();
        using CanvasBitmap expected = await CanvasBitmap.LoadAsync(canvasDevice, stream.AsRandomAccessStream());
        using CanvasBitmap actual = CanvasBitmap.CreateFromColors(canvasDevice, pixelColors, 1280, 720);

        TolerantImageComparer.AssertEqual(expected, actual, threshold);
    }
}