using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Helpers;
using ComputeSharp.SwapChain.Shaders.D2D1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
public class ShadersTests
{
    [TestMethod]
    public void HelloWorld()
    {
        RunTest<HelloWorld>();
    }

    [TestMethod]
    public void ColorfulInfinity()
    {
        RunTest<ColorfulInfinity>();
    }

    [TestMethod]
    public void FractalTiling()
    {
        RunTest<FractalTiling>();
    }

    [TestMethod]
    public void MengerJourney()
    {
        RunTest<MengerJourney>(0.000011f);
    }

    // This test and the other 3 below are skipped because they do produce valid result,
    // but the resulting images are different than the ones used as reference, so they
    // cannot be compared. They can be uncommented once the reason why they are seemingly
    // being randomized and producing different outputs has been identified and resolved.
    [TestMethod]
    [Ignore]
    public void TwoTiledTruchet()
    {
        RunTest<TwoTiledTruchet>();
    }

    [TestMethod]
    public void Octagrams()
    {
        RunTest<Octagrams>();
    }

    [TestMethod]
    public void ProteanClouds()
    {
        RunTest<ProteanClouds>();
    }

    [TestMethod]
    [Ignore]
    public void PyramidPattern()
    {
        RunTest<PyramidPattern>();
    }

    [TestMethod]
    [Ignore]
    public void TriangleGridContouring()
    {
        RunTest<TriangleGridContouring>();
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
                extents: [(uint)texture.Width, (uint)texture.Height],
                bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
                channelDepth: D2D1ChannelDepth.Four,
                filter: D2D1Filter.MinMagMipLinear,
                extendModes: [D2D1ExtendMode.Mirror, D2D1ExtendMode.Mirror],
                data: MemoryMarshal.AsBytes(pixels.Span),
                strides: [(uint)(texture.Width * sizeof(Rgba32))]);
        }

        ContouredLayers shader = new(0f, new int2(1280, 720));

        D2D1TestRunner.RunAndCompareShader(in shader, 1280, 720, $"{nameof(ContouredLayers)}.png", nameof(ContouredLayers), resourceTextures: (0, resourceTextureManager));
    }

    [TestMethod]
    public void TerracedHills()
    {
        RunTest<TerracedHills>(0.000026f);
    }

    private static void RunTest<T>(float threshold = 0.00001f)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        T shader = (T)Activator.CreateInstance(typeof(T), 0f, new int2(1280, 720))!;

        D2D1TestRunner.RunAndCompareShader(in shader, 1280, 720, $"{typeof(T).Name}.png", typeof(T).Name, threshold);
    }
}