using System.IO;
using System.Reflection;
using ComputeSharp.BokehBlur.Processors;
using ComputeSharp.D2D1.Tests.Effects;
using ComputeSharp.D2D1.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("EndToEnd")]
public class EndToEndTests
{
    [AssemblyInitialize]
    public static void ConfigureImageSharp(TestContext _)
    {
        Configuration.Default.PreferContiguousImageBuffers = true;
    }

    [TestMethod]
    public unsafe void Invert()
    {
        D2D1TestRunner.RunAndCompareShader(new InvertEffect(), null, "Landscape.png", "Landscape_Inverted.png");
    }

    [TestMethod]
    public unsafe void InvertWithThreshold()
    {
        D2D1TestRunner.RunAndCompareShader(new InvertWithThresholdEffect(1), null, "Landscape.png", "Landscape_Inverted.png");
    }

    [TestMethod]
    public unsafe void Pixelate()
    {
        D2D1TestRunner.RunAndCompareShader(
            new PixelateEffect.Shader(new PixelateEffect.Shader.Constants(1280, 840, 16)),
            static () => new PixelateEffect(),
            "Landscape.png",
            "Landscape_Pixelate.png");
    }

    [TestMethod]
    public unsafe void ZonePlate()
    {
        D2D1TestRunner.RunAndCompareShader(new ZonePlateEffect(1280, 720, 800), null, 1280, 720, "ZonePlate.png");
    }

    [TestMethod]
    public unsafe void CheckerboardClip()
    {
        D2D1TestRunner.RunAndCompareShader(new CheckerboardClipEffect(1280, 840, 32), null, "Landscape.png", "Landscape_CheckerboardClip.png");
    }

    [TestMethod]
    public void BokehBlur()
    {
        BokehBlurEffect bokehBlurEffect = new(32, 1);

        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");
        string temporaryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "temp");

        bokehBlurEffect.ApplyEffect(
            Path.Combine(assetsPath, "Landscape.png"),
            Path.Combine(temporaryPath, "Landscape_BokehBlur.png"));
    }
}