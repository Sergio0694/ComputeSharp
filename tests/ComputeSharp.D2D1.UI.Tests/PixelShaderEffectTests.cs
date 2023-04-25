using System;
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
}
