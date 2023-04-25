using System;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Uwp;
using Microsoft.Graphics.Canvas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.UI.Tests;

[TestClass]
[TestCategory("PixelShaderEffect<T>")]
public partial class PixelShaderEffectTests
{
    [TestMethod]
    public unsafe void ConstantBuffer_IsInitiallySetToDefault()
    {
        PixelShaderEffect<ShaderWithSomeProperties> effect = new();

        ShaderWithSomeProperties shader = effect.ConstantBuffer;
        ShaderWithSomeProperties defaultShader = default;

        ReadOnlySpan<byte> actual = new(&shader, sizeof(ShaderWithSomeProperties));
        ReadOnlySpan<byte> expected = new(&defaultShader, sizeof(ShaderWithSomeProperties));

        // The default constant buffer is set to 0, and it can be retrieved correctly
        Assert.IsTrue(actual.SequenceEqual(expected));
    }

    [TestMethod]
    public unsafe void ConstantBuffer_RoundTripsCorrectly()
    {
        ShaderWithSomeProperties shader = new(
            A: 3.14f,
            B: 123456,
            C: new int2(111, -222),
            D: new uint2x2(8888, 123456, 98765, 192837465),
            E: new float2(-3.14f, 6.28f));

        PixelShaderEffect<ShaderWithSomeProperties> effect = new() { ConstantBuffer = shader };

        ShaderWithSomeProperties roundTripShader = effect.ConstantBuffer;

        ReadOnlySpan<byte> expected = new(&shader, sizeof(ShaderWithSomeProperties));
        ReadOnlySpan<byte> actual = new(&roundTripShader, sizeof(ShaderWithSomeProperties));

        // The resulting constant buffer matches the source, when the effect is not realized
        Assert.IsTrue(actual.SequenceEqual(expected));

        // Draw the effect, to force realization
        using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
        using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
        {
            drawingSession.DrawImage(effect);
        }

        roundTripShader = effect.ConstantBuffer;
        actual = new(&roundTripShader, sizeof(ShaderWithSomeProperties));

        // The shader also matches when retrieving it through the realized effect
        Assert.IsTrue(actual.SequenceEqual(expected));

        // Draw the effect on a different device (this forces it to unrealize and re-realize).
        // This also acts as a unit test for the device changed logic, since that's used here.
        using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
        using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
        {
            drawingSession.DrawImage(effect);
        }

        roundTripShader = effect.ConstantBuffer;
        actual = new(&roundTripShader, sizeof(ShaderWithSomeProperties));

        // The shader also matches when retrieving it through the realized effect
        Assert.IsTrue(actual.SequenceEqual(expected));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void SourcesOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        PixelShaderEffect<ShaderWith0Inputs> effect0 = new();

        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect0.Sources[0] = null);
        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect0.Sources[1] = null);

        PixelShaderEffect<ShaderWith1Input> effect1 = new();

        effect1.Sources[0] = null;

        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect1.Sources[1] = null);
        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect1.Sources[2] = null);

        PixelShaderEffect<ShaderWith1Input> effect2 = new();

        effect2.Sources[0] = null;
        effect2.Sources[1] = null;

        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect2.Sources[2] = null);
        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect2.Sources[3] = null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NullTransformMapper_UnrealizedEffect_ThrowsArgumentNullException()
    {
        PixelShaderEffect<ShaderWith0Inputs> effect = new();

        effect.TransformMapper = null!;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NullTransformMapper_RealizedEffect_ThrowsArgumentNullException()
    {
        PixelShaderEffect<ShaderWith0Inputs> effect = new();

        using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
        using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
        {
            drawingSession.DrawImage(effect);
        }

        effect.TransformMapper = null!;
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public unsafe void GraphCycle_IsDetectedCorrectly()
    {
        PixelShaderEffect<ShaderWith0Inputs> effect1 = new();
        PixelShaderEffect<ShaderWith2Inputs> effect2 = new();
        PixelShaderEffect<ShaderWith1Input> effect3 = new();
        PixelShaderEffect<ShaderWith1Input> effect4 = new();
        PixelShaderEffect<ShaderWith2Inputs> effect5 = new();
        PixelShaderEffect<ShaderWith2Inputs> effect6 = new();
        PixelShaderEffect<ShaderWith0Inputs> effect7 = new();

        // Build a graph with a cycle in it:
        //
        //     ┌───┐   ┌───┐   ┌───┐
        //     │ 1 ├──►│ 2 ├──►│ 3 ├──────────┐
        //     └───┘   └───┘   └───┘          ▼
        // ┌───┐   ┌───┐ ▲       │  ┌───┐   ┌───┐
        // │ 7 ├──►│ 6 ├─┘       └─►│ 4 ├──►│ 5 │
        // └───┘   └───┘            └───┘   └───┘
        //           ▲                 │
        //           └─────────────────┘
        //
        // The cycle is 4 -> 6 -> 2 -> 3 -> 4.
        effect2.Sources[0] = effect1;
        effect2.Sources[1] = effect6;
        effect3.Sources[0] = effect2;
        effect4.Sources[0] = effect3;
        effect5.Sources[0] = effect3;
        effect5.Sources[1] = effect4;
        effect6.Sources[0] = effect4;
        effect6.Sources[1] = effect7;

        using ComPtr<ID2D1Image> d2D1Image = default;

        // Realize the effect for it to detect a graph cycle. We specifically don't want to draw
        // the effect, because in that case we'd still get an exception from D2D, even if the
        // effect from ComputeSharp failed to detect a graph cycle. We want to make sure the
        // detection and subsequent error is coming from our wrapper, not from D2D's logic.
        Win2DHelper.GetD2DImage(effect5, new CanvasDevice(), d2D1Image.GetAddressOf());
    }

    [D2DInputCount(0)]
    [AutoConstructor]
    private partial struct ShaderWithSomeProperties : ID2D1PixelShader
    {
        public float A;
        public int B;
        public int2 C;
        public uint2x2 D;
        public float2 E;

        public float4 Execute()
        {
            return default;
        }
    }

    [D2DInputCount(0)]
    private partial struct ShaderWith0Inputs : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return default;
        }
    }

    [D2DInputCount(1)]
    [D2DInputSimple(0)]
    private partial struct ShaderWith1Input : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return D2D.GetInput(0);
        }
    }

    [D2DInputCount(2)]
    [D2DInputSimple(0)]
    [D2DInputSimple(1)]
    private partial struct ShaderWith2Inputs : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return D2D.GetInput(0) + D2D.GetInput(1);
        }
    }
}
