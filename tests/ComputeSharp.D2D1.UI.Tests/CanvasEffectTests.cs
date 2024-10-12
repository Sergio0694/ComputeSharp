using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using ComputeSharp.D2D1.UI.Tests.Helpers;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp;
#else
using ComputeSharp.D2D1.WinUI;
#endif
using ComputeSharp.SwapChain.Shaders.D2D1;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.Windows;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI;

#nullable enable

namespace ComputeSharp.D2D1.UI.Tests;

[TestClass]
[TestCategory("CanvasEffect")]
public partial class CanvasEffectTests
{
    [TestMethod]
    public void CanvasEffect_IsRealizedAndInvalidatedCorrectly()
    {
        EffectWithNoInputs effect = new();

        try
        {
            using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
            using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
            {
                drawingSession.DrawImage(effect);
            }

            Assert.AreEqual(effect.NumberOfBuildEffectGraphCalls, 1);
            Assert.AreEqual(effect.NumberOfConfigureEffectGraphCalls, 1);
            Assert.AreEqual(effect.NumberOfDisposeCalls, 0);

            effect.Value = 42;

            using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
            using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
            {
                drawingSession.DrawImage(effect);
            }

            Assert.AreEqual(effect.NumberOfBuildEffectGraphCalls, 1);
            Assert.AreEqual(effect.NumberOfConfigureEffectGraphCalls, 2);
            Assert.AreEqual(effect.NumberOfDisposeCalls, 0);

            effect.ValueWithReload = 123;

            using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
            using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
            {
                drawingSession.DrawImage(effect);
            }

            Assert.AreEqual(effect.NumberOfBuildEffectGraphCalls, 2);
            Assert.AreEqual(effect.NumberOfConfigureEffectGraphCalls, 3);
            Assert.AreEqual(effect.NumberOfDisposeCalls, 0);
        }
        finally
        {
            effect.Dispose();

            Assert.AreEqual(effect.NumberOfDisposeCalls, 1);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CanvasEffect_EffectSettingNotRegisteredOutputNode()
    {
        EffectSettingNotRegisteredOutputNode effect = new();

        try
        {
            using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
            using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

            drawingSession.DrawImage(effect);
        }
        catch
        {
            Assert.IsTrue(effect.WasConfigureEffectGraphCalled);

            throw;
        }
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public void CanvasEffect_EffectNotRegisteringAnOutputNode()
    {
        const int COR_E_INVALIDOPERATION = unchecked((int)0x80131509);

        EffectNotRegisteringAnOutputNode effect = new();

        try
        {
            using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
            using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

            drawingSession.DrawImage(effect);
        }
        catch (Exception e)
        {
            // Note: this test should ideally expect and catch an InvalidOperationException, as that's the exception
            // type thrown by CanvasEffect. But it seems that CsWinRT isn't correctly forwarding the exception type
            // in this case, and it ends up only throwing a COMException on the managed side. To work around this in
            // this test, we can just catch a generic Exception and manually verify that the HRESULT is a match.
            // See https://github.com/microsoft/CsWinRT/issues/1393 (and ideally update the test once that's fixed).
            Assert.AreEqual(COR_E_INVALIDOPERATION, e.HResult);
            Assert.IsTrue(effect.WasConfigureEffectGraphCalled);

            throw;
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CanvasEffect_EffectRegisteringNodesMultipleTimes1()
    {
        EffectRegisteringNodesMultipleTimes1 effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CanvasEffect_EffectRegisteringNodesMultipleTimes2()
    {
        EffectRegisteringNodesMultipleTimes2 effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    public void CanvasEffect_EffectRegisteringNullObjects()
    {
        EffectRegisteringNullObjects effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    public void CanvasEffect_EffectConfiguringGraphIncorrectly()
    {
        EffectConfiguringGraphIncorrectly effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    public void CanvasEffect_EffectOnlyUsingAnonymousNodes()
    {
        EffectOnlyUsingAnonymousNodes effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    public void CanvasEffect_EffectUsingEffectGraphInInvalidState()
    {
        EffectUsingEffectGraphInInvalidState effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    public async Task CanvasEffect_PixelShaderSwitchEffect()
    {
        PixelShaderSwitchEffect effect = new();

        async Task TestAsync(string shaderName)
        {
            Color[] pixelColors;

            using CanvasDevice canvasDevice = new();

            // Same logic as in the shader tests
            using (CanvasRenderTarget renderTarget = new(canvasDevice, 1280, 720, 96.0f))
            {
                using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
                {
                    drawingSession.DrawImage(effect);
                }

                pixelColors = renderTarget.GetPixelColors();
            }

            StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/Shaders/{shaderName}.png"));

            using Stream stream = await imageFile.OpenStreamForReadAsync();
            using CanvasBitmap expected = await CanvasBitmap.LoadAsync(canvasDevice, stream.AsRandomAccessStream());
            using CanvasBitmap actual = CanvasBitmap.CreateFromColors(canvasDevice, pixelColors, 1280, 720);

            TolerantImageComparer.AssertEqual(expected, actual, threshold: 0.00001f);
        }

        // Every time the effect is re-drawn, a different shader is set as the output node
        await TestAsync(nameof(HelloWorld));
        await TestAsync(nameof(ColorfulInfinity));
        await TestAsync(nameof(FractalTiling));
        await TestAsync(nameof(Octagrams));
        await TestAsync(nameof(ProteanClouds));
    }

    [TestMethod]
    public void CanvasEffect_EffectTestingDisposal()
    {
        EffectTestingDisposal effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        // Force creating the graph (the drawing fails because the effects are dummies)
        try
        {
            drawingSession.DrawImage(effect);
        }
        catch (Exception e) when (e.HResult == E.E_FAIL)
        {
        }

        Assert.AreEqual(1, effect.Effects1.Count);
        Assert.AreEqual(1, effect.Effects2.Count);
        Assert.AreEqual(1, effect.Effects3.Count);
        Assert.AreEqual(0, effect.Effects1[0].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects2[0].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects3[0].NumberOfDisposeCalls);

        effect.InvalidateCreation();

        // Effects aren't disposed yet until we redraw
        Assert.AreEqual(1, effect.Effects1.Count);
        Assert.AreEqual(1, effect.Effects2.Count);
        Assert.AreEqual(1, effect.Effects3.Count);
        Assert.AreEqual(0, effect.Effects1[0].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects2[0].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects3[0].NumberOfDisposeCalls);

        // Redraw, which builds a new effect graph and disposes the old one
        try
        {
            drawingSession.DrawImage(effect);
        }
        catch (Exception e) when (e.HResult == E.E_FAIL)
        {
        }

        Assert.AreEqual(2, effect.Effects1.Count);
        Assert.AreEqual(2, effect.Effects2.Count);
        Assert.AreEqual(2, effect.Effects3.Count);
        Assert.AreEqual(1, effect.Effects1[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects2[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects3[0].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects1[1].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects2[1].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects3[1].NumberOfDisposeCalls);

        // Redrawing now doesn't change anything
        try
        {
            drawingSession.DrawImage(effect);
        }
        catch (Exception e) when (e.HResult == E.E_FAIL)
        {
        }

        Assert.AreEqual(2, effect.Effects1.Count);
        Assert.AreEqual(2, effect.Effects2.Count);
        Assert.AreEqual(2, effect.Effects3.Count);
        Assert.AreEqual(1, effect.Effects1[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects2[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects3[0].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects1[1].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects2[1].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects3[1].NumberOfDisposeCalls);

        effect.InvalidateUpdate();

        // Invalidating with update mode also doesn't do anything here
        Assert.AreEqual(2, effect.Effects1.Count);
        Assert.AreEqual(2, effect.Effects2.Count);
        Assert.AreEqual(2, effect.Effects3.Count);
        Assert.AreEqual(1, effect.Effects1[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects2[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects3[0].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects1[1].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects2[1].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects3[1].NumberOfDisposeCalls);

        // Redraw, and also verify the disposal state wasn't changed
        try
        {
            drawingSession.DrawImage(effect);
        }
        catch (Exception e) when (e.HResult == E.E_FAIL)
        {
        }

        Assert.AreEqual(2, effect.Effects1.Count);
        Assert.AreEqual(2, effect.Effects2.Count);
        Assert.AreEqual(2, effect.Effects3.Count);
        Assert.AreEqual(1, effect.Effects1[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects2[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects3[0].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects1[1].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects2[1].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects3[1].NumberOfDisposeCalls);

        // Verify that invalidating with update after creation is ignored
        effect.InvalidateCreation();
        effect.InvalidateUpdate();

        Assert.AreEqual(2, effect.Effects1.Count);
        Assert.AreEqual(2, effect.Effects2.Count);
        Assert.AreEqual(2, effect.Effects3.Count);
        Assert.AreEqual(1, effect.Effects1[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects2[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects3[0].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects1[1].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects2[1].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects3[1].NumberOfDisposeCalls);

        // Redraw, disposing the old graph once again
        try
        {
            drawingSession.DrawImage(effect);
        }
        catch (Exception e) when (e.HResult == E.E_FAIL)
        {
        }

        Assert.AreEqual(3, effect.Effects1.Count);
        Assert.AreEqual(3, effect.Effects2.Count);
        Assert.AreEqual(3, effect.Effects3.Count);
        Assert.AreEqual(1, effect.Effects1[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects2[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects3[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects1[1].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects2[1].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects3[1].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects1[2].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects2[2].NumberOfDisposeCalls);
        Assert.AreEqual(0, effect.Effects3[2].NumberOfDisposeCalls);

        // Finally dispose the effect
        effect.Dispose();

        Assert.AreEqual(3, effect.Effects1.Count);
        Assert.AreEqual(3, effect.Effects2.Count);
        Assert.AreEqual(3, effect.Effects3.Count);
        Assert.AreEqual(1, effect.Effects1[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects2[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects3[0].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects1[1].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects2[1].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects3[1].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects1[2].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects2[2].NumberOfDisposeCalls);
        Assert.AreEqual(1, effect.Effects3[2].NumberOfDisposeCalls);
    }

    private sealed class EffectWithNoInputs : CanvasEffect
    {
        private static readonly CanvasEffectNode<PixelShaderEffect<ShaderWithNoInputs>> Effect = new();

        private int value;

        public int Value
        {
            get => this.value;
            set => SetAndInvalidateEffectGraph(ref this.value, value);
        }

        public int ValueWithReload
        {
            get => this.value;
            set => SetAndInvalidateEffectGraph(ref this.value, value, CanvasEffectInvalidationType.Creation);
        }

        public int NumberOfBuildEffectGraphCalls { get; private set; }

        public int NumberOfConfigureEffectGraphCalls { get; private set; }

        public int NumberOfDisposeCalls { get; private set; }

        protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            NumberOfBuildEffectGraphCalls++;

            effectGraph.RegisterOutputNode(Effect, new PixelShaderEffect<ShaderWithNoInputs>());
        }

        protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
            NumberOfConfigureEffectGraphCalls++;

            effectGraph.GetNode(Effect).ConstantBuffer = new ShaderWithNoInputs(this.value);

            // Also test the non-generic overload
            Assert.IsTrue(effectGraph.GetNode((ICanvasEffectNode)Effect) is PixelShaderEffect<ShaderWithNoInputs>);
        }

        protected override void Dispose(bool disposing)
        {
            NumberOfDisposeCalls++;

            base.Dispose(disposing);
        }
    }

    private sealed class EffectSettingNotRegisteredOutputNode : CanvasEffect
    {
        public bool WasConfigureEffectGraphCalled { get; private set; }

        protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            effectGraph.RegisterOutputNode(new CanvasEffectNode<ColorSourceEffect>(), new ColorSourceEffect());
            effectGraph.RegisterOutputNode(new CanvasEffectNode<ColorSourceEffect>(), new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
            WasConfigureEffectGraphCalled = true;

            effectGraph.SetOutputNode(new CanvasEffectNode<ColorSourceEffect>());
        }
    }

    private sealed class EffectNotRegisteringAnOutputNode : CanvasEffect
    {
        public bool WasConfigureEffectGraphCalled { get; private set; }

        protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            effectGraph.RegisterNode(new ColorSourceEffect());
            effectGraph.RegisterNode(new CanvasEffectNode<ColorSourceEffect>(), new ColorSourceEffect());
            effectGraph.RegisterNode(new CanvasEffectNode<ColorSourceEffect>(), new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
            WasConfigureEffectGraphCalled = true;
        }
    }

    private sealed class EffectRegisteringNodesMultipleTimes1 : CanvasEffect
    {
        protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            CanvasEffectNode<ColorSourceEffect> node = new();

            effectGraph.RegisterNode(node, new ColorSourceEffect());
            effectGraph.RegisterOutputNode(node, new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
            Assert.Fail();
        }
    }

    private sealed class EffectRegisteringNodesMultipleTimes2 : CanvasEffect
    {
        protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            CanvasEffectNode<ColorSourceEffect> node = new();

            effectGraph.RegisterNode(node, new ColorSourceEffect());
            effectGraph.RegisterNode(node, new ColorSourceEffect());
            effectGraph.RegisterOutputNode(new CanvasEffectNode<ColorSourceEffect>(), new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
            Assert.Fail();
        }
    }

    private sealed class EffectRegisteringNullObjects : CanvasEffect
    {
        protected override unsafe void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            // Verify that if the effect graph is invalid, arguments are validated first
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(CanvasEffectGraph).RegisterNode(null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(CanvasEffectGraph).RegisterNode(null!, new ColorSourceEffect()));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(CanvasEffectGraph).RegisterNode(new CanvasEffectNode<ColorSourceEffect>(), null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(CanvasEffectGraph).RegisterNode(null!, (ColorSourceEffect?)null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(CanvasEffectGraph).RegisterOutputNode(null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(CanvasEffectGraph).RegisterOutputNode(null!, new ColorSourceEffect()));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(CanvasEffectGraph).RegisterOutputNode(new CanvasEffectNode<ColorSourceEffect>(), null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(CanvasEffectGraph).RegisterOutputNode(null!, (ColorSourceEffect)null!));

            void* ptr = &effectGraph;

            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(CanvasEffectGraph*)ptr).RegisterNode(null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(CanvasEffectGraph*)ptr).RegisterNode(null!, new ColorSourceEffect()));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(CanvasEffectGraph*)ptr).RegisterNode(new CanvasEffectNode<ColorSourceEffect>(), null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(CanvasEffectGraph*)ptr).RegisterNode(null!, (ColorSourceEffect?)null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(CanvasEffectGraph*)ptr).RegisterOutputNode(null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(CanvasEffectGraph*)ptr).RegisterOutputNode(null!, new ColorSourceEffect()));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(CanvasEffectGraph*)ptr).RegisterOutputNode(new CanvasEffectNode<ColorSourceEffect>(), null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(CanvasEffectGraph*)ptr).RegisterOutputNode(null!, (ColorSourceEffect)null!));

            effectGraph.RegisterOutputNode(new CanvasEffectNode<ColorSourceEffect>(), new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
        }
    }

    private sealed class EffectConfiguringGraphIncorrectly : CanvasEffect
    {
        private static readonly CanvasEffectNode<ColorSourceEffect> EffectNode1 = new();
        private static readonly CanvasEffectNode<ColorSourceEffect> EffectNode2 = new();
        private static readonly CanvasEffectNode<ColorSourceEffect> EffectNode3 = new();
#pragma warning disable CA2213
        private readonly ColorSourceEffect effect1 = new();
        private readonly ColorSourceEffect effect2 = new();
        private readonly ColorSourceEffect effect3 = new();
#pragma warning restore CA2213

        protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            effectGraph.RegisterNode(EffectNode1, this.effect1);
            effectGraph.RegisterNode(EffectNode2, this.effect2);
            effectGraph.RegisterOutputNode(EffectNode3, this.effect3);
        }

        protected override unsafe void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
            void* ptr = &effectGraph;

            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(CanvasEffectGraph*)ptr).GetNode((CanvasEffectNode<ColorSourceEffect>)null!));
            _ = Assert.ThrowsException<ArgumentException>(() => (*(CanvasEffectGraph*)ptr).GetNode(new CanvasEffectNode<ColorSourceEffect>()));
            _ = Assert.ThrowsException<InvalidOperationException>(() => (*(CanvasEffectGraph*)ptr).RegisterNode(new CanvasEffectNode<ColorSourceEffect>(), new ColorSourceEffect()));
            _ = Assert.ThrowsException<InvalidOperationException>(() => (*(CanvasEffectGraph*)ptr).RegisterOutputNode(new CanvasEffectNode<ColorSourceEffect>(), new ColorSourceEffect()));

            Assert.AreSame(effectGraph.GetNode(EffectNode1), this.effect1);
            Assert.AreSame(effectGraph.GetNode(EffectNode2), this.effect2);
            Assert.AreSame(effectGraph.GetNode(EffectNode3), this.effect3);
        }
    }

    private sealed class EffectOnlyUsingAnonymousNodes : CanvasEffect
    {
        protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            effectGraph.RegisterNode(new ColorSourceEffect());
            effectGraph.RegisterNode(new ColorSourceEffect());
            effectGraph.RegisterOutputNode(new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
        }
    }

    private sealed class EffectUsingEffectGraphInInvalidState : CanvasEffect
    {
        protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            _ = Assert.ThrowsException<InvalidOperationException>(() => default(CanvasEffectGraph).RegisterNode(new ColorSourceEffect()));
            _ = Assert.ThrowsException<InvalidOperationException>(() => default(CanvasEffectGraph).RegisterNode(new CanvasEffectNode<ColorSourceEffect>(), new ColorSourceEffect()));
            _ = Assert.ThrowsException<InvalidOperationException>(() => default(CanvasEffectGraph).RegisterOutputNode(new ColorSourceEffect()));
            _ = Assert.ThrowsException<InvalidOperationException>(() => default(CanvasEffectGraph).RegisterOutputNode(new CanvasEffectNode<ColorSourceEffect>(), new ColorSourceEffect()));

            effectGraph.RegisterOutputNode(new CanvasEffectNode<ColorSourceEffect>(), new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
            _ = Assert.ThrowsException<InvalidOperationException>(() => default(CanvasEffectGraph).GetNode(new CanvasEffectNode<ColorSourceEffect>()));
        }
    }

    private sealed class PixelShaderSwitchEffect : CanvasEffect
    {
        private static readonly CanvasEffectNode<PixelShaderEffect<HelloWorld>> HelloWorldNode = new();
        private static readonly CanvasEffectNode<PixelShaderEffect<ColorfulInfinity>> ColorfulInfinityNode = new();
        private static readonly CanvasEffectNode<PixelShaderEffect<FractalTiling>> FractalTilingNode = new();
        private static readonly CanvasEffectNode<PixelShaderEffect<Octagrams>> OctagramsNode = new();
        private static readonly CanvasEffectNode<PixelShaderEffect<ProteanClouds>> ProteanCloudsNode = new();

        private int step;

        protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            effectGraph.RegisterNode(HelloWorldNode, new PixelShaderEffect<HelloWorld> { ConstantBuffer = new HelloWorld(0, new int2(1280, 720)) });
            effectGraph.RegisterNode(ColorfulInfinityNode, new PixelShaderEffect<ColorfulInfinity> { ConstantBuffer = new ColorfulInfinity(0, new int2(1280, 720)) });
            effectGraph.RegisterNode(FractalTilingNode, new PixelShaderEffect<FractalTiling> { ConstantBuffer = new FractalTiling(0, new int2(1280, 720)) });
            effectGraph.RegisterNode(OctagramsNode, new PixelShaderEffect<Octagrams> { ConstantBuffer = new Octagrams(0, new int2(1280, 720)) });
            effectGraph.RegisterNode(ProteanCloudsNode, new PixelShaderEffect<ProteanClouds> { ConstantBuffer = new ProteanClouds(0, new int2(1280, 720)) });
            effectGraph.SetOutputNode(HelloWorldNode);
        }

        protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
            switch (this.step++)
            {
                case 0: break;
                case 1:
                    effectGraph.SetOutputNode(ColorfulInfinityNode);
                    break;
                case 2:
                    effectGraph.SetOutputNode(FractalTilingNode);
                    break;
                case 3 or 4:
                    // Test the overload which is non-generic, allowing ternary expressions too
                    effectGraph.SetOutputNode(this.step == 4 ? OctagramsNode : ProteanCloudsNode);
                    break;
                default:
                    Assert.Fail();
                    break;
            }
        }
    }

    private sealed class EffectTestingDisposal : CanvasEffect
    {
        private static readonly CanvasEffectNode<DummyCanvasImageTrackingDisposal> EffectNode1 = new();
        private static readonly CanvasEffectNode<DummyCanvasImageTrackingDisposal> EffectNode2 = new();
        private static readonly CanvasEffectNode<DummyCanvasImageTrackingDisposal> EffectNode3 = new();
        public readonly List<DummyCanvasImageTrackingDisposal> Effects1 = new();
        public readonly List<DummyCanvasImageTrackingDisposal> Effects2 = new();
        public readonly List<DummyCanvasImageTrackingDisposal> Effects3 = new();

        public void InvalidateCreation()
        {
            InvalidateEffectGraph(CanvasEffectInvalidationType.Creation);
        }

        public void InvalidateUpdate()
        {
            InvalidateEffectGraph(CanvasEffectInvalidationType.Update);
        }

        protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
        {
            this.Effects1.Add(new DummyCanvasImageTrackingDisposal());
            this.Effects2.Add(new DummyCanvasImageTrackingDisposal());
            this.Effects3.Add(new DummyCanvasImageTrackingDisposal());

            effectGraph.RegisterNode(EffectNode1, this.Effects1[^1]);
            effectGraph.RegisterNode(EffectNode2, this.Effects2[^1]);
            effectGraph.RegisterOutputNode(EffectNode3, this.Effects3[^1]);
        }

        protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
        {
        }
    }

    private sealed class DummyCanvasImageTrackingDisposal : ICanvasImage
    {
        public int NumberOfDisposeCalls { get; private set; }

        public Rect GetBounds(ICanvasResourceCreator resourceCreator)
        {
            Assert.Fail();

            return default;
        }

        public Rect GetBounds(ICanvasResourceCreator resourceCreator, Matrix3x2 transform)
        {
            Assert.Fail();

            return default;
        }

        public void Dispose()
        {
            NumberOfDisposeCalls++;
        }
    }

    [D2DInputCount(0)]
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    private partial struct ShaderWithNoInputs : ID2D1PixelShader
    {
        public int Value;

        public float4 Execute()
        {
            return default;
        }
    }
}
