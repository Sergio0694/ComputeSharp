using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
public partial class PixelTypeTests
{
    [TestMethod]
    public unsafe void Bgra32_ToPixel()
    {
        Bgra32 color = new(0x20, 0x4A, 0x7C, 0xE4);

        float4 pixel = color.ToPixel();

        Assert.AreEqual(0.1254902f, pixel.R, delta: 0.0001f);
        Assert.AreEqual(0.2901961f, pixel.G, delta: 0.0001f);
        Assert.AreEqual(0.4862745f, pixel.B, delta: 0.0001f);
        Assert.AreEqual(0.89411765f, pixel.A, delta: 0.0001f);
    }

    [TestMethod]
    public unsafe void R16_ToPixel()
    {
        R16 color = new(0x2020);

        float pixel = color.ToPixel();

        Assert.AreEqual(0.1254902f, pixel, delta: 0.0001f);
    }

    [TestMethod]
    public unsafe void R8_ToPixel()
    {
        R8 color = new(0x20);

        float pixel = color.ToPixel();

        Assert.AreEqual(0.1254902f, pixel, delta: 0.0001f);
    }

    [TestMethod]
    public unsafe void Rg16_ToPixel()
    {
        Rg16 color = new(0x20, 0x4A);

        float2 pixel = color.ToPixel();

        Assert.AreEqual(0.1254902f, pixel.R, delta: 0.0001f);
        Assert.AreEqual(0.2901961f, pixel.G, delta: 0.0001f);
    }

    [TestMethod]
    public unsafe void Rg32_ToPixel()
    {
        Rg32 color = new(0x2020, 0x4A4A);

        float2 pixel = color.ToPixel();

        Assert.AreEqual(0.1254902f, pixel.R, delta: 0.0001f);
        Assert.AreEqual(0.2901961f, pixel.G, delta: 0.0001f);
    }

    [TestMethod]
    public unsafe void Rgba32_ToPixel()
    {
        Rgba32 color = new(0x20, 0x4A, 0x7C, 0xE4);

        float4 pixel = color.ToPixel();

        Assert.AreEqual(0.1254902f, pixel.R, delta: 0.0001f);
        Assert.AreEqual(0.2901961f, pixel.G, delta: 0.0001f);
        Assert.AreEqual(0.4862745f, pixel.B, delta: 0.0001f);
        Assert.AreEqual(0.89411765f, pixel.A, delta: 0.0001f);
    }

    [TestMethod]
    public unsafe void Rgba64_ToPixel()
    {
        Rgba64 color = new(0x2020, 0x4A4A, 0x7C7C, 0xE4E4);

        float4 pixel = color.ToPixel();

        Assert.AreEqual(0.1254902f, pixel.R, delta: 0.0001f);
        Assert.AreEqual(0.2901961f, pixel.G, delta: 0.0001f);
        Assert.AreEqual(0.4862745f, pixel.B, delta: 0.0001f);
        Assert.AreEqual(0.89411765f, pixel.A, delta: 0.0001f);
    }
}