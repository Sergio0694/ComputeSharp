using ComputeSharp.D2D1.Uwp;
using Microsoft.Graphics.Canvas;
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

        using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
        using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
        {
            drawingSession.DrawImage(effect);
        }

        Assert.AreEqual(effect.NumberOfBuildEffectGraphCalls, 1);
        Assert.AreEqual(effect.NumberOfConfigureEffectGraphCalls, 1);

        effect.Value = 42;

        using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
        using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
        {
            drawingSession.DrawImage(effect);
        }

        Assert.AreEqual(effect.NumberOfBuildEffectGraphCalls, 1);
        Assert.AreEqual(effect.NumberOfConfigureEffectGraphCalls, 2);

        effect.ValueWithReload = 123;

        using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
        using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
        {
            drawingSession.DrawImage(effect);
        }

        Assert.AreEqual(effect.NumberOfBuildEffectGraphCalls, 2);
        Assert.AreEqual(effect.NumberOfConfigureEffectGraphCalls, 3);
    }

    private sealed class EffectWithNoInputs : CanvasEffect
    {
        private PixelShaderEffect<ShaderWithNoInputs>? effect;

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

        protected override ICanvasImage BuildEffectGraph()
        {
            NumberOfBuildEffectGraphCalls++;

            return this.effect = new PixelShaderEffect<ShaderWithNoInputs>();
        }

        protected override void ConfigureEffectGraph()
        {
            NumberOfConfigureEffectGraphCalls++;

            this.effect!.ConstantBuffer = new ShaderWithNoInputs(this.value);
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
