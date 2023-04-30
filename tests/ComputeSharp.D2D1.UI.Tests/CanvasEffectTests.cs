using System;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp;
#else
using ComputeSharp.D2D1.WinUI;
#endif
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#nullable enable

namespace ComputeSharp.D2D1.UI.Tests;

[TestClass]
[TestCategory("CanvasEffect")]
public partial class CanvasEffectTests
{
    [TestMethod]
    public unsafe void CanvasEffect_IsRealizedAndInvalidatedCorrectly()
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
    [ExpectedException(typeof(InvalidOperationException))]
    public unsafe void CanvasEffect_EffectRegisteringMultipleOutputNodes()
    {
        EffectRegisteringMultipleOutputNodes effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public unsafe void CanvasEffect_EffectNotRegisteringAnOutputNode()
    {
        EffectNotRegisteringAnOutputNode effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public unsafe void CanvasEffect_EffectRegisteringNodesMultipleTimes1()
    {
        EffectRegisteringNodesMultipleTimes1 effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public unsafe void CanvasEffect_EffectRegisteringNodesMultipleTimes2()
    {
        EffectRegisteringNodesMultipleTimes2 effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    public unsafe void CanvasEffect_EffectRegisteringNullObjects()
    {
        EffectRegisteringNullObjects effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    public unsafe void CanvasEffect_EffectConfiguringGraphIncorrectly()
    {
        EffectConfiguringGraphIncorrectly effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    public unsafe void CanvasEffect_EffectOnlyUsingAnonymousNodes()
    {
        EffectOnlyUsingAnonymousNodes effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    [TestMethod]
    public unsafe void CanvasEffect_EffectUsingEffectGraphInInvalidState()
    {
        EffectUsingEffectGraphInInvalidState effect = new();

        using CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f);
        using CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession();

        drawingSession.DrawImage(effect);
    }

    private sealed class EffectWithNoInputs : CanvasEffect
    {
        private static readonly EffectNode<PixelShaderEffect<ShaderWithNoInputs>> Effect = new();

        private int value;

        public int Value
        {
            get => this.value;
            set => SetAndInvalidateEffectGraph(ref this.value, value);
        }

        public int ValueWithReload
        {
            get => this.value;
            set => SetAndInvalidateEffectGraph(ref this.value, value, InvalidationType.Creation);
        }

        public int NumberOfBuildEffectGraphCalls { get; private set; }

        public int NumberOfConfigureEffectGraphCalls { get; private set; }

        public int NumberOfDisposeCalls { get; private set; }

        protected override void BuildEffectGraph(EffectGraph effectGraph)
        {
            NumberOfBuildEffectGraphCalls++;

            effectGraph.RegisterAndSetOutputNode(Effect, new PixelShaderEffect<ShaderWithNoInputs>());
        }

        protected override void ConfigureEffectGraph(EffectGraph effectGraph)
        {
            NumberOfConfigureEffectGraphCalls++;

            effectGraph.GetNode(Effect).ConstantBuffer = new ShaderWithNoInputs(this.value);
        }

        protected override void Dispose(bool disposing)
        {
            NumberOfDisposeCalls++;

            base.Dispose(disposing);
        }
    }

    private sealed class EffectRegisteringMultipleOutputNodes : CanvasEffect
    {
        protected override void BuildEffectGraph(EffectGraph effectGraph)
        {
            effectGraph.RegisterAndSetOutputNode(new EffectNode<ColorSourceEffect>(), new ColorSourceEffect());
            effectGraph.RegisterAndSetOutputNode(new EffectNode<ColorSourceEffect>(), new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(EffectGraph effectGraph)
        {
            Assert.Fail();
        }
    }

    private sealed class EffectNotRegisteringAnOutputNode : CanvasEffect
    {
        protected override void BuildEffectGraph(EffectGraph effectGraph)
        {
            effectGraph.RegisterNode(new ColorSourceEffect());
            effectGraph.RegisterNode(new EffectNode<ColorSourceEffect>(), new ColorSourceEffect());
            effectGraph.RegisterNode(new EffectNode<ColorSourceEffect>(), new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(EffectGraph effectGraph)
        {
            Assert.Fail();
        }
    }

    private sealed class EffectRegisteringNodesMultipleTimes1 : CanvasEffect
    {
        protected override void BuildEffectGraph(EffectGraph effectGraph)
        {
            EffectNode<ColorSourceEffect> node = new();

            effectGraph.RegisterNode(node, new ColorSourceEffect());
            effectGraph.RegisterAndSetOutputNode(node, new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(EffectGraph effectGraph)
        {
            Assert.Fail();
        }
    }

    private sealed class EffectRegisteringNodesMultipleTimes2 : CanvasEffect
    {
        protected override void BuildEffectGraph(EffectGraph effectGraph)
        {
            EffectNode<ColorSourceEffect> node = new();

            effectGraph.RegisterNode(node, new ColorSourceEffect());
            effectGraph.RegisterNode(node, new ColorSourceEffect());
            effectGraph.RegisterAndSetOutputNode(new EffectNode<ColorSourceEffect>(), new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(EffectGraph effectGraph)
        {
            Assert.Fail();
        }
    }

    private sealed class EffectRegisteringNullObjects : CanvasEffect
    {
        protected override unsafe void BuildEffectGraph(EffectGraph effectGraph)
        {
            // Verify that if the effect graph is invalid, arguments are validated first
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(EffectGraph).RegisterNode(null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(EffectGraph).RegisterNode(null!, new ColorSourceEffect()));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(EffectGraph).RegisterNode(new EffectNode<ColorSourceEffect>(), null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(EffectGraph).RegisterNode(null!, (ColorSourceEffect?)null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(EffectGraph).RegisterAndSetOutputNode(null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(EffectGraph).RegisterAndSetOutputNode(null!, new ColorSourceEffect()));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(EffectGraph).RegisterAndSetOutputNode(new EffectNode<ColorSourceEffect>(), null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => default(EffectGraph).RegisterAndSetOutputNode(null!, (ColorSourceEffect)null!));

            void* ptr = &effectGraph;

            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(EffectGraph*)ptr).RegisterNode(null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(EffectGraph*)ptr).RegisterNode(null!, new ColorSourceEffect()));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(EffectGraph*)ptr).RegisterNode(new EffectNode<ColorSourceEffect>(), null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(EffectGraph*)ptr).RegisterNode(null!, (ColorSourceEffect?)null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(EffectGraph*)ptr).RegisterAndSetOutputNode(null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(EffectGraph*)ptr).RegisterAndSetOutputNode(null!, new ColorSourceEffect()));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(EffectGraph*)ptr).RegisterAndSetOutputNode(new EffectNode<ColorSourceEffect>(), null!));
            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(EffectGraph*)ptr).RegisterAndSetOutputNode(null!, (ColorSourceEffect)null!));

            effectGraph.RegisterAndSetOutputNode(new EffectNode<ColorSourceEffect>(), new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(EffectGraph effectGraph)
        {
        }
    }

    private sealed class EffectConfiguringGraphIncorrectly : CanvasEffect
    {
        private static readonly EffectNode<ColorSourceEffect> EffectNode1 = new();
        private static readonly EffectNode<ColorSourceEffect> EffectNode2 = new();
        private static readonly EffectNode<ColorSourceEffect> EffectNode3 = new();
#pragma warning disable CA2213
        private readonly ColorSourceEffect effect1 = new();
        private readonly ColorSourceEffect effect2 = new();
        private readonly ColorSourceEffect effect3 = new();
#pragma warning restore CA2213

        protected override void BuildEffectGraph(EffectGraph effectGraph)
        {
            effectGraph.RegisterNode(EffectNode1, this.effect1);
            effectGraph.RegisterNode(EffectNode2, this.effect2);
            effectGraph.RegisterAndSetOutputNode(EffectNode3, this.effect3);
        }

        protected override unsafe void ConfigureEffectGraph(EffectGraph effectGraph)
        {
            void* ptr = &effectGraph;

            _ = Assert.ThrowsException<ArgumentNullException>(() => (*(EffectGraph*)ptr).GetNode((EffectNode<ColorSourceEffect>)null!));
            _ = Assert.ThrowsException<ArgumentException>(() => (*(EffectGraph*)ptr).GetNode(new EffectNode<ColorSourceEffect>()));
            _ = Assert.ThrowsException<InvalidOperationException>(() => (*(EffectGraph*)ptr).RegisterNode(new EffectNode<ColorSourceEffect>(), new ColorSourceEffect()));
            _ = Assert.ThrowsException<InvalidOperationException>(() => (*(EffectGraph*)ptr).RegisterAndSetOutputNode(new EffectNode<ColorSourceEffect>(), new ColorSourceEffect()));

            Assert.AreSame(effectGraph.GetNode(EffectNode1), this.effect1);
            Assert.AreSame(effectGraph.GetNode(EffectNode2), this.effect2);
            Assert.AreSame(effectGraph.GetNode(EffectNode3), this.effect3);
        }
    }

    private sealed class EffectOnlyUsingAnonymousNodes : CanvasEffect
    {
        protected override void BuildEffectGraph(EffectGraph effectGraph)
        {
            effectGraph.RegisterNode(new ColorSourceEffect());
            effectGraph.RegisterNode(new ColorSourceEffect());
            effectGraph.RegisterAndSetOutputNode(new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(EffectGraph effectGraph)
        {
        }
    }

    private sealed class EffectUsingEffectGraphInInvalidState : CanvasEffect
    {
        protected override void BuildEffectGraph(EffectGraph effectGraph)
        {
            _ = Assert.ThrowsException<InvalidOperationException>(() => default(EffectGraph).RegisterNode(new ColorSourceEffect()));
            _ = Assert.ThrowsException<InvalidOperationException>(() => default(EffectGraph).RegisterNode(new EffectNode<ColorSourceEffect>(), new ColorSourceEffect()));
            _ = Assert.ThrowsException<InvalidOperationException>(() => default(EffectGraph).RegisterAndSetOutputNode(new ColorSourceEffect()));
            _ = Assert.ThrowsException<InvalidOperationException>(() => default(EffectGraph).RegisterAndSetOutputNode(new EffectNode<ColorSourceEffect>(), new ColorSourceEffect()));

            effectGraph.RegisterAndSetOutputNode(new EffectNode<ColorSourceEffect>(), new ColorSourceEffect());
        }

        protected override void ConfigureEffectGraph(EffectGraph effectGraph)
        {
            _ = Assert.ThrowsException<InvalidOperationException>(() => default(EffectGraph).GetNode(new EffectNode<ColorSourceEffect>()));
        }
    }

    [D2DInputCount(0)]
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
