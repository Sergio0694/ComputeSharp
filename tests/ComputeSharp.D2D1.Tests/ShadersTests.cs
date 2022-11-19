using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("Shaders")]
public class ShadersTests
{
    [TestMethod]
    public void HelloWorld()
    {
        RunTest<Shaders.HelloWorld>();
    }

    [TestMethod]
    public void ColorfulInfinity()
    {
        RunTest<Shaders.ColorfulInfinity>();
    }

    [TestMethod]
    public void FractalTiling()
    {
        RunTest<Shaders.FractalTiling>();
    }

    [TestMethod]
    public void MengerJourney()
    {
        RunTest<Shaders.MengerJourney>(0.000011f);
    }

    // This test and the other 3 below are skipped because they do produce valid result,
    // but the resulting images are different than the ones used as reference, so they
    // cannot be compared. They can be uncommented once the reason why they are seemingly
    // being randomized and producing different outputs has been identified and resolved.
    [TestMethod]
    [Ignore]
    public void TwoTiledTruchet()
    {
        RunTest<Shaders.TwoTiledTruchet>();
    }

    [TestMethod]
    public void Octagrams()
    {
        RunTest<Shaders.Octagrams>();
    }

    [TestMethod]
    public void ProteanClouds()
    {
        RunTest<Shaders.ProteanClouds>();
    }

    [TestMethod]
    [Ignore]
    public void PyramidPattern()
    {
        RunTest<Shaders.PyramidPattern>();
    }

    [TestMethod]
    [Ignore]
    public void TriangleGridContouring()
    {
        RunTest<Shaders.TriangleGridContouring>();
    }

    [TestMethod]
    [Ignore]
    public unsafe void ContouredLayers()
    {
        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string expectedPath = Path.Combine(assemblyPath, "Assets", "Textures", "RustyMetal.png");

        D2D1ResourceTextureManager resourceTextureManager;

        using (Image<Rgba32> texture = Image.Load<Rgba32>(expectedPath))
        {
            if (!texture.DangerousTryGetSinglePixelMemory(out Memory<Rgba32> pixels))
            {
                Assert.Inconclusive();
            }

            resourceTextureManager = new D2D1ResourceTextureManager(
                extents: stackalloc uint[] { (uint)texture.Width, (uint)texture.Height },
                bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
                channelDepth: D2D1ChannelDepth.Four,
                filter: D2D1Filter.MinMagMipLinear,
                extendModes: stackalloc D2D1ExtendMode[] { D2D1ExtendMode.Mirror, D2D1ExtendMode.Mirror },
                data: MemoryMarshal.AsBytes(pixels.Span),
                strides: stackalloc uint[] { (uint)(texture.Width * sizeof(Rgba32)) });
        }

        Shaders.ContouredLayers shader = new(0f, new int2(1280, 720));

        D2D1TestRunner.RunAndCompareShader(in shader, 1280, 720, $"{nameof(Shaders.ContouredLayers)}.png", nameof(Shaders.ContouredLayers), resourceTextures: (0, resourceTextureManager));
    }

    [TestMethod]
    public void TerracedHills()
    {
        RunTest<Shaders.TerracedHills>(0.000026f);
    }

    private static void RunTest<T>(float threshold = 0.00001f)
        where T : unmanaged, ID2D1PixelShader
    {
        T shader = (T)Activator.CreateInstance(typeof(T), 0f, new int2(1280, 720))!;

        D2D1TestRunner.RunAndCompareShader(in shader, 1280, 720, $"{typeof(T).Name}.png", typeof(T).Name, threshold);
    }
}