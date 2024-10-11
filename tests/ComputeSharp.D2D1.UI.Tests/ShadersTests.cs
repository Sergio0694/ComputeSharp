using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.D2D1.UI.Tests.Helpers;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp;
#else
using ComputeSharp.D2D1.WinUI;
#endif
using ComputeSharp.SwapChain.Shaders.D2D1;
using Microsoft.Graphics.Canvas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.Storage;
using Windows.UI;

#nullable enable

namespace ComputeSharp.D2D1.UI.Tests;

[TestClass]
[TestCategory("Shaders")]
public class ShadersTests
{
    [TestMethod]
    [DataRow(WrapperType.PixelShaderEffect)]
    [DataRow(WrapperType.CanvasEffect)]
    public async Task HelloWorld(WrapperType wrapperType)
    {
        await RunTestAsync<HelloWorld>(wrapperType);
    }

    [TestMethod]
    [DataRow(WrapperType.PixelShaderEffect)]
    [DataRow(WrapperType.CanvasEffect)]
    public async Task ColorfulInfinity(WrapperType wrapperType)
    {
        await RunTestAsync<ColorfulInfinity>(wrapperType);
    }

    [TestMethod]
    [DataRow(WrapperType.PixelShaderEffect)]
    [DataRow(WrapperType.CanvasEffect)]
    public async Task FractalTiling(WrapperType wrapperType)
    {
        await RunTestAsync<FractalTiling>(wrapperType);
    }

    [TestMethod]
    [DataRow(WrapperType.PixelShaderEffect)]
    [DataRow(WrapperType.CanvasEffect)]
    public async Task MengerJourney(WrapperType wrapperType)
    {
        await RunTestAsync<MengerJourney>(wrapperType, 0.000011f);
    }

    [TestMethod]
    [DataRow(WrapperType.PixelShaderEffect)]
    [DataRow(WrapperType.CanvasEffect)]
    public async Task Octagrams(WrapperType wrapperType)
    {
        await RunTestAsync<Octagrams>(wrapperType);
    }

    [TestMethod]
    [DataRow(WrapperType.PixelShaderEffect)]
    [DataRow(WrapperType.CanvasEffect)]
    public async Task ProteanClouds(WrapperType wrapperType)
    {
        await RunTestAsync<ProteanClouds>(wrapperType);
    }

    [TestMethod]
    [DataRow(WrapperType.PixelShaderEffect)]
    [DataRow(WrapperType.CanvasEffect)]
    public async Task TerracedHills(WrapperType wrapperType)
    {
        await RunTestAsync<TerracedHills>(wrapperType, 0.000026f);
    }

    private static async Task RunTestAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(WrapperType wrapperType, float threshold = 0.00001f)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        T shader = (T)Activator.CreateInstance(typeof(T), 0f, new int2(1280, 720))!;

        // Either create the effect directly, or a canvas effect
        using ICanvasImage canvasImage = wrapperType switch
        {
            WrapperType.PixelShaderEffect => new PixelShaderEffect<T>() { ConstantBuffer = shader },
            _ => new TestCanvasEffect<T> { ConstantBuffer = shader }
        };

        Color[] pixelColors;

        using CanvasDevice canvasDevice = new();

        // Compute the resulting image
        using (CanvasRenderTarget renderTarget = new(canvasDevice, 1280, 720, 96.0f))
        {
            // Draw the shader on the render target
            using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
            {
                drawingSession.DrawImage(canvasImage);
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

    private sealed class TestCanvasEffect<T> : CanvasEffect
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        private static readonly CanvasEffectNode<PixelShaderEffect<T>> Effect = new();

        private T constantBuffer;

        public T ConstantBuffer
        {
            get => this.constantBuffer;
            set => SetAndInvalidateEffectGraph(ref this.constantBuffer, value);
        }

        protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            effectGraph.RegisterOutputNode(Effect, new PixelShaderEffect<T>());
        }

        protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
            effectGraph.GetNode(Effect).ConstantBuffer = this.constantBuffer;
        }
    }

    public enum WrapperType
    {
        PixelShaderEffect,
        CanvasEffect
    }
}